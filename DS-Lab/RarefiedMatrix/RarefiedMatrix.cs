using System.Collections.Generic;

public class RarefiedMatrix
{
    private List<RarefiedMatrix> _upNodesList;
    private List<RarefiedMatrix> _leftNodesList;

    public int Rows => _leftNodesList.Count;
    public int Columns => _upNodesList.Count;
    
    public RarefiedMatrix(int rows, int columns)
    {
        _leftNodesList = new List<RarefiedMatrix>(rows);
        _upNodesList = new List<RarefiedMatrix>(columns);
    }
}