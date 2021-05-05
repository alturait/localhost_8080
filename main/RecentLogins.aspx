<%@ Page Title="Recent Activity" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="RecentLogins.aspx.vb" Inherits="main_RecentLogins" %>

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
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="loginID" DataSourceID="Sqlogins" Width="100%" AllowPaging="True" GridLines="None" PageSize="100">
                                <Columns>
                                    <asp:BoundField DataField="loginID" HeaderText="loginID" InsertVisible="False" ReadOnly="True" SortExpression="loginID" Visible="False" />
                                    <asp:BoundField DataField="company" HeaderText="Company" SortExpression="company">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="login_date" HeaderText="Date" SortExpression="login_date" DataFormatString="{0:d}" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="login_time" HeaderText="Time" SortExpression="login_time" >
                                    
                                    <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="Sqlogins" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [company], [name], [login_date], [login_time], [loginID] FROM [v_logins] ORDER BY [login_date] DESC, [login_time]"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

