<%@ Page Title="PO Note" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="PONote.aspx.vb" Inherits="PONote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" CssClass="heading1" Text="Date"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="datelbl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="heading1" Text="Author"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="authorlbl" runat="server" Width="300px" AutoPostBack="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label3" runat="server" CssClass="heading1" Text="Note"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="notetb" runat="server" Width="100%" Rows="5" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="savebtn" runat="server" Text="Save" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

