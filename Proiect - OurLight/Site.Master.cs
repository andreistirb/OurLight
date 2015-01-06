using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proiect___OurLight
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        TextBox mSearchTextBox;

        protected void Page_Load(object sender, EventArgs e)
        {
            mSearchTextBox = (TextBox) this.MenuLoginView.FindControl("SearchTextBoxID");
        }

        protected void mSearch(object Sender, EventArgs e)
        {
            Response.Redirect("~/Search.aspx?s=" + (mSearchTextBox.Text).ToLower());
        }

    }
}
