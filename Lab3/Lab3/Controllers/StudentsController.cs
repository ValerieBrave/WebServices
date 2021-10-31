using Lab3.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Xml.Serialization;

namespace Lab3.Controllers
{
    public class StudentsController : ApiController
    {
        private StudentContext context = new StudentContext();

        public object Get([FromUri] GetStudentsQueryObject request)
        {
            try
            {
                List<Student> students = new List<Student>();

                if (request.Sort == SortTypes.NameAsc)
                {
                    students = context.Students.OrderBy(x => x.Name).ToList();
                }
                else if (request.Sort == SortTypes.NameDesc)
                {
                    students = context.Students.OrderByDescending(x => x.Name).ToList();
                }
                else if (request.Sort == SortTypes.IdAsc)
                {
                    students = context.Students.OrderBy(x => x.StudentId).ToList();
                }
                else if (request.Sort == SortTypes.IdDesc)
                {
                    students = context.Students.OrderByDescending(x => x.StudentId).ToList();
                }

                if (request.Limit != null)
                {
                    students = students.Skip(request.Offset).Take(request.Limit.Value).ToList();
                }

                if (request.MinId.HasValue)
                {
                    students = students.Where(x => x.StudentId >= request.MinId).ToList();
                }

                if (request.MaxId.HasValue)
                {
                    students = students.Where(x => x.StudentId <= request.MaxId).ToList();
                }

                if (request.Like != null)
                {
                    students = students.Where(x => x.Name.ToLower().Contains(request.Like.ToLower())).ToList();
                }

                if (request.GlobalLike != null)
                {
                    students = students.Where(x => (x.StudentId.ToString() + x.Name + x.Phone).Contains(request.GlobalLike)).ToList();
                }

                List<StudentDto> studentDtos = new List<StudentDto>();
                string link = Request.RequestUri.Query == "" ? Request.RequestUri.AbsoluteUri : Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.Query, string.Empty);

                foreach (Student student in students)
                {
                    StudentDto studentDto = new StudentDto { StudentId = student.StudentId, Name = student.Name, Phone = student.Phone };
                    studentDto.Links = new List<LinkDto>
                {
                    new LinkDto($"{link}/{studentDto.StudentId}", "self", "GET"),
                    new LinkDto($"{link}/{studentDto.StudentId}", "UpdateStudent", "PUT"),
                    new LinkDto($"{link}/{studentDto.StudentId}", "DeleteStudent", "DELETE"),
                };
                    studentDtos.Add(studentDto);
                }

                if (request.Format == ResponseTypes.XML)
                {
                    using (var writer = new StringWriter())
                    {
                        XmlSerializer serializer;

                        if (request.Columns == null || request.Columns.Length == 0)
                        {
                            serializer = new XmlSerializer(studentDtos.GetType());
                        }
                        else
                        {
                            XmlAttributes studentDtoPropertyAttributes = new XmlAttributes();
                            studentDtoPropertyAttributes.XmlIgnore = true;

                            XmlAttributeOverrides studentDtoAttributes = new XmlAttributeOverrides();
                            foreach (var prop in typeof(StudentDto).GetProperties())
                            {
                                if (!request.Columns.ToLower().Contains(prop.Name.ToLower()) && prop.Name.ToLower() != "links")
                                {
                                    studentDtoAttributes.Add(typeof(StudentDto), prop.Name, studentDtoPropertyAttributes);
                                }
                            }

                            serializer = new XmlSerializer(studentDtos.GetType(), studentDtoAttributes);
                        }

                        XmlMediaTypeFormatter formatter = new XmlMediaTypeFormatter
                        {
                            UseXmlSerializer = true
                        };
                        formatter.SetSerializer(studentDtos.GetType(), serializer);
                        return Content(HttpStatusCode.OK, studentDtos, formatter);
                    }
                }

                string result = request.Columns == null || request.Columns.Length == 0 ? JsonConvert.SerializeObject(studentDtos) : JsonConvert.SerializeObject(studentDtos,
                    Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { ContractResolver = new DynamicContractResolver(request.Columns.ToLower().Split(',').Append("links").Append("href").Append("rel").Append("method").ToArray()) });

                return Content(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new { status = HttpStatusCode.BadRequest, href = "http://localhost:54218/api/errors/500" });
            }
        }


        public object Get(int id, [FromUri] ResponseTypes format = ResponseTypes.JSON)
        {
            Student student = context.Students.Find(id);

            if (student == null)
            {
                return Content(HttpStatusCode.BadRequest, new { status = HttpStatusCode.BadRequest, href = "http://localhost:54218/api/errors/404" });
            }
            else
            {
                if (format == ResponseTypes.XML)
                {
                    using (var writer = new StringWriter())
                    {
                        XmlSerializer serializer = new XmlSerializer(student.GetType());
                        serializer.Serialize(writer, student);

                        return Content(HttpStatusCode.OK, writer.ToString());
                    }
                }
                return Content(HttpStatusCode.OK, student);
            }
        }

        public object Post([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, new { status = HttpStatusCode.BadRequest, href = "http://localhost:54218/api/errors/400" });
            }
            Student newStudent = context.Students.Add(student);
            context.SaveChanges();
            return Content(HttpStatusCode.OK, newStudent);
        }


        public object Put(int id, [FromBody] Student student)
        {
            if(context.Students.Find(id) == null)
                return Content(HttpStatusCode.BadRequest, new { status = HttpStatusCode.BadRequest, href = "http://localhost:54218/api/errors/404" });
            Student to_change = context.Students.Find(id);
            to_change.Name = student.Name;
            to_change.Phone = student.Phone;
            context.Entry(to_change).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }


        public object Delete(int id)
        {
            Student student = context.Students.Find(id);
            if (student == null)
            {
                return Content(HttpStatusCode.BadRequest, new { status = HttpStatusCode.BadRequest, href = "http://localhost:54218/api/errors/404" });
            }
            context.Students.Remove(student);
            context.SaveChanges();
            return Ok();
        }
    }
}
