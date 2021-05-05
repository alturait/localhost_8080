<%@ Page Title="Virtual Stock Room" Language="VB" MasterPageFile="CustomerMaster.master" AutoEventWireup="false" CodeFile="Warehouse.aspx.vb" Inherits="customer_Warehouse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                        <asp:Label ID="warehouselbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                            <asp:Button ID="resetbtn" runat="server" Text="Reset" CssClass="pushbutton1 gold" CausesValidation="False" OnClientClick="return confirm('Resetting will remove all items from your stock room. Do you wish to continue?');" Font-Size="Large" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            This is your virtual stock room where you can set add &amp; remove parts, set mins and maxes for each and track the value of your on hand inventory. To add a part, search for it then select MORE INFO and click on Add To Stock Room. To update on hand quantity, min and/or max, make changes on each line and then click on Update Inventory at the bottom of the page.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:DropDownList ID="manufacturerdd" runat="server" AutoPostBack="True" DataSourceID="SqlManufacturers" DataTextField="manufacturer" DataValueField="manufacturer" AppendDataBoundItems="True" Font-Size="Large">
                                <asp:ListItem Value="0">Select Manufacturer</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlInventory" GridLines="None" Width="100%" AllowSorting="True" ShowFooter="True">
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
                                            <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label> <asp:LinkButton ID="detailbtn" runat="server" Text='<%# Bind("partnumber")%>' CommandName="Detail" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" /> 
                                            <asp:Label ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber")%>' Visible="False"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="removebtn" runat="server" Text="Remove" CommandName="Remove" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
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
                                            <asp:Label ID="costlbl" runat="server" Text='<%# FormatCurrency(appcode.GetCompanyPrice(Session("selected_companyID"), Eval("manufacturer"), Eval("partnumber")), 2)%>' Width="60"></asp:Label>&nbsp;<asp:Label ID="uomlbl" runat="server" Text='<%# appcode.GetUOM(Eval("manufacturer"), Eval("partnumber")).ToString%>'></asp:Label>
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
                        <td style="text-align: center">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="savebtn" runat="server" Text="Update Inventory" CssClass="pushbutton1 gold" CausesValidation="False" />
                            <asp:Button ID="orderbtn" runat="server" Text="Create Order" CssClass="pushbutton1 gold" CausesValidation="False" Visible="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlAllInventory" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_warehouse] WHERE companyID=@companyID ORDER BY manufacturer,partnumber">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlInventory" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_warehouse] WHERE companyID=@companyID and manufacturer=@manufacturer ORDER BY manufacturer,partnumber">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
            <asp:ControlParameter ControlID="manufacturerdd" Name="manufacturer" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlManufacturers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer] FROM t_warehouse WHERE companyID=@companyID AND manufacturer<>'' GROUP BY manufacturer ORDER BY [manufacturer]">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

