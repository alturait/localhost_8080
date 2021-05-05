<%@ Page Title="Users" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="Contacts.aspx.vb" Inherits="Contacts" %>

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
                                    <asp:BoundField DataField="name" HeaderText="Administrators" SortExpression="name" >
                                    <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="User ID" InsertVisible="False" SortExpression="userID">
                                        <ItemTemplate>
                                            <asp:Label ID="userIDlbl" runat="server" Text='<%# Bind("userID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="title" HeaderText="Title" SortExpression="title" >
                                    <HeaderStyle HorizontalAlign="Left" Width="20%" />
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
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlMobileUsers" GridLines="None" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="name" HeaderText="Mobile Users" SortExpression="name">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="accesscode" HeaderText="Access Code" SortExpression="accesscode">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Confirmations To" SortExpression="website_userID">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# appcode.GetUserName(Eval("website_userID"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:SqlDataSource ID="SqlCustomerContacts" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM t_user WHERE ([companyID] = @companyID) ORDER BY [name]">
                                <SelectParameters>
                                    <asp:SessionParameter Name="companyID" SessionField="selected_companyID" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlMobileUsers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [website_userID], [accesscode], [email], [name] FROM [t_mobileuser] WHERE ([website_companyID] = @website_companyID) ORDER BY [name]">
                                <SelectParameters>
                                    <asp:SessionParameter Name="website_companyID" SessionField="selected_companyID" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

