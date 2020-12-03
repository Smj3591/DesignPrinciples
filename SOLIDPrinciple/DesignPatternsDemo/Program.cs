using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // This Works fine in single thread environment only [ITERATION 1]
            //Singletone instance1 = Singletone.GetInstance;
            //instance1.PrintData("First instance data");
            //Singletone instance2 = Singletone.GetInstance;
            //instance2.PrintData("Second instance data");

            // Multithread call [ITERATION 2]

            Parallel.Invoke
            (
                ()=>PrintFirstData(),
                ()=>PrintSecondData()
            );

            Console.ReadLine();
        }

        private static void PrintFirstData()
        {
            Singletone instance1 = Singletone.GetInstance;
            instance1.PrintData("First instance data");
        }

        private static void PrintSecondData()
        {
            Singletone instance1 = Singletone.GetInstance;
            instance1.PrintData("First instance data");
        }
    }
}
