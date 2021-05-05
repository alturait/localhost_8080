<%@ Page Title="Home" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="image-slider.js" type="text/javascript"></script>
    <link href="App_Themes/LubeTracker/slider.css" rel="stylesheet" />
    <link href="App_Themes/LubeTracker/thumbnail-slider.css" rel="stylesheet" />
    <script src="thumbnail-slider.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%" class="pagetable">
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="sliderFrame">
                    <div id="slider">
                        <img src="images/haultruck.jpg" alt="#htmlcaption1" />
                        <img src="images/excavator.jpg" />
                        <img src="images/loader.jpg" />
                        <img src="images/bulldozer.jpg" />
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top" colspan="2">
                <table style="width: 80%" align="center">
                    <tr>
                        <td style="vertical-align: top;" colspan="2">
                            <p>DFO is a full service supplier of products necessary to service heavy equipment. We pride ourselves on superior service and our ability to integrate with our customer's maintenance organizations on multiple levels. We provide customized supply solutions that improve logistics and productivity while decreasing inventory requirements and turn around time.</p>
                            <p>Eliminate the need for large inventories by ordering filter service kits delivered just in time for maintenance to be performed. Customize kits to fit your specific requirements. Order by assetID and interval and expect delivery in 24 hours. </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <h1>Key Suppliers</h1>
                            <table style="width: 100%">
                                <tr>
                                    <td><img src="Images/Logos/alemite.jpg" width="75" alt="Alemite" /></td>
                                    <td><img src="Images/Logos/bossair.jpg" width="75" alt="Bossair" /></td>
                                    <td><img src="Images/Logos/checkers.jpg" width="75" alt="Checkers" /></td>
                                    <td><img src="Images/Logos/coxreels.jpg" width="75" alt="Cox Reels" /></td>
                                    <td><img src="Images/Logos/eastpenn.jpg" width="75" alt="Deka" /></td>
                                    <td><img src="Images/Logos/donaldson.jpg" width="75" alt="Donaldson" /></td>
                                    <td><img src="Images/Logos/eagle.jpg" width="75" alt="Eagle" /></td>
                                </tr>
                                <tr>
                                    <td><img src="Images/Logos/ecco.jpg" width="75" alt="Ecco" /></td>
                                    <td><img src="Images/Logos/fillrite.jpg" width="75" alt="Fillrite" /></td>
                                    <td><img src="Images/Logos/fleetguard.png" width="75" alt="Fleetguard" /></td>
                                    <td><img src="Images/Logos/flomax.jpg" width="75" alt="Flomax" /></td>
                                    <td><img src="Images/Logos/gpi.jpg" width="75" alt="GPI" /></td>
                                    <td><img src="Images/Logos/graco.png" width="75" alt="Graco" /></td>
                                    <td><img src="Images/Logos/hannay.jpg" width="75" alt="Hannay" /></td>
                                </tr>
                                <tr>
                                    <td><img src="Images/Logos/husky.jpg" width="75" alt="Husky" /></td>
                                    <td><img src="Images/Logos/ingersollrand.jpg" width="75" alt="Ingersoll Rand" /></td>
                                    <td><img src="Images/Logos/lincoln.jpg" width="75" alt="Lincoln" /></td>
                                    <td><img src="Images/Logos/mann.jpg" width="75" alt="Mann-Hummel" /></td>
                                    <td><img src="Images/Logos/morrison.jpg" width="75" alt="Morrison" /></td>
                                    <td><img src="Images/Logos/newpig.jpg" width="75" alt="New Pig" /></td>
                                    <td><img src="Images/Logos/mobil.png" width="75" alt="Mobil" /></td>
                                </tr>
                                <tr>
                                    <td><img src="Images/Logos/opw.jpg" width="75" alt="OPW" /></td>
                                    <td><img src="Images/Logos/piusi.png" width="75" alt="Piusi" /></td>
                                    <td><img src="Images/Logos/reelcraft.jpg" width="75" alt="Reelcraft" /></td>
                                    <td><img src="Images/Logos/trux.png" width="75" alt="Trux" /></td>
                                    <td><img src="Images/Logos/valvoline.png" width="75" alt="Valvoline" /></td>
                                    <td><img src="Images/Logos/zerex.jpg" width="75" alt="Zerex" /></td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%; vertical-align: top;">
                            <h1>Products</h1>
                            <ul style="font-size: 14pt; font-weight: bold;">
                                <li>Filtration Products</li>
                                <li>Oils & Lubricants</li>
                                <li>Oil & Lubrication Equipment</li>
                                <li>Fuel Dispensing Equipment</li>
                                <li>Batteries & Starters</li>
                                <li>Shop Supplies</li>
                                <li>Safety Products</li>
                                <li>Tools & Shop Equipment</li>
                            </ul>
                        </td>
                        <td style="width: 50%; vertical-align: top;">
                            <h1>Services</h1>
                            <ul style="font-size: 14pt; font-weight: bold;">
                                <li>Daily Deliveries</li>
                                <li>Onsite Inventory Management</li>
                                <li>Service Kitting</li>
                                <li>LubeTracker Web Tool</li>
                            </ul>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                Proud Member of <a href="http://www.hdatruckpride.com" target="_blank"><img src="http://www.desertfleetoutfitters.com/Images/hdalogo.jpg" width="75" /></a>
            </td>
        </tr>
    </table>
</asp:Content>

