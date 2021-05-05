<%@ Page Title="Confirm Order" Language="VB" MasterPageFile="MobileMaster.master" AutoEventWireup="false" CodeFile="ConfirmOrder.aspx.vb" Inherits="mobile_ConfirmOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top;">
                <table style="width: 100%">
                    <tr>
                        <td colspan="3" style="text-align: center"><asp:Label ID="Label12" runat="server" Text="ASSET ID  " Font-Bold="True" Font-Size="XX-Large" Font-Italic="True"></asp:Label>
                            <asp:Label ID="assetIDlbl" runat="server" Font-Size="XX-Large"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table style="padding: 5px; border-style: solid; border-width: medium; width: 100%">
                                <tr>
                                    <td colspan="3">
                                    <asp:Label ID="hourslbl1" runat="server" Font-Bold="True" Font-Size="XX-Large" Font-Italic="True">ORDER INFORMATION</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="vertical-align: top; color: #FF0000; font-size: x-large">Select a ship to address and enter a po or reference. If everything is correct, click ORDER to submit. If you need to make changes, click BACK.</td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label3" runat="server" Text="Ship To" Font-Bold="False" Font-Size="XX-Large"></asp:Label></td>
                                    <td>
                                    <asp:DropDownList ID="shiptodd" runat="server" DataSourceID="SqlShipTo" DataTextField="shipto" DataValueField="shipID" Font-Size="XX-Large" Font-Bold="True">
                                    </asp:DropDownList>
                                    </td>
                                    <td rowspan="7" style="vertical-align: top; text-align: center">
                                        <asp:Button ID="orderbtn" runat="server" Text="ORDER" Font-Size="XX-Large" Width="250px" Height="250px" />
                                        <br />
                                        <br />
                                        <asp:Button ID="backbtn" runat="server" Text="BACK" Font-Size="XX-Large" Height="100px" Width="250px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%"><asp:Label ID="Label1" runat="server" Text="PO" Font-Bold="True" Font-Size="XX-Large" ForeColor="#0066FF"></asp:Label></td>
                                    <td><asp:TextBox ID="potb" runat="server" Font-Size="XX-Large" BorderStyle="Solid" BorderWidth="2px" Width="300px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label13" runat="server" Text="Equipment" Font-Bold="False" Font-Size="XX-Large"></asp:Label></td>
                                    <td>
                                    <asp:Label ID="equipmentlbl" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label4" runat="server" Text="Kit" Font-Bold="False" Font-Size="XX-Large"></asp:Label></td>
                                    <td>
                                    <asp:Label ID="kitlbl" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label9" runat="server" Text="Interval" Font-Bold="False" Font-Size="XX-Large"></asp:Label></td>
                                    <td>
                                    <asp:Label ID="intervallbl" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label2" runat="server" Text="Hours/Miles" Font-Bold="True" Font-Size="XX-Large" ForeColor="#0066FF"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="hourstb" runat="server" Font-Size="XX-Large" BorderStyle="Solid" BorderWidth="2px" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>                    
                    <tr>
                        <td colspan="3">
                            <table style="padding: 5px; border-style: solid; border-width: medium; width: 100%">
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>                            
                                <tr>
                                    <td>
                                    <asp:Label ID="hourslbl0" runat="server" Font-Bold="True" Font-Size="XX-Large" Font-Italic="True">KIT CONTENTS</asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>                            
                                <tr>
                                    <td colspan="3" style="vertical-align: top; color: #FF0000; font-size: x-large">Uncheck any filters that you do <u>not</u> want to be included in this order only.</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlFilters" Font-Size="X-Large" GridLines="None" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Part #" SortExpression="partnumber">
                                                <ItemTemplate>
                                                    <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer")%>'></asp:Label> <asp:Label ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item" SortExpression="description">
                                                <ItemTemplate>
                                                    <asp:Label ID="itemlbl" runat="server" Text='<%# Bind("description")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty" SortExpression="quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="qtylbl" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="chkBoxClass">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="selectcb" runat="server" Checked="True" Font-Size="XX-Large" />
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="XX-Large" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="partID" SortExpression="partID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="partIDlbl" runat="server" Text='<%# Bind("partID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            
                            </table>
                        </td>
                    </tr>                    
                    <tr>
                        <td></td>
                        <td></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            &nbsp;</td>
                        <td style="text-align: center">
                            &nbsp;</td>
                    </tr>
                </table>
    <asp:SqlDataSource ID="SqlFilters" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [partID],[manufacturer], [partnumber], [description], [quantity] FROM [v_serviceparts] WHERE ([serviceprofileID] = @serviceprofileID) AND ([selected]='True') ORDER BY [manufacturer], [partnumber]">
        <SelectParameters>
            <asp:SessionParameter Name="serviceprofileID" SessionField="mobile_serviceprofileID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlShipTo" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [shipto], [shipID] FROM [t_ship] WHERE ([companyID] = @companyID) ORDER BY [shipto]">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="mobile_companyID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

