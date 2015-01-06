using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Diagnostics;

namespace Proiect___OurLight
{
    public partial class Photo : System.Web.UI.Page
    {
        String q;
        SqlConnection con;
        SqlCommand command;

        protected void Page_Load(object sender, EventArgs e)
        {
            q = Request.Params["id"];
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            command = new SqlCommand();
            con.Open();
            command.Connection = con;
            String path;
            String user="";
            SqlDataSourcePhotoDetails.SelectCommand = "SELECT aspnet_Users.UserName, aspnet_Users.UserId, Photos.Id, Photos.Title, Photos.UploadDate, Photos.Description, Photos.UserId AS Expr1 FROM aspnet_Users INNER JOIN Photos ON aspnet_Users.UserId = Photos.UserId where (Photos.Id = '" + q + "' )";
            SqlDataSourcePhotoComments.SelectCommand = "SELECT aspnet_Users.UserName, aspnet_Users.UserId, Photos.Id, Photos.Title, Photos.UploadDate, Photos.Description, Photos.UserId AS Expr1, Comments.Id AS CommExpr, Comments.Text, Comments.Date, Comments.PhotoId FROM Comments INNER JOIN Photos ON Comments.PhotoId = Photos.Id INNER JOIN aspnet_Users ON Comments.UserId = aspnet_Users.UserId WHERE (Comments.PhotoId = '" + q + "' )";
            path = "Pictures/" + q + ".jpg";
            user_img.Src = path;
            //verifica daca userul logat este acelasi cu cel al pozei -> editButton visibility =  false
            command.CommandText = "SELECT aspnet_Users.UserName FROM aspnet_Users INNER JOIN Photos ON aspnet_Users.UserId = Photos.UserId WHERE (Photos.Id = @PhotoId)";
            command.Parameters.Add("@PhotoId", SqlDbType.Int).Value = q;
            SqlDataReader dr;
            dr = command.ExecuteReader();
            if (dr.Read())
            {
                user = dr.GetString(0);
            }
            dr.Close();
            if (user.Equals(User.Identity.Name))
            {
                deleteButton.Visible = true;
                for (int i = 0; i < CommentsDataList.Items.Count; i++)
                {
                    CommentsDataList.Items[i].FindControl("CommentDeleteButton").Visible = true;
                }
            }
            command.Parameters.Clear();
            command.CommandText = "SELECT aspnet_Users.UserName FROM aspnet_Roles INNER JOIN aspnet_UsersInRoles ON aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId INNER JOIN aspnet_Users ON aspnet_UsersInRoles.UserId = aspnet_Users.UserId WHERE (aspnet_Roles.RoleName = 'Admin') AND (aspnet_Users.UserName = @UserName)";
            command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = User.Identity.Name;
            dr = command.ExecuteReader();
            if (dr.Read())
            {
                deleteButton.Visible = true;
                for (int i = 0; i < CommentsDataList.Items.Count; i++)
                {
                    CommentsDataList.Items[i].FindControl("CommentDeleteButton").Visible = true;
                }
            }
            Console.Out.Write(q);
        }

        protected void deletePhoto(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandText = "DELETE FROM Photos_Tags where PhotoId = @PhotoId";
            command.Parameters.Add("@PhotoId", SqlDbType.Int).Value = q;
            command.ExecuteNonQuery();
            command.CommandText = "DELETE FROM Comments where PhotoId = @PhotoId";
            command.ExecuteNonQuery();
            command.CommandText = "DELETE FROM Photos where Photos.Id = @PhotoId";
            command.ExecuteNonQuery();
            connection.Close();
            Response.Redirect("You.aspx");
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

        protected void CommentsDataListDeleteCommand(object source, DataListCommandEventArgs e)
        {
            int id = (int) CommentsDataList.DataKeys[e.Item.ItemIndex];
            Debug.WriteLine(id);
            SqlDataSourcePhotoComments.DeleteParameters.Add("CommentId", id.ToString());
            SqlDataSourcePhotoComments.Delete();
        }
    }


}