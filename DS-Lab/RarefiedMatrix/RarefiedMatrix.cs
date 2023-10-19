using System.Collections.Generic;
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

    public override string ToString()
    {
        var mainStringBuilder = new StringBuilder();
        var currentStringBuilder = new StringBuilder();

        var upNodes = new List<RarefiedMatrixNode>(_upNodesList);

        for (int row = 0; row < _leftNodesList.Count; row++)
        {
            var currentNode = _leftNodesList[row];
            
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