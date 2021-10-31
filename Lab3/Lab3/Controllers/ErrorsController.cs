using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Xml.Serialization;

namespace Lab3.Controllers
{
    public class ErrorsController : ApiController
    {
        [Route("api/errors/{code}")]
        public object Get([FromUri] int code)
        {
            Error error = new Error()
            {
                Code = code
            };
            switch (code)
            {
                case 400:
                    error.Message = "client error. item is invalid";
                    break;
                case 404:
                    error.Message = "client error. item not found";
                    break;
                case 500:
                    error.Message = "server error. internal exception";
                    break;
                default:
                    error.Message = "unknown error.";
                    break;
            }
            XmlSerializer res = new XmlSerializer(typeof(Error));
            XmlMediaTypeFormatter formatter = new XmlMediaTypeFormatter
            {
                UseXmlSerializer = true
            };
            formatter.SetSerializer(typeof(Error), res);
            return Content(HttpStatusCode.OK, error, formatter);
        }
    }
}
