<%@ Page Title="New Equipment" Language="VB" MasterPageFile="Anonymous.master" AutoEventWireup="false" CodeFile="ApplicationProfile.aspx.vb" Inherits="ApplicationProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="equiplbl" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                        </td>
                        <td style="width: 35%; text-align: right;">
                            <asp:Button ID="copybtn" runat="server" Text="Save Profile" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label36" runat="server" CssClass="heading1" Text="Asset ID"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:textbox ID="assetIDtb" runat="server" Width="125"></asp:textbox>
                        </td>
                        <td style="width: 15%">
                            <asp:Label ID="Label39" runat="server" CssClass="heading1" Text="Location"></asp:Label>
                        </td>
                        <td style="width: 35%; ">
                            <asp:DropDownList ID="locationdd" runat="server" DataSourceID="SqlLocations" DataTextField="shipto" DataValueField="shipID">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label29" runat="server" CssClass="heading1" Text="OEM"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="oemtb" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                        <td style="width: 15%">
                            <asp:Label ID="Label35" runat="server" CssClass="heading1" Text="Engine OEM"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="engineoemtb" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" CssClass="heading1" Text="Model"></asp:Label>
                            </td>
                        <td>
                            <asp:Label ID="equipmenttb" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label30" runat="server" CssClass="heading1" Text="Engine Model"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="enginetb" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label34" runat="server" CssClass="heading1" Text="Description"></asp:Label>
                            </td>
                        <td>
                            <asp:Label ID="descriptiontb" runat="server" CssClass="est_label1"></asp:Label>
                            </td>
                        <td>
                            <asp:Label ID="Label37" runat="server" CssClass="heading1" Text="Year"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="yeartb" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                    </tr>
                    <tr>    
                        <td>
                            &nbsp;<asp:Label ID="Label38" runat="server" CssClass="heading1" Text="VIN"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="vintb" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label31" runat="server" CssClass="heading1" Text="Options"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="optionstb" runat="server" CssClass="est_label1"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="applicationID" DataSourceID="SqlParts" Width="100%" GridLines="None" AllowSorting="True">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lineIDlbl" runat="server" Text='<%# Eval("applicationID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 15%; text-align: left">
                                                        <asp:Label ID="Label21" runat="server" Text="Mfr"></asp:Label>
                                                    </td>
                                                    <td style="width: 15%; text-align: left">
                                                        <asp:Label ID="Label22" runat="server" Text="Part Number"></asp:Label>
                                                    </td>
                                                    <td style="width: 35%; text-align: left">
                                                        <asp:Label ID="Label4" runat="server" Text="Description"></asp:Label>
                                                    </td>
                                                    <td style="width: 15%; text-align: left">
                                                        <asp:Label ID="Label23" runat="server" Text="OEM Part Number"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="Label24" runat="server" Text="Qty"></asp:Label>
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
                                                    <td style="width: 15%; text-align: left">
                                                        <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Eval("manufacturer")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 15%; text-align: left">
                                                        <asp:Label ID="partnumberlbl" runat="server" Text='<%# Eval("partnumber")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 35%; text-align: left">
                                                        <asp:Label ID="descriptionlbl" runat="server" Text='<%# Eval("catalog_type")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 15%; text-align: left">
                                                        <asp:Label ID="oempnlbl" runat="server" Text='<%# Eval("oem_partnumber")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="quantitylbl" runat="server" Text='<%# Eval("quantity")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="uomlbl" runat="server" Text="EACH"></asp:Label>
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
                        <td colspan="4">
                            <asp:SqlDataSource ID="SqlParts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="select * from t_application_data where equipment_oem=@equipment_oem and equipment_model=@equipment_model and engine_oem=@engine_oem and engine_model=@engine_model and equipment_year=@equipment_year and equipment_vin=@equipment_vin">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="equipment_oem" QueryStringField="oem" DefaultValue=" " />
                                    <asp:QueryStringParameter Name="equipment_model" QueryStringField="model" DefaultValue=" " />
                                    <asp:QueryStringParameter Name="engine_oem" QueryStringField="eoem" DefaultValue=" " />
                                    <asp:QueryStringParameter DefaultValue=" " Name="engine_model" QueryStringField="emodel" />
                                    <asp:QueryStringParameter DefaultValue=" " Name="equipment_year" QueryStringField="year" />
                                    <asp:QueryStringParameter Name="equipment_vin" QueryStringField="vin" DefaultValue=" " />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlLocations" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [shipID], [shipto] FROM [t_ship] WHERE ([companyID] = @companyID) ORDER BY [shipto]">
                                <SelectParameters>
                                    <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlLocationsByUser" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [shipID], [shipto] FROM [v_user_location] WHERE ([userID] = @userID) ORDER BY [shipto]">
                                <SelectParameters>
                                    <asp:SessionParameter Name="userID" SessionField="userID" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

