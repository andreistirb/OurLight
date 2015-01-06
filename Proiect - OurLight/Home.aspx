<%@ Page Title="Welcome to OurLight - photo sharing" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="Proiect___OurLight._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

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
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        New Photos
    </h2>
    <br />
    <asp:SqlDataSource ID="SqlDataSourceHome" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT top 5 [Title], [Id], [Category] FROM [Photos] ORDER BY [Id] DESC"></asp:SqlDataSource>

    <asp:DataList ID="HomeDataList" DataSourceID="SqlDataSourceHome" runat="server" RepeatDirection="Horizontal">
        <ItemTemplate>
          <a href='<%# "Photo.aspx?id=" + DataBinder.Eval(Container.DataItem,"Id") %>'>
            <div class="img_thumb_container">
                <img src='<%#"Pictures/" + DataBinder.Eval(Container.DataItem,"Id") + ".jpg" %>' alt="Image" width="200px"/>
                <label class="img_thumb_photo_title"><%#DataBinder.Eval(Container.DataItem,"Title") %></label>
            </div>
          </a>
        </ItemTemplate>
    </asp:DataList>
    
    <hr />
    <h2>Latest Events</h2>
    <br />
</asp:Content>
