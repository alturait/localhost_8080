<%@ Page Title="Order Line" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="EditOrderLine.aspx.vb" Inherits="main_EditOrderLine" %>

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
                            <asp:TextBox ID="mfrtb" runat="server" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="heading1" Text="Part Number"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="pntb" runat="server" Width="300px" AutoPostBack="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" CssClass="heading1" Text="Item"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="itemtb" runat="server" Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:Label ID="Label5" runat="server" CssClass="heading1" Text="Qty"></asp:Label>
                        </td>
                        <td colspan="2" class="auto-style1">
                            <asp:TextBox ID="quantitytb" runat="server" Width="30px">1</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" CssClass="heading1" Text="UOM"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="uomtb" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">
                            Cost</td>
                        <td colspan="2" class="auto-style3">
                            <asp:Label ID="costlbl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="Label40" runat="server" CssClass="heading1" Text="Price"></asp:Label>
                        </td>
                        <td colspan="2" class="auto-style2">
                            <asp:TextBox ID="pricetb" runat="server" Width="50px"></asp:TextBox>
                        &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:Label ID="Label41" runat="server" CssClass="heading1" Text="Availablity"></asp:Label>
                        </td>
                        <td colspan="2" class="auto-style1">
                            <asp:TextBox ID="availabilitytb" runat="server" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:Label ID="Label42" runat="server" CssClass="heading1" Text="Kit ID"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="kitIDlbl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <asp:Button ID="submitbtn" runat="server" Text="Save Line" CssClass="pushbutton1 gold" />
                            </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <asp:DataList ID="DataList2" runat="server" DataSourceID="SqlCompetitors">
                                <ItemTemplate>
                                    <asp:Label ID="manufacturerLabel" runat="server" Text='<%# Eval("manufacturer") %>' />
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("partnumber") %>' />
                                    <br />
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="luberfinerpn" runat="server" />
    <asp:SqlDataSource ID="SqlCompetitors" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT manufacturer,partnumber FROM [t_luberfiner_xref] WHERE luberfiner_pn=@luberfiner_pn GROUP BY manufacturer, partnumber ORDER BY [manufacturer], partnumber">
        <SelectParameters>
            <asp:ControlParameter ControlID="luberfinerpn" Name="luberfiner_pn" PropertyName="Value" />
        </SelectParameters>
    </asp:SqlDataSource>
    </asp:Content>

