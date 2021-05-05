<%@ Page Title="" Language="VB" MasterPageFile="CustomerMaster.master" AutoEventWireup="false" CodeFile="NeedsPO.aspx.vb" Inherits="customer_NeedsPO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="text-align: left">
                                        <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="vertical-align: top;" colspan="2">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="orderID" DataSourceID="SqlOrdersByCompany" GridLines="None" Width="100%" ShowFooter="True" AllowSorting="True">
                                            <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                            <RowStyle BorderWidth="1px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Order ID" InsertVisible="False" SortExpression="orderID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="orderIDlbl" runat="server" Text='<%# Bind("orderID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Purchase Order" SortExpression="purchaseorder">
                                                    <ItemTemplate>
                                                        <asp:Label ID="potb" runat="server" Text='<%# Bind("purchaseorder") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="order_date" HeaderText="Ordered On" SortExpression="order_date" DataFormatString="{0:d}" >
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Sub-Total">
                                                    <ItemTemplate>
                                                        <asp:Label ID="subtotallbl" runat="server" Text='<%# FormatCurrency(appcode.GetOrderSubTotal(Eval("orderID")), 2)%>'></asp:Label>
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
                                            <RowStyle BorderWidth="1px" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlOrdersByCompany" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM t_order WHERE confirmed=@confirmed and isReturn='No' and needspo=@needspo and companyID=@companyID ORDER BY [order_date]">
    <SelectParameters>
        <asp:Parameter DefaultValue="True" Name="confirmed" />
        <asp:Parameter DefaultValue="True" Name="needspo" />
        <asp:SessionParameter DefaultValue="" Name="companyID" SessionField="companyID" />
    </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>

