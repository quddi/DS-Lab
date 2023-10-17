using System;
using System.Collections.Generic;
using System.Text;

public class SkipList<TKey, TValue> where TKey : IComparable
{
    private readonly int MaxHeight;
    private readonly Random _random = new();
    private SkipListNode<TKey, TValue> _emptyHead;

    public int LevelsCount => _emptyHead.ChildrenCount;

    public SkipList(int maxHeight = 16) : this(new SkipListNode<TKey, TValue>(default, default))
    {
        MaxHeight = maxHeight;
    }

    public SkipList(SkipListNode<TKey, TValue> emptyHead)
    {
        _emptyHead = emptyHead;
    }

    public void Add(TKey key, TValue value)
    {
        var newNode = new SkipListNode<TKey, TValue>(key, value);
        
        var levelPairPreviousNode = new Dictionary<int, SkipListNode<TKey, TValue>>();

        for (int currentLevel = LevelsCount - 1; currentLevel >= 0; currentLevel--)
        {
            var currentNode = _emptyHead[currentLevel];

            while (true)
            {
                var node = currentNode[currentLevel];
                
                if (node == null || node.Key.CompareTo(key) > 0)
                    break;

                currentNode = node;
            }
        
            levelPairPreviousNode.Add(currentLevel, currentNode);
        }

        bool continueIncreaseLevel = true;

        for (int level = 0; continueIncreaseLevel && level < _emptyHead.ChildrenCount; level++)
        {
            var previousNode = levelPairPreviousNode[level];
            
            newNode.AddNext(previousNode[level]);
            
            previousNode[level] = newNode;
            
            continueIncreaseLevel = ExtensionsMethods.GetRandomSuccess(50);
        }
    }

    public bool TryRemove(TKey key)
    {

        return false;
    }

    public override string ToString()
    {
        var indexesList = new List<TKey>();

        var firstLevel = 0;
        
        var currentNode = _emptyHead[firstLevel];

        while (currentNode != null)
        {
            indexesList.Add(currentNode.Key);

            currentNode = currentNode[firstLevel];
        }

        var mainStringBuilder = new StringBuilder();
        var levelStringBuilder = new StringBuilder();
        
        for (int level = LevelsCount - 1; level >= firstLevel; level--)
        {
            currentNode = _emptyHead[level];
            levelStringBuilder.Append($"[{(level + 1).ToBeatifiedString()}]:");

            foreach (var currentKey in indexesList)
            {
                levelStringBuilder.Append(Constants.SkipListSpacing);
                if (currentNode == null || currentNode.Key.CompareTo(currentKey) != 0)
                {
                    levelStringBuilder.Append(Constants.SkipListSpacing);
                }
                else
                {
                    levelStringBuilder.Append(currentNode.Value.ToBeatifiedString());
                    currentNode = currentNode[level];
                }
            }

            mainStringBuilder.Append(levelStringBuilder);

            if (level != 0)
                mainStringBuilder.Append("\n");

            levelStringBuilder.Clear();
        }

        return mainStringBuilder.ToString();
    }

    private int RandomHeight()
    {
        int height = 1;
        
        while (_random.Next(2) == 0 && height < MaxHeight)
        {
            height++;
        }
        
        return height;
    }
}