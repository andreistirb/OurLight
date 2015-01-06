using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proiect___OurLight
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String s = Request.Params["s"];
            SqlDataSourceSearch.SelectCommand = "SELECT DISTINCT Photos.Id, Photos.Title FROM Photos_Tags INNER JOIN Photos ON Photos_Tags.PhotoId = Photos.Id INNER JOIN Tags ON Photos_Tags.TagId = Tags.Id where Tags.Text = '" + s + "'";
        }
    }
}