using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MultiListNode<T> where T : IComparable
{
    public T Data { get; set; }
    public List<MultiListNode<T>> Children { get; set; }

    public int TotalChildrenAmount => Children.Sum(child => child.TotalChildrenAmount) + OwnChildrenAmount;

    public int OwnChildrenAmount => Children.Count;

    public MultiListNode(T data)
    {
        Data = data;
        Children = new List<MultiListNode<T>>();
    }

    public void AddChild(MultiListNode<T> child)
    {
        Children.Add(child);
    }

    public void AddChild(MultiListNode<T> child, int position)
    {
        if (position < 0 || position > Children.Count)
            throw new ArgumentException($"Trying to insert a node into a wrong position {position}! It must be [0; {Children.Count}]");
        
        Children.Insert(Math.Min(position, Children.Count), child);
    }

    public bool TryRemoveChild(MultiListNode<T> child)
    {
        if (!Children.Contains(child)) 
            return false;

        Children.Remove(child);

        return true;
    }
    
    public int CountNodesAtLevel(int levelNumber)
    {
        if (levelNumber == 0)
            return 1;

        if (levelNumber < 0)
            return 0;

        if (levelNumber == 1)
            return OwnChildrenAmount;

        return Children.Sum(child => child.CountNodesAtLevel(levelNumber - 1));
    }

    public MultiListNode<T> GetCopy()
    {
        var clonedNode = new MultiListNode<T>(Data);

        foreach (var child in Children)
            clonedNode.AddChild(child.GetCopy());

        return clonedNode;
    }
    
    public override string ToString()
    {
        return ToStringHelper(this, 0);
    }

    private static string ToStringHelper(MultiListNode<T> node, int level)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(new string(' ', level * 4) + node.Data);

        if (node.OwnChildrenAmount > 0) stringBuilder.Append("---▼");

        stringBuilder.Append("\n");

        foreach (var child in node.Children)
        {
            stringBuilder.Append(ToStringHelper(child, level + 1));
        }

        return stringBuilder.ToString();
    }
}