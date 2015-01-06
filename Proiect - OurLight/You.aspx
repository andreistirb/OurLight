<%@ Page Title="You " Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="You.aspx.cs" Inherits="Proiect___OurLight.You" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">

    $(document).ready(function () {
        $(".img_thumb_container label").css("display", "block");
        $(".img_thumb_container").hover(function () {
            $(this).find('label.img_thumb_photo_title').show("blind", 500);
        },
        function () {
            $(this).find('label.img_thumb_photo_title').hide("blind", "fast");
        });
    });

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>
        Your Gallery
    </h2>
    <br />
    <h3>Albums <a href="NewGallery.aspx"><asp:Label id="CreateNewAlbumLabel" runat="server" Text="(+)" ></asp:Label></a></h3>
    <hr />

    <asp:SqlDataSource ID="SqlDataSourceYourAlbums" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Galleries.Id, Galleries.Title, Galleries.UserId, Galleries.Cover FROM Galleries INNER JOIN aspnet_Users ON Galleries.UserId = aspnet_Users.UserId"
        ></asp:SqlDataSource>
    <asp:DataList ID="DataListAlbums" runat="server" DataSourceID="SqlDataSourceYourAlbums" RepeatDirection="Horizontal" RepeatColumns="5" >
    <ItemTemplate>
      <a href='<%# "Gallery.aspx?id=" + DataBinder.Eval(Container.DataItem,"Id") %>'>
        <div class="img_thumb_container">
            <img src='<%#"Pictures/" + DataBinder.Eval(Container.DataItem,"Cover") + ".jpg" %>' alt="Image" width='200px'/>
            <label class="img_thumb_photo_title"><%#DataBinder.Eval(Container.DataItem,"Title") %></label>
        </div>
      </a>
    </ItemTemplate> 
    </asp:DataList>

    <br />
    <h3>Photos <a href="UploadPhotos.aspx"><asp:Label id="UploadPhotoLabel" runat="server" Text="(+)" ></asp:Label></a></h3>
    <hr />
   <asp:SqlDataSource ID="SqlDataSourceYourGallery" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        
        SelectCommand="SELECT Photos.Id, Photos.Title, Photos.Description, Photos.UserId, aspnet_Users.UserId AS Expr1, aspnet_Users.UserName FROM Photos INNER JOIN aspnet_Users ON Photos.UserId = aspnet_Users.UserId"></asp:SqlDataSource>
    <asp:DataList ID="YouGalleryDataList" runat="server" DataSourceID="SqlDataSourceYourGallery" RepeatDirection="Horizontal" RepeatColumns="5" >
    <ItemTemplate>
      <a href='<%# "Photo.aspx?id=" + DataBinder.Eval(Container.DataItem,"Id") %>'>
        <div class="img_thumb_container">
            <img src='<%#"Pictures/" + DataBinder.Eval(Container.DataItem,"Id") + ".jpg" %>' alt="Image" width='200px'/>
            <label class="img_thumb_photo_title"><%#DataBinder.Eval(Container.DataItem,"Title") %></label>
        </div>
      </a>
    </ItemTemplate>   
   </asp:DataList>


</asp:Content>
