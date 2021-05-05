﻿<%@ Page Title="Equipment Profile" Language="VB" MasterPageFile="~/customer/CustomerMaster.master" AutoEventWireup="false" CodeFile="EquipmentProfile.aspx.vb" Inherits="customer_EquipmentProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                           <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                            <asp:Button ID="backbtn" runat="server" Text="Back" CssClass="pushbutton1 gold" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: right;">
                            <asp:Button ID="copybtn" runat="server" Text="Copy Asset" CssClass="pushbutton1 gold" ToolTip="Copy Profile" />
                        &nbsp;<asp:Button ID="editbtn" runat="server" Text="Edit Asset Information" CssClass="pushbutton1 gold" ToolTip="Edit Asset Info" />
                            </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 50%; vertical-align: top;">
                            <table style="width: 100%">
                                <tr>
                                    <td><asp:Label ID="Label28" runat="server" CssClass="heading1" Text="Asset ID"></asp:Label>*</td>
                                    <td><asp:Label ID="equipmentnumtb" runat="server" CssClass="est_textbox2"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label37" runat="server" CssClass="heading1" Text="Location"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="locationtb" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label33" runat="server" CssClass="heading1" Text="VIN/Serial"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="vintb" runat="server"></asp:Label>
                                        </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label30" runat="server" CssClass="heading1" Text="Engine"></asp:Label>
                                    </td>
                                    <td style="vertical-align: top">
                                        <asp:Label ID="enginetb" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>    
                                    <td>
                                        <asp:Label ID="Label39" runat="server" CssClass="heading1" Text="Engine S/N"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="enginesntb" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label31" runat="server" CssClass="heading1" Text="Options"></asp:Label>
                                    </td>
                                    <td style="vertical-align: top">
                                        <asp:Label ID="optionstb" runat="server" CssClass="est_textbox2"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="intervallbl" runat="server" CssClass="heading1" Text="Hours/Miles"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="hours_milestb" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top">
                                        <asp:Label ID="ophourslbl" runat="server" CssClass="heading1" Text="Operating Hours/Week"></asp:Label>
                                    </td>
                                    <td style="vertical-align: top">
                                        <asp:Label ID="ophourstb" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top">
                                        
                                    </td>
                                    <td style="vertical-align: top">
                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="2" style="vertical-align: top">
                            <asp:Image ID="equipmentImage" runat="server" CssClass="est_picture1"/>
                            <p>
                                <asp:Label ID="notestb" runat="server"></asp:Label>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                        <td colspan="2" style="text-align: right">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="1">
                            <asp:Label ID="Label4" runat="server" Text="Filters" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                        <td colspan="2"></td>
                        <td colspan="1" style="text-align: right">                           
                            <asp:Button ID="addfilterbtn" runat="server" Text="Add Filter To Profile" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="partID" DataSourceID="SqlFilters" Width="100%" GridLines="None" ShowHeader="False" BorderWidth="1px" EmptyDataText="No Filters listed for this asset.">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 10%; text-align: left">
                                                        <asp:Label ID="Label21" runat="server" Text="Mfr"></asp:Label>
                                                    </td>
                                                    <td style="width: 70%; text-align: left">
                                                        <asp:Label ID="Label22" runat="server" Text="Part Number"></asp:Label>
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
                                                    <td>
                                                        <asp:Label ID="Label11" runat="server" Text="Description" CssClass="heading1"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="descriptionlbl" runat="server" Text='<%# Eval("description")%>'></asp:Label>
                                                        <asp:Label ID="parttypelbl" runat="server" Text='<%# Eval("part_type")%>' Visible="False"></asp:Label>
                                                    </td>
                                                    <td colspan="2" style="text-align: right">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 10%; text-align: left">
                                                        <asp:Label ID="Label14" runat="server" Text="Primary" CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td style="width: 70%; text-align: left">
                                                        <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Eval("manufacturer")%>'></asp:Label>
                                                        <a href='CatalogPage.aspx?productID=<%# appcode.GetProductID(Eval("manufacturer"), Eval("partnumber"))%>'><asp:Label ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label></a>
                                                        (DFO: <asp:Label ID="onhandlbl" runat="server" Text='<%# appcode.GetOnHand(Eval("manufacturer"), Eval("partnumber")).ToString%>'></asp:Label>)
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="quantitylbl" runat="server" Text='<%# Eval("quantity")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="pricelbl" runat="server" Text='<%# FormatCurrency(appcode.GetCompanyPrice(Session("selected_companyID"), Eval("manufacturer"), Eval("partnumber").ToString),2)%>'></asp:Label>&nbsp;
                                                        <asp:Label ID="uomlbl" runat="server" Text='<%# Eval("uom")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label12" runat="server" Text="Alternate "></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("alt_manufacturer")%>'></asp:Label>
                                                        <asp:Label ID="Label13" runat="server" Text='<%# Eval("alt_partnumber")%>'></asp:Label>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text="OEM "></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label15" runat="server" Text='<%# Eval("oem_manufacturer")%>'></asp:Label>&nbsp;
                                                        <asp:Label ID="oemlbl" runat="server" Text='<%# Eval("oem_partnumber")%>'></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="partIDlbl" runat="server" Text='<%# Eval("partID")%>' Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label9" runat="server" Text="Notes" CssClass="heading1"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="noteslbl" runat="server" Text='<%# Eval("notes")%>'></asp:Label>
                                                    </td>
                                                    <td colspan="2" style="text-align: right">
                                                        <asp:LinkButton ID="editbtn" runat="server" Text="Edit" CommandName="Edit" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                                        <asp:LinkButton ID="deletebtn" runat="server" Text="Remove" CommandName="Remove" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
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
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label5" runat="server" Text="Fluids" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                        <td colspan="2" style="text-align: right">                          
                            <asp:Button ID="addfluidbtn" runat="server" Text="Add Fluid" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">

                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="partID" DataSourceID="SqlFluids" Width="100%" GridLines="None" ShowHeader="False" BorderWidth="1px" EmptyDataText="No Fluids listed for this asset.">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 10%; text-align: left">
                                                        <asp:Label ID="Label42" runat="server" Text="Mfr"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; text-align: left">
                                                        <asp:Label ID="Label43" runat="server" Text="Part Number"></asp:Label>
                                                    </td>
                                                    <td style="width: 45%; text-align: left">
                                                        <asp:Label ID="Label44" runat="server" Text="Description"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="Label45" runat="server" Text="Qty"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="Label46" runat="server" Text="UOM"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 10%; text-align: left">
                                                        <asp:Label ID="Label47" runat="server" Text="Primary" CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; text-align: left">
                                                        <asp:Label ID="manufacturerlbl0" runat="server" Text='<%# Eval("manufacturer")%>'></asp:Label>
                                                        <asp:Label ID="partnumberlbl0" runat="server" Text='<%# Eval("partnumber")%>' CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td style="width: 45%; text-align: left">
                                                        <asp:Label ID="descriptionlbl0"  runat="server" Text='<%# Eval("description")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="quantitylbl0" runat="server" Text='<%# Eval("quantity")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="pricelbl0" runat="server" Text='<%# FormatCurrency(appcode.GetCompanyPrice(Session("selected_companyID"), Eval("manufacturer"), Eval("partnumber").ToString),2)%>'></asp:Label>&nbsp;
                                                        <asp:Label ID="uomlbl0" runat="server" Text='<%# Eval("uom")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label48" runat="server" Text="Alternate "></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label49" runat="server" Text='<%# Eval("alt_manufacturer")%>'></asp:Label>
                                                        <asp:Label ID="Label50" runat="server" Text='<%# Eval("alt_partnumber")%>'></asp:Label>
                                                    </td>
                                                    <td></td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="parttypelbl0" runat="server" Text='<%# Eval("part_type")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label51" runat="server" Text="OEM "></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label52" runat="server" Text='<%# Eval("oem_manufacturer")%>'></asp:Label>&nbsp;
                                                        <asp:Label ID="oemlbl0" runat="server" Text='<%# Eval("oem_partnumber")%>'></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="partIDlbl0" runat="server" Text='<%# Eval("partID")%>' Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label53" runat="server" Text="Notes" CssClass="heading1"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="noteslbl0" runat="server" Text='<%# Eval("notes")%>'></asp:Label>
                                                    </td>
                                                    <td colspan="2" style="text-align: right">
                                                        <asp:LinkButton ID="editbtn0" runat="server" Text="Edit" CommandName="Edit" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                                        <asp:LinkButton ID="deletebtn0" runat="server" Text="Remove" CommandName="Remove" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
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
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label6" runat="server" Text="Belts & Hoses" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                        <td colspan="2" style="text-align: right">                          
                            <asp:Button ID="addbelthosebtn" runat="server" Text="Add Belt-Hose" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">

                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="partID" DataSourceID="SqlBelts" Width="100%" GridLines="None" ShowHeader="False" BorderWidth="1px" EmptyDataText="No Belts-Hoses listed for this asset.">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 10%; text-align: left">
                                                        <asp:Label ID="Label54" runat="server" Text="Mfr"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; text-align: left">
                                                        <asp:Label ID="Label55" runat="server" Text="Part Number"></asp:Label>
                                                    </td>
                                                    <td style="width: 45%; text-align: left">
                                                        <asp:Label ID="Label56" runat="server" Text="Description"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="Label57" runat="server" Text="Qty"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="Label58" runat="server" Text="UOM"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 10%; text-align: left">
                                                        <asp:Label ID="Label59" runat="server" Text="Primary" CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; text-align: left">
                                                        <asp:Label ID="manufacturerlbl1" runat="server" Text='<%# Eval("manufacturer")%>'></asp:Label>
                                                        <asp:Label ID="partnumberlbl1" runat="server" Text='<%# Eval("partnumber")%>' CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td style="width: 45%; text-align: left">
                                                        <asp:Label ID="descriptionlbl1"  runat="server" Text='<%# Eval("description")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="quantitylbl1" runat="server" Text='<%# Eval("quantity")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="pricelbl1" runat="server" Text='<%# FormatCurrency(appcode.GetCompanyPrice(Session("selected_companyID"), Eval("manufacturer"), Eval("partnumber").ToString),2)%>'></asp:Label>&nbsp;
                                                        <asp:Label ID="uomlbl1" runat="server" Text='<%# Eval("uom")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label60" runat="server" Text="Alternate "></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label61" runat="server" Text='<%# Eval("alt_manufacturer")%>'></asp:Label>
                                                        <asp:Label ID="Label62" runat="server" Text='<%# Eval("alt_partnumber")%>'></asp:Label>
                                                    </td>
                                                    <td></td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="parttypelbl1" runat="server" Text='<%# Eval("part_type")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label63" runat="server" Text="OEM "></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label64" runat="server" Text='<%# Eval("oem_manufacturer")%>'></asp:Label>&nbsp;
                                                        <asp:Label ID="oemlbl1" runat="server" Text='<%# Eval("oem_partnumber")%>'></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="partIDlbl1" runat="server" Text='<%# Eval("partID")%>' Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label65" runat="server" Text="Notes" CssClass="heading1"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="noteslbl1" runat="server" Text='<%# Eval("notes")%>'></asp:Label>
                                                    </td>
                                                    <td colspan="2" style="text-align: right">
                                                        <asp:LinkButton ID="editbtn1" runat="server" Text="Edit" CommandName="Edit" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                                        <asp:LinkButton ID="deletebtn1" runat="server" Text="Remove" CommandName="Remove" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
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
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label7" runat="server" Text="Other Parts" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                        <td colspan="2" style="text-align: right">                          
                            <asp:Button ID="addpartbtn" runat="server" Text="Add Part" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">

                            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataKeyNames="partID" DataSourceID="SqlOther" Width="100%" GridLines="None" ShowHeader="False" BorderWidth="1px" EmptyDataText="No Other Parts listed for this asset.">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 10%; text-align: left">
                                                        <asp:Label ID="Label66" runat="server" Text="Mfr"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; text-align: left">
                                                        <asp:Label ID="Label67" runat="server" Text="Part Number"></asp:Label>
                                                    </td>
                                                    <td style="width: 45%; text-align: left">
                                                        <asp:Label ID="Label68" runat="server" Text="Description"></asp:Label>
                                                    </td>
                                                    <td style="width: 5%; text-align: center">

                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="Label69" runat="server" Text="Qty"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="Label70" runat="server" Text="UOM"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 10%; text-align: left">
                                                        <asp:Label ID="Label71" runat="server" Text="Primary" CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%; text-align: left">
                                                        <asp:Label ID="manufacturerlbl2" runat="server" Text='<%# Eval("manufacturer")%>'></asp:Label>
                                                        <asp:Label ID="partnumberlbl2" runat="server" Text='<%# Eval("partnumber")%>' CssClass="est_heading1"></asp:Label>
                                                    </td>
                                                    <td style="width: 45%; text-align: left">
                                                        <asp:Label ID="descriptionlbl2"  runat="server" Text='<%# Eval("description")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 5%; text-align: center">
                                                        <asp:CheckBox ID="selectcb2" runat="server" />
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="quantitylbl2" runat="server" Text='<%# Eval("quantity")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; text-align: center">
                                                        <asp:Label ID="pricelbl2" runat="server" Text='<%# FormatCurrency(appcode.GetCompanyPrice(Session("selected_companyID"), Eval("manufacturer"), Eval("partnumber").ToString),2)%>'></asp:Label>&nbsp;
                                                        <asp:Label ID="uomlbl2" runat="server" Text='<%# Eval("uom")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label72" runat="server" Text="Alternate "></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label73" runat="server" Text='<%# Eval("alt_manufacturer")%>'></asp:Label>
                                                        <asp:Label ID="Label74" runat="server" Text='<%# Eval("alt_partnumber")%>'></asp:Label>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="parttypelbl2" runat="server" Text='<%# Eval("part_type")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label75" runat="server" Text="OEM "></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label76" runat="server" Text='<%# Eval("oem_manufacturer")%>'></asp:Label>&nbsp;
                                                        <asp:Label ID="oemlbl2" runat="server" Text='<%# Eval("oem_partnumber")%>'></asp:Label>
                                                    </td>
                                                    <td></td>
                                                    <td colspan="2">
                                                        <asp:Label ID="partIDlbl2" runat="server" Text='<%# Eval("partID")%>' Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label77" runat="server" Text="Notes" CssClass="heading1"></asp:Label>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:Label ID="noteslbl2" runat="server" Text='<%# Eval("notes")%>'></asp:Label>
                                                    </td>
                                                    <td colspan="2" style="text-align: right">
                                                        <asp:LinkButton ID="editbtn2" runat="server" Text="Edit" CommandName="Edit" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                                        <asp:LinkButton ID="deletebtn2" runat="server" Text="Remove" CommandName="Remove" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
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
                        <td colspan="4">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="serviceprofileID" runat="server" />
    <asp:SqlDataSource ID="SqlLubeServices" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT *, name + ' - ' + STR(interval) + ' ' + interval_type as service_name FROM [t_serviceprofile] WHERE ([equipmentID] = @equipmentID) and servicetype='Lube' ORDER BY interval">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="equipmentID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlFilters" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM t_parts WHERE equipmentID = @equipmentID and part_type='Filter' ORDER BY partnumber">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="equipmentID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlFluids" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM t_parts WHERE equipmentID = @equipmentID and part_type='Fluid' ORDER BY partnumber">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="equipmentID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlBelts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM t_parts WHERE equipmentID = @equipmentID and part_type='Belt-Hose' ORDER BY partnumber">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="equipmentID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlOther" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM t_parts WHERE equipmentID = @equipmentID and part_type='Other' ORDER BY partnumber">
        <SelectParameters>
            <asp:SessionParameter Name="equipmentID" SessionField="equipmentID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCompetitors" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer] FROM [t_luberfiner_xref] group by manufacturer ORDER BY [manufacturer]"></asp:SqlDataSource>
</asp:Content>

