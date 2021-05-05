<%@ Page Title="Edit Supplier" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="EditSupplier.aspx.vb" Inherits="EST_EditSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="est_pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                        <td style="text-align: right; padding-bottom: 10px">
                            <asp:Button ID="contactsbtn" runat="server" Text="Contacts" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label10" runat="server" CssClass="est_heading1" Text="Company"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:TextBox ID="companytb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="companytb" ErrorMessage="Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label11" runat="server" CssClass="est_heading1" Text="Address"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="address1tb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="address1tb" ErrorMessage="Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:TextBox ID="address2tb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" CssClass="est_heading1" Text="City"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="citytb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="citytb" ErrorMessage="Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" CssClass="est_heading1" Text="State"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="statetb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="statetb" ErrorMessage="Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label15" runat="server" CssClass="est_heading1" Text="Zip Code"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="zipcodetb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="zipcodetb" ErrorMessage="Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label16" runat="server" CssClass="est_heading1" Text="Phone"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="phonetb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="phonetb" ErrorMessage="Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label17" runat="server" CssClass="est_heading1" Text="Fax"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="faxtb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="contactlbl" runat="server" CssClass="est_heading1" Text="Primary Contact"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="contact_emailtb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="accountinglbl" runat="server" CssClass="est_heading1" Text="AR Contact"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="billing_emailtb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="warehouselbl" runat="server" CssClass="est_heading1" Text="Shipping"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="warehouse_emailtb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="equipmentlbl" runat="server" CssClass="est_heading1" Text=" Equipment email"></asp:Label>
                        &nbsp;
                            <asp:TextBox ID="equipment_emailtb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <asp:Button ID="savebtn" runat="server" Text="Save" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:SqlDataSource ID="SqlShipTos" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM t_ship WHERE ([companyID] = @companyID) ORDER BY [shipto]">
                                <SelectParameters>
                                    <asp:SessionParameter Name="companyID" SessionField="selected_supplierID" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlContacts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM t_user WHERE ([companyID] = @companyID) ORDER BY [name]">
                                <SelectParameters>
                                    <asp:SessionParameter Name="companyID" SessionField="selected_supplierID" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

