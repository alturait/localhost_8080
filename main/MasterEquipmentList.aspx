<%@ Page Title="" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="MasterEquipmentList.aspx.vb" Inherits="main_MasterEquipmentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:DropDownList ID="oemdd" runat="server" DataSourceID="SqlOEMs" DataTextField="equipment_oem" DataValueField="equipment_oem" AutoPostBack="True" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">Select OEM</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlEquipmentByOEM" PageSize="25" GridLines="None">
                                <Columns>
                                    <asp:BoundField DataField="equipment_oem" HeaderText="OEM" SortExpression="equipment_oem">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Model" SortExpression="equipment_model">
                                        <ItemTemplate>
                                            <asp:Label ID="modellbl" runat="server" Text='<%# Bind("equipment_model") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="quantity" HeaderText="Qty" ReadOnly="True" SortExpression="quantity" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="modelbtn" runat="server" Text="View" CommandName="Model" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
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
    <asp:SqlDataSource ID="SqlEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT count(equipmentID) as quantity, equipment_oem, equipment_model FROM [t_equipment] GROUP BY equipment_oem, equipment_model ORDER BY equipment_oem, equipment_model">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlEquipmentByOEM" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT count(equipmentID) as quantity, equipment_oem, equipment_model FROM [t_equipment] WHERE equipment_oem=@equipment_oem GROUP BY equipment_oem, equipment_model ORDER BY equipment_oem, equipment_model">
        <SelectParameters>
            <asp:ControlParameter ControlID="oemdd" Name="equipment_oem" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlOEMs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [equipment_oem] FROM [t_equipment] GROUP BY equipment_oem ORDER BY [equipment_oem]"></asp:SqlDataSource>
</asp:Content>

