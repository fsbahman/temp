using SomeLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace test_aspx
{
    public partial class test1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            var class1 = new Class1();
            txt3.Text = class1.sum(int.Parse(txt1.Text), int.Parse(txt2.Text)).ToString();
        }
    }
}