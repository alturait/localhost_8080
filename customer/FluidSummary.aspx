<%@ Page Title="Fluid Usage" Language="VB" MasterPageFile="CustomerMaster.master" AutoEventWireup="false" CodeFile="FluidSummary.aspx.vb" Inherits="customer_FluidSummary" %>

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
                            <asp:DropDownList ID="monthdd" runat="server" AutoPostBack="True">
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="yeardd" runat="server" AutoPostBack="True" Height="16px">
                                <asp:ListItem>2015</asp:ListItem>
                                <asp:ListItem>2016</asp:ListItem>
                                <asp:ListItem>2017</asp:ListItem>
                                <asp:ListItem>2018</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="vertical-align: top">
                            <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" DataSourceID="SqlFluidsByType" EmptyDataText="NOT USED" GridLines="None" Width="100%" AllowSorting="True">
                                <Columns>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="fluidIDlbl" runat="server" Text='<%# Bind("fluidID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="fluid" HeaderText="Fluid" SortExpression="fluid">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="quantity" HeaderText="Qty" SortExpression="quantity">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="uom" HeaderText="Units" SortExpression="uom">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="assetbtn" runat="server" Text="By Asset" CommandName="ByAsset" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" style="padding: 10px;" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="detailbtn" runat="server" Text="Detail" CommandName="Detail" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" style="padding: 10px;" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlFluidsByType" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT fluidID, fluid, uom, sum(quantity) as quantity FROM v_fluid WHERE (companyID = @companyID) and MONTH(filldate)=@month and YEAR(filldate)=@year GROUP BY fluidID, fluid, uom ORDER BY fluid, uom">
                    <SelectParameters>
                        <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
                        <asp:ControlParameter ControlID="monthdd" Name="month" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="yeardd" Name="year" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

