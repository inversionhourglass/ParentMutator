using NewParentDefinitions;
using OriginalParentDefinitions;
using ParentMutator;
using System;
using System.Collections.Generic;

[assembly: MutateParent(typeof(NonVirtualMembers), typeof(NewNonVirtualMembers))]

namespace ChildDefinitions
{
    public class ChildNonVirtualMembers : NonVirtualMembers
    {
        public new string this[int index]
        {
            set => base[index] = value;
            get => base[index];
        }

        public new event Action<List<object>> Event
        {
            add => base.Event += value;
            remove => base.Event -= value;
        }

        public new string Property
        {
            get => base.Property;
            set => base.Property = value;
        }

        public new string Method() => base.Method();
    }
}
