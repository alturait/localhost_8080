<%@ Page Title="Copy Profile" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="CopyEquipment.aspx.vb" Inherits="CopyEquipment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="equiplbl" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label28" runat="server" CssClass="heading1" Text="Asset ID"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:Textbox ID="equipmentnumtb" runat="server" CssClass="est_textbox2"></asp:Textbox>
                        </td>
                        <td style="width: 15%">
                            
                        </td>
                        <td style="width: 35%">
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" CssClass="heading1" Text="Year"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="yearlbl" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label35" runat="server" CssClass="heading1" Text="Engine OEM"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="engineoemlbl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label29" runat="server" CssClass="heading1" Text="OEM"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="equipmentoemlbl" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label30" runat="server" CssClass="heading1" Text="Engine"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="enginetb" runat="server"></asp:Label></td>
                    </tr>
                    <tr>    
                        <td>
                            <asp:Label ID="Label7" runat="server" CssClass="heading1" Text="Model"></asp:Label></td>
                        <td>
                            <asp:Label ID="equipmenttb" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label31" runat="server" CssClass="heading1" Text="Options"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="optionstb" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label34" runat="server" CssClass="heading1" Text="Description"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="descriptiontb" runat="server"></asp:Label>
                        </td>
                        <td>    
                            <asp:Label ID="Label33" runat="server" CssClass="heading1" Text="VIN"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="vintb" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label37" runat="server" CssClass="heading1" Text="Location"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="locationdd" runat="server" DataSourceID="SqlLocations" DataTextField="shipto" DataValueField="shipID">
                            </asp:DropDownList>
                        </td>
                        <td>    
                            <asp:Label ID="Label40" runat="server" CssClass="heading1" Text="Interval Type"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="intervalrb" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
                                <asp:ListItem>Miles</asp:ListItem>
                                <asp:ListItem>Hours</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label41" runat="server" CssClass="heading1" Text="Fuel Type"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:RadioButtonList ID="fuelrb" runat="server" RepeatDirection="Vertical">
                                <asp:ListItem>Red Diesel</asp:ListItem>
                                <asp:ListItem>Clear Diesel</asp:ListItem>
                                <asp:ListItem>Gasoline</asp:ListItem>
                                <asp:ListItem>CNG</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label10" runat="server" CssClass="heading1" Text="DEF"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:RadioButtonList ID="defrb" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label42" runat="server" CssClass="heading1" Text="Root Interval"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Label ID="irootlbl" runat="server"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="Label6" runat="server" CssClass="heading1" Text="Equipment Notes"></asp:Label><br />
                            <asp:TextBox ID="notestb" runat="server" TextMode="MultiLine" Rows="5" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="partID" DataSourceID="SqlParts" Width="100%" GridLines="None">
                                <Columns>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lineIDlbl" runat="server" Text='<%# Eval("partID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 10%; text-align: left">
                                                        <asp:Label ID="Label4" runat="server" Text=" "></asp:Label>
                                                    </td>
                                                    <td style="width: 60%; text-align: left">
                                                        <asp:Label ID="Label21" runat="server" Text=" "></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="Label24" runat="server" Text="Qty"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: right">
                                                        <asp:Label ID="Label12" runat="server" Text="Price"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="Label8" runat="server" Text="UOM"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 10%; text-align: left">
                                                        <asp:Label ID="Label12" runat="server" Text="Part Number" CssClass="est_heading1"></asp:Label>
                                                        <asp:Label ID="part_typelbl" runat="server" Text='<%# Eval("part_type").ToString%>' Visible="False"></asp:Label>
                                                    </td>
                                                    <td style="width: 60%; text-align: left">
                                                        <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Eval("manufacturer").ToString%>'></asp:Label>&nbsp;<asp:Label ID="partnumberlbl" runat="server" Text='<%# Eval("partnumber").ToString%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="quantitylbl" runat="server" Text='<%# Eval("quantity").ToString%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: right">
                                                        <asp:Label ID="pricelbl" runat="server" Text='<%# FormatCurrency(Eval("price").ToString, 2)%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="uomlbl" runat="server" Text='<%# Eval("uom").ToString%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label15" runat="server" Text="Description" CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:Label ID="descriptionlbl"  runat="server" Text='<%# Eval("description").ToString%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label9" runat="server" Text="Alternate" CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:Label ID="alt_manufacturerlbl" runat="server" Text='<%# Eval("alt_manufacturer").ToString%>'></asp:Label>&nbsp;<asp:Label ID="alt_partnumberlbl" runat="server" Text='<%# Eval("alt_partnumber").ToString%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text="OEM" CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:Label ID="oem_manufacturerlbl" runat="server" Text='<%# Eval("oem_manufacturer").ToString%>'></asp:Label>&nbsp;<asp:Label ID="oem_partnumberlbl" runat="server" Text='<%# Eval("oem_partnumber").ToString%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlParts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM t_parts WHERE equipmentID = @equipmentID ORDER BY part_type">
                                <SelectParameters>
                                    <asp:SessionParameter Name="equipmentID" SessionField="equipmentID" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Button ID="savebtn" runat="server" Text="Save Profile" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlLocations" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT shipID,shipto FROM [t_ship] WHERE ([companyID] = @companyID) ORDER BY shipto">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlLocationsByUser" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT shipID,shipto FROM [v_user_location] WHERE ([userID] = @userID) ORDER BY shipto">
        <SelectParameters>
            <asp:SessionParameter Name="userID" SessionField="userID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

