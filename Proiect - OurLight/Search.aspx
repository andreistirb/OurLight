<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Proiect___OurLight.Search" %>
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

<asp:SqlDataSource ID="SqlDataSourceSearch" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT DISTINCT Photos.Id, Photos.Title FROM Photos_Tags INNER JOIN Photos ON Photos_Tags.PhotoId = Photos.Id INNER JOIN Tags ON Photos_Tags.TagId = Tags.Id " ></asp:SqlDataSource>



<asp:DataList ID="SearchDataList" DataSourceID="SqlDataSourceSearch" runat="server" RepeatDirection="Horizontal">
        <ItemTemplate>
          <a href='<%# "Photo.aspx?id=" + DataBinder.Eval(Container.DataItem,"Id") %>'>
            <div class="img_thumb_container">
                <img src='<%#"Pictures/" + DataBinder.Eval(Container.DataItem,"Id") + ".jpg" %>' alt="Image" width="200px"/>
                <label class="img_thumb_photo_title"><%#DataBinder.Eval(Container.DataItem,"Title") %></label>
            </div>
          </a>
        </ItemTemplate>
    </asp:DataList>
</asp:Content>
