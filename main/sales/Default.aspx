<%@ Page Title="Sales Home Page" Language="VB" MasterPageFile="Sales.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="LT_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="~/App_Themes/appcode/StyleSheet.css" rel="stylesheet" />
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
                            </asp:DropDownList>
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
                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlTopItems" AllowSorting="True" GridLines="None" CellPadding="4" HorizontalAlign="Center">
                                            <Columns>
                                                <asp:BoundField DataField="partnumber" HeaderText="Top 10 Items" SortExpression="partnumber" >
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="sales" HeaderText="Sales" SortExpression="sales" DataFormatString="{0:c}" >
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                    <td style="vertical-align: top">
                                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlTopMfrs" AllowSorting="True" GridLines="None" CellPadding="4" HorizontalAlign="Center">
                                            <Columns>
                                                <asp:BoundField DataField="manufacturer" HeaderText="Top 10 Mfrs" SortExpression="manufacturer" >
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
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
    <asp:SqlDataSource ID="SqlMonthlySales" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT MONTH([order_date]) as order_month, SUM([quantity] * [price]) as sales FROM [v_order_lines] WHERE ([companyID] = @companyID) and YEAR(order_date)=@order_year GROUP BY MONTH(order_date) ORDER BY MONTH(order_date)">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
            <asp:ControlParameter ControlID="yeardd" Name="order_year" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlAllMonthlySales" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT MONTH([order_date]) as order_month, SUM([quantity] * [price]) as sales FROM [v_order_lines] WHERE ([repID] = @repID) and YEAR(order_date)=@order_year GROUP BY MONTH(order_date) ORDER BY MONTH(order_date)">
        <SelectParameters>
            <asp:SessionParameter Name="repID" SessionField="userID" Type="Int32" />
            <asp:ControlParameter ControlID="yeardd" Name="order_year" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlTopMfrs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT TOP(10) [manufacturer], sum([quantity] * [price]) as sales FROM [v_order_lines] WHERE ([companyID] = @companyID) and YEAR(order_date)=@order_year GROUP BY manufacturer ORDER BY [sales] DESC">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
            <asp:ControlParameter ControlID="yeardd" Name="order_year" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlAllTopMfrs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT TOP(10) [manufacturer], sum([quantity] * [price]) as sales FROM [v_order_lines] WHERE ([repID] = @repID) and YEAR(order_date)=@order_year GROUP BY manufacturer ORDER BY [sales] DESC">
        <SelectParameters>
            <asp:SessionParameter Name="repID" SessionField="userID" Type="Int32" />
            <asp:ControlParameter ControlID="yeardd" Name="order_year" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlTopItems" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT TOP(10) [partnumber], sum([quantity] * [price]) as sales FROM [v_order_lines] WHERE ([companyID] = @companyID) and YEAR(order_date)=@order_year GROUP BY partnumber ORDER BY [sales] DESC">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
            <asp:ControlParameter ControlID="yeardd" Name="order_year" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlAllTopItems" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT TOP(10) [partnumber], sum([quantity] * [price]) as sales FROM [v_order_lines] WHERE ([repID] = @repID) and YEAR(order_date)=@order_year GROUP BY partnumber ORDER BY [sales] DESC">
        <SelectParameters>
            <asp:SessionParameter Name="repID" SessionField="userID" Type="Int32" />
            <asp:ControlParameter ControlID="yeardd" Name="order_year" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    </asp:Content>

