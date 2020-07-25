using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyArrays.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Sort a string array in the descending order.
        /// </summary>
        public static string[] SortDescending(this string[] value)
        {
            int totalStrings = value.Length;
            string[] result = new string[totalStrings];

            //Cache the current indices to retrieve later for matching
            int[][] arrChache = new int[totalStrings][];
            //Array containing integers for characters in each string element in the 'value' array
            int[][] arr = new int[totalStrings][];

            //Assign values to 'arrCache' & 'arr' for each element
            for (int i = 0; i < totalStrings; i++)
            {
                //Convert element to lower case for easy manipulation and post-fix the actual index for later retrival
                string element = value[i].ConvertToLowerCase() + (char)i;
                arrChache[i] = new int[element.Length];
                arr[i] = new int[element.Length];

                for (int j = 0; j < element.Length; j++)
                {
                    arrChache[i][j] = element[j];
                    arr[i][j] = element[j];
                }
            }

            int[] placeholder;
            // Iterate 0 to one less than the array length 
            for (int i = 0; i < arr.Length - 1; i++)
            {
                // Iterate 1 to the array length 
                for (int j = i + 1; j < arr.Length; j++)
                {
                    // Compare with next element
                    int leastCount = arr[i].Length > arr[j].Length ? arr[j].Length : arr[i].Length;
                    for (int k = 0; k < leastCount-1; k++)
                    {
                        if (arr[i][k] < arr[j][k])
                        {
                            placeholder = arr[i];
                            arr[i] = arr[j];
                            arr[j] = placeholder;

                            break;
                        }
                        else if (arr[i][k] > arr[j][k])
                            break;
                    }
                }
            }

            //Generate the sorted strings by the courtsy of sorted indices
            for (int i = 0; i < totalStrings; i++)
            {
                //Retrieving actual index after manipulation as the initally post-fixed index
                result[i] = value[arr[i][arr[i].Length - 1]];
            }

            return result;
        }
    }
}
