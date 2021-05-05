<%@ Page Title="Modify Kits" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="ModifyAllKits.aspx.vb" Inherits="main_ModifyAllKits" %>

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
                        <td colspan="2">
                            <table style="width: 100%">
                                <tr>
                                    <td></td>
                                    <td>Primary</td>
                                    <td>Replace With</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Manufacturer</td>
                                    <td>
                                        <asp:DropDownList ID="mfrdd" runat="server" DataSourceID="SqlMfrs" DataTextField="manufacturer" DataValueField="manufacturer" AppendDataBoundItems="True" AutoPostBack="True">
                                            <asp:ListItem Value="0">Select Manufacturer</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="rmanufactuertb" runat="server" Text="Mfr" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                    <td rowspan="2" style="text-align: center; vertical-align: middle">
                                        <asp:Button ID="replacebtn" runat="server" Text="Replace" CssClass="pushbutton1 gold" ToolTip="Replace part in all kits" />
                                        <asp:Label ID="errmsg1" runat="server" ForeColor="Red" Text="Invalid Part Number" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Part Number</td>
                                    <td>
                                        <asp:DropDownList ID="partnumberdd" runat="server" DataSourceID="SqlParts" DataTextField="partnumber" DataValueField="partnumber" AutoPostBack="True"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="rpartnumbnertb" runat="server" Text="Part Number" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:CheckBox ID="selectallcb" runat="server" AutoPostBack="True" Text="Select All" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="equipmentID" DataSourceID="SqlAssets" GridLines="None" ShowHeader="False">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="selectcb" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="assetID" HeaderText="assetID" SortExpression="assetID" />
                                                <asp:TemplateField HeaderText="equipmentID" SortExpression="equipmentID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="equipmentIDlbl" runat="server" Text='<%# Bind("equipmentID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="equipment_oem" HeaderText="equipment_oem" SortExpression="equipment_oem" />
                                                <asp:BoundField DataField="equipment_model" HeaderText="equipment_model" SortExpression="equipment_model" />
                                                <asp:BoundField DataField="equipment_description" HeaderText="equipment_description" SortExpression="equipment_description" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="viewbtn" runat="server" Text="Profile" CommandName="ViewProfile" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" style="padding: 10px;" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlParts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT partnumber,manufacturer FROM v_parts WHERE (companyID = @companyID and manufacturer=@manufacturer) GROUP BY manufacturer,partnumber ORDER BY manufacturer">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
            <asp:ControlParameter ControlID="mfrdd" Name="manufacturer" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlMfrs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer] FROM [v_parts] WHERE ([companyID] = @companyID) GROUP BY manufacturer ORDER BY [manufacturer]">
        <SelectParameters>
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlAssets" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [equipmentID], [equipment_year], [equipment_oem], [equipment_model], [equipment_description], [assetID] FROM [v_parts] WHERE (([manufacturer] = @manufacturer) AND ([partnumber] = @partnumber) AND companyID=@companyID) ORDER BY [assetID]">
        <SelectParameters>
            <asp:ControlParameter ControlID="mfrdd" Name="manufacturer" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="partnumberdd" Name="partnumber" PropertyName="SelectedValue" Type="String" />
            <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

