<%@ Page Title="Home Page" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="LT_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../App_Themes/appcode/StyleSheet.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>&nbsp; 
                            </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="salesytdbtn" runat="server">Sales YTD</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </asp:Content>

