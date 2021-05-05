<%@ Page Title="Send Update" Language="VB" MasterPageFile="MobileMaster.master" AutoEventWireup="false" CodeFile="SendUpdate.aspx.vb" Inherits="mobile_SendUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top;">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2" style="vertical-align: top; color: #FF0000; font-size: x-large">Select an administrator, assign a priority and type your message in the box below. Click on SEND UPDATE to submit your report.</td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
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
                            <asp:Label ID="Label2" runat="server" Text="Send To" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="userdd" runat="server" Font-Size="XX-Large" DataSourceID="SqlUsers" DataTextField="name" DataValueField="userID"></asp:DropDownList>
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
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label6" runat="server" Text="Priority" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="prioritydd" runat="server" Font-Size="XX-Large">
                                <asp:ListItem>RED</asp:ListItem>
                                <asp:ListItem>YELLOW</asp:ListItem>
                                <asp:ListItem>BLUE</asp:ListItem>
                            </asp:DropDownList>
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
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="sendbtn" runat="server" Font-Size="XX-Large" Height="100px" Text="SEND STATUS" Width="250px" />
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

