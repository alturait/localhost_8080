<%@ Page Title="All Discounts" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="AllDiscounts.aspx.vb" Inherits="main_AllDiscounts" %>

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
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="SqlDiscountMfrs" DataTextField="manufacturer" DataValueField="manufacturer">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDiscounts" GridLines="None" Width="100%">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="multiplierIDlbl" runat="server" Text='<%# Bind("multiplierID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Manufacturer" SortExpression="manufacturer">
                                        <ItemTemplate>
                                            <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company" SortExpression="company">
                                        <ItemTemplate>
                                            <a href='AllDiscounts.aspx?companyID=<%# Eval("companyID") %>'><asp:Label ID="companylbl" runat="server" Text='<%# Bind("company") %>'></asp:Label></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PC" SortExpression="price_category">
                                        <ItemTemplate>
                                            <asp:Label ID="categorylbl" runat="server" Text='<%# Bind("price_category") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount">
                                        <ItemTemplate>
                                            <asp:Label ID="discounttb" runat="server" Text='<%#FormatPercent(appcode.GetDiscount(Eval("companyID"), Eval("manufacturer"), Eval("price_category")), 0)%>' Width="40"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="removebtn" runat="server" Text="Remove" CommandName="Remove" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" style="padding: 10px;" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:SqlDataSource ID="SqlDiscounts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="select multiplierID,companyID,company,manufacturer,price_category from v_discounts where manufacturer=@manufacturer order by manufacturer,company,price_category">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownList1" Name="manufacturer" PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDiscountMfrs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer] FROM [t_discount] GROUP BY [manufacturer] ORDER BY [manufacturer]"></asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

