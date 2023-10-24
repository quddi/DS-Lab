public class RarefiedMatrixNode
{
    public int Key { get; set; }
    
    public int Value { get; set; }
    
    public RarefiedMatrixNode? RightNode { get; set; }
    public RarefiedMatrixNode? DownNode { get; set; }

    public void SwapNextNodes()
    {
        (RightNode, DownNode) = (DownNode, RightNode);
    }
}