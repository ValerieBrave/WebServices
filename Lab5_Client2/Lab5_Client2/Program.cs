using Lab5_Client2.WCFSimplexReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_Client2
{
    class Program
    {
        static void Main(string[] args)
        {
            WCFServiceInterfaceClient client = new WCFServiceInterfaceClient("tcpEndpoint");

            var sumResult = client.Sum(new A { f = 3.2f, k = 1, s = "4" }, new A { f = 1.3f, k = 2, s = "12" });

            Console.WriteLine($"Sum\nf = {sumResult.f}\nk = {sumResult.k}\ns = {sumResult.s}");
            Console.WriteLine($"\n\nConcat\nresult = " + client.Concat(sumResult.s, sumResult.f));
            Console.WriteLine($"\n\nAdd\nresult = " + client.Add(sumResult.k, 4));

            client.Close();
            Console.ReadKey();
        }
    }
}
