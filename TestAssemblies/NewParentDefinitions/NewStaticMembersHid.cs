using OriginalParentDefinitions;
using System;
using System.Collections.Generic;

namespace NewParentDefinitions
{
    public class NewStaticMembersHid : StaticMembersHid
    {
        public new static event Action<List<object>> Event
        {
            add { }

            remove { }
        }

        public new static string Property
        {
            get => nameof(NewStaticMembersHid);
            set { }
        }

        public new static string Method() => nameof(NewStaticMembersHid);
    }
}
