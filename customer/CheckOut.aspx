<%@ Page Title="Check Out" Language="VB" MasterPageFile="CustomerMaster.master" AutoEventWireup="false" CodeFile="CheckOut.aspx.vb" Inherits="customer_CheckOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label12" runat="server" CssClass="est_heading1" Text="Purchase Order"></asp:Label></td>
                        <td colspan="2">
                            <asp:TextBox ID="potb" runat="server" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label13" runat="server" CssClass="est_heading1" Text="Reference"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="referencetb" runat="server" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 30%">
                            <asp:Label ID="Label30" runat="server" CssClass="est_heading1" Text="Ship To"></asp:Label>
                                    </td>
                                    <td>
                            <asp:DropDownList ID="shiptodd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlShipTos" DataTextField="shipto" DataValueField="shipID">
                            </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>                            
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 15%;">
                            <asp:Label ID="Label29" runat="server" CssClass="est_heading1" Text="Bill To"></asp:Label>
                        </td>
                        <td style="vertical-align: top; width: 35%;">
                            <asp:Label ID="customerlbl" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                        <td style="vertical-align: top; width: 50%;" rowspan="7">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 30%">
                                        <asp:Label ID="Label31" runat="server" CssClass="heading1" Text="Ship To" Width="125px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="shiptotb" runat="server" Width="300"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" CssClass="heading1" Text="Address"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="saddress1tb" runat="server" Width="300"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:TextBox ID="saddress2tb" runat="server" Width="300"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:TextBox ID="saddress3tb" runat="server" Width="300"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" CssClass="heading1" Text="City"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="scitytb" runat="server" Width="300"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" CssClass="heading1" Text="State"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="sstatetb" runat="server" Width="300"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" CssClass="heading1" Text="Zip Code"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="szipcodetb" runat="server" Width="300"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                        <td style="vertical-align: top">
                            <asp:Label ID="billtolbl" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label11" runat="server" CssClass="est_heading1" Text="Contact"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:TextBox ID="contacttb" runat="server" Width="300"></asp:TextBox>
                            </td>
                    </tr>
                    <tr>    
                        <td>
                            <asp:Label ID="Label1" runat="server" CssClass="est_heading1" Text="Email"></asp:Label>
                        </td>
                        <td><asp:TextBox ID="emailtb" runat="server" Width="300"></asp:TextBox></td>
                    </tr>
                    <tr>    
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>    
                        <td>
                            <asp:Label ID="assetlbl1" runat="server" CssClass="est_heading1" Text="Asset ID"></asp:Label>
                        </td>
                        <td><asp:Label ID="assetlbl" runat="server"></asp:Label></td>
                    </tr>
                    <tr>    
                        <td>
                            <asp:Label ID="pm_kitlbl1" runat="server" CssClass="est_heading1" Text="Kit"></asp:Label>
                        </td>
                        <td><asp:Label ID="pm_kitlbl" runat="server"></asp:Label></td>
                    </tr>
                    <tr>    
                        <td>
                            <asp:Label ID="pm_intervallbl1" runat="server" CssClass="est_heading1" Text="Interval"></asp:Label>
                        </td>
                        <td><asp:Label ID="pm_intervallbl" runat="server"></asp:Label></td>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 30%"><asp:Label ID="Label16" runat="server" CssClass="est_heading1" Text="Carrier"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="shipmethoddd" runat="server">
                                            <asp:ListItem>Best Way</asp:ListItem>
                                            <asp:ListItem>Local Delivery</asp:ListItem>
                                            <asp:ListItem>UPS</asp:ListItem>
                                            <asp:ListItem>Fedex</asp:ListItem>
                                            <asp:ListItem>Freight</asp:ListItem>
                                            <asp:ListItem>Will Call</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:CheckBox ID="rushcb" runat="server" Text="Rush" />
                                    </td>
                                </tr>
                            </table>                            
                        </td>
                    </tr>
                    <tr>    
                        <td>
                            <asp:Label ID="pm_hourslbl" runat="server" CssClass="est_heading1" Text="Hours/Miles"></asp:Label>
                        </td>
                        <td><asp:TextBox ID="pm_hourstb" runat="server">0</asp:TextBox></td>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 30%"><asp:Label ID="Label17" runat="server" CssClass="est_heading1" Text="Options"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="shipoptionsdd" runat="server">
                                            <asp:ListItem>Ship &amp; BO</asp:ListItem>
                                            <asp:ListItem>Ship Complete</asp:ListItem>
                                            <asp:ListItem>Ship &amp; Cancel BO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="cartID" DataSourceID="SqlCart" Width="100%" GridLines="None" ShowFooter="True">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="cartID" InsertVisible="False" SortExpression="cartID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="cartIDlbl" runat="server" Text='<%# Bind("cartID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="productID" SortExpression="productID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="productIDlbl" runat="server" Text='<%# Bind("productID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 35%; text-align: left;">Part Number</td>
                                                    <td style="width: 10%; text-align: center;">Qty</td>
                                                    <td style="width: 10%; text-align: center;">UoM</td>
                                                    <td style="width: 15%; text-align: right;">Price</td>
                                                    <td style="width: 10%; text-align: right;">Extended</td>
                                                    <td style="width: 20%; text-align: right;"></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 35%; text-align: left;">
                                                        <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer")%>'></asp:Label>&nbsp;
                                                        <asp:Label ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label>
                                                        <asp:Label ID="Label14" runat="server" Text='<%# appcode.GetItemKitID(Eval("cartID"))%>'></asp:Label>
                                                        <asp:Label ID="kitIDlbl" runat="server" Text='<%# Eval("kitID")%>' Visible="False"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="qtytb" runat="server" Text='<%# Bind("quantity") %>' Width="40"></asp:Label></td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="uomlbl" runat="server" Text='<%# Bind("uom") %>'></asp:Label></td>
                                                    <td style="width: 15%; text-align: right;"><asp:Label ID="pricelbl" runat="server" Text='<%# FormatCurrency(Eval("price"), 2)%>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="extendedlbl" runat="server" Text='<%# FormatCurrency(Eval("extended", "{0:c}"), 2)%>'></asp:Label></td>
                                                    <td style="width: 20%; text-align: right;"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:Label ID="itemnamelbl" runat="server" Text='<%# Bind("item") %>'></asp:Label>
                                                        <asp:Label ID="corelbl" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="X-Small"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 35%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 15%; text-align: left;"><asp:Label ID="Label2" runat="server" Text="Sub-Total" Font-Bold="True"></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="subtotallbl" runat="server" Font-Bold="True"></asp:Label></td>
                                                    <td style="width: 20%; text-align: center;"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 35%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 15%; text-align: left;"><asp:Label ID="Label3" runat="server" Text="Sales Tax" Font-Bold="True"></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="salestaxlbl" runat="server" Font-Bold="True"></asp:Label></td>
                                                    <td style="width: 20%; text-align: center;"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 35%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"></td>
                                                    <td style="width: 15%; text-align: left;"><asp:Label ID="Label4" runat="server" Text="Total" Font-Bold="True"></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="grandtotallbl" runat="server" Font-Bold="True"></asp:Label></td>
                                                    <td style="width: 20%; text-align: center;"></td>
                                                </tr>
                                            </table>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="Label6" runat="server" CssClass="est_heading1" Text="Special Instructions"></asp:Label><br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:TextBox ID="notestb" runat="server" CssClass="est_textbox1" Rows="5" TextMode="MultiLine" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="3"><asp:Button ID="submitbtn" runat="server" Text="Place Order" CssClass="pushbutton1 gold" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlCart" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT *,price * quantity as extended FROM t_cart WHERE companyID=@companyID and userID=@userID ORDER BY cartID">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
            <asp:SessionParameter Name="vendorID" SessionField="vendorID" Type="Int32" />
            <asp:SessionParameter Name="userID" SessionField="userID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlVendors" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [vendorID], [company] FROM [v_company_vendor] WHERE ([companyID] = @companyID)">
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
</asp:Content>

