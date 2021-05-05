<%@ Page Title="Select Equipment" Language="VB" MasterPageFile="Anonymous.master" AutoEventWireup="false" CodeFile="Applications.aspx.vb" Inherits="Applications" %>

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
                            <asp:Label ID="Label3" runat="server" CssClass="est_heading1" Text="OEM"></asp:Label>
                            <asp:DropDownList ID="oemdd" runat="server" AutoPostBack="True" DataSourceID="SqlOEMs" DataTextField="equipment_oem" DataValueField="equipment_oem" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">Select OEM</asp:ListItem>
                            </asp:DropDownList>                            
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label4" runat="server" CssClass="est_heading1" Text="Model"></asp:Label>
                            <asp:DropDownList ID="modeldd" runat="server" AutoPostBack="True" DataSourceID="SqlModels" DataTextField="equipment_model" DataValueField="equipment_model" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">Select Model</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlEquipment" Width="100%" AllowSorting="True" GridLines="None" AllowPaging="True" PageSize="50">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Equipment" SortExpression="equipment_model">
                                        <ItemTemplate>
                                            <asp:Label ID="equipment_oemlbl" runat="server" Text='<%# Bind("equipment_oem")%>'></asp:Label>&nbsp;
                                            <asp:Label ID="equipment_modellbl" runat="server" Text='<%# Bind("equipment_model")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Engine" SortExpression="engine_model">
                                        <ItemTemplate>
                                            <asp:Label ID="engine_oemlbl" runat="server" Text='<%# Bind("engine_oem")%>'></asp:Label>&nbsp;
                                            <asp:Label ID="engine_modellbl" runat="server" Text='<%# Bind("engine_model")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" SortExpression="equipment_description">
                                        <ItemTemplate>
                                            <asp:Label ID="descriptionlbl" runat="server" Text='<%# Bind("equipment_description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Year" SortExpression="equipment_year">
                                        <ItemTemplate>
                                            <asp:Label ID="yearlbl" runat="server" Text='<%# Bind("equipment_year") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VIN" SortExpression="equipment_vin">
                                        <ItemTemplate>
                                            <asp:Label ID="vinlbl" runat="server" Text='<%# Bind("equipment_vin") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="15%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="selectbtn" runat="server" Text="Select" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlEquipment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipment_oem,equipment_model,equipment_description,equipment_year,equipment_option,equipment_vin,engine_oem,engine_model FROM t_application_data GROUP BY equipment_oem,equipment_model,equipment_description,equipment_year,equipment_option,equipment_vin,engine_oem,engine_model ORDER BY equipment_oem,equipment_model">
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlEquipmentByOEM" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipment_oem,equipment_model,equipment_description,equipment_year,equipment_option,equipment_vin,engine_oem,engine_model FROM t_application_data WHERE equipment_oem=@equipment_oem GROUP BY equipment_oem,equipment_model,equipment_description,equipment_year,equipment_option,equipment_vin,engine_oem,engine_model ORDER BY equipment_oem,equipment_model">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="equipment_oem" QueryStringField="oem" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlEquipmentByModel" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipment_oem,equipment_model,equipment_description,equipment_year,equipment_option,equipment_vin,engine_oem,engine_model FROM t_application_data WHERE equipment_oem=@equipment_oem and equipment_model=@equipment_model GROUP BY equipment_oem,equipment_model,equipment_description,equipment_year,equipment_option,equipment_vin,engine_oem,engine_model ORDER BY equipment_oem,equipment_model">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="equipment_oem" QueryStringField="oem" />
                        <asp:QueryStringParameter Name="equipment_model" QueryStringField="model" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlOEMs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipment_oem FROM t_application_data group by equipment_oem ORDER BY equipment_oem ">
        <SelectParameters>
            <asp:ControlParameter ControlID="oemdd" Name="equipment_oem" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>                
    <asp:SqlDataSource ID="SqlModels" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT equipment_model FROM t_application_data where equipment_oem=@equipment_oem group by equipment_model ORDER BY equipment_model ">
        <SelectParameters>
            <asp:QueryStringParameter Name="equipment_oem" QueryStringField="oem" />
        </SelectParameters>
    </asp:SqlDataSource>                
</asp:Content>

