using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class WCFSimplex : WCFServiceInterface
    {
        public int Add(int x, int y)
        {
            return x + y;
        }

        public string Concat(string x, double y)
        {
            return x + y.ToString();
        }

        public A Sum(A x, A y)
        {
            return new A {s= x.s + y.s, k = x.k + y.k, f = x.f + y.f } ;
        }
    }
}
