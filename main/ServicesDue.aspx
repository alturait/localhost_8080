<%@ Page Title="Services Due" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="ServicesDue.aspx.vb" Inherits="main_ServicesDue" %>

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
                        <td style="text-align: right"></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlEquipment" Width="100%" AllowSorting="True" GridLines="None" EmptyDataText="No Equipment Found" Style="padding: 10px">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" SortExpression="equipmentID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="equipmentIDlbl" runat="server" Text='<%# Eval("equipmentID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID" SortExpression="interval_root" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="interval_rootlbl" runat="server" Text='<%# Eval("interval_root")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="assetID" HeaderText="Asset ID" SortExpression="assetID">
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Equipment Description" SortExpression="equipment">
                                        <ItemTemplate>
                                            <asp:Label ID="Label23242" runat="server" Text='<%# Eval("equipment")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location" SortExpression="locationID">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# appcode.GetLocation(Eval("locationID"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current">
                                        <ItemTemplate>
                                            <asp:Label ID="currenthourstb" runat="server" Text='<%# appcode.GetHoursMiles(Eval("equipmentID"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Last Kit">
                                        <ItemTemplate>
                                            <asp:Label ID="lastkithours" runat="server" Text='<%# appcode.GetLastKitHoursMiles(Eval("equipmentID")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="intervallbl" runat="server" Text='<%# appcode.GetIntervalType(Eval("equipmentID"))%>' Width="50"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="viewbtn" runat="server" Text="Profile" CommandName="ViewProfile" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" style="padding: 10px;" />
                                            <asp:LinkButton ID="resetbtn" runat="server" Text="Reset" CommandName="Reset" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" style="padding: 10px;" />
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
    <asp:SqlDataSource ID="SqlEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipmentID,interval_root,assetID,equipment_oem + ' ' + equipment_model + ' ' + equipment_description as equipment,equipment_year,equipment_vin,engine_oem + ' ' + engine_model as engine,locationID,hours_miles FROM [t_equipment] WHERE ([companyID] = @companyID) AND serviceFlagA='True' ORDER BY assetID">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

