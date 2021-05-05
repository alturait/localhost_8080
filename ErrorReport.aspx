<%@ Page Title="Error Report" Language="VB" AutoEventWireup="false" CodeFile="ErrorReport.aspx.vb" Inherits="ErrorReport" %>

    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                        <td style="text-align: right"></td>
                        <td style="text-align: right"></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <p>Error reported by <asp:Label ID="userlbl" runat="server"></asp:Label></p>
                            <p>Error = <asp:Label ID="errorlbl" runat="server"></asp:Label></p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

