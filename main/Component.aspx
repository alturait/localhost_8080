<%@ Page Title="Component" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="Component.aspx.vb" Inherits="main_Component" %>

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
                        <td style="text-align: right">
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Component"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="componenttb" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            
                            <asp:Button ID="savebtn" runat="server" Text="Save" />
                            
                            <asp:Button ID="deletebtn" runat="server" Text="Delete" />
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
    </table>
</asp:Content>

