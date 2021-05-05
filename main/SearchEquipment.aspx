<%@ Page Title="Equipment Search" Language="VB" MasterPageFile="~/main/Anonymous.master" AutoEventWireup="false" CodeFile="SearchEquipment.aspx.vb" Inherits="main_SearchEquipment" %>

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
                                    <asp:BoundField DataField="company" HeaderText="Company" SortExpression="company ">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
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
                            <asp:SqlDataSource ID="SqlEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT company,equipmentID,assetID,equipment_oem + ' ' + equipment_model + ' ' + equipment_description as equipment,locationID FROM [v_parts] WHERE vendorID=@vendorID and (partnumber=@searchterm or alt_partnumber=@searchterm or oem_partnumber=@searchterm) GROUP BY company,equipmentID,assetID,equipment_oem,equipment_model,equipment_description,locationID ORDER BY company,assetID">
                                <SelectParameters>
                                    <asp:SessionParameter Name="vendorID" SessionField="vendorID" />
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

