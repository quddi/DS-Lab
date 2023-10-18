using System;
using System.Collections.Generic;
using System.Text;

public class SkipList<TKey, TValue> where TKey : IComparable
{
    private const int IncreaseLevelChance = 50;
    private readonly Random Random = new();
    private readonly Func<int, int> MaxHeightFormula; 
    
    private SkipListNode<TKey, TValue> _emptyHead;

    public int Height => _emptyHead.ChildrenCount;
    private int MaxHeight => MaxHeightFormula(GetElementsCount());

    public SkipList() : this(new(),int.MaxValue) { }

    public SkipList(SkipListNode<TKey, TValue> emptyHead, int maxHeight)
    {
        _emptyHead = emptyHead;
        MaxHeightFormula = _ => maxHeight;
    }
    
    public SkipList(SkipListNode<TKey, TValue> emptyHead, Func<int, int> maxHeightFormula)
    {
        _emptyHead = emptyHead;
        MaxHeightFormula = maxHeightFormula;
    }

    public int GetElementsCount()
    {
        var currentNode = _emptyHead[Constants.FirstLevel];

        if (currentNode == null) return 0;
        
        var count = 0;

        while (currentNode != null)
        {
            count++;

            currentNode = currentNode[Constants.FirstLevel];
        }

        return count;
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

    public void Add(TKey key, TValue value, int? height = null)
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

        if (height != null) height = ExtensionsMethods.Clamp(height.Value, 1, MaxHeight);

        var shouldIncrease = true;
        
        for (int level = 0; shouldIncrease; level++)
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

            shouldIncrease = ShouldIncreaseLevel(level, height);
        }
    }

    private bool ShouldIncreaseLevel(int level, int? height)
    {
        return height == null
            ? ExtensionsMethods.GetRandomSuccess(IncreaseLevelChance) && level < MaxHeight
            : level < height - 1;
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

    public SkipList<TKey, TValue> Clone()
    {
        var allNodes = new List<SkipListNode<TKey, TValue>>();
        var allNodesCopy = new Dictionary<TKey, SkipListNode<TKey, TValue>>();
        
        var newHead = new SkipListNode<TKey, TValue>(_emptyHead.Key, _emptyHead.Value);

        var currentNode = _emptyHead[Constants.FirstLevel];

        while (currentNode != null)
        {
            allNodes.Add(currentNode);
            
            allNodesCopy.Add(currentNode.Key, new SkipListNode<TKey, TValue>(currentNode.Key, currentNode.Value));

            currentNode = currentNode[Constants.FirstLevel];
        }

        for (int level = 0; level < Height; level++)
        {
            if (_emptyHead[level] == null)
            {
                newHead[level] = null;
                continue;
            }

            var nextNodeKey = _emptyHead[level].Key;

            newHead[level] = allNodesCopy[nextNodeKey];
        }

        var currentRealNode = _emptyHead[Constants.FirstLevel];

        while (currentRealNode != null)
        {
            var currentCopyNode = allNodesCopy[currentRealNode.Key];
            
            for (int level = 0; level < currentRealNode.ChildrenCount; level++)
            {
                var nextRealNode = currentRealNode[level];

                currentCopyNode[level] = nextRealNode != null ? allNodesCopy[nextRealNode.Key] : null;
            }
            
            currentRealNode = currentRealNode[Constants.FirstLevel];
        }

        return new SkipList<TKey, TValue>(newHead, MaxHeightFormula);
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