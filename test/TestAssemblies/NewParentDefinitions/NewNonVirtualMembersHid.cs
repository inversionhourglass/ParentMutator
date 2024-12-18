using OriginalParentDefinitions;
using System;
using System.Collections.Generic;

namespace NewParentDefinitions
{
    public class NewNonVirtualMembersHid : NonVirtualMembersHid
    {
        public new string this[int index]
        {
            set { }
            get => nameof(NewNonVirtualMembersHid);
        }

        public new event Action<List<object>> Event
        {
            add { }
            remove { }
        }

        public new string Property
        {
            get => nameof(NewNonVirtualMembersHid);
            set { }
        }

        public new string Method() => nameof(NewNonVirtualMembersHid);
    }
}
