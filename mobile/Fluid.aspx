<%@ Page Title="Fluids" Language="VB" MasterPageFile="MobileMaster.master" AutoEventWireup="false" CodeFile="Fluid.aspx.vb" Inherits="mobile_Fluid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top;">
                <table style="padding: 5px; border-style: solid; border-width: medium; width: 100%">
                    <tr>
                        <td colspan="2" style="vertical-align: top; color: #FF0000; font-size: x-large">&nbsp;</td>                        
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            <asp:Label ID="Label12" runat="server" Text="Equipment" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="equipmentlbl" runat="server" Text="Label" Font-Size="XX-Large"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Fluid" Font-Bold="True" Font-Size="XX-Large" ForeColor="#0066FF"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="fluiddd" runat="server" Font-Size="XX-Large" DataSourceID="SqlFluids" DataTextField="fluid" DataValueField="fluidID"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="Units" Font-Bold="True" Font-Size="XX-Large" ForeColor="#0066FF"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="uomdd" runat="server" Font-Size="XX-Large">
                                <asp:ListItem>GALLONS</asp:ListItem>
                                <asp:ListItem>QUARTS</asp:ListItem>
                                <asp:ListItem>OUNCES</asp:ListItem>
                                <asp:ListItem>LITRES</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Quantity" Font-Bold="True" Font-Size="XX-Large" ForeColor="#0066FF"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="qtytb" runat="server" Font-Size="XX-Large" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="Hours/Miles" Font-Bold="True" Font-Size="XX-Large" ForeColor="#0066FF"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="hourstb" runat="server" Font-Size="XX-Large" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="System" Font-Bold="True" Font-Size="XX-Large" ForeColor="#0066FF"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="notetb" runat="server" Font-Size="XX-Large" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="savebtn" runat="server" Font-Size="XX-Large" Text="UPDATE" />
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlFluids" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [fluidID], [fluid] FROM [t_fluid] WHERE ([companyID] = @companyID) ORDER BY [fluid]">
                    <SelectParameters>
                        <asp:SessionParameter Name="companyID" SessionField="mobile_companyID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

