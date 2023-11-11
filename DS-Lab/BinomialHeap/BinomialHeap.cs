using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BinomialHeap
{
    private BinomialHeapNode? _firstTreeRoot;

    public BinomialHeap(BinomialHeapNode? firstTreeRoot = null)
    {
        _firstTreeRoot = firstTreeRoot;
    }

    public bool Contains(int data)
    {
        return GetNode(data) != null;
    }

    public BinomialHeapNode? GetNode(int data)
    {
        return GetNode(_firstTreeRoot, data);
    }

    public (int DecimalElementsCount, int BinaryElementsCount, int TreesCount) GetSize()
    {
        if (_firstTreeRoot == null)
            return (0, 0, 0);

        int standardSum = 0;
        int binarySum = 0;
        int treesSum = 0;

        var current = _firstTreeRoot;

        while (current != null)
        {
            standardSum += (int)Math.Pow(2, current.Degree);
            binarySum +=  (int)Math.Pow(10, current.Degree);
            treesSum++;

            current = current.Sibling;
        }

        return (standardSum, binarySum, treesSum);
    }

    public int? GetMin()
    {
        if (_firstTreeRoot == null) return null;

        var currentNode = _firstTreeRoot;
        int minValue = Int32.MaxValue;

        while (currentNode != null)
        {
            minValue = Math.Min(currentNode.Data, minValue);

            currentNode = currentNode.Sibling;
        }

        return minValue;
    }

    public void Insert(int data)
    {
        var node = new BinomialHeapNode
        {
            Data = data,
            Degree = 0
        };

        MergeWith(new BinomialHeap(node));
    }

    public int? SnatchMin()
    {
        if (_firstTreeRoot == null) return null;

        BinomialHeapNode? currentNode = _firstTreeRoot;
        BinomialHeapNode? minValueNode = new BinomialHeapNode { Data = int.MaxValue };
        BinomialHeapNode? minValueNodePrevious = null;
        BinomialHeapNode? previous = null;

        while (currentNode != null)
        {
            if (currentNode.Data < minValueNode.Data)
            {
                minValueNode = currentNode;
                minValueNodePrevious = previous;
            }

            previous = currentNode;
            currentNode = currentNode.Sibling;
        }

        if (minValueNodePrevious != null)
            minValueNodePrevious.Sibling = minValueNode.Sibling;
        else
            _firstTreeRoot = minValueNode.Sibling;

        BinomialHeapNode child = minValueNode.Child;
        previous = null;

        while (child != null)
        {
            var sibling = child.Sibling;
            child.Sibling = previous;
            previous = child;
            child = sibling;
        }
        
        MergeWith(new BinomialHeap(previous));

        return minValueNode.Data;
    }

    public bool TryDecreaseData(int currentData, int targetData)
    {
        var containingNode = GetNode(currentData);

        if (containingNode == null)
            return false; 
        
        DecreaseData(containingNode, targetData);

        return true;
    }
    
    private void DecreaseData(BinomialHeapNode node, int targetData)
    {
        if (node.Data < targetData)
            throw new InvalidOperationException($"The node data was already lower than target! ({node.Data} < {targetData})");

        node.Data = targetData;

        BinomialHeapNode? firstNode = node;
        BinomialHeapNode? secondNode = firstNode.Parent;

        while (secondNode != null && firstNode.Data < secondNode.Data)
        {
            (firstNode.Data, secondNode.Data) = (secondNode.Data, firstNode.Data);

            firstNode = secondNode;
            secondNode = firstNode.Parent;
        }
    }

    public bool TryDelete(int data)
    {
        var containingNode = GetNode(data);

        if (containingNode == null)
            return false; 
        
        DecreaseData(containingNode, int.MinValue);
        SnatchMin();

        return true;
    }

    public void MergeWith(BinomialHeap secondaryHeap)
    {
        if (secondaryHeap._firstTreeRoot == null) 
            return;

        if (_firstTreeRoot == null)
        {
            _firstTreeRoot = secondaryHeap._firstTreeRoot;
            return;
        }
        
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
            else if (current.Data <= next.Data)
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

    private static BinomialHeapNode? GetNode(BinomialHeapNode? node, int data)
    {
        if (node == null)
            return null;
        
        if (node.Data == data) 
            return node;

        if (node.Child == null && node.Sibling == null)
            return null;

        var gotFromChild = GetNode(node.Child, data);

        return gotFromChild ?? GetNode(node.Sibling, data);
    }
}