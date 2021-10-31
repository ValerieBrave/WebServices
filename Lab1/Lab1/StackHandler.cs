using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace Lab1
{
    public class StackHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable { get { return true; } }
        public static int Result { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Session["stack"] == null) context.Session["stack"] = new Stack<int>();    // creating stack on the very first request
                Stack<int> stack = (Stack<int>)context.Session["stack"];  // filling empty stack with session data
                switch (context.Request.HttpMethod)
                {
                    case "GET":
                        {
                            context.Response.ContentType = "json/text";
                            
                            context.Response.Write(new JavaScriptSerializer().Serialize(new { result = Result, stack }));
                        } break;
                    case "POST":
                        {
                            Result = int.Parse(context.Request.Params["result"]);
                        } break;
                    case "PUT":
                        {
                            stack.Push(int.Parse(context.Request.Params["push"]));
                            context.Session["stack"] = stack;   // synchronizing stack in handler with stack in session
                            Result += stack.Peek();
                        } break;
                    case "DELETE":
                        {
                            if(stack.Count != 0)
                            {
                                stack.Pop();
                                context.Session["stack"] = stack;   // synchronizing stack in handler with stack in session
                                Result += stack.Peek();
                            }
                        } break;
                    default:
                        {
                            context.Response.StatusCode = 405;
                            context.Response.End();
                        } break;
                }
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "json/text";
                context.Response.Write(new JavaScriptSerializer().Serialize(new { error = e.Message }));
            }
        }
    }
}
