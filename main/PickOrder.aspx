<%@ Page Title="Pick Order" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="PickOrder.aspx.vb" Inherits="EST_PickOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
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
                        <td style="width: 15%">
                            <asp:Label ID="Label28" runat="server" CssClass="heading1" Text="OrderID"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="orderIDlbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                        <td style="width: 15%">
                            <asp:Label ID="Label39" runat="server" CssClass="heading1" Text="Ordered On"></asp:Label>
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
                            <asp:Label ID="potb" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label42" runat="server" CssClass="heading1" Text="Deliver By"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="deliverby_datetb" runat="server"></asp:Label>
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
                            <asp:Label ID="Label45" runat="server" CssClass="heading1" Text="Ship To"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
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
                            <asp:Label ID="Label30" runat="server" CssClass="heading1" Text="Ship To"></asp:Label>
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
                            <asp:Label ID="contactlbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label47" runat="server" CssClass="heading1" Text="Ship Method"></asp:Label></td>
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
                            
                            <asp:Label ID="Label48" runat="server" CssClass="heading1" Text="Ship Options"></asp:Label>    
                            
                        </td>
                        <td>
                            
                            <asp:DropDownList ID="shipoptionsdd" runat="server">
                                <asp:ListItem>Ship &amp; BO</asp:ListItem>
                                <asp:ListItem>Ship Complete</asp:ListItem>
                                <asp:ListItem>Ship &amp; Cancel BO</asp:ListItem>
                            </asp:DropDownList>
                            
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label43" runat="server" CssClass="heading1" Text="Fax"></asp:Label></td>
                        <td><asp:Label ID="faxlbl" runat="server" CssClass="est_label1"></asp:Label></td>
                        <td>    
                            <asp:Label ID="Label49" runat="server" CssClass="heading1" Text="Kit"></asp:Label></td>
                        <td>
                            <asp:CheckBox ID="kitcb" runat="server" />
                            </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2"><asp:Label ID="Label1" runat="server" Text="Lines" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
                        <td colspan="2" style="text-align: right"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="lineID" DataSourceID="SqlLines" Width="100%" GridLines="None">
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
                                                    <td style="width: 40%; text-align: left;">Part Number</td>
                                                    <td style="width: 10%; text-align: center;">OH</td>
                                                    <td style="width: 10%; text-align: center;">Qty</td>
                                                    <td style="width: 10%; text-align: center;">Shipped</td>
                                                    <td style="width: 10%; text-align: center;">UoM</td>
                                                    <td style="width: 20%; text-align: center;">Pick</td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 40%; text-align: left;">
                                                        <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label>&nbsp;
                                                        <asp:Label ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label>
                                                        <asp:Label ID="Label1" runat="server" Text=" ( Kit " Visible='<%# appcode.IsKitOrderItem(Eval("lineID")) %>'></asp:Label>
                                                        <asp:Label ID="kitIDlbl" runat="server" Text='<%# Eval("kitID")%>' Visible='<%# appcode.IsKitOrderItem(Eval("lineID"))%>'></asp:Label>
                                                        <asp:Label ID="Label5" runat="server" Text=")" Visible='<%# appcode.IsKitOrderItem(Eval("lineID"))%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="onhandlbl" runat="server" Text='<%# appcode.GetOnHand(Eval("manufacturer"), Eval("partnumber")).ToString%>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="qtytb" runat="server" Text='<%# Bind("quantity") %>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="shipqtytb" runat="server" Text='<%# appcode.GetShipQty(Eval("lineID"))%>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="uomlbl" runat="server" Text='<%# Bind("uom") %>'></asp:Label></td>
                                                    <td style="width: 20%; text-align: center;"><asp:TextBox ID="pickqtytb" runat="server" Width="40" Enabled='<%# appcode.IsOrderLineOpen(Eval("lineID"))%>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6"><asp:Label ID="itemnamelbl" runat="server" Text='<%# Bind("item") %>'></asp:Label></td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;</td>
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
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Button ID="savebtn" runat="server" Text="Save" CssClass="pushbutton1 gold" />
                            <asp:Button ID="cancelbtn" runat="server" Text="Back" CssClass="pushbutton1 gold" />                            
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlLines" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_order_line] WHERE ([orderID] = @orderID) ORDER BY [lineID]">
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
                <asp:HiddenField ID="companyIDlbl" runat="server" />
                <asp:HiddenField ID="vendorIDlbl" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

