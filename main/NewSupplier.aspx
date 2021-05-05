<%@ Page Title="New Supplier" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="NewSupplier.aspx.vb" Inherits="EST_NewSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="3" style="padding-bottom: 10px">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
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
                        <td style="width: 15%">&nbsp;</td>
                        <td style="width: 35%">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="Label7" runat="server" Text="Billing Address" Font-Bold="True" Font-Size="Medium"></asp:Label>
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
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="Label6" runat="server" Text="Shipping Address" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            <asp:CheckBox ID="sameascb" runat="server" AutoPostBack="True" Text="Same As Billing  Address" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label21" runat="server" CssClass="est_heading1" Text="Ship To"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="shiptotb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="shiptotb" ErrorMessage="Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" CssClass="est_heading1" Text="Address"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="s_address1tb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="s_address1tb" ErrorMessage="Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:TextBox ID="s_address2tb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:TextBox ID="s_address3tb" runat="server" CssClass="address3tb"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="est_heading1" Text="City"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="s_citytb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="s_citytb" ErrorMessage="Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" CssClass="est_heading1" Text="State"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="s_statetb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="s_statetb" ErrorMessage="Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" CssClass="est_heading1" Text="Zip Code"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="s_zipcodetb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="s_zipcodetb" ErrorMessage="Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label23" runat="server" CssClass="est_heading1" Text="Phone"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="s_phonetb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="s_phonetb" ErrorMessage="Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="Label9" runat="server" Text="Contact Information" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label22" runat="server" CssClass="est_heading1" Text="Name"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="c_nametb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="c_nametb" ErrorMessage="Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label24" runat="server" CssClass="est_heading1" Text="Title"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="c_titletb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label25" runat="server" CssClass="est_heading1" Text="Phone"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="c_phonetb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="c_phonetb" ErrorMessage="Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label34" runat="server" CssClass="est_heading1" Text="Fax"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="c_faxtb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label26" runat="server" CssClass="est_heading1" Text="Email"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="c_emailtb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="c_emailtb" ErrorMessage="Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label35" runat="server" CssClass="est_heading1" Text="Password"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="c_passwordtb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="c_passwordtb" ErrorMessage="Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <asp:Button ID="submitbtn" runat="server" Text="Save" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

