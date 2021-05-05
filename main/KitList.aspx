<%@ Page Title="Kit List" Language="VB" MasterPageFile="~/main/Anonymous.master" AutoEventWireup="false" CodeFile="KitList.aspx.vb" Inherits="main_KitList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlKits" AllowSorting="True" Width="100%" GridLines="None">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Kit ID" SortExpression="kitcode">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# appcode.GetKitID(Eval("kitcode"))%>' Font-Bold="True"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Frequency" SortExpression="quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="qtylbl" runat="server" Text='<%# Eval("quantity")%>' Font-Bold="True"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Asset - Interval">
                                        <ItemTemplate>
                                            <asp:Label ID="assetslbl" runat="server" Text='<%# appcode.GetSameAs(Eval("kitcode"), Session("selected_companyID"))%>'></asp:Label><br />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contents">
                                        <ItemTemplate>
                                            <asp:Label ID="contentslbl" runat="server" Text='<%# appcode.GetKitContents(Eval("kitcode"), Session("selected_companyID"))%>'></asp:Label><br />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kit Cost">
                                        <ItemTemplate>
                                            <asp:Label ID="contentslbl" runat="server" Text='<%# FormatCurrency(appcode.GetKitCost(Eval("kitcode")), 2)%>' Font-Bold="True"></asp:Label>&nbsp;
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
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
                <asp:SqlDataSource ID="SqlKits" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT kitcode, count(kitcode) as quantity FROM [v_serviceprofile] WHERE ([companyID] = @companyID) GROUP BY kitcode ORDER BY [quantity] desc">
                    <SelectParameters>
                        <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    </asp:Content>

