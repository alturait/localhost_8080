<%@ Page Language="VB" AutoEventWireup="false" CodeFile="utility.aspx.vb" Inherits="main_utility" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        DELETES EQUIPMENT AT SPECIFIC LOCATION<br />
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlCompanies" DataTextField="company" DataValueField="companyID" AutoPostBack="True">
        </asp:DropDownList>
        &nbsp;COMPANY<br />
        <br />
        <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlLocations" DataTextField="shipto" DataValueField="shipID" AutoPostBack="True">
        </asp:DropDownList>
        &nbsp;LOCATIONS\<br />
        <br />
        <asp:Button ID="abtn" runat="server" Text="Delete Equipment" OnClientClick="return confirm('Delete this Equipment?');" />
    </div>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <asp:SqlDataSource ID="SqlCompanies" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [companyID], [company] FROM [t_company] WHERE ([customer] = @customer) ORDER BY [company]">
            <SelectParameters>
                <asp:Parameter DefaultValue="True" Name="customer" Type="Boolean" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlLocations" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [shipID], [shipto] FROM [t_ship] WHERE ([companyID] = @companyID)">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" Name="companyID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
