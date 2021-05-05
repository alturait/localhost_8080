<%@ Page Title="Sales YTD" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="SalesYTD.aspx.vb" Inherits="main_SalesYTD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>&nbsp; 
                            <asp:DropDownList ID="yeardd" runat="server" AutoPostBack="True">
                                <asp:ListItem>2015</asp:ListItem>
                                <asp:ListItem>2016</asp:ListItem>
                                <asp:ListItem>2017</asp:ListItem>
                                <asp:ListItem>2018</asp:ListItem>
                                <asp:ListItem>2019</asp:ListItem>
                                <asp:ListItem>2020</asp:ListItem>
                                <asp:ListItem>2021</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="companyIDlbl" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="vertical-align: top; width: 25%;">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text="Total Sales" CssClass="est_heading1"></asp:Label>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="totalsaleslbl" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label7" runat="server" Text="# of Orders" CssClass="est_heading1"></asp:Label>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="totalorderslbl" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label199" runat="server" Text="# of Kits" CssClass="est_heading1"></asp:Label>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="totalkitslbl" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            </table>
                                    </td>
                                    <td style="vertical-align: top">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlMonthlySales" AllowSorting="True" GridLines="None" CellPadding="4" HorizontalAlign="Center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Month" SortExpression="order_month">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# MonthName(Eval("order_month"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="sales" HeaderText="Sales" SortExpression="sales" DataFormatString="{0:c}" >
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                    <td style="vertical-align: top">
                                        &nbsp;</td>
                                    <td style="vertical-align: top">
                                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlTopMfrs" AllowSorting="True" GridLines="None" CellPadding="4" HorizontalAlign="Center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Top 10 Mfrs" SortExpression="manufacturer">
                                                    <ItemTemplate>
                                                        <a href='SalesYTDbymfr.aspx?mfr=<%# Eval("manufacturer")%>'><asp:Label ID="Label1" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label></a>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="sales" HeaderText="Sales" SortExpression="sales" DataFormatString="{0:c}" >
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlMonthlySales" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT MONTH([shipment_date]) as order_month, SUM([ship_qty] * [price]) as sales FROM [v_order_lines] WHERE ([companyID] = @companyID) and YEAR(shipment_date)=@order_year AND isReturn='No' GROUP BY MONTH(shipment_date) ORDER BY MONTH(shipment_date)">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
            <asp:ControlParameter ControlID="yeardd" Name="order_year" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlAllMonthlySales" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT MONTH([shipment_date]) as order_month, SUM([ship_qty] * [price]) as sales FROM [v_order_lines] WHERE YEAR(shipment_date)=@order_year AND isReturn='No' GROUP BY MONTH(shipment_date) ORDER BY MONTH(shipment_date)">
        <SelectParameters>
            <asp:ControlParameter ControlID="yeardd" Name="order_year" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlTopMfrs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT TOP(10) [manufacturer], sum([quantity] * [price]) as sales FROM [v_order_lines] WHERE orderID in (select t_order.orderID from t_order where ([companyID] = @companyID) and YEAR(shipment_date)=@order_year) AND isReturn='No' GROUP BY manufacturer ORDER BY [sales] DESC">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
            <asp:ControlParameter ControlID="yeardd" Name="order_year" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlAllTopMfrs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT TOP(10) [manufacturer], sum([quantity] * [price]) as sales FROM [v_order_lines] WHERE orderID in (select t_order.orderID from t_order where YEAR(shipment_date)=@order_year) AND isReturn='No' GROUP BY manufacturer ORDER BY [sales] DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="yeardd" Name="order_year" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    </asp:Content>

