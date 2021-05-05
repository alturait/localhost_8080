<%@ Page Title="Edit Quote" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="EditQuote.aspx.vb" Inherits="EST_EditQuote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="4" style="padding-bottom: 10px">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            <asp:CheckBox ID="sendcb" runat="server" Text="Email Quote" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="Label51" runat="server" Text="Email Message" Font-Bold="True" Font-Size="Medium"></asp:Label>                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:TextBox ID="emailmessagetb" runat="server" CssClass="est_textbox1" Rows="5" TextMode="MultiLine" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label53" runat="server" CssClass="heading1" Text="Quote ID"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="quoteIDlbl" runat="server" Font-Bold="False"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label39" runat="server" CssClass="heading1" Text="Requested On"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="requestdatelbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label42" runat="server" CssClass="heading1" Text="Deliver By"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="deliverby_datetb" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="deliverby_datetb" PopupButtonID="dpbtn1"></ajaxToolkit:CalendarExtender>
                            <asp:ImageButton ID="dpbtn1" runat="server" ImageUrl="~/Images/dp_image.jpg" ImageAlign="TextTop" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label35" runat="server" CssClass="heading1" Text="Vendor"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="vendorlbl" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label40" runat="server" CssClass="heading1" Text="Remit To"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="remittolbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" CssClass="heading1" Text="Customer"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="customerlbl" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                        <td>
                            <asp:Label ID="Label45" runat="server" CssClass="heading1" Text="Select Ship To"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="shiptodd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlShipTos" DataTextField="shipto" DataValueField="shipID">
                                <asp:ListItem Value="0">None</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label29" runat="server" CssClass="heading1" Text="Bill To"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="billtolbl" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label30" runat="server" CssClass="heading1" Text="Ship To"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="shiptolbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4"></td>
                    </tr>
                    <tr>    
                        <td>
                            <asp:Label ID="Label7" runat="server" CssClass="heading1" Text="Contact"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="contactdd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlContacts" DataTextField="name" DataValueField="userID">
                                <asp:ListItem Value="0">None</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label49" runat="server" CssClass="heading1" Text="Kit"></asp:Label>    
                            </td>
                        <td>
                            <asp:CheckBox ID="kitcb" runat="server" />
                            </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:Label ID="Label34" runat="server" CssClass="heading1" Text="Phone"></asp:Label>
                        </td>
                        <td class="auto-style1">
                            <asp:Label ID="phonelbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                        <td class="auto-style1">    
                            
                            <asp:Label ID="Label52" runat="server" CssClass="heading1" Text="Kit ID"></asp:Label>    
                            
                        </td>
                        <td class="auto-style1">
                            
                            <asp:Label ID="kitIDlbl" runat="server" CssClass="est_label1"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label43" runat="server" CssClass="heading1" Text="Email"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="totb" runat="server" Width="200"></asp:TextBox>
                            </td>
                        <td>    
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label46" runat="server" CssClass="heading1" Text="CC"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="cctb" runat="server" Width="200"></asp:TextBox>
                            </td>
                        <td>    
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label1" runat="server" Text="Lines" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
                        <td colspan="3" style="text-align: right">
                            <asp:Button ID="updatelinesbtn" runat="server" Text="Update Lines" CssClass="pushbutton1 gold" /></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="lineID" DataSourceID="SqlLines" Width="100%" GridLines="None" ShowFooter="True">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="lineID" InsertVisible="False" SortExpression="lineID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lineIDlbl" runat="server" Text='<%# Bind("lineID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="partID" HeaderText="partID" SortExpression="partID" Visible="False" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 20%; text-align: left;">Manufacturer</td>
                                                    <td style="width: 20%; text-align: left;">Part Number</td>
                                                    <td style="width: 10%; text-align: center;">Qty</td>
                                                    <td style="width: 20%; text-align: center;">Price</td>
                                                    <td style="width: 20%; text-align: left;">Availability</td>
                                                    <td style="width: 10%; text-align: right;">Extended</td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 20%; text-align: left;"><asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label></td>
                                                    <td style="width: 20%; text-align: left;"><asp:Label ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: center;"><asp:TextBox ID="qtytb" runat="server" Text='<%# Bind("quantity") %>' Width="40"></asp:TextBox></td>
                                                    <td style="width: 20%; text-align: center;"><asp:TextBox ID="pricetb" runat="server" Text='<%# FormatCurrency(Eval("price"), 2)%>' Width="60"></asp:TextBox> <asp:Label ID="uomlbl" runat="server" Text='<%# Bind("uom") %>'></asp:Label></td>
                                                    <td style="width: 20%; text-align: left;"><asp:TextBox ID="availabilitytb" runat="server" Text='<%# Eval("availability")%>'></asp:TextBox></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="extendedlbl" runat="server" Text='<%# FormatCurrency(Eval("extended", "{0:c}"), 2)%>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5"><asp:Label ID="itemnamelbl" runat="server" Text='<%# Bind("item") %>'></asp:Label></td>
                                                    <td style="text-align: right;">
                                                        <asp:LinkButton ID="deletebtn" runat="server" Text="Delete" CommandName="Delete" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                                    </td>
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
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="1"><asp:Label ID="Label6" runat="server" Text="Notes" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
                        <td colspan="3">

                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:TextBox ID="notestb" runat="server" CssClass="est_textbox1" Rows="5" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Button ID="submitbtn" runat="server" Text="Submit" CssClass="pushbutton1 gold" />
                            <asp:Button ID="cancelbtn" runat="server" Text="Delete" CssClass="pushbutton1 gold" OnClientClick="return confirm('Delete this Quote?');" />                            
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlLines" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT *,quantity*price as extended FROM [t_quote_line] WHERE ([quoteID] = @quoteID) ORDER BY [lineID]">
                    <SelectParameters>
                        <asp:SessionParameter Name="quoteID" SessionField="quoteID" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlVendors" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [vendorID], [company] FROM [v_company_vendor] WHERE ([companyID] = @companyID) ORDER BY [company]">
                    <SelectParameters>
                        <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlShipTos" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [shipID], [shipto] FROM [t_ship] WHERE ([companyID] = @companyID) ORDER BY [shipto]">
                    <SelectParameters>
                        <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlShipTosByUser" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [shipID], [shipto] FROM [v_user_location] WHERE ([userID] = @userID) ORDER BY [shipto]">
                    <SelectParameters>
                        <asp:SessionParameter Name="userID" SessionField="userID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlContacts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [userID], [name] FROM [t_user] WHERE ([companyID] = @companyID) ORDER BY [name]">
                    <SelectParameters>
                        <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:HiddenField ID="companyIDlbl" runat="server" />
                <asp:HiddenField ID="completelbl" runat="server" />
                <asp:HiddenField ID="serviceIDlbl" runat="server" />
                <asp:HiddenField ID="vendorIDlbl" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

