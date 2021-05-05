<%@ Page Title="Customer Usage" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="CustomerUsage.aspx.vb" Inherits="main_CustomerUsage" %>

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
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlAllCustomerUsage" Width="100%" AllowSorting="True" GridLines="None" HorizontalAlign="Center" Style="padding: 10px" EmptyDataText="No Results Found">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <EmptyDataRowStyle Font-Bold="True" HorizontalAlign="Center" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:BoundField DataField="companyID" Visible="False"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Company" SortExpression="company">
                                        <ItemTemplate>
                                            <asp:Label ID="companylbl" runat="server" Text='<%# Bind("company") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="manufacturer" HeaderText="Manufacturer" SortExpression="manufacturer">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="partnumber" HeaderText="Part Number" SortExpression="partnumber">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Quantity" SortExpression="quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("quantity")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlCustomerUsage" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [companyID], [company], [manufacturer], [partnumber], SUM([quantity]) as quantity FROM [v_order_lines] WHERE ([companyID]=@companyID) AND ([partnumber] = @searchterm) GROUP BY [companyID], [company], [manufacturer], [partnumber] ORDER BY quantity desc">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
            <asp:QueryStringParameter Name="searchterm" QueryStringField="search" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlAllCustomerUsage" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [companyID], [company], [manufacturer],[partnumber], SUM([quantity]) as quantity FROM [v_order_lines] WHERE ([partnumber] = @searchterm) GROUP BY [companyID], [company], [manufacturer], [partnumber] ORDER BY quantity desc">
        <SelectParameters>
            <asp:QueryStringParameter Name="searchterm" QueryStringField="search" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

