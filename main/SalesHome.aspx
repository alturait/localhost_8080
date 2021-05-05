<%@ Page Title="Sales" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="SalesHome.aspx.vb" Inherits="main_SalesHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            
                        &nbsp;<asp:DropDownList ID="monthdd" runat="server" AutoPostBack="True">
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="yeardd" runat="server" AutoPostBack="True">
                                            <asp:ListItem>2015</asp:ListItem>
                                            <asp:ListItem>2016</asp:ListItem>
                                            <asp:ListItem>2017</asp:ListItem>
                                            <asp:ListItem>2018</asp:ListItem>
                                            <asp:ListItem>2019</asp:ListItem>
                                            <asp:ListItem>2020</asp:ListItem>
                                            <asp:ListItem>2021</asp:ListItem>
                                        </asp:DropDownList>
                           
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%; vertical-align: top;">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Shipments" CssClass="est_heading1"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="totalsaleslbl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label202" runat="server" Text="Sales" CssClass="est_heading1"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="torderslbl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="# of Shipments" CssClass="est_heading1"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="totalshipmentslbl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label203" runat="server" Text="# of Orders" CssClass="est_heading1"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="totalorderslbl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label1" runat="server" Text="New Orders" CssClass="est_heading1"></asp:Label></td>
                                    <td style="text-align: right"><asp:Label ID="neworderslbl" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label199" runat="server" Text="Open Orders" CssClass="est_heading1"></asp:Label></td>
                                    <td style="text-align: right" class="auto-style1"><asp:Label ID="openorderslbl" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label200" runat="server" Text="Back Orders" CssClass="est_heading1"></asp:Label></td>
                                    <td style="text-align: right" class="auto-style1"><asp:Label ID="backorderslbl" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label201" runat="server" Text="Needs PO" CssClass="est_heading1"></asp:Label></td>
                                    <td style="text-align: right" class="auto-style1"><asp:Label ID="needspolbl" runat="server"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                        <td style="vertical-align: top">
                            
                                        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataSourceID="SqlTopItemsMonthly" AllowSorting="True" GridLines="None" CellPadding="4" HorizontalAlign="Center">
                                            <Columns>
                                                <asp:BoundField DataField="partnumber" HeaderText="Top 10 Items" SortExpression="partnumber" >
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="sales" HeaderText="Shipments" SortExpression="sales" DataFormatString="{0:c}" >
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                            
                        </td>
                        <td style="vertical-align: top">
                            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" DataSourceID="SqlTopMfrsMonthly" AllowSorting="True" GridLines="None" CellPadding="4" HorizontalAlign="Center">
                                <Columns>
                                    <asp:BoundField DataField="manufacturer" HeaderText="Top 10 Mfrs" SortExpression="manufacturer" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="sales" HeaderText="Shipments" SortExpression="sales" DataFormatString="{0:c}" >
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
    <asp:SqlDataSource ID="SqlTopMfrsMonthly" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT TOP(10) [manufacturer], sum([quantity] * [price]) as sales FROM [v_order_lines] WHERE orderID in (select t_order.orderID from t_order where MONTH(shipment_date)=@tmonth and YEAR(shipment_date)=@tyear) AND isReturn='No' GROUP BY manufacturer ORDER BY [sales] DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="monthdd" Name="tmonth" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="yeardd" Name="tyear" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlTopItemsMonthly" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT TOP(10) partnumber, sum(quantity * price) as sales FROM v_order_lines WHERE orderID in (select t_order.orderID from t_order where MONTH(shipment_date)=@tmonth and YEAR(shipment_date)=@tyear) AND isReturn='No' GROUP BY partnumber ORDER BY sales DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="monthdd" Name="tmonth" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="yeardd" Name="tyear" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

