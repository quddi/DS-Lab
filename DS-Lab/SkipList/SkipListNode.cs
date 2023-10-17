using System;
using System.Collections.Generic;
using System.Linq;

public class SkipListNode<TKey, TValue> where TKey : IComparable
{
    public TKey Key { get; }
    public TValue Value { get; }
    private List<SkipListNode<TKey, TValue>> Next { get; }

    public int ChildrenCount => Next.Count;

    public SkipListNode<TKey, TValue> this[int index]
    {
        get => index >= 0 && index < ChildrenCount ? Next[index] : null;
        set
        {
            if (index < ChildrenCount) Next[index] = value;
            if (index == ChildrenCount) Next.Add(value);
        }
    }

    public SkipListNode(TKey key, TValue value)
    {
        Key = key;
        Value = value;
        Next = new List<SkipListNode<TKey, TValue>>();
    }

    public void AddNext(SkipListNode<TKey, TValue> node, int? position = null)
    {
        if (position == null) Next.Add(node);
        else Next.Insert(position.Value, node);
    }
}
