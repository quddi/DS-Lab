using System;
using System.Collections.Generic;

public static class Program
{
    private static readonly Deque<int> Deque = new();
    
    private static readonly MultiList<int> MultiList = new();
    private static MultiList<int> MultiListCopy;
    
    private static readonly SkipList<int, string> SkipList = new();
    private static SkipList<int, string> SkipListCopy;

    private static RarefiedMatrix FirstRarefiedMatrix = new();
    private static readonly RarefiedMatrix SecondRarefiedMatrix = new();

    public static void Main()
    {
        //TestDeque();

        //TestMultiList();
        
        //TestSkipList();
        
        //TestRarefiedMatrix();
        
        TestBinomialHeapManually();
    }

    private static void TestBinomialHeapManually()
    {
        var node15 = new BinomialHeapNode { Data = 15, Degree = 0 };
        var node14 = new BinomialHeapNode { Data = 14, Degree = 0 };
        var node13 = new BinomialHeapNode { Data = 13, Degree = 1 };
        var node12 = new BinomialHeapNode { Data = 12, Degree = 0 };
        var node11 = new BinomialHeapNode { Data = 11, Degree = 0 };
        var node10 = new BinomialHeapNode { Data = 10, Degree = 1 };
        var node9 = new BinomialHeapNode { Data = 9, Degree = 2 };
        var node8 = new BinomialHeapNode { Data = 8, Degree = 3 };
        var node7 = new BinomialHeapNode { Data = 7, Degree = 0 };
        var node6 = new BinomialHeapNode { Data = 6, Degree = 1 };
        var node5 = new BinomialHeapNode { Data = 5, Degree = 0 };
        var node4 = new BinomialHeapNode { Data = 4, Degree = 0 };
        var node3 = new BinomialHeapNode { Data = 3, Degree = 2 };
        var node2 = new BinomialHeapNode { Data = 2, Degree = 1 };
        var node1 = new BinomialHeapNode { Data = 1, Degree = 0 };

        node15.Sibling = node9;

        node13.Child = node14;
        node13.Sibling = node9;

        node9.Child = node10;
        node10.Sibling = node11;
        node10.Child = node12;
        
        node9.Sibling = node8;
        node8.Child = node3;
        node3.Sibling = node6;
        node6.Sibling = node7;
        node3.Child = node2;
        node2.Sibling = node4;
        node2.Child = node1;
        node6.Child = node5;

        /*var node18 = new BinomialHeapNode { Data = 18 18, Degree = 2 };
        var node20 = new BinomialHeapNode { Data = 20 20, Degree = 1 };
        var node22 = new BinomialHeapNode { Data = 22 22, Degree = 0 };
        var node24 = new BinomialHeapNode { Data = 24 24, Degree = 0 };

        node18.Child = node20;
        node20.Child = node24;
        node20.Sibling = node22;*/

        var binomialHeap1 = new BinomialHeap(node15);
        //var binomialHeap2 = new BinomialHeap(node18);
        Console.WriteLine(binomialHeap1);
        //Console.WriteLine(binomialHeap2);
        
        //binomialHeap1.MergeWith(binomialHeap2);
        //binomialHeap1.Delete(new BinomialHeapNode {Data = 8});
        binomialHeap1.TryDelete(8);
        
        Console.WriteLine(binomialHeap1);
    }

    private static void TestRarefiedMatrix()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nFirst matrix:\n{FirstRarefiedMatrix}");
            FirstRarefiedMatrix.ToDefaultMatrix().Print();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nSecond matrix:\n{SecondRarefiedMatrix}");
            SecondRarefiedMatrix.ToDefaultMatrix().Print();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nChoose an operation:");
            Console.WriteLine("1. First matrix: generate");
            Console.WriteLine("2. Second matrix: generate");
            Console.WriteLine("3. First matrix: set value");
            Console.WriteLine("4. First matrix: get value");
            Console.WriteLine("5. First matrix: resize");
            Console.WriteLine("6. First matrix: transpose");
            Console.WriteLine("7. First matrix: multiplying per value");
            Console.WriteLine("8. First & second matrix: sum");
            Console.WriteLine("9. Exit");

            Console.ForegroundColor = ConsoleColor.Cyan;
            int input1, input2, input3, input4;
            
            if (!int.TryParse(Console.ReadLine(), out var choice) || choice is < 1 or > 9)
            {
                Console.WriteLine("Invalid input. Please enter the operation number.");
                continue;
            }

            if (choice == 1)
            {
                Console.Write("Input rows amount: ");

                if (!int.TryParse(Console.ReadLine(), out input1) || input1 is < 0 or > 10)
                {
                    Console.WriteLine("It must be an integer number, from [0, 10]");
                    continue;
                }
                
                Console.Write("Input columns amount: ");

                if (!int.TryParse(Console.ReadLine(), out input2) || input2 is < 0 or > 10)
                {
                    Console.WriteLine("It must be an integer number, from [0, 10]");
                    continue;
                }
                
                FirstRarefiedMatrix.FromDefaultMatrix(ExtensionsMethods.GetRandomMatrix(input1, input2));
            }
            else if (choice == 2)
            {
                Console.Write("Input rows amount: ");

                if (!int.TryParse(Console.ReadLine(), out input1) || input1 is < 0 or > 10)
                {
                    Console.WriteLine("It must be an integer number, from [0, 10]");
                    continue;
                }
                
                Console.Write("Input columns amount: ");

                if (!int.TryParse(Console.ReadLine(), out input2) || input2 is < 0 or > 10)
                {
                    Console.WriteLine("It must be an integer number, from [0, 10]");
                    continue;
                }
                
                SecondRarefiedMatrix.FromDefaultMatrix(ExtensionsMethods.GetRandomMatrix(input1, input2));
            }
            else if (choice == 3)
            {
                Console.Write("Input row: ");

                if (!int.TryParse(Console.ReadLine(), out input1) || input1 is < 0 or > 10)
                {
                    Console.WriteLine("It must be an integer number, from [0, 10]");
                    continue;
                }
                
                Console.Write("Input column: ");

                if (!int.TryParse(Console.ReadLine(), out input2) || input2 is < 0 or > 10)
                {
                    Console.WriteLine("It must be an integer number, from [0, 10]");
                    continue;
                }
                
                Console.Write("Input key: ");

                if (!int.TryParse(Console.ReadLine(), out input3))
                {
                    Console.WriteLine("It must be an integer number");
                    continue;
                }
                
                Console.Write("Input value: ");

                if (!int.TryParse(Console.ReadLine(), out input4))
                {
                    Console.WriteLine("It must be an integer number");
                    continue;
                }

                try
                {
                    FirstRarefiedMatrix.SetValue(input1, input2, input3, input4);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Caught an exception! Message: {ex.Message}");
                }
            }
            else if (choice == 4)
            {
                Console.Write("Input row: ");

                if (!int.TryParse(Console.ReadLine(), out input1) || input1 is < 0 or > 10)
                {
                    Console.WriteLine("It must be an integer number, from [0, 10]");
                    continue;
                }
                
                Console.Write("Input column: ");

                if (!int.TryParse(Console.ReadLine(), out input2) || input2 is < 0 or > 10)
                {
                    Console.WriteLine("It must be an integer number, from [0, 10]");
                    continue;
                }
                
                try
                {
                    Console.WriteLine($"FirstMatrix[{input1}, {input2}]: {FirstRarefiedMatrix.GetValue(input1, input2)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Caught an exception! Message: {ex.Message}");
                }
            }
            else if (choice == 5)
            {
                Console.Write("Input rows amount: ");

                if (!int.TryParse(Console.ReadLine(), out input1) || input1 is < 0 or > 10)
                {
                    Console.WriteLine("It must be an integer number, from [0, 10]");
                    continue;
                }
                
                Console.Write("Input columns amount: ");

                if (!int.TryParse(Console.ReadLine(), out input2) || input2 is < 0 or > 10)
                {
                    Console.WriteLine("It must be an integer number, from [0, 10]");
                    continue;
                }
                
                FirstRarefiedMatrix.Resize(input1, input2);
                Console.WriteLine("Resized!");
            }
            else if (choice == 6)
            {
                FirstRarefiedMatrix.Transpose();
                Console.WriteLine("Transposed!");
            }
            else if (choice == 7)
            {
                Console.Write("Enter a value to multiply by: ");

                if (!int.TryParse(Console.ReadLine(), out input1))
                {
                    Console.WriteLine("It must be an integer number");
                    continue;
                }

                FirstRarefiedMatrix *= input1;
            }
            else if (choice == 8)
            {
                try
                {
                    var newMatrix = FirstRarefiedMatrix + SecondRarefiedMatrix;

                    Console.WriteLine($"Sum matrix:\n{newMatrix}");
                    newMatrix.ToDefaultMatrix().Print();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Caught an exception. Message: {ex.Message}");
                }
            }
            else
            {
                return;
            }
        }
    }

    private static void TestRarefiedMatrixManually()
    {
        var rarefiedMatrix1 = new RarefiedMatrix();
        var rarefiedMatrix2 = new RarefiedMatrix();
        
        rarefiedMatrix1.FromDefaultMatrix(ExtensionsMethods.GetRandomMatrix(3, 5));
        rarefiedMatrix2.FromDefaultMatrix(ExtensionsMethods.GetRandomMatrix(3, 5));

        Console.WriteLine(rarefiedMatrix1);
        Console.WriteLine(rarefiedMatrix2);
        rarefiedMatrix1.ToDefaultMatrix().Print();
        rarefiedMatrix2.ToDefaultMatrix().Print();
        
        Console.WriteLine();

        var sumMatrix = rarefiedMatrix1 + rarefiedMatrix2;
        
        Console.WriteLine(sumMatrix);
        sumMatrix.ToDefaultMatrix().Print();
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