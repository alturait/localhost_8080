<%@ Page Title="Edit Return" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="EditReturn.aspx.vb" Inherits="main_EditReturn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            <asp:Button ID="submitbtn" runat="server" Text="Accept" CssClass="pushbutton1 gold" />
                            <asp:Button ID="cancelbtn" runat="server" Text="Delete" CssClass="pushbutton1 gold" OnClientClick="return confirm('Delete this Return?');" />
                        </td>
                        <td style="text-align: right;" colspan="2">
                            <asp:CheckBox ID="sendcb" runat="server" Text="Send RGA Confirmation" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="Label50" runat="server" Text="Email Message" Font-Bold="True" Font-Size="Medium"></asp:Label>                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:TextBox ID="emailmessagetb" runat="server" CssClass="est_textbox1" Rows="5" TextMode="MultiLine" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label28" runat="server" CssClass="heading1" Text="RGA ID"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="orderIDlbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                        <td style="width: 15%">
                            <asp:Label ID="Label39" runat="server" CssClass="heading1" Text="Requested On"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="orderdatelbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label46" runat="server" CssClass="heading1" Text="Purchase Order"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="potb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label42" runat="server" CssClass="heading1" Text="Received On"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="deliverby_datetb" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="deliverby_datetb" PopupButtonID="dpbtn1"></ajaxToolkit:CalendarExtender>
                            <asp:ImageButton ID="dpbtn1" runat="server" ImageUrl="~/Images/dp_image.jpg" ImageAlign="TextTop" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label51" runat="server" CssClass="heading1" Text="Original Invoice"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="invtb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
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
                        <td>
                            <asp:Label ID="Label41" runat="server" CssClass="heading1" Text="Phone"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="vphonelbl" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                        <td>
                            <asp:Label ID="Label44" runat="server" CssClass="heading1" Text="Fax"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="vfaxlbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            </td>
                        <td>
                            </td>
                        <td>
                            </td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" CssClass="heading1" Text="Customer"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="customerlbl" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                        <td>
                            <asp:Label ID="Label45" runat="server" CssClass="heading1" Text="Select Ship From"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="shiptodd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlShipTos" DataTextField="shipto" DataValueField="shipID">
                                <asp:ListItem Value="0">None</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label29" runat="server" CssClass="heading1" Text="Bill To"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="billtolbl" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label30" runat="server" CssClass="heading1" Text="Ship From"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="shiptolbl" runat="server" CssClass="est_label1"></asp:Label>
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
                            <asp:Label ID="Label7" runat="server" CssClass="heading1" Text="Contact"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="contactdd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlContacts" DataTextField="name" DataValueField="userID">
                                <asp:ListItem Value="0">None</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:DropDownList ID="shipmethoddd" runat="server">
                                <asp:ListItem>Local Delivery</asp:ListItem>
                                <asp:ListItem>Will Call</asp:ListItem>
                                <asp:ListItem>Fedex</asp:ListItem>
                                <asp:ListItem>Drop Ship</asp:ListItem>
                            </asp:DropDownList>
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
                            
                            &nbsp;</td>
                        <td>
                            
                            <asp:DropDownList ID="shipotionsdd" runat="server">
                                <asp:ListItem>Ship &amp; BO</asp:ListItem>
                                <asp:ListItem>Ship Complete</asp:ListItem>
                                <asp:ListItem>Ship &amp; Cancel BO</asp:ListItem>
                            </asp:DropDownList>
                            
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label8" runat="server" Text="Email" CssClass="heading1"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="totb" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td>    
                            &nbsp;</td>
                        <td>
                            <asp:CheckBox ID="kitcb" runat="server" />
                            </td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label9" runat="server" Text="CC" CssClass="heading1"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="cctb" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td>    
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="kitIDlbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2"><asp:Label ID="Label1" runat="server" Text="Lines" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
                        <td colspan="2" style="text-align: right">
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
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 30%; text-align: left;">Manufacturer</td>
                                                    <td style="width: 30%; text-align: left;">Part Number</td>
                                                    <td style="width: 10%; text-align: center;">Qty</td>
                                                    <td style="width: 10%; text-align: right;">Price</td>
                                                    <td style="width: 10%; text-align: center;">UoM</td>
                                                    <td style="width: 10%; text-align: right;">Extended</td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 30%; text-align: left;"><asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label></td>
                                                    <td style="width: 30%; text-align: left;"><asp:Label ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: center;"><asp:TextBox ID="qtytb" runat="server" Text='<%# Bind("quantity") %>' Width="40"></asp:TextBox></td>
                                                    <td style="width: 10%; text-align: right;"><asp:TextBox ID="pricetb" runat="server" Text='<%# FormatCurrency(Eval("price"), 2)%>' Width="50"></asp:TextBox></td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="uomlbl" runat="server" Text='<%# Bind("uom") %>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="extendedlbl" runat="server" Text='<%# FormatCurrency(Eval("extended", "{0:c}"), 2)%>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5"><asp:Label ID="itemnamelbl" runat="server" Text='<%# Bind("item") %>'></asp:Label></td>
                                                    <td style="width: 20%; text-align: right;" colspan="2">
                                                        <asp:LinkButton ID="deletebtn" runat="server" Text="Delete" CommandName="Delete" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 70%"></td>
                                                    <td style="text-align: left;"><asp:Label ID="Label2" runat="server" Text="Sub-Total" Font-Bold="True"></asp:Label></td>
                                                    <td style="text-align: right;"><asp:Label ID="subtotallbl" runat="server" Font-Bold="True"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 70%"></td>
                                                    <td style="text-align: left;"><asp:Label ID="Label3" runat="server" Text="Sales Tax" Font-Bold="True"></asp:Label></td>
                                                    <td style="text-align: right;"><asp:Label ID="salestaxlbl" runat="server" Font-Bold="True"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 70%"></td>
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
                        <td colspan="4">
                            <asp:Label ID="Label6" runat="server" CssClass="heading1" Text="Notes"></asp:Label><br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:TextBox ID="notestb" runat="server" CssClass="est_textbox1" Rows="5" TextMode="MultiLine" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlLines" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT *,quantity*price as extended FROM [t_order_line] WHERE ([orderID] = @orderID) ORDER BY [lineID]">
                    <SelectParameters>
                        <asp:SessionParameter Name="orderID" SessionField="orderID" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlVendors" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [vendorID], [company] FROM [v_company_vendor] WHERE ([companyID] = @companyID) ORDER BY [company]">
                    <SelectParameters>
                        <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlShipTos" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [shipID], [shipto] FROM [t_ship] WHERE ([companyID] = @companyID) ORDER BY [shipto]">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="companyIDlbl" Name="companyID" PropertyName="Value" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlContacts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [userID], [name] FROM [t_user] WHERE ([companyID] = @companyID) ORDER BY [name]">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="companyIDlbl" Name="companyID" PropertyName="Value" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:HiddenField ID="placedlbl" runat="server" />
                <asp:HiddenField ID="companyIDlbl" runat="server" />
                <asp:HiddenField ID="vendorIDlbl" runat="server" />
                <asp:HiddenField ID="serviceIDlbl" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

