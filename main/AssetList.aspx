<%@ Page Title="Asset List" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="AssetList.aspx.vb" Inherits="main_AssetList" %>

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
                            <asp:DropDownList ID="locationdd" runat="server" AppendDataBoundItems="True" DataSourceID="SqlLocations" DataTextField="shipto" DataValueField="shipID" AutoPostBack="True">
                                <asp:ListItem Value="0">All Locations</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="pbookbtn" runat="server" Text="Profile Book" CssClass="pushbutton1 gold" />
                            <asp:Button ID="sbookbtn" runat="server" Text="Service Book" CssClass="pushbutton1 gold" />
                        </td>
                        <td style="text-align: right">
                            <asp:CheckBox ID="hidecb" runat="server" AutoPostBack="True" Checked="True" Text="Hide Unknown" />&nbsp;
                            <asp:DropDownList ID="typedd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlEquipmentType" DataTextField="equipment_description" DataValueField="equipment_description">
                                <asp:ListItem Value="0">Select Equipment Type</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="addequipmentbtn" runat="server" Text="New Equipment" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Stock Efficiency: "></asp:Label>
                            <asp:Label ID="efficiencylbl" runat="server" Text="Label"></asp:Label>
                        </td>
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
                                    <asp:TemplateField HeaderText="Last">
                                        <ItemTemplate>
                                            <asp:Label ID="lasthourstb" runat="server" Text='<%# appcode.GetLastKitHoursMiles(Eval("equipmentID")) %>' Width="50"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current">
                                        <ItemTemplate>
                                            <asp:Textbox ID="updatehourstb" runat="server" Width="50" Text='<%# appcode.GetHoursMiles(Eval("equipmentID"))%>'></asp:Textbox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
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
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Button ID="updatehoursbtn" runat="server" Text="Update Hours/Miles" />
                <asp:CheckBox ID="createkitscb" runat="server" Text="Create Kits" />
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipmentID,interval_root,assetID,equipment_oem + ' ' + equipment_model + ' ' + equipment_description as equipment,equipment_year,equipment_vin,engine_oem + ' ' + engine_model as engine,locationID,hours_miles FROM [t_equipment] WHERE ([companyID] = @companyID) ORDER BY assetID">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlEquipmentJobOnly" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipmentID,interval_root,assetID,equipment_oem + ' ' + equipment_model + ' ' + equipment_description as equipment,equipment_year,equipment_vin,engine_oem + ' ' + engine_model as engine,locationID,hours_miles FROM [t_equipment] WHERE ([companyID] = @companyID) AND locationID<>0 ORDER BY assetID">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlEquipmentType" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipment_description FROM [t_equipment] WHERE ([companyID] = @companyID) GROUP BY equipment_description ORDER BY equipment_description">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlEquipmentByType" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipmentID,interval_root,assetID,equipment_oem + ' ' + equipment_model + ' ' + equipment_description as equipment,equipment_year,equipment_vin,engine_oem + ' ' + engine_model as engine,locationID,hours_miles FROM [t_equipment] WHERE ([companyID] = @companyID) and equipment_description=@equipment_description ORDER BY assetID">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
            <asp:QueryStringParameter Name="equipment_description" QueryStringField="equipment_type" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlEquipmentByLocation" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipmentID,interval_root,assetID,equipment_oem + ' ' + equipment_model + ' ' + equipment_description as equipment,equipment_year,equipment_vin,engine_oem + ' ' + engine_model as engine,locationID,hours_miles FROM [t_equipment] WHERE ([companyID] = @companyID) and locationID=@locationID ORDER BY assetID">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
            <asp:SessionParameter Name="locationID" SessionField="this_locationID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlLocations" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [shipID], [shipto] FROM [t_ship] WHERE ([companyID] = @companyID) ORDER BY [shipto]">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

