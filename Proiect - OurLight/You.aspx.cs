using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Proiect___OurLight
{
    public partial class You : System.Web.UI.Page
    {
        String username;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["user"] == null)
            {
                username = User.Identity.Name;
            }
            else
            {
                username = Request.Params["user"];
                CreateNewAlbumLabel.Visible = false;
                UploadPhotoLabel.Visible = false;
            }
            SqlDataSourceYourGallery.SelectCommand = "SELECT Photos.Id, Photos.Title, Photos.Description, Photos.UserId, aspnet_Users.UserId AS Expr1, aspnet_Users.UserName FROM Photos INNER JOIN aspnet_Users ON Photos.UserId = aspnet_Users.UserId where aspnet_Users.UserName = '" + username + "'";
            SqlDataSourceYourAlbums.SelectCommand = "SELECT Galleries.Id, Galleries.Title, Galleries.UserId, Galleries.Cover FROM Galleries INNER JOIN aspnet_Users ON Galleries.UserId = aspnet_Users.UserId WHERE aspnet_Users.UserName = '" + username + "'";
        }
    }
}