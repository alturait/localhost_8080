<%@ Page Title="Error Page" Language="VB" MasterPageFile="CustomerMaster.master" AutoEventWireup="false" CodeFile="Error.aspx.vb" Inherits="customer_Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td><asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"/></td>
                    </tr>
                    <tr>
                        <td style="text-align: center">OOPS! There was an error in the application and the webmaster has been notified. Click <a href="Default.aspx">HERE</a> to return to the home page.</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

