<%@ Page Title="Product Detail" Language="VB" MasterPageFile="CustomerMaster.master" AutoEventWireup="false" CodeFile="CatalogPage.aspx.vb" Inherits="customer_CatalogPage" %>

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
                                        <asp:Label ID="Label8" runat="server" CssClass="heading1" Text="UOM"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="uomtb" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label52" runat="server" CssClass="heading1" Text="Core"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="corelbl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label5" runat="server" CssClass="heading1" Text="On Hand"></asp:Label></td>
                                    <td><asp:Label ID="stocklbl" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="2">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2"><asp:Label ID="Label51" runat="server" Font-Bold="True" Text=" Cross Reference"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlXref" EmptyDataText="Unknown" GridLines="None" Width="50%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Mfr" ShowHeader="False" SortExpression="xref_manufacturer">
                                                    <ItemTemplate>
                                                        <asp:Label ID="xmanufacturerlbl" runat="server" Text='<%# Bind("xref_manufacturer") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PN" ShowHeader="False" SortExpression="xref_partnumber">
                                                    <ItemTemplate>
                                                        <a href='CatalogPage.aspx?productID=<%# appcode.GetProductID(Eval("xref_manufacturer"), Eval("xref_partnumber")) %>'><asp:Label ID="xpartnumberlbl" runat="server" text='<%# Bind("xref_partnumber") %>'></asp:Label></a>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="On Hand">
                                                    <ItemTemplate>
                                                        <asp:Label ID="onhandlbl" runat="server" Text='<%# appcode.GetOnHand(Eval("xref_manufacturer"), Eval("xref_partnumber")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="xrefID" InsertVisible="False" SortExpression="xrefID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="xrefIDlbl" runat="server" Text='<%# Bind("xrefID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
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
                                        <asp:Panel ID="Panel2" runat="server" DefaultButton="addbtn">
                                            <p>
                                                <asp:TextBox ID="qtytb" runat="server" Width="40px"></asp:TextBox>
                                                <asp:Button ID="addbtn" runat="server" Text="Add to Cart" CssClass="pushbutton1 gold" />
                                            </p>
                                            <p style="text-align: center">
                                                <asp:Button ID="favoritebtn" runat="server" CssClass="pushbutton1 gold" Text="Add to Favorites" />
                                            </p>
                                            <p style="text-align: center">
                                                <asp:Button ID="warehousebtn" runat="server" CssClass="pushbutton1 gold" Text="Add to Stock Room" />
                                            </p>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="ORDER HISTORY" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlHistory" GridLines="None" Width="50%" AllowPaging="True" HorizontalAlign="Left" AllowSorting="True" EmptyDataText="No History">
                                <Columns>
                                    <asp:TemplateField HeaderText="Order ID" SortExpression="orderID">
                                        <ItemTemplate>
                                            <asp:Label ID="orderIDlbl" runat="server" Text='<%# Bind("orderID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="order_date" HeaderText="Date" SortExpression="order_date" DataFormatString="{0:d}" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="quantity" HeaderText="Qty" SortExpression="quantity" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="price" DataFormatString="{0:c}" HeaderText="Price" SortExpression="price">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="viewbtn" runat="server" Text="View" CommandName="View" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="purchasinglbl" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlHistory" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [orderID], [company], [order_date], [quantity], [price] FROM [v_order_lines] WHERE ([partnumber] = @partnumber) and companyID=@companyID ORDER BY [order_date] DESC, [company]">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="partnumbertb" Name="partnumber" PropertyName="Text" Type="String" />
                        <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:HiddenField ID="categoryIDlbl" runat="server" />
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlXref" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_xref] WHERE (([ref_manufacturer] = @ref_manufacturer) AND ([ref_partnumber] = @ref_partnumber)) ORDER BY [xref_manufacturer], [xref_partnumber]">
        <SelectParameters>
            <asp:ControlParameter ControlID="manufacturertb" Name="ref_manufacturer" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="partnumbertb" Name="ref_partnumber" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

