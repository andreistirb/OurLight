<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Photo.aspx.cs" Inherits="Proiect___OurLight.Photo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="img_container">
        <img id="user_img" alt="Photo" runat="server" width="500" />
    </div>

    

    <div class="title_container">
        <asp:SqlDataSource ID="SqlDataSourcePhotoDetails" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            SelectCommand="SELECT aspnet_Users.UserName, aspnet_Users.UserId, Photos.Id, Photos.Title, Photos.UploadDate, Photos.Description, Photos.UserId AS Expr1 FROM aspnet_Users INNER JOIN Photos ON aspnet_Users.UserId = Photos.UserId"
            ></asp:SqlDataSource>

        <asp:DataList ID="DataListPhotoDetails" runat="server" 
            DataSourceID="SqlDataSourcePhotoDetails" DataKeyField="Id">
            <ItemTemplate>

            <h2><asp:Label ID="Label1" runat="server" Text='<%# Eval("Title") %>' /></h2>
            
                By: 
                <a href="You.aspx?user=<%# Eval("UserName") %>"><asp:Label ID="UserNameLabel" runat="server" Text='<%# Eval("UserName") %>' /></a>
                <br />
                <h3>
                <asp:Label ID="DescriptionLabel" runat="server" 
                    Text='<%# Eval("Description") %>' />
                </h3>
                <br />
                
            </ItemTemplate>
        </asp:DataList>
        <asp:Button ID="deleteButton" runat="server" Text="Delete" Visible="false" OnClick="deletePhoto" />
    </div>

    <div class="comments_container">

        <h2>Comments: </h2>
        <br />
        <asp:SqlDataSource ID="SqlDataSourcePhotoComments" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"  
        DeleteCommand="DELETE FROM Comments where Id = @CommentId">
         
        </asp:SqlDataSource>

        <asp:DataList ID="CommentsDataList" runat="server" DataSourceID="SqlDataSourcePhotoComments" DataKeyField="CommExpr" OnDeleteCommand="CommentsDataListDeleteCommand">
            <ItemTemplate>
                <a href="You.aspx?user=<%# Eval("UserName") %>"><asp:Label ID="UserNameLabel" runat="server" Text='<%# Eval("UserName") %>' /></a> wrote: 
                <br />
                <asp:Label ID="CommentLabel" runat="server" Text='<%# Eval("Text") %>'/>
                <asp:Button ID="CommentDeleteButton" runat="server" Text="Delete Comment" Visible="false"  CommandName="delete" />
            </ItemTemplate>
        
        </asp:DataList>
    
        <br />
        <asp:TextBox ID="commentsTextBox" class="commentsTextBox" runat="server"></asp:TextBox>
        <asp:Button ID="commentsButton" Text="Add comment" runat="server" OnClick="addComment" />
    
    </div>

</asp:Content>
