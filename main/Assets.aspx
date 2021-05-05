<%@ Page Title="Assets" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="Assets.aspx.vb" Inherits="MyEquipmentList" %>

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
                            &nbsp;
                            <asp:DropDownList ID="assetdd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlEquipment" DataTextField="assetID" DataValueField="equipmentID">
                                <asp:ListItem Value="0">Select Equipment</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="addequipmentbtn" runat="server" Text="New Equipment" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <p>
                                <asp:LinkButton ID="assetlistbtn" runat="server">Full Asset List</asp:LinkButton>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipmentID,assetID FROM [t_equipment] WHERE ([companyID] = @companyID) ORDER BY assetID">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlLocations" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [shipID], [shipto] FROM [t_ship] WHERE ([companyID] = @companyID) ORDER BY [shipto]">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    </asp:Content>

