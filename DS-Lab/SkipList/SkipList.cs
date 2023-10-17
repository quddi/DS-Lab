using System;
using System.Collections.Generic;
using System.Text;

public class SkipList<TKey, TValue> where TKey : IComparable
{
    private const int IncreaseLevelChance = 50;
    private readonly int MaxHeight;
    private readonly Random Random = new();
    
    private SkipListNode<TKey, TValue> _emptyHead;

    public int Height => _emptyHead.ChildrenCount;

    public SkipList() : this(new SkipListNode<TKey, TValue>(default, default)) { }

    public SkipList(SkipListNode<TKey, TValue> emptyHead, int maxHeight = int.MaxValue)
    {
        _emptyHead = emptyHead;
        MaxHeight = maxHeight;
    }

    public void Add(TKey key, TValue value)
    {
        var newNode = new SkipListNode<TKey, TValue>(key, value);
        
        var levelPairPreviousNode = new Dictionary<int, SkipListNode<TKey, TValue>>();

        for (int i = 0; i < Height; i++)
        {
            levelPairPreviousNode[i] = _emptyHead;
        }

        for (int currentLevel = Height - 1; currentLevel >= 0; currentLevel--)
        {
            var currentNode = _emptyHead;

            while (true)
            {
                if (currentNode[currentLevel] == null || currentNode[currentLevel].Key.CompareTo(key) > 0)
                    break;
                
                currentNode = currentNode[currentLevel];
            }

            if (levelPairPreviousNode.ContainsKey(currentLevel)) levelPairPreviousNode[currentLevel] = currentNode;
            else levelPairPreviousNode.Add(currentLevel, currentNode);
        }

        bool continueIncreaseLevel = true;

        for (int level = 0; continueIncreaseLevel && level < MaxHeight; level++)
        {
            if (level < Height)
            {
                var previousNode = levelPairPreviousNode[level];
            
                newNode.AddNext(previousNode[level]);
            
                previousNode[level] = newNode;
            }
            else
            {
                _emptyHead.AddNext(newNode);

                newNode[level] = null;
            }

            continueIncreaseLevel = ExtensionsMethods.GetRandomSuccess(IncreaseLevelChance);
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
        
        for (int level = Height - 1; level >= firstLevel; level--)
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
}