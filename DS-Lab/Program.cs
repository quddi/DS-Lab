﻿using System;
using System.Collections.Generic;

public static class Program
{
    private static readonly Deque<int> Deque = new();
    
    private static readonly MultiList<int> MultiList = new();
    private static MultiList<int> MultiListCopy;
    
    private static readonly SkipList<int, string> SkipList = new();
    private static SkipList<int, string> SkipListCopy;

    public static void Main()
    {
        //TestDeque();

        //TestMultiList();
        
        //TestSkipList();
        
        TestRarefiedMatrixManually();
    }

    private static void TestRarefiedMatrixManually()
    {
        var node1 = new RarefiedMatrixNode { Key = 1, Value = 1 };
        var node2 = new RarefiedMatrixNode { Key = 2, Value = 2 };
        var node3 = new RarefiedMatrixNode { Key = 3, Value = 3 };
        var node4 = new RarefiedMatrixNode { Key = 4, Value = 4 };
        var node7 = new RarefiedMatrixNode { Key = 7, Value = 7 };
        var node8 = new RarefiedMatrixNode { Key = 8, Value = 8 };
        var node9 = new RarefiedMatrixNode { Key = 9, Value = 9 };

        node1.RightNode = node2;
        node3.RightNode = node8;
        node4.RightNode = node7;
        node7.RightNode = node9;

        node3.DownNode = node4;
        node1.DownNode = node3;
        node2.DownNode = node9;

        var upList = new List<RarefiedMatrixNode>();
        
        upList.Add(node1);
        upList.Add(node8);
        upList.Add(node7);
        upList.Add(node2);

        var leftList = new List<RarefiedMatrixNode>();
        
        leftList.Add(node1);
        leftList.Add(node3);
        leftList.Add(node4);

        var rarefiedMatrix = new RarefiedMatrix(upList, leftList);
        
        Console.WriteLine(rarefiedMatrix);
        Console.WriteLine();
        rarefiedMatrix.ToDefaultMatrix().Print();
        Console.WriteLine();
        
        rarefiedMatrix.Resize(5,5);
        rarefiedMatrix.SetValue(4, 4, 11, 11);
        
        Console.WriteLine(rarefiedMatrix);
        Console.WriteLine();
        rarefiedMatrix.ToDefaultMatrix().Print();
        
        rarefiedMatrix.Resize(3,3);
        Console.WriteLine(rarefiedMatrix);
        Console.WriteLine();
        rarefiedMatrix.ToDefaultMatrix().Print();
    }
    
    private static void TestSkipList()
    {
        SkipList.Clear();

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"SkipList:\n{SkipList}");
            
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("SkipList copy: ");
            Console.WriteLine(SkipListCopy == null ? "Empty SkipList" : SkipListCopy);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1. Add an element");
            Console.WriteLine("2. Check if it contains a key");
            Console.WriteLine("3. Remove an element");
            Console.WriteLine("4. Clear the SkipList");
            Console.WriteLine("5. Clone the SkipList");
            Console.WriteLine("6. Exit");
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            int choice;
            
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter the operation number.");
                continue;
            }

            if (choice == 1)
            {
                Console.Write("Enter a key (integer) to add: ");
                if (int.TryParse(Console.ReadLine(), out int key))
                {
                    Console.Write("Enter a value (string) to associate with the key: ");
                    var value = Console.ReadLine();

                    Console.Write("Enter a value - new node height or something else, if you want it to be selected randomly: ");

                    var result = int.TryParse(Console.ReadLine(), out int height);
                    
                    SkipList.Add(key, value, result ? height : null);

                    Console.WriteLine($"Element ({key}, {value}) added to the SkipList.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter an integer for the key.");
                }
            }
            else if (choice == 2)
            {
                Console.Write("Enter a key to check if it exists: ");
                if (int.TryParse(Console.ReadLine(), out var key))
                {
                    var containsKey = SkipList.Contains(key);
                    Console.WriteLine($"SkipList contains key {key}: {containsKey}");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter an integer for the key.");
                }
            }
            else if (choice == 3)
            {
                Console.Write("Enter a key to remove: ");
                if (int.TryParse(Console.ReadLine(), out var key))
                {
                    var removed = SkipList.TryRemove(key);
                    if (removed)
                        Console.WriteLine($"Element with key {key} removed from the SkipList.");
                    else
                        Console.WriteLine($"Element with key {key} not found in the SkipList.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter an integer for the key.");
                }
            }
            else if (choice == 4)
            {
                SkipList.Clear();
                Console.WriteLine("SkipList is cleared.");
            }
            else if (choice == 5)
            {
                SkipListCopy = SkipList.Clone();
                Console.WriteLine("SkipList cloned successfully.");
            }
            else if (choice == 6)
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid operation number. Please select a valid operation.");
            }
        }
    }

    private static void TestSkipListManually()
    {
        var headNode = new SkipListNode<int, int>(int.MinValue, int.MinValue);
        var _2Node = new SkipListNode<int, int>(2, 2);
        var _3Node = new SkipListNode<int, int>(3, 3);
        var _5Node = new SkipListNode<int, int>(5, 5);
        var _6Node = new SkipListNode<int, int>(6, 6);
        var _7Node = new SkipListNode<int, int>(7, 7);
        var _8Node = new SkipListNode<int, int>(8, 8);
        var _10Node = new SkipListNode<int, int>(10, 10);
        var _12Node = new SkipListNode<int, int>(12, 12);
        var _13Node = new SkipListNode<int, int>(13, 13);
        var _15Node = new SkipListNode<int, int>(15, 15);
        var _17Node = new SkipListNode<int, int>(17, 17);
        var _19Node = new SkipListNode<int, int>(19, 19);
        var _20Node = new SkipListNode<int, int>(20, 20);

        _2Node[0] = _3Node;
        _2Node[1] = _3Node;
        _2Node[2] = _8Node;

        _3Node[0] = _5Node;
        _3Node[1] = _7Node;

        _5Node[0] = _6Node;

        _6Node[0] = _7Node;

        _7Node[0] = _8Node;
        _7Node[1] = _8Node;

        _8Node[0] = _10Node;
        _8Node[1] = _10Node;
        _8Node[2] = _10Node;

        _10Node[0] = _12Node;
        _10Node[1] = _12Node;
        _10Node[2] = _19Node;
        _10Node[3] = _20Node;

        _12Node[0] = _13Node;
        _12Node[1] = _15Node;

        _13Node[0] = _15Node;

        _15Node[0] = _17Node;
        _15Node[1] = _19Node;

        _17Node[0] = _19Node;

        _19Node[0] = _20Node;
        _19Node[1] = _20Node;
        _19Node[2] = _20Node;

        headNode[0] = _2Node;
        headNode[1] = _2Node;
        headNode[2] = _2Node;
        headNode[3] = _10Node;

        var skipList = new SkipList<int, int>(headNode, 5);

        Console.WriteLine(skipList + "\n");

        //skipList.Clear();

        //skipList.Add(3, 3);

        Console.WriteLine(skipList);

        Console.WriteLine("----------------");

        var copy = skipList.Clone();

        Console.WriteLine(copy);

        Console.WriteLine("----------------");

        skipList.TryRemove(10);
        skipList.Add(10, 10);

        Console.WriteLine(skipList);
        Console.WriteLine(copy);

        Console.WriteLine("----------------");

        copy.Clear();

        copy.Add(2, 2, 0);
        copy.Add(10, 10);
        copy.Add(5, 5);

        Console.WriteLine(copy);
    }

    private static void TestDeque()
    {
        Deque.Clear();

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Deque);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1. Add an element to the front");
            Console.WriteLine("2. Add an element to the rear");
            Console.WriteLine("3. Remove an element from the front");
            Console.WriteLine("4. Remove an element from the rear");
            Console.WriteLine("5. Get an element from the front");
            Console.WriteLine("6. Get an element from the rear");
            Console.WriteLine("7. Swap the first and the last elements");
            Console.WriteLine("8. Check emptiness");
            Console.WriteLine("9. Get the number of elements");
            Console.WriteLine("10. Reverse");
            Console.WriteLine("11. Contains element");
            Console.WriteLine("12. Clear the deque");
            Console.WriteLine("13. Exit");

            Console.ForegroundColor = ConsoleColor.Cyan;
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter the operation number.");
                continue;
            }

            if (choice == 1)
            {
                Console.Write("Enter an element to add to the front: ");

                if (int.TryParse(Console.ReadLine(), out var itemToAddFront))
                {
                    Deque.AddFront(itemToAddFront);
                    Console.WriteLine("Element added to the front successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter an integer.");
                }
            }
            else if (choice == 2)
            {
                Console.Write("Enter an element to add to the rear: ");
                if (int.TryParse(Console.ReadLine(), out var itemToAddRear))
                {
                    Deque.AddRear(itemToAddRear);
                    Console.WriteLine("Element added to the rear successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter an integer.");
                }
            }
            else if (choice == 3)
            {
                try
                {
                    var removedItem = Deque.RemoveFront();
                    Console.WriteLine("Removed element from the front: " + removedItem);
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("_deque is empty. Cannot remove an element.");
                }
            }
            else if (choice == 4)
            {
                try
                {
                    var removedItem = Deque.RemoveRear();
                    Console.WriteLine("Removed element from the rear: " + removedItem);
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Deque is empty. Cannot remove an element.");
                }
            }
            else if (choice == 5)
            {
                try
                {
                    var frontItem = Deque.PeekFront();
                    Console.WriteLine("Element from the front: " + frontItem);
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Deque is empty. Cannot get an element.");
                }
            }
            else if (choice == 6)
            {
                try
                {
                    var rearItem = Deque.PeekRear();
                    Console.WriteLine("Element from the rear: " + rearItem);
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Deque is empty. Cannot get an element.");
                }
            }
            else if (choice == 7)
            {
                Deque.SwapFirstAndLast();

                Console.WriteLine("Swapped the first and the last elements");
            }
            else if (choice == 8)
            {
                Console.WriteLine("Deque is empty: " + Deque.IsEmpty);
            }
            else if (choice == 9)
            {
                Console.WriteLine("Number of elements in the deque: " + Deque.Count);
            }
            else if (choice == 10)
            {
                Deque.Reverse();

                Console.WriteLine("Reversed the deque");
            }
            else if (choice == 11)
            {
                Console.Write("Enter an element to check if the deque contains it: ");

                if (int.TryParse(Console.ReadLine(), out var itemToCheck))
                    Console.WriteLine($"Deque contains {itemToCheck}: {Deque.Contains(itemToCheck)}");
                else
                    Console.WriteLine("Invalid input. Please enter an integer.");
            }
            else if (choice == 12)
            {
                Deque.Clear();
                Console.WriteLine("Deque is cleared.");
            }
            else if (choice == 13)
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid operation number. Please select a valid operation.");
            }
        }
    }

    private static void TestMultiList()
    {
        MultiList.Clear();

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("MultiList:");
            Console.WriteLine(MultiList);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("MultiList copy: ");
            Console.WriteLine(MultiListCopy == null ? "Empty multi list" : MultiListCopy);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1. Get total number of elements in the multi-list");
            Console.WriteLine("2. Get number of elements at each level");
            Console.WriteLine("4. Add an element to the multi-list");
            Console.WriteLine("5. Remove a level from the multi-list");
            Console.WriteLine("6. Remove a branch from the multi-list");
            Console.WriteLine("7. Create a copy of the multi-list");
            Console.WriteLine("8. Clear the multi-list");
            Console.WriteLine("9. Exit");

            Console.ForegroundColor = ConsoleColor.Cyan;

            if (!int.TryParse(Console.ReadLine(), out var choice))
            {
                Console.WriteLine("Invalid input. Please enter the operation number.");
                continue;
            }

            if (choice == 1)
            {
                Console.WriteLine("Total number of elements in the multi-list: " + MultiList.TotalElementsCount);
            }
            else if (choice == 2)
            {
                Console.Write("Enter the level number: ");
                if (int.TryParse(Console.ReadLine(), out var levelNumber))
                {
                    var count = MultiList.CountNodesAmountAtLevel(levelNumber);
                    Console.WriteLine($"Number of elements at level {levelNumber}: {count}");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter an integer.");
                }
            }
            else if (choice == 3)
            {
                Console.WriteLine("Type of the element in the multi-list: " + typeof(int));
            }
            else if (choice == 4)
            {
                if (MultiList.TotalElementsCount == 0)
                {
                    Console.WriteLine("The multi-list is empty. Enter the value for the root node: ");
                    if (int.TryParse(Console.ReadLine(), out var rootValue))
                    {
                        MultiList.AddNode(0, 0, new MultiListNode<int>(rootValue));
                        Console.WriteLine($"Added the root node with value {rootValue}.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for the root node value. Please enter an integer.");
                    }
                }
                else
                {
                    Console.Write("Enter the value of the parent node: ");
                    if (int.TryParse(Console.ReadLine(), out var parentNodeValue))
                    {
                        if (!MultiList.ContainsValue(parentNodeValue))
                        {
                            Console.WriteLine(
                                $"Parent node with value {parentNodeValue} not found in the multi-list.");
                        }
                        else
                        {
                            Console.Write("Enter the value of the element to add: ");
                            if (int.TryParse(Console.ReadLine(), out var itemToAdd))
                            {
                                if (MultiList.ContainsValue(itemToAdd))
                                {
                                    Console.WriteLine($"Element {itemToAdd} already exists in the multi-list.");
                                }
                                else
                                {
                                    Console.Write(
                                        "Enter the position to add the element (0 for front, or a specific index): ");
                                    if (int.TryParse(Console.ReadLine(), out var position))
                                    {
                                        var newNode = new MultiListNode<int>(itemToAdd);
                                        try
                                        {
                                            MultiList.AddNode(parentNodeValue, position, newNode);
                                            Console.WriteLine(
                                                $"Element {itemToAdd} added to the multi-list as a child of {parentNodeValue} at position {position}.");
                                        }
                                        catch (ArgumentException argumentException)
                                        {
                                            Console.WriteLine(argumentException.Message);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input for the position. Please enter an integer.");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for the element to add. Please enter an integer.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for the parent node value. Please enter an integer.");
                    }
                }
            }
            else if (choice == 5)
            {
                Console.Write("Enter the level number to remove: ");
                if (int.TryParse(Console.ReadLine(), out var levelNumber))
                {
                    MultiList.RemoveLevel(levelNumber);
                    Console.WriteLine($"Removed level {levelNumber} from the multi-list.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter an integer.");
                }
            }
            else if (choice == 6)
            {
                Console.Write("Enter the value of the branch root to remove: ");
                if (int.TryParse(Console.ReadLine(), out var branchRootValue))
                {
                    if (MultiList.TryRemoveBranch(branchRootValue))
                        Console.WriteLine($"Removed branch with root {branchRootValue} from the multi-list.");
                    else
                        Console.WriteLine($"Branch with root {branchRootValue} not found in the multi-list.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter an integer.");
                }
            }
            else if (choice == 7)
            {
                MultiListCopy = MultiList.Clone();
                Console.WriteLine("Created a copy of the multi-list.");
            }
            else if (choice == 8)
            {
                MultiList.Clear();
                Console.WriteLine("Multi-list is cleared.");
            }
            else if (choice == 9)
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid operation number. Please select a valid operation.");
            }
        }
    }
}