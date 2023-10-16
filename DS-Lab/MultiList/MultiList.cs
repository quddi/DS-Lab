using System;
using System.Linq;

public class MultiList<T> where T : IComparable
{
    public MultiListNode<T> Root { get; set; }

    public int TotalElementsCount => Root != null ? Root.TotalChildrenAmount + 1 : 0;

    public bool IsEmpty => TotalElementsCount == 0;

    public MultiList() { }

    public MultiList(T rootData)
    {
        Root = new MultiListNode<T>(rootData);
    }

    public int CountNodesAmountAtLevel(int levelNumber)
    {
        return Root?.CountNodesAtLevel(levelNumber) ?? 0;
    }

    public bool ContainsValue(T value)
    {
        return ContainsValueHelper(Root, value);
    }

    public MultiListNode<T> GetNode(T nodeValue)
    {
        return GetNodeHelper(Root, nodeValue);
    }
    
    public MultiListNode<T> GetParentNode(T anyChildValue)
    {
        return GetParentNodeHelper(Root, anyChildValue);
    }

    public int CountLevels()
    {
        return CountLevelsHelper(Root);
    }

    public void AddNode(T parentNodeValue, int position, MultiListNode<T> node)
    {
        if (Root == null)
        {
            Root = node;
            return;
        }
        
        if (ContainsValue(node.Data))
            throw new ArgumentException($"There already was {node.Data} value!");

        var parentNode = GetNode(parentNodeValue);

        if (parentNode == null)
            throw new ArgumentException($"Not found node with {parentNodeValue} value!");
        
        parentNode.AddChild(node, position);
    }

    public bool TryRemoveBranch(T branchRootValue)
    {
        if (branchRootValue.CompareTo(Root.Data) == 0)
        {
            Clear();
            return true;
        }
        
        var node = GetNode(branchRootValue);
        var parentNode = GetParentNode(branchRootValue);

        if (node == null || parentNode == null) return false;

        return parentNode.TryRemoveChild(node);
    }
    
    public void RemoveLevel(int levelNumber)
    {
        if (levelNumber < 0)
            throw new ArgumentException("Level number must be non-negative.");

        if (levelNumber == 0)
        {
            Clear();
            return;
        }

        RemoveLevelHelper(Root, levelNumber - 1);
    }

    public void Clear()
    {
        Root = null;
    }
    
    public MultiList<T> Clone()
    {
        if (Root == null)
            return new MultiList<T>();

        var clonedRoot = Root.GetCopy();
        
        var clonedMultiList = new MultiList<T>(clonedRoot.Data)
        {
            Root = clonedRoot
        };

        return clonedMultiList;
    }

    private MultiListNode<T> GetNodeHelper(MultiListNode<T> currentNode, T value)
    {
        if (currentNode == null)
            return null;

        if (currentNode.Data.CompareTo(value) == 0)
            return currentNode; 

        foreach (var child in currentNode.Children)
        {
            MultiListNode<T> result = GetNodeHelper(child, value);
            
            if (result != null)
                return result;
        }

        return null;
    }

    private bool ContainsValueHelper(MultiListNode<T> currentNode, T value)
    {
        if (currentNode == null)
            return false;

        if (currentNode.Data.CompareTo(value) == 0)
            return true;

        foreach (var child in currentNode.Children)
            if (ContainsValueHelper(child, value))
                return true;

        return false;
    }

    private int CountLevelsHelper(MultiListNode<T> currentNode)
    {
        if (currentNode == null)
            return 0;

        int maxChildDepth = 0;
        
        foreach (var child in currentNode.Children)
        {
            int childDepth = CountLevelsHelper(child);
            maxChildDepth = Math.Max(maxChildDepth, childDepth);
        }

        return 1 + maxChildDepth;
    }
    
    private MultiListNode<T> GetParentNodeHelper(MultiListNode<T> currentNode, T value)
    {
        if (currentNode == null)
            return null;

        if (currentNode.Children.Any(child => child.Data.CompareTo(value) == 0))
            return currentNode;

        foreach (var child in currentNode.Children)
        {
            MultiListNode<T> result = GetParentNodeHelper(child, value);

            if (result != null)
                return result;
        }

        return null;
    }

    private void RemoveLevelHelper(MultiListNode<T> currentNode, int currentLevel)
    {
        if (currentNode == null)
            return;

        if (currentLevel == 0)
        {
            currentNode.Children.Clear();
        }
        else
        {
            foreach (var node in currentNode.Children)
                RemoveLevelHelper(node, currentLevel - 1);
        }
    }
    
    public override string ToString()
    {
        if (Root == null) return "Empty multi list";
        
        return Root.ToString();
    }
}