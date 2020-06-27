using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StaticTest.Code
{
    public static class Deserializer
    {
        public static readonly PropertyMapping InnerCollectionMapping =
            new PropertyMapping(
                typeof(TestClass).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .First(x => x.Name == "Inners"), null);

        public static void DeserializeIntoObject(TestClass message, IEnumerable<ItemDataPair> itemDataPairs)
        {
            var createdLists = new Dictionary<PropertyMapping, IList>();
            var lastComplexCollectionItem = new object();

            foreach (var itemDataPair in itemDataPairs)
            {
                DeserializeFromPath(message, itemDataPair, createdLists, ref lastComplexCollectionItem, false);
            }
        }

        private static void DeserializeFromPath(TestClass parent, ItemDataPair itemDataPair, IDictionary<PropertyMapping, IList> createdLists, ref object lastComplexCollectionItem, bool isInner)
        {
            switch (isInner)
            {
                case false:
                    DeserializeTestClass(parent, itemDataPair, createdLists, ref lastComplexCollectionItem);
                    break;

                case true:
                    DeserializeComplexCollection(parent, itemDataPair, createdLists, ref lastComplexCollectionItem);
                    break;
            }
        }

        private static void DeserializeTestClass(TestClass testClass, ItemDataPair itemDataPair, IDictionary<PropertyMapping, IList> createdLists, ref object lastComplexCollectionItem)
        {
            if (itemDataPair.Key == 1)
            {
                testClass.Root = itemDataPair.Value;
            }
            else
            {
                DeserializeFromPath(testClass, itemDataPair, createdLists, ref lastComplexCollectionItem, true);
            }
        }

        private static void DeserializeComplexCollection(object parent, ItemDataPair itemDataPair, IDictionary<PropertyMapping, IList> createdLists, ref object lastComplexCollectionItem)
        {
            if (!createdLists.TryGetValue(InnerCollectionMapping, out var list))
            {
                list = CreateListOfType(InnerCollectionMapping.InnerType);
                createdLists.Add(InnerCollectionMapping, list);
                InnerCollectionMapping.Property.SetValue(parent, list);
            }

            if (lastComplexCollectionItem.GetType() != typeof(TestClass.Inner) || itemDataPair.Key == 2)
            {
                // Since this is an item data pair with a key of 2 OR the last item we dealt with wasn't an Inner,
                // than this is a *new* instance of TestClass.Inner
                lastComplexCollectionItem = new TestClass.Inner();
                list.Add(lastComplexCollectionItem);
            }

            switch (itemDataPair.Key)
            {
                case 2:
                    ((TestClass.Inner) lastComplexCollectionItem).First = itemDataPair.Value;
                    break;
                
                case 3:
                    ((TestClass.Inner) lastComplexCollectionItem).Second = itemDataPair.Value;
                    break;
            }
        }

        private static IList CreateListOfType(Type innerType)
        {
            var listType = typeof(List<>).MakeGenericType(innerType);
            return (IList) Activator.CreateInstance(listType);
        }
    }
}