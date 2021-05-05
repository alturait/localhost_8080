<%@ Page Title="Cross Reference" Language="VB" MasterPageFile="Anonymous.master" AutoEventWireup="false" CodeFile="CrossReference.aspx.vb" Inherits="CrossReference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="searchbtn">
                                <asp:Label ID="Label1" runat="server" Text="Part Number"></asp:Label> 
                                <asp:TextBox ID="searchterm" runat="server"></asp:TextBox>
                                <asp:Button ID="searchbtn" runat="server" Text="Search" />
                                <asp:Button ID="crossbtn" runat="server" Text="Cross" />
                            </asp:Panel>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Search Term: "></asp:Label>
                            <asp:Label ID="searchtermlbl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlXref" GridLines="None" Width="100%" AllowSorting="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="productID" InsertVisible="False" SortExpression="productID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="productIDlbl" runat="server" Text='<%# Bind("productID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="vertical-align: top; text-align: left; width: 30%"><asp:Label ID="Label2" runat="server" Text="Manufacturer" CssClass="est_heading1"></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: left; width: 20%"><asp:Label ID="Label3" runat="server" Text="Part Number" CssClass="est_heading1"></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: left; width: 50%"><asp:Label ID="Label4" runat="server" Text="Description" CssClass="est_heading1"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="vertical-align: top; text-align: left; width: 30%"><asp:Label ID="manufacturerlbl" runat="server" Text='<%# Eval("manufacturer2")%>'></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: left; width: 20%"><asp:Label ID="partnumberlbl" runat="server" Text='<%# Eval("partnumber2")%>'></asp:Label></td>
                                                    <td style="vertical-align: top; text-align: left; width: 50%"><asp:Label ID="packagelbl" runat="server" Text='<%# Eval("item")%>'></asp:Label></td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlXref" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [v_product_xref] WHERE ([partnumber1] = @partnumber) ORDER BY [manufacturer2], [partnumber2]">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="pripartnumberlbl" Name="partnumber" PropertyName="Value" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:HiddenField ID="pripartnumberlbl" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

