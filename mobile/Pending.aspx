<%@ Page Title="Pending Kits" Language="VB" MasterPageFile="MobileMaster.master" AutoEventWireup="false" CodeFile="Pending.aspx.vb" Inherits="mobile_Pending" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Pending Kits"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="pendingbtn" runat="server" Text="Assets" Font-Size="Large" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="kitID" DataSourceID="SqlOpenKits" GridLines="None" Width="100%" AllowSorting="True" Font-Size="Large">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField SortExpression="companyID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="companyIDlbl" runat="server" Text='<%# Bind("companyID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="equipmentID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="equipmentIDlbl" runat="server" Text='<%# Bind("equipmentID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kit ID" SortExpression="kitID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="serviceprofileIDlbl" runat="server" Text='<%# Bind("kitID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Asset ID" SortExpression="assetID">
                                        <ItemTemplate>
                                            <asp:Label ID="assetIDlbl" runat="server" Text='<%# Bind("assetID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Equipment">
                                        <ItemTemplate>
                                            <asp:Label ID="equipmentlbl" runat="server" Text='<%# appcode.GetEquipment(Eval("equipmentID"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Interval">
                                        <ItemTemplate>
                                            <asp:Label ID="intervallbl" runat="server" Text='<%# Eval("interval")%>'></asp:Label> <asp:Label ID="intervaltypelbl" runat="server" Text='<%# Eval("interval_type")%>'></asp:Label>
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
                                    <asp:BoundField DataField="order_date" HeaderText="Ordered On" SortExpression="order_date" DataFormatString="{0:d}">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Est. Del.">
                                        <ItemTemplate>
                                            <asp:Label ID="estdellbl" runat="server" Text='<%# appcode.GetEstimatedDelivery(Eval("orderID"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
<asp:SqlDataSource ID="SqlOpenKits" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT orderID,kitID,assetID,equipmentID,interval,interval_type,companyID,company,order_date FROM [v_kit_lines] WHERE companyID=@companyID and complete=@complete GROUP BY orderID,kitID,companyID,company,assetID,equipmentID,interval,interval_type,order_date ORDER BY company,[assetID]">
    <SelectParameters>
        <asp:SessionParameter Name="companyID" SessionField="mobile_companyID" />
        <asp:Parameter DefaultValue="False" Name="complete" />
    </SelectParameters>
</asp:SqlDataSource>
</asp:Content>

