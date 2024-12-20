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
    }
}
