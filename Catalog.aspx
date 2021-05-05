<%@ Page Title="Product Catalog" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Catalog.aspx.vb" Inherits="Catalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%" class="pagetable">
        <tr>
            <td style="vertical-align: top">
                <table style="width: 1024px">
                    <tr>
                        <td>
                            <asp:LinkButton ID="backbtn" runat="server" />
                        </td>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="manufacturerdd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlManufacturers" DataTextField="manufacturer" DataValueField="manufacturer">
                                            <asp:ListItem Value="0">Filter by Manufacturer</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right">

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 20%;">
                            <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlCategory" DataKeyField="categoryID">
                                <ItemTemplate>
                                    <a href='Catalog.aspx?categoryID=<%# Eval("categoryID")%>'><asp:Label ID="categoryLabel" runat="server" Text='<%# Eval("category") %>' /></a>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                        <td style="vertical-align: top; width: 80%;">
                            <asp:Panel ID="Panel2" runat="server">
                                    <asp:DataList ID="DataList2" runat="server" DataKeyField="productID" DataSourceID="SqlFeatureProducts" Width="100%" GridLines="Horizontal">
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="itemlbl" runat="server" Text='<%# Eval("item")%>' Font-Size="Large" Font-Bold="True"></asp:Label>
                                                        <asp:Label ID="productIDlbl" runat="server" Text='<%# Eval("productID")%>' Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 70%; vertical-align: top;">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <p><asp:Label ID="descriptionlbl" runat="server" Text='<%# Left(Eval("description"), 255)%>'></asp:Label></p>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="text-align: center"><asp:LinkButton runat="server" CommandName="more" CommandArgument='<%# Eval("productID")%>' ID="morebtn" Text="More Info" CssClass="push_button1 orange" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: center" colspan="2">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 30%">
                                                                    <asp:Label ID="Label47" runat="server" CssClass="heading1" Text="Manufacturer"></asp:Label>
                                                                </td>
                                                                <td style="width: 70%">
                                                                    <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Eval("manufacturer")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" CssClass="heading1" Text="Part Number"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="partnumberlbl" runat="server" Text='<%# Eval("partnumber")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label44" runat="server" CssClass="heading1" Text="UPC Code"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="upctb" runat="server" Text='<%# Eval("upc")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label43" runat="server" CssClass="heading1" Text="Package"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="packagetb" runat="server" Text='<%# Eval("package")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label46" runat="server" CssClass="heading1" Text="Category"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="categorytb" runat="server" Text='<%# Eval("category")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label40" runat="server" CssClass="heading1" Text="MSRP"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="msrptb" runat="server" Text='<%# FormatCurrency(Eval("msrp"), 2)%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="text-align: center; vertical-align: top; width: 30%;">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <img alt="" src='Images/Catalog/<%# GetPicture(Eval("partnumber"))%>.jpg' height="200" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Label ID="Label48" runat="server" Text="Sale Price: " Font-Bold="True" Font-Size="Medium"></asp:Label>
                                                                    <asp:Label ID="salepricelbl" runat="server" Font-Bold="True" Font-Size="Medium" Text='<%# FormatCurrency(Eval("saleprice"), 2)%>'></asp:Label>&nbsp;
                                                                    <asp:Label ID="uomlbl" runat="server" Font-Bold="True" Font-Size="Medium" Text='<%# Eval("uom")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: center" colspan="2">
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                            </asp:Panel>
                            <asp:Panel ID="Panel3" runat="server">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlProducts" Width="100%" AllowPaging="True" PageSize="10" AllowSorting="True" BorderWidth="0px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="productID" InsertVisible="False" SortExpression="productID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="productIDlbl" runat="server" Text='<%# Bind("productID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="100%">
                                            <HeaderTemplate>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="vertical-align: top; text-align: left; width: 10%"></td>
                                                        <td style="vertical-align: top; text-align: left; width: 15%"><asp:Label ID="Label3" runat="server" Text="Part Number" CssClass="est_heading1"></asp:Label></td>
                                                        <td style="vertical-align: top; text-align: left; width: 45%"><asp:Label ID="Label1" runat="server" Text="Item" CssClass="est_heading1"></asp:Label></td>
                                                        <td style="vertical-align: top; text-align: right; width: 20%"><asp:Label ID="Label4" runat="server" Text="Sale Price" CssClass="est_heading1"></asp:Label></td>
                                                        <td style="vertical-align: top; text-align: right; width: 10%"></td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="vertical-align: middle; text-align: left; width: 10%" ><img alt="" src='Images/Catalog/<%# GetPicture(Eval("partnumber"))%>.jpg' width="50" height="50" /></td>
                                                        <td style="vertical-align: middle; text-align: left; width: 15%"><asp:Label ID="parnumberlbl" runat="server" Text='<%# Eval("partnumber")%>'></asp:Label></td>
                                                        <td style="vertical-align: middle; text-align: left; width: 45%"><asp:Label ID="Label13" runat="server" Text='<%# Eval("item")%>'></asp:Label></td>
                                                        <td style="vertical-align: middle; text-align: right; width: 20%"><asp:Label ID="pricelbl" runat="server" Text='<%# FormatCurrency(Eval("saleprice"), 2)%>'></asp:Label> <asp:Label ID="uomlbl" runat="server" Text='<%# Eval("uom")%>'></asp:Label></td>
                                                        <td style="vertical-align: middle; text-align: right; width: 10%"><asp:Button ID="detailbtn" runat="server" Text="More Info" CommandName="Detail" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="pushbutton1 gold" /></td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlSearch" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_product] WHERE category<>0 and (partnumber like '%' + @search + '%' or item like '%' + @search + '%') ORDER BY [manufacturer], [partnumber]">
        <SelectParameters>
            <asp:QueryStringParameter Name="search" QueryStringField="search" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlSearchByMfr" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_product] WHERE category<>0 and (partnumber like '%' + @search + '%' or item like '%' + @search + '%') and manufacturer=@manufacturer ORDER BY [manufacturer], [partnumber]">
        <SelectParameters>
            <asp:QueryStringParameter Name="search" QueryStringField="search" />
            <asp:QueryStringParameter Name="manufacturer" QueryStringField="manufacturer" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlManufacturers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer] FROM [t_product] WHERE partnumber like '%' + @search + '%' or item like '%' + @search + '%' GROUP BY manufacturer ORDER BY [manufacturer]">
        <SelectParameters>
            <asp:QueryStringParameter Name="search" QueryStringField="search" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlProducts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [v_catalog] WHERE categoryID=@categoryID or parentID=@categoryID ORDER BY [featured] desc, [partnumber]">
        <SelectParameters>
            <asp:QueryStringParameter Name="categoryID" QueryStringField="categoryID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCategory" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [categoryID], [category] FROM [t_category] WHERE ([parentID] = @parentID) ORDER BY [category]">
        <SelectParameters>
            <asp:QueryStringParameter Name="parentID" QueryStringField="categoryID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:HiddenField ID="parentIDlbl" runat="server" />
    <asp:SqlDataSource ID="SqlFeatureProducts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT TOP (2) * FROM v_catalog WHERE featured=@featured ORDER BY NEWID()">
        <SelectParameters>
            <asp:Parameter DefaultValue="True" Name="featured" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

