<%@ Page Title="Warehouse" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="Warehouse.aspx.vb" Inherits="main_Warehouse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 30%">
                                        <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                    </td>
                                    <td style="width: 40%;text-align: center">
                                        <asp:DropDownList ID="manufacturerdd" runat="server" AutoPostBack="True" DataSourceID="SqlManufacturers" DataTextField="manufacturer" DataValueField="manufacturer" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Select Manufacturer</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:CheckBox ID="allusagecb" runat="server" AutoPostBack="True" Text="All Usage" />
                                    </td>
                                    <td style="width: 30%;text-align: right">
                                        <asp:Button ID="pricesheetbtn" runat="server" Text="Print Pricesheet" CssClass="pushbutton1 gold" CausesValidation="False" />
                                        <asp:Button ID="worksheetbtn" runat="server" Text="Print Worksheet" CssClass="pushbutton1 gold" CausesValidation="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%">
                                        &nbsp;</td>
                                    <td style="width: 40%;text-align: center">
                                        
                                        &nbsp;</td>
                                    <td style="width: 30%;text-align: right">
                                        
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlInventory" GridLines="None" Width="100%" AllowSorting="True" ShowFooter="True" AllowPaging="True" PageSize="25">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="warehouseIDlbl" runat="server" Text='<%# Bind("warehouseID")%>'></asp:Label> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Part Number" SortExpression="partnumber">
                                        <ItemTemplate>
                                            <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label> <asp:Label ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Main" SortExpression="nStock">
                                        <ItemTemplate>
                                            <asp:checkbox ID="nstockcb" Checked='<%# Bind("nStock") %>' runat="server"></asp:checkbox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="editbtn" runat="server" Text="Edit" CommandName="Edit" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" /> <asp:LinkButton ID="removebtn" runat="server" Text="Remove" CommandName="Remove" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CY">
                                        <ItemTemplate>
                                            <asp:Label ID="frequencylbl" runat="server" Text='<%# appcode.GetUsage(Eval("manufacturer"), Eval("partnumber"), Session("selected_companyID"), Year(Now()))%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="On Hand">
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
                                    <asp:TemplateField HeaderText="Unit Cost">
                                        <ItemTemplate>
                                            <asp:TextBox ID="costtb" runat="server" Text='<%# FormatNumber(appcode.GetCompanyPrice(Session("selected_companyID"), Eval("manufacturer"), Eval("partnumber")), 2)%>' Width="60"></asp:TextBox>&nbsp;
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="uomlbl" runat="server" Text='<%# appcode.GetUOM(Eval("manufacturer"), Eval("partnumber")).ToString%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value">
                                        <ItemTemplate>
                                            <asp:Label ID="valuelbl" runat="server" Text='<%# FormatCurrency(Eval("onhand") * appcode.GetCompanyPrice(Session("selected_companyID"), Eval("manufacturer"), Eval("partnumber")), 2)%>'></asp:Label>&nbsp;
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="salestotallbl" runat="server" CssClass="est_heading1"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="savebtn" runat="server" Text="Update Inventory" CssClass="pushbutton1 gold" CausesValidation="False" />
                            <asp:Button ID="cartbtn" runat="server" Text="Add To Cart" CssClass="pushbutton1 gold" CausesValidation="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlManufacturers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer] FROM t_warehouse WHERE companyID=@companyID GROUP BY manufacturer ORDER BY [manufacturer]">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlInventoryAll" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_warehouse] WHERE companyID=@companyID and manufacturer=@manufacturer ORDER BY manufacturer,partnumber">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
            <asp:ControlParameter ControlID="manufacturerdd" Name="manufacturer" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlInventory" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_warehouse] WHERE companyID=@companyID and manufacturer=@manufacturer and max>0 ORDER BY manufacturer,partnumber">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
            <asp:ControlParameter ControlID="manufacturerdd" Name="manufacturer" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlWarehouse" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [location] FROM [t_warehouse] WHERE ([companyID] = @companyID) GROUP BY [location] ORDER BY [location]">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

