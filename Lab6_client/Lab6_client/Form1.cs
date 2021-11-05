﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WS_SVVModel;

namespace Lab6_client
{
    public partial class Form1 : Form
    {
        WS_SVVEntities1 client;
        public Form1()
        {
            client = new WS_SVVEntities1(new Uri("http://localhost:57310/Lab6WcfDataService.svc/"));
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.resultBox.Text = "";
            foreach (var student in client.Student.AsEnumerable())
            {
                this.resultBox.Text += string.Format("Id {0}: {1}\n", student.Id, student.Name);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.resultBox.Text = "";
            foreach (var note in client.Note.AsEnumerable())
            {
                this.resultBox.Text += string.Format("Id {0}: Note '{1}' on exam '{2}' (Student ID: {3})\n", note.Id, note.Note1, note.Subj, note.StudentId);
            }
        }
    }
}
