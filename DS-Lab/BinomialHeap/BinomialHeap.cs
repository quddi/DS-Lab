using System.Text;

public class BinomialHeap
{
    private BinomialHeapNode? _firstTreeRoot;

    public BinomialHeap(BinomialHeapNode? firstTreeRoot)
    {
        _firstTreeRoot = firstTreeRoot;
    }

    public override string ToString()
    {
        if (_firstTreeRoot == null)
            return "Empty binomial heap."; 
        
        var stringBuilder = new StringBuilder();

        var currentSubTreeRoot = _firstTreeRoot;
        
        while (currentSubTreeRoot != null)
        {
            var isLastTree = currentSubTreeRoot.Sibling == null;
            
            stringBuilder.Append(currentSubTreeRoot.ToString(isLastTree));

            if (!isLastTree)
                stringBuilder.Append("|\n|\n");

            currentSubTreeRoot = currentSubTreeRoot.Sibling;
        }
        
        return stringBuilder.ToString();
    }
}