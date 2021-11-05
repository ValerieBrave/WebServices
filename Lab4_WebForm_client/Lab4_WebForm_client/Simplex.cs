using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab4_WebForm_client
{
    public class Simplex : ISimplexSoap
    {
        public int Add(int x, int y)
        {
            return x+y;
        }

        public int Adds(int x, int y)
        {
            return x+y;
        }

        public string Concat(string s, double d)
        {
            return s+d.ToString();
        }

        public A Sum(A a1, A a2)
        {
            return new A { s = a1.s + a2.s, k = a1.k + a2.k, f = a1.f + a1.f };
        }
    }
}