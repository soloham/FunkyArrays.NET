namespace FunkyArrays.Core.Interfaces
{
    public interface IComplexArray<T>
    {
        IComplexArray<T> SubArray { get; set; }
        T[] Value { get; set; }
    }
}