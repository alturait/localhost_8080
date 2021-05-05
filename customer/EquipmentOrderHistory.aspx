<%@ Page Title="Kit History" Language="VB" MasterPageFile="~/customer/CustomerMaster.master" AutoEventWireup="false" CodeFile="EquipmentOrderHistory.aspx.vb" Inherits="customer_EquipmentOrderHistory" %>

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
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  DataSourceID="SqlKitOrders" AllowSorting="True" GridLines="None" Width="100%" AllowPaging="True" PageSize="25">
                                <Columns>
                                    <asp:TemplateField HeaderText="Asset ID" SortExpression="assetID">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="assetbtn" runat="server" Text='<%# Bind("assetID") %>' CommandName="Asset" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" style="padding: 10px;" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Equipment" SortExpression="equipment_oem">
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("equipment_oem")%>'></asp:Label> <asp:Label ID="Label8" runat="server" Text='<%# Eval("equipment_model")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Interval" SortExpression="name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3478" runat="server" Text='<%# Eval("interval")%>'></asp:Label> <asp:Label ID="Label6906" runat="server" Text='<%# Eval("interval_type")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hours/Miles" SortExpression="name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("hours_miles")%>'></asp:Label> <asp:Label ID="Label6" runat="server" Text='<%# Eval("interval_type")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order ID" SortExpression="orderID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="orderIDlbl" runat="server" Text='<%# Bind("orderID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="order_date" HeaderText="Date" SortExpression="order_date" DataFormatString="{0:d}">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="PO" SortExpression="purchaseorder">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="orderbtn" runat="server" Text='<%# Bind("purchaseorder")%>' CommandName="Order" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" style="padding: 10px;" />                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="equipmentID" SortExpression="equipmentID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="equipmentIDlbl" runat="server" Text='<%# Bind("equipmentID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="serviceprofileID" SortExpression="serviceprofileID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("serviceprofileID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlKitOrders" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [name], [interval], [interval_type], [orderID], [order_date], [equipmentID], [serviceprofileID], [purchaseorder], [equipment_oem], [equipment_model], [equipment_description], [assetID], [hours_miles], sum([quantity] * [price]) as cost FROM [v_kitorders] WHERE ([companyID] = @companyID) GROUP BY [name], [interval], [interval_type], [orderID], [order_date], [equipmentID], [serviceprofileID], [purchaseorder], [equipment_oem], [equipment_model], [equipment_description], [assetID], [hours_miles] ORDER BY [order_date] DESC">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlKitOrdersByEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [name], [interval], [interval_type], [orderID], [order_date], [equipmentID], [serviceprofileID], [purchaseorder], [equipment_oem], [equipment_model], [equipment_description], [assetID], [hours_miles], sum([quantity] * [price]) as cost FROM [v_kitorders] WHERE ([equipmentID] = @equipmentID) GROUP BY [name], [interval], [interval_type], [orderID], [order_date], [equipmentID], [serviceprofileID], [purchaseorder], [equipment_oem], [equipment_model], [equipment_description], [assetID], [hours_miles] ORDER BY [order_date] DESC">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="equipmentID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

