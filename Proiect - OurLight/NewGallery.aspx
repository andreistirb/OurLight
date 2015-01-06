<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewGallery.aspx.cs" Inherits="Proiect___OurLight.NewGallery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<fieldset>

<legend>Create Album</legend>
<input id="albumTitleId" runat="server" placeholder="Title" />
<textarea ID="albumDescriptionId" runat="server" placeholder="Description" rows="4" cols="50"></textarea>
<asp:Button id="albumSaveButton" runat="server" Text="Create" OnClick="btn_Create" />
</fieldset>
</asp:Content>
