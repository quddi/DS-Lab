public class SkipListNode<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }
    public SkipListNode<TKey, TValue>[] Next { get; set; }

    public SkipListNode(TKey key, TValue value, int maxLevel)
    {
        Key = key;
        Value = value;
        Next = new SkipListNode<TKey, TValue>[maxLevel];
    }
}
