<%@ Page Title="Favorites" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="Favorites.aspx.vb" Inherits="main_Favorites" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%" class="pagetable">
        <tr>
            <td style="vertical-align: top">
                <table style="width: 1024px">
                    <tr>
                        <td>
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr>                        
                        <td style="vertical-align: top;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlProducts" Width="100%" AllowSorting="True" BorderWidth="0px">
                                <Columns>
                                    <asp:TemplateField HeaderText="productID" InsertVisible="False" SortExpression="productID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="productIDlbl" runat="server" Text='<%# Bind("productID") %>'></asp:Label>
                                            <asp:Label ID="favoriteIDlbl" runat="server" Text='<%# Bind("favoriteID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="100%">
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="vertical-align: top; text-align: left; width: 15%"><asp:Label ID="Label1" runat="server" Text="Item" CssClass="est_heading1"></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: left; width: 55%"></td>
                                                    <td style="vertical-align: top; text-align: right; width: 10%"><asp:Label ID="Label4" runat="server" Text="Sale Price" CssClass="est_heading1"></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: center; width: 10%"><asp:Label ID="Label2" runat="server" Text="Qty" CssClass="est_heading1"></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: right; width: 10%"></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="vertical-align: middle; text-align: center; width: 15%" ><asp:Label ID="categorylbl" runat="server" Text='<%# Eval("category")%>' Font-Size="X-Small"></asp:Label><br /><img alt="" src='../Images/Catalog/<%# GetPicture(Eval("partnumber"))%>.jpg' width="50" height="50" /></td>
                                                    <td style="vertical-align: middle; text-align: left; width: 55%"><asp:Label ID="Label13" runat="server" Text='<%# Eval("item")%>'></asp:Label><br /><asp:LinkButton ID="detailbtn" runat="server" Text='<%# Eval("partnumber")%>' CommandName="Detail" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" /> - <asp:LinkButton ID="removebtn" runat="server" Text="Remove" CommandName="Remove" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" /></td>
                                                    <td style="vertical-align: middle; text-align: right; width: 10%"><asp:Label ID="pricelbl" runat="server" Text='<%# FormatCurrency(appcode.GetCompanyPrice(Session("selected_companyID"), Eval("manufacturer"), Eval("partnumber")), 2)%>'></asp:Label> <asp:Label ID="uomlbl" runat="server" Text='<%# Eval("uom")%>'></asp:Label></td>
                                                    <td style="vertical-align: middle; text-align: center; width: 10%"><asp:TextBox ID="qtytb" runat="server" Text="1" Width="30"></asp:TextBox></td>
                                                    <td style="vertical-align: middle; text-align: right; width: 10%">
                                                        <asp:LinkButton ID="addbtn" runat="server" Text="Add To Cart" CommandName="Add" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" /><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlProducts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [v_favorites] WHERE companyID=@companyID ORDER BY category,[partnumber]">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="companyID" SessionField="selected_companyID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlAllFavorites" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT productID,favoriteID,item,category,manufacturer,partnumber,uom FROM [v_favorites] GROUP BY productID,favoriteID,item,category,manufacturer,partnumber,uom ORDER BY category,[partnumber]">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="companyID" SessionField="selected_companyID" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

