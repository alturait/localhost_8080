<%@ Page Title="Purchase Order" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="PO.aspx.vb" Inherits="EST_PO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label28" runat="server" CssClass="heading1" Text="PO ID"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="poIDlbl" runat="server" CssClass="est_label1"></asp:Label>
                            <asp:CheckBox ID="submittedcb" runat="server" Text="Submitted" />
                        </td>
                        <td style="width: 15%">
                            &nbsp;</td>
                        <td style="width: 35%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label50" runat="server" CssClass="heading1" Text="PO"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="potb" runat="server"></asp:Label>
                            </td>
                        <td>
                            <asp:Label ID="Label39" runat="server" CssClass="heading1" Text="PO Date"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="podatelbl" runat="server" CssClass="est_label1"></asp:Label>
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
                            <asp:Label ID="Label56" runat="server" CssClass="heading1" Text="Supplier"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="suppliertb" runat="server" CssClass="est_textbox2"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label40" runat="server" CssClass="heading1" Text="Ship To"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="shiptotb" runat="server" CssClass="est_textbox2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label51" runat="server" CssClass="heading1" Text="Address"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="sup_address1tb" runat="server" CssClass="est_textbox2"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label57" runat="server" CssClass="heading1" Text="Address"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ship_address1tb" runat="server" CssClass="est_textbox2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">&nbsp;</td>
                        <td style="vertical-align: top">
                            <asp:Label ID="sup_address2tb" runat="server" CssClass="est_textbox2"></asp:Label>
                        </td>
                        <td style="vertical-align: top">&nbsp;</td>
                        <td>
                            <asp:Label ID="ship_address2tb" runat="server" CssClass="est_textbox2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label58" runat="server" CssClass="heading1" Text="City"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="sup_citytb" runat="server" CssClass="est_textbox2"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label53" runat="server" CssClass="heading1" Text="City"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ship_citytb" runat="server" CssClass="est_textbox2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label59" runat="server" CssClass="heading1" Text="State"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="sup_statetb" runat="server" CssClass="est_textbox2"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label54" runat="server" CssClass="heading1" Text="State"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ship_statetb" runat="server" CssClass="est_textbox2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label60" runat="server" CssClass="heading1" Text="Zip Code"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="sup_zipcodetb" runat="server" CssClass="est_textbox2"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label55" runat="server" CssClass="heading1" Text="Zip Code"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ship_zipcodetb" runat="server" CssClass="est_textbox2"></asp:Label>
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
                            <asp:Label ID="Label61" runat="server" CssClass="heading1" Text="Contact"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="sup_contacttb" runat="server" CssClass="est_textbox2"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label47" runat="server" CssClass="heading1" Text="Ordered By"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="usertb" runat="server" CssClass="est_textbox2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label64" runat="server" CssClass="heading1" Text="Phone"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="sup_phonetb" runat="server" CssClass="est_textbox2"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label41" runat="server" CssClass="heading1" Text="Phone"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="vphonetb" runat="server" CssClass="est_textbox2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label63" runat="server" CssClass="heading1" Text="Fax"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="sup_faxtb" runat="server" CssClass="est_textbox2"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label44" runat="server" CssClass="heading1" Text="Fax"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="vfaxtb" runat="server" CssClass="est_textbox2"></asp:Label>
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
                            <asp:Label ID="Label49" runat="server" CssClass="heading1" Text="Email To"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="email1tb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label48" runat="server" CssClass="heading1" Text="CC"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="email2tb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="polineID" DataSourceID="SqlLines" Width="100%" GridLines="None" ShowFooter="True">
                                <Columns>
                                    <asp:BoundField DataField="polineID" HeaderText="polineID" InsertVisible="False" ReadOnly="True" SortExpression="polineID" Visible="False" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 20%; text-align: left;">Manufacturer</td>
                                                    <td style="width: 20%; text-align: left;">Part Number</td>
                                                    <td style="width: 10%; text-align: center;">Qty</td>
                                                    <td style="width: 10%; text-align: center;">Received</td>
                                                    <td style="width: 15%; text-align: right;">Cost</td>
                                                    <td style="width: 10%; text-align: center;">UoM</td>
                                                    <td style="width: 15%; text-align: right;">Extended</td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 20%; text-align: left;"><asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label></td>
                                                    <td style="width: 20%; text-align: left;"><asp:Label ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="qtytb" runat="server" Text='<%# Bind("quantity") %>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="receivedlbl" runat="server" Text='<%# appcode.GetReceivedPOLineQuantity(Session("poID"), Eval("manufacturer"), Eval("partnumber"))%>'></asp:Label></td>
                                                    <td style="width: 15%; text-align: right;"><asp:Label ID="pricelbl" runat="server" Text='<%# FormatCurrency(Eval("cost"), 2)%>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="uomlbl" runat="server" Text='<%# Bind("uom") %>'></asp:Label></td>
                                                    <td style="width: 15%; text-align: right;"><asp:Label ID="extendedlbl" runat="server" Text='<%# FormatCurrency(Eval("quantity") * Eval("cost"), 2)%>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="8"><hr /></td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 20%; text-align: left;"></td>
                                                    <td style="width: 20%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 15%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"><asp:Label ID="Label2" runat="server" Text="Sub-Total" Font-Bold="True"></asp:Label></td>
                                                    <td style="width: 15%; text-align: right;"><asp:Label ID="subtotallbl" runat="server" Font-Bold="True"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Button ID="deletebtn" runat="server" CssClass="pushbutton1 gold" Text="Delete PO" OnClientClick="return confirm('Delete this PO?');"/>
                            <asp:Button ID="emailbtn" runat="server" CssClass="pushbutton1 gold" Text="Email PO" />
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
                &nbsp;</td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <asp:SqlDataSource ID="SqlLines" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT polineID,manufacturer,partnumber,sum(quantity) as quantity,cost,uom FROM [t_poline] WHERE ([poID] = @poID) group by polineID,manufacturer,partnumber,cost,uom ORDER BY [polineID]">
                    <SelectParameters>
                        <asp:SessionParameter Name="poID" SessionField="poID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

