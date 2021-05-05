<%@ Page Title="Add Order Line" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="AddOrderLine.aspx.vb" Inherits="main_AddOrderLine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" CssClass="heading1" Text="Manufacturer"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="manufacturerdd" runat="server" AppendDataBoundItems="True" DataSourceID="SqlManufacturers" DataTextField="manufacturer" DataValueField="manufacturer" AutoPostBack="True">
                                <asp:ListItem Value="0">Select Mfr</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="heading1" Text="Part Number"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="partnumberdd" runat="server" DataSourceID="SqlPartNumber" DataTextField="partnumber" DataValueField="partnumber" AppendDataBoundItems="True" AutoPostBack="True" style="height: 22px">
                                <asp:ListItem Value="0">Select Part Number</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" CssClass="heading1" Text="Item"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="itemlbl" runat="server" CssClass="textbox4" Width="400px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" CssClass="heading1" Text="Qty"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="quantitytb" runat="server" Width="30px">1</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" CssClass="heading1" Text="UOM"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="uomlbl" runat="server" Width="50px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label40" runat="server" CssClass="heading1" Text="Price"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="pricetb" runat="server" Width="50px"></asp:TextBox>
                        &nbsp;<asp:Label ID="pricelbl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <asp:Button ID="submitbtn" runat="server" Text="Add To Order" CssClass="pushbutton1 gold" />
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlManufacturers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT upper(manufacturer) as manufacturer FROM [t_product] GROUP BY manufacturer ORDER BY [manufacturer]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlPartNumber" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [productID], [partnumber] FROM [t_product] WHERE ([manufacturer] = @manufacturer) ORDER BY [partnumber]">
        <SelectParameters>
            <asp:ControlParameter ControlID="manufacturerdd" Name="manufacturer" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    </asp:Content>

