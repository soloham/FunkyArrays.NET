using FunkyArrays.Core.Interfaces;

namespace FunkyArrays.Core
{
    /// <summary>
    /// Use this object to create a complex hierarchy of nested object of type T
    /// </summary>
    public class ComplexArray<T> : IComplexArray<T>
    {
        public ComplexArray(T[] value, IComplexArray<T> subArray = null)
        {
            Value = value;
            SubArray = subArray;
        }

        /// <summary>
        /// Value of the object's type
        /// </summary>
        public T[] Value { get; set; }

        /// <summary>
        /// Self composed object for enabling complex nested hierarchy
        /// </summary>
        public IComplexArray<T> SubArray { get; set; }

        /// <summary>
        /// Traverses through the nested graph of the array of ComplexArray objects and generates a flat representation of the specified Type
        /// </summary>
        public static T[] FlattenArray(IComplexArray<T>[] complexArray)
        {
            T[] output = new T[0];

            for (int i = 0; i < complexArray.Length; i++)
            {
                T[] _output = FlattenSingle(complexArray[i]);
                for (int y = 0; y < _output.Length; y++)
                {
                    ArrayOperations.Push(ref output, _output[y]);
                }
            }

            return output;
        }

        /// <summary>
        /// Traverses through the nested graph and generates a flat representation of the specified Type
        /// </summary>
        public static T[] FlattenSingle(IComplexArray<T> single)
        {
            T[] output = new T[0];

            for (int i = 0; i < single.Value.Length; i++)
            {
                ArrayOperations.Push(ref output, single.Value[i]);
            }
            if (single.SubArray != null)
            {
                T[] subOutput = FlattenSingle(single.SubArray);
                for (int i = 0; i < subOutput.Length; i++)
                {
                    ArrayOperations.Push(ref output, subOutput[i]);
                }
            }

            return output;
        }
    }
}
