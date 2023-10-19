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
    
    public RarefiedMatrix(int rows, int columns)
    {
        _leftNodesList = new(rows);
        _upNodesList = new(columns);
    }

    public RarefiedMatrix(List<RarefiedMatrixNode> upNodesList, List<RarefiedMatrixNode> leftNodesList)
    {
        _upNodesList = upNodesList;
        _leftNodesList = leftNodesList;
    }

    public void SetValue(int row, int column, int key, int value)
    {
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

                        existingNode.Key = key;
                        existingNode.Value = value;
                    }
                    else
                    {
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

                    nextUpNodes[currentColumn] = nextUpNodes[currentColumn].DownNode;
                    
                    continue;
                }

                if (currentColumn == column && nextUpNodes[currentColumn] == null)
                {

                    previousLeftNode = nextLeftNode;

                    nextLeftNode = nextLeftNode.RightNode;
                    
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

    public int[,] ToDefaultMatrix()
    {
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

    public override string ToString()
    {
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
}