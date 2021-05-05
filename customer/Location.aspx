<%@ Page Title="Location" Language="VB" MasterPageFile="~/customer/CustomerMaster.master" AutoEventWireup="false" CodeFile="Location.aspx.vb" Inherits="customer_Location" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            <asp:Button ID="savebtn" runat="server" Text="Save" CssClass="pushbutton1 gold" />
                            <asp:Button ID="deletebtn" runat="server" Text="Delete" CssClass="pushbutton1 gold" CausesValidation="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="msglbl" runat="server" Visible="False"></asp:Label>
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
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label21" runat="server" CssClass="est_heading1" Text="Location"></asp:Label>
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
                            &nbsp;</td>
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
                            <asp:TextBox ID="s_address3tb" runat="server" CssClass="est_textbox1"></asp:TextBox>
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
                        <td >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" CssClass="est_heading1" Text="State"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="s_statetb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" CssClass="est_heading1" Text="Zip Code"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="s_zipcodetb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label23" runat="server" CssClass="est_heading1" Text="Phone"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="s_phonetb" runat="server" CssClass="est_textbox1"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

