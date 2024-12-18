using NewParentDefinitions;
using OriginalParentDefinitions;
using ParentMutator;
using System;
using System.Collections.Generic;

[assembly: MutateParent(typeof(StaticMembers), typeof(NewStaticMembers))]

namespace ChildDefinitions
{
    public class ChildStaticMembersHid : StaticMembers
    {
        public static void AddEvent(Action<List<object>> action)
        {
            Event += action;
        }

        public static void RemoveEvent(Action<List<object>> action)
        {
            Event -= action;
        }

        public static void SetProperty(string value)
        {
            Property = value;
        }

        public static string GetProperty(string value)
        {
            return Property;
        }

        public static string CallMethod()
        {
            return Method();
        }
    }
}
