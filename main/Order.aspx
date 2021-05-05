<%@ Page Title="Order" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="Order.aspx.vb" Inherits="EST_Order" %>

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
                            <asp:Button ID="editbtn" runat="server" Text="Edit" CssClass="pushbutton1 gold" />
                            <asp:CheckBox ID="rcb" runat="server" AutoPostBack="True" Visible="False" />
                        </td>
                        <td colspan="2" style="text-align: right">
                            <asp:Button ID="printbtn" runat="server" CssClass="pushbutton1 gold" Text="Pick Ticket" />
                            <asp:Button ID="pickbtn" runat="server" CssClass="pushbutton1 gold" Text="Pick Order" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label28" runat="server" CssClass="heading1" Text="OrderID"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="orderIDlbl" runat="server" CssClass="est_label1"></asp:Label>
                            &nbsp;<asp:Label ID="bolbl" runat="server" Font-Bold="True" ForeColor="Red" Text="BACKORDER" Visible="False"></asp:Label>
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
                            <asp:Label ID="Label43" runat="server" CssClass="heading1" Text="Purchase Order"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="potb" runat="server" CssClass="est_label1"></asp:Label>&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="needspocb" runat="server" Text="Needs PO" AutoPostBack="True" />
                        </td>
                        <td>
                            <asp:Label ID="Label51" runat="server" CssClass="heading1" Text="Ordered By"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="order_userIDlbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label1" runat="server" CssClass="heading1" Text="Assets"></asp:Label></td>
                        <td>
                            <asp:Label ID="assetlbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
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
                        <td style="vertical-align: top">
                            <asp:Label ID="Label35" runat="server" CssClass="heading1" Text="Vendor"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="vendorlbl" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label40" runat="server" CssClass="heading1" Text="Sales Rep"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="replbl" runat="server" CssClass="est_label1"></asp:Label>
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
                        <td>
                            <asp:Label ID="Label44" runat="server" CssClass="heading1" Text="Fax"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="vfaxlbl" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                        <td>
                            <asp:Label ID="Label52" runat="server" CssClass="heading1" Text="Include Samples"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="samplecb" runat="server" AutoPostBack="True" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            Samples Complete</td>
                        <td>
                            <asp:CheckBox ID="sample_completecb" runat="server" AutoPostBack="True" />
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
                            </td>
                        <td>
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
                            
                            <asp:DropDownList ID="shipotionsdd" runat="server" AutoPostBack="True">
                                <asp:ListItem>Ship &amp; BO</asp:ListItem>
                                <asp:ListItem>Ship Complete</asp:ListItem>
                                <asp:ListItem>Ship &amp; Cancel BO</asp:ListItem>
                            </asp:DropDownList>
                            
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
                            <asp:Label ID="Label50" runat="server" CssClass="heading1" Text="CC"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="cctb" runat="server" Width="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">&nbsp;</td>
                    </tr>
                    <asp:Panel ID="Panel1" runat="server" Visible="False">
                        <tr>
                            <td colspan="4" style="text-align: left"><asp:Label ID="Label8" runat="server" Text="KIT INFO" Font-Size="Medium" Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                            
                                    <table style="width: 100%; background-color: #CCCCCC">
                                    <tr>
                                        <td style="width: 15%"><asp:Label ID="Label11" runat="server" Text="Asset ID" CssClass="heading1"></asp:Label></td>
                                        <td colspan="3">
                                            <asp:Label ID="assetIDlbl" runat="server" CssClass="est_label1"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="Label13" runat="server" Text="Equipment" CssClass="heading1"></asp:Label></td>
                                        <td colspan="3">
                                            <asp:Label ID="equipmentlbl" runat="server" CssClass="est_label1"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>    
                                            <asp:Label ID="Label12" runat="server" CssClass="heading1" Text="Kit"></asp:Label></td>
                                        <td colspan="3">
                                            <asp:Label ID="kitlbl" runat="server" CssClass="est_label1"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="Label14" runat="server" Text="Hours/Miles" CssClass="heading1"></asp:Label></td>
                                        <td colspan="3">
                                            <asp:Label ID="hourslbl" runat="server" CssClass="est_label1"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center">&nbsp;</td>
                        </tr>
                    </asp:Panel>
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
                                                    <td style="width: 30%; text-align: left;">Part Number</td>
                                                    <td style="width: 10%; text-align: center;">Est. Del.</td>
                                                    <td style="width: 10%; text-align: center;">PO</td>
                                                    <td style="width: 10%; text-align: center;">OH</td>
                                                    <td style="width: 10%; text-align: center;">Qty</td>
                                                    <td style="width: 10%; text-align: center;">Shipped</td>
                                                    <td style="width: 10%; text-align: right;">Price</td>
                                                    <td style="width: 10%; text-align: center;">UoM</td>
                                                    <td style="width: 10%; text-align: right;">Extended</td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 30%; text-align: left;">
                                                        <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label>&nbsp;
                                                        <a href='VCatalogPage.aspx?productID=<%# appcode.GetProductID(Eval("manufacturer"), Eval("partnumber"))%>'><asp:Label ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber") %>' Font-Bold='<%# appcode.IsOnPO(Eval("manufacturer"), Eval("partnumber"))%>'></asp:Label></a>
                                                    </td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="estdellbl" runat="server" Text='<%# appcode.GetEstimatedArrival(Eval("manufacturer"), Eval("partnumber"), appcode.GetOpenLineQuantity(Eval("lineID"))).ToString%>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="polbl" runat="server" Text='<%# appcode.GetOnPO(Eval("manufacturer"), Eval("partnumber")).ToString%>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="onhandlbl" runat="server" Text='<%# appcode.GetOnHand(Eval("manufacturer"), Eval("partnumber")).ToString%>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="qtytb" runat="server" Text='<%# Bind("quantity") %>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="shippedlbl" runat="server" Text='<%# Bind("ship_qty") %>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="pricelbl" runat="server" Text='<%# FormatCurrency(Eval("price"), 2)%>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="uomlbl" runat="server" Text='<%# Bind("uom") %>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="extendedlbl" runat="server" Text='<%# FormatCurrency(Eval("extended", "{0:c}"), 2)%>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5"><asp:Label ID="itemnamelbl" runat="server" Text='<%# Bind("item") %>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"></td>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="gplbl" runat="server" Text='<%# FormatPercent(appcode.GetGPPercent(Eval("cost"), Eval("price")), 2)%>' ForeColor="Red"></asp:Label>
                                                    </td>
                                                    <td></td>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="GPDlbl" runat="server" Text='<%# FormatCurrency(appcode.GetGPDollars(Eval("cost"), Eval("price"), Eval("quantity")), 2)%>' ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 30%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"><asp:Label ID="Label2" runat="server" Text="Sub-Total" Font-Bold="True"></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="subtotallbl" runat="server" Font-Bold="True"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"><asp:Label ID="Label3" runat="server" Text="Sales Tax" Font-Bold="True"></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="salestaxlbl" runat="server" Font-Bold="True"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"><asp:Label ID="Label4" runat="server" Text="Total" Font-Bold="True"></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="grandtotallbl" runat="server" Font-Bold="True"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"><asp:Label ID="Label6" runat="server" Text="Total GP" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="totalgplbl" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
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
                &nbsp;</td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                            <asp:Label ID="Label46" runat="server" Text="Shipments" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlShipments" GridLines="None" Width="100%">
                    <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                    <RowStyle BorderWidth="1px" />
                    <Columns>
                        <asp:TemplateField HeaderText="shipmentID" SortExpression="shipmentID" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="shipmentIDlbl" runat="server" Text='<%# Bind("shipmentID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="carrier" HeaderText="Carrier" SortExpression="carrier">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tracking" HeaderText="Tracking Number" SortExpression="tracking">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="shipment_date" HeaderText="Shipped On" SortExpression="shipment_date">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="invoiceID" HeaderText="Invoice" SortExpression="invoiceID">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="invoicetotallbl" runat="server" Text='<%# FormatCurrency(appcode.GetInvoiceTotal(Eval("shipmentID")), 2)%>'></asp:Label>
                            </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="viewbtn" runat="server" Text="View" CommandName="View" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <asp:SqlDataSource ID="SqlLines" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT manufacturer,partnumber,lineID,quantity,SUM(ship_qty) as ship_qty,price,uom,item,cost,quantity * price as extended, quantity * cost as extendedcost FROM [v_order_lines] WHERE ([orderID] = @orderID) GROUP BY manufacturer,partnumber,lineID,quantity,price,uom,item,cost ORDER BY [lineID]">
                    <SelectParameters>
                        <asp:SessionParameter Name="orderID" SessionField="orderID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlShipments" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [v_shipments] WHERE orderID= @orderID and shipped=@shipped ORDER BY shipmentID">
                    <SelectParameters>
                        <asp:SessionParameter Name="orderID" SessionField="orderID" />
                        <asp:Parameter DefaultValue="True" Name="shipped" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:HiddenField ID="userIDlbl" runat="server" />
                <asp:HiddenField ID="serviceIDlbl" runat="server" />
                <asp:HiddenField ID="companyIDlbl" runat="server" />
                <asp:HiddenField ID="vendorIDlbl" runat="server" />
            </td>
        </tr>
    </table>
    </asp:Content>

