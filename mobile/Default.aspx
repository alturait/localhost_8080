<%@ Page Title="Login" Language="VB" MasterPageFile="MobileMaster.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="mobile_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top;">
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align: top;">
                            <table>
                                <tr>
                                    <td>
                                        <p style="font-size: xx-large">
                                        Welcome to <strong>DFO Filters & Equipment!</strong> 
                                        </p>
                                        <p style="text-align: center">
                                            <asp:Image ID="logoimg" runat="server" />
                                        </p>
                                        <p style="font-size: X-large">
                                            If you do not have an access code, contact Ken Gardner at 480-295-1676 to get one set up.
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                            <table align="center">
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label ID="Label2" runat="server" Text="Access Code" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel1" runat="server" DefaultButton="loginbtn">
                                            <asp:TextBox ID="accesstb" runat="server" TextMode="Password" Font-Size="XX-Large"></asp:TextBox>
                                            <asp:Button ID="loginbtn" runat="server" Text="ENTER" Font-Size="XX-Large" />
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center" colspan="2">
                                        
                                    </td>
                                </tr>
                                <tr><td colspan="2" style="text-align: center">&nbsp;</td></tr>
                                <asp:Panel ID="Panel3" runat="server" Visible="False">
                                    <tr><td colspan="2" style="text-align: center"><asp:Label ID="errmsglbl" runat="server" Font-Bold="True" ForeColor="Red" /></td></tr>
                                    <tr><td style="text-align: center">&nbsp;</td></tr>
                                </asp:Panel>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

