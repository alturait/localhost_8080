<%@ Page Title="Inventory Worksheet" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="Inventory.aspx.vb" Inherits="main_Inventory" %>

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
                        &nbsp;<asp:DropDownList ID="manufacturerdd" runat="server" AutoPostBack="True" DataSourceID="SqlManufacturers" DataTextField="manufacturer" DataValueField="manufacturer"></asp:DropDownList>
                        </td>
                        <td style="text-align: right">
                            <asp:DropDownList ID="podd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlPOs" DataTextField="po" DataValueField="poID" Height="16px">
                                <asp:ListItem Value="0">New PO</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="addPObtn" runat="server" CssClass="pushbutton1 gold" Text="Add to PO" />
                        &nbsp;<asp:Button ID="savebtn" runat="server" Text="Update Inventory" CssClass="pushbutton1 gold" CausesValidation="False" />
                        </td>
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
                                    <asp:TemplateField HeaderText="Stock" SortExpression="nStock">
                                        <ItemTemplate>
                                            <asp:Checkbox ID="stockcb" runat="server" Checked='<%# Eval("nStock") %>'></asp:Checkbox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Part Number" SortExpression="partnumber">
                                        <ItemTemplate>
                                            <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label> <asp:Label ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Alternate - OH / PO">
                                        <ItemTemplate>
                                            <asp:Label ID="xreflbl" runat="server" Text='<%# appcode.GetXRefOnHand(Eval("manufacturer"), Eval("partnumber")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LY">
                                        <ItemTemplate>
                                            <asp:Label ID="lylbl" runat="server" Text='<%# appcode.GetSalesQtyYTD(Eval("manufacturer"), Eval("partnumber"), Year(Now()) - 1)%>' Width="35"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="YTD">
                                        <ItemTemplate>
                                            <asp:Label ID="ytdlbl" runat="server" Text='<%# appcode.GetSalesQtyYTD(Eval("manufacturer"), Eval("partnumber"), Year(Now()))%>' Width="35"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="On Order">
                                        <ItemTemplate>
                                            <asp:Label ID="onorderlbl" runat="server" Text='<%# appcode.GetOnOrderAmount(Eval("manufacturer"), Eval("partnumber")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="On PO">
                                        <ItemTemplate>
                                            <asp:Label ID="onpolbl" runat="server" Text='<%# appcode.GetOnPO(Eval("manufacturer"), Eval("partnumber")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="On Hand" SortExpression="onhand">
                                        <ItemTemplate>
                                            <asp:TextBox ID="onhandtb" runat="server" Text='<%# Eval("onhand")%>' Width="35"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Min">
                                        <ItemTemplate>
                                            <asp:TextBox ID="mintb" runat="server" Text='<%# Eval("min")%>' Width="35"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Max">
                                        <ItemTemplate>
                                            <asp:TextBox ID="maxtb" runat="server" Text='<%# Eval("max")%>' Width="35"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pkg" Visible="True">
                                        <ItemTemplate>
                                            <asp:Label ID="pkglbl" runat="server" Text='<%# Eval("package")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="costlbl" runat="server" Text='<%# Eval("cost")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="uomlbl" runat="server" Text='<%# Eval("uom")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlMfrInventory" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT *, cost*onhand as value FROM [t_product] WHERE (nStock=@nStock and manufacturer=@manufacturer) OR (onhand<>0 and manufacturer=@manufacturer) ORDER BY partnumber">
        <SelectParameters>
            <asp:Parameter DefaultValue="True" Name="nStock" />
            <asp:ControlParameter ControlID="manufacturerdd" DefaultValue="" Name="manufacturer" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlInventory" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT *, cost*onhand as value FROM [t_product] WHERE nStock=@nStock ORDER BY manufacturer,partnumber">
        <SelectParameters>
            <asp:Parameter DefaultValue="True" Name="nStock" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlManufacturers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer] FROM t_product WHERE nStock=@nStock OR onhand>0 GROUP BY manufacturer ORDER BY [manufacturer]">
        <SelectParameters>
            <asp:Parameter DefaultValue="True" Name="nStock" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlPOs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [poID], [supplier] + ' - ' + po as po FROM [t_po] where complete='False'"></asp:SqlDataSource>
</asp:Content>

