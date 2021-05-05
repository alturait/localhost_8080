<%@ Page Title="Sales Flyer" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="Flyer.aspx.vb" Inherits="main_Flyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="est_pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="pagelbl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="flyerdd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlFlyers" DataTextField="title" DataValueField="flyerID">
                                <asp:ListItem Value="0">Select Flyer</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="editbtn" runat="server" Text="Edit" CssClass="pushbutton1 gold" />
                            <asp:Button ID="newbtn" runat="server" Text="New" CssClass="pushbutton1 gold" />
                        </td>
                        <td style="text-align: right">
                            <asp:TextBox ID="emailtb" runat="server" Width="300px"></asp:TextBox>
                            <asp:Button ID="sendtestbtn" runat="server" Text="Send Test" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label1" runat="server" Text="Attachment: "></asp:Label>
                            <asp:Label ID="pdflbl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                            <asp:Label ID="flyerlbl" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlFlyers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [flyerID], [title] FROM [t_flyer] ORDER BY [title]"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

