using System;
using System.Collections.Generic;

namespace OriginalParentDefinitions
{
    public class NonVirtualMembersHid
    {
        public string Value;
        public Action<List<object>> Action;

        public string this[int index]
        {
            get => Value;
            set => Value = value;
        }

        public event Action<List<object>> Event
        {
            add
            {
                if (Action == null)
                {
                    Action = value;
                }
                else
                {
                    Action += value;
                }
            }

            remove
            {
                if (Action != null)
                {
                    Action -= value;
                }
            }
        }

        public string Property { get; set; }

        public string Method() => nameof(NonVirtualMembersHid);
    }
}
