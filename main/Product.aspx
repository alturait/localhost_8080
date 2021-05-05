<%@ Page Title="Edit Product" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="Product.aspx.vb" Inherits="Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table class="pagetable">
        <tr>
            <td class="est_pagebody" style="vertical-align: top">
                <table style="width: 100%">
                   <tr>
                       <td>
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                       </td>
                       <td style="text-align: right">
                            
                       </td>
                   </tr> 
                   <tr>
                        <td style="vertical-align: top;">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label55" runat="server" CssClass="heading1" Text="Featured"></asp:Label>
                                        </td>
                                    <td>
                                        <asp:CheckBox ID="featurecb" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" CssClass="heading1" Text="Clearance"></asp:Label>
                                        </td>
                                    <td>
                                        <asp:CheckBox ID="clearancecb" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:DropDownList ID="categorydd" runat="server" DataSourceID="SqlCategory" DataTextField="category" DataValueField="categoryID" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Select Category</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label54" runat="server" CssClass="heading1" Text="Category"></asp:Label>
                                        </td>
                                    <td>
                                        <asp:Label ID="categorylbl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" CssClass="heading1" Text="Manufacturer"></asp:Label>
                                        *</td>
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
                                        *</td>
                                    <td>
                                        <asp:TextBox ID="partnumbertb" runat="server" CssClass="est_textbox2" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label44" runat="server" CssClass="heading1" Text="UPC Code"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="upctb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label42" runat="server" CssClass="heading1" Text="Item"></asp:Label>
                                        *</td>
                                    <td>
                                        <asp:TextBox ID="itemtb" runat="server" CssClass="est_textbox2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top">
                                        <asp:Label ID="Label52" runat="server" CssClass="heading1" Text="Description"></asp:Label>
                                        </td>
                                    <td>
                                        <asp:TextBox ID="descriptiontb" runat="server" CssClass="est_textbox2" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label43" runat="server" CssClass="heading1" Text="Package"></asp:Label>
                                        </td>
                                    <td>
                                        <asp:TextBox ID="packagetb" runat="server" CssClass="est_textbox2" Width="60"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" CssClass="heading1" Text="Weight (lbs)"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="weighttb" runat="server" CssClass="est_textbox2" Width="60"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label51" runat="server" CssClass="heading1" Text="Taxable"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="taxablecb" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label47" runat="server" CssClass="heading1" Text="Rebate"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="rebatecb" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label46" runat="server" CssClass="heading1" Text="Category"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="categorytb" runat="server" CssClass="est_textbox2" Width="160"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" CssClass="heading1" Text="Core Charge"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="coredd" runat="server" AppendDataBoundItems="True" DataSourceID="SqlCoreCharges" DataTextField="partnumber" DataValueField="productID">
                                            <asp:ListItem Value="0">No Core Charge</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" CssClass="heading1" Text="Cost"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="costtb" runat="server" CssClass="est_textbox2" Width="60"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label40" runat="server" CssClass="heading1" Text="MSRP"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="msrptb" runat="server" CssClass="est_textbox2" Width="60"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label53" runat="server" CssClass="heading1" Text="Sale Price"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="salepricetb" runat="server" CssClass="est_textbox2" Width="60"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" CssClass="heading1" Text="UOM"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="uomtb" runat="server" CssClass="est_textbox2" Width="60"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label56" runat="server" CssClass="heading1" Text="Stock"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="nstockcb" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" CssClass="heading1" Text="On Hand"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="onhandtb" runat="server" CssClass="est_textbox2" Width="60"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" CssClass="heading1" Text="Min"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="mintb" runat="server" CssClass="est_textbox2" Width="60"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" CssClass="heading1" Text="Max"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="maxtb" runat="server" CssClass="est_textbox2" Width="60"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <asp:Button ID="savebtn" runat="server" CssClass="pushbutton1 gold" Text="Save" />
                                        <asp:Button ID="deletebtn" runat="server" Text="Delete" CssClass="pushbutton1 gold" OnClientClick="return confirm('Delete this product?');" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="vertical-align: top; text-align: center;">
                            <asp:Label ID="Label6" runat="server" Text="Picture" Font-Bold="True" Font-Size="Medium"></asp:Label><br /><br />
                            <asp:Image ID="Image1" runat="server" CssClass="est_picture" /><br />
                            <asp:Label ID="picturelbl" runat="server" Text="Label"></asp:Label>
                            <asp:Panel ID="Panel2" runat="server">
                                <br />
                                <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;
                                <asp:Button ID="uploadButton" runat="server" Text="Upload" CssClass="button"/>
                                <br />
                                <asp:Label ID="msglbl" runat="server"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlManufacturers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer] FROM [t_product] GROUP BY manufacturer ORDER BY [manufacturer]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCoreCharges" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT productID, partnumber FROM t_product WHERE manufacturer = @manufacturer AND item = 'CORE CHARGE' ORDER BY msrp">
        <SelectParameters>
            <asp:ControlParameter ControlID="manufacturerdd" Name="manufacturer" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCategory" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [categoryID], STR([parentID]) + '-' + [category] as category FROM [t_category] ORDER BY [parentID], [category]"></asp:SqlDataSource>
    </asp:Content>

