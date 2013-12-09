using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossingSimulation
{
    class Program
    {
        public struct Car
        {
            int inTime;
            int outTime;
            int position;
            public Car(int a, int b, int c)
            {
                inTime = a;
                outTime = b;
                position = c;
            }
        }
        static void Main(string[] args)
        {
        }
    }
}
