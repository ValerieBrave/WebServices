using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Lab4
{
    [WebService(Namespace = "http://svv/", Description ="WebServices lab #4")]
    [ScriptService]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]
    public class Simplex : System.Web.Services.WebService
    {
        [WebMethod(MessageName ="Sum_1", Description ="Return sum of two ints")]
        public int Add(int x, int y)
        {
            return x + y;
        }
        [WebMethod(MessageName = "Sum_2", Description ="Return concat of string and double")]
        public string Concat(string s, double d)
        {
            return s+d.ToString();
        }
        [WebMethod(MessageName = "Sum_3", Description ="Return new object from two provided")]
        public A Sum(A a1, A a2)
        {
            return new A
            {
                s = a1.s + a2.s,
                k = a1.k + a2.k,
                f = a1.f + a2.f
            };
        }

        //////////////////////
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        [WebMethod(MessageName = "Sum_4", Description = "Sum of 2 int. Response JSON")]

        public int Adds(int x, int y)
        {
            return x + y;
        }
    }
}
