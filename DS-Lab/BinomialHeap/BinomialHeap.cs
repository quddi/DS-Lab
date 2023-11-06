using System.Text;

public class BinomialHeap
{
    private BinomialHeapNode? _firstTreeRoot;
    
    public override string ToString()
    {
        if (_firstTreeRoot == null)
            return "Empty binomial heap."; 
        
        var stringBuilder = new StringBuilder();

        while (_firstTreeRoot != null)
        {
            
        }
        
        return stringBuilder.ToString();
    }
}