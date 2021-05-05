<%@ Page Title="Customer Sales" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="AccountsByRep.aspx.vb" Inherits="main_AccountsByRep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="est_pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                        <td>

                        </td>
                        <td style="text-align: right">
                            
                            <asp:DropDownList ID="repdd" runat="server" AutoPostBack="True">
                                <asp:ListItem>All</asp:ListItem>
                                <asp:ListItem>Ken Gardner</asp:ListItem>
                                <asp:ListItem>Daniel Kasitch</asp:ListItem>
                            </asp:DropDownList>
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlCustomers" DataKeyNames="companyID" GridLines="None" Width="100%" AllowSorting="True" ShowFooter="True">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:BoundField DataField="name" HeaderText="Rep" SortExpression="name" >
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="companyID" SortExpression="companyID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="companyIDlbl" runat="server" Text='<%# Bind("companyID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="repID" HeaderText="repID" SortExpression="repID" Visible="False">
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Customer" SortExpression="company">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="viewbtn" runat="server" Text='<%# Eval("company")%>' CommandName="View" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="city" HeaderText="City" SortExpression="city" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="state" HeaderText="State" SortExpression="state" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="c_phone" HeaderText="Phone" SortExpression="c_phone" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Sales MTD">
                                        <ItemTemplate>
                                            <asp:Label ID="salesmtdlbl" runat="server" Text=''></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="salesmtdtotallbl" runat="server" CssClass="est_heading1"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales YTD" SortExpression="sales">
                                        <ItemTemplate>
                                            <asp:Label ID="salesytdlbl" runat="server" Text='<%# Bind("sales", "{0:c}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="salesytdtotallbl" runat="server" CssClass="est_heading1"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:SqlDataSource ID="SqlCustomers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT name,companyID,company,city,state,zipcode,c_phone,sum(quantity*price) as sales FROM v_companybyrep where customer='True' and year(order_date)=year(getDate()) GROUP BY name,companyID,company,city,state,zipcode,c_phone ORDER BY [company]">
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlCustomersByRep" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT name,companyID,company,city,state,zipcode,c_phone,sum(quantity*price) as sales FROM v_companybyrep where customer='True' and year(order_date)=year(getDate()) and name=@name GROUP BY name,companyID,company,city,state,zipcode,c_phone ORDER BY [company]">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="repdd" Name="name" PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
