<%@ Page Title="Inventory" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="InventoryDashboard.aspx.vb" Inherits="main_InventoryDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>

                        </td>
                        <td style="text-align: right">

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">

                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlInventorySummary" AllowSorting="True" GridLines="None" Width="50%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Manufacturer" SortExpression="manufacturer">
                                        <ItemTemplate>
                                            <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="items" HeaderText="SKUs" ReadOnly="True" SortExpression="items" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Value" SortExpression="value">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# FormatCurrency(Eval("value"),2) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Order">
                                        <ItemTemplate>
                                            <asp:Label ID="orderqtylbl" runat="server" Text='<%# appcode.GetMfrItemsToOrder(Eval("manufacturer"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="mfrbtn" runat="server" Text="Inventory" CommandName="Detail" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="wsbtn" runat="server" Text="Worksheet" CommandName="Worksheet" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlInventorySummary" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer], count(productID) as items, sum(cost*onhand) as value FROM [t_product] WHERE nStock=@nStock OR onhand>0 GROUP BY [manufacturer] ORDER BY [manufacturer]">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="True" Name="nStock" Type="Boolean" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

