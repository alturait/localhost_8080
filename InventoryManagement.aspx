<%@ Page Title="Inventory Management" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="InventoryManagement.aspx.vb" Inherits="InventoryManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 500px;
            height: 437px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%" class="pagetable">
        <tr>
            <td style="vertical-align: top">
                <table style="width: 1024px">
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="pagelbl" runat="server" Font-Size="Medium" Font-Bold="True"></asp:Label>
                            <p>
                                Our customer&#39;s will always require some inventory on hand. Let us help you manage it. We can organize and keep your stock room filled with the products you need to complete your equipment services. Our representatives will:</p>
                            <ul>
                                <li>Label and organize your shelves.</li>
                                <li>Monitor equipment requirements and make sure you have all of the right parts and none of the ones you no longer need.</li>
                                <li>Make sure product levels are appropriate to your equipment needs.</li>
                                <li>Provide equipment data books that allow your technicians and mechanics to look up what they need for a particular service or repair.</li>
                            </ul>
                            <p>
                                We take care of your stock room so your people can focus on the mission critical tasks of repairing and maintaining your fleet. Using our service reduces your overhead costs and increases your worker productivity. And best of all it's FREE when you buy your filters from us.</p>
                        </td>
                        <td style="vertical-align: top">
                            &nbsp;<img alt="" src="Images/donaldson1.jpg" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

