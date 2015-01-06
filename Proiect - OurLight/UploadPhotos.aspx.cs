using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Diagnostics;

namespace Proiect___OurLight
{
    public partial class UploadPhotos : System.Web.UI.Page
    {

        SqlConnection con;
        SqlCommand command;
        char[] delimiterChars = { ' ' };
        string[] tags;

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSourceUserGalleries.SelectCommand = "SELECT Galleries.Id, Galleries.Title FROM aspnet_Users INNER JOIN Galleries ON aspnet_Users.UserId = Galleries.UserId where aspne_Users.UserName = " + User.Identity.Name;

            uploadPhotoCategoryList.AppendDataBoundItems = true;
            uploadLoggedUserAlbumsList.AppendDataBoundItems = true;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            command = new SqlCommand();
            command.CommandText = "SELECT Name FROM Categories";
            command.Connection = con;

            try
            {
                con.Open();
                uploadPhotoCategoryList.DataSource = command.ExecuteReader();
                uploadPhotoCategoryList.DataTextField = "Name";
                uploadPhotoCategoryList.DataValueField = "Name";
                uploadPhotoCategoryList.DataBind();
                con.Close();
            }
            catch (Exception ex)
            {
            }

            command.CommandText = "SELECT Galleries.Id, Galleries.Title FROM aspnet_Users INNER JOIN Galleries ON aspnet_Users.UserId = Galleries.UserId where aspnet_Users.UserName = '" + User.Identity.Name + "'";

            try
            {
                con.Open();
                uploadLoggedUserAlbumsList.DataSource = command.ExecuteReader();
                uploadLoggedUserAlbumsList.DataTextField = "Title";
                uploadLoggedUserAlbumsList.DataValueField = "Id";
                uploadLoggedUserAlbumsList.DataBind();
                con.Close();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            int imgTitle;
            Guid userId = Guid.Empty;
            String userName = "";
            command.Connection = con;
            con.Open();
            if (FileUpload1.HasFile)
            {
                try
                {
                    command.CommandText = "SELECT max(Id) from Photos";
                    SqlDataReader dr;
                    dr = command.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr.IsDBNull(0))
                        {
                            imgTitle = 1;
                        }
                        else
                        {
                            imgTitle = dr.GetInt32(0) + 1;
                        }
                    }
                    else
                    {
                        imgTitle = 1;
                    }
                    dr.Close();
                    userName = User.Identity.Name;
                    command.CommandText = "SELECT UserId from aspnet_Users where UserName = '" + userName + "'";
                    dr = command.ExecuteReader();
                    if (dr.Read())
                    {
                        userId = dr.GetGuid(0);
                    }
                    dr.Close();
                    command.CommandText = "INSERT INTO Photos (Title, Description, UploadDate, UserId, Category) values (@Title, @Description, GetDate(), @UserId, @Category)";
                    command.Parameters.Add("@Title", SqlDbType.VarChar).Value = uploadPhotoTitle.Value;
                    command.Parameters.Add("@Description", SqlDbType.VarChar).Value = uploadPhotoDescription.Value;
                    command.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = userId;
                    command.Parameters.Add("@Category", SqlDbType.VarChar).Value = uploadPhotoCategoryList.SelectedValue;
                    command.ExecuteNonQuery();
                    //saving the file
                    FileUpload1.SaveAs(Server.MapPath("Pictures/") +
                                                imgTitle + ".jpg");
                    if (uploadLoggedUserAlbumsList.SelectedValue != "0")
                    {
                        command.Parameters.Clear();
                        command.CommandText = "INSERT INTO GalleriesPhotos (GalleryId, PhotoId) values (@GalleryId, @PhotoId)";
                        command.Parameters.Add("@GalleryId", SqlDbType.Int).Value = uploadLoggedUserAlbumsList.SelectedValue;
                        command.Parameters.Add("@PhotoId", SqlDbType.Int).Value = imgTitle;
                        command.ExecuteNonQuery();
                    }

                    //pentru tag-uri
                    string mText = uploadTags.Value;
                    SqlCommand tagCommand = new SqlCommand();
                    tagCommand.Connection = con;
                    tags = mText.Split(delimiterChars);
                    int tagId;
                    foreach (string s in tags)
                    {
                        command.CommandText = "Select Id from Tags where Text = '" + s.ToLower() +"'";
                        command.Parameters.Clear();
                        dr = command.ExecuteReader();
                        if (dr.Read())
                        {
                            tagId = dr.GetInt32(0);
                            dr.Close();
                            tagCommand.CommandText = "INSERT INTO Photos_Tags (PhotoId, TagId) VALUES (@PhotoId, @TagId)";
                            tagCommand.Parameters.Add("@PhotoId", SqlDbType.Int).Value = imgTitle;
                            tagCommand.Parameters.Add("@TagId", SqlDbType.Int).Value = tagId;
                            tagCommand.ExecuteNonQuery();
                            tagCommand.Parameters.Clear();
                        }
                        else
                        {
                            dr.Close();
                            SqlDataReader pr;
                            tagCommand.CommandText = "SELECT MAX(Id) AS Expr1 FROM Tags";
                            pr = tagCommand.ExecuteReader();
                            if (pr.Read())
                            {
                                if (pr.IsDBNull(0))
                                {
                                    tagId = 1;
                                }
                                else
                                {
                                    tagId = pr.GetInt32(0) + 1;
                                }
                            }
                            else
                            {
                                tagId = 1;
                            }
                            pr.Close();
                            tagCommand.CommandText = "INSERT INTO Tags (Text) VALUES (@Text)";
                            tagCommand.Parameters.Add("@Text", SqlDbType.VarChar).Value = s.ToLower();
                            tagCommand.ExecuteNonQuery();
                            tagCommand.Parameters.Clear();
                            tagCommand.CommandText = "INSERT INTO Photos_Tags VALUES (@PhotoId, @TagId)";
                            tagCommand.Parameters.Add("@PhotoId", SqlDbType.Int).Value = imgTitle;
                            tagCommand.Parameters.Add("@TagId", SqlDbType.Int).Value = tagId;
                            tagCommand.ExecuteNonQuery();
                            tagCommand.Parameters.Clear();
                        }
                    }
                    con.Close();
                    
                    //Showing the file information
                    lblmessage.Text = "File uploaded";
                }
                catch (Exception ex)
                {
                    lblmessage.Text = "Upload error: " + ex.ToString();
                }
            }
            else
            {
                lblmessage.Text = "No file selected";
            }

            Response.Redirect("~/UploadPhotos.aspx");
            
            //User.Identity.GetUserId();

        }
    
    }
}