<%@ Page Title="Needs PO" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="NeedsPO.aspx.vb" Inherits="main_NeedsPO" %>

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
                        <td>
                            <asp:DropDownList ID="customerdd" runat="server" DataSourceID="SqlCustomers" DataTextField="company" DataValueField="companyID" AppendDataBoundItems="True" AutoPostBack="True">
                                <asp:ListItem Value="0">Select Company</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="contactdd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlContacts" DataTextField="name" DataValueField="userID">
                                <asp:ListItem Value="0">Select Contact</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="sendbtn" runat="server" Text="Send PO Requests" />
                            <asp:Button ID="updatebtn" runat="server" Text="Update List" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="orderID" DataSourceID="SqlOrdersByCompany" GridLines="None" Width="100%" ShowFooter="True" AllowSorting="True">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Order ID" InsertVisible="False" SortExpression="orderID">
                                        <ItemTemplate>
                                            <asp:Label ID="orderIDlbl" runat="server" Text='<%# Bind("orderID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="invoiceID" HeaderText="Invoice" SortExpression="invoiceID">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="company" HeaderText="Customer" SortExpression="company">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="purchaseorder" HeaderText="Purchase Order" SortExpression="purchaseorder" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="order_date" HeaderText="Ordered On" SortExpression="order_date" DataFormatString="{0:d}" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Sub-Total">
                                        <ItemTemplate>
                                            <asp:Label ID="subtotallbl" runat="server" Text='<%# FormatCurrency(appcode.GetOrderSubTotal(Eval("orderID")), 2)%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="salestotallbl" runat="server" CssClass="est_heading1"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Resend">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="resendcb" runat="server"/>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Needs PO">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="needspocb" runat="server" checked='<%# Bind("needspo") %>'/>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="viewbtn" runat="server" Text="View" CommandName="View" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BorderWidth="1px" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
<asp:SqlDataSource ID="SqlOrders" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_order] WHERE confirmed=@confirmed and isReturn='No' and needspo=@needspo ORDER BY [order_date] desc">
    <SelectParameters>
        <asp:Parameter DefaultValue="True" Name="confirmed" />
        <asp:Parameter DefaultValue="True" Name="needspo" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOrdersByCompany" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [v_order_invoice] WHERE confirmed=@confirmed and isReturn='No' and needspo=@needspo and companyID=@companyID ORDER BY [order_date] desc">
    <SelectParameters>
        <asp:Parameter DefaultValue="True" Name="confirmed" />
        <asp:Parameter DefaultValue="True" Name="needspo" />
        <asp:ControlParameter ControlID="customerdd" DefaultValue="" Name="companyID" PropertyName="SelectedValue" />
    </SelectParameters>
</asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCustomers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [company], [companyID] FROM [t_order] WHERE ([needspo] = @needspo) GROUP BY company, companyID ORDER BY [company]">
        <SelectParameters>
            <asp:Parameter DefaultValue="True" Name="needspo" Type="Boolean" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlContacts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [userID], [name] FROM [t_user] WHERE ([companyID] = @companyID) ORDER BY [name]">
        <SelectParameters>
            <asp:ControlParameter ControlID="customerdd" Name="companyID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

