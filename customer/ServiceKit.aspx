<%@ Page Title="Service Kit" Language="VB" MasterPageFile="~/customer/CustomerMaster.master" AutoEventWireup="false" CodeFile="ServiceKit.aspx.vb" Inherits="customer_ServiceKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                            <asp:Button ID="backbtn" runat="server" Text="Profile" CssClass="pushbutton1 gold" />
                            <asp:Button ID="printbtn" runat="server" Text="PDF" CssClass="pushbutton1 gold" />
                        </td>
                        <td style="text-align: center" rowspan="6">
                            <asp:Panel ID="orderpanel" runat="server">
                                <asp:Label ID="Label49" runat="server" CssClass="heading1" Text="Hours/Miles"></asp:Label>
                                &nbsp;<asp:TextBox ID="choursmilestb" runat="server"></asp:TextBox>
                                <asp:Button ID="orderbtn" runat="server" Text="Order" CssClass="pushbutton1 gold" />
                                <br />
                                <br />
                            </asp:Panel>
                            <asp:CheckBox ID="showcb" runat="server" Text="Show All Parts" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server" CssClass="heading1" Text="Kit Name"></asp:Label>
                        </td>
                        <td>
                            <asp:Textbox ID="servicenametb" runat="server" Width="100%"></asp:Textbox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" CssClass="heading1" Text="Interval"></asp:Label>
                        </td>
                        <td>
                            <asp:Textbox ID="intervaltb" runat="server" Width="100%"></asp:Textbox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label48" runat="server" CssClass="heading1" Text="Interval Type"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="intervalrb" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>Miles</asp:ListItem>
                                <asp:ListItem>Hours</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="interval_errorlbl" runat="server" ForeColor="Red" Text="Interval must be a number" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label42" runat="server" CssClass="heading1" Text="Estimated Cost"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="totalpartstb" runat="server" CssClass="est_textbox1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label36" runat="server" CssClass="heading1" Text="Notes"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="servicenotestb" runat="server" CssClass="est_textbox1" Rows="5" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="partID" DataSourceID="SqlEquipmentParts" Width="100%" GridLines="None">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="partIDlbl" runat="server" Text='<%# Eval("partID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 15%; text-align: left">
                                                        <asp:Label ID="Label21" runat="server" Text="Mfr"></asp:Label>
                                                    </td>
                                                    <td style="width: 50%; text-align: left">
                                                        <asp:Label ID="Label22" runat="server" Text="Part Number"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="Label24" runat="server" Text="Qty"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: right">
                                                        <asp:Label ID="Label4" runat="server" Text="Price"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="Label8" runat="server" Text="UOM"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="Label3" runat="server" Text="Select"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 15%; text-align: left">
                                                        <asp:Label ID="Label1" runat="server" Text="Primary" CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td style="width: 50%; text-align: left">
                                                        <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Eval("manufacturer")%>' CssClass="est_heading1"></asp:Label> <asp:Label ID="partnumberlbl" runat="server" Text='<%# Eval("partnumber")%>' CssClass="est_heading1"></asp:Label> (DFO: <asp:Label ID="onhandlbl" runat="server" Text='<%# appcode.GetOnHand(Eval("manufacturer"), Eval("partnumber")).ToString%>'></asp:Label>)
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="quantitylbl" runat="server" Text='<%# Eval("quantity")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: right">
                                                        <asp:Label ID="pricelbl" runat="server" Text='<%# FormatCurrency(Eval("price"))%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="uomlbl" runat="server" Text='<%# Eval("uom")%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:CheckBox ID="selectcb" runat="server" Checked='<%# appcode.IsKitItemSelected(Session("serviceprofileID"), Eval("manufacturer"), Eval("partnumber"))%>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text="Alternate" CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td colspan="6">
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("alt_manufacturer")%>'></asp:Label>
                                                        <asp:Label ID="oemlbl" runat="server" Text='<%# Eval("alt_partnumber")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text="OEM" CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td colspan="6">
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("oem_manufacturer")%>'></asp:Label>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("oem_partnumber")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label11" runat="server" Text="Description" CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td colspan="6">
                                                        <asp:Label ID="descriptionlbl"  runat="server" Text='<%# Eval("description")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <asp:Button ID="savebtn" runat="server" Text="Save" CssClass="pushbutton1 gold" />
                            <asp:Button ID="deletebtn" runat="server" Text="Delete"  OnClientClick="return confirm('Delete this service profile?')" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlEquipmentParts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM t_parts WHERE equipmentID = @equipmentID ORDER BY partnumber">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="equipmentID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlServiceParts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM v_kitparts WHERE serviceprofileID=@serviceprofileID ORDER BY partnumber">
        <SelectParameters>
            <asp:SessionParameter Name="serviceprofileID" SessionField="serviceprofileID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:HiddenField ID="kitcode" runat="server" />
</asp:Content>

