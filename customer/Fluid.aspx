<%@ Page Title="Fluid" Language="VB" MasterPageFile="CustomerMaster.master" AutoEventWireup="false" CodeFile="Fluid.aspx.vb" Inherits="customer_Fluid" %>

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
                        <td style="text-align: right" colspan="2">
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 20%;">
                            <a href="Assets.aspx">Asset List</a><br />
                            <a href="OpenKits.aspx">Pending Kits</a><br />
                            <a href="EquipmentOrderHistory.aspx?eid=0">History(All)</a><br />
                            <a href="PartSummary.aspx">Part Summary</a><br /><br />
                            <a href="FluidList.aspx">Fluid List</a><br />
                            <a href="FluidSummary.aspx">Fluid Usage</a><br />
                            <a href="FindEquipment.aspx">New Asset</a>
                        </td>
                        <td colspan="2" style="vertical-align: top">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Fluid" CssClass="est_heading1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="fluidtb" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="savebtn" runat="server" Text="Save" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

