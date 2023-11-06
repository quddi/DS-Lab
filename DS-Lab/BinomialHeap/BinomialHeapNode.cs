public class BinomialHeapNode
{
    public int Key { get; set; }
    
    public int Value { get; set; }
    
    public int Degree { get; set; }

    public BinomialHeapNode? Parent { get; set; }
    
    public BinomialHeapNode? Child { get; set; }
    
    public BinomialHeapNode? Sibling { get; set; }
}