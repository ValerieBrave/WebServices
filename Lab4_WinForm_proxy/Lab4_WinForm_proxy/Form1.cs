using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4_WinForm_proxy
{
    public partial class Form1 : Form
    {
        public Simplex client;
        public Form1()
        {
            this.client = new Simplex();
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string s1 = this.a1_s.Text;
            string s2 = this.a2_s.Text;
            int k1 = int.Parse(this.a1_k.Text);
            int k2 = int.Parse(this.a2_k.Text);
            float f1 = float.Parse(this.a1_f.Text);
            float f2 = float.Parse(this.a2_f.Text);

            var a1 = new A { s = s1, k = k1, f = f1 };
            var a2 = new A { s = s2, k = k2, f = f2 };

            var res = client.Sum(a1, a2);

            this.res_s.Text = res.s;
            this.res_k.Text = res.k.ToString();
            this.res_f.Text = res.f.ToString();

        }
    }
}
