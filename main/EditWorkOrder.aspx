<%@ Page Title="Work Order" Language="VB" MasterPageFile="~/main/Admin.master" AutoEventWireup="false" CodeFile="EditWorkOrder.aspx.vb" Inherits="main_EditWorkOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table class="est_pagetable">
        <tr>
            <td class="pagebody" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="pagelbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>                            
                        </td>
                        <td colspan="2" style="text-align: right">
                            
                            <asp:Button ID="savebtn" runat="server" Text="Save" />
                            <asp:Button ID="cancelbtn" runat="server" Text="Delete" OnClientClick="return confirm('Delete this Work Order?');" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label28" runat="server" CssClass="heading1" Text="Work Order ID*"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="repairIDlbl" runat="server" CssClass="est_label1"></asp:Label>
                        </td>
                        <td style="width: 15%">
                            <asp:Label ID="Label39" runat="server" CssClass="heading1" Text="Received On*"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:Textbox ID="orderdatetb" runat="server"></asp:Textbox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="orderdatetb" PopupButtonID="dpbtn1"></ajaxToolkit:CalendarExtender>
                            <asp:ImageButton ID="dpbtn1" runat="server" ImageUrl="~/Images/dp_image.jpg" ImageAlign="TextTop" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label35" runat="server" CssClass="heading1" Text="Company*"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="customerdd" runat="server" AutoPostBack="True" DataSourceID="SqlCustomer" DataTextField="company" DataValueField="companyID">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label42" runat="server" CssClass="heading1" Text="Est Delivery"></asp:Label>
                        </td>
                        <td>
                            <asp:Textbox ID="est_deliverytb" runat="server"></asp:Textbox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="est_deliverytb" PopupButtonID="dpbtn2"></ajaxToolkit:CalendarExtender>
                            <asp:ImageButton ID="dpbtn2" runat="server" ImageUrl="~/Images/dp_image.jpg" ImageAlign="TextTop" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label41" runat="server" CssClass="heading1" Text="Contact*"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="customeruserdd" runat="server" DataSourceID="SqlCustomerUser" DataTextField="name" DataValueField="userID">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label43" runat="server" CssClass="heading1" Text="PO"></asp:Label>
                        </td>
                        <td>
                            <asp:Textbox ID="potb" runat="server" CssClass="est_label1"></asp:Textbox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label52" runat="server" CssClass="heading1" Text="Phone"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="totb0" runat="server" Width="200"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label60" runat="server" CssClass="heading1" Text="Approved"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="approvedcb" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label56" runat="server" CssClass="heading1" Text="Email"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="emailtb" runat="server" Width="200"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label61" runat="server" CssClass="heading1" Text="Approved By"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="approvedbytb" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            </td>
                        <td class="auto-style1">
                            </td>
                        <td class="auto-style1"></td>
                        <td class="auto-style1"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="Label72" runat="server" Text="Equipment" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label44" runat="server" CssClass="heading1" Text="Manufacturer"></asp:Label>
                        </td>
                        <td>
                            <asp:Textbox ID="manufacturertb" runat="server" CssClass="est_label1"></asp:Textbox>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label53" runat="server" CssClass="heading1" Text="Model"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Textbox ID="modeltb" runat="server" CssClass="est_label1"></asp:Textbox>
                        </td>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label54" runat="server" CssClass="heading1" Text="Serial Number"></asp:Label>
                        </td>
                        <td>
                            <asp:Textbox ID="serialnumbertb" runat="server" CssClass="est_label1"></asp:Textbox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label55" runat="server" CssClass="heading1" Text="Part Number"></asp:Label>
                        </td>
                        <td>
                            <asp:Textbox ID="partnumbertb" runat="server" CssClass="est_label1"></asp:Textbox>
                            </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label57" runat="server" CssClass="heading1" Text="Description"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="descriptiontb" runat="server" Width="100%" Rows="4" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label58" runat="server" CssClass="heading1" Text="Work Required"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="requestedtb" runat="server" Width="100%" Rows="4" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label70" runat="server" CssClass="heading1" Text="Estimate"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="estimatetb" runat="server"></asp:TextBox>
                        </td>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>    
                        <td colspan="4">
                            <asp:Label ID="Label73" runat="server" Text="Vendor" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label63" runat="server" CssClass="heading1" Text="Vendor"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="vendordd" runat="server" AutoPostBack="True" DataSourceID="SqlVendor" DataTextField="company" DataValueField="companyID" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">None</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>    
                            
                            &nbsp;</td>
                        <td>
                            
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label64" runat="server" CssClass="heading1" Text="Vendor Contact"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="vendoruserdd" runat="server" DataSourceID="SqlVendorUser" DataTextField="name" DataValueField="userID" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">None</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label74" runat="server" CssClass="heading1" Text="Sent On"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Textbox ID="sentontb" runat="server"></asp:Textbox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="sentontb" PopupButtonID="dpbtn3"></ajaxToolkit:CalendarExtender>
                            <asp:ImageButton ID="dpbtn3" runat="server" ImageUrl="~/Images/dp_image.jpg" ImageAlign="TextTop" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label75" runat="server" CssClass="heading1" Text="Returned On"></asp:Label>
                        </td>
                        <td>
                            <asp:Textbox ID="returnedontb" runat="server"></asp:Textbox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="returnedontb" PopupButtonID="dpbtn4"></ajaxToolkit:CalendarExtender>
                            <asp:ImageButton ID="dpbtn4" runat="server" ImageUrl="~/Images/dp_image.jpg" ImageAlign="TextTop" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label59" runat="server" CssClass="heading1" Text="Work Performed"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="performedtb" runat="server" Width="100%" Rows="4" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label69" runat="server" CssClass="heading1" Text="Hours"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="hourstb" runat="server"></asp:TextBox>
                        </td>
                        <td style="vertical-align: top">
                            </td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top" class="auto-style2">
                            <asp:Label ID="Label66" runat="server" CssClass="heading1" Text="Labor"></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="labortb" runat="server"></asp:TextBox>
                        </td>
                        <td style="vertical-align: top" class="auto-style2">
                            </td>
                        <td class="auto-style2">
                            </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label67" runat="server" CssClass="heading1" Text="Parts"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="partstb" runat="server"></asp:TextBox>
                        </td>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label68" runat="server" CssClass="heading1" Text="Total"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="totaltb" runat="server"></asp:TextBox>
                        </td>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label76" runat="server" CssClass="heading1" Text="Complete"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="completecb" runat="server" />
                        </td>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label77" runat="server" CssClass="heading1" Text="Order ID"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="orderIDtb" runat="server"></asp:TextBox>
                        </td>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label79" runat="server" CssClass="heading1" Text="Invoice Amount"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="invoiceamounttb" runat="server"></asp:TextBox>
                        </td>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label78" runat="server" CssClass="heading1" Text="Delivered On"></asp:Label>
                        </td>
                        <td>
                            <asp:Textbox ID="deliveredtb" runat="server"></asp:Textbox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="deliveredtb" PopupButtonID="dpbtn5"></ajaxToolkit:CalendarExtender>
                            <asp:ImageButton ID="dpbtn5" runat="server" ImageUrl="~/Images/dp_image.jpg" ImageAlign="TextTop" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">&nbsp;</td>
                    </tr>
                    <asp:Panel ID="Panel1" runat="server" Visible="False">
                        <tr>
                            <td colspan="4" style="text-align: left"><asp:Label ID="Label8" runat="server" Text="KIT INFO" Font-Size="Medium" Font-Strikeout="False" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                            
                                    <table style="width: 100%; background-color: #CCCCCC">
                                    <tr>
                                        <td style="width: 15%"><asp:Label ID="Label11" runat="server" Text="Asset ID" CssClass="heading1"></asp:Label></td>
                                        <td colspan="3">
                                            <asp:Label ID="assetIDlbl" runat="server" CssClass="est_label1"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="Label13" runat="server" Text="Equipment" CssClass="heading1"></asp:Label></td>
                                        <td colspan="3">
                                            <asp:Label ID="equipmentlbl" runat="server" CssClass="est_label1"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>    
                                            <asp:Label ID="Label12" runat="server" CssClass="heading1" Text="Kit"></asp:Label></td>
                                        <td colspan="3">
                                            <asp:Label ID="kitlbl" runat="server" CssClass="est_label1"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="Label14" runat="server" Text="Hours/Miles" CssClass="heading1"></asp:Label></td>
                                        <td colspan="3">
                                            <asp:Label ID="hourslbl" runat="server" CssClass="est_label1"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center">&nbsp;</td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td colspan="4">

                            <asp:SqlDataSource ID="SqlCustomer" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [companyID], [company] FROM [t_company] WHERE ([customer] = @customer) ORDER BY [company]">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="True" Name="customer" Type="Boolean" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlCustomerUser" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [userID], [name] FROM [t_user] WHERE ([companyID] = @companyID) ORDER BY [name]">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="customerdd" Name="companyID" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlVendor" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_company] WHERE ([supplier] = @supplier) ORDER BY [company]">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="True" Name="supplier" Type="Boolean" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlVendorUser" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [t_user] WHERE ([companyID] = @companyID) ORDER BY [name]">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="vendordd" Name="companyID" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

