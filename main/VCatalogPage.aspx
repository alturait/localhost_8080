<%@ Page Title="Product Detail" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="VCatalogPage.aspx.vb" Inherits="main_VCatalogPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="pagetable">
        <tr>
            <td class="pagebody" style="padding: 10px; vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                        <asp:Button ID="editproductbtn" runat="server" Text="Edit" CssClass="pushbutton1 gold" />
                                        <asp:Button ID="backbtn" runat="server" Text="Back" CssClass="pushbutton1 gold" />
                                    </td>
                                    <td style="text-align: right">
                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 10px; vertical-align: top; padding-top: 10px; padding-bottom: 10px;">
                            <table style="width: 100%">
                                <tr>
                                    <td style="padding-top: 10px; padding-bottom: 10px;" colspan="2">
                                        <asp:Label ID="pnlbl" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
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
                                        <asp:Label ID="msrpgplbl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" CssClass="heading1" Text="Sale Price"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="saleprice2lbl" runat="server"></asp:Label>
                                        <asp:Label ID="salegplbl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" CssClass="heading1" Text="Cost"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="currentcostlbl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label7" runat="server" CssClass="heading1" Text="Core"></asp:Label></td>
                                    <td><asp:Label ID="corelbl" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label5" runat="server" CssClass="heading1" Text="On Hand"></asp:Label></td>
                                    <td><asp:Label ID="stocklbl" runat="server"></asp:Label>&nbsp;<asp:Label ID="snslbl" runat="server" ForeColor="Red"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label49" runat="server" CssClass="heading1" Text="Min"></asp:Label></td>
                                    <td><asp:Label ID="minlbl" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label50" runat="server" CssClass="heading1" Text="Max"></asp:Label></td>
                                    <td><asp:Label ID="maxlbl" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="2">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label51" runat="server" Font-Bold="True" Text=" Cross Reference"></asp:Label>
                                        &nbsp;<asp:LinkButton ID="xrefbtn" runat="server" ForeColor="Red">Add</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
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
                                                        <a href='VCatalogPage.aspx?productID=<%# appcode.GetProductID(Eval("xref_manufacturer"), Eval("xref_partnumber")) %>'><asp:Label ID="xpartnumberlbl" runat="server" text='<%# Bind("xref_partnumber") %>'></asp:Label></a>
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
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="deletebtn" runat="server" Text="Remove" CommandName="Remove" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" ForeColor="Red" />
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
                                        <asp:Label ID="Label48" runat="server" Text="Customer Price: " Font-Bold="True" Font-Size="Medium"></asp:Label>
                                        <asp:Label ID="salepricelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                        <asp:Label ID="salegp2lbl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center" colspan="2">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <p>
                                                <asp:TextBox ID="qtytb" runat="server" Width="40px"></asp:TextBox>
                                                <asp:Panel ID="Panel4" runat="server">
                                                    <asp:Button ID="addbtn" runat="server" Text="Add to Cart" CssClass="pushbutton1 gold" />
                                                    <p style="text-align: center">
                                                        <asp:Button ID="favoritebtn" runat="server" CssClass="pushbutton1 gold" Text="Add to Favorites" />
                                                    </p>
                                                    <p style="text-align: center">
                                                        <asp:Button ID="warehousebtn" runat="server" CssClass="pushbutton1 gold" Text="Add to Stock Room" />
                                                    </p>
                                                </asp:Panel>
                                                <p>
                                                </p>
                                                <asp:Panel ID="Panel5" runat="server">
                                                    <p>
                                                        <asp:DropDownList ID="podd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlPOs" DataTextField="po" DataValueField="poID" Height="16px">
                                                            <asp:ListItem Value="0">New PO</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Button ID="addPObtn" runat="server" CssClass="pushbutton1 gold" Text="Add to PO" />
                                                    </p>
                                                </asp:Panel>
                                                <p>
                                                </p>
                                                <p>
                                                </p>
                                                <p>
                                                </p>
                                                <p>
                                                </p>
                                                <p>
                                                </p>
                                                <p>
                                                </p>
                                                <p>
                                                </p>
                                                <p>
                                                </p>
                                                <p>
                                                </p>
                                                <p>
                                                </p>
                                                <p>
                                                </p>
                                                <p>
                                                </p>
                                            </p>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%">
                            <asp:Label ID="Label3" runat="server" Text="ORDER HISTORY" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="ON PO" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%; vertical-align: top;">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlHistory" GridLines="None" Width="90%" AllowPaging="True" HorizontalAlign="Left">
                                <Columns>
                                    <asp:BoundField DataField="company" HeaderText="Customer" SortExpression="company" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Order ID" SortExpression="orderID">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="orderIDlbl" runat="server" Text='<%# Eval("orderID")%>' CommandName="Detail" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="shipment_date" HeaderText="Date" SortExpression="shipment_date" DataFormatString="{0:d}" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="quantity" HeaderText="Ord" SortExpression="quantity" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ship_qty" HeaderText="Ship" SortExpression="ship_qty" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="price" DataFormatString="{0:c}" HeaderText="Price" SortExpression="price">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td style="vertical-align: top">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="poID" DataSourceID="SqlOnPO" GridLines="None" Width="100%" AllowPaging="True">
                                <Columns>
                                    <asp:BoundField DataField="supplier" HeaderText="Supplier" SortExpression="supplier" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="PO" SortExpression="poID">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="poIDlbl" runat="server" Text='<%# Bind("poID")%>' CommandName="Detail" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="quantity" HeaderText="Ord" SortExpression="quantity" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Received">
                                        <ItemTemplate>
                                            <asp:Label ID="receivedlbl" runat="server" Text='<%#appcode.GetReceivedPOLineQuantity(Eval("poID"), Eval("manufacturer"), Eval("partnumber")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="cost" DataFormatString="{0:c}" HeaderText="Cost" SortExpression="cost">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="categoryIDlbl" runat="server" />
                <asp:SqlDataSource ID="SqlHistory" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [orderID], [company], [shipment_date], [quantity], [ship_qty], [price] FROM [v_order_lines] WHERE ([partnumber] = @partnumber) ORDER BY [order_date] DESC, [company]">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="partnumbertb" Name="partnumber" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlOnPO" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [v_poline] WHERE ([manufacturer] = @manufacturer) and partnumber=@partnumber ORDER BY [poID] DESC">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="manufacturertb" Name="manufacturer" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="partnumbertb" Name="partnumber" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:HiddenField ID="costlbl" runat="server" />
                <asp:SqlDataSource ID="SqlPOs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [poID], [supplier] + ' - ' + po as po FROM [t_po] where complete='False'"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlXref" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_xref] WHERE (([ref_manufacturer] = @ref_manufacturer) AND ([ref_partnumber] = @ref_partnumber)) ORDER BY [xref_manufacturer], [xref_partnumber]">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="manufacturertb" Name="ref_manufacturer" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="partnumbertb" Name="ref_partnumber" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

