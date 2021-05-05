<%@ Page Title="Edit Company" Language="VB" MasterPageFile="Sales.master" AutoEventWireup="false" CodeFile="Company.aspx.vb" Inherits="Company" %>

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
                        </td>
                        <td style="text-align: right">
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label19" runat="server" CssClass="est_heading1" Text="Sales Rep"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:DropDownList ID="repdd" runat="server" DataSourceID="SqlDFOUsers" DataTextField="name" DataValueField="userID" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
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
                        <td style="vertical-align: top">
                            <asp:Label ID="Label18" runat="server" CssClass="est_heading1" Text="Logo"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Image ID="Image1" runat="server" CssClass="est_picture" />
                            <asp:Panel ID="Panel2" runat="server">
                                <br />
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                &nbsp;
                                <asp:Button ID="uploadButton" runat="server" CssClass="button" Text="Upload" />
                                <br />
                                <asp:Label ID="msglbl" runat="server"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="warehouselbl1" runat="server" CssClass="est_heading1" Text="Sales Tax"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="salestaxtb" runat="server" CssClass="est_textbox1" Width="30px"></asp:TextBox>&nbsp;%
                            <asp:CheckBox ID="chargetaxcb" runat="server" Text="Charge Tax" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3"><asp:Label ID="Label1" runat="server" Text="Emails" Font-Size="Medium" Font-Bold="True"></asp:Label>
                            <br />
                            
                            <asp:Label ID="equipmentlbl" runat="server" CssClass="est_heading1" Text="Equipment"></asp:Label>
                            
                        &nbsp;
                            
                            <asp:TextBox ID="equipmenttb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                            
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
                        <td colspan="2" style="text-align: center">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="savebtn" runat="server" Text="Save" CssClass="pushbutton1 gold" />
                        </td>
                        <td></td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlDFOUsers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [name], [userID] FROM [t_user] WHERE ([companyID] = @companyID)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="98" Name="companyID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

