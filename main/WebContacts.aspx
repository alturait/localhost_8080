<%@ Page Title="Users" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="WebContacts.aspx.vb" Inherits="main_WebContacts" %>

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
                            <asp:Button ID="newcontactbtn" runat="server" Text="New Contact" CssClass="pushbutton1 gold" />
                        </td>
                        <td>

                        </td>
                        <td style="text-align: right">
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlCustomerContacts" DataKeyNames="userID" GridLines="None" Width="100%" AllowSorting="True">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="userID" InsertVisible="False" SortExpression="userID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="userIDlbl" runat="server" Text='<%# Bind("userID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="name" HeaderText="Contacts" SortExpression="name" >
                                    <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="title" HeaderText="Title" SortExpression="title" >
                                    <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="c_phone" HeaderText="Phone" SortExpression="c_phone" >
                                    <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="c_fax" HeaderText="Fax" SortExpression="c_fax" >
                                    <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField>
                                    <asp:CheckBoxField DataField="administrator" HeaderText="Admin" SortExpression="administrator">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:CheckBoxField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="sendbtn" runat="server" Text="Send Login" CommandName="Send" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" style="padding: 10px;" />
                                            <asp:LinkButton ID="editbtn" runat="server" Text="Edit" CommandName="Edit" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" style="padding: 10px;" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="15%" />
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
                            <asp:SqlDataSource ID="SqlCustomerContacts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM t_user WHERE ([companyID] = @companyID) ORDER BY [name]">
                                <SelectParameters>
                                    <asp:SessionParameter Name="companyID" SessionField="companyID" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

