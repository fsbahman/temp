using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace customHttpError
{
    public partial class largefile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            throw new HttpException(500, "Not found");
        }
    }
}