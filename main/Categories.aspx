<%@ Page Title="Categories" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="Categories.aspx.vb" Inherits="main_Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="pagelbl" runat="server"></asp:Label>
                            <asp:Button ID="backbtn" runat="server" Text="Back" />
                        </td>
                        <td style="text-align: right"></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="addtolbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            <asp:Label ID="emessagelbl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="categorytb" runat="server"></asp:TextBox>
                            <asp:Button ID="savebtn" runat="server" Text="Add To" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataList ID="DataList1" runat="server" DataKeyField="categoryID" DataSourceID="SqlCategory">
                                <ItemTemplate>
                                    <a href='Categories.aspx?categoryID=<%# Eval("categoryID")%>'><asp:Label ID="categoryLabel" runat="server" Text='<%# Eval("category") %>' /></a> - 
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("categoryID")%>' /> 
                                    <a href='Categories.aspx?categoryID=<%# Eval("categoryID")%>&mode=delete'><asp:Label ID="Label2" runat="server" Text="Delete" /></a>
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:SqlDataSource ID="SqlCategory" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [categoryID], [category] FROM [t_category] WHERE ([parentID] = @parentID) ORDER BY [category]">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="parentID" QueryStringField="categoryID" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:HiddenField ID="parentIDlbl" runat="server" />
                        </td>
                        <td style="vertical-align: top; text-align: center">
                            <asp:Label ID="Label6" runat="server" Text="Picture" Font-Bold="True" Font-Size="Medium"></asp:Label><br /><br />
                            <asp:Image ID="Image1" runat="server" CssClass="est_picture" /><br />
                            <asp:Label ID="picturelbl" runat="server" Text="Label"></asp:Label>
                            <asp:Panel ID="Panel2" runat="server">
                                <br />
                                <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;
                                <asp:Button ID="uploadButton" runat="server" Text="Upload" CssClass="button"/>
                                <br />
                                <asp:Label ID="msglbl" runat="server"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

