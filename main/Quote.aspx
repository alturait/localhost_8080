<%@ Page Title="Quote" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="Quote.aspx.vb" Inherits="EST_Quote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            <asp:Button ID="copybtn" runat="server" CssClass="pushbutton1 gold" Text="Create Order" />
                            <asp:Button ID="editbtn" runat="server" CssClass="pushbutton1 gold" Text="Edit" />
                        </td>
                        <td colspan="2" style="text-align: right">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label28" runat="server" CssClass="heading1" Text="Quote ID"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="quoteIDlbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                        <td style="width: 15%">
                            <asp:Label ID="Label39" runat="server" CssClass="heading1" Text="Quoted On"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="quotedatelbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="Label42" runat="server" CssClass="heading1" Text="Deliver By"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="deliverby_datetb" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label35" runat="server" CssClass="heading1" Text="Vendor"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="vendorlbl" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label40" runat="server" CssClass="heading1" Text="Remit To"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="remittolbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label41" runat="server" CssClass="heading1" Text="Phone"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="vphonelbl" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:Label ID="Label44" runat="server" CssClass="heading1" Text="Fax"></asp:Label>
                        </td>
                        <td class="auto-style1">
                            <asp:Label ID="vfaxlbl" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                        <td class="auto-style1">
                            </td>
                        <td class="auto-style1">
                            </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" CssClass="heading1" Text="Customer"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="customerlbl" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                        <td>
                            </td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label29" runat="server" CssClass="heading1" Text="Bill To"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="billtolbl" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label30" runat="server" CssClass="heading1" Text="Ship To"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="shiptolbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                    </tr>
                    <tr>    
                        <td>
                            <asp:Label ID="Label7" runat="server" CssClass="heading1" Text="Contact"></asp:Label></td>
                        <td>
                            <asp:Label ID="contactlbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label49" runat="server" CssClass="heading1" Text="Kit"></asp:Label>    
                            </td>
                        <td>
                            <asp:CheckBox ID="kitcb" runat="server" />
                            </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label34" runat="server" CssClass="heading1" Text="Phone"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="phonelbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                        <td>    
                            <asp:Label ID="Label52" runat="server" CssClass="heading1" Text="Kit ID"></asp:Label>    
                            </td>
                        <td>
                            <asp:Label ID="kitIDlbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label45" runat="server" CssClass="heading1" Text="Email"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="totb" runat="server" Width="200"></asp:TextBox>
                        </td>
                        <td>    
                            </td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label46" runat="server" CssClass="heading1" Text="CC"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="cctb" runat="server" Width="200"></asp:TextBox>
                        </td>
                        <td>    
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="lineID" DataSourceID="SqlLines" Width="100%" GridLines="None" ShowFooter="True">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:BoundField DataField="lineID" HeaderText="lineID" InsertVisible="False" ReadOnly="True" SortExpression="lineID" Visible="False" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 20%; text-align: left;">Manufacturer</td>
                                                    <td style="width: 20%; text-align: left;">Part Number</td>
                                                    <td style="width: 10%; text-align: center;">Qty</td>
                                                    <td style="width: 20%; text-align: right; padding-right: 15px;">Price</td>
                                                    <td style="width: 20%; text-align: left;">Availablity</td>
                                                    <td style="width: 10%; text-align: right;">Extended</td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 20%; text-align: left;"><asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label></td>
                                                    <td style="width: 20%; text-align: left;"><asp:Label ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="qtytb" runat="server" Text='<%# Bind("quantity") %>' Width="40"></asp:Label></td>
                                                    <td style="width: 20%; text-align: right; padding-right: 15px;"><asp:Label ID="pricelbl" runat="server" Text='<%# FormatCurrency(Eval("price"), 2)%>'></asp:Label> <asp:Label ID="uomlbl" runat="server" Text='<%# Bind("uom") %>'></asp:Label></td>
                                                    <td style="width: 20%; text-align: left;"><asp:Label ID="availabilitytb" runat="server" Text='<%# Eval("availability")%>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="extendedlbl" runat="server" Text='<%# FormatCurrency(Eval("extended", "{0:c}"), 2)%>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6"><asp:Label ID="itemnamelbl" runat="server" Text='<%# Bind("item") %>'></asp:Label></td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 20%; text-align: left;"></td>
                                                    <td style="width: 20%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 20%; text-align: left;"></td>
                                                    <td style="width: 20%; text-align: left;"><asp:Label ID="Label2" runat="server" Text="Sub-Total" Font-Bold="True"></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="subtotallbl" runat="server" Font-Bold="True"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;"></td>
                                                    <td style="text-align: left;"></td>
                                                    <td style="text-align: left;"></td>
                                                    <td style="text-align: left;"></td>
                                                    <td style="text-align: left;"><asp:Label ID="Label3" runat="server" Text="Sales Tax" Font-Bold="True"></asp:Label></td>
                                                    <td style="text-align: right;"><asp:Label ID="salestaxlbl" runat="server" Font-Bold="True"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;"></td>
                                                    <td style="text-align: left;"></td>
                                                    <td style="text-align: left;"></td>
                                                    <td style="text-align: left;"></td>
                                                    <td style="text-align: left;"><asp:Label ID="Label4" runat="server" Text="Total" Font-Bold="True"></asp:Label></td>
                                                    <td style="text-align: right;"><asp:Label ID="grandtotallbl" runat="server" Font-Bold="True"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <asp:Label ID="notestb" runat="server" CssClass="est_label1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <asp:SqlDataSource ID="SqlLines" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT *,quantity*price as extended FROM [t_quote_line] WHERE ([quoteID] = @quoteID) ORDER BY [lineID]">
                    <SelectParameters>
                        <asp:SessionParameter Name="quoteID" SessionField="quoteID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:HiddenField ID="userIDlbl" runat="server" />
                <asp:HiddenField ID="companyIDlbl" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

