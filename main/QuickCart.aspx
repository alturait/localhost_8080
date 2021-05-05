<%@ Page Title="" Language="VB" MasterPageFile="~/main/Anonymous.master" AutoEventWireup="false" CodeFile="QuickCart.aspx.vb" Inherits="main_QuickCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="pagetable">
        <tr>
            <td class="pagebody">
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align: top" colspan="3">

                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table style="width: 60%" align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Manufacturer" CssClass="heading1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Part Number" CssClass="heading1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Qty" CssClass="heading1"></asp:Label>
                                    </td>
                                    <td style="width: 40%">
                
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="mfrdd1" runat="server" DataSourceID="SqlMfrs" DataTextField="manufacturer" DataValueField="manufacturer">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="pntb1" runat="server" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="qtytb1" runat="server" Width="30px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="status1" runat="server" ForeColor="#CC0000"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="mfrdd2" runat="server" DataSourceID="SqlMfrs" DataTextField="manufacturer" DataValueField="manufacturer">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="pntb2" runat="server" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="qtytb2" runat="server" Width="30px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="status2" runat="server" ForeColor="#CC0000"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="mfrdd3" runat="server" DataSourceID="SqlMfrs" DataTextField="manufacturer" DataValueField="manufacturer">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="pntb3" runat="server" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="qtytb3" runat="server" Width="30px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="status3" runat="server" ForeColor="#CC0000"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="mfrdd4" runat="server" DataSourceID="SqlMfrs" DataTextField="manufacturer" DataValueField="manufacturer">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="pntb4" runat="server" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="qtytb4" runat="server" Width="30px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="status4" runat="server" ForeColor="#CC0000"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="mfrdd5" runat="server" DataSourceID="SqlMfrs" DataTextField="manufacturer" DataValueField="manufacturer">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="pntb5" runat="server" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="qtytb5" runat="server" Width="30px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="status5" runat="server" ForeColor="#CC0000"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="mfrdd6" runat="server" DataSourceID="SqlMfrs" DataTextField="manufacturer" DataValueField="manufacturer">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="pntb6" runat="server" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="qtytb6" runat="server" Width="30px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="status6" runat="server" ForeColor="#CC0000"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="mfrdd7" runat="server" DataSourceID="SqlMfrs" DataTextField="manufacturer" DataValueField="manufacturer">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="pntb7" runat="server" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="qtytb7" runat="server" Width="30px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="status7" runat="server" ForeColor="#CC0000"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="mfrdd8" runat="server" DataSourceID="SqlMfrs" DataTextField="manufacturer" DataValueField="manufacturer">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="pntb8" runat="server" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="qtytb8" runat="server" Width="30px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="status8" runat="server" ForeColor="#CC0000"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="mfrdd9" runat="server" DataSourceID="SqlMfrs" DataTextField="manufacturer" DataValueField="manufacturer">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="pntb9" runat="server" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="qtytb9" runat="server" Width="30px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="status9" runat="server" ForeColor="#CC0000"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="mfrdd10" runat="server" DataSourceID="SqlMfrs" DataTextField="manufacturer" DataValueField="manufacturer">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="pntb10" runat="server" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="qtytb10" runat="server" Width="30px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="status10" runat="server" ForeColor="#CC0000"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <asp:Button ID="continuebtn" runat="server" Text="Add Items &amp; Continue" CssClass="pushbutton1 gold" />
                            <asp:Button ID="cartbtn" runat="server" Text="Add Items &amp; Check Out" CssClass="pushbutton1 gold" />
                            <asp:Button ID="resetbtn" runat="server" Text="Reset" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlMfrs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer] FROM [t_product] GROUP BY manufacturer ORDER BY [manufacturer]"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

