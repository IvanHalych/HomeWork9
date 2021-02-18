using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimesNumber
{
    public static class PrimeNumber
    {
        public static bool IsPrime(this int number)
        {
            if (number > 1)
            {
                var sqrt = Math.Sqrt(number);
                var count = ((int)sqrt) - 1;
                var range = Enumerable.Range(2, count);
                return range.All(n => (number % n) != 0);
            }
            else 
                return false;
        }
    }
}
