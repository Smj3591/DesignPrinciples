using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsDemo
{
    public sealed class SingletoneEagerLoading
    {
        private static int instancecounter = 0;

        // Eager loading : Object created initially before instantiation
        private static readonly SingletoneEagerLoading _instance = new SingletoneEagerLoading();
        private SingletoneEagerLoading()
        {
            instancecounter++;
            Console.WriteLine("Instance creation counter : " + instancecounter);
        }

        // Here CLR will take care of instance creation i.e. only one instance will be created.
        public static SingletoneEagerLoading GetInstanceEager
        {
            get
            {
                return _instance;
            }
        }

    }
}
