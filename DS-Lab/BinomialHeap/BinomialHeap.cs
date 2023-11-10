using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BinomialHeap
{
    private BinomialHeapNode? _firstTreeRoot;

    public BinomialHeap(BinomialHeapNode? firstTreeRoot)
    {
        _firstTreeRoot = firstTreeRoot;
    }

    public int? GetMin()
    {
        if (_firstTreeRoot == null) return null;

        var currentNode = _firstTreeRoot;
        int minValue = Int32.MaxValue;

        while (currentNode != null)
        {
            minValue = Math.Min(currentNode.Key, minValue);

            currentNode = currentNode.Sibling;
        }

        return minValue;
    }

    public void Insert(int key, int value)
    {
        var node = new BinomialHeapNode
        {
            Key = key,
            Value = value,
            Degree = 0
        };
        
        MergeWith(new BinomialHeap(node));
    }

    public void MergeWith(BinomialHeap secondaryHeap)
    {
        MergeTreesList(secondaryHeap);

        BinomialHeapNode? previous = null;
        BinomialHeapNode? current = _firstTreeRoot!;
        BinomialHeapNode? next = _firstTreeRoot!.Sibling;

        while (next != null)
        {
            if (current.Degree != next.Degree || (next.Sibling != null && next.Sibling.Degree == current.Degree))
            {
                previous = current;
                current = next;
            }
            else if (current.Key <= next.Key)
            {
                current.Sibling = next.Sibling;
                LinkTrees(next, current);
            }
            else
            {
                if (previous == null)
                    _firstTreeRoot = next;
                else
                    previous.Sibling = next;

                LinkTrees(current, next);
                current = next;
            }

            next = current.Sibling;
        }
    }

    private void MergeTreesList(BinomialHeap secondaryHeap)
    {
        var primaryCurrentNode = _firstTreeRoot;
        var secondaryCurrentNode = secondaryHeap._firstTreeRoot;

        var list = new List<BinomialHeapNode>();

        while (primaryCurrentNode != null && secondaryCurrentNode != null)
        {
            if (primaryCurrentNode.Degree < secondaryCurrentNode.Degree)
            {
                list.Add(primaryCurrentNode);
                primaryCurrentNode = primaryCurrentNode.Sibling;
            }
            else
            {
                list.Add(secondaryCurrentNode);
                secondaryCurrentNode = secondaryCurrentNode.Sibling;
            }
        }

        while (primaryCurrentNode != null)
        {
            list.Add(primaryCurrentNode);
            primaryCurrentNode = primaryCurrentNode.Sibling;
        }

        while (secondaryCurrentNode != null)
        {
            list.Add(secondaryCurrentNode);
            secondaryCurrentNode = secondaryCurrentNode.Sibling;
        }

        for (int i = 0; i < list.Count - 1; i++)
            list[i].Sibling = list[i + 1];

        secondaryHeap._firstTreeRoot = null;

        _firstTreeRoot = list.First();
    }

    private static void LinkTrees(BinomialHeapNode child, BinomialHeapNode parent)
    {
        if (child.Degree != parent.Degree)
            throw new ArgumentException($"Linked trees must have the same degree, but they was: {child.Degree} and {parent.Degree}!");
        
        child.Parent = parent;
        child.Sibling = parent.Child;
        parent.Child = child;
        parent.Degree++;
    }

    public override string ToString()
    {
        if (_firstTreeRoot == null)
            return "Empty binomial heap."; 
        
        var stringBuilder = new StringBuilder();

        var currentSubTreeRoot = _firstTreeRoot;
        
        while (currentSubTreeRoot != null)
        {
            var isLastTree = currentSubTreeRoot.Sibling == null;
            
            stringBuilder.Append(currentSubTreeRoot.ToString(isLastTree));

            if (!isLastTree)
                stringBuilder.Append("|\n|\n");

            currentSubTreeRoot = currentSubTreeRoot.Sibling;
        }
        
        return stringBuilder.ToString();
    }
}