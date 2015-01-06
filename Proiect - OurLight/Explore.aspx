<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Explore.aspx.cs" Inherits="Proiect___OurLight.Explore" %>
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

<asp:SqlDataSource ID="SqlDataSourceExploreCategory" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT [Id], [Name] FROM [Categories] ORDER BY [Name]"></asp:SqlDataSource>

<asp:Repeater ID="ExploreCategoryRepeater" DataSourceID="SqlDataSourceExploreCategory" runat="server" >
<ItemTemplate>
    <asp:HiddenField ID="categorie" runat="server"  
        Value='<%#DataBinder.Eval(Container.DataItem,"Name") %>' />
    <h2><%#DataBinder.Eval(Container.DataItem,"Name") %></h2>
    <asp:SqlDataSource ID="SqlDataSourceExploreItem" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT top 5 [Id], [Title] FROM [Photos] WHERE [Category] = @categorie ORDER BY [Id] desc" >
        
        <SelectParameters>
            <asp:ControlParameter
                Name="categorie"
                Type="String"
                ControlID="categorie"
                PropertyName="Value"
            />
        </SelectParameters>
        
        </asp:SqlDataSource>
    
    <asp:DataList ID="ExploreDataList" DataSourceID="SqlDataSourceExploreItem" runat="server" RepeatDirection="Horizontal">
        <ItemTemplate>
          <a href='<%# "Photo.aspx?id=" + DataBinder.Eval(Container.DataItem,"Id") %>'>
            <div class="img_thumb_container">
                <img src='<%#"Pictures/" + DataBinder.Eval(Container.DataItem,"Id") + ".jpg" %>' alt="Image" width="200px"/>
                <label class="img_thumb_photo_title"><%#DataBinder.Eval(Container.DataItem,"Title") %></label>
            </div>
          </a>
        </ItemTemplate>
    </asp:DataList>
</ItemTemplate>
<SeparatorTemplate><hr /></SeparatorTemplate>

</asp:Repeater>
</asp:Content>
