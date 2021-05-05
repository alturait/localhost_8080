<%@ Page Title="Order Kit" Language="VB" MasterPageFile="~/main/Anonymous.master" AutoEventWireup="false" CodeFile="Kit.aspx.vb" Inherits="main_Kit" %>

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
                        </td>
                        <td style="text-align: right" colspan="2">
                            <asp:Button ID="submitbtn" runat="server" Text="Check Out" CssClass="pushbutton1 gold" />
                            <asp:Button ID="quotebtn" runat="server" Text="Create Quote" CssClass="pushbutton1 gold" />
                            <asp:Button ID="cancelbtn" runat="server" Text="Cancel" CssClass="pushbutton1 gold" OnClientClick="return confirm('Empty Cart?');" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" CssClass="est_heading1" Text="Kit ID"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="kitIDlbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label35" runat="server" CssClass="est_heading1" Text="Vendor"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="vendorlbl" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label41" runat="server" CssClass="est_heading1" Text="Phone"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="vphonelbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td rowspan="3" style="vertical-align: top">
                            <asp:Label ID="remittolbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label44" runat="server" CssClass="est_heading1" Text="Fax"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="vfaxlbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            </td>
                        <td>
                            </td>
                        <td>
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
                            <asp:Label ID="Label29" runat="server" CssClass="est_heading1" Text="Bill To"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="customerlbl" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label30" runat="server" CssClass="est_heading1" Text="Ship To"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:DropDownList ID="shiptodd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlShipTos" DataTextField="shipto" DataValueField="shipID">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>    
                        <td>
                            &nbsp;</td>
                        <td style="vertical-align: top">
                            <asp:Label ID="billtolbl" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                        <td>
                            &nbsp;</td>
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
                            <asp:Label ID="Label7" runat="server" CssClass="est_heading1" Text="Contact"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="contactdd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlContacts" DataTextField="name" DataValueField="userID">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label34" runat="server" CssClass="est_heading1" Text="Phone"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="phonelbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2"><asp:Label ID="Label1" runat="server" Text="Lines" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
                        <td colspan="2" style="text-align: right">
                            <asp:Button ID="updatebtn1" runat="server" Text="Update All" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
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
                                                    <td style="width: 20%; text-align: left;">Manufacturer</td>
                                                    <td style="width: 20%; text-align: left;">Part Number</td>
                                                    <td style="width: 5%; text-align: center;">Qty</td>
                                                    <td style="width: 15%; text-align: right;">Price</td>
                                                    <td style="width: 10%; text-align: center;">UoM</td>
                                                    <td style="width: 10%; text-align: right;">Extended</td>
                                                    <td style="width: 20%; text-align: right;"></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 20%; text-align: left;"><asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label></td>
                                                    <td style="width: 20%; text-align: left;"><asp:Label ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label></td>
                                                    <td style="width: 5%; text-align: center;"><asp:TextBox ID="qtytb" runat="server" Text='<%# Bind("quantity") %>' Width="40"></asp:TextBox></td>
                                                    <td style="width: 15%; text-align: right;"><asp:Label ID="pricelbl" runat="server" Text='<%# FormatCurrency(Eval("price"), 2)%>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: center;"><asp:Label ID="uomlbl" runat="server" Text='<%# Bind("uom") %>'></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="extendedlbl" runat="server" Text='<%# FormatCurrency(Eval("extended", "{0:c}"), 2)%>'></asp:Label></td>
                                                    <td style="width: 20%; text-align: right;">
                                                        <asp:LinkButton ID="updatebtn" runat="server" Text="Update" CommandName="Update" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                                        <asp:LinkButton ID="deletebtn" runat="server" Text="Remove" CommandName="Remove" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7">
                                                        <asp:Label ID="itemnamelbl" runat="server" Text='<%# Bind("item") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 20%; text-align: left;"></td>
                                                    <td style="width: 20%; text-align: left;"></td>
                                                    <td style="width: 5%; text-align: left;"></td>
                                                    <td style="width: 15%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"><asp:Label ID="Label2" runat="server" Text="Sub-Total" Font-Bold="True"></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="subtotallbl" runat="server" Font-Bold="True"></asp:Label></td>
                                                    <td style="width: 20%; text-align: center;"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; text-align: left;"></td>
                                                    <td style="width: 20%; text-align: left;"></td>
                                                    <td style="width: 5%; text-align: left;"></td>
                                                    <td style="width: 15%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"><asp:Label ID="Label3" runat="server" Text="Sales Tax" Font-Bold="True"></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="salestaxlbl" runat="server" Font-Bold="True"></asp:Label></td>
                                                    <td style="width: 20%; text-align: center;"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; text-align: left;"></td>
                                                    <td style="width: 20%; text-align: left;"></td>
                                                    <td style="width: 5%; text-align: left;"></td>
                                                    <td style="width: 15%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left;"><asp:Label ID="Label4" runat="server" Text="Total" Font-Bold="True"></asp:Label></td>
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
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlCart" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT *,price * quantity as extended FROM t_cart WHERE companyID=@companyID and vendorID=@vendorID and userID=@userID and kitID=@kitID ORDER BY cartID">
        <SelectParameters>
            <asp:SessionParameter Name="userID" SessionField="userID" Type="Int32" />
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
            <asp:SessionParameter Name="vendorID" SessionField="vendorID" Type="Int32" />
            <asp:SessionParameter Name="kitID" SessionField="serviceprofileID" Type="Int32" />
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

