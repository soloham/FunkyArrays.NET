using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyArrays.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Convert a string to lowercase characters
        /// </summary>
        public static string ConvertToLowerCase(this string value)
        {
            string lowered = "";

            //Iterate over each character to extract its index & determine it's case
            foreach (char character in value)
            {
                int i = character;
                //If the character is in the ASCII range of Uppercase Alphabets
                if (character >= 65 && character < 91)
                {
                    //Then get the lowercase version and append to the output string
                    lowered += (char)(i + 32);
                }
                else
                    //Otherwise just append to the output
                    lowered += character;
            }

            return lowered;
        }
    }
}
