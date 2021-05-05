<%@ Page Title="Sign In" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="Logon.aspx.vb" Inherits="Logon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
    <tr>
        <td class="pagebody" style="vertical-align: top;">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%; vertical-align: top;">
                            <table>
                                <tr>
                                    <td>
                                        <p>
                                        Welcome to <strong>Desert Fleet Outfitters</strong>! We provide web tools that allow users to manage their fleet inventory requirements and order customized kits for performing preventative maintenance on their vehicles. 
                                        </p>
                                        <p>
                                            If you do not have an account, contact us at 480-295-1676 or email us at <a href="mailto:sale@desertfleetoutfitters.com">sales@desertfleetoutfitters.com</a> to become a site member and enjoy all the benefits that membership provides.
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: center;">
                            <table align="center">
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label ID="Label1" runat="server" Text="User Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="usernametb" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel1" runat="server" DefaultButton="loginbtn">
                                            <asp:TextBox ID="passwordtb" runat="server" TextMode="Password"></asp:TextBox>
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
                                <tr>
                                    <td colspan="2" style="text-align: center"><asp:LinkButton ID="loginbtn" runat="server" Text="Login" CssClass="push_button1 orange" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    </table>
        </td>
    </tr>
</table>
</asp:Content>

