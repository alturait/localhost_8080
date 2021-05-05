<%@ Page Title="Sales YTD" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="SalesYTDByMfr.aspx.vb" Inherits="main_SalesByMfrDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label> &nbsp;
                            <asp:Label ID="yearlbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        &nbsp;<asp:LinkButton ID="backbtn" runat="server">BACK</asp:LinkButton>
                        </td>
                        <td style="text-align: right" colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">

                            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlYTDByMfr" GridLines="None">
                                <Columns>
                                    <asp:TemplateField HeaderText="Part Number" SortExpression="partnumber">
                                        <ItemTemplate>
                                            <a href='SalesYTDByMfrDetail.aspx?mfr=<%# Eval("manufacturer")%>&partnumber=<%# Eval("partnumber")%>'><asp:Label ID="Label1" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="price" HeaderText="Sales" SortExpression="price" DataFormatString="{0:c}" >
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlYTDByMfr" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer], [partnumber], sum([quantity]*[price]) as price FROM [v_order_lines] WHERE ([manufacturer] = @manufacturer) AND year(order_date)=year(getDate()) GROUP BY manufacturer, partnumber ORDER BY [price] desc">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="manufacturer" QueryStringField="mfr" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

