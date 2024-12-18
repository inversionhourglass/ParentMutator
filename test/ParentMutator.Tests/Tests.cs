using ChildDefinitions;
using NewParentDefinitions;
using OriginalParentDefinitions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ParentMutator.Tests
{
    public class Tests
    {
        private static readonly WeavedAssembly Assembly;

        static Tests()
        {
            Assembly = new("ChildDefinitions");
        }

        [Fact]
        public void EmptyTest()
        {
            var instance = Assembly.GetInstance(nameof(ChildEmpty));
            var type = (Type)instance.GetType();

            Assert.Equal(typeof(NewEmpty), type.BaseType);
        }

        [Fact]
        public void ConstructorTest()
        {
            var name = "test";
            var instance = Assembly.GetInstance(nameof(ChildConstructor), args: [name]);
            var actualName = (string)instance.Name;

            Assert.Equal(typeof(NewConstructor), ((Type)instance.GetType()).BaseType);
            Assert.Equal(NewConstructor.Format(name), actualName);
        }

        [Fact]
        public void VirtualTest()
        {
            var instance = Assembly.GetInstance(nameof(ChildVirtualMembers));

            var type = (Type)instance.GetType();

            Assert.Equal(typeof(NewVirtualMembers), type.BaseType);

            var value = "test";
            Assert.NotEqual(value, (string)instance[1]);
            instance[1] = value;
            Assert.Equal(value, (string)instance[1]);

            var eventInfo = type.GetEvent("Event")!;
            Action<List<object>> act = list => list.Add(new());
            var action = (Action<List<object>>)instance.Action;
            Assert.Null(action);
            eventInfo.AddEventHandler(instance, act);
            action = (Action<List<object>>)instance.Action;
            Assert.NotNull(action);
            eventInfo.RemoveEventHandler(instance, act);
            action = (Action<List<object>>)instance.Action;
            Assert.Null(action);

            Assert.NotEqual(value, (string)instance.Property);
            instance.Property = value;
            Assert.Equal(value, (string)instance.Property);

            Assert.Equal(nameof(VirtualMembers), (string)instance.Method());
        }

        [Fact]
        public void VirtualOverrideTest()
        {
            var instance = Assembly.GetInstance(nameof(ChildVirtualMembersOverride));

            var type = (Type)instance.GetType();

            Assert.Equal(typeof(NewVirtualMembersOverride), type.BaseType);

            var value = "test";
            Assert.Equal(nameof(NewVirtualMembersOverride), (string)instance[1]);
            instance[1] = value;
            Assert.Equal(nameof(NewVirtualMembersOverride), (string)instance[1]);

            var eventInfo = type.GetEvent("Event")!;
            Action<List<object>> act = list => list.Add(new());
            var action = (Action<List<object>>)instance.Action;
            Assert.Null(action);
            eventInfo.AddEventHandler(instance, act);
            action = (Action<List<object>>)instance.Action;
            Assert.Null(action);
            eventInfo.RemoveEventHandler(instance, act);
            action = (Action<List<object>>)instance.Action;
            Assert.Null(action);

            Assert.Equal(nameof(NewVirtualMembersOverride), (string)instance.Property);
            instance.Property = value;
            Assert.Equal(nameof(NewVirtualMembersOverride), (string)instance.Property);

            Assert.Equal(nameof(NewVirtualMembersOverride), (string)instance.Method());
        }

        [Fact]
        public void NonVirtualTest()
        {
            var instance = Assembly.GetInstance(nameof(ChildNonVirtualMembers));

            var type = (Type)instance.GetType();

            Assert.Equal(typeof(NewNonVirtualMembers), type.BaseType);

            var value = "test";
            Assert.NotEqual(value, (string)instance[1]);
            instance[1] = value;
            Assert.Equal(value, (string)instance[1]);

            var eventInfo = type.GetEvent("Event")!;
            Action<List<object>> act = list => list.Add(new());
            var action = (Action<List<object>>)instance.Action;
            Assert.Null(action);
            eventInfo.AddEventHandler(instance, act);
            action = (Action<List<object>>)instance.Action;
            Assert.NotNull(action);
            eventInfo.RemoveEventHandler(instance, act);
            action = (Action<List<object>>)instance.Action;
            Assert.Null(action);

            Assert.NotEqual(value, (string)instance.Property);
            instance.Property = value;
            Assert.Equal(value, (string)instance.Property);

            Assert.Equal(nameof(NonVirtualMembers), (string)instance.Method());
        }

        [Fact]
        public void NonVirtualHidTest()
        {
            var instance = Assembly.GetInstance(nameof(ChildNonVirtualMembersHid));

            var type = (Type)instance.GetType();

            Assert.Equal(typeof(NewNonVirtualMembersHid), type.BaseType);

            var value = "test";
            Assert.Equal(nameof(NewNonVirtualMembersHid), (string)instance[1]);
            instance[1] = value;
            Assert.Equal(nameof(NewNonVirtualMembersHid), (string)instance[1]);
            
            var eventInfo = type.GetEvent("Event")!;
            Action<List<object>> act = list => list.Add(new());
            var action = (Action<List<object>>)instance.Action;
            Assert.Null(action);
            eventInfo.AddEventHandler(instance, act);
            action = (Action<List<object>>)instance.Action;
            Assert.Null(action);
            eventInfo.RemoveEventHandler(instance, act);
            action = (Action<List<object>>)instance.Action;
            Assert.Null(action);
            
            Assert.Equal(nameof(NewNonVirtualMembersHid), (string)instance.Property);
            instance.Property = value;
            Assert.Equal(nameof(NewNonVirtualMembersHid), (string)instance.Property);
            
            Assert.Equal(nameof(NewNonVirtualMembersHid), (string)instance.Method());
        }

        [Fact]
        public void StaticMemberTest()
        {
            var sInstance = Assembly.GetStaticInstance(nameof(ChildStaticMembers));

            var action = (Action<List<object>>)sInstance.GetAction();
            Assert.Null(action);
            sInstance.AddEvent(action);
            action = (Action<List<object>>)sInstance.GetAction();
            Assert.NotNull(action);
            sInstance.RemoveEvent(action);
            action = (Action<List<object>>)sInstance.GetAction();
            Assert.Null(action);

            var value = "test";
            Assert.NotEqual(value, (string)sInstance.GetProperty());
            sInstance.SetProperty(value);
            Assert.Equal(value, (string)sInstance.GetProperty());

            Assert.Equal(nameof(StaticMembers), (string)sInstance.CallMethod());
        }

        [Fact]
        public void StaticMemberHidTest()
        {
            var sInstance = Assembly.GetStaticInstance(nameof(ChildStaticMembersHid));

            var action = (Action<List<object>>)sInstance.GetAction();
            Assert.Null(action);
            sInstance.AddEvent(action);
            action = (Action<List<object>>)sInstance.GetAction();
            Assert.Null(action);
            sInstance.RemoveEvent(action);
            action = (Action<List<object>>)sInstance.GetAction();
            Assert.Null(action);

            var value = "test";
            Assert.Equal(nameof(NewStaticMembersHid), (string)sInstance.GetProperty());
            sInstance.SetProperty(value);
            Assert.Equal(nameof(NewStaticMembersHid), (string)sInstance.GetProperty());

            Assert.Equal(nameof(NewStaticMembersHid), (string)sInstance.CallMethod());
        }

        [Fact]
        public void ExplicitInterfaceTest()
        {
            var instance = Assembly.GetInstance(nameof(ChildExplicitInterface));

            Assert.Equal(typeof(NewExplicitInterface), ((Type)instance.GetType()).BaseType);

            var typeName = (string)instance.GetTypeName();

            Assert.Equal(nameof(ExplicitInterface), typeName);
        }

        [Fact]
        public void ExplicitInterfaceOverrideTest()
        {
            var instance = Assembly.GetInstance(nameof(ChildExplicitInterfaceOverride));

            Assert.Equal(typeof(NewExplicitInterfaceOverride), ((Type)instance.GetType()).BaseType);

            var typeName = (string)instance.GetTypeName();

            Assert.Equal(nameof(NewExplicitInterfaceOverride), typeName);
        }
    }
}
