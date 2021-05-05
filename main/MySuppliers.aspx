<%@ Page Title="Suppliers" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="MySuppliers.aspx.vb" Inherits="EST_MySuppliers" %>

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
                            <asp:Button ID="newbtn" runat="server" Text="New Supplier" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlVendors" Width="100%" GridLines="None">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="supplierID" InsertVisible="False" SortExpression="supplierID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="companyIDlbl" runat="server" Text='<%# Bind("companyID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company" SortExpression="company">
                                        <ItemTemplate>
                                            <asp:Label ID="companylbl" runat="server" Text='<%# Bind("company") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="city" HeaderText="City" SortExpression="city" >
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="state" HeaderText="State" SortExpression="state" >
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="zipcode" HeaderText="Zip Code" SortExpression="zipcode" >
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="c_phone" HeaderText="Phone" SortExpression="c_phone" >
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="historybtn" runat="server" Text="History" CommandName="History" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                            <asp:LinkButton ID="viewbtn" runat="server" Text="View" CommandName="View" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%" />
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
    <asp:SqlDataSource ID="SqlVendors" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM t_company where supplier=@supplier ORDER BY [company]">
        <SelectParameters>
            <asp:Parameter DefaultValue="True" Name="supplier" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:HiddenField ID="companyIDlbl" runat="server" />
</asp:Content>

