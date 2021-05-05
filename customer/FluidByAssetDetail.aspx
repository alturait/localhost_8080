<%@ Page Title="Fluid By Asset Detail" Language="VB" MasterPageFile="CustomerMaster.master" AutoEventWireup="false" CodeFile="FluidByAssetDetail.aspx.vb" Inherits="customer_FluidByAssetDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td><asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
                        <td style="text-align: right">
                            <asp:DropDownList ID="yeardd" runat="server" AutoPostBack="True" Height="16px">
                                <asp:ListItem>2015</asp:ListItem>
                                <asp:ListItem>2016</asp:ListItem>
                                <asp:ListItem>2017</asp:ListItem>
                                <asp:ListItem>2018</asp:ListItem>
                                <asp:ListItem>2019</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" DataSourceID="SqlFluidsByType" EmptyDataText="NOT USED" GridLines="None" Width="100%" AllowSorting="True">
                                <Columns>
                                    <asp:BoundField DataField="assetID" HeaderText="Asset ID" SortExpression="assetID">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="filldate" DataFormatString="{0:d}" HeaderText="Date" SortExpression="filldate">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="filltime" HeaderText="Time" SortExpression="filltime">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
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
                                    <asp:BoundField DataField="note" HeaderText="System" SortExpression="note">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="hours_miles" HeaderText="Hours/Miles" SortExpression="hours_miles">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="name" HeaderText="Technician" SortExpression="name">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlFluidsByType" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT assetID, filldate, filltime, fluid, uom, name, quantity, hours_miles, note FROM v_fluid WHERE equipmentID=@equipmentID and fluidID=@fluidID and YEAR(filldate)=@year ORDER BY fluid, uom">
                    <SelectParameters>
                        <asp:SessionParameter Name="equipmentID" SessionField="selected_equipmentID" Type="Int32" />
                        <asp:SessionParameter Name="fluidID" SessionField="fluidID" Type="Int32" />
                        <asp:ControlParameter ControlID="yeardd" Name="year" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

