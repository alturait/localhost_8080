<%@ Page Title="Top 25" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="Top25.aspx.vb" Inherits="main_Top25" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>

                        </td>
                        <td style="text-align: right">

                            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlMfrs" DataTextField="manufacturer" DataValueField="manufacturer" AutoPostBack="True">
                            </asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlTop25">
                                <Columns>
                                    <asp:BoundField DataField="manufacturer" HeaderText="manufacturer" SortExpression="manufacturer" />
                                    <asp:BoundField DataField="partnumber" HeaderText="partnumber" SortExpression="partnumber" />
                                    <asp:BoundField DataField="partcount" HeaderText="partcount" SortExpression="partcount" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlMfrs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer] FROM [t_parts] GROUP BY [manufacturer] ORDER BY [manufacturer]"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlTop25" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Top 25 [manufacturer], [partnumber], count(partID) as partcount FROM [t_parts] where [manufacturer]=@manufacturer GROUP BY [manufacturer],[partnumber] ORDER BY partcount desc">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownList1" Name="manufacturer" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

