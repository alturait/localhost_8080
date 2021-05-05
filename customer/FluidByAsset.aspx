<%@ Page Title="Fluid Usage By Asset" Language="VB" MasterPageFile="CustomerMaster.master" AutoEventWireup="false" CodeFile="FluidByAsset.aspx.vb" Inherits="customer_FluidByAsset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>&nbsp;
                        </td>
                        <td style="text-align: right">
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
                            <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" DataSourceID="SqlFluidsByAsset" EmptyDataText="NOT USED" GridLines="None" Width="100%" AllowSorting="True">
                                <Columns>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="equipmentIDlbl" runat="server" Text='<%# Bind("equipmentID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="fluidIDlbl" runat="server" Text='<%# Bind("fluidID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Asset" SortExpression="assetID">
                                        <ItemTemplate>
                                            <asp:Label ID="assetIDlbl" runat="server" Text='<%# Bind("assetID")%>'></asp:Label>
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
                                            <asp:LinkButton ID="detailbtn" runat="server" Text="Detail" CommandName="Detail" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" style="padding: 10px;" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlFluidsByAsset" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipmentID, assetID, fluidID, fluid, uom, sum(quantity) as quantity FROM v_fluid WHERE (companyID = @companyID) and fluidID=@fluidID and YEAR(filldate)=@year GROUP BY equipmentID, assetID, fluidID, fluid, uom ORDER BY assetID, fluid, uom">
                    <SelectParameters>
                        <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
                        <asp:SessionParameter Name="fluidID" SessionField="fluidID" Type="Int32" />
                        <asp:ControlParameter ControlID="yeardd" Name="year" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

