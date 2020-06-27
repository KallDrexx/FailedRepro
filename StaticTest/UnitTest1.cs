using System;
using Shouldly;
using StaticTest.Code;
using Xunit;

namespace StaticTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var itemDataPairs = new[]
            {
                new ItemDataPair(1, "hi"),
                new ItemDataPair(2, "hi2"),
                new ItemDataPair(3, "hi3"),
                new ItemDataPair(2, "hi2"),
                new ItemDataPair(3, "hi3"),
                new ItemDataPair(2, "hi2"),
                new ItemDataPair(3, "hi3"),
                new ItemDataPair(2, "hi2"),
                new ItemDataPair(3, "hi3"),
                new ItemDataPair(2, "hi2"),
                new ItemDataPair(3, "hi3"),
                new ItemDataPair(2, "hi2"),
                new ItemDataPair(3, "hi3"),
                new ItemDataPair(2, "hi2"),
                new ItemDataPair(3, "hi3"),
                new ItemDataPair(2, "hi2"),
                new ItemDataPair(3, "hi3"),
                new ItemDataPair(2, "hi2"),
                new ItemDataPair(3, "hi3"),
                new ItemDataPair(2, "hi2"),
                new ItemDataPair(3, "hi3"),
                new ItemDataPair(2, "hi2"),
                new ItemDataPair(3, "hi3"),
                new ItemDataPair(2, "hi2"),
                new ItemDataPair(3, "hi3"),
            };
            
            var test = new TestClass();
            var stuff = Deserializer.InnerCollectionMapping;
            Deserializer.DeserializeIntoObject(test, itemDataPairs);
            
            test.Root.ShouldBe("hi");
            test.Inners.ShouldNotBeNull();
            test.Inners.Count.ShouldBe(12);
        }
    }
}