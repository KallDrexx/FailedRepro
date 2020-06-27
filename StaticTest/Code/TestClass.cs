using System.Collections.Generic;

namespace StaticTest.Code
{
    public class TestClass
    {
        public string Root { get; set; } // Item #1
        public List<Inner> Inners { get; set; }
        
        public class Inner
        {
            public string First { get; set; } // Item #2
            public string Second { get; set; } // Item #3
        }
    }
}