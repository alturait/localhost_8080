<%@ Page Title="Company Information" Language="VB" MasterPageFile="~/customer/CustomerMaster.master" AutoEventWireup="false" CodeFile="Company.aspx.vb" Inherits="customer_Company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="est_pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2" style="padding-bottom: 10px">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            <asp:Button ID="savebtn" runat="server" Text="Save" CssClass="pushbutton1 gold" />
                        </td>
                        <td style="text-align: right">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <p>
                                To add locations or users to the account, please click on the appropriate link above in the blue menu bar.</p>
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
                        <td colspan="3">
                            <asp:Label ID="Label18" runat="server" CssClass="est_heading1" Text="Billing"></asp:Label>
                        </td>
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
                        <td colspan="3">
                            <p>
                                Check the box below if we should charge tax to your orders. If not please email a copy of your exemption or reseller certificate to <a href="mailto:accounting@dfofilters.com">accounting@dfofilters.com</a> .</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="warehouselbl2" runat="server" CssClass="est_heading1" Text="Charge Tax"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chargetaxcb" runat="server" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="warehouselbl1" runat="server" CssClass="est_heading1" Text="Sales Tax"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="salestaxtb" runat="server" CssClass="est_textbox1" Width="30px"></asp:Label>&nbsp;%
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            
                            &nbsp;</td>
                        <td>
                            
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            
                            <p>
                                Please enter emails to receive notifications for each department.</p>
                            <p>
                            
                            <asp:Label ID="equipmentlbl" runat="server" CssClass="est_heading1" Text="Equipment"></asp:Label>
                            
                        &nbsp;
                            
                            <asp:TextBox ID="equipmenttb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                            
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            
                            <asp:Label ID="purchasinglbl" runat="server" CssClass="est_heading1" Text="Purchasing"></asp:Label>
                            
                        </td>
                        <td>
                            
                            <asp:TextBox ID="purchasingtb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                            
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            
                            <asp:Label ID="payableslbl" runat="server" CssClass="est_heading1" Text="Payables"></asp:Label>
                            
                        </td>
                        <td>
                            
                            <asp:TextBox ID="payablestb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                            
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            
                            <asp:Label ID="receivinglbl" runat="server" CssClass="est_heading1" Text="Receiving"></asp:Label>
                            
                        </td>
                        <td>
                            
                            <asp:TextBox ID="receivingtb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                            
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            
                            &nbsp;</td>
                        <td>
                            
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            
                            <p>
                                If you have a logo to be shown on kits, please upload below.</p>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="est_heading1" Text="Company Logo"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;
                            <asp:Button ID="uploadButton" runat="server" Text="Upload" CssClass="button"/>
                            <br />
                            <asp:Label ID="msglbl" runat="server"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" CssClass="est_heading1" Text="Logo"></asp:Label>
                        </td>
                        <td>
                            <asp:Image ID="Image1" runat="server" CssClass="est_picture" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

