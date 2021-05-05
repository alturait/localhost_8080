<%@ Page Title="Mailing List" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="MailingList.aspx.vb" Inherits="main_MailingList" %>

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
                        <td style="text-align: right">
                            <asp:DropDownList ID="flyerdd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlFlyers" DataTextField="title" DataValueField="flyerID">
                                <asp:ListItem Value="0">Select Flyer</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="sendbtn" runat="server" Text="Send To List" CssClass="pushbutton1 gold" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Name: " Font-Bold="True"></asp:Label>
                            <asp:TextBox ID="nametb" runat="server"></asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="Button1" runat="server" Text="SELECT ALL" />
                            <asp:Button ID="savebtn" runat="server" Text="SAVE LIST" />
                            <asp:Button ID="addbtn" runat="server" Text="ADD EMAIL" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="listitemID" DataSourceID="SqlList" Width="100%" GridLines="None">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="listitemID" InsertVisible="False" SortExpression="listitemID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="listitemIDlbl" runat="server" Text='<%# Bind("listitemID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company" SortExpression="company">
                                        <ItemTemplate>
                                            <asp:Label ID="companylbl" runat="server" Text='<%# Bind("company") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" SortExpression="name">
                                        <ItemTemplate>
                                            <asp:Label ID="namelbl" runat="server" Text='<%# Bind("name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email" SortExpression="email">
                                        <ItemTemplate>
                                            <asp:Label ID="emaillbl" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="selectedcb" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="editbtn" runat="server" Text="Edit" CommandName="Edit" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                            <asp:LinkButton ID="removebtn" runat="server" Text="Remove" CommandName="Remove" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlList" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_listitem] WHERE listID=@listID order by company,name,email">
                                <SelectParameters>
                                    <asp:SessionParameter Name="listID" SessionField="listID" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlFlyers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [flyerID], [title] FROM [t_flyer] ORDER BY [title]"></asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

