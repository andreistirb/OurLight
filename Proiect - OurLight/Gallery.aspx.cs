using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proiect___OurLight
{
    public partial class Gallery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String q = Request.Params["id"];
            SqlDataSourceGallery.SelectCommand = "SELECT Id, Title, Description FROM Galleries WHERE Id = " + q;
            SqlDataSourceGalleryPhotos.SelectCommand = "SELECT Photos.Id AS Expr1, Photos.Title AS Expr2 FROM Photos INNER JOIN GalleriesPhotos ON Photos.Id = GalleriesPhotos.PhotoId where GalleriesPhotos.GalleryId = " + q;
        }
    }
}