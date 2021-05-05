<%@ Page Title="Recent Errors" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="RecentErrors.aspx.vb" Inherits="main_RecentErrors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td><asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="errorID" DataSourceID="SqlErrors" Width="100%" AllowPaging="True" PageSize="100">
                                <Columns>
                                    <asp:BoundField DataField="errorID" HeaderText="errorID" InsertVisible="False" ReadOnly="True" SortExpression="errorID" Visible="False" />
                                    <asp:TemplateField HeaderText="userID" SortExpression="userID">
                                        <HeaderTemplate>
                                            <table style="width: 1000px">
                                                <tr>
                                                    <td style="width: 100px;text-align: left">Date</td>
                                                    <td style="width: 100px;text-align: left">Time</td>
                                                    <td style="width: 500px;text-align: left">Page</td>
                                                    <td style="width: 300px;text-align: left">User</td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 1000px">
                                                <tr>
                                                    <td style="width: 100px";text-align: left><%#FormatDateTime(Eval("error_date"), DateFormat.ShortDate)%></td>
                                                    <td style="width: 100px";text-align: left><%# Eval("error_time")%></td>
                                                    <td style="width: 500px";text-align: left><%# Eval("error_page")%></td>
                                                    <td style="width: 300px";text-align: left><%# appcode.GetUserName(Eval("userID"))%></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4";text-align: left><%# Eval("error_msg")%></td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
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
                <asp:SqlDataSource ID="SqlErrors" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_error] ORDER BY [error_date] DESC, [error_time]"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

