using System;
using System.Collections.Generic;
using System.Text;

namespace MIT6._0002
{
    public class lecture2_segment2_fib
    {
        public int Fib(int n)
        {
            if(n == 0 || n == 1)
            {
                return 1;
            }
            else
            {
                return Fib(n - 1) + Fib(n - 2);
            }
        }

        public delegate int Fibonacci(int x);

        public void RunFibs(int numFibs, Fibonacci fib)
        {
            for (int i = 0; i < numFibs; i++)
            {
                Console.WriteLine($"Fib({i}) = {fib(i)}");
            }
        }

        public void Test()
        {
            RunFibs(21, Fib);
        }
    }

}
