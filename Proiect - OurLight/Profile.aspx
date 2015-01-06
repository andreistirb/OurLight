<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Proiect___OurLight.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<fieldset>
    <legend>Profile</legend>
    <asp:Label ID="firstnameLabel" runat="server">First Name: </asp:Label>
    <br />
    <asp:TextBox ID="firstnameTextBox" runat="server" Visible="false"></asp:TextBox>
    <br />
    <asp:Label ID="lastnameLabel" runat="server">Last Name: </asp:Label>
    <br />
    <asp:TextBox ID="lastnameTextBox" runat="server" Visible="false"></asp:TextBox>
    <br />
    <asp:Label ID="ageLabel" runat="server">Age: </asp:Label>
    <br />
    <asp:TextBox ID="ageTextBox" runat="server" Visible="false"></asp:TextBox>
    <br />
    <asp:Label ID="countryLabel" runat="server">Country: </asp:Label>
    <br />
    <asp:TextBox ID="countryTextBox" runat="server" Visible="false"></asp:TextBox>
    <br />
    <asp:Label ID="hometownLabel" runat="server">Hometown: </asp:Label>
    <br />
    <asp:TextBox ID="hometownTextBox" runat="server" Visible="false"></asp:TextBox>
    <br />
    <asp:Label ID="equipmentLabel" runat="server">Equipment: </asp:Label>
    <br />
    <asp:TextBox ID="equipmentTextBox" runat="server" Visible="false"></asp:TextBox>
    <br />
    <asp:Button ID="updateButton" runat="server" Text="Save" OnClick="updateProfileDB" Visible="false" />
    <asp:Button ID="editButton" runat="server" Text="Edit" OnClick="editProfileDB" />
</fieldset>

</asp:Content>
