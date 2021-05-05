<%@ Page Title="Asset" Language="VB" MasterPageFile="CustomerMaster.master" AutoEventWireup="false" CodeFile="Asset.aspx.vb" Inherits="customer_Asset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%">
                            <asp:Panel ID="Panel1" runat="server">
                            <table style="width: 100%">
                                <tr style="border: thin solid #000000">
                                    <td style="vertical-align: top; width: 40%;">
                                        <table style="padding: 5px; width: 100%; vertical-align: top;">
                                            <tr>
                                                <td style="padding: 5px; text-align: left; vertical-align: top;">
                                                    <asp:Label ID="Label6" runat="server" CssClass="heading1" Text="Asset ID" Font-Size="Medium"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td style="padding: 5px; text-align: left;">
                                                    <asp:DropDownList ID="equipmentdd" runat="server" DataSourceID="SqlEquipment" DataTextField="assetID" DataValueField="equipmentID" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:LinkButton ID="profilebtn" runat="server" Font-Size="Small" Text="Edit Info" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px; text-align: left;">
                                                        <asp:Label ID="Label9" runat="server" CssClass="heading1" Text="VIN/Serial" Font-Size="Medium"></asp:Label>
                                                </td>
                                                <td style="padding: 5px; text-align: left;">
                                                    <asp:Label ID="vinlbl" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="padding: 5px; text-align: left;">
                                                        <asp:Label ID="Label10" runat="server" CssClass="heading1" Text="Location" Font-Size="Medium"></asp:Label>
                                                </td>
                                                <td style="padding: 5px; text-align: left;">
                                                    <asp:Label ID="locationlbl" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="padding: 5px; text-align: left;">
                                                        <asp:Label ID="hoursmileslbl" runat="server" CssClass="heading1" Text="Current Hours" Font-Size="Medium"></asp:Label>
                                                </td>
                                                <td style="padding: 5px; text-align: left;">
                                                    <asp:Label ID="choursmileslbl" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="padding: 5px; text-align: left;">
                                                        <asp:Label ID="Label11" runat="server" CssClass="heading1" Text="Hours/Week" Font-Size="Medium"></asp:Label>
                                                </td>
                                                <td style="padding: 5px; text-align: left;">
                                                    <asp:Label ID="hpwlbl" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="padding: 5px; text-align: left;">
                                                        <asp:Label ID="Label12" runat="server" CssClass="heading1" Text="Last Kit" Font-Size="Medium"></asp:Label>
                                                </td>
                                                <td style="padding: 5px; text-align: left;">
                                                    <asp:Label ID="pmlbl" runat="server" Font-Bold="False"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="padding: 5px; text-align: left;">
                                                        <asp:Label ID="Label13" runat="server" CssClass="heading1" Text="Order Date" Font-Size="Medium"></asp:Label>
                                                </td>
                                                <td style="padding: 5px; text-align: left;">
                                                    <asp:Label ID="datelbl" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="border-style: solid; border-width: medium; vertical-align: top">
                                        <asp:Panel ID="Panel4" runat="server">
                                            <table style="padding: 5px; width: 100%">
                                                <tr>
                                                    <td style="text-align: center"> 
                                                    <asp:Label ID="equipmentlbl" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                                                    <br />
                                                    <asp:LinkButton ID="fullprobtn" runat="server" Font-Size="Small">Full Profile</asp:LinkButton>&nbsp;|&nbsp;
                                                    <asp:LinkButton ID="newkitbtn" runat="server" Font-Size="Small">Create Kit</asp:LinkButton>
                                                    <br /><br />
                                                    <asp:Image ID="equipmentImage" runat="server" CssClass="est_picture1" Width="200px"/></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="errmsglbl" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center">
                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlServiceKits" GridLines="None" Width="80%" CellPadding="5" HorizontalAlign="Center">
                                                            <Columns>
                                                                <asp:TemplateField Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="serviceprofileIDlbl" runat="server" Text='<%# Bind("serviceprofileID")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Service" SortExpression="name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("name") %>' ToolTip='<%# appcode.GetFilterList(Eval("serviceprofileID"))%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Size="Large" HorizontalAlign="Left" />
                                                                    <ItemStyle Font-Size="Large" HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Interval" SortExpression="interval">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("interval") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="center" Font-Size="Large" />
                                                                <ItemStyle HorizontalAlign="center" Font-Size="Large" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="editbtn" runat="server" Text="View Kit" CommandName="edit" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" Font-Size="Large" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border: thin solid #000000">
                                        <h3>ORDER HISTORY</h3>
                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"  DataSourceID="SqlKitOrdersByEquipment" AllowSorting="True" GridLines="None" Width="100%" AllowPaging="True" PageSize="10">
                                            <Columns>
                                                <asp:BoundField DataField="order_date" HeaderText="Date" SortExpression="order_date" DataFormatString="{0:d}">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Hours/Miles" SortExpression="name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("hours_miles")%>'></asp:Label>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("interval_type")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Interval" SortExpression="name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3478" runat="server" Text='<%# Eval("interval")%>'></asp:Label>
                                                        <asp:Label ID="Label6906" runat="server" Text='<%# Eval("interval_type")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order ID" SortExpression="orderID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="orderIDlbl" runat="server" Text='<%# Bind("orderID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Purchase Order" SortExpression="purchaseorder">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="orderbtn" runat="server" Text='<%# Bind("purchaseorder")%>' CommandName="Order" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" style="padding: 10px;" />                                        </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="equipmentID" SortExpression="equipmentID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="equipmentIDlbl" runat="server" Text='<%# Bind("equipmentID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="serviceprofileID" SortExpression="serviceprofileID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("serviceprofileID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border: thin solid #000000">
                                        <table style="width: 100%">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="Medium" Text="FLUIDS"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" DataSourceID="SqlFluids" EmptyDataText="NOT USED" GridLines="None" Width="100%">
                                                        <Columns>
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
                                                            <ItemStyle HorizontalAlign="Left" />
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
                                            <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border: thin solid #000000">
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Medium" Text="ALERTS"></asp:Label>
                                                    &nbsp;<asp:Button ID="alertbtn" runat="server" Height="26px" Text="ADD ALERT" />
                                                </td>
                                                <td style="text-align: right">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" DataKeyNames="alertID" DataSourceID="SqlNotes" EmptyDataText="NO ALERTS" ForeColor="Red" GridLines="None" ShowHeader="False" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="alertID" SortExpression="alertID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="alertIDlbl" runat="server" Text='<%# Eval("alertID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="date_entered" DataFormatString="{0:d}" HeaderText="Date" SortExpression="date_entered">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="note" HeaderText="Note" SortExpression="note">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="deletebtn" runat="server" Text="Delete" CommandName="Delete" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="10%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>

                                        </table>
                                    </td>
                                </tr>
                            </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlKitOrdersByEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [name], [interval], [interval_type], [orderID], [order_date], [equipmentID], [serviceprofileID], [purchaseorder], [equipment_oem], [equipment_model], [equipment_description], [assetID], [hours_miles], sum([quantity] * [price]) as cost FROM [v_kitorders] WHERE ([equipmentID] = @equipmentID) GROUP BY [name], [interval], [interval_type], [orderID], [order_date], [equipmentID], [serviceprofileID], [purchaseorder], [equipment_oem], [equipment_model], [equipment_description], [assetID], [hours_miles] ORDER BY [order_date] DESC">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="equipmentID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [equipmentID], [assetID] FROM [t_equipment] WHERE ([companyID] = @companyID) ORDER BY [assetID]">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlServiceKits" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [serviceprofileID], [name], [interval] FROM [t_serviceprofile] WHERE ([equipmentID] = @equipmentID) ORDER BY [name]">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="equipmentID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDShipTo" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [shipto], [shipID] FROM [t_ship] WHERE ([companyID] = @companyID) ORDER BY [shipto]">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlNotes" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_alert] WHERE ([equipmentID] = @equipmentID) ORDER BY [date_entered] DESC">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="equipmentID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlFluids" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [fluid], [uom], [filldate], [filltime], [name], [hours_miles], [note], [quantity] FROM [v_fluid] WHERE ([equipmentID] = @equipmentID) ORDER BY [filldate] DESC, [filltime]">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="equipmentID" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

