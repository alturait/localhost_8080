<%@ Page Title="Welcome To DFO!" Language="VB" MasterPageFile="CustomerMaster.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="customer_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 10px; width: 50%; vertical-align: top; height: 100%;">
                            <h2>GETTING STARTED</h2>
                            <h3>LubeTracker</h3>
                            <p>
                                Helps to keep track of your equipment. Create, edit, order and track custom PM kits.Track hours, locations and service intervals with the click of a button. LubeTracker saves time and provides you with a common portal for communicating with your technicians and mechanics.</p>
                            <h3>Catalog</h3>
                            <p>
                                Find what you need. We stock thousands of products and offer access to thousands more. Search by category, part number or description. Save items to your favorites or put them in your virtual stock room.</p>
                            <h3>Stock Room</h3>
                            <p>
                                Create a virtual stock room and track mininum and maximum quantities for internal use or as a guide line for us to restock and maintain your maintenance inventory.</p>
                            <h3>Orders</h3>
                            <p>
                                View open orders and history. Expedite and track estimated deliveries.</p>
                        </td>
                        <td style="padding: 10px; width: 50%; vertical-align: top; height: 100%;">
                            <asp:Panel ID="FeathurePanel" runat="server">
                                <asp:DataList ID="DataList2" runat="server" DataKeyField="productID" DataSourceID="SqlFeatureProducts" Width="100%" GridLines="Horizontal">
                                    <ItemTemplate>
                                        <table style="width: 100%">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="itemlbl" runat="server" Text='<%# Eval("item")%>' Font-Size="Large" Font-Bold="True"></asp:Label>
                                                    <asp:Label ID="productIDlbl" runat="server" Text='<%# Eval("productID")%>' Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%; vertical-align: top;">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <p><asp:Label ID="descriptionlbl" runat="server" Text='<%# Left(Eval("description"), 255)%>'></asp:Label></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="text-align: center">
                                                                <asp:LinkButton runat="server" CommandName="more" CommandArgument='<%# Eval("productID")%>' ID="morebtn" Text="More Info" CssClass="push_button1 orange" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: center" colspan="2">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" CssClass="heading1" Text="Part Number"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="partnumberlbl" runat="server" Text='<%# Eval("partnumber")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label43" runat="server" CssClass="heading1" Text="Package"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="packagetb" runat="server" Text='<%# Eval("package")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label40" runat="server" CssClass="heading1" Text="MSRP"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="msrptb" runat="server" Text='<%# FormatCurrency(Eval("msrp"), 2)%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="text-align: center; vertical-align: top; width: 30%;">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <img alt="" src='../Images/Catalog/<%# GetPicture(Eval("partnumber"))%>.jpg' width="200" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label ID="Label48" runat="server" Text="Sale Price: " Font-Bold="True" Font-Size="Medium"></asp:Label>
                                                                <asp:Label ID="salepricelbl" runat="server" Font-Bold="True" Font-Size="Medium" Text='<%# FormatCurrency(Eval("saleprice"), 2)%>'></asp:Label>&nbsp;
                                                                <asp:Label ID="uomlbl" runat="server" Font-Bold="True" Font-Size="Medium" Text='<%# Eval("uom")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 10px; vertical-align: top;" colspan="2">

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlLocations" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [shipID], [shipto] FROM [t_ship] WHERE ([companyID] = @companyID) ORDER BY [shipto]">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlLocationsByUser" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [shipID], [shipto] FROM [v_user_location] WHERE ([userID] = @userID) ORDER BY [shipto]">
        <SelectParameters>
            <asp:SessionParameter Name="userID" SessionField="userID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlBranches" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [shipID], [shipto] FROM [t_ship] WHERE ([companyID] = @companyID) ORDER BY [shipto]">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="companyID" SessionField="vendorID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlPSSRs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [userID], [name] FROM [t_user] WHERE ([companyID] = @companyID) ORDER BY [name]">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="companyID" SessionField="vendorID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlPMAgreements" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_equipment] WHERE (([companyID] = @companyID) AND ([verified] = @verified)) ORDER BY assetID,[equipment_oem], [equipment_model]">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
            <asp:Parameter DefaultValue="True" Name="verified" Type="Boolean" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlXREF" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer], partnumber FROM [t_luberfiner_xref] WHERE luberfiner_pn=@luberfiner_pn GROUP BY manufacturer, partnumber ORDER BY [manufacturer], partnumber">
        <SelectParameters>
            <asp:ControlParameter ControlID="luberfinerpn" Name="luberfiner_pn" PropertyName="Value" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT company,equipmentID,assetID,equipment_oem + ' ' + equipment_model + ' ' + equipment_description as equipment,equipment_vin,locationID FROM [v_parts] WHERE ([companyID] = @companyID) and (assetID like '%' + @searchterm + '%' or partnumber like '%' + @searchterm + '%' or alt_partnumber like '%' + @searchterm + '%' or oem_partnumber like '%' + @searchterm + '%' or equipment_oem like '%' + @searchterm + '%' or equipment_model like '%' + @searchterm + '%' or equipment_description like '%' + @searchterm + '%' or engine_model like '%' + @searchterm + '%') GROUP BY company,equipmentID,assetID,equipment_oem,equipment_model,equipment_description,equipment_vin,locationID ORDER BY assetID">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
            <asp:QueryStringParameter Name="searchterm" QueryStringField="searchterm" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlAllEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT company,equipmentID,assetID,equipment_oem + ' ' + equipment_model + ' ' + equipment_description as equipment,equipment_vin,locationID FROM [v_parts] WHERE (assetID like '%' + @searchterm + '%' or partnumber like '%' + @searchterm + '%' or alt_partnumber like '%' + @searchterm + '%' or oem_partnumber like '%' + @searchterm + '%' or equipment_oem like '%' + @searchterm + '%' or equipment_model like '%' + @searchterm + '%' or equipment_description like '%' + @searchterm + '%' or engine_model like '%' + @searchterm + '%') GROUP BY company,equipmentID,assetID,equipment_oem,equipment_model,equipment_description,equipment_vin,locationID ORDER BY assetID">
        <SelectParameters>
            <asp:QueryStringParameter Name="searchterm" QueryStringField="searchterm" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlMfrs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer] FROM [t_luberfiner_xref] GROUP BY manufacturer ORDER BY [manufacturer]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlFeatureProducts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT TOP (1) * FROM v_catalog WHERE featured=@featured ORDER BY NEWID()">
        <SelectParameters>
            <asp:Parameter DefaultValue="True" Name="featured" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:HiddenField ID="luberfinerpn" runat="server" />
</asp:Content>

