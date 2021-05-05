<%@ Page Title="Part Summary" Language="VB" MasterPageFile="~/customer/CustomerMaster.master" AutoEventWireup="false" CodeFile="PartSummary.aspx.vb" Inherits="customer_PartSummary" %>

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
                            <asp:Label ID="Label1" runat="server" Text="Location" Font-Bold="True"></asp:Label>
                            <asp:DropDownList ID="locationdd" runat="server" AutoPostBack="True" DataSourceID="SqlLocations" DataTextField="shipto" DataValueField="shipID" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">All Locations</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="pdfbtn" runat="server" Text="PDF" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlPartSummaryByCustomer" AllowSorting="True" GridLines="None" Width="60%" PageSize="25" Style="padding: 2px">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Part Number" SortExpression="partnumber">
                                        <ItemTemplate>
                                            <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Part Number" SortExpression="partnumber">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="detailbtn" runat="server" Text='<%# Bind("partnumber")%>' CommandName="Detail" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" style="padding: 10px;" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="numlines" HeaderText="Frequency" ReadOnly="True" SortExpression="numlines" >
                                    <HeaderStyle HorizontalAlign="Center" Width="13%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="DFO">
                                        <ItemTemplate>
                                            <asp:Label ID="onhandlbl" runat="server" Text='<%# appcode.GetOnHand(Eval("manufacturer"), Eval("partnumber"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="13%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price">
                                        <ItemTemplate>
                                            <asp:Label ID="pricelbl" runat="server" Text='<%#FormatCurrency(appcode.GetCompanyPrice(Session("companyID"), Eval("manufacturer"), Eval("partnumber")), 2)%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" Width="15%" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="viewbtn" runat="server" Text="Equipment" CommandName="Equipment" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" style="padding: 10px;" />
                                            <asp:Label ID="partnumberlbl" runat="server" Text='<%# Eval("partnumber") %>' Visible="False"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="19%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlPartSummaryByCustomer" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT manufacturer,partnumber,count(partnumber) as numlines FROM v_part_summary WHERE companyID=@companyID GROUP BY manufacturer,partnumber ORDER BY [manufacturer], [partnumber]">
                                <SelectParameters>
                                    <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlPartSummaryByLocation" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT manufacturer,partnumber,count(partnumber) as numlines FROM v_part_summary WHERE companyID=@companyID and locationID=@locationID GROUP BY manufacturer,partnumber ORDER BY [manufacturer], [partnumber]">
                                <SelectParameters>
                                    <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
                                    <asp:SessionParameter Name="locationID" SessionField="this_locationID" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlLocations" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [shipID], [shipto] FROM [t_ship] WHERE ([companyID] = @companyID) ORDER BY [shipto]">
                                <SelectParameters>
                                    <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

