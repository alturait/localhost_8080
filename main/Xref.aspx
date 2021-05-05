<%@ Page Title="XREF" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="Xref.aspx.vb" Inherits="main_Xref" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 50%">
                    <tr>
                        <td style="width: 20%">
                            <asp:Label ID="Label1" runat="server" Text="Reference 1"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="mfrlbl" runat="server" Font-Bold="True"></asp:Label>
                        &nbsp;<asp:Label ID="partnumberlbl" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Reference 2"></asp:Label>
                        </td>
                        <td style="width: 70%">
                            <asp:DropDownList ID="manufacturerdd" runat="server" AppendDataBoundItems="True" DataSourceID="SqlManufacturers" DataTextField="manufacturer" DataValueField="manufacturer" AutoPostBack="True">
                                <asp:ListItem Value="0">Select Mfr</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="partnumberdd" runat="server" DataSourceID="SqlPartNumber" DataTextField="partnumber" DataValueField="partnumber" AppendDataBoundItems="True" AutoPostBack="True" style="height: 22px">
                                <asp:ListItem Value="0">Select Part Number</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="addbtn" runat="server" Text="Add XREF" Width="100%" />
                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
    <asp:SqlDataSource ID="SqlManufacturers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT upper(manufacturer) as manufacturer FROM [t_product] GROUP BY manufacturer ORDER BY [manufacturer]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlPartNumber" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [productID], [partnumber] FROM [t_product] WHERE ([manufacturer] = @manufacturer) ORDER BY [partnumber]">
        <SelectParameters>
            <asp:ControlParameter ControlID="manufacturerdd" Name="manufacturer" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

