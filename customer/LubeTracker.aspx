<%@ Page Title="Fleet Information" Language="VB" MasterPageFile="CustomerMaster.master" AutoEventWireup="false" CodeFile="LubeTracker.aspx.vb" Inherits="customer_CustomerReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="est_pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>&nbsp;
                            </td>
                        <td>

                            <asp:DropDownList ID="sortbydd" runat="server" AutoPostBack="True">
                                <asp:ListItem Value="1">By Manufacturer</asp:ListItem>
                                <asp:ListItem Value="2">By Model</asp:ListItem>
                                <asp:ListItem Value="3">By Description</asp:ListItem>
                                <asp:ListItem Value="4">By Year</asp:ListItem>
                            </asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Total Units" Font-Bold="True"></asp:Label>&nbsp;<asp:Label ID="numunitslbl" runat="server"></asp:Label></td>
                        <td>
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top" colspan="2">
                            <table style="width: 100%">
                                <asp:Panel ID="Panel1" runat="server">
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlOEMBreakdown" GridLines="None" AllowSorting="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="OEM" SortExpression="equipment_oem">
                                                        <ItemTemplate>
                                                            <a href='SearchEquipment.aspx?searchterm=<%# Eval("equipment_oem")%>'><asp:Label ID="equipment_oemlbl" runat="server" Text='<%# Eval("equipment_oem")%>'></asp:Label></a>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count" SortExpression="equipment_count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("equipment_count")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="Panel2" runat="server">
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlModelBreakdown" GridLines="None" AllowSorting="True">
                                                <Columns>
                                                    <asp:BoundField DataField="equipment_m" HeaderText="Model" SortExpression="equipment_m" >
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Count" SortExpression="equipment_count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" runat="server" Text='<%# Eval("equipment_count")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="Panel3" runat="server">
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlTypeBreakdown" GridLines="None" AllowSorting="True">
                                                <Columns>
                                                    <asp:BoundField DataField="equipment_description" HeaderText="Description" SortExpression="equipment_description" >
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Count" SortExpression="equipment_count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("equipment_count")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="Panel4" runat="server">
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataSourceID="SqlYearBreakdown" GridLines="None" AllowSorting="True">
                                                <Columns>
                                                    <asp:BoundField DataField="equipment_year" HeaderText="Year" SortExpression="equipment_year" >
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Count" SortExpression="equipment_count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("equipment_count")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            
                            <asp:SqlDataSource ID="SqlOEMBreakdown" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [equipment_oem], count(equipment_oem) as equipment_count FROM [t_equipment] WHERE ([companyID] = @companyID) GROUP BY [equipment_oem] ORDER BY [equipment_oem]">
                                <SelectParameters>
                                    <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            
                            <asp:SqlDataSource ID="SqlTypeBreakdown" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [equipment_description], count(equipment_description) as equipment_count FROM [t_equipment] WHERE ([companyID] = @companyID) GROUP BY [equipment_description] ORDER BY [equipment_description]">
                                <SelectParameters>
                                    <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            
                            <asp:SqlDataSource ID="SqlModelBreakdown" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [equipment_oem] + ' ' + [equipment_model] as equipment_m, count(equipment_model) as equipment_count FROM [t_equipment] WHERE ([companyID] = @companyID) GROUP BY [equipment_oem], [equipment_model] ORDER BY [equipment_m]">
                                <SelectParameters>
                                    <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            
                            <asp:SqlDataSource ID="SqlYearBreakdown" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [equipment_year], count(equipment_year) as equipment_count FROM [t_equipment] WHERE ([companyID] = @companyID) GROUP BY [equipment_year] ORDER BY [equipment_year] desc">
                                <SelectParameters>
                                    <asp:SessionParameter Name="companyID" SessionField="selected_companyID" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

