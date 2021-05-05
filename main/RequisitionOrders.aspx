<%@ Page Title="Orders" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="RequisitionOrders.aspx.vb" Inherits="main_RequisitionOrders" %>

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
                        <td style="text-align: right">
                            <asp:Label ID="manufacturerlbl" runat="server"></asp:Label>&nbsp;<asp:Label ID="partnumberlbl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlOrders" GridLines="None" HorizontalAlign="Center" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="company" HeaderText="Company" SortExpression="company">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="order_date" DataFormatString="{0:d}" HeaderText="Date" SortExpression="order_date">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Order ID" SortExpression="orderID">
                                        <ItemTemplate>
                                            <a href='Order.aspx?orderID=<%# Eval("orderID")%>'><asp:Label ID="Label1" runat="server" Text='<%# Eval("orderID") %>'></asp:Label></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="quantity" HeaderText="Qty" SortExpression="quantity">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="price" DataFormatString="{0:c}" HeaderText="Price" SortExpression="price">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="uom" HeaderText="UOM" SortExpression="uom">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlOrders" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [orderID], [manufacturer], [partnumber], [quantity], [price], [uom], [company], [order_date] FROM [v_order_lines] WHERE (([complete] = @complete) AND ([manufacturer] = @manufacturer) AND ([partnumber] = @partnumber)) ORDER BY [orderID]">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="False" Name="complete" Type="Boolean" />
                                    <asp:ControlParameter ControlID="manufacturerlbl" Name="manufacturer" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="partnumberlbl" Name="partnumber" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

