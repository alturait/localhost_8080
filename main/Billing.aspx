<%@ Page Title="Shipments" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="Billing.aspx.vb" Inherits="main_Billing" %>

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
                            <asp:Label ID="shipmentslbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlShipments" GridLines="None" Width="100%" ShowFooter="True" AllowSorting="True">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="shipmentID" SortExpression="shipmentID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="shipmentIDlbl" runat="server" Text='<%# Bind("shipmentID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice ID" SortExpression="invoiceID">
                                        <ItemTemplate>
                                            <asp:Label ID="invoiceIDlbl" runat="server" Text='<%# Bind("invoiceID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order ID" SortExpression="orderID">
                                        <ItemTemplate>
                                            <asp:Label ID="orderIDlbl" runat="server" Text='<%# Bind("orderID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="company" HeaderText="Company" SortExpression="company">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="purchaseorder" HeaderText="Purchase Order" SortExpression="purchaseorder">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="order_date" HeaderText="Order Date" SortExpression="order_date" DataFormatString="{0:d}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="amountlbl" runat="server" Text='<%# FormatCurrency(appcode.GetInvoiceTotal(Eval("shipmentID")), 2)%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="salestotallbl" runat="server" CssClass="est_heading1"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="viewbtn" runat="server" Text="View" CommandName="View" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
<asp:SqlDataSource ID="SqlShipments" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [v_shipments] WHERE shipped=@shipped and invoiced=@invoiced and qbo=@qbo ORDER BY [shipmentID]">
    <SelectParameters>
        <asp:Parameter DefaultValue="True" Name="shipped" />
        <asp:Parameter DefaultValue="True" Name="invoiced" />
        <asp:Parameter DefaultValue="False" Name="qbo" />
    </SelectParameters>
</asp:SqlDataSource>
</asp:Content>

