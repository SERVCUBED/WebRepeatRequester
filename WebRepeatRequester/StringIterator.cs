namespace WebRepeatRequester
{
    class StringIterator
    {
        string[] _items;
        int prevIndex = -1;

        public StringIterator(string concat, char separator)
        {
            _items = concat.Split(separator);
        }

        public string Next()
        {
            prevIndex++;

            if (prevIndex > _items.Length - 1)
                prevIndex = 0;

            return _items[prevIndex];
        }
    }
}
