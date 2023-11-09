using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BinomialHeapNode
{
    public int Key { get; set; }
    
    public int Value { get; set; }
    
    public int Degree { get; set; }

    public BinomialHeapNode? Parent { get; set; }
    
    public BinomialHeapNode? Child { get; set; }
    
    public BinomialHeapNode? Sibling { get; set; }
    
    public string ToString(bool printZeroLevel)
    {
        return ToStringHelper(this, 0, Degree, 
            Enumerable.Range(printZeroLevel ? 0 : 1, Degree).ToHashSet());
    }

    private static string ToStringHelper(BinomialHeapNode node, int level, int maxDegree, HashSet<int> levelsForStick)
    {
        var stringBuilder = new StringBuilder();
        
        for (int i = 0; i < level; i++)
            stringBuilder.Append(levelsForStick.Contains(maxDegree - i) ? "|   " : "    ");
        
        stringBuilder.Append(node.Value);

        if (node.Child != null) 
            stringBuilder.Append("---▼");

        if (node.Sibling == null && node.Degree != maxDegree) 
            levelsForStick.Remove(node.Degree);

        stringBuilder.Append("\n");

        var currentNode = node.Child;

        while (currentNode != null)
        {
            stringBuilder.Append(ToStringHelper(currentNode, level + 1, maxDegree, levelsForStick));

            currentNode = currentNode.Sibling;
        }

        return stringBuilder.ToString();
    }
}