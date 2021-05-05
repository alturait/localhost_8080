<%@ Page Title="Order Kit" Language="VB" MasterPageFile="~/mobile/MobileMaster.master" AutoEventWireup="false" CodeFile="Order.aspx.vb" Inherits="mobile_Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
    <tr>
        <td class="pagebody" style="vertical-align: top;">
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center;"><asp:Label ID="Label12" runat="server" Text="ASSET ID  " Font-Bold="True" Font-Size="XX-Large" Font-Italic="True" ForeColor="#0066FF"></asp:Label>
                        <asp:DropDownList ID="assetdd" runat="server" AutoPostBack="True" DataSourceID="SqlAssets" DataTextField="assetID" DataValueField="equipmentID" Font-Size="XX-Large" AppendDataBoundItems="True" Font-Bold="True">
                            <asp:ListItem Value="0">SELECT ASSET</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: right">
                        <asp:Button ID="pendingbtn" runat="server" Text="Pending" Font-Size="Large" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">&nbsp;</td>
                    <td style="vertical-align: top;">&nbsp;</td>
                
                </tr>
                <td colspan="2">
                    <table style="padding: 5px; border-style: solid; border-width: medium; width: 100%">
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 20%"><asp:Label ID="Label20" runat="server" Text="EQUIPMENT" Font-Bold="True" Font-Size="XX-Large" Font-Italic="True"></asp:Label></td>
                            <td style="vertical-align: top;">&nbsp;</td>
                            <td rowspan="8" style="text-align: center; vertical-align: top">
                                <asp:Panel ID="Panel3" runat="server">
                                    <asp:Image ID="equipmentImage" runat="server" CssClass="est_picture1" Width="250px" BorderStyle="Solid" BorderWidth="1px"/>
                                    <br />
                                    <asp:LinkButton ID="LinkButton1" runat="server">DELETE IMAGE</asp:LinkButton>
                                    <br /><br />
                                </asp:Panel>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%"><asp:Label ID="Label6" runat="server" Text="OEM" Font-Bold="False" Font-Size="XX-Large"></asp:Label></td>
                            <td style="vertical-align: top;">
                                <asp:Label ID="equipment_oemlbl" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%"><asp:Label ID="Label5" runat="server" Text="Model" Font-Bold="False" Font-Size="XX-Large"></asp:Label></td>
                            <td style="vertical-align: top;">
                                <asp:Label ID="equipment_modellbl" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%"><asp:Label ID="Label8" runat="server" Text="Year" Font-Bold="False" Font-Size="XX-Large"></asp:Label></td>
                            <td style="vertical-align: top;">
                                <asp:Label ID="equipment_yearlbl" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%"><asp:Label ID="Label7" runat="server" Text="Description" Font-Bold="False" Font-Size="XX-Large"></asp:Label></td>
                            <td style="vertical-align: top;">
                                <asp:Label ID="equipment_descriptionlbl" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="Label10" runat="server" Text="VIN/Serial" Font-Bold="False" Font-Size="XX-Large"></asp:Label></td>
                            <td style="vertical-align: top;">
                                <asp:Label ID="equipment_vinlbl" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;&nbsp;</td>
                            <td style="vertical-align: top;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="Label16" runat="server" Text="Location" Font-Bold="True" Font-Size="XX-Large" ForeColor="#0066FF"></asp:Label></td>
                            <td style="vertical-align: top;">
                                <asp:DropDownList ID="locationdd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlShipTo" DataTextField="shipto" DataValueField="shipID" Font-Bold="True" Font-Size="XX-Large">
                                    <asp:ListItem Value="0">None</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                    </table>
                </td>
                <tr>
                    <td colspan="2">
                        <table style="padding: 5px; border-style: solid; border-width: medium; width: 100%">
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2"><asp:Label ID="Label19" runat="server" Text="FILTER KIT" Font-Bold="True" Font-Size="XX-Large" Font-Italic="True"></asp:Label></td>
                                <td rowspan="6" style="text-align: center; vertical-align: top">
                                    <asp:Button ID="orderbtn" runat="server" Text="CHECK OUT" Font-Size="XX-Large" Height="250" Width="250" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="vertical-align: top; color: #FF0000; font-size: x-large">Select a kit then click CHECK OUT.</td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 20%"><asp:Label ID="Label4" runat="server" Text="Kit" Font-Bold="True" Font-Size="XX-Large" ForeColor="#0066FF"></asp:Label></td>
                                <td style="vertical-align: top;">
                                    <asp:DropDownList ID="kitdd" runat="server" AutoPostBack="True" DataSourceID="SqlKits" DataTextField="name" DataValueField="serviceprofileID" Font-Size="XX-Large" Font-Bold="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td><asp:Label ID="Label9" runat="server" Text="Interval" Font-Bold="False" Font-Size="XX-Large"></asp:Label></td>
                                <td style="vertical-align: top;">
                                    <asp:Label ID="intervallbl" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td style="vertical-align: top;">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top;" colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; color: #FF0000; font-size: x-large">
                                    To see more information or possible cross references, click on Detail.</td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top;" colspan="3">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlFilters" Font-Size="XX-Large" GridLines="None" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Part #" SortExpression="partnumber">
                                                <ItemTemplate>
                                                    <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer")%>'></asp:Label> <asp:Label ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="description">
                                                <ItemTemplate>
                                                    <asp:Label ID="itemlbl" runat="server" Text='<%# Bind("description")%>' Visible="False"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty" SortExpression="quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="qtylbl" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="partID" SortExpression="partID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="partIDlbl" runat="server" Text='<%# Bind("partID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="viewbtn" runat="server" Text="Detail" CommandName="View" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" Font-Size="XX-Large" />&nbsp;
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle Height="75px" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table style="padding: 5px; border-style: solid; border-width: medium; width: 100%">
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="Label15" runat="server" Text="FILTER KIT HISTORY" Font-Size="XX-Large" Font-Bold="True" Font-Italic="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" DataKeyNames="orderID" DataSourceID="SqlOrders" GridLines="None" Width="100%" Font-Size="XX-Large">
                                        <Columns>
                                            <asp:BoundField DataField="name" HeaderText="Service" SortExpression="name" >
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Interval" SortExpression="interval">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label13" runat="server" Text='<%# Eval("interval")%>'></asp:Label> <asp:Label ID="Label14" runat="server" Text='<%# Eval("interval_type")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="orderID" HeaderText="Order ID" SortExpression="orderID" ReadOnly="True" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="order_date" HeaderText="Date" SortExpression="order_date" DataFormatString="{0:d}" >
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="hours_miles" HeaderText="Hours" SortExpression="hours_miles" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table style="padding: 5px; border-style: solid; border-width: medium; width: 100%">
                            <tr>
                                <td>
                                    &nbsp;&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="APPROVED FLUIDS" Font-Size="XX-Large" Font-Bold="True" Font-Italic="True"></asp:Label>
                                &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style1"> </td>
                            </tr>
                            <tr>
                                <td class="auto-style1"> 
                                    <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" DataSourceID="SqlApprovedFluids" GridLines="None" Width="100%" Font-Size="XX-Large" DataKeyNames="partID">
                                        <Columns>
                                            <asp:BoundField DataField="manufacturer" HeaderText="Manufacturer" SortExpression="manufacturer">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="partnumber" HeaderText="Part Number" SortExpression="partnumber">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="quantity" HeaderText="Qty" SortExpression="quantity">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="uom" HeaderText="Units" SortExpression="uom">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1"> &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style1"> 
                                    <asp:Label ID="Label23" runat="server" Text="FLUID HISTORY" Font-Size="XX-Large" Font-Bold="True" Font-Italic="True"></asp:Label>
                                &nbsp;<asp:Button ID="fluidbtn" runat="server" Font-Size="XX-Large" Text="ADD FILL RECEIPT" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1"> &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" DataSourceID="SqlFluids" GridLines="None" Width="100%" Font-Size="XX-Large">
                                        <Columns>
                                            <asp:BoundField DataField="filldate" HeaderText="Date" SortExpression="filldate" DataFormatString="{0:d}" >
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fluid" HeaderText="Fluid" SortExpression="fluid" >
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Quantity" SortExpression="uom">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("quantity")%>'></asp:Label>&nbsp;
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("uom") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td> </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table style="padding: 5px; border-style: solid; border-width: medium; width: 100%">
                            <tr>
                                <td colspan="3">&nbsp;&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="Label21" runat="server" Text="ALERTS" Font-Bold="True" Font-Size="XX-Large" Font-Italic="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="alertID" DataSourceID="SqlNotes" Width="100%" GridLines="None" EmptyDataText="NO ALERTS" ForeColor="Red" Font-Size="X-Large" ShowHeader="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="noteID" SortExpression="noteID" Visible="False">
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
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;&nbsp;</td>
                            </tr>                        
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table style="padding: 5px; border-style: solid; border-width: medium; width: 100%">
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>                        
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="Label22" runat="server" Text="STATUS UPDATE" Font-Bold="True" Font-Size="XX-Large" Font-Italic="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="color: #FF0000; font-size: x-large">Send a quick message to key personnel to document a change in equipment status or report a problem.</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="color: #FF0000; font-size: x-large">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center">
                                    <asp:Button ID="updatebtn" runat="server" Text="REPORT STATUS" Font-Size="XX-Large" Width="400px" Height="100" /></td>
                            </tr>
                            <tr>
                                <td colspan="3" style="color: #FF0000; font-size: x-large">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table style="padding: 5px; border-style: solid; border-width: medium; width: 100%">
                            <tr>
                                <td colspan="3" class="auto-style1"></td>
                            </tr>                        
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="Label1" runat="server" Text="KIT CHANGES" Font-Bold="True" Font-Size="XX-Large" Font-Italic="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="color: #FF0000; font-size: x-large">Click on MAKE CHANGES to submit changes to this kit.</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="color: #FF0000; font-size: x-large">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center">
                                    <asp:Button ID="correctbtn" runat="server" Text="MAKE CHANGES" Font-Size="XX-Large" Width="400px" Height="100" /></td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>                        
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table style="padding: 5px; border-style: solid; border-width: medium; width: 100%">
                            <tr>
                                <td>&nbsp;</td>
                            </tr>                        
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel4" runat="server">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label17" runat="server" Text="UPLOAD PICTURE" Font-Bold="True" Font-Size="XX-Large" Font-Italic="True"></asp:Label>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="color: #FF0000; font-size: x-large">Click on Choose File then take a picture or select a file to update this profile. Picture size must be under 1MB.</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center">&nbsp;&nbsp;<asp:Button ID="uploadButton" runat="server" Font-Size="XX-Large" Height="100" Text="UPLOAD" ToolTip="Use the button on the left to choose a file on your computer then click here to upload the image into the database." Width="400px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:FileUpload ID="FileUpload1" runat="server" Font-Size="XX-Large" />
                                                    <asp:Label ID="msglbl" runat="server" Font-Size="XX-Large"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>                        
                            <tr>
                                <td style="text-align: center">&nbsp;</td>
                            </tr>                        
                            </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
    <asp:SqlDataSource ID="SqlFilters" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [partID],[manufacturer], [partnumber], [description], [quantity] FROM [v_serviceparts] WHERE ([serviceprofileID] = @serviceprofileID) AND ([selected]='True') ORDER BY [manufacturer], [partnumber]">
        <SelectParameters>
            <asp:SessionParameter Name="serviceprofileID" SessionField="mobile_serviceprofileID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlShipTo" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [shipto], [shipID] FROM [t_ship] WHERE ([companyID] = @companyID) ORDER BY [shipto]">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="mobile_companyID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlOrders" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [name], [interval_type], [interval], [orderID], [order_date], [hours_miles], sum(quantity*price) as price FROM [v_kitorders] WHERE ([equipmentID] = @equipmentID) GROUP BY [name], [interval_type], [interval], [orderID], [order_date], [hours_miles] order by order_date desc">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="mobile_equipmentID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlAssets" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [equipmentID], [assetID] FROM [v_serviceprofile] WHERE ([companyID] = @companyID) GROUP BY equipmentID,assetID ORDER BY [assetID]">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="mobile_companyID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlKits" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [serviceprofileID], [name] FROM [t_serviceprofile] WHERE ([equipmentID] = @equipmentID) ORDER BY [name]">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="mobile_equipmentID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlNotes" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_alert] WHERE ([equipmentID] = @equipmentID) ORDER BY [date_entered] DESC">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="mobile_equipmentID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlFluids" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [name], [quantity], [filldate], [filltime], [fluid], [uom] FROM [v_fluid] WHERE ([equipmentID] = @equipmentID)">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="mobile_equipmentID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlApprovedFluids" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM t_parts WHERE equipmentID = @equipmentID and part_type='Fluid' ORDER BY partnumber">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="mobile_equipmentID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    </asp:Content>

