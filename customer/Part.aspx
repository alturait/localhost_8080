<%@ Page Title="Part" Language="VB" MasterPageFile="~/customer/CustomerMaster.master" AutoEventWireup="false" CodeFile="Part.aspx.vb" Inherits="customer_Part" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table class="pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            <asp:Button ID="submitbtn" runat="server" Text="Save" CssClass="pushbutton1 gold" />
                            <asp:Button ID="cancelbtn" runat="server" Text="Delete" CssClass="pushbutton1 gold" OnClientClick="return confirm('Delete this part?');" />
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="backbtn" runat="server" Text="Back" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="equiplbl" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 70%; vertical-align: top;">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 30%">
                                        <asp:Label ID="Label46" runat="server" CssClass="heading1" Text="Type"></asp:Label>
                                    </td>
                                    <td style="width: 70%">
                                        <asp:Label ID="typelbl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" CssClass="heading1" Text="Manufacturer"></asp:Label>
                                    </td>
                                    <td>
                                        <ajaxToolkit:ComboBox ID="manufacturerdd" runat="server" DataSourceID="SqlManufacturers" DataTextField="manufacturer" DataValueField="manufacturer" MaxLength="0" style="display: inline;">
                                        </ajaxToolkit:ComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" CssClass="heading1" Text="Part Number"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="partnumbertb" runat="server" CssClass="textbox1" AutoPostBack="True" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label47" runat="server" CssClass="heading1" Text="Alternate Mfr"></asp:Label>
                                    </td>
                                    <td>
                                        <ajaxToolkit:ComboBox ID="altmfrdd" runat="server" DataSourceID="SqlAltManufacturers" DataTextField="manufacturer" DataValueField="manufacturer" MaxLength="0" style="display: inline;">
                                        </ajaxToolkit:ComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label48" runat="server" CssClass="heading1" Text="Alternate PN"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="altpntb" runat="server" CssClass="textbox1" AutoPostBack="True" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label45" runat="server" CssClass="heading1" Text="OEM Mfr"></asp:Label>
                                    </td>
                                    <td>
                                        <ajaxToolkit:ComboBox ID="oemmfrdd" runat="server" DataSourceID="SqlOEMManufacturers" DataTextField="manufacturer" DataValueField="manufacturer" MaxLength="0" style="display: inline;">
                                        </ajaxToolkit:ComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" CssClass="heading1" Text="OEM PN"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="oempntb" runat="server" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" CssClass="heading1" Text="Description"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="descriptiontb" runat="server" CssClass="textbox4" Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" CssClass="heading1" Text="Qty"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="quantitytb" runat="server" Width="30px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" CssClass="heading1" Text="UOM"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="uomtb" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label40" runat="server" CssClass="heading1" Text="Price"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="pricetb" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top">
                                        <asp:Label ID="Label49" runat="server" CssClass="heading1" Text="Notes"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="notestb" runat="server" Rows="3" TextMode="MultiLine" Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                            <asp:Label ID="otherlbl" runat="server" Font-Bold="True" Font-Size="Medium">Equipment that uses this part:</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:GridView ID="GridView3" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlEquipment" GridLines="None" Width="90%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="equipmentID" SortExpression="equipmentID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="equipmentIDlbl" runat="server" Text='<%# Bind("equipmentID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="assetID" HeaderText="Asset ID" SortExpression="assetID">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Equipment" SortExpression="equipment_oem">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# appcode.GetEquipment(Eval("equipmentID"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="quantity" HeaderText="Qty" SortExpression="quantity">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="viewbtn" runat="server" Text="Profile" CommandName="ViewProfile" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" style="padding: 10px;" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="2" style="vertical-align: top">
                            <p><asp:Label ID="Label6" runat="server" CssClass="heading1" Text="Alternate Part Numbers"></asp:Label></p>
                            <asp:DataList ID="DataList2" runat="server" DataSourceID="SqlCompetitors">
                                <ItemTemplate>
                                    <asp:Label ID="manufacturerLabel" runat="server" Text='<%# Eval("manufacturer") %>' /> <asp:Label ID="Label4" runat="server" Text='<%# Eval("partnumber") %>' /><br />
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlManufacturers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT upper(manufacturer) as manufacturer FROM [t_parts] GROUP BY manufacturer ORDER BY [manufacturer]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlAltManufacturers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT upper(alt_manufacturer) as manufacturer FROM [t_parts] GROUP BY alt_manufacturer ORDER BY [alt_manufacturer]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlOEMManufacturers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT upper(oem_manufacturer) as manufacturer FROM [t_parts] GROUP BY oem_manufacturer ORDER BY [oem_manufacturer]"></asp:SqlDataSource>
    <asp:HiddenField ID="luberfinerpn" runat="server" />
    <asp:SqlDataSource ID="SqlCompetitors" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer], partnumber FROM [t_luberfiner_xref] WHERE luberfiner_pn=@luberfiner_pn GROUP BY manufacturer, partnumber ORDER BY [manufacturer], partnumber">
        <SelectParameters>
            <asp:ControlParameter ControlID="luberfinerpn" Name="luberfiner_pn" PropertyName="Value" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [equipmentID], [equipment_oem], [assetID], [quantity] FROM [v_parts] WHERE (([manufacturer] = @manufacturer) AND ([partnumber] = @partnumber) AND ([companyID]=@companyID)) ORDER BY [assetID]">
        <SelectParameters>
            <asp:ControlParameter ControlID="manufacturerdd" Name="manufacturer" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="partnumbertb" Name="partnumber" PropertyName="Text" Type="String" />
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

