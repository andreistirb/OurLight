using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Proiect___OurLight
{
    public partial class Photo : System.Web.UI.Page
    {
        String q;

        protected void Page_Load(object sender, EventArgs e)
        {
            q = Request.Params["id"];
            String path;
            SqlDataSourcePhotoDetails.SelectCommand = "SELECT aspnet_Users.UserName, aspnet_Users.UserId, Photos.Id, Photos.Title, Photos.UploadDate, Photos.Description, Photos.UserId AS Expr1 FROM aspnet_Users INNER JOIN Photos ON aspnet_Users.UserId = Photos.UserId where (Photos.Id = '" + q + "' )";
            SqlDataSourcePhotoComments.SelectCommand = "SELECT aspnet_Users.UserName, aspnet_Users.UserId, Photos.Id, Photos.Title, Photos.UploadDate, Photos.Description, Photos.UserId AS Expr1, Comments.Text, Comments.Date, Comments.PhotoId FROM Comments INNER JOIN Photos ON Comments.PhotoId = Photos.Id INNER JOIN aspnet_Users ON Comments.UserId = aspnet_Users.UserId WHERE (Comments.PhotoId = '" + q + "' )";
            path = "Pictures/" + q + ".jpg";
            user_img.Src = path;
            //verifica daca userul logat este acelasi cu cel al pozei -> editButton visibility =  false
            Console.Out.Write(q);
        }

        protected void addComment(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO Comments (Text, Date, PhotoId, UserId) VALUES (@mText, @mDate, @mPhotoId, @mUserId)";
            command.Parameters.Add("@mPhotoId", SqlDbType.Int).Value = q;
            
            
            SqlCommand selectCommand = new SqlCommand();
            selectCommand.CommandText = "SELECT UserId FROM aspnet_Users where UserName = @username";
            selectCommand.Parameters.Add("@username", SqlDbType.NVarChar).Value = User.Identity.Name;
            SqlDataReader dr;
            selectCommand.Connection = connection;
            connection.Open();
            try
            {

                dr = selectCommand.ExecuteReader();
                if (dr.Read())
                {
                    command.Parameters.Add("@mUserId", SqlDbType.UniqueIdentifier).Value = dr.GetGuid(0);
                }
                dr.Close();
                connection.Close();

                command.Parameters.Add("@mDate", SqlDbType.Date).Value = DateTime.Now;
                command.Parameters.Add("@mText", SqlDbType.VarChar).Value = commentsTextBox.Text;
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {

            }
            commentsTextBox.Text = "";
        }
    }
}