<%@ Page Title="" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="AdvancedSearch.aspx.vb" Inherits="main_AdvancedSearch" %>

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
                        <td style="padding-top: 10px; vertical-align: top;">
                            <asp:Label ID="Label1" runat="server" Text="Search Equipment Database" Font-Size="Medium" Font-Bold="True"></asp:Label>
                            <p>Search your equipment database for a part number or type in a keyword to display a list of equipment associated with your search term. When entering a filter number, do not use any spaces or &quot;-&quot; characters in your search term. Partial part number searches are sometimes more revealing.</p>
                            <p style="text-align: center">
                                <asp:TextBox ID="searchterm" runat="server"></asp:TextBox>
                                <asp:Button ID="searchbtn" runat="server" Text="Search Equipment" CssClass="pushbutton1 gold" />
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlEquipment" Width="100%" AllowSorting="True" GridLines="None" AllowPaging="False" HorizontalAlign="Center" Style="padding: 10px" EmptyDataText="No Results Found">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <EmptyDataRowStyle Font-Bold="True" HorizontalAlign="Center" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Company" SortExpression="company">
                                        <ItemTemplate>
                                            <asp:Label ID="companylbl" runat="server" Text='<%# Bind("company") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID" SortExpression="equipmentID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="equipmentIDlbl" runat="server" Text='<%# Bind("equipmentID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="assetID" HeaderText="Asset ID" SortExpression="assetID">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Equipment" SortExpression="equipment">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("equipment")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VIN/Serial" SortExpression="equipment_vin">
                                        <ItemTemplate>
                                            <asp:Label ID="Label92" runat="server" Text='<%# Bind("equipment_vin")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location" SortExpression="locationID">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# appcode.GetLocation(Eval("locationID"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="viewbtn" runat="server" Text="Profile" CommandName="ViewProfile" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="15" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT company,equipmentID,assetID,equipment_oem + ' ' + equipment_model + ' ' + equipment_description as equipment,equipment_vin,locationID FROM [v_parts] WHERE ([companyID] = @companyID) and (assetID like '%' + @searchterm + '%' or partnumber like '%' + @searchterm + '%' or alt_partnumber like '%' + @searchterm + '%' or oem_partnumber like '%' + @searchterm + '%' or equipment_oem like '%' + @searchterm + '%' or equipment_model like '%' + @searchterm + '%' or equipment_description like '%' + @searchterm + '%' or engine_model like '%' + @searchterm + '%') GROUP BY company,equipmentID,assetID,equipment_oem,equipment_model,equipment_description,equipment_vin,locationID ORDER BY assetID">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
            <asp:QueryStringParameter Name="searchterm" QueryStringField="searchterm" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlAllEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT company,equipmentID,assetID,equipment_oem + ' ' + equipment_model + ' ' + equipment_description as equipment,equipment_vin,locationID FROM [v_parts] WHERE (assetID like '%' + @searchterm + '%' or partnumber like '%' + @searchterm + '%' or alt_partnumber like '%' + @searchterm + '%' or oem_partnumber like '%' + @searchterm + '%' or equipment_oem like '%' + @searchterm + '%' or equipment_model like '%' + @searchterm + '%' or equipment_description like '%' + @searchterm + '%' or engine_model like '%' + @searchterm + '%') GROUP BY company,equipmentID,assetID,equipment_oem,equipment_model,equipment_description,equipment_vin,locationID ORDER BY assetID">
        <SelectParameters>
            <asp:QueryStringParameter Name="searchterm" QueryStringField="searchterm" />
        </SelectParameters>
    </asp:SqlDataSource>
    </asp:Content>

