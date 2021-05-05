<%@ Page Title="Task" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="Task.aspx.vb" Inherits="Task" %>

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
                                                        
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Component"></asp:Label>
                                    </td>
                                    <td>
                                        
                                        <asp:DropDownList ID="componentdd" runat="server" DataSourceID="SqlComponents" DataTextField="component" DataValueField="componentID" Height="17px">
                                        </asp:DropDownList>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Description"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="descriptiontb" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Cost"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="costtb" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Price"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="pricetb" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            
                            <asp:Button ID="Button1" runat="server" Text="Save" />
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlComponents" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_component] ORDER BY [component]"></asp:SqlDataSource>
</asp:Content>

