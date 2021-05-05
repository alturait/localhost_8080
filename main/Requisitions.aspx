<%@ Page Title="Requisitions" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="Requisitions.aspx.vb" Inherits="EST_Requisitions" %>

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
                            <asp:DropDownList ID="manufacturerdd" runat="server" AutoPostBack="True" DataSourceID="SqlManufacturers" DataTextField="manufacturer" DataValueField="manufacturer">
                            </asp:DropDownList>
                        &nbsp;<asp:Label ID="pagelbl0" runat="server" Font-Bold="True" Font-Size="Medium">Select </asp:Label>
                            <asp:CheckBox ID="selectallcb" runat="server" AutoPostBack="True" Text="All" />
                            <asp:CheckBox ID="selectneeded" runat="server" AutoPostBack="True" Text="Needed" />
                            <asp:CheckBox ID="selectopen" runat="server" AutoPostBack="True" Text="Open" />
                        </td>
                        <td style="text-align: right">
                            <asp:DropDownList ID="podd" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlPOs" DataTextField="po" DataValueField="poID" Height="16px">
                                <asp:ListItem Value="0">New PO</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="addPObtn" runat="server" CssClass="pushbutton1 gold" Text="Add To PO" />&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlMfrRequisitions" GridLines="None" Width="100%" AllowSorting="True">
                                <AlternatingRowStyle BackColor="#f7ce0a" BorderWidth="1px" />
                                <RowStyle BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Part Number" SortExpression="partnumber">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="editbtn" runat="server" Text='<%# Eval("manufacturer") + " " + Eval("partnumber")%>' CommandName="Edit" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Alternate - OH / PO">
                                        <ItemTemplate>
                                            <asp:Label ID="xreflbl" runat="server" Text='<%# appcode.GetXRefOnHand(Eval("manufacturer"), Eval("partnumber")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ordersbtn" runat="server" Text="Orders" CommandName="Orders" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="nstockcb" runat="server" Checked='<%# appcode.isNormalStock(Eval("manufacturer"), Eval("partnumber"))%>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="manufacturerlbl" runat="server" Text='<%# Bind("manufacturer") %>'></asp:Label> <asp:Label ID="partnumberlbl" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label> 
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="On Hand">
                                        <ItemTemplate>
                                            <asp:TextBox ID="onhandtb" runat="server" Text='<%# appcode.GetOnHand(Eval("manufacturer"), Eval("partnumber"))%>' Width="35"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="On PO">
                                        <ItemTemplate>
                                            <asp:Label ID="onpolbl" runat="server" Text='<%# appcode.GetOnPO(Eval("manufacturer"), Eval("partnumber"))%>' Width="35"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Required" SortExpression="quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="quantitylbl" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Needed">
                                        <ItemTemplate>
                                            <asp:Label ID="neededlbl" runat="server" Text='<%# appcode.GetNeededQuantity(Eval("quantity"), appcode.GetOnHand(Eval("manufacturer"), Eval("partnumber")))%>' Font-Bold='<%# appcode.IsNeed(Eval("manufacturer"), Eval("partnumber"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Open">
                                        <ItemTemplate>
                                            <asp:Label ID="openlbl" runat="server" Text='<%# Eval("quantity") - appcode.GetOnPO(Eval("manufacturer"), Eval("partnumber")) - appcode.GetOnHand(Eval("manufacturer"), Eval("partnumber"))%>' Font-Bold='<%# appcode.IsNeed(Eval("manufacturer"), Eval("partnumber"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="selectcb" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="costlbl" runat="server" Text='<%# appcode.GetProductCost(Eval("manufacturer"), Eval("partnumber"))%>'></asp:Label>
                                            <asp:Label ID="uomlbl" runat="server" Text='<%# appcode.GetUOM(Eval("manufacturer"), Eval("partnumber"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="updatebtn" runat="server" Text="Update Inventory" CssClass="pushbutton1 gold" />                        
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlMfrRequisitions" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT manufacturer,partnumber,sum(quantity) as quantity FROM [v_requisitions] WHERE manufacturer=@manufacturer and vendorID=@vendorID and isReturn='No' GROUP BY manufacturer,partnumber ORDER BY manufacturer,partnumber">
        <SelectParameters>
            <asp:ControlParameter ControlID="manufacturerdd" Name="manufacturer" PropertyName="SelectedValue" />
            <asp:SessionParameter Name="vendorID" SessionField="vendorID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlRequisitions" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT manufacturer,partnumber,cost,uom,sum(quantity) as quantity FROM [v_requisitions] WHERE vendorID=@vendorID and isReturn='No' GROUP BY manufacturer,partnumber,cost,uom ORDER BY manufacturer,partnumber">
        <SelectParameters>
            <asp:SessionParameter Name="vendorID" SessionField="vendorID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlManufacturers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [manufacturer] FROM [v_requisitions] WHERE vendorID=@vendorID and isReturn='No' GROUP BY manufacturer ORDER BY [manufacturer]">
        <SelectParameters>
            <asp:SessionParameter Name="vendorID" SessionField="vendorID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlPOs" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [poID], [supplier] + ' - ' + po as po FROM [t_po] where complete='False' ORDER BY po"></asp:SqlDataSource>
</asp:Content>

