<%@ Page Title="Product Detail" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="CatalogPage.aspx.vb" Inherits="CatalogPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="pagetable">
        <tr>
            <td class="pagebody" style="padding: 10px; vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td style="padding: 10px; vertical-align: top; padding-top: 10px; padding-bottom: 10px;">
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2" style="padding-top: 10px; padding-bottom: 10px;">
                                        <asp:Label ID="pnlbl" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                                        <asp:LinkButton ID="backbtn" runat="server" CssClass="push_button1 orange" Text="Back" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-top: 10px; padding-bottom: 10px;">
                                        <asp:Label ID="descriptiontb" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        <asp:Label ID="Label47" runat="server" CssClass="heading1" Text="Manufacturer"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="manufacturertb" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" CssClass="heading1" Text="Part Number"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="partnumbertb" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label44" runat="server" CssClass="heading1" Text="UPC Code"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="upctb" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label42" runat="server" CssClass="heading1" Text="Item"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="itemtb" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label43" runat="server" CssClass="heading1" Text="Package"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="packagetb" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" CssClass="heading1" Text="Weight (lbs)"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="weighttb" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" CssClass="heading1" Text="UOM"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="uomtb" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <asp:Panel ID="Panel1" runat="server">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label46" runat="server" CssClass="heading1" Text="Category"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="categorytb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label40" runat="server" CssClass="heading1" Text="MSRP"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="msrptb" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td style="border: medium inset #FF9900; padding: 10px; text-align: center; vertical-align: top; width: 30%;">
                            <table align="center">
                                <tr>
                                    <td colspan="2">
                                        <asp:Image ID="productImage" runat="server" AlternateText="" CssClass="est_picture" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label48" runat="server" Text="Your Price: " Font-Bold="True" Font-Size="Medium"></asp:Label>
                                        <asp:Label ID="salepricelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center" colspan="2">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <p>
                                                <asp:TextBox ID="qtytb" runat="server" Width="40px"></asp:TextBox>
                                                <asp:Button ID="addbtn" runat="server" Text="Add to Cart" CssClass="pushbutton1 gold" />
                                            </p>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
                <asp:HiddenField ID="categoryIDlbl" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

