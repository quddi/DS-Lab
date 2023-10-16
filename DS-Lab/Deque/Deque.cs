using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Deque<T>
{
    private DequeNode<T> _front;
    private DequeNode<T> _rear;
    private int _count;

    public Deque()
    {
        _front = null;
        _rear = null;
        _count = 0;
    }

    public int Count => _count;
    public bool IsEmpty => Count == 0;

    public void AddFront(T item)
    {
        DequeNode<T> newDequeNode = new DequeNode<T>(item);

        if (IsEmpty)
        {
            _front = newDequeNode;
            _rear = newDequeNode;
        }
        else
        {
            newDequeNode.Next = _front;
            _front.Previous = newDequeNode;
            _front = newDequeNode;
        }

        _count++;
    }

    public void AddRear(T item)
    {
        DequeNode<T> newDequeNode = new DequeNode<T>(item);

        if (IsEmpty)
        {
            _front = newDequeNode;
            _rear = newDequeNode;
        }
        else
        {
            newDequeNode.Previous = _rear;
            _rear.Next = newDequeNode;
            _rear = newDequeNode;
        }

        _count++;
    }

    public T RemoveFront()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Deque is empty");

        T data = _front.Data;
        _front = _front.Next;

        if (_front != null)
            _front.Previous = null;
        else
            _rear = null;

        _count--;
        return data;
    }

    public T RemoveRear()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Deque is empty");

        T data = _rear.Data;
        _rear = _rear.Previous;

        if (_rear != null)
            _rear.Next = null;
        else
            _front = null;

        _count--;
        return data;
    }

    public T PeekFront()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Deque is empty");

        return _front.Data;
    }

    public T PeekRear()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Deque is empty");

        return _rear.Data;
    }

    public void Clear()
    {
        _front = null;
        _rear = null;
        _count = 0;
    }

    public void SwapFirstAndLast()
    {
        if (IsEmpty || _front == _rear)
            return;

        T firstItem = _front.Data;
        T lastItem = _rear.Data;

        _front.Data = lastItem;
        _rear.Data = firstItem;
    }

    public void Reverse()
    {
        if (Count is 0 or 1) return;

        if (Count == 2)
        {
            SwapFirstAndLast();
            return;
        }

        var allNodes = new List<DequeNode<T>>();
        var current = _front;

        while (current != null)
        {
            allNodes.Add(current);
            current = current.Next;
        }

        foreach (var node in allNodes)
            node.SwapParentAndChild();

        _front = allNodes.Last();
        _rear = allNodes.First();
    }

    public bool Contains(T item)
    {
        DequeNode<T> current = _front;

        while (current != null)
        {
            if (current.Data.Equals(item))
                return true;

            current = current.Next;
        }

        return false;
    }

    public override string ToString()
    {
        if (IsEmpty)
            return "Deque is empty";

        DequeNode<T> currentDequeNode = _front;
        var stringBuilder = new StringBuilder();

        stringBuilder.Append("Deque: ");

        while (currentDequeNode != null)
        {
            stringBuilder.Append(currentDequeNode.Data);

            if (currentDequeNode.Next != null)
                stringBuilder.Append(" <-> ");

            currentDequeNode = currentDequeNode.Next;
        }

        return stringBuilder.ToString();
    }
}