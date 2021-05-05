<%@ Page Title="User" Language="VB" MasterPageFile="Sales.master" AutoEventWireup="false" CodeFile="Contact.aspx.vb" Inherits="Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="3" style="text-align: left">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="emaillbl" runat="server" ForeColor="Red" Text="That email address is already in use. Please select another." Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label10" runat="server" CssClass="est_heading1" Text="Company"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="companytb" runat="server" CssClass="est_pageheading"></asp:Label>
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label22" runat="server" CssClass="est_heading1" Text="Name"></asp:Label>
                            *</td>
                        <td>
                            <asp:TextBox ID="c_nametb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="c_nametb" ErrorMessage="Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label36" runat="server" CssClass="est_heading1" Text="Administrator"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="admincb" runat="server" AutoPostBack="True" />
                        </td>
                        <td>&nbsp;</td>
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
                            *</td>
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
                            *</td>
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
                            *</td>
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
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="savebtn" runat="server" Text="Save" CssClass="pushbutton1 gold" />
                            <asp:Button ID="deletebtn" runat="server" Text="Delete" CausesValidation="False" CssClass="pushbutton1 gold" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

