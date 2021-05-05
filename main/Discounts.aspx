<%@ Page Title="Discounts" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="Discounts.aspx.vb" Inherits="main_Discounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="est_pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                        <td colspan="2" style="text-align: right">
                            <asp:Button ID="UpdateKitsbtn" runat="server" Text="Update Kits" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDiscounts" GridLines="None" Width="100%">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Manufacturer" SortExpression="manufacturer">
                                        <ItemTemplate>
                                            <a href='ProductList.aspx?manufacturer=<%# Eval("manufacturer")%>'><asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Products" SortExpression="products">
                                        <ItemTemplate>
                                            <asp:Label ID="productslbl" runat="server" Text='<%# Bind("products")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="In Catalog" SortExpression="incatalog">
                                        <ItemTemplate>
                                            <asp:Label ID="incataloglbl" runat="server" Text='<%# appcode.InCatalog(Eval("manufacturer"), Eval("price_category"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category" SortExpression="price_category">
                                        <ItemTemplate>
                                            <asp:Label ID="categorylbl" runat="server" Text='<%# Bind("price_category") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="discounttb" runat="server" Text='<%# appcode.GetDiscount(Session("selected_companyID"), Eval("manufacturer"), Eval("price_category")) * 100%>' Width="40"></asp:TextBox>%
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="setbtn" runat="server" Text="Set" CommandName="Set" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:SqlDataSource ID="SqlDiscounts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="select manufacturer,price_category,count(productID) as products from t_product group by manufacturer,price_category order by manufacturer,price_category">
                                <SelectParameters>
                                    <asp:SessionParameter Name="vendorID" SessionField="vendorID" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

