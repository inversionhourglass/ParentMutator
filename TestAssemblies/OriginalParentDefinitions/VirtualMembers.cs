using System;
using System.Collections.Generic;

namespace OriginalParentDefinitions
{
    public class VirtualMembers
    {
        public string Value;
        public Action<List<object>> Action;

        public virtual string this[int index]
        {
            get => Value;
            set => Value = value;
        }

        public virtual event Action<List<object>> Event
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

        public virtual string Property { get; set; }

        public virtual string Method() => nameof(VirtualMembers);
    }
}
