using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsDemo
{
    class SingletoneLazyLoadingWithLazyKeyword
    {
        private static int instancecounter = 0;

        // Lazy keyword itself a thread safe so it avoid multiple object creation as well as race condition....
        private static readonly Lazy<SingletoneLazyLoadingWithLazyKeyword> _instance = 
            new Lazy<SingletoneLazyLoadingWithLazyKeyword>();

        private SingletoneLazyLoadingWithLazyKeyword()
        {
            instancecounter++;
            Console.WriteLine("Instance creation counter : " + instancecounter);
        }

        public static SingletoneLazyLoadingWithLazyKeyword GetInstance
        {
            get
            {
                return _instance.Value;
            }
        }
    }
}
