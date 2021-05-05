<%@ Page Title="Catalog Items" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="CatalogItems.aspx.vb" Inherits="main_CatalogItems" %>

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
                        <td style="text-align: right; padding-bottom: 10px">
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">

                            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlCatalog">
                                <Columns>
                                    <asp:BoundField DataField="category" HeaderText="category" SortExpression="category" />
                                    <asp:BoundField DataField="manufacturer" HeaderText="manufacturer" SortExpression="manufacturer" />
                                    <asp:BoundField DataField="partnumber" HeaderText="partnumber" SortExpression="partnumber" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlCatalog" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer], [category], [partnumber] FROM [v_catalog] WHERE ([categoryID] <> @categoryID) GROUP BY manufacturer,category,partnumber ORDER BY category,manufacturer,partnumber">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="categoryID" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

