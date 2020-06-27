namespace StaticTest.Code
{
    public class ItemDataPair
    {
        public int Key { get; }
        public string Value { get; }

        public ItemDataPair(int key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}