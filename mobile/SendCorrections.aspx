<%@ Page Title="Send Corrections" Language="VB" MasterPageFile="~/mobile/MobileMaster.master" AutoEventWireup="false" CodeFile="SendCorrections.aspx.vb" Inherits="mobile_SendCorrections" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top;">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2" style="vertical-align: top; color: #FF0000; font-size: x-large">Type your corrections in the box below then click SEND CORRECIONS. Updates generally take 24 hours. Call 480-295-1676 if you need this corrected sooner.</td>                        
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td></tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label1" runat="server" Text="Asset" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="equipmentlbl" runat="server" Text="Label" Font-Size="XX-Large"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label11" runat="server" Text="Kit" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="kitlbl" runat="server" Text="Label" Font-Size="XX-Large"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label4" runat="server" Text="From" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="fromlbl" runat="server" Text="Label" Font-Size="XX-Large"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">&nbsp;</td>
                        <td> &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 15%; vertical-align: top;">
                            <asp:Label ID="Label10" runat="server" Text="Message" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="messagetb" runat="server" Font-Size="XX-Large" TextMode="MultiLine" Rows="10" Width="800px" BorderStyle="Solid" BorderWidth="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; vertical-align: top;">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="2">
                            <asp:Button ID="sendbtn" runat="server" Font-Size="XX-Large" Height="100px" Text="SEND CORRECTIONS" Width="400px" />
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlUsers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [userID], [name] FROM [t_user] WHERE ([companyID] = @companyID) ORDER BY [name]">
                    <SelectParameters>
                        <asp:SessionParameter Name="companyID" SessionField="mobile_companyID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

