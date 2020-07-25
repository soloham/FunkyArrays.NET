using FunkyArrays.Enums;
using FunkyArrays.Extensions;
using System;
using System.Text.RegularExpressions;

namespace FunkyArrays.Core
{
    public static class ArrayOperations
    {
        /// <summary>
        /// Merge the provided string arrays into one flat array
        /// </summary>
        public static string[] MergeElements(params string[][] inputs)
        {
            string[] output = new string[0];

            for (int i = 0; i < inputs.Length; i++)
            {
                string[] curArray = inputs[i];
                for (int y = 0; y < curArray.Length; y++)
                {
                    Push(ref output, curArray[y]);
                }
            }

            return output;
        }

        /// <summary>
        /// Merge the provided string arrays and the ComplexArray object's array into one flat array
        /// <seealso cref="FunkyArrays.ComplexArray"/>
        /// </summary>
        public static string[] MergeElements(string[] flatInput, ComplexArray<string>[] complexInput, bool requireUnique = true)
        {
            string[] output = new string[0];

            string[] flattenedComplexInput = ComplexArray<string>.FlattenArray(complexInput);

            int maxElements = flatInput.Length > flattenedComplexInput.Length ? 
                flatInput.Length : flattenedComplexInput.Length;

            for (int i = 0; i < maxElements; i++)
            {
                string flatEle = i < flatInput.Length? flatInput[i] : null;
                string flattenedEle = i < flattenedComplexInput.Length ? flattenedComplexInput[i] : null;

                if(flatEle != null && !Exists(output, flatEle, ignoreCase: requireUnique)) 
                    Push(ref output, flatEle);

                if(flattenedEle != null && !Exists(output, flattenedEle, ignoreCase: requireUnique)) 
                    Push(ref output, flattenedEle);
            }

            return output;
        }

        /// <summary>
        /// Extract arrays corresponding to various cases found in the input array and the complex array
        /// </summary>
        public static (string[] camelCased, string[] lowerCased, string[] upperCased) ExtractCased(string[] flatInput, ComplexArray<string>[] complexInput, bool requireUnique = false) 
        {
            string[] combined = MergeElements(flatInput, complexInput, requireUnique);
            return ExtractCased(combined);
        }
        /// <summary>
        /// Extract arrays corresponding to various cases including un-matched cases found in the input array and the complex array
        /// </summary>
        public static (string[] camelCased, string[] lowerCased, string[] upperCased, string[] unMathed) ExtractCasedInclusive(string[] flatInput, ComplexArray<string>[] complexInput, bool requireUnique = false)
        {
            string[] combined = MergeElements(flatInput, complexInput, requireUnique);
            return ExtractCasedInclusive(combined);
        }

        /// <summary>
        /// Extract arrays corresponding to various cases including un-matched cases found in the input array
        /// </summary>
        public static (string[] camelCased, string[] lowerCased, string[] upperCased, string[] unMathed) ExtractCasedInclusive(string[] flatInput)
        {
            string[] camelOutput = new string[0];
            string[] lowerOutput = new string[0];
            string[] upperOutput = new string[0];
            string[] unMathedOutput = new string[0];

            for (int i = 0; i < flatInput.Length; i++)
            {
                string element = flatInput[i];

                StringCase elementCase = GetCase(element);
                switch (elementCase)
                {
                    case StringCase.CAMEL:
                        Push(ref camelOutput, element);
                        break;
                    case StringCase.LOWER:
                        Push(ref lowerOutput, element);
                        break;
                    case StringCase.UPPER:
                        Push(ref upperOutput, element);
                        break;
                    case StringCase.UNIDENTIFIED:
                        Push(ref unMathedOutput, element);
                        break;
                }
            }

            return (camelOutput, lowerOutput, upperOutput, unMathedOutput);
        }

        /// <summary>
        /// Extract arrays corresponding to various cases found in the input array
        /// </summary>
        public static (string[] camelCased, string[] lowerCased, string[] upperCased) ExtractCased(string[] flatInput)
        {
            string[] camelOutput = new string[0];
            string[] lowerOutput = new string[0];
            string[] upperOutput = new string[0];

            for (int i = 0; i < flatInput.Length; i++)
            {
                string element = flatInput[i];

                StringCase elementCase = GetCase(element);
                switch (elementCase)
                {
                    case StringCase.CAMEL:
                        Push(ref camelOutput, element);
                        break;
                    case StringCase.LOWER:
                        Push(ref lowerOutput, element);
                        break;
                    case StringCase.UPPER:
                        Push(ref upperOutput, element);
                        break;
                    case StringCase.UNIDENTIFIED:
                        break;
                }
            }

            return (camelOutput, lowerOutput, upperOutput);
        }

        /// <summary>
        /// Merge the provided string arrays and the ComplexArray object's array into one flat array and order them in the desending order
        /// <seealso cref="FunkyArrays.ComplexArray"/>
        /// </summary>
        public static string[] MergeCasedAndOrder(string[] flatInput, ComplexArray<string>[] complexInput, bool requireUnique = false)
        {
            (string[] camelCased, string[] lowerCased, string[] upperCased) result = ExtractCased(flatInput, complexInput, requireUnique);
            return MergeElements(result.camelCased, result.upperCased, result.lowerCased);
        }

        /// <summary>
        /// Gets the case of the string
        /// </summary>
        public static StringCase GetCase(string input)
        {
            StringCase result = StringCase.UNIDENTIFIED;

            if(Regex.Match(input, "^[a-z]+([A-Z]([a-z0-9]|[A-Z])+)+").Success)
            {
                result = StringCase.CAMEL;
            }
            else if (Regex.Match(input, "^[a-z]+$").Success)
            {
                result = StringCase.LOWER;
            }
            else if (Regex.Match(input, "^[A-Z]+$").Success)
            {
                result = StringCase.UPPER;
            }

            return result;
        }

        /// <summary>
        /// Checks if element exists or not in the provided array
        /// </summary>
        public static bool Exists(in string[] arr, string element, bool ignoreCase = false)
        {
            bool exists = false;
            for (int i = 0; i < arr.Length; i++)
            {
                exists = ignoreCase? arr[i].ConvertToLowerCase() == element.ConvertToLowerCase() : arr[i] == element;
                if (exists) break;
            }
            return exists;
        }

        /// <summary>
        /// Checks if element of type T exists in the array
        /// </summary>
        public static bool Exists<T>(in T[] arr, T element)
        {
            bool exists = false;
            for (int i = 0; i < arr.Length; i++)
            {
                exists = arr[i].Equals(element);
                if (exists) break;
            }
            return exists;
        }

        /// <summary>
        /// Increase Array size and Add the value to it
        /// </summary>
        public static int Push<T>(ref T[] array, T value)
        {
            Array.Resize(ref array, array.Length + 1);
            var index = Array.IndexOf(array, default(T));

            if (index != -1)
            {
                array[index] = value;
            }

            return index;
        }

    }
}
