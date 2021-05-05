<%@ Page Title="Part" Language="VB" MasterPageFile="~/mobile/MobileMaster.master" AutoEventWireup="false" CodeFile="Part.aspx.vb" Inherits="mobile_Part" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top;">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Button ID="backbtn" runat="server" Font-Size="XX-Large" Text="BACK" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 30%">
                                        &nbsp;</td>
                                    <td style="width: 70%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" CssClass="heading1" Text="Manufacturer" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="mfrlbl" runat="server" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" CssClass="heading1" Text="Part Number" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="partnumberlbl" runat="server" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label47" runat="server" CssClass="heading1" Text="Alternate Mfr" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="altmfrlbl" runat="server" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label48" runat="server" CssClass="heading1" Text="Alternate PN" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="altpnlbl" runat="server" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label45" runat="server" CssClass="heading1" Text="OEM Mfr" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="oemmfrlbl" runat="server" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" CssClass="heading1" Text="OEM PN" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="oempnlbl" runat="server" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" CssClass="heading1" Text="Description" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="descriptionlbl" runat="server" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" CssClass="heading1" Text="Qty" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="quantitylbl" runat="server" Font-Size="XX-Large"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                            <asp:Label ID="otherlbl" runat="server" Font-Bold="True" Font-Size="XX-Large">EQUIPMENT THAT USES THIS PART</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlEquipment" GridLines="None" Width="100%" Font-Size="XX-Large">
                                            <Columns>
                                                <asp:BoundField DataField="assetID" HeaderText="Asset ID" SortExpression="assetID">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Equipment" SortExpression="equipment_oem">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label49" runat="server" Text='<%# appcode.GetEquipment(Eval("equipmentID"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="quantity" HeaderText="Qty" SortExpression="quantity">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>

                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>

                            <asp:Label ID="Label50" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="POSSIBLE ALTERNATES"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataList ID="DataList2" runat="server" DataSourceID="SqlCompetitors" Font-Size="XX-Large">
                                <ItemTemplate>
                                    <asp:Label ID="manufacturerLabel" runat="server" Text='<%# Eval("manufacturer") %>' /> <asp:Label ID="Label4" runat="server" Text='<%# Eval("partnumber") %>' /><br />
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="luberfinerpn" runat="server" />
    <asp:SqlDataSource ID="SqlEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipmentID,[equipment_oem], [assetID], [quantity] FROM [v_parts] WHERE (([manufacturer] = @manufacturer) AND ([partnumber] = @partnumber) AND ([companyID]=@companyID)) ORDER BY [assetID]">
        <SelectParameters>
            <asp:ControlParameter ControlID="mfrlbl" Name="manufacturer" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="partnumberlbl" Name="partnumber" PropertyName="Text" Type="String" />
            <asp:SessionParameter Name="companyID" SessionField="mobile_companyID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCompetitors" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer], partnumber FROM [t_luberfiner_xref] WHERE luberfiner_pn=@luberfiner_pn GROUP BY manufacturer, partnumber ORDER BY [manufacturer], partnumber">
        <SelectParameters>
            <asp:ControlParameter ControlID="luberfinerpn" Name="luberfiner_pn" PropertyName="Value" />
        </SelectParameters>
    </asp:SqlDataSource>
    </asp:Content>

