<%@ Page Title="Parts" Language="VB" MasterPageFile="~/main/Anonymous.master" AutoEventWireup="false" CodeFile="MyEquipmentParts.aspx.vb" Inherits="main_MyEquipmentParts" %>

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
                            <asp:Button ID="backbtn" runat="server" Text="BACK" CssClass="pushbutton1 gold" />                            
                        </td>
                        <td><asp:Label ID="equipmenttb" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
                        <td style="text-align: right;">
                            <asp:DropDownList ID="parttypedd" runat="server">
                                <asp:ListItem>Filter</asp:ListItem>
                                <asp:ListItem>Fluid</asp:ListItem>
                                <asp:ListItem>Belt-Hose</asp:ListItem>
                                <asp:ListItem>Battery</asp:ListItem>
                                <asp:ListItem>Other</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="addpartbtn" runat="server" Text="Add Part" CssClass="pushbutton1 gold" />                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding-top: 10px">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="partID" DataSourceID="SqlParts" Width="100%" GridLines="None" ShowHeader="False" BorderWidth="1">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 10%; text-align: left">
                                                        <asp:Label ID="Label21" runat="server" Text="Mfr"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; text-align: left">
                                                        <asp:Label ID="Label22" runat="server" Text="Part Number"></asp:Label>
                                                    </td>
                                                    <td style="width: 45%; text-align: left">
                                                        <asp:Label ID="Label23" runat="server" Text="Description"></asp:Label>
                                                    </td>
                                                    <td style="width: 5%; text-align: center">

                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="Label24" runat="server" Text="Qty"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="Label8" runat="server" Text="UOM"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 10%; text-align: left">
                                                        <asp:Label ID="Label14" runat="server" Text="Primary" CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; text-align: left">
                                                        <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Eval("manufacturer")%>'></asp:Label>
                                                        <asp:Label ID="partnumberlbl" runat="server" Text='<%# Eval("partnumber")%>' CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td style="width: 45%; text-align: left">
                                                        <asp:Label ID="descriptionlbl"  runat="server" Text='<%# Eval("description")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 5%; text-align: center">
                                                        <asp:CheckBox ID="selectcb" runat="server" />
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="quantitylbl" runat="server" Text='<%# Eval("quantity")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="pricelbl" runat="server" Text='<%# FormatCurrency(Eval("Price"),2)%>'></asp:Label>&nbsp;
                                                        <asp:Label ID="uomlbl" runat="server" Text='<%# Eval("uom")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label12" runat="server" Text="Alternate "></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("alt_manufacturer")%>'></asp:Label>
                                                        <asp:Label ID="Label13" runat="server" Text='<%# Eval("alt_partnumber")%>'></asp:Label>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="parttypelbl" runat="server" Text='<%# Eval("part_type")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text="OEM "></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label15" runat="server" Text='<%# Eval("oem_manufacturer")%>'></asp:Label>&nbsp;
                                                        <asp:Label ID="oemlbl" runat="server" Text='<%# Eval("oem_partnumber")%>'></asp:Label>
                                                    </td>
                                                    <td></td>
                                                    <td colspan="2">
                                                        <asp:Label ID="partIDlbl" runat="server" Text='<%# Eval("partID")%>' Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label9" runat="server" Text="Notes" CssClass="heading1"></asp:Label>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:Label ID="noteslbl" runat="server" Text='<%# Eval("notes")%>'></asp:Label>
                                                    </td>
                                                    <td colspan="2" style="text-align: right">
                                                        <asp:LinkButton ID="editbtn" runat="server" Text="Edit" CommandName="Edit" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                                        <asp:LinkButton ID="deletebtn" runat="server" Text="Remove" CommandName="Remove" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlParts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM t_parts WHERE equipmentID = @equipmentID ORDER BY partnumber">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="equipmentID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlAssets" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [equipmentID], [equipment_oem] + ' ' + [equipment_model] + ' ' + [equipment_description] + ' #' + [assetID] as eassetID FROM [t_equipment] WHERE ([companyID] = @companyID) ORDER BY equipment_oem,equipment_model,assetID">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlAssetsByUser" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [equipmentID], [equipment_oem] + ' ' + [equipment_model] + ' ' + [equipment_description] + ' #' + [assetID] as eassetID FROM [v_user_equipment] WHERE ([userID] = @userID) ORDER BY equipment_oem,equipment_model,assetID">
        <SelectParameters>
            <asp:SessionParameter Name="userID" SessionField="userID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    </asp:Content>

