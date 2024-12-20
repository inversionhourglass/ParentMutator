using System.Collections.Generic;
using System.Linq;
using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

namespace ParentMutator.Fody;

public class ModuleWeaver : SimulationModuleWeaver
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ModuleWeaver() : base(false) { }

    public ModuleWeaver(bool testRun) : base(testRun) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected override void ExecuteInternal()
    {
        var mutations = GetAllMutations();

        MutateTypes(ModuleDefinition.Types, mutations);
    }

    private void MutateTypes(Mono.Collections.Generic.Collection<TypeDefinition> typeDefs, Dictionary<string, TypeReference> mutations)
    {
        foreach (var typeDef in typeDefs)
        {
            if (typeDef.HasNestedTypes)
            {
                MutateTypes(typeDef.NestedTypes, mutations);
            }

            if (typeDef.IsEnum || typeDef.IsInterface || typeDef.IsArray || typeDef.IsValueType || typeDef.BaseType == null) continue;

            var baseTypeRef = typeDef.BaseType;
            if (!mutations.TryGetValue(baseTypeRef.FullName, out var newBaseTypeRef))
            {
                if (baseTypeRef is not GenericInstanceType git || !mutations.TryGetValue(git.ElementType.FullName, out newBaseTypeRef)) continue;
                newBaseTypeRef = newBaseTypeRef.MakeGenericInstanceType(git.GenericArguments.ToArray());
            }

            typeDef.BaseType = newBaseTypeRef;

            foreach (var methodDef in typeDef.Methods)
            {
                MutateMethodBody(methodDef.Body, typeDef.BaseType.FullName, newBaseTypeRef);
            }
        }
    }

    private void MutateMethodBody(MethodBody methodBody, string originalBaseTypeFullName, TypeReference newBaseTypeRef)
    {
        var newBaseTypeDef = newBaseTypeRef.ToDefinition();
        var newBaseTypeFullName = newBaseTypeRef.FullName;
        var instructions = methodBody.Instructions;

        foreach (var instruction in instructions)
        {
            if (instruction.OpCode.Code != Code.Call || instruction.Operand is not MethodReference methodRef) continue;

            if (methodRef.DeclaringType.FullName != newBaseTypeFullName) continue;

            var methodDef = methodRef.ToDefinition();
            var newMethodDef = newBaseTypeDef.GetMethod(true, md => IsSameSignature(md, methodDef));
        }
    }

    private bool IsSameSignature(MethodDefinition method1, MethodDefinition method2)
    {
        if (method1.Name != method2.Name) return false;

        if (method1.Parameters.Count != method2.Parameters.Count) return false;

        if (method1.GenericParameters.Count != method2.GenericParameters.Count) return false;

        // // 获取泛型参数映射关系
        // var genericMap = new Dictionary<string, TypeReference>();
        // var declaringType1 = method1.DeclaringType;
        // var declaringType2 = method2.DeclaringType;
        
        // if (declaringType1 is GenericInstanceType genericInstance1 && 
        //     declaringType2.HasGenericParameters)
        // {
        //     for (int i = 0; i < declaringType2.GenericParameters.Count; i++)
        //     {
        //         genericMap[declaringType2.GenericParameters[i].FullName] = 
        //             genericInstance1.GenericArguments[i];
        //     }
        // }

        // // 比较参数类型
        // for (var i = 0; i < method1.Parameters.Count; i++)
        // {
        //     var p1Type = method1.Parameters[i].ParameterType;
        //     var p2Type = method2.Parameters[i].ParameterType;

        //     if (!AreTypesEquivalent(p1Type, p2Type, genericMap))
        //         return false;
        // }

        return true;
    }

    private bool AreTypesEquivalent(TypeReference type1, TypeReference type2, 
        Dictionary<string, TypeReference> genericMap)
    {
        // 处理泛型参数
        if (type2.IsGenericParameter)
        {
            return genericMap.TryGetValue(type2.FullName, out var mappedType) && 
                   type1.FullName == mappedType.FullName;
        }

        // 处理泛型实例类型
        if (type1 is GenericInstanceType git1 && type2 is GenericInstanceType git2)
        {
            if (git1.ElementType.FullName != git2.ElementType.FullName)
                return false;

            for (int i = 0; i < git1.GenericArguments.Count; i++)
            {
                if (!AreTypesEquivalent(git1.GenericArguments[i], 
                    git2.GenericArguments[i], genericMap))
                    return false;
            }
            return true;
        }

        // 直接比较类型全名
        return type1.FullName == type2.FullName;
    }

    private Dictionary<string, TypeReference> GetAllMutations()
    {
        var mutateAttributes = ModuleDefinition.Assembly.CustomAttributes
                                    .Concat(ModuleDefinition.CustomAttributes)
                                    .Where(attr => attr.AttributeType.FullName == Constants.ATTRIBUTE_MUTATE_PARENT)
                                    .ToList();
        
        var map = new Dictionary<string, TypeReference>();

        foreach (var attribute in mutateAttributes)
        {
            var originalParentTypeRef = attribute.ConstructorArguments[0].Value as TypeReference;
            var newParentTypeRef = attribute.ConstructorArguments[1].Value as TypeReference;

            if (originalParentTypeRef == null || newParentTypeRef == null) continue;

            // [MutateParent(typeof(X<>), typeof(Y))] not allowed
            // [MutateParent(typeof(X), typeof(Y<>))] not allowed
            if (originalParentTypeRef.GenericParameters.Count != newParentTypeRef.GenericParameters.Count)
            {
                throw new FodyWeavingException($"Open generic parameters count not match. The original type is {originalParentTypeRef.FullName} and the new type is {newParentTypeRef.FullName}");
            }

            if (!IsInherit(newParentTypeRef, originalParentTypeRef))
            {
                throw new FodyWeavingException($"The new parent type {newParentTypeRef.FullName} must inherit from the original parent type {originalParentTypeRef.FullName}");
            }

            map[originalParentTypeRef.FullName] = this.Import(newParentTypeRef);
        }

        return map;
    }

    private bool IsInherit(TypeReference typeRef, TypeReference baseTypeRef)
    {
        var typeDef = typeRef.ToDefinition();

        while(typeDef != null)
        {
            var baseType = typeDef.BaseType;
            if (baseType == null) return false;

            if (baseType.FullName == baseTypeRef.FullName) return true;

            if (baseType is GenericInstanceType git && git.ElementType.FullName == baseTypeRef.FullName) return true;

            typeDef = baseType.Resolve();
        }

        return false;
    }
}
