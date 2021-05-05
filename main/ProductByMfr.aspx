<%@ Page Title="By Manufacturer" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="ProductByMfr.aspx.vb" Inherits="main_ProductByMfr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="categorydd" runat="server" DataSourceID="SqlCategory" DataTextField="category" DataValueField="categoryID" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">Select Category</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="addbtn" runat="server" Text="Add Selected" />
                        </td>
                        <td style="text-align: right">
                            <asp:DropDownList ID="manufacturerdd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlManufacturers" DataTextField="manufacturer" DataValueField="manufacturer">
                                <asp:ListItem Value="0">Select Manufacturer</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlProductsByMfr" GridLines="None" Width="100%" PageSize="25" AllowSorting="True" EmptyDataText="No Results Found" AllowPaging="True">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="productID" InsertVisible="False" SortExpression="productID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="productIDlbl" runat="server" Text='<%# Bind("productID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="vertical-align: top; text-align: left; width: 5%"></td>
                                                    <td style="vertical-align: top; text-align: left; width: 25%"><asp:Label ID="Label3" runat="server" Text="Part Number" CssClass="est_heading1"></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: left; width: 20%"><asp:Label ID="Label2" runat="server" Text="Category" CssClass="est_heading1"></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: center; width: 10%"><asp:Label ID="Label4" runat="server" Text="Package" CssClass="est_heading1"></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: right; width: 10%"><asp:Label ID="Label1" runat="server" Text="Cost" CssClass="est_heading1"></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: right; width: 10%"><asp:Label ID="Label5" runat="server" Text="MSRP" CssClass="est_heading1"></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: right; width: 10%"><asp:Label ID="Label6" runat="server" Text="Price" CssClass="est_heading1"></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: right; width: 10%"><asp:Label ID="Label7" runat="server" Text="UoM" CssClass="est_heading1"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="vertical-align: top; text-align: left; width: 5%"><asp:CheckBox ID="selectcb" runat="server" /></td>
                                                    <td style="vertical-align: top; text-align: left; width: 25%"><asp:Label ID="partnumberlbl" runat="server" Text='<%# Eval("partnumber")%>'></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: left; width: 20%"><asp:Label ID="categorylbl" runat="server" Text='<%# appcode.GetCategoryAndParentID(Eval("category"))%>'></asp:Label><asp:Label ID="categoryIDlbl" runat="server" Text='<%# Eval("category")%>' Visible="False"></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: center; width: 10%"><asp:Label ID="packagelbl" runat="server" Text='<%# Eval("package")%>'></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: right; width: 10%"><asp:Label ID="costlbl" runat="server" Text='<%# FormatCurrency(appcode.GetCost(Eval("manufacturer"), Eval("partnumber")), 2)%>'></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: right; width: 10%"><asp:Label ID="msrplbl" runat="server" Text='<%# FormatCurrency(Eval("msrp"),2)%>'></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: right; width: 10%"><asp:Label ID="pricelbl" runat="server" Text='<%# FormatCurrency(appcode.GetCompanyPrice(Session("selected_companyID"), Eval("manufacturer"), Eval("partnumber")), 2)%>'></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: right; width: 10%"><asp:Label ID="uomlbl" runat="server" Text='<%# Eval("uom")%>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td style="vertical-align: top; text-align: left;" colspan="6"><asp:Label ID="Label13" runat="server" Text='<%# Eval("item")%>'></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: right;">
                                                        <asp:LinkButton ID="viewbtn" runat="server" Text="Detail" CommandName="Detail" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlProductsByMfr" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_product] WHERE manufacturer=@manufacturer ORDER BY [partnumber]">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="manufacturerdd" Name="manufacturer" PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlManufacturers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer] FROM [t_product] GROUP BY manufacturer ORDER BY [manufacturer]">
                            </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCategory" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [categoryID], STR([parentID]) + '-' + [category] as category FROM [t_category] ORDER BY [parentID], [category]"></asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

