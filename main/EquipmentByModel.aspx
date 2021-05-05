<%@ Page Title="" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="EquipmentByModel.aspx.vb" Inherits="main_EquipmentByModel" %>

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
                        <td style="text-align: right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlEquipment" PageSize="25" GridLines="None">
                                <Columns>
                                    <asp:TemplateField HeaderText="equipmentID" SortExpression="equipmentID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="equipmentIDlbl" runat="server" Text='<%# Bind("equipmentID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="companyID" SortExpression="companyID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="companyIDlbl" runat="server" Text='<%# Bind("companyID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="equipment_description" HeaderText="Description" SortExpression="equipment_description" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="equipment_year" HeaderText="Year" SortExpression="equipment_year" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="equipment_vin" HeaderText="VIN/Serial" SortExpression="equipment_vin" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="company" HeaderText="Company" SortExpression="company" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="profilebtn" runat="server" Text="View" CommandName="Profile" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
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
    </table>
    <asp:SqlDataSource ID="SqlEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [v_equipment] WHERE equipment_model=@equipment_model ORDER BY equipment_oem, equipment_model">
        <SelectParameters>
            <asp:QueryStringParameter Name="equipment_model" QueryStringField="model" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

