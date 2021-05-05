<%@ Page Title="Search Equipment" Language="VB" MasterPageFile="CustomerMaster.master" AutoEventWireup="false" CodeFile="SearchEquipment.aspx.vb" Inherits="customer_SearchEquipment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="searchtermlbl0" runat="server" Font-Bold="True">Search Term: </asp:Label>
                            <asp:Label ID="searchtermlbl" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">

                            <asp:GridView ID="GridView3" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlEquipment" GridLines="None" Width="60%">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
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
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="viewbtn" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="ViewProfile" Text="Profile" />
                                            &nbsp;
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
    <asp:HiddenField ID="luberfinerpn" runat="server" />
</asp:Content>

