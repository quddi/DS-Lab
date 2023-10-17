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

    public bool Contains(TKey key)
    {
        var currentNode = _emptyHead[Constants.FirstLevel];

        while (currentNode != null)
        {
            if (currentNode.Key.CompareTo(key) == 0) 
                return true;

            currentNode = currentNode[Constants.FirstLevel];
        }

        return false;
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
        if (!Contains(key)) 
            return false;

        var previousNodes = new List<SkipListNode<TKey, TValue>>();
        var nextNodes = new List<SkipListNode<TKey, TValue>>();

        for (int level = 0; level < Height; level++)
        {
            var currentNode = _emptyHead;

            while (true)
            {
                if (currentNode[level] == null)
                    goto LoopsEnd;

                if (currentNode[level].Key.CompareTo(key) == 0)
                {
                    previousNodes.Add(currentNode);
                    nextNodes.Add(currentNode[level][level]);
                    break;
                }

                currentNode = currentNode[level];
            }
        }
        
        LoopsEnd:
        if (previousNodes.Count != nextNodes.Count)
            throw new($"Previous and nex nodes counts was different! ({previousNodes.Count} and {nextNodes.Count})");

        for (int i = 0; i < previousNodes.Count; i++)
        {
            previousNodes[i][i] = nextNodes[i];
        }
        
        return true;
    }

    public void Clear()
    {
        _emptyHead = new(_emptyHead.Key, _emptyHead.Value)
        {
            [0] = null
        };
    }

    public override string ToString()
    {
        var indexesList = new List<TKey>();

        var currentNode = _emptyHead[Constants.FirstLevel];

        while (currentNode != null)
        {
            indexesList.Add(currentNode.Key);

            currentNode = currentNode[Constants.FirstLevel];
        }

        var mainStringBuilder = new StringBuilder();
        var levelStringBuilder = new StringBuilder();
        
        for (int level = Height - 1; level >= Constants.FirstLevel; level--)
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