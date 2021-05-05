<%@ Page Title="Asset List" Language="VB" MasterPageFile="CustomerMaster.master" AutoEventWireup="false" CodeFile="Assets.aspx.vb" Inherits="customer_Assets" %>

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
                                    <td style="width: 25%";text-align: left">
                                        <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                        <asp:Button ID="addequipmentbtn" runat="server" Text="New Asset" CssClass="pushbutton1 gold" />
                                    </td>
                                    <td style="width: 25%";text-align: right">
                                        <asp:Label ID="Label2" runat="server" Text="Asset: " Font-Bold="True"></asp:Label>
                                        <asp:DropDownList ID="assetdd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlAssets" DataTextField="assetID" DataValueField="equipmentID">
                                            <asp:ListItem Value="0">Select Asset</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 25%";text-align: center">
                                        <asp:Label ID="Label1" runat="server" Text="Location: " Font-Bold="True"></asp:Label>
                                        <asp:DropDownList ID="locationdd" runat="server" AppendDataBoundItems="True" DataSourceID="SqlLocations" DataTextField="shipto" DataValueField="shipID" AutoPostBack="True">
                                            <asp:ListItem Value="0">All Locations</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 25%";text-align: center">
                                        <asp:Label ID="Label4" runat="server" Text="Description: " Font-Bold="True"></asp:Label>
                                        <asp:DropDownList ID="typedd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlEquipmentType" DataTextField="equipment_description" DataValueField="equipment_description" Height="16px">
                                            <asp:ListItem Value="0">All Equipment</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td ;text-align: left" colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table style="width: 100%">
                                            <tr>
                                                <td ;text-align: left" style="text-align: center">
                                                    <asp:CheckBox ID="selectall" runat="server" AutoPostBack="True" Font-Bold="True" Text="Select All" />
                                                </td>
                                                <td ;text-align: left" style="text-align: center">
                                                    <asp:Label ID="Label23243" runat="server" Text="Move Selected To " Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="newlocationdd" runat="server" AppendDataBoundItems="True" DataSourceID="SqlLocations" DataTextField="shipto" DataValueField="shipID">
                                                        <asp:ListItem Value="0">New Location</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Button ID="movebtn" runat="server" Text="Apply" />
                                                </td>
                                                <td ;text-align: center" style="text-align: center">
                                                    <asp:Label ID="Label23244" runat="server" Text="Change Description of Selected  To " Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="changetb" runat="server"></asp:TextBox>
                                                    <asp:Button ID="changebtn" runat="server" Text="Apply" />
                                                </td>
                                            </tr>
                                        </table>
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
                                        <table style="width: 100%">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlEquipment" Width="100%" AllowSorting="True" GridLines="None" EmptyDataText="No Equipment Found" Style="padding: 10px" AllowPaging="True" PageSize="100">
                                                        <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                                        <RowStyle BorderWidth="1px" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ID" SortExpression="equipmentID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="equipmentIDlbl" runat="server" Text='<%# Bind("equipmentID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="assetID" HeaderText="Asset ID" SortExpression="assetID">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Equipment" SortExpression="equipment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label23242" runat="server" Text='<%# Bind("equipment")%>'></asp:Label>
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
                                                            <asp:TemplateField HeaderText="Kits">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label783" runat="server" Text='<%# appcode.GetNumKits(Eval("equipmentID"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Last">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label7832" runat="server" Text='<%# appcode.GetLastKitDate(Eval("equipmentID"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select" SortExpression="equipment">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="selectcb" runat="server" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="viewbtn" runat="server" Text="View" CommandName="ViewProfile" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" style="padding: 10px;" />
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
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlEquipmentByType" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipmentID,assetID,equipment_oem + ' ' + equipment_model + ' ' + equipment_description as equipment,equipment_year,equipment_vin,engine_oem + ' ' + engine_model as engine,locationID,hours_miles FROM [t_equipment] WHERE ([companyID] = @companyID) and equipment_description=@equipment_description ORDER BY assetID">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
            <asp:QueryStringParameter Name="equipment_description" QueryStringField="equipment_type" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlEquipmentType" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipment_description FROM [t_equipment] WHERE ([companyID] = @companyID) AND equipment_description<>'' GROUP BY equipment_description ORDER BY equipment_description">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipmentID,assetID,equipment_oem + ' ' + equipment_model + ' ' + equipment_description as equipment,equipment_year,equipment_vin,engine_oem + ' ' + engine_model as engine,locationID,hours_miles FROM [t_equipment] WHERE ([companyID] = @companyID) ORDER BY assetID">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlEquipmentByLocation" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipmentID,assetID,equipment_oem + ' ' + equipment_model + ' ' + equipment_description as equipment,equipment_year,equipment_vin,engine_oem + ' ' + engine_model as engine,locationID,hours_miles FROM [t_equipment] WHERE ([companyID] = @companyID) and locationID=@locationID ORDER BY assetID">
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
                            <asp:SqlDataSource ID="SqlAssets" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [equipmentID], [assetID] FROM [t_equipment] WHERE ([companyID] = @companyID) ORDER BY [assetID]">
                                <SelectParameters>
                                    <asp:SessionParameter Name="companyID" SessionField="companyID" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </asp:Content>

