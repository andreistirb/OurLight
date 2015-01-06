<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Gallery.aspx.cs" Inherits="Proiect___OurLight.Gallery" %>
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

    <asp:SqlDataSource ID="SqlDataSourceGallery" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT Id, Title, Description FROM Galleries"></asp:SqlDataSource>
    <asp:DataList ID="DataListContent" runat="server" DataSourceID="SqlDataSourceGallery">
    <ItemTemplate>
        <h2><%#DataBinder.Eval(Container.DataItem,"Title") %></h2>
        <h3><%# DataBinder.Eval(Container.DataItem,"Description") %></h3>
   
    </ItemTemplate>
    </asp:DataList>
    <hr />

    <asp:SqlDataSource ID="SqlDataSourceGalleryPhotos" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Photos.Id AS Expr1, Photos.Title AS Expr2 FROM Photos INNER JOIN GalleriesPhotos ON Photos.Id = GalleriesPhotos.PhotoId"
        ></asp:SqlDataSource>
    <asp:DataList ID="DataListAlbums" runat="server" DataSourceID="SqlDataSourceGalleryPhotos" RepeatDirection="Horizontal" RepeatColumns="5" >
    <ItemTemplate>
      <a href='<%# "Photo.aspx?id=" + DataBinder.Eval(Container.DataItem,"Expr1") %>'>
        <div class="img_thumb_container">
            <img src='<%#"Pictures/" + DataBinder.Eval(Container.DataItem,"Expr1") + ".jpg" %>' alt="Image" width='200px'/>
            <label class="img_thumb_photo_title"><%#DataBinder.Eval(Container.DataItem,"Expr2") %></label>
        </div>
      </a>
    </ItemTemplate> 
    </asp:DataList>

</asp:Content>
