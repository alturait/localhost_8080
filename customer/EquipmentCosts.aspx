<%@ Page Title="Equipment Costs" Language="VB" MasterPageFile="CustomerMaster.master" AutoEventWireup="false" CodeFile="EquipmentCosts.aspx.vb" Inherits="customer_EquipmentCosts" %>

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
                            <asp:DropDownList ID="yeardd" runat="server" AutoPostBack="True">
                                <asp:ListItem>2014</asp:ListItem>
                                <asp:ListItem>2015</asp:ListItem>
                                <asp:ListItem>2016</asp:ListItem>
                                <asp:ListItem>2017</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 20%;">
                            <a href="OpenKits.aspx">Kits On Order</a><br />
                            <a href="Assets.aspx">Asset List</a><br />
                            <a href="LubeTracker.aspx">Fleet Info</a><br />
                            <a href="EquipmentOrderHistory.aspx">Kit History</a><br />
                            <a href="EquipmentCosts.aspx">Annual Costs</a><br />
                            <a href="PartSummary.aspx">Parts Summary</a>
                        </td>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  DataSourceID="SqlKitOrders" AllowSorting="True" GridLines="None" Width="100%">
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
                                    <asp:TemplateField HeaderText="equipmentID" SortExpression="equipmentID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="equipmentIDlbl" runat="server" Text='<%# Bind("equipmentID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="cost" HeaderText="Cost" SortExpression="cost" DataFormatString="{0:c}">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
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
    <asp:SqlDataSource ID="SqlKitOrders" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [equipmentID], [equipment_oem], [equipment_model], [equipment_description], [assetID], sum([quantity] * [price]) as cost FROM [v_kitorders] WHERE ([companyID] = @companyID) and YEAR(order_date)=@year GROUP BY [equipmentID], [equipment_oem], [equipment_model], [equipment_description], [assetID] ORDER BY [cost] DESC">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
            <asp:ControlParameter ControlID="yeardd" Name="year" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

