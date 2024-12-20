using OriginalParentDefinitions;
using System;
using System.Collections.Generic;

namespace NewParentDefinitions
{
    public class NewVirtualMembersOverride : VirtualMembersOverride
    {
        public override string this[int index]
        {
            set { }
            get => nameof(NewVirtualMembersOverride);
        }

        public override event Action<List<object>> Event
        {
            add { }
            remove { }
        }

        public override string Property
        {
            get => nameof(NewVirtualMembersOverride);
            set { }
        }

        public override string Method() => nameof(NewVirtualMembersOverride);
    }
}
