using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RarefiedMatrix
{
    private List<RarefiedMatrixNode> _upNodesList;
    private List<RarefiedMatrixNode> _leftNodesList;

    public int Rows => _leftNodesList.Count;
    public int Columns => _upNodesList.Count;

    public int this[int row, int column]
    {
        get => GetValue(row, column);
        set => SetValue(row, column, value, value);
    }
    
    public RarefiedMatrix(int rows = 0, int columns = 0)
    {
        _leftNodesList = new(rows);
        _upNodesList = new(columns);
    }

    public RarefiedMatrix(List<RarefiedMatrixNode> upNodesList, List<RarefiedMatrixNode> leftNodesList)
    {
        _upNodesList = upNodesList;
        _leftNodesList = leftNodesList;
    }

    public int GetValue(int row, int column)
    {
        if (row >= Rows || row < 0)
            throw new ArgumentException($"Row parameter was {row} but must be [0; {Rows - 1}]");
        
        if (column >= Columns)
            throw new ArgumentException($"Column parameter was {column} but must be [0; {Columns - 1}]");
        
        var nextUpNodes = new List<RarefiedMatrixNode>(_upNodesList);
        
        for (int currentRow = 0; currentRow < _leftNodesList.Count; currentRow++)
        {
            RarefiedMatrixNode nextLeftNode = _leftNodesList[currentRow];
            
            for (int currentColumn = 0; currentColumn < _upNodesList.Count; currentColumn++)
            {
                if (currentColumn == column && currentRow == row)
                {
                    if (nextLeftNode != null && 
                        nextUpNodes[currentColumn] != null && 
                        nextLeftNode.Key == nextUpNodes[currentColumn].Key)
                    {
                        return nextLeftNode.Value;
                    }

                    return 0;
                }

                if (currentRow == row && nextLeftNode == null)
                {
                    nextUpNodes[currentColumn] = nextUpNodes[currentColumn]?.DownNode;
                    
                    continue;
                }

                if (currentColumn == column && nextUpNodes[currentColumn] == null)
                {
                    nextLeftNode = nextLeftNode?.RightNode;
                    
                    continue;
                }
                
                if (nextLeftNode == null || nextUpNodes[currentColumn] == null) 
                    continue;
                
                if (nextLeftNode.Key == nextUpNodes[currentColumn].Key)
                {
                    nextLeftNode = nextLeftNode.RightNode;

                    nextUpNodes[currentColumn] = nextUpNodes[currentColumn].DownNode;
                }
            }
        }

        throw new InvalidOperationException();
    }
    
    public void SetValue(int row, int column, int key, int value)
    {
        if (row >= Rows || row < 0)
            throw new ArgumentException($"Row parameter was {row} but must be [0; {Rows - 1}]");
        
        if (column >= Columns)
            throw new ArgumentException($"Column parameter was {column} but must be [0; {Columns - 1}]");
        
        var nextUpNodes = new List<RarefiedMatrixNode>(_upNodesList);
        var previousUpNodes = _upNodesList.Select(_ => (RarefiedMatrixNode)null!).ToList();
        
        for (int currentRow = 0; currentRow < _leftNodesList.Count; currentRow++)
        {
            RarefiedMatrixNode nextLeftNode = _leftNodesList[currentRow];
            RarefiedMatrixNode previousLeftNode = null;
            
            for (int currentColumn = 0; currentColumn < _upNodesList.Count; currentColumn++)
            {
                if (currentColumn == column && currentRow == row)
                {
                    if (nextLeftNode != null && nextUpNodes[currentColumn] != null &&
                        nextLeftNode.Key == nextUpNodes[currentColumn].Key)
                    {
                        var existingNode = nextLeftNode;
                        
                        if (value != 0)
                        {
                            existingNode.Key = key;
                            existingNode.Value = value;
                        }
                        else
                        {
                            var newNextUpNode = existingNode.DownNode;

                            var newNextLeftNode = existingNode.RightNode;

                            if (previousLeftNode != null)
                            {
                                previousLeftNode.RightNode = newNextLeftNode;

                                if (previousUpNodes[column] == null)
                                    _upNodesList[column] = nextLeftNode.DownNode!;
                            }
                            else
                            {
                                if (previousUpNodes[column] != null)
                                    previousUpNodes[column].DownNode = nextUpNodes[column]?.DownNode;
                                else 
                                    _upNodesList[column] = _upNodesList[column]?.DownNode!;
                                
                                _leftNodesList[row] = _leftNodesList[row]?.RightNode!;
                            }
                            
                            nextUpNodes[currentColumn].DownNode = newNextUpNode;
                        }
                    }
                    else
                    {
                        if (value == 0) return;
                        
                        var newNode = new RarefiedMatrixNode
                        {
                            Key = key,
                            Value = value,
                            RightNode = nextLeftNode,
                            DownNode = nextUpNodes[currentColumn],
                        };

                        if (previousLeftNode == null) _leftNodesList[currentRow] = newNode;
                        else previousLeftNode.RightNode = newNode;

                        if (previousUpNodes[currentColumn] == null) _upNodesList[currentColumn] = newNode;
                        else previousUpNodes[currentColumn].DownNode = newNode;
                    }
                    
                    return;
                }

                if (currentRow == row && nextLeftNode == null)
                {
                    previousUpNodes[currentColumn] = nextUpNodes[currentColumn];

                    nextUpNodes[currentColumn] = nextUpNodes[currentColumn]?.DownNode;
                    
                    continue;
                }

                if (currentColumn == column && nextUpNodes[currentColumn] == null)
                {

                    previousLeftNode = nextLeftNode;

                    nextLeftNode = nextLeftNode?.RightNode;
                    
                    continue;
                }
                
                if (nextLeftNode == null || nextUpNodes[currentColumn] == null) 
                    continue;
                
                if (nextLeftNode.Key == nextUpNodes[currentColumn].Key)
                {
                    previousLeftNode = nextLeftNode;

                    nextLeftNode = nextLeftNode.RightNode;

                    previousUpNodes[currentColumn] = nextUpNodes[currentColumn];

                    nextUpNodes[currentColumn] = nextUpNodes[currentColumn].DownNode;
                }
            }
        }
    }

    public void Resize(int newRowsCount, int newColumnsCount)
    {
        if (newRowsCount == 0 || newColumnsCount == 0)
        {
            _upNodesList.Clear();
            _leftNodesList.Clear();
            
            return;
        }

        if (newColumnsCount > Columns)
            _upNodesList.AddRange(Enumerable.Repeat<RarefiedMatrixNode>(null!, newColumnsCount - Columns));
        else
            _upNodesList = _upNodesList.Take(newColumnsCount).ToList();

        if (newRowsCount > Rows)
            _leftNodesList.AddRange(Enumerable.Repeat<RarefiedMatrixNode>(null!, newRowsCount - Rows));
        else
            _leftNodesList = _leftNodesList.Take(newRowsCount).ToList();
    }

    public void Clear()
    {
        _upNodesList = new List<RarefiedMatrixNode>(Enumerable.Repeat<RarefiedMatrixNode>(null!, _upNodesList.Count));
        _leftNodesList = new List<RarefiedMatrixNode>(Enumerable.Repeat<RarefiedMatrixNode>(null!, _leftNodesList.Count));
    }

    public int[,] ToDefaultMatrix()
    {
        if (Rows == 0 || Columns == 0) return new int[0, 0];
        
        var upNodes = new List<RarefiedMatrixNode>(_upNodesList);

        var result = new int[Rows, Columns];
        
        for (int row = 0; row < _leftNodesList.Count; row++)
        {
            var currentNode = _leftNodesList[row];
            
            for (int column = 0; column < _upNodesList.Count; column++)
            {
                if (upNodes[column] != null && currentNode != null && upNodes[column].Key == currentNode.Key)
                {
                    result[row, column] = currentNode.Value;
                    
                    upNodes[column] = upNodes[column].DownNode;
                    currentNode = currentNode.RightNode;
                }
                else
                {
                    result[row, column] = 0;
                }
            }
        }

        return result;
    }

    public void FromDefaultMatrix(int[,] matrix)
    {
        Clear();
        Resize(matrix.GetLength(0), matrix.GetLength(1));

        if (matrix.ContainsCopies(new HashSet<int> { 0 }))
            throw new ArgumentException("All values of the source matrix must be unique, but they were not!");
        
        var previousUpNodes = new List<RarefiedMatrixNode>(Enumerable.Repeat<RarefiedMatrixNode>(null!, _upNodesList.Count));

        for (int row = 0; row < Rows; row++)
        {
            RarefiedMatrixNode? previousLeftNode = null;

            for (int column = 0; column < Columns; column++)
            {
                if (matrix[row, column] == 0)
                    continue;

                var previousUpNode = previousUpNodes[column];
                
                var newNode = new RarefiedMatrixNode
                {
                    Key = matrix[row, column],
                    Value = matrix[row, column],
                };

                if (previousLeftNode == null) _leftNodesList[row] = newNode;
                else previousLeftNode.RightNode = newNode;

                previousLeftNode = newNode;
                
                if (previousUpNode == null) _upNodesList[column] = newNode;
                else previousUpNode.DownNode = newNode;

                previousUpNodes[column] = newNode;
            }
        }
    }

    public void Transpose()
    {
        var allNodes = new List<RarefiedMatrixNode>();

        for (var i = 0; i < _upNodesList.Count; i++)
        {
            var currentNode = _upNodesList[i];

            while (currentNode != null)
            {
                allNodes.Add(currentNode);

                currentNode = currentNode.DownNode;
            }
        }
        
        allNodes.ForEach(node => node.SwapNextNodes());

        (_upNodesList, _leftNodesList) = (_leftNodesList, _upNodesList);
    }

    public override string ToString()
    {
        if (Rows == 0 || Columns == 0)
            return "Empty rarefied matrix"; 
        
        var mainStringBuilder = new StringBuilder();
        var currentStringBuilder = new StringBuilder();

        var upNodes = new List<RarefiedMatrixNode>(_upNodesList);

        foreach (var node in _leftNodesList)
        {
            var currentNode = node;
            
            for (int column = 0; column < _upNodesList.Count; column++)
            {
                if (upNodes[column] != null && currentNode != null && upNodes[column].Key == currentNode.Key)
                {
                    currentStringBuilder.Append(currentNode.Value.ToBeatifiedString());

                    upNodes[column] = upNodes[column].DownNode;
                    currentNode = currentNode.RightNode;
                }
                else
                {
                    currentStringBuilder.Append(Constants.RarefiedMatrixSpacing);
                }
                
                currentStringBuilder.Append(Constants.RarefiedMatrixSpacing);
            }

            mainStringBuilder.Append(currentStringBuilder);
            mainStringBuilder.Append("\n\n");
            currentStringBuilder.Clear();
        }

        return mainStringBuilder.ToString();
    }
    
    public static RarefiedMatrix operator *(RarefiedMatrix rarefiedMatrix, int value)
    {
        if (value == 1)
            return rarefiedMatrix;
        
        if (value == 0)
        {
            rarefiedMatrix.Clear();

            return rarefiedMatrix;
        }

        for (var i = 0; i < rarefiedMatrix._upNodesList.Count; i++)
        {
            var currentUpNode = rarefiedMatrix._upNodesList[i];
            
            while (currentUpNode != null)
            {
                currentUpNode.Value *= value;

                currentUpNode = currentUpNode.DownNode;
            }
        }

        return rarefiedMatrix;
    }
}