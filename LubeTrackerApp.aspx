<%@ Page Title="LubeTracker™" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="LubeTrackerApp.aspx.vb" Inherits="LubeTrackerApp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                                Tired of looking up what parts you need or rummaging through old invoices to determine what items you need to perform a service on your equipment? Let us help you by utilizing our easy to use web tool that tracks all that information for you.</p>
                            <p>
                                LubeTracker™ is a powerful web tool that enables fleet maintenance organizations to manage their equipment inventory online. Easily share information with other network members. Define service requirements then easily schedule and track your services.
                            </p>
                            <ul>
                                <li>Create an equipment database that tracks every vehicle and/or piece of heavy equipment in your company&#39;s inventory. Track each piece by location and monitor status and service requirements at a glance.</li>
                                <li>Create and configure multiple services for each vehicle by defining the parts necessary and the work to be performed for each type of service. </li>
                                <li>Keep track of filters, belts, fluids and any other parts necessary to maintain your equipment.</li>
                                <li>Create detaiiled service requests and email or print them to put them in the hands of your maintenance personnel. </li>
                            </ul>
                            <p>
                                You can enter information yourself or let us conduct a parts survey on your fleet and enter the information for you. Once information is in the database it is always at your fingertips. <strong>LubeTracker™ saves you time and money!</strong></p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

