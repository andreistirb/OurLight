using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace Proiect___OurLight
{
    public partial class NewGallery : System.Web.UI.Page
    {

        SqlConnection con;
        SqlCommand command;
        SqlDataReader dr;

        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            command = new SqlCommand();
        }

        protected void btn_Create(object sender, EventArgs e)
        {
            Guid userId = Guid.Empty;
            String userName = User.Identity.Name;
            command.Connection = con;
            con.Open();

            //get current user ID
            command.CommandText = "SELECT UserId from aspnet_Users where UserName = '" + userName + "'";
            dr = command.ExecuteReader();
            if (dr.Read())
            {
                userId = dr.GetGuid(0);
            }
            dr.Close();

            //add new album to database
            command.CommandText = "Insert into Galleries(Title,Description,UserId) values (@Title,@Description,@UserId)";
            command.Parameters.Add("@Title", System.Data.SqlDbType.VarChar).Value = albumTitleId.Value;
            command.Parameters.Add("@Description", System.Data.SqlDbType.VarChar).Value = albumDescriptionId.Value;
            command.Parameters.Add("@UserId", System.Data.SqlDbType.UniqueIdentifier).Value = userId;
            command.ExecuteNonQuery();
            con.Close();
            Response.Redirect("You.aspx");
        }
    }
}