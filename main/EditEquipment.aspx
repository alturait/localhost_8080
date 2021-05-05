<%@ Page Title="Edit Equipment" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="EditEquipment.aspx.vb" Inherits="main_EditEquipment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="4">
                           <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%"><asp:Label ID="Label28" runat="server" CssClass="heading1" Text="Asset ID"></asp:Label>*</td>
                        <td style="width: 35%"><asp:TextBox ID="equipmentnumtb" runat="server" CssClass="est_textbox2"></asp:TextBox></td>
                        <td style="width: 15%">&nbsp;</td>
                        <td style="width: 35%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label33" runat="server" CssClass="heading1" Text="VIN/Serial"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="vintb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                            </td>
                        <td>
                            <asp:Label ID="Label31" runat="server" CssClass="heading1" Text="Options"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="optionstb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label29" runat="server" CssClass="heading1" Text="OEM"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <ajaxToolkit:ComboBox ID="oemdd" runat="server" AutoCompleteMode="Suggest" DataSourceID="SqlEquipmentOEM" DataTextField="equipment_oem" DataValueField="equipment_oem" MaxLength="0" style="display: inline;" AppendDataBoundItems="True" AutoPostBack="True">
                            </ajaxToolkit:ComboBox>
                            </td>
                        <td>
                            <asp:Label ID="Label35" runat="server" CssClass="heading1" Text="Engine OEM"></asp:Label>
                        </td>
                        <td>
                            <ajaxToolkit:ComboBox ID="engineoemdd" runat="server" AutoCompleteMode="Suggest" DataSourceID="SqlEngineOEM" DataTextField="engine_oem" DataValueField="engine_oem" MaxLength="0" style="display: inline;" AppendDataBoundItems="True" AutoPostBack="True">
                            </ajaxToolkit:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" CssClass="heading1" Text="Year"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:DropDownList ID="yeardd" runat="server" AppendDataBoundItems="True" DataSourceID="SqlYears" DataTextField="yearID" DataValueField="yearID">
                                <asp:ListItem Value="">Unknown</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        <td>
                            <asp:Label ID="Label30" runat="server" CssClass="heading1" Text="Engine"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="enginetb" runat="server" CssClass="est_textbox2"></asp:TextBox></td>
                    </tr>
                    <tr>    
                        <td>
                            <asp:Label ID="Label7" runat="server" CssClass="heading1" Text="Model"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="equipmenttb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label39" runat="server" CssClass="heading1" Text="Engine S/N"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="enginesntb" runat="server" CssClass="est_textbox2"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label34" runat="server" CssClass="heading1" Text="Description"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="descriptiontb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                        <td>    
                            <asp:Label ID="intervallbl" runat="server" CssClass="heading1" Text="Hours/Miles"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="hours_milestb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label37" runat="server" CssClass="heading1" Text="Location"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="locationdd" runat="server" DataSourceID="SqlLocations" AppendDataBoundItems="True" DataTextField="shipto" DataValueField="shipID">
                                <asp:ListItem Value="0">Unknown</asp:ListItem>
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
                            <asp:Label ID="ophourslbl" runat="server" CssClass="heading1" Text="Operating Hours/Week"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:TextBox ID="ophourstb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label41" runat="server" CssClass="heading1" Text="Root Interval"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <p>
                            <asp:TextBox ID="irootlbl" runat="server" CssClass="est_textbox2"></asp:TextBox>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table style="width: 100%">
                                <tr>
                                    <asp:Panel ID="Panel3" runat="server">
                                        <td style="vertical-align: top; width: 320px;">
                                            <asp:Image ID="equipmentImage" runat="server" CssClass="est_picture1"/>
                                        </td>
                                    </asp:Panel>
                                    <td style="vertical-align: top">
                                        <table style="width: 100%">
                                            <asp:Panel ID="Panel4" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label16" runat="server" Text="UPLOAD PICTURE" Font-Bold="True"></asp:Label>&nbsp;
                                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                                        <asp:Label ID="msglbl" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:Button ID="uploadButton" runat="server" Text="Upload Image" CssClass="pushbutton1 gold" ToolTip="Use the button on the left to choose a file on your computer then click here to upload the image into the database."/>
                                                        <asp:Button ID="deletebtn" runat="server" Text="Delete Image" CssClass="pushbutton1 gold" ToolTip="Delete the image."/>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="Label6" runat="server" CssClass="heading1" Text="Notes"></asp:Label>
                                                    <asp:TextBox ID="notestb" runat="server" TextMode="MultiLine" Rows="5" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Button ID="savebtn" runat="server" Text="Save" CssClass="pushbutton1 gold" ToolTip="Save this profile and return to the asset list." />
                            <asp:Button ID="cancelbtn" runat="server" Text="Delete" CssClass="pushbutton1 gold" ToolTip="Delete this profile and all associated service information. This action cannot be undone." OnClientClick="return confirm('Delete this equipment profile?');" />                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="serviceprofileID" runat="server" />
    <asp:SqlDataSource ID="SqlEquipmentOEM" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipment_oem FROM [t_equipment] group by equipment_oem ORDER BY [equipment_oem]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlEngineOEM" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT engine_oem FROM [t_equipment] group by engine_oem ORDER BY [engine_oem]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlYears" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [yearID] FROM [t_years] ORDER BY [yearID] DESC"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlLubeServices" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT *, name + ' - ' + STR(interval) + ' ' + interval_type as service_name FROM [t_serviceprofile] WHERE ([equipmentID] = @equipmentID) and servicetype='Lube' ORDER BY interval">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="equipmentID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlLocations" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT shipID,shipto FROM [t_ship] WHERE ([companyID] = @companyID) ORDER BY shipto">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlLocationsByUser" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [shipID], [shipto] FROM [v_user_location] WHERE ([companyID] = @companyID) and userID=@userID ORDER BY [shipto]">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
            <asp:SessionParameter Name="userID" SessionField="userID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlOEMs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipment_oem FROM t_application_data group by equipment_oem ORDER BY equipment_oem ">
    </asp:SqlDataSource>                
</asp:Content>

