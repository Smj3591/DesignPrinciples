using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsDemo
{
    // to avoid inheritance outside the class 
    // as well as within a class (nested class) too
    // so make it as sealed
    public sealed class Singletone
    {
        private static Singletone _instance = null;
        private static int instancecounter = 0;
        private static readonly object objlock = new object();
        // private constructor helps in external inheritance as well as instantiation of class
        private Singletone()
        {
            instancecounter++;
            Console.WriteLine("No of instances created : " + instancecounter);
        }

        // This is only way to create object so made it as a public property
        public static Singletone GetInstance
        {
            // It is also called as lazy instantiation

            // Here Only one instance is created...
            #region "Single Thread Environment"
            // This Works fine in single thread environment only [ITERATION 1]
            //get
            //{
            //    if (_instance == null)
            //        _instance = new Singletone();
            //    return _instance;
            //}

            #endregion

            #region "Multi Threaded Environment"
            // This Works fine in multi threaded environment [ITERATION 2]
            get
            {
                // double check locking
                if (_instance == null)
                {
                    lock (objlock) // Lock is used to avoid race condition and only one instance creation
                    {
                        if (_instance == null)
                            _instance = new Singletone();
                    }
                }
                return _instance;
            }

            #endregion

        }
        public void PrintData(string message)
        {
            Console.WriteLine(message);
        }
    }
}
