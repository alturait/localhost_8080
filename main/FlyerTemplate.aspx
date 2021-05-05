<%@ Page Title="" Language="VB" MasterPageFile="Admin.master" AutoEventWireup="false" CodeFile="FlyerTemplate.aspx.vb" Inherits="main_EmailTemplate" ValidateRequest="false" %>

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
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="flyerdd" runat="server" DataSourceID="SqlFlyers" DataTextField="title" DataValueField="flyerID" AppendDataBoundItems="True" Enabled="False">
                                <asp:ListItem Value="0">New Flyer</asp:ListItem>
                            </asp:DropDownList>&nbsp;
                            <asp:CheckBox ID="showadscb" runat="server" Text="Show Ads" AutoPostBack="True" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="strTablelbl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label67" runat="server" Text="Title"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="titletb" runat="server" Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label65" runat="server" Text="Header Picture"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="headertb" runat="server" Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label77" runat="server" Text="Footer Picture"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="footertb" runat="server" Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label66" runat="server" Text="Message"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="messagetb" runat="server" Width="600px" Rows="10" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <asp:Panel ID="Panel1" runat="server" Visible="False">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label15" runat="server" Text="Ad 1"></asp:Label>
                                        <asp:CheckBox ID="ad1cb" runat="server" />
                                        <asp:Label ID="ad1IDlbl" runat="server" Visible="False"></asp:Label>
                                    </td>
                                    <td style="text-align: right" colspan="3">
                                        <asp:Label ID="Label68" runat="server" Text="ID "></asp:Label>
                                        <asp:TextBox ID="pID1" runat="server" Width="70">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Picture"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="picturetb1" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="sizetb1" runat="server" Width="30">300</asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        <asp:RadioButtonList ID="rb1" runat="server" RepeatDirection="Horizontal" DataTextField="H">
                                            <asp:ListItem>H</asp:ListItem>
                                            <asp:ListItem>W</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Line 1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line1tb1" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font1dd1" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color1tb1" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold1cb1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Line 2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line2tb1" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font2dd1" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color2tb1" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold2cb1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Line 3"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line3tb1" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font3dd1" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color3tb1" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold3cb1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Line 4"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line4tb1" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font4dd1" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color4tb1" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold4cb1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="Line 5"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line5tb1" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font5dd1" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color5tb1" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold5cb1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="Line 6"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line6tb1" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font6dd1" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color6tb1" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold6cb1" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label8" runat="server" Text="Ad 2"></asp:Label>
                                        <asp:CheckBox ID="ad2cb" runat="server" />
                                        <asp:Label ID="ad2IDlbl" runat="server" Visible="False"></asp:Label>
                                    </td>
                                    <td style="text-align: right" colspan="3">
                                        <asp:Label ID="Label69" runat="server" Text="ID "></asp:Label>
                                        <asp:TextBox ID="pID2" runat="server" Width="70">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="Picture"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="picturetb2" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="sizetb2" runat="server" Width="30">300</asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        <asp:RadioButtonList ID="rb2" runat="server" RepeatDirection="Horizontal" DataTextField="H">
                                            <asp:ListItem>H</asp:ListItem>
                                            <asp:ListItem>W</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="Line 1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line1tb2" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font1dd2" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color1tb2" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold1cb2" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text="Line 2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line2tb2" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font2dd2" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color2tb2" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold2cb2" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="Line 3"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line3tb2" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font3dd2" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color3tb2" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold3cb2" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Text="Line 4"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line4tb2" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font4dd2" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color4tb2" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold4cb2" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" Text="Line 5"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line5tb2" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font5dd2" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color5tb2" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold5cb2" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" Text="Line 6"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line6tb2" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font6dd2" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color6tb2" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold6cb2" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label17" runat="server" Text="Ad 3"></asp:Label>
                                        <asp:CheckBox ID="ad3cb" runat="server" />
                                        <asp:Label ID="ad3IDlbl" runat="server" Visible="False"></asp:Label>
                                    </td>
                                    <td style="text-align: right" colspan="3">
                                        <asp:Label ID="Label70" runat="server" Text="ID "></asp:Label>
                                        <asp:TextBox ID="pID3" runat="server" Width="70">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label18" runat="server" Text="Picture"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="picturetb3" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="sizetb3" runat="server" Width="30">300</asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        <asp:RadioButtonList ID="rb3" runat="server" RepeatDirection="Horizontal" DataTextField="H">
                                            <asp:ListItem>H</asp:ListItem>
                                            <asp:ListItem>W</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" Text="Line 1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line1tb3" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font1dd3" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color1tb3" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold1cb3" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label20" runat="server" Text="Line 2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line2tb3" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font2dd3" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color2tb3" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold2cb3" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label21" runat="server" Text="Line 3"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line3tb3" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font3dd3" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color3tb3" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold3cb3" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label22" runat="server" Text="Line 4"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line4tb3" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font4dd3" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color4tb3" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold4cb3" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label23" runat="server" Text="Line 5"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line5tb3" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font5dd3" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color5tb3" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold5cb3" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label24" runat="server" Text="Line 6"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line6tb3" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font6dd3" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color6tb3" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold6cb3" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label25" runat="server" Text="Ad 4"></asp:Label>
                                        <asp:CheckBox ID="ad4cb" runat="server" />
                                        <asp:Label ID="ad4IDlbl" runat="server" Visible="False"></asp:Label>
                                    </td>
                                    <td style="text-align: right" colspan="3">
                                        <asp:Label ID="Label71" runat="server" Text="ID "></asp:Label>
                                        <asp:TextBox ID="pID4" runat="server" Width="70">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label26" runat="server" Text="Picture"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="picturetb4" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="sizetb4" runat="server" Width="30">300</asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        <asp:RadioButtonList ID="rb4" runat="server" RepeatDirection="Horizontal" DataTextField="H">
                                            <asp:ListItem>H</asp:ListItem>
                                            <asp:ListItem>W</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label27" runat="server" Text="Line 1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line1tb4" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font1dd4" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color1tb4" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold1cb4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label28" runat="server" Text="Line 2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line2tb4" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font2dd4" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color2tb4" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold2cb4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label29" runat="server" Text="Line 3"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line3tb4" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font3dd4" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color3tb4" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold3cb4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label30" runat="server" Text="Line 4"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line4tb4" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font4dd4" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color4tb4" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold4cb4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label31" runat="server" Text="Line 5"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line5tb4" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font5dd4" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color5tb4" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold5cb4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label32" runat="server" Text="Line 6"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line6tb4" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font6dd4" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color6tb4" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold6cb4" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label33" runat="server" Text="Ad 5"></asp:Label>
                                        <asp:CheckBox ID="ad5cb" runat="server" />
                                        <asp:Label ID="ad5IDlbl" runat="server" Visible="False"></asp:Label>
                                    </td>
                                    <td style="text-align: right" colspan="3">
                                        <asp:Label ID="Label72" runat="server" Text="ID "></asp:Label>
                                        <asp:TextBox ID="pID5" runat="server" Width="70">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label34" runat="server" Text="Picture"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="picturetb5" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="sizetb5" runat="server" Width="30">300</asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        <asp:RadioButtonList ID="rb5" runat="server" RepeatDirection="Horizontal" DataTextField="H">
                                            <asp:ListItem>H</asp:ListItem>
                                            <asp:ListItem>W</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label35" runat="server" Text="Line 1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line1tb5" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font1dd5" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color1tb5" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold1cb5" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label36" runat="server" Text="Line 2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line2tb5" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font2dd5" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color2tb5" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold2cb5" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label37" runat="server" Text="Line 3"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line3tb5" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font3dd5" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color3tb5" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold3cb5" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label38" runat="server" Text="Line 4"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line4tb5" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font4dd5" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color4tb5" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold4cb5" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label39" runat="server" Text="Line 5"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line5tb5" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font5dd5" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color5tb5" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold5cb5" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label40" runat="server" Text="Line 6"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line6tb5" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font6dd5" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color6tb5" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold6cb5" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label41" runat="server" Text="Ad 6"></asp:Label>
                                        <asp:CheckBox ID="ad6cb" runat="server" />
                                        <asp:Label ID="ad6IDlbl" runat="server" Visible="False"></asp:Label>
                                    </td>
                                    <td style="text-align: right" colspan="3">
                                        <asp:Label ID="Label73" runat="server" Text="ID "></asp:Label>
                                        <asp:TextBox ID="pID6" runat="server" Width="70">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label42" runat="server" Text="Picture"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="picturetb6" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="sizetb6" runat="server" Width="30">300</asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        <asp:RadioButtonList ID="rb6" runat="server" RepeatDirection="Horizontal" DataTextField="H">
                                            <asp:ListItem>H</asp:ListItem>
                                            <asp:ListItem>W</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label43" runat="server" Text="Line 1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line1tb6" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font1dd6" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color1tb6" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold1cb6" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label44" runat="server" Text="Line 2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line2tb6" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font2dd6" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color2tb6" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold2cb6" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label45" runat="server" Text="Line 3"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line3tb6" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font3dd6" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color3tb6" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold3cb6" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label46" runat="server" Text="Line 4"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line4tb6" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font4dd6" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color4tb6" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold4cb6" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label47" runat="server" Text="Line 5"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line5tb6" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font5dd6" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color5tb6" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold5cb6" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label48" runat="server" Text="Line 6"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line6tb6" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font6dd6" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color6tb6" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold6cb6" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label49" runat="server" Text="Ad 7"></asp:Label>
                                        <asp:CheckBox ID="ad7cb" runat="server" />
                                        <asp:Label ID="ad7IDlbl" runat="server" Visible="False"></asp:Label>
                                    </td>
                                    <td style="text-align: right" colspan="3">
                                        <asp:Label ID="Label74" runat="server" Text="ID "></asp:Label>
                                        <asp:TextBox ID="pID7" runat="server" Width="70">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label50" runat="server" Text="Picture"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="picturetb7" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="sizetb7" runat="server" Width="30">300</asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        <asp:RadioButtonList ID="rb7" runat="server" RepeatDirection="Horizontal" DataTextField="H">
                                            <asp:ListItem>H</asp:ListItem>
                                            <asp:ListItem>W</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label51" runat="server" Text="Line 1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line1tb7" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font1dd7" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color1tb7" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold1cb7" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label52" runat="server" Text="Line 2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line2tb7" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font2dd7" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color2tb7" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold2cb7" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label53" runat="server" Text="Line 3"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line3tb7" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font3dd7" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color3tb7" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold3cb7" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label54" runat="server" Text="Line 4"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line4tb7" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font4dd7" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color4tb7" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold4cb7" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label55" runat="server" Text="Line 5"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line5tb7" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font5dd7" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color5tb7" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold5cb7" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label56" runat="server" Text="Line 6"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line6tb7" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font6dd7" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color6tb7" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold6cb7" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label57" runat="server" Text="Ad 8"></asp:Label>
                                        <asp:CheckBox ID="ad8cb" runat="server" />
                                        <asp:Label ID="ad8IDlbl" runat="server" Visible="False"></asp:Label>
                                    </td>
                                    <td style="text-align: right" colspan="3">
                                        <asp:Label ID="Label75" runat="server" Text="ID "></asp:Label>
                                        <asp:TextBox ID="pID8" runat="server" Width="70">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label58" runat="server" Text="Picture"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="picturetb8" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="sizetb8" runat="server" Width="30">300</asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        <asp:RadioButtonList ID="rb8" runat="server" RepeatDirection="Horizontal" DataTextField="H">
                                            <asp:ListItem>H</asp:ListItem>
                                            <asp:ListItem>W</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label59" runat="server" Text="Line 1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line1tb8" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font1dd8" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color1tb8" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold1cb8" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label60" runat="server" Text="Line 2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line2tb8" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font2dd8" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color2tb8" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold2cb8" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label61" runat="server" Text="Line 3"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line3tb8" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font3dd8" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color3tb8" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold3cb8" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label62" runat="server" Text="Line 4"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line4tb8" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font4dd8" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color4tb8" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold4cb8" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label63" runat="server" Text="Line 5"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line5tb8" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font5dd8" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color5tb8" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold5cb8" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label64" runat="server" Text="Line 6"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="line6tb8" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="font6dd8" runat="server">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="color6tb8" runat="server" Width="50">#000000</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="bold6cb8" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    </asp:Panel>
                    <tr>
                        <td>
                            <asp:Label ID="Label76" runat="server" Text="Attachment"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="filesdd" runat="server" AppendDataBoundItems="True">
                                <asp:ListItem Value="None">None</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="savebtn" runat="server" Text="Save" CssClass="pushbutton1 gold" />
                            <asp:Button ID="deletebtn" runat="server" Text="Delete" CssClass="pushbutton1 gold" OnClientClick="return confirm('Delete this Flyer?');" />
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlFlyers" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [flyerID], [title] FROM [t_flyer] ORDER BY [title]"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

