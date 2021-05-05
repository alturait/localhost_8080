<%@ Page Title="Task List" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="TaskList.aspx.vb" Inherits="main_TaskList" %>

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
                            <asp:Button ID="savebtn" runat="server" Text="Save" />
                            <asp:Button ID="descriptionbtn" runat="server" Text="Add Description" />
                            <asp:Button ID="componentbtn" runat="server" Text="Add Component" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="descriptionID,componentID" DataSourceID="SqlTaskList" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="selectcb" checked='<%# appcode.IsTaskSelected(Session("serviceprofileID"), Eval("descriptionID")) %>' runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Component" SortExpression="component">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="componentbtn" runat="server" Text='<%# Bind("component") %>' CommandName="EditComponent" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cost" HeaderText="Cost" SortExpression="cost" DataFormatString="{0:c}" >
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="price" HeaderText="Price" SortExpression="price" DataFormatString="{0:c}" >
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="worklineID" SortExpression="worklineID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="worklineIDlbl" runat="server" Text='<%# appcode.GetWorklineID(Session("serviceprofileID"), Eval("descriptionID")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="descriptionID" SortExpression="descriptionID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="descriptionIDlbl" runat="server" Text='<%# Bind("descriptionID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="componentID" SortExpression="componentID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="componentIDlbl" runat="server" Text='<%# Bind("componentID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="editbtn" runat="server" Text="Edit" CommandName="Edit" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />&nbsp;
                                            <asp:LinkButton ID="deletebtn" runat="server" Text="Delete" CommandName="Delete" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlTaskList" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [description], [component], [descriptionID], [componentID], [cost], [price] FROM [v_workdescription] ORDER BY [component], [description]"></asp:SqlDataSource>
</asp:Content>

