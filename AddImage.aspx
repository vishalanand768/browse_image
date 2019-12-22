<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddImage.aspx.cs" Inherits="browse_image.AddImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                   <td>Name :</td>
                    <td><asp:TextBox ID="txtname" runat="server"></asp:TextBox></td>
                </tr>

                <tr>
                   <td>Image Upload :</td>
                    <td><asp:FileUpload ID="fui" runat="server"></asp:FileUpload></td>
                </tr>

                 <tr>
                   <td></td>
                    <td><asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label></td>
                </tr>

                <tr>
                   <td></td>
                    <td><asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click"></asp:Button></td>
                </tr>

                <tr>
                    <td></td>
                    <td><asp:TextBox ID="txtStartDate" runat="server" TextMode="Date"></asp:TextBox>
                                      To
                      <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date"></asp:TextBox>
                      <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" /></td>
                </tr>

                <tr>
                   <td></td>
                    <td><asp:GridView ID="grd" runat="server" AutoGenerateColumns="false" OnRowCommand="grd_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <%#Eval("name") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Profile Pic">
                                <ItemTemplate>
                                    <asp:Image ID="img1" runat="server" width="60px" Height="40px" ImageUrl='<%#Eval("img","~/Pics/{0}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btndelete" runat="server" Text="Delete" CommandName="A" CommandArgument='<%#Eval("id") %>' /> |
                                    <asp:LinkButton ID="btnedit" runat="server" Text="Edit" CommandName="B" CommandArgument='<%#Eval("id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        </asp:GridView></td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>
