<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="GuestCart.aspx.vb" Inherits="GuestCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align: top">                            
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Button ID="submitbtn" runat="server" Text="Check Out" CssClass="pushbutton1 gold" />
                                        <asp:Button ID="cancelbtn" runat="server" Text="Empty Cart" CssClass="pushbutton1 gold" OnClientClick="return confirm('Empty Cart?');" />
                                        <asp:Button ID="updatebtn1" runat="server" Text="Update All" CssClass="pushbutton1 gold" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label1" runat="server" Text="Lines" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
                                    <td style="text-align: right">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="cartID" DataSourceID="SqlGuestCart" Width="100%" GridLines="None" ShowFooter="True">
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
                                        <asp:SqlDataSource ID="SqlGuestCart" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT *,quantity*price as extended FROM [t_cart] WHERE ([ssID] = @ssID)">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="ssID" SessionField="ssID" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                                </table>
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
    </table>
    </asp:Content>

