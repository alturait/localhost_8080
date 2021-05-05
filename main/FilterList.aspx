<%@ Page Title="Kit Requirements" Language="VB" MasterPageFile="~/main/Anonymous.master" AutoEventWireup="false" CodeFile="FilterList.aspx.vb" Inherits="main_FilterList" %>

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
                            <asp:DropDownList ID="mfrdd" runat="server" DataSourceID="SqlMfrs" DataTextField="manufacturer" DataValueField="manufacturer" AppendDataBoundItems="True" AutoPostBack="True">
                                <asp:ListItem>Select Manufacturer</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="podd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlPOs" DataTextField="po" DataValueField="poID" Height="16px">
                                <asp:ListItem Value="0">New PO</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="addPObtn" runat="server" CssClass="pushbutton1 gold" Text="Add to PO" />
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="updatebtn" runat="server" Text="Update On Hand" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="Label1" runat="server" Text="Customers: " CssClass="est_heading1"></asp:Label>
                            <asp:Label ID="customerslbl" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlPartSummary" AllowSorting="True" GridLines="None" Width="100%" Style="padding: 10px" ShowFooter="True">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Part Number" SortExpression="partnumber">
                                        <ItemTemplate>
                                            <a href='SearchEquipment.aspx?searchterm=<%# Eval("partnumber")%>'>
                                            <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer")%>'></asp:Label>&nbsp;
                                            <asp:Label ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label>
                                            </a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="numlines" HeaderText="Kits" ReadOnly="True" SortExpression="numlines" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Pkg">
                                        <ItemTemplate>
                                            <asp:Label ID="packagelbl" runat="server" Text='<%# appcode.GetPackage(Eval("manufacturer"), Eval("partnumber"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cost">
                                        <ItemTemplate>
                                            <asp:Label ID="costlbl" runat="server" Text='<%# FormatCurrency(appcode.GetCost(Eval("manufacturer"), Eval("partnumber")), 2)%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Extended">
                                        <ItemTemplate>
                                            <asp:Label ID="extlbl" runat="server" Text='<%# FormatCurrency(appcode.GetExtended(Eval("manufacturer"), Eval("partnumber")), 2)%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="subtotallbl" runat="server" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="On Hand">
                                        <ItemTemplate>
                                            <asp:TextBox ID="onhandtb" runat="server" Text='<%# appcode.GetOnHand(Eval("manufacturer"), Eval("partnumber"))%>' Width="30"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="selectcb" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlPartSummary" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT manufacturer,partnumber,description,count(partnumber) as numlines FROM v_part_summary WHERE companyID=@companyID and manufacturer=@manufacturer GROUP BY manufacturer,partnumber,description ORDER BY [manufacturer], [partnumber]">
                                <SelectParameters>
                                    <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
                                    <asp:ControlParameter ControlID="mfrdd" Name="manufacturer" PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlMfrs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer] FROM [v_part_summary] group by manufacturer ORDER BY [manufacturer]"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlPOs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [poID], [supplier] + ' - ' + po as po FROM [t_po] where complete='False'"></asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

