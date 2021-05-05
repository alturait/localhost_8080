<%@ Page Title="" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="SupplierHistory.aspx.vb" Inherits="main_SupplierHistory" %>

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
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="monthdd" runat="server" AutoPostBack="True">
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
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right">
                            
                            <asp:DropDownList ID="vendordd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlSuppliers" DataTextField="company" DataValueField="company">
                                <asp:ListItem Value="0">Select Supplier</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="poID" DataSourceID="SqlPOs" GridLines="None" Width="100%">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="poID" InsertVisible="False" SortExpression="poID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="poIDlbl" runat="server" Text='<%# Bind("poID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="po" HeaderText="PO" SortExpression="po">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="supplier" HeaderText="Supplier" SortExpression="supplier">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="date_submitted" DataFormatString="{0:d}" HeaderText="Submitted On" SortExpression="date_submitted">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="username" HeaderText="Submitted By" SortExpression="username">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:CheckBoxField DataField="submitted" HeaderText="Submitted" SortExpression="submitted" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:CheckBoxField>
                                    <asp:BoundField DataField="email_1" HeaderText="Emailed To" SortExpression="email_1">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
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
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlPOs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_po] WHERE supplier=@supplier AND complete=@complete and MONTH(date_submitted)=@month and YEAR(date_submitted)=@year ORDER BY poID">
        <SelectParameters>
            <asp:ControlParameter ControlID="vendordd" Name="supplier" PropertyName="SelectedValue" />
            <asp:Parameter DefaultValue="True" Name="complete" />
            <asp:ControlParameter ControlID="monthdd" Name="month" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="yeardd" Name="year" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlSuppliers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [company] FROM [t_company] WHERE ([supplier] = @supplier) ORDER BY [company]">
        <SelectParameters>
            <asp:Parameter DefaultValue="True" Name="supplier" Type="Boolean" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

