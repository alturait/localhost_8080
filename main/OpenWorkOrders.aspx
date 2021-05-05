<%@ Page Title="Work Orders" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="OpenWorkOrders.aspx.vb" Inherits="main_OpenWorkOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td><asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlRepairs" Width="100%" AllowPaging="True" GridLines="None" EmptyDataText="No Open Work Orders">
                                <Columns>
                                    <asp:TemplateField HeaderText="WO ID" SortExpression="repairID">
                                        <ItemTemplate>
                                            <asp:Label ID="repairIDlbl" runat="server" Text='<%# Bind("repairID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="company" HeaderText="Customer" SortExpression="company" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="s_company" HeaderText="Vendor" SortExpression="s_company">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="manufacturer" HeaderText="Manufacturer" SortExpression="manufacturer" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="model" HeaderText="Model" SortExpression="model" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="serial_number" HeaderText="Serial #" SortExpression="serial_number" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="date_received" HeaderText="Received On" SortExpression="date_received" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:CheckBoxField DataField="work_approved" HeaderText="Approved" SortExpression="work_approved" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:CheckBoxField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="printbtn" runat="server" Text="Print" CommandName="Print" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                            <asp:LinkButton ID="editbtn" runat="server" Text="Edit" CommandName="Edit" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
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
        <tr>
            <td>
                <asp:SqlDataSource ID="SqlRepairs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [repairID], [company], [name], [s_company], [s_name], [manufacturer], [model], [serial_number], [date_received], [date_return_estimate], [work_approved] FROM [v_repairs] ORDER BY [date_received]"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

