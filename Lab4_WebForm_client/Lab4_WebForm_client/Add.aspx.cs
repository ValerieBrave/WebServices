using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab4_WebForm_client
{
    public partial class Add : System.Web.UI.Page
    {
        public Simplex client;
        protected void Page_Load(object sender, EventArgs e)
        {
            client = new Simplex();
        }
        protected void Sum_Click(object sender, EventArgs e)
        {
            int x, y;
            if (int.TryParse(first.Text.ToString(), out x) && int.TryParse(second.Text.ToString(), out y))
            {
                result.Text = client.Add(x, y).ToString();
                
            }
            else
            {
                result.Text = "Error!";
            }
        }
    }
}