<%@ Page Title="Service Kit" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="ServiceKit.aspx.vb" Inherits="EST_EditService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                        <td style="text-align: right" colspan="2">
                            <asp:Button ID="backbtn" runat="server" Text="Back" CssClass="pushbutton1 gold" />
                            <asp:Button ID="printbtn" runat="server" Text="Label" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label12" runat="server" CssClass="heading1" Text="Kit ID"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="kitIDlbl" runat="server" CssClass="est_textbox1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label42" runat="server" CssClass="heading1" Text="Kit Cost"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="totalpartstb" runat="server" CssClass="est_textbox1"></asp:Label>
                        </td>
                        <td colspan="2" style="text-align: center">
                            Enter current equipment hours to help track kit intervals.
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server" CssClass="heading1" Text="Kit Name"></asp:Label>
                        </td>
                        <td>
                            <asp:Textbox ID="servicenametb" runat="server" Width="300px"></asp:Textbox>
                        </td>
                        <td colspan="2" style="text-align: center">
                            <asp:Label ID="hourslbl" runat="server" CssClass="heading1" Text="Hours"></asp:Label>
                            <asp:TextBox ID="hourstb" runat="server" Width="80px"></asp:TextBox>
                            <asp:Button ID="orderbtn" runat="server" Text="Order Kit" CssClass="pushbutton1 gold" />                            
                            <asp:Button ID="servicebtn" runat="server" Text="Order Service" CssClass="pushbutton1 gold" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" CssClass="heading1" Text="Interval"></asp:Label>
                        </td>
                        <td>
                            <asp:Textbox ID="intervaltb" runat="server" Width="300px"></asp:Textbox>
                        </td>
                        <td style="text-align: center"></td>
                        <td style="text-align: center"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label48" runat="server" CssClass="heading1" Text="Interval Type"></asp:Label>
                        </td>
                        <td>

                            <asp:RadioButtonList ID="intervalrb" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>Miles</asp:ListItem>
                                <asp:ListItem>Hours</asp:ListItem>
                            </asp:RadioButtonList>

                        </td>
                        <td style="text-align: center" colspan="2">

                            <asp:Label ID="interval_errorlbl" runat="server" ForeColor="Red" Text="Interval must be a number" Visible="False"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label36" runat="server" CssClass="heading1" Text="Instructions"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="servicenotestb" runat="server" CssClass="est_textbox1" Rows="5" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">Check the box next to each part to be included in this service kit.</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="Label49" runat="server" Font-Bold="True" Text="Parts"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="partID" DataSourceID="SqlEquipmentParts" Width="100%" GridLines="None">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="partIDlbl" runat="server" Text='<%# Eval("partID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 15%; text-align: left">
                                                        <asp:Label ID="Label21" runat="server" Text="Mfr"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; text-align: left">
                                                        <asp:Label ID="Label22" runat="server" Text="Part Number"></asp:Label>
                                                    </td>
                                                    <td style="width: 40%; text-align: left">
                                                        <asp:Label ID="Label23" runat="server" Text="Description"></asp:Label>
                                                    </td>
                                                    <td style="width: 5%; text-align: center">
                                                        <asp:Label ID="Label24" runat="server" Text="Qty"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: right">
                                                        <asp:Label ID="Label4" runat="server" Text="Price"></asp:Label>
                                                    </td>
                                                    <td style="width: 5%; text-align: center">
                                                        <asp:Label ID="Label8" runat="server" Text="UOM"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="Label3" runat="server" Text="Select"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 15%; text-align: left">
                                                        <asp:Label ID="Label1" runat="server" Text="Primary" CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; text-align: left">
                                                        <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Eval("manufacturer")%>' CssClass="est_heading1"></asp:Label> <asp:Label ID="partnumberlbl" runat="server" Text='<%# Eval("partnumber")%>' CssClass="est_heading1"></asp:Label> (<asp:Label ID="onhandlbl" runat="server" Text='<%# appcode.GetOnHand(Eval("manufacturer"), Eval("partnumber")).ToString%>'></asp:Label>)
                                                    </td>
                                                    <td style="width: 40%; text-align: left">
                                                        <asp:Label ID="descriptionlbl"  runat="server" Text='<%# Eval("description")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 5%; text-align: center">
                                                        <asp:Label ID="quantitylbl" runat="server" Text='<%# Eval("quantity")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: right">
                                                        <asp:Label ID="pricelbl" runat="server" Text='<%# FormatCurrency(appcode.GetCompanyPrice(Session("selected_companyID"), Eval("manufacturer"), Eval("partnumber")), 2)%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 5%; text-align: center">
                                                        <asp:Label ID="uomlbl" runat="server" Text='<%# Eval("uom")%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:CheckBox ID="selectcb" runat="server" Checked='<%# appcode.IsKitItemSelected(Session("serviceprofileID"), Eval("manufacturer"), Eval("partnumber"))%>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text="Alternate" CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td colspan="6">
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("alt_manufacturer")%>'></asp:Label>
                                                        <asp:Label ID="oemlbl" runat="server" Text='<%# Eval("alt_partnumber")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text="OEM" CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td colspan="6">
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("oem_manufacturer")%>'></asp:Label>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("oem_partnumber")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="Label50" runat="server" Font-Bold="True" Text="Tasks"></asp:Label>
                        &nbsp;<asp:Button ID="tasklistbtn" runat="server" Text="Edit Task List" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlTasks" GridLines="None" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="component" HeaderText="component" SortExpression="component" />
                                    <asp:BoundField DataField="description" HeaderText="description" SortExpression="description" />
                                    <asp:TemplateField HeaderText="cost" SortExpression="cost">
                                        <ItemTemplate>
                                            <asp:Label ID="costlbl" runat="server" Text='<%# Bind("cost") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="price" SortExpression="price">
                                        <ItemTemplate>
                                            <asp:Label ID="pricelbl" runat="server" Text='<%# Bind("price") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Button ID="savebtn" runat="server" Text="Save" CssClass="pushbutton1 gold" />
                            <asp:Button ID="deletebtn" runat="server" Text="Delete" CssClass="pushbutton1 gold" />
                            <asp:CheckBox ID="showcb" runat="server" Text="Show All Parts" Visible="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlEquipmentParts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM t_parts WHERE equipmentID = @equipmentID ORDER BY partnumber">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="equipmentID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlTasks" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [component], [description], [cost], [price] FROM [v_workline] WHERE ([serviceprofileID] = @serviceprofileID) ORDER BY [componentID], [worklineID]">
        <SelectParameters>
            <asp:SessionParameter Name="serviceprofileID" SessionField="serviceprofileID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlParts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM v_kitparts WHERE serviceprofileID=@serviceprofileID ORDER BY partnumber">
        <SelectParameters>
            <asp:SessionParameter Name="serviceprofileID" SessionField="serviceprofileID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:HiddenField ID="kitcode" runat="server" />
</asp:Content>

