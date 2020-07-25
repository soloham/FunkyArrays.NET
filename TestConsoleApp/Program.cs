using FunkyArrays;
using FunkyArrays.Core;
using FunkyArrays.Extensions;
using System;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //CASE #1
            //--Inputs
            //---FlatArray:
            string[] i11 = new[] { "Islamabad", "lahore", "karachi", "SARGODHA", "mulTan" };
            //---ComplexArray:
            ComplexArray<string>[] i12 = new ComplexArray<string>[] {
                new ComplexArray<string>(
                    new[] { "islamabad" }, 
                    new ComplexArray<string>(new[] { "Lahore" }, 
                    new ComplexArray<string>(new[] {"sARGODHA" }))
                )
            };
            //--Operation
            string[] case1 = ArrayOperations.MergeElements(i11, i12);
            //--Output
            Console.WriteLine("----------------------\nCASE #1: \n----------------------");
            LogArray(case1);


            //CASE #2
            //--Inputs
            //---FlatArray:
            string[] i21 = new[] { "Islamabad", "lahore", "karachi", "SARGODHA", "mulTan" };
            //---ComplexArray:
            ComplexArray<string>[] i22 = new ComplexArray<string>[] {
                new ComplexArray<string>(
                    new[] {"islamabad" }, 
                    new ComplexArray<string>(new[] {"Lahore" }, 
                    new ComplexArray<string>(new[] {"guJrat" }))
                )
            };
            //--Operation
            (string[] camelCased, string[] lowerCased, string[] upperCased) case2 
                = ArrayOperations.ExtractCased(i21, i22, requireUnique: false);
            //--Output
            Console.WriteLine("\n\n----------------------\nCASE #2: \n----------------------");
            Console.WriteLine("> Camel Cased: \n-----------");
            LogArray(case2.camelCased);
            Console.WriteLine("-----------");
            Console.WriteLine("> Lower Cased: \n-----------");
            LogArray(case2.lowerCased);
            Console.WriteLine("-----------");
            Console.WriteLine("> Upper Cased: \n-----------");
            LogArray(case2.upperCased);
            Console.WriteLine("-----------");


            //CASE #3
            //--Inputs
            //---FlatArray:
            string[] i31 = new[] { "Islamabad", "lahore", "karachi", "SARGODHA", "guJraT", "mulTan" };
            //---ComplexArray:
            ComplexArray<string>[] i32 = new ComplexArray<string>[] {
                new ComplexArray<string>(
                    new[] {"islamabad" }, 
                    new ComplexArray<string>(new[] {"Lahore" }, 
                    new ComplexArray<string>(new[] {"fAiSalaBad" }))
                )
            };
            //--Operation 1:
            var case3 = ArrayOperations.MergeCasedAndOrder(i31, i32, requireUnique: false);
            //--Output 1
            Console.WriteLine("\n\n---------------------------\nCASE #3 (Cased Merge): \n---------------------------");
            LogArray(case3);
            //--Operation 2:
            case3 = case3.SortDescending(); 
            //--Output 2
            Console.WriteLine("----------------------\n(Sorted): \n----------------------");
            LogArray(case3);

            Console.ReadKey();
        }

        public static void LogArray(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine($"{i + 1} -> {arr[i]}");
            }
        }
    }
}
