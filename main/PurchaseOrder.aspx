<%@ Page Title="Purchase Order" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="PurchaseOrder.aspx.vb" Inherits="EST_PurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Panel ID="Panel2" runat="server" DefaultButton="addpnbtn">
                                <asp:Label ID="Label5" runat="server" Text="PART NUMBER"></asp:Label>
                                <asp:TextBox ID="partnumbertb" runat="server"></asp:TextBox>
                                <asp:Button ID="addpnbtn" runat="server" Text=" ADD TO PO" />
                            </asp:Panel>
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="removebtn" runat="server" CssClass="pushbutton1 gold" Text="Remove Selected"/>
                            <asp:Button ID="printbtn" runat="server" CssClass="pushbutton1 gold" Text="Receiving Ticket"/>
                        </td>
                    </tr>
                    <asp:Panel ID="Panel1" runat="server">
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="Label65" runat="server" Text="Email Message" Font-Bold="True" Font-Size="Medium"></asp:Label>                            
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:TextBox ID="emailmessagetb" runat="server" CssClass="est_textbox1" Rows="5" TextMode="MultiLine" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td style="width: 35%">
                                &nbsp;</td>
                            <td style="width: 15%">
                                &nbsp;</td>
                            <td style="width: 35%">
                                &nbsp;</td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td>
                            <asp:Label ID="Label28" runat="server" CssClass="heading1" Text="PO ID"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="poIDlbl" runat="server" CssClass="est_label1"></asp:Label>
                            <asp:CheckBox ID="submittedcb" runat="server" Text="Submitted" AutoPostBack="True" />
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
                            <asp:TextBox ID="potb" runat="server"></asp:TextBox>
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
                            <asp:Label ID="Label66" runat="server" CssClass="heading1" Text="Arrival Date"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="estimated_arrivaltb" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="estimated_arrivaltb" PopupButtonID="dpbtn1"></ajaxToolkit:CalendarExtender>
                            <asp:ImageButton ID="dpbtn1" runat="server" ImageUrl="~/Images/dp_image.jpg" ImageAlign="TextTop" />
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
                        <td>&nbsp;</td>
                        <td>
                            <asp:DropDownList ID="supplierdd" runat="server" DataSourceID="SqlSuppliers" DataTextField="company" DataValueField="companyID" AppendDataBoundItems="True" AutoPostBack="True">
                                <asp:ListItem Value="0">Select Supplier</asp:ListItem>
                            </asp:DropDownList>
                        </td>
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
                            <asp:TextBox ID="suppliertb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label40" runat="server" CssClass="heading1" Text="Ship To"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="shiptotb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label51" runat="server" CssClass="heading1" Text="Address"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="sup_address1tb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label57" runat="server" CssClass="heading1" Text="Address"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="ship_address1tb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top" class="auto-style3">
                            &nbsp;</td>
                        <td style="vertical-align: top">
                            <asp:TextBox ID="sup_address2tb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                        <td>
                            <asp:TextBox ID="ship_address2tb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label58" runat="server" CssClass="heading1" Text="City"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="sup_citytb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label53" runat="server" CssClass="heading1" Text="City"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="ship_citytb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label59" runat="server" CssClass="heading1" Text="State"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="sup_statetb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label54" runat="server" CssClass="heading1" Text="State"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="ship_statetb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label60" runat="server" CssClass="heading1" Text="Zip Code"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="sup_zipcodetb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label55" runat="server" CssClass="heading1" Text="Zip Code"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="ship_zipcodetb" runat="server" CssClass="est_textbox2"></asp:TextBox>
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
                            &nbsp;</td>
                        <td>
                            <asp:DropDownList ID="sup_contactdd" runat="server" DataSourceID="SqlSupContacts" DataTextField="name" DataValueField="userID" AppendDataBoundItems="True" AutoPostBack="True">
                                <asp:ListItem Value="0">Select Contact</asp:ListItem>
                            </asp:DropDownList>
                        </td>
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
                            <asp:TextBox ID="sup_contacttb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label47" runat="server" CssClass="heading1" Text="Ordered By"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="usertb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label64" runat="server" CssClass="heading1" Text="Phone"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="sup_phonetb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label41" runat="server" CssClass="heading1" Text="Phone"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="vphonetb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label63" runat="server" CssClass="heading1" Text="Fax"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="sup_faxtb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label44" runat="server" CssClass="heading1" Text="Fax"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="vfaxtb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4"></td>
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
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right">
                            <asp:Button ID="updatebtn" runat="server" CssClass="pushbutton1 gold" Text="Update" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="polineID" DataSourceID="SqlLines" Width="100%" GridLines="None" ShowFooter="True" AllowSorting="True">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="polineID" InsertVisible="False" SortExpression="polineID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="polineIDlbl" runat="server" Text='<%# Bind("polineID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="partnumber">
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 15%; text-align: left;">Manufacturer</td>
                                                    <td style="width: 15%; text-align: left;">Part Number</td>
                                                    <td style="width: 10%; text-align: center;">OH/MIN/MAX</td>
                                                    <td style="width: 10%; text-align: center;">Qty</td>
                                                    <td style="width: 10%; text-align: center;">Received</td>
                                                    <td style="width: 10%; text-align: center;">Receive</td>
                                                    <td style="width: 10%; text-align: right;">Cost</td>
                                                    <td style="width: 10%; text-align: center;">UoM</td>
                                                    <td style="width: 10%; text-align: right;">Extended</td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 15%; text-align: left;"><asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label></td>
                                                    <td style="width: 15%; text-align: left;"><asp:LinkButton ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber") %>' CommandName="Detail" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" /></td>
                                                    <td style="width: 10%; text-align: center;">
                                                        <asp:Label ID="ohlbl" runat="server" Text='<%# appcode.GetOnHand(Eval("manufacturer"), Eval("partnumber"))%>'></asp:Label> / 
                                                        <asp:Label ID="Label5" runat="server" Text='<%# appcode.GetMin(Eval("manufacturer"), Eval("partnumber"))%>'></asp:Label> / 
                                                        <asp:Label ID="Label6" runat="server" Text='<%# appcode.GetMax(Eval("manufacturer"), Eval("partnumber"))%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center;"><asp:TextBox ID="qtytb" runat="server" Text='<%# Bind("quantity") %>' Width="40" Enabled='<%# Not appcode.IsPOSubmitted(Session("poID"))%>'></asp:TextBox></td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="receivedlbl" runat="server" Text='<%# appcode.GetReceivedPOLineQuantity(Session("poID"), Eval("manufacturer"), Eval("partnumber"))%>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: center;"><asp:TextBox ID="receivetb" runat="server" Text="0" Width="40" Enabled='<%# appcode.IsNotPOLineComplete(Session("poID"), Eval("manufacturer"), Eval("partnumber"))%>'></asp:TextBox></td>
                                                    <td style="width: 10%; text-align: right;"><asp:TextBox ID="costlbl" runat="server" Text='<%# FormatCurrency(Eval("cost"), 2)%>' Width="50"></asp:TextBox></td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="uomlbl" runat="server" Text='<%# Bind("uom") %>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="extendedlbl" runat="server" Text='<%# FormatCurrency(Eval("quantity") * Eval("cost"), 2)%>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# appcode.GetItemName(Eval("manufacturer"), Eval("partnumber"))%>'></asp:Label>-
                                                        <asp:Label ID="Label4" runat="server" Text='<%# appcode.GetPackage(Eval("manufacturer"), Eval("partnumber"))%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="Label3" runat="server" Text='<%# FormatCurrency(appcode.GetMSRP(Eval("manufacturer"), Eval("partnumber")), 2)%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: center"><asp:CheckBox ID="selectcb" runat="server" /></td>
                                                    <td style="text-align: right">
                                                        <asp:LinkButton ID="viewbtn" runat="server" Text="Delete" CommandName="Delete" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 15%; text-align: left;"></td>
                                                    <td style="width: 15%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"><asp:Label ID="Label2" runat="server" Text="Sub-Total" Font-Bold="True"></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="subtotallbl" runat="server" Font-Bold="True"></asp:Label></td>
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
            <td>
                <asp:Label ID="Label7" runat="server" Text="Notes" Font-Size="Medium" Font-Bold="True"></asp:Label>
            &nbsp;<asp:Button ID="addbtn" runat="server" Text="Add" CssClass="pushbutton1 gold" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="noteID" DataSourceID="SqlNotes" Width="100%" GridLines="None">
                    <Columns>
                        <asp:TemplateField HeaderText="noteID" SortExpression="noteID" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="noteIDlbl" runat="server" Text='<%# Eval("noteID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="date_entered" DataFormatString="{0:d}" HeaderText="Date" SortExpression="date_entered">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="author" HeaderText="Author" SortExpression="author">
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="note" HeaderText="Note" SortExpression="note">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="deletebtn" runat="server" Text="Delete" CommandName="Delete" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                            </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="10%" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: center;">
                <asp:Button ID="savebtn" runat="server" CssClass="pushbutton1 gold" Text="Save" />
                <asp:Button ID="emailbtn" runat="server" CssClass="pushbutton1 gold" Text="Submit" />
                <asp:Button ID="deletebtn" runat="server" CssClass="pushbutton1 gold" OnClientClick="return confirm('Delete this Note?');" Text="Delete" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <asp:SqlDataSource ID="SqlLines" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT polineID,manufacturer,partnumber,sum(quantity) as quantity,cost,uom FROM [t_poline] WHERE ([poID] = @poID) group by polineID,manufacturer,partnumber,cost,uom ORDER BY manufacturer, partnumber">
                    <SelectParameters>
                        <asp:SessionParameter Name="poID" SessionField="poID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlSuppliers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [companyID], [company] FROM [t_company] WHERE ([supplier] = @supplier) ORDER BY [company]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="True" Name="supplier" Type="Boolean" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlSupContacts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [userID], [name] FROM [t_user] WHERE ([companyID] = @companyID) ORDER BY [name]">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="supplierdd" Name="companyID" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlNotes" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_po_notes] WHERE ([poID] = @poID) ORDER BY [date_entered] DESC">
                    <SelectParameters>
                        <asp:SessionParameter Name="poID" SessionField="poID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

