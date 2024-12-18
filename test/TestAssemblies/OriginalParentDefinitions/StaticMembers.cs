using System;
using System.Collections.Generic;

namespace OriginalParentDefinitions
{
    public class StaticMembers
    {
        public static Action<List<object>> Action;

        public static event Action<List<object>> Event
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

        public static string Property { get; set; }

        public static string Method() => nameof(NonVirtualMembers);
    }
}
