<%@ Page Title="Service Kits" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Kitting.aspx.vb" Inherits="Kitting" %>

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
                                At DFO, it's all about making your job easier. We provide custom designed service kits that provide signifcant logistical savings to your company!
                            </p>
                            <ul>
                                <li>Organize kits by service interval and define unique partnumbers.</li>
                                <li>Order by a single part number from one supplier.</li>
                                <li>Reduce the need for cumbersome and costly inventory.</li>
                                <li>We deliver just in time.</li>
                            </ul>
                            <p>
                                Think about how your process works and streamline it. We deliver kits to your staging area and eliminate the need to move parts in and out of your stock room. Each kit comes with a data sheet that tells your technician what service is to be performed on what equipment at what location. Contents of the kit are listed and any special instructions you have are provided.</p>
                            <p>
                                Each kit can contain any part you require including:</p>
                            <ul>
                                <li>Filters</li>
                                <li>Belts &amp; Hoses</li>
                                <li>Batteries</li>
                                <li>Oils &amp; Lubricants</li>
                                <li>Safety Items</li>
                                <li>Sample Containers</li>
                            </ul>
                            <p>
                                Used in combination with LubeTracker™, a customer need only call us with the equipment ID and the service interval and we deliver the kit you need to your door or job site.</p>
                        </td>
                        <td style="vertical-align: top">
                            <img src="Images/donaldson1.jpg" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

