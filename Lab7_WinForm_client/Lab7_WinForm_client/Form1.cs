using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7_WinForm_client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //rss
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(@"http://localhost:8008/FeedService/students/" + textBox1.Text + "/notes?format=rss");
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            string content = new StreamReader(res.GetResponseStream()).ReadToEnd();
            richTextBox1.Text = content;
        }

        private void button2_Click(object sender, EventArgs e) //atom
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(@"http://localhost:8008/FeedService/students/" + textBox1.Text + "/notes?format=atom");
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            string content = new StreamReader(res.GetResponseStream()).ReadToEnd();
            richTextBox1.Text = content;
        }

       
    }
}
