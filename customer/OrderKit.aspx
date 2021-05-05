<%@ Page Title="Order Kit" Language="VB" MasterPageFile="CustomerMaster.master" AutoEventWireup="false" CodeFile="OrderKit.aspx.vb" Inherits="customer_OrderKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="LubeTracker" Font-Size="Medium" Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="equipmentlbl" runat="server" Font-Size="X-Large" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 20%;">
                            <a href="Asset.aspx">Order Kit</a><br />
                            <a href="Assets.aspx">Asset List</a><br />
                            <a href="OpenKits.aspx">On Order</a><br />
                            <a href="EquipmentOrderHistory.aspx">History</a><br />
                            <a href="PartSummary.aspx">Parts Summary</a>
                            <a href="FindEquipment.aspx">New Asset</a><br />
                        </td>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="vertical-align: top; width: 30%;">
                                        <table style="padding: 5px; width: 100%">
                                            <tr>
                                                <td style="padding: 5px; text-align: left;">
                                                    <asp:Label ID="Label6" runat="server" CssClass="heading1" Text="Asset ID" Font-Size="Medium"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td style="padding: 5px; text-align: left;">
                                                    <asp:DropDownList ID="equipmentdd" runat="server" DataSourceID="SqlEquipment" DataTextField="assetID" DataValueField="equipmentID" AutoPostBack="True" AppendDataBoundItems="True" Font-Size="Large">
                                                        <asp:ListItem Value="0">Select Asset</asp:ListItem>
                                                    </asp:DropDownList>                                                    
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="padding: 5px; text-align: left;">
                                                        <asp:Label ID="Label9" runat="server" CssClass="heading1" Text="VIN/SERIAL" Font-Size="Medium"></asp:Label>
                                                </td>
                                                <td style="padding: 5px; text-align: left;">
                                                    &nbsp;</td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="padding: 5px; text-align: left;">
                                                        <asp:Label ID="Label10" runat="server" CssClass="heading1" Text="Location" Font-Size="Medium"></asp:Label>
                                                </td>
                                                <td style="padding: 5px; text-align: left;">
                                                    &nbsp;</td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="padding: 5px; text-align: left;">
                                                        <asp:Label ID="hoursmileslbl" runat="server" CssClass="heading1" Text="Current Hours" Font-Size="Medium"></asp:Label>
                                                </td>
                                                <td style="padding: 5px; text-align: left;">
                                                    &nbsp;</td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="padding: 5px; text-align: left;">
                                                        <asp:Label ID="Label11" runat="server" CssClass="heading1" Text="Hours/Week" Font-Size="Medium"></asp:Label>
                                                </td>
                                                <td style="padding: 5px; text-align: left;">
                                                    &nbsp;</td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="padding: 5px; text-align: left;">
                                                        <asp:Label ID="Label12" runat="server" CssClass="heading1" Text="Last Kit" Font-Size="Medium"></asp:Label>
                                                </td>
                                                <td style="padding: 5px; text-align: left;">
                                                    &nbsp;</td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="padding: 5px; text-align: left;">
                                                        <asp:Label ID="Label13" runat="server" CssClass="heading1" Text="Ordered On" Font-Size="Medium"></asp:Label>
                                                </td>
                                                <td style="padding: 5px; text-align: left;">
                                                    &nbsp;</td>
                                            </tr>
                                            
                                                <tr>
                                                    <td style="padding: 5px; text-align: left;">
                                                        <asp:Label ID="hourslbl" runat="server" CssClass="heading1" Text="Current Hours" Font-Size="Medium"></asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                    <td style="padding: 5px; text-align: left;">
                                                        <asp:TextBox ID="hourstb" runat="server" Width="120px" Font-Size="Medium"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; text-align: left;">
                                                        <asp:Label ID="Label5" runat="server" CssClass="heading1" Text="PO" Font-Size="Medium"></asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                    <td style="padding: 5px; text-align: left;">
                                                        <asp:TextBox ID="potb" runat="server" Width="120px" Font-Size="Medium"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; text-align: left;">
                                                        <asp:Label ID="shiptolbl" runat="server" CssClass="heading1" Text="Ship To" Font-Size="Medium"></asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                    <td style="padding: 5px; text-align: left;">
                                                        <asp:DropDownList ID="shiptodd" runat="server" DataSourceID="SqlDShipTo" DataTextField="shipto" DataValueField="shipID" Font-Size="Medium"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; text-align: left;">
                                                        <asp:Label ID="shiptolbl0" runat="server" CssClass="heading1" Text="Need By" Font-Size="Medium"></asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                    <td style="padding: 5px; text-align: left;">
                                                        <asp:TextBox ID="deliverbytb" runat="server" Width="120px" Font-Size="Medium"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="deliverbytb" PopupButtonID="dpbtn1"></ajaxToolkit:CalendarExtender>
                                                        <asp:ImageButton ID="dpbtn1" runat="server" ImageUrl="~/Images/dp_image.jpg" ImageAlign="TextTop" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: center;">
                                                        <asp:LinkButton ID="profilebtn" runat="server" Text="Profile" Font-Size="Large" />&nbsp;|&nbsp;<asp:LinkButton ID="historybtn" runat="server" Text="History" Font-Size="Large" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: center;">
                                                        <asp:Image ID="equipmentImage" runat="server" CssClass="est_picture1"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>

                                                    </td>
                                                </tr>
                                            
                                        </table>
                                    </td>
                                    <td style="vertical-align: top">
                                        
                                            <table style="padding: 5px; width: 100%">
                                                <tr>
                                                    <td colspan="2" style="text-align: center">
                                                        <asp:Label ID="errmsglbl" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>                                    
                                                    <td colspan="2" style="text-align: center">
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: center">
                                                        <asp:Label ID="Label8" runat="server" Font-Size="Medium" ForeColor="Red" Text="Last Kit: "></asp:Label>
                                                        <asp:Label ID="datelbl" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>&nbsp;-&nbsp;
                                                        <asp:Label ID="pmlbl" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr><td colspan="2" style="text-align: center">&nbsp;</td></tr>
                                                <tr>
                                                    <td style="text-align: center; " colspan="2">
                                                        <asp:LinkButton ID="newkitbtn" runat="server">NEW KIT</asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr><td colspan="2" style="text-align: center">&nbsp;</td></tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: center">
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
                                                                <asp:TemplateField HeaderText="Cost" SortExpression="total_cost">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# FormatCurrency(appcode.GetKitCost(Eval("serviceprofileID")), 2)%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="right" Font-Size="Large" />
                                                                <ItemStyle HorizontalAlign="right" Font-Size="Large" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="addbtn" runat="server" Text="Order" CommandName="add" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" ToolTip='<%# appcode.GetFilterList(Eval("serviceprofileID"))%>' Font-Size="Large" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="editbtn" runat="server" Text="Edit" CommandName="edit" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" Font-Size="Large" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr><td colspan="2">&nbsp;</td></tr>
                                                <tr>
                                                    <td style="text-align: left; "colspan="2">To place an order, enter a PO and Date in the appropriate boxes. Hours/Miles are optional. Mouse over a service to see a list of included filters.</td>
                                                </tr>
                                                <tr><td colspan="2">&nbsp;</td></tr>
                                                <tr>
                                                    <td style="text-align: center; " colspan="2">
                                                        <asp:CheckBox ID="confirmcb" runat="server" Text="Send Email Confirmation" Font-Size="Medium" />
                                                    </td>
                                                </tr>
                                            </table>
                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>

            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [equipmentID], [assetID] FROM [t_equipment] WHERE ([companyID] = @companyID) ORDER BY [assetID]">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlServiceKits" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [serviceprofileID], [name], [interval] FROM [t_serviceprofile] WHERE ([equipmentID] = @equipmentID) ORDER BY [interval]">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="equipmentID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDShipTo" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [shipto], [shipID] FROM [t_ship] WHERE ([companyID] = @companyID) ORDER BY [shipto]">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

