﻿<%@ Page Title="Shipping" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="ShippingHome.aspx.vb" Inherits="main_ShippingHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            
                        </td>
                        <td style="text-align: right">
                           
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <table style="width: 100%">
                                <tr>
                                    <td><asp:Label ID="Label2" runat="server" Text="Open Orders" CssClass="est_heading1"></asp:Label></td>
                                    <td style="text-align: right"><asp:Label ID="openorderslbl" runat="server"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                                                   
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

