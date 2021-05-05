<%@ Page Title="Inventory Requirements" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="InventoryRequirements.aspx.vb" Inherits="main_InventoryRequirements" %>

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
                        <td style="text-align: right"></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlMfrInventory" GridLines="None" Width="100%" AllowSorting="True" ShowFooter="True">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="editbtn" runat="server" Text="Detail" CommandName="Edit" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Part Number" SortExpression="partnumber">
                                        <ItemTemplate>
                                            <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label> <asp:Label ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order" SortExpression="onhand">
                                        <ItemTemplate>
                                            <asp:TextBox ID="ordertb" runat="server" Text='<%# Eval("max") - Eval("onhand") - appcode.GetOnPOAmount(Eval("manufacturer"), Eval("partnumber"))%>' Width="35"></asp:TextBox>
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
    <asp:SqlDataSource ID="SqlMfrInventory" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_product] WHERE nStock=@nStock and onhand<=min ORDER BY manufacturer,partnumber">
        <SelectParameters>
            <asp:Parameter DefaultValue="True" Name="nStock" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

