using System;

namespace ParentMutator;

/// <summary>
/// Specifies the modification of type inheritance, changing the type from the original parent class to a new parent class
/// </summary>
/// <param name="originalParentType">Original parent class type</param>
/// <param name="newParentType">New parent class type</param>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class MutateParentAttribute(Type originalParentType, Type newParentType) : Attribute
{
    /// <summary>
    /// Original parent class type
    /// </summary>
    public Type OriginalParentType => originalParentType;

    /// <summary>
    /// New parent class type
    /// </summary>
    public Type NewParentType => newParentType;
}

