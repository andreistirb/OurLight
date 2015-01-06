using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Data;

namespace Proiect___OurLight
{
    public partial class Profile : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand command;
        String username;

        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            command = new SqlCommand();
            command.Connection = con;
            con.Open();

            if (Request.Params["user"] == null)
            {
                command.CommandText = "SELECT p.UserId, p.FirstName, p.LastName, p.Age, p.Country, p.Hometown, p.Equipment FROM Profile AS p INNER JOIN aspnet_Users AS u ON p.UserId = u.UserId WHERE (u.UserName = '" + User.Identity.Name + "')";
                username = User.Identity.Name;
            }
            else
            {
                command.CommandText = "SELECT p.UserId, p.FirstName, p.LastName, p.Age, p.Country, p.Hometown, p.Equipment FROM Profile AS p INNER JOIN aspnet_Users AS u ON p.UserId = u.UserId WHERE (u.UserName = '" + Request.Params["user"] + "')";
                editButton.Visible = false;
            }

            SqlDataReader dr;
            Debug.WriteLine(command.CommandText);
            Debug.WriteLine(Request.Params.Count);
            dr = command.ExecuteReader();
            if (dr.Read())
            {
                firstnameLabel.Text = "First Name: " + dr.GetString(1);
                lastnameLabel.Text = "Last Name: " + dr.GetString(2);
                ageLabel.Text = "Age: " + dr.GetInt32(3);
                countryLabel.Text = "Country: " + dr.GetString(4);
                hometownLabel.Text = "Hometown: " + dr.GetString(5);
                equipmentLabel.Text = "Equipment: " + dr.GetString(6);
            }
            else
            {

            }
            dr.Close();
        }

        protected void updateProfileDB(object sender, EventArgs e)
        {
            Guid mUserId = Guid.Empty;
            SqlDataReader dr;
            command.CommandText = "SELECT UserId from aspnet_Users where UserName = '" + username + "'";
            dr = command.ExecuteReader();
            if (dr.Read())
            {
                mUserId = dr.GetGuid(0);
            }
            dr.Close();
            command.CommandText = "UPDATE Profile SET FirstName = @FirstName, LastName = @LastName, Age = @Age, Country = @Country, Hometown = @Hometown, Equipment = @Equipment  where UserId = @UserId";
            command.Parameters.Add("@FirstName", System.Data.SqlDbType.VarChar).Value = firstnameTextBox.Text;
            command.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = mUserId;
            command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lastnameTextBox.Text;
            command.Parameters.Add("@Age", SqlDbType.Int).Value = ageTextBox.Text;
            command.Parameters.Add("@Country", SqlDbType.VarChar).Value = countryTextBox.Text;
            command.Parameters.Add("@Hometown", SqlDbType.VarChar).Value = hometownTextBox.Text;
            command.Parameters.Add("@Equipment", SqlDbType.VarChar).Value = equipmentTextBox.Text;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }

        protected void editProfileDB(object sender, EventArgs e)
        {
            updateButton.Visible = true;
            editButton.Visible = false;
            firstnameTextBox.Visible = true;
            lastnameTextBox.Visible = true;
            ageTextBox.Visible = true;
            countryTextBox.Visible = true;
            hometownTextBox.Visible = true;
            equipmentTextBox.Visible = true;
        }
    }
}