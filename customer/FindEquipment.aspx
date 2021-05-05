<%@ Page Title="Find Equipment" Language="VB" MasterPageFile="~/customer/CustomerMaster.master" AutoEventWireup="false" CodeFile="FindEquipment.aspx.vb" Inherits="customer_FindEquipment" %>

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
                        <td>
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="esearchbtn">
                                <p style="text-align: center">
                                    <asp:TextBox ID="esearchterm" runat="server"></asp:TextBox>
                                    <asp:Button ID="esearchbtn" runat="server" Text="Find Equipment" CssClass="pushbutton1 gold" />
                                    <asp:Button ID="newbtn" runat="server" Text="New Profile" CssClass="pushbutton1 gold" />
                                </p>
                            </asp:Panel>                            
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Search Term: " Font-Bold="True" Font-Size="Medium"></asp:Label>
                            <asp:Label ID="searchtermlbl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlEquipment" Width="100%" AllowSorting="True" GridLines="None" HorizontalAlign="Center" Style="padding: 10px">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" SortExpression="equipmentID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="equipmentIDlbl" runat="server" Text='<%# Bind("equipmentID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Equipment" SortExpression="equipment">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("equipment")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="engine" HeaderText="Engine" SortExpression="engine" />
                                    <asp:TemplateField HeaderText="VIN/Serial" SortExpression="equipment_vin">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2342" runat="server" Text='<%# Bind("equipment_vin")%>'></asp:Label>
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
                            <asp:SqlDataSource ID="SqlEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipmentID,equipment_vin,equipment_oem + ' ' + equipment_model + ' ' + equipment_description as equipment,engine_oem + ' ' + engine_model as engine FROM [v_parts] WHERE (partnumber like @searchterm or alt_partnumber like @searchterm or oem_partnumber like @searchterm or equipment_oem like '%' + @searchterm + '%' or equipment_model like '%' + @searchterm + '%' or equipment_description like '%' + @searchterm + '%' or engine_model like '%' + @searchterm + '%') GROUP BY equipmentID,equipment_vin,equipment_oem,equipment_model,equipment_description,engine_oem,engine_model ORDER BY equipment">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="searchterm" QueryStringField="searchterm" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

