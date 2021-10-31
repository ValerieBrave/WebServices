using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Http.Results;
using Newtonsoft.Json;
using System.Web.SessionState;
using System.Web.Mvc;

namespace Lab2.Controllers
{
    [SessionState(SessionStateBehavior.Required)]
    public class ValuesController : ApiController
    {
        int result; // session object
        Stack<int> stack; // for multiple connections
        public ValuesController()
        {
            this.result = HttpContext.Current.Session["result"] == null? 0: (int)HttpContext.Current.Session["result"];
            this.stack = (Stack<int>)HttpContext.Current.Application["stack"] == null ? new Stack<int>() : (Stack<int>)HttpContext.Current.Application["stack"];
        }
      
        // GET api/values/5
        public string Get()
        {
            
            return JsonConvert.SerializeObject(new { result = this.result, stack = (Stack<int>)HttpContext.Current.Application["stack"] });
        }

        // POST api/values
        public void Post(int result)
        {
            this.result = result;
            HttpContext.Current.Session["result"] = result;
        }

        // PUT api/values/5
        public void Put(int add)
        {
            this.stack.Push(add);
            HttpContext.Current.Application["stack"] = stack;
            this.result += add;
            HttpContext.Current.Session["result"] = this.result;
        }

        // DELETE api/values/5
        public void Delete()
        {
            if (this.stack.Count > 0)
            {
                this.stack.Pop();
                this.result += this.stack.Peek();
                HttpContext.Current.Session["result"] = this.result;
            }
            HttpContext.Current.Application["stack"] = stack;
        }
    }
}
