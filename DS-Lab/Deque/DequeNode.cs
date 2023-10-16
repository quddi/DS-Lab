public class DequeNode<T>
{
    public T Data { get; set; }
    public DequeNode<T> Next { get; set; }
    public DequeNode<T> Previous { get; set; }

    public DequeNode(T data)
    {
        Data = data;
        Next = null;
        Previous = null;
    }

    public void SwapParentAndChild()
    {
        (Next, Previous) = (Previous, Next);
    }
}