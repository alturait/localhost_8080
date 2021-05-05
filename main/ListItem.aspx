<%@ Page Title="List Item" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="ListItem.aspx.vb" Inherits="main_ListItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="3" style="text-align: left">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label26" runat="server" CssClass="est_heading1" Text="Email"></asp:Label>
                            *</td>
                        <td>
                            <asp:TextBox ID="emailtb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label22" runat="server" CssClass="est_heading1" Text="Name"></asp:Label>
                            *</td>
                        <td>
                            <asp:TextBox ID="nametb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label10" runat="server" CssClass="est_heading1" Text="Company"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:TextBox ID="companytb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <asp:Button ID="savebtn" runat="server" Text="Save" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

