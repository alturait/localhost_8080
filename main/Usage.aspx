<%@ Page Title="Usage" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="Usage.aspx.vb" Inherits="main_Usage" %>

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
                        <td style="text-align: right">
                            <asp:DropDownList ID="mfrdd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlManufacturers" DataTextField="manufacturer" DataValueField="manufacturer" style="height: 22px">
                                <asp:ListItem Value="0">Select Manufacturer</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlCustomerUsage" GridLines="None" Width="100%" ShowFooter="True" AllowSorting="True" AllowCustomPaging="True" PageSize="15">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="productIDlbl" runat="server" Text='<%# appcode.GetProductID(Eval("manufacturer"), Eval("partnumber"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Manufacturer" SortExpression="manufacturer">
                                        <ItemTemplate>
                                            <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Part Number" SortExpression="partnumber">
                                        <ItemTemplate>
                                            <asp:Label ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item" SortExpression="item">
                                        <ItemTemplate>
                                            <asp:Label ID="itemlbl" runat="server" Text='<%# Bind("item") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="quantity" HeaderText="Qty" SortExpression="quantity">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="UoM" SortExpression="uom">
                                        <ItemTemplate>
                                            <asp:Label ID="uomlbl" runat="server" Text='<%# Bind("uom") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlCustomerUsage" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT vendorID, manufacturer, partnumber, item, uom, sum(quantity) as quantity FROM [v_order_lines] WHERE ([vendorID] = @vendorID) and companyID=@companyID GROUP BY vendorID, manufacturer, partnumber, item, uom ORDER BY [manufacturer], [partnumber]">
        <SelectParameters>
            <asp:SessionParameter Name="vendorID" SessionField="vendorID" Type="Int32" />
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCustomerUsageByMfr" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT vendorID, manufacturer, partnumber, item, uom, sum(quantity) as quantity FROM [v_order_lines] WHERE ([vendorID] = @vendorID) and companyID=@companyID and manufacturer=@manufacturer GROUP BY vendorID, manufacturer, partnumber, item, uom ORDER BY [manufacturer], [partnumber]">
        <SelectParameters>
            <asp:SessionParameter Name="vendorID" SessionField="vendorID" Type="Int32" />
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
            <asp:QueryStringParameter Name="manufacturer" QueryStringField="manufacturer" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlPOs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [poID], [supplier] + ' - ' + po as po FROM [t_po]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlManufacturers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer] FROM [t_product] GROUP BY manufacturer ORDER BY [manufacturer]">
    </asp:SqlDataSource>
</asp:Content>

