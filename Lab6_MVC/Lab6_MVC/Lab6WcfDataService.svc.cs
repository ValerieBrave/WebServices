//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using Lab6_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Data.Services.Providers;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;

namespace Lab6_MVC
{
    public class Lab6WcfDataService : EntityFrameworkDataService<WS_SVVEntities1>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }
        [WebGet]
        public IQueryable<Note> ChangeNote(int id, String subject, int note1, int studentId)
        {
            WS_SVVEntities1 context = this.CurrentDataSource;
            Note note = context.Note.Find(id);
            note.Subj = subject;
            note.StudentId = studentId;
            note.Note1 = note1;
            context.SaveChanges();
            return context.Note;
        }

        [WebGet]
        public IQueryable<Note> InsertNote(String subject, int note1, int studentId)
        {
            Note note = new Note();
            note.Subj = subject;
            note.StudentId = studentId;
            note.Note1 = note1;
            WS_SVVEntities1 context = this.CurrentDataSource;
            context.Note.Add(note);
            context.SaveChanges();
            return context.Note;
        }

        [WebGet]
        public IQueryable<Student> ChangeStudent(int id, String name)
        {
            WS_SVVEntities1 context = this.CurrentDataSource;
            Student student = context.Student.Find(id);
            student.Name = name;
            context.SaveChanges();
            return context.Student;
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        //[WebGet(ResponseFormat = WebMessageFormat.Json)]
        public IQueryable<Student> InsertStudent(String Name)
        {
            Student student = new Student();
            student.Name = Name;
            WS_SVVEntities1 context = this.CurrentDataSource;
            context.Student.Add(student);
            context.SaveChanges();
            return context.Student;
        }
        
    }
}
