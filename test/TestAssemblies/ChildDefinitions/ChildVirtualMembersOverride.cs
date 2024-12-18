using NewParentDefinitions;
using OriginalParentDefinitions;
using ParentMutator;
using System;
using System.Collections.Generic;

[assembly: MutateParent(typeof(VirtualMembersOverride), typeof(NewVirtualMembersOverride))]

namespace ChildDefinitions
{
    public class ChildVirtualMembersOverride : VirtualMembersOverride
    {
        public override string this[int index] { get => base[index]; set => base[index] = value; }

        public override event Action<List<object>> Event { add => base.Event += value; remove => base.Event -= value; }

        public override string Property { get => base.Property; set => base.Property = value; }

        public override string Method()
        {
            return base.Method();
        }
    }
}
