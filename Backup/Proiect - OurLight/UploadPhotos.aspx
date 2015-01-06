<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadPhotos.aspx.cs" Inherits="Proiect___OurLight.UploadPhotos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<fieldset class="uploadPhotos">

<asp:SqlDataSource ID="SqlDataSourceUserGalleries" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT Galleries.Id, Galleries.Title FROM aspnet_Users INNER JOIN Galleries ON aspnet_Users.UserId = Galleries.UserId"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSourceCategories" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT Name FROM Categories"></asp:SqlDataSource>

<legend>Upload a new photo</legend>
    <input id="uploadPhotoTitle" runat="server" placeholder="Title"  />
    <textarea id="uploadPhotoDescription" runat="server" placeholder="Description" rows="4" cols="50" />
    <textarea id="uploadTags" runat="server" placeholder="Tags" rows="2" cols="50"></textarea>
    <asp:DropDownList ID="uploadPhotoCategoryList" runat="server">
       <asp:ListItem Text="Category" Value=""></asp:ListItem> 
    </asp:DropDownList>
    <asp:DropDownList ID="uploadLoggedUserAlbumsList" runat="server">
        <asp:ListItem Text="Add to album" Value="0"></asp:ListItem>
    </asp:DropDownList>
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Save" style="width:85px" />
    <asp:Label ID="lblmessage" runat="server" Text="" />
    
</fieldset>

</asp:Content>
