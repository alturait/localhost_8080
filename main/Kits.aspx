<%@ Page Title="Kits" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="Kits.aspx.vb" Inherits="main_KitList2" %>

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
                        <td style="text-align: right"> </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlKits" AllowSorting="True" Width="100%" GridLines="None">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="kitcodelbl" runat="server" Text='<%# Eval("kitcode")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 15%"><asp:Label ID="Label4" runat="server" Text="Kit" CssClass="est_heading1"></asp:Label></td>
                                                    <td style="width: 15%"><asp:Label ID="Label9" runat="server" Text="Interval" CssClass="est_heading1"></asp:Label></td>
                                                    <td style="width: 10%; text-align: right;"><asp:Label ID="Label12" runat="server" Text="Kit Cost" CssClass="est_heading1"></asp:Label></td>
                                                    <td style="width: 10%;"></td>
                                                    <td style="width: 50%"><asp:Label ID="Label11" runat="server" Text="Assets" CssClass="est_heading1"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="vertical-align: top; text-align: left; width: 15%">
                                                        <asp:LinkButton ID="viewbtn" runat="server" Text='<%# appcode.GetKitName(Eval("serviceprofileID"))%>' CommandName="kit" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" style="padding: 10px;" ToolTip='<%# appcode.GetKitContents(Eval("kitcode"), Session("selected_companyID"))%>' />
                                                    </td>
                                                    <td style="vertical-align: top; text-align: left; width: 15%">
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("interval")%>'></asp:Label>&nbsp;
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("interval_type")%>'></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: top; text-align: right; width: 10%">
                                                        <asp:Label ID="Label7" runat="server" Text='<%# FormatCurrency(appcode.GetKitCost(Eval("kitcode")), 2)%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%;"></td>
                                                    <td style="vertical-align: top; text-align: left; width: 60%">
                                                        <asp:Label ID="otherlbl" runat="server" Text='<%# appcode.GetAssets(Eval("kitcode"), Session("selected_companyID"))%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
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
                <asp:SqlDataSource ID="SqlIntervals" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [interval] FROM [v_serviceprofile] WHERE ([companyID] = @companyID) GROUP BY interval ORDER BY [interval]">
                    <SelectParameters>
                        <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlKits" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT kitcode, interval, interval_type, serviceprofileID FROM [v_serviceprofile] WHERE ([companyID] = @companyID) GROUP BY kitcode, interval, interval_type, serviceprofileID ORDER BY interval">
                    <SelectParameters>
                        <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlKitsByEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT kitcode, interval, interval_type, serviceprofileID FROM [v_serviceprofile] WHERE ([equipmentID] = @equipmentID) GROUP BY kitcode, interval, interval_type, serviceprofileID ORDER BY interval">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="equipmentID" QueryStringField="equipmentID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

