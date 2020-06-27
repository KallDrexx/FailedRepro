using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StaticTest.Code
{
    public class PropertyMapping
    {
        public PropertyInfo Property { get; }
        public bool IsCollection { get; }
        public Type InnerType { get; }
        public int? ItemNumber { get; }

        public PropertyMapping(PropertyInfo property, int? itemNumber)
        {
            Property = property;
            ItemNumber = itemNumber;

            if (property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
            {
                IsCollection = true;
                InnerType = property.PropertyType.GetGenericArguments().First();
            }
        }
    }
}