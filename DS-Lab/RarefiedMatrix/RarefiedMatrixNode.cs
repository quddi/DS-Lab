public class RarefiedMatrixNode
{
    public int Value { get; set; }
    
    public RarefiedMatrixNode? RightNode { get; set; }
    public RarefiedMatrixNode? DownNode { get; set; }
}