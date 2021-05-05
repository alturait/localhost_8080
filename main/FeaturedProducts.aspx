<%@ Page Title="Featured Products" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="FeaturedProducts.aspx.vb" Inherits="main_FeaturedProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                        <td style="text-align: right; padding-bottom: 10px">
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">

                            <asp:DataList ID="DataList1" runat="server" DataKeyField="productID" DataSourceID="SqlFeaturedProducts">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 400px; text-align: center">
                                                <a href="VCatalogPage.aspx?productID=<%# Eval("productID")%>">
                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# GetPicture(Eval("partnumber"))%>' Width="300" />
                                                </a>
                                            </td>
                                            <td vertical-align: top">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="ITEM: " Font-Size="Large" Font-Bold="True"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="manufacturerLabel" runat="server" Font-Size="Large" Text='<%# Eval("manufacturer") %>' />&nbsp;<asp:Label ID="partnumberLabel" runat="server" Font-Size="Large" Text='<%# Eval("partnumber") %>' />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="DESCRIPTION: " Font-Size="Large" Font-Bold="True"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="itemLabel" runat="server" Font-Size="Large" Text='<%# Eval("item") %>' />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="SALE PRICE: " Font-Size="Large" Font-Bold="True"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="salepriceLabel" runat="server" Font-Size="Large" Text='<%# FormatCurrency(Eval("saleprice"), 2)%>' />&nbsp;<asp:Label ID="uomLabel" runat="server" Text='<%# Eval("uom") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>                                    
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:SqlDataSource ID="SqlFeaturedProducts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_product] WHERE ([featured] = @featured) order by manufacturer, partnumber">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="True" Name="featured" Type="Boolean" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

