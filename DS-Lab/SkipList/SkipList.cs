using System;

public class SkipList<TKey, TValue> where TKey : IComparable<TKey>
{
    private int _currentLevel;
    private SkipListNode<TKey, TValue> _head;
    private Random _random = new();

    public int Count { get; private set; }
    
    public int MaxLevel { get; }

    public SkipList(int maxLevel)
    {
        MaxLevel = maxLevel;
        _currentLevel = 1;
        Count = 0;
        _head = new SkipListNode<TKey, TValue>(default, default, MaxLevel);
    }

    public SkipList() : this(3) { }

    public void Add(TKey key, TValue value)
    {
        SkipListNode<TKey, TValue>[] update = new SkipListNode<TKey, TValue>[MaxLevel];
        SkipListNode<TKey, TValue> current = _head;

        for (int i = _currentLevel - 1; i >= 0; i--)
        {
            while (current.Next[i] != null && current.Next[i].Key.CompareTo(key) < 0)
            {
                current = current.Next[i];
            }
            update[i] = current;
        }

        current = current.Next[0];

        if (current == null || !current.Key.Equals(key))
        {
            int newLevel = GetNewNodeLevel();
            if (newLevel > _currentLevel)
            {
                for (int i = _currentLevel; i < newLevel; i++)
                {
                    update[i] = _head;
                }
                _currentLevel = newLevel;
            }

            current = new SkipListNode<TKey, TValue>(key, value, newLevel);

            for (int i = 0; i < newLevel; i++)
            {
                current.Next[i] = update[i].Next[i];
                update[i].Next[i] = current;
            }

            Count++;
        }
        else
        {
            current.Value = value;
        }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        SkipListNode<TKey, TValue> current = _head;

        for (int i = _currentLevel - 1; i >= 0; i--)
        {
            while (current.Next[i] != null && current.Next[i].Key.CompareTo(key) < 0)
            {
                current = current.Next[i];
            }
        }

        current = current.Next[0];

        if (current != null && current.Key.Equals(key))
        {
            value = current.Value;
            return true;
        }

        value = default(TValue);
        return false;
    }

    public bool Remove(TKey key)
    {
        SkipListNode<TKey, TValue>[] update = new SkipListNode<TKey, TValue>[MaxLevel];
        SkipListNode<TKey, TValue> current = _head;

        for (int i = _currentLevel - 1; i >= 0; i--)
        {
            while (current.Next[i] != null && current.Next[i].Key.CompareTo(key) < 0)
            {
                current = current.Next[i];
            }
            update[i] = current;
        }

        current = current.Next[0];

        if (current != null && current.Key.Equals(key))
        {
            for (int i = 0; i < _currentLevel; i++)
            {
                if (update[i].Next[i] != current)
                {
                    break;
                }
                update[i].Next[i] = current.Next[i];
            }

            while (_currentLevel > 1 && _head.Next[_currentLevel - 1] == null)
            {
                _currentLevel--;
            }

            Count--;
            return true;
        }

        return false;
    }

    public SkipList<TKey, TValue> Clone()
    {
        var clone = new SkipList<TKey, TValue>(MaxLevel);
        SkipListNode<TKey, TValue> current = _head.Next[0];

        while (current != null)
        {
            clone.Add(current.Key, current.Value);
            current = current.Next[0];
        }

        return clone;
    }

    public void Clear()
    {
        Count = 0;
        _currentLevel = 1;
        _head = new SkipListNode<TKey, TValue>(default(TKey), default(TValue), MaxLevel);
    }

    private int GetNewNodeLevel()
    {
        int level = 1;
        while (level < MaxLevel && _random.NextDouble() < 0.5)
        {
            level++;
        }
        return level;
    }
}