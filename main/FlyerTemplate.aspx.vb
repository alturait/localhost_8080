Imports System.Data.SqlClient
Imports System.IO

Partial Class main_EmailTemplate
    Inherits System.Web.UI.Page

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        Dim strTable As String = BuildTable()
        Dim flyerID As Integer
        If Session("flyerID").ToString <> "0" Then
            flyerID = Session("flyerID")
            If ad1cb.Checked = True Then
                appcode.UpdateAd(ad1IDlbl.Text, pID1.Text, picturetb1.Text, sizetb1.Text, rb1.SelectedValue, line1tb1.Text, font1dd1.SelectedValue, color1tb1.Text, bold1cb1.Checked, line2tb1.Text, font2dd1.SelectedValue, color2tb1.Text, bold2cb1.Checked, line3tb1.Text, font3dd1.SelectedValue, color3tb1.Text, bold3cb1.Checked, line4tb1.Text, font4dd1.SelectedValue, color4tb1.Text, bold4cb1.Checked, line5tb1.Text, font5dd1.SelectedValue, color5tb1.Text, bold5cb1.Checked, line6tb1.Text, font6dd1.SelectedValue, color6tb1.Text, bold6cb1.Checked)
            End If
            If ad2cb.Checked = True Then
                appcode.UpdateAd(ad2IDlbl.Text, pID2.Text, picturetb2.Text, sizetb2.Text, rb2.SelectedValue, line1tb2.Text, font1dd2.SelectedValue, color1tb2.Text, bold1cb2.Checked, line2tb2.Text, font2dd2.SelectedValue, color2tb2.Text, bold2cb2.Checked, line3tb2.Text, font3dd2.SelectedValue, color3tb2.Text, bold3cb2.Checked, line4tb2.Text, font4dd2.SelectedValue, color4tb2.Text, bold4cb2.Checked, line5tb2.Text, font5dd2.SelectedValue, color5tb2.Text, bold5cb2.Checked, line6tb2.Text, font6dd2.SelectedValue, color6tb2.Text, bold6cb2.Checked)
            End If
            If ad3cb.Checked = True Then
                appcode.UpdateAd(ad3IDlbl.Text, pID3.Text, picturetb3.Text, sizetb3.Text, rb3.SelectedValue, line1tb3.Text, font1dd3.SelectedValue, color1tb3.Text, bold1cb3.Checked, line2tb3.Text, font2dd3.SelectedValue, color2tb3.Text, bold2cb3.Checked, line3tb3.Text, font3dd3.SelectedValue, color3tb3.Text, bold3cb3.Checked, line4tb3.Text, font4dd3.SelectedValue, color4tb3.Text, bold4cb3.Checked, line5tb3.Text, font5dd3.SelectedValue, color5tb3.Text, bold5cb3.Checked, line6tb3.Text, font6dd3.SelectedValue, color6tb3.Text, bold6cb3.Checked)
            End If
            If ad4cb.Checked = True Then
                appcode.UpdateAd(ad4IDlbl.Text, pID4.Text, picturetb4.Text, sizetb4.Text, rb4.SelectedValue, line1tb4.Text, font1dd4.SelectedValue, color1tb4.Text, bold1cb4.Checked, line2tb4.Text, font2dd4.SelectedValue, color2tb4.Text, bold2cb4.Checked, line3tb4.Text, font3dd4.SelectedValue, color3tb4.Text, bold3cb4.Checked, line4tb4.Text, font4dd4.SelectedValue, color4tb4.Text, bold4cb4.Checked, line5tb4.Text, font5dd4.SelectedValue, color5tb4.Text, bold5cb4.Checked, line6tb4.Text, font6dd4.SelectedValue, color6tb4.Text, bold6cb4.Checked)
            End If
            If ad5cb.Checked = True Then
                appcode.UpdateAd(ad5IDlbl.Text, pID5.Text, picturetb5.Text, sizetb5.Text, rb5.SelectedValue, line1tb5.Text, font1dd5.SelectedValue, color1tb5.Text, bold1cb5.Checked, line2tb5.Text, font2dd5.SelectedValue, color2tb5.Text, bold2cb5.Checked, line3tb5.Text, font3dd5.SelectedValue, color3tb5.Text, bold3cb5.Checked, line4tb5.Text, font4dd5.SelectedValue, color4tb5.Text, bold4cb5.Checked, line5tb5.Text, font5dd5.SelectedValue, color5tb5.Text, bold5cb5.Checked, line6tb5.Text, font6dd5.SelectedValue, color6tb5.Text, bold6cb5.Checked)
            End If
            If ad6cb.Checked = True Then
                appcode.UpdateAd(ad6IDlbl.Text, pID6.Text, picturetb6.Text, sizetb6.Text, rb6.SelectedValue, line1tb6.Text, font1dd6.SelectedValue, color1tb6.Text, bold1cb6.Checked, line2tb6.Text, font2dd6.SelectedValue, color2tb6.Text, bold2cb6.Checked, line3tb6.Text, font3dd6.SelectedValue, color3tb6.Text, bold3cb6.Checked, line4tb6.Text, font4dd6.SelectedValue, color4tb6.Text, bold4cb6.Checked, line5tb6.Text, font5dd6.SelectedValue, color5tb6.Text, bold5cb6.Checked, line6tb6.Text, font6dd6.SelectedValue, color6tb6.Text, bold6cb6.Checked)
            End If
            If ad7cb.Checked = True Then
                appcode.UpdateAd(ad7IDlbl.Text, pID7.Text, picturetb7.Text, sizetb7.Text, rb7.SelectedValue, line1tb7.Text, font1dd7.SelectedValue, color1tb7.Text, bold1cb7.Checked, line2tb7.Text, font2dd7.SelectedValue, color2tb7.Text, bold2cb7.Checked, line3tb7.Text, font3dd7.SelectedValue, color3tb7.Text, bold3cb7.Checked, line4tb7.Text, font4dd7.SelectedValue, color4tb7.Text, bold4cb7.Checked, line5tb7.Text, font5dd7.SelectedValue, color5tb7.Text, bold5cb7.Checked, line6tb7.Text, font6dd7.SelectedValue, color6tb7.Text, bold6cb7.Checked)
            End If
            If ad8cb.Checked = True Then
                appcode.UpdateAd(ad8IDlbl.Text, pID8.Text, picturetb8.Text, sizetb8.Text, rb8.SelectedValue, line1tb8.Text, font1dd8.SelectedValue, color1tb8.Text, bold1cb8.Checked, line2tb8.Text, font2dd8.SelectedValue, color2tb8.Text, bold2cb8.Checked, line3tb8.Text, font3dd8.SelectedValue, color3tb8.Text, bold3cb8.Checked, line4tb8.Text, font4dd8.SelectedValue, color4tb8.Text, bold4cb8.Checked, line5tb8.Text, font5dd8.SelectedValue, color5tb8.Text, bold5cb8.Checked, line6tb8.Text, font6dd8.SelectedValue, color6tb8.Text, bold6cb8.Checked)
            End If
            appcode.UpdateFlyer(flyerdd.SelectedValue, titletb.Text, headertb.Text, messagetb.Text, strTable, ad1IDlbl.Text, ad2IDlbl.Text, ad3IDlbl.Text, ad4IDlbl.Text, ad5IDlbl.Text, ad6IDlbl.Text, ad7IDlbl.Text, ad8IDlbl.Text, filesdd.SelectedValue)
        Else
            If titletb.Text <> "" Then
                Dim ad1 As Integer = 0
                Dim ad2 As Integer = 0
                Dim ad3 As Integer = 0
                Dim ad4 As Integer = 0
                Dim ad5 As Integer = 0
                Dim ad6 As Integer = 0
                Dim ad7 As Integer = 0
                Dim ad8 As Integer = 0
                If ad1cb.Checked = True Then
                    ad1 = appcode.InsertAd(pID1.Text, picturetb1.Text, sizetb1.Text, rb1.SelectedValue, line1tb1.Text, font1dd1.SelectedValue, color1tb1.Text, bold1cb1.Checked, line1tb1.Text, font2dd1.SelectedValue, color2tb1.Text, bold2cb1.Checked, line3tb1.Text, font3dd1.SelectedValue, color3tb1.Text, bold3cb1.Checked, line4tb1.Text, font4dd1.SelectedValue, color4tb1.Text, bold4cb1.Checked, line5tb1.Text, font5dd1.SelectedValue, color5tb1.Text, bold5cb1.Checked, line6tb1.Text, font6dd1.SelectedValue, color6tb1.Text, bold6cb1.Checked)
                End If
                If ad2cb.Checked = True Then
                    ad2 = appcode.InsertAd(pID2.Text, picturetb2.Text, sizetb2.Text, rb2.SelectedValue, line1tb2.Text, font1dd2.SelectedValue, color1tb2.Text, bold1cb2.Checked, line1tb2.Text, font2dd2.SelectedValue, color2tb2.Text, bold2cb2.Checked, line3tb2.Text, font3dd2.SelectedValue, color3tb2.Text, bold3cb2.Checked, line4tb2.Text, font4dd2.SelectedValue, color4tb2.Text, bold4cb2.Checked, line5tb2.Text, font5dd2.SelectedValue, color5tb2.Text, bold5cb2.Checked, line6tb2.Text, font6dd2.SelectedValue, color6tb2.Text, bold6cb2.Checked)
                End If
                If ad3cb.Checked = True Then
                    ad3 = appcode.InsertAd(pID3.Text, picturetb3.Text, sizetb3.Text, rb3.SelectedValue, line1tb3.Text, font1dd3.SelectedValue, color1tb3.Text, bold1cb3.Checked, line1tb3.Text, font2dd3.SelectedValue, color2tb3.Text, bold2cb3.Checked, line3tb3.Text, font3dd3.SelectedValue, color3tb3.Text, bold3cb3.Checked, line4tb3.Text, font4dd3.SelectedValue, color4tb3.Text, bold4cb3.Checked, line5tb3.Text, font5dd3.SelectedValue, color5tb3.Text, bold5cb3.Checked, line6tb3.Text, font6dd3.SelectedValue, color6tb3.Text, bold6cb3.Checked)
                End If
                If ad4cb.Checked = True Then
                    ad4 = appcode.InsertAd(pID4.Text, picturetb4.Text, sizetb4.Text, rb4.SelectedValue, line1tb4.Text, font1dd4.SelectedValue, color1tb4.Text, bold1cb4.Checked, line1tb4.Text, font2dd4.SelectedValue, color2tb4.Text, bold2cb4.Checked, line3tb4.Text, font3dd4.SelectedValue, color3tb4.Text, bold3cb4.Checked, line4tb4.Text, font4dd4.SelectedValue, color4tb4.Text, bold4cb4.Checked, line5tb4.Text, font5dd4.SelectedValue, color5tb4.Text, bold5cb4.Checked, line6tb4.Text, font6dd4.SelectedValue, color6tb4.Text, bold6cb4.Checked)
                End If
                If ad5cb.Checked = True Then
                    ad5 = appcode.InsertAd(pID5.Text, picturetb5.Text, sizetb5.Text, rb5.SelectedValue, line1tb5.Text, font1dd5.SelectedValue, color1tb5.Text, bold1cb5.Checked, line1tb5.Text, font2dd5.SelectedValue, color2tb5.Text, bold2cb5.Checked, line3tb5.Text, font3dd5.SelectedValue, color3tb5.Text, bold3cb5.Checked, line4tb5.Text, font4dd5.SelectedValue, color4tb5.Text, bold4cb5.Checked, line5tb5.Text, font5dd5.SelectedValue, color5tb5.Text, bold5cb5.Checked, line6tb5.Text, font6dd5.SelectedValue, color6tb5.Text, bold6cb5.Checked)
                End If
                If ad6cb.Checked = True Then
                    ad6 = appcode.InsertAd(pID6.Text, picturetb6.Text, sizetb6.Text, rb6.SelectedValue, line1tb6.Text, font1dd6.SelectedValue, color1tb6.Text, bold1cb6.Checked, line1tb6.Text, font2dd6.SelectedValue, color2tb6.Text, bold2cb6.Checked, line3tb6.Text, font3dd6.SelectedValue, color3tb6.Text, bold3cb6.Checked, line4tb6.Text, font4dd6.SelectedValue, color4tb6.Text, bold4cb6.Checked, line5tb6.Text, font5dd6.SelectedValue, color5tb6.Text, bold5cb6.Checked, line6tb6.Text, font6dd6.SelectedValue, color6tb6.Text, bold6cb6.Checked)
                End If
                If ad7cb.Checked = True Then
                    ad7 = appcode.InsertAd(pID7.Text, picturetb7.Text, sizetb7.Text, rb7.SelectedValue, line1tb7.Text, font1dd7.SelectedValue, color1tb7.Text, bold1cb7.Checked, line1tb7.Text, font2dd7.SelectedValue, color2tb7.Text, bold2cb7.Checked, line3tb7.Text, font3dd7.SelectedValue, color3tb7.Text, bold3cb7.Checked, line4tb7.Text, font4dd7.SelectedValue, color4tb7.Text, bold4cb7.Checked, line5tb7.Text, font5dd7.SelectedValue, color5tb7.Text, bold5cb7.Checked, line6tb7.Text, font6dd7.SelectedValue, color6tb7.Text, bold6cb7.Checked)
                End If
                If ad8cb.Checked = True Then
                    ad8 = appcode.InsertAd(pID8.Text, picturetb8.Text, sizetb8.Text, rb8.SelectedValue, line1tb8.Text, font1dd8.SelectedValue, color1tb8.Text, bold1cb8.Checked, line1tb8.Text, font2dd8.SelectedValue, color2tb8.Text, bold2cb8.Checked, line3tb8.Text, font3dd8.SelectedValue, color3tb8.Text, bold3cb8.Checked, line4tb8.Text, font4dd8.SelectedValue, color4tb8.Text, bold4cb8.Checked, line5tb8.Text, font5dd8.SelectedValue, color5tb8.Text, bold5cb8.Checked, line6tb8.Text, font6dd8.SelectedValue, color6tb8.Text, bold6cb8.Checked)
                End If
                flyerID = appcode.InsertFlyer(titletb.Text, headertb.Text, messagetb.Text, strTable, ad1, ad2, ad3, ad4, ad5, ad6, ad7, ad8, filesdd.SelectedValue)
            End If
        End If
        Session("flyerID") = flyerID
        Response.Redirect("Flyer.aspx")
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim filePaths() As String = Directory.GetFiles(Server.MapPath("~/PDF_Attachments/"))
            Dim files As List(Of ListItem) = New List(Of ListItem)
            For Each filePath As String In filePaths
                'files.Add(New ListItem(Path.GetFileName(filePath), filePath))
                filesdd.Items.Add(New ListItem(Path.GetFileName(filePath), Path.GetFileName(filePath)))
            Next
            'GridView1.DataSource = files
            'GridView1.DataBind()

            strTablelbl.Text = Session("strTable")
            If Session("flyerID").ToString <> "0" Then
                Dim conn As New SqlConnection(appcode.ConnectionString)
                Dim commandString As String
                conn.Open()
                commandString = "select * from t_flyer where flyerID=@flyerID"
                Dim comm As New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@flyerID", Session("flyerID"))
                Dim reader As SqlDataReader = comm.ExecuteReader
                If reader.Read Then
                    filesdd.SelectedValue = reader.Item("pdf_attachment")
                    titletb.Text = reader.Item("title").ToString
                    headertb.Text = reader.Item("header").ToString
                    messagetb.Text = reader.Item("message").ToString
                    ad1IDlbl.Text = reader.Item("ad1")
                    ad2IDlbl.Text = reader.Item("ad2")
                    ad3IDlbl.Text = reader.Item("ad3")
                    ad4IDlbl.Text = reader.Item("ad4")
                    ad5IDlbl.Text = reader.Item("ad5")
                    ad6IDlbl.Text = reader.Item("ad6")
                    ad7IDlbl.Text = reader.Item("ad7")
                    ad8IDlbl.Text = reader.Item("ad8")
                    If reader.Item("ad1") <> 0 Then
                        ad1cb.Checked = True
                    End If
                    If reader.Item("ad2") <> 0 Then
                        ad2cb.Checked = True
                    End If
                    If reader.Item("ad3") <> 0 Then
                        ad3cb.Checked = True
                    End If
                    If reader.Item("ad4") <> 0 Then
                        ad4cb.Checked = True
                    End If
                    If reader.Item("ad5") <> 0 Then
                        ad5cb.Checked = True
                    End If
                    If reader.Item("ad6") <> 0 Then
                        ad6cb.Checked = True
                    End If
                    If reader.Item("ad7") <> 0 Then
                        ad7cb.Checked = True
                    End If
                    If reader.Item("ad8") <> 0 Then
                        ad8cb.Checked = True
                    End If
                    GetAds(reader.Item("ad1"), reader.Item("ad2"), reader.Item("ad3"), reader.Item("ad4"), reader.Item("ad5"), reader.Item("ad6"), reader.Item("ad7"), reader.Item("ad8"))
                End If
                reader.Close()
                conn.Close()
            Else
                deletebtn.Visible = False
                ad1cb.Checked = False
                ad2cb.Checked = False
                ad2cb.Checked = False
                ad4cb.Checked = False
                ad5cb.Checked = False
                ad6cb.Checked = False
                ad7cb.Checked = False
                ad8cb.Checked = False
            End If
            flyerdd.SelectedValue = Session("flyerID")
        End If
        pagelbl.Text = filesdd.SelectedValue
    End Sub

    Sub GetAds(ByVal ad1 As Integer, ByVal ad2 As Integer, ByVal ad3 As Integer, ByVal ad4 As Integer, ByVal ad5 As Integer, ByVal ad6 As Integer, ByVal ad7 As Integer, ByVal ad8 As Integer)
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String = ""
        conn.Open()
        Dim comm As New SqlCommand(commandString, conn)
        Dim reader As SqlDataReader
        If ad1 <> 0 Then
            commandString = "select * from t_ad where adID=@adID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@adID", ad1)
            reader = comm.ExecuteReader
            If reader.Read Then
                pID1.Text = reader.Item("productID")
                picturetb1.Text = reader.Item("pic").ToString
                sizetb1.Text = reader.Item("pic_size").ToString
                rb1.SelectedValue = reader.Item("pic_hw").ToString
                line1tb1.Text = reader.Item("line1").ToString
                font1dd1.SelectedValue = reader.Item("line1_size").ToString
                color1tb1.Text = reader.Item("line1_color").ToString
                bold1cb1.Checked = reader.Item("line1_bold")
                line2tb1.Text = reader.Item("line2").ToString
                font2dd1.SelectedValue = reader.Item("line2_size").ToString
                color2tb1.Text = reader.Item("line2_color").ToString
                bold2cb1.Checked = reader.Item("line2_bold")
                line3tb1.Text = reader.Item("line3").ToString
                font3dd1.SelectedValue = reader.Item("line3_size").ToString
                color3tb1.Text = reader.Item("line3_color").ToString
                bold3cb1.Checked = reader.Item("line3_bold")
                line4tb1.Text = reader.Item("line4").ToString
                font4dd1.SelectedValue = reader.Item("line4_size").ToString
                color4tb1.Text = reader.Item("line4_color").ToString
                bold4cb1.Checked = reader.Item("line4_bold")
                line5tb1.Text = reader.Item("line5").ToString
                font5dd1.SelectedValue = reader.Item("line5_size").ToString
                color5tb1.Text = reader.Item("line5_color").ToString
                bold5cb1.Checked = reader.Item("line5_bold")
                line6tb1.Text = reader.Item("line6").ToString
                font6dd1.SelectedValue = reader.Item("line6_size").ToString
                color6tb1.Text = reader.Item("line6_color").ToString
                bold6cb1.Checked = reader.Item("line6_bold")
            End If
            reader.Close()
        End If
        If ad2 <> 0 Then
            commandString = "select * from t_ad where adID=@adID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@adID", ad2)
            reader = comm.ExecuteReader
            If reader.Read Then
                pID2.Text = reader.Item("productID")
                picturetb2.Text = reader.Item("pic").ToString
                sizetb2.Text = reader.Item("pic_size").ToString
                rb2.SelectedValue = reader.Item("pic_hw").ToString
                line1tb2.Text = reader.Item("line1").ToString
                font1dd2.SelectedValue = reader.Item("line1_size").ToString
                color1tb2.Text = reader.Item("line1_color").ToString
                bold1cb2.Checked = reader.Item("line1_bold")
                line2tb2.Text = reader.Item("line2").ToString
                font2dd2.SelectedValue = reader.Item("line2_size").ToString
                color2tb2.Text = reader.Item("line2_color").ToString
                bold2cb2.Checked = reader.Item("line2_bold")
                line3tb2.Text = reader.Item("line3").ToString
                font3dd2.SelectedValue = reader.Item("line3_size").ToString
                color3tb2.Text = reader.Item("line3_color").ToString
                bold3cb2.Checked = reader.Item("line3_bold")
                line4tb2.Text = reader.Item("line4").ToString
                font4dd2.SelectedValue = reader.Item("line4_size").ToString
                color4tb2.Text = reader.Item("line4_color").ToString
                bold4cb2.Checked = reader.Item("line4_bold")
                line5tb2.Text = reader.Item("line5").ToString
                font5dd2.SelectedValue = reader.Item("line5_size").ToString
                color5tb2.Text = reader.Item("line5_color").ToString
                bold5cb2.Checked = reader.Item("line5_bold")
                line6tb2.Text = reader.Item("line6").ToString
                font6dd2.SelectedValue = reader.Item("line6_size").ToString
                color6tb2.Text = reader.Item("line6_color").ToString
                bold6cb2.Checked = reader.Item("line6_bold")
            End If
            reader.Close()
        End If
        If ad3 <> 0 Then
            commandString = "select * from t_ad where adID=@adID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@adID", ad3)
            reader = comm.ExecuteReader
            If reader.Read Then
                pID3.Text = reader.Item("productID")
                picturetb3.Text = reader.Item("pic").ToString
                sizetb3.Text = reader.Item("pic_size").ToString
                rb3.SelectedValue = reader.Item("pic_hw").ToString
                line1tb3.Text = reader.Item("line1").ToString
                font1dd3.SelectedValue = reader.Item("line1_size").ToString
                color1tb3.Text = reader.Item("line1_color").ToString
                bold1cb3.Checked = reader.Item("line1_bold")
                line2tb3.Text = reader.Item("line2").ToString
                font2dd3.SelectedValue = reader.Item("line2_size").ToString
                color2tb3.Text = reader.Item("line2_color").ToString
                bold2cb3.Checked = reader.Item("line2_bold")
                line3tb3.Text = reader.Item("line3").ToString
                font3dd3.SelectedValue = reader.Item("line3_size").ToString
                color3tb3.Text = reader.Item("line3_color").ToString
                bold3cb3.Checked = reader.Item("line3_bold")
                line4tb3.Text = reader.Item("line4").ToString
                font4dd3.SelectedValue = reader.Item("line4_size").ToString
                color4tb3.Text = reader.Item("line4_color").ToString
                bold4cb3.Checked = reader.Item("line4_bold")
                line5tb3.Text = reader.Item("line5").ToString
                font5dd3.SelectedValue = reader.Item("line5_size").ToString
                color5tb3.Text = reader.Item("line5_color").ToString
                bold5cb3.Checked = reader.Item("line5_bold")
                line6tb3.Text = reader.Item("line6").ToString
                font6dd3.SelectedValue = reader.Item("line6_size").ToString
                color6tb3.Text = reader.Item("line6_color").ToString
                bold6cb3.Checked = reader.Item("line6_bold")
            End If
            reader.Close()
        End If
        If ad4 <> 0 Then
            commandString = "select * from t_ad where adID=@adID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@adID", ad4)
            reader = comm.ExecuteReader
            If reader.Read Then
                pID4.Text = reader.Item("productID")
                picturetb4.Text = reader.Item("pic").ToString
                sizetb4.Text = reader.Item("pic_size").ToString
                rb4.SelectedValue = reader.Item("pic_hw").ToString
                line1tb4.Text = reader.Item("line1").ToString
                font1dd4.SelectedValue = reader.Item("line1_size").ToString
                color1tb4.Text = reader.Item("line1_color").ToString
                bold1cb4.Checked = reader.Item("line1_bold")
                line2tb4.Text = reader.Item("line2").ToString
                font2dd4.SelectedValue = reader.Item("line2_size").ToString
                color2tb4.Text = reader.Item("line2_color").ToString
                bold2cb4.Checked = reader.Item("line2_bold")
                line3tb4.Text = reader.Item("line3").ToString
                font3dd4.SelectedValue = reader.Item("line3_size").ToString
                color3tb4.Text = reader.Item("line3_color").ToString
                bold3cb4.Checked = reader.Item("line3_bold")
                line4tb4.Text = reader.Item("line4").ToString
                font4dd4.SelectedValue = reader.Item("line4_size").ToString
                color4tb4.Text = reader.Item("line4_color").ToString
                bold4cb4.Checked = reader.Item("line4_bold")
                line5tb4.Text = reader.Item("line5").ToString
                font5dd4.SelectedValue = reader.Item("line5_size").ToString
                color5tb4.Text = reader.Item("line5_color").ToString
                bold5cb4.Checked = reader.Item("line5_bold")
                line6tb4.Text = reader.Item("line6").ToString
                font6dd4.SelectedValue = reader.Item("line6_size").ToString
                color6tb4.Text = reader.Item("line6_color").ToString
                bold6cb4.Checked = reader.Item("line6_bold")
            End If
            reader.Close()
        End If
        If ad5 <> 0 Then
            commandString = "select * from t_ad where adID=@adID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@adID", ad5)
            reader = comm.ExecuteReader
            If reader.Read Then
                pID5.Text = reader.Item("productID")
                picturetb5.Text = reader.Item("pic").ToString
                sizetb5.Text = reader.Item("pic_size").ToString
                rb5.SelectedValue = reader.Item("pic_hw").ToString
                line1tb5.Text = reader.Item("line1").ToString
                font1dd5.SelectedValue = reader.Item("line1_size").ToString
                color1tb5.Text = reader.Item("line1_color").ToString
                bold1cb5.Checked = reader.Item("line1_bold")
                line2tb5.Text = reader.Item("line2").ToString
                font2dd5.SelectedValue = reader.Item("line2_size").ToString
                color2tb5.Text = reader.Item("line2_color").ToString
                bold2cb5.Checked = reader.Item("line2_bold")
                line3tb5.Text = reader.Item("line3").ToString
                font3dd5.SelectedValue = reader.Item("line3_size").ToString
                color3tb5.Text = reader.Item("line3_color").ToString
                bold3cb5.Checked = reader.Item("line3_bold")
                line4tb5.Text = reader.Item("line4").ToString
                font4dd5.SelectedValue = reader.Item("line4_size").ToString
                color4tb5.Text = reader.Item("line4_color").ToString
                bold4cb5.Checked = reader.Item("line4_bold")
                line5tb5.Text = reader.Item("line5").ToString
                font5dd5.SelectedValue = reader.Item("line5_size").ToString
                color5tb5.Text = reader.Item("line5_color").ToString
                bold5cb5.Checked = reader.Item("line5_bold")
                line6tb5.Text = reader.Item("line6").ToString
                font6dd5.SelectedValue = reader.Item("line6_size").ToString
                color6tb5.Text = reader.Item("line6_color").ToString
                bold6cb5.Checked = reader.Item("line6_bold")
            End If
            reader.Close()
        End If
        If ad6 <> 0 Then
            commandString = "select * from t_ad where adID=@adID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@adID", ad6)
            reader = comm.ExecuteReader
            If reader.Read Then
                pID6.Text = reader.Item("productID")
                picturetb6.Text = reader.Item("pic").ToString
                sizetb6.Text = reader.Item("pic_size").ToString
                rb6.SelectedValue = reader.Item("pic_hw").ToString
                line1tb6.Text = reader.Item("line1").ToString
                font1dd6.SelectedValue = reader.Item("line1_size").ToString
                color1tb6.Text = reader.Item("line1_color").ToString
                bold1cb6.Checked = reader.Item("line1_bold")
                line2tb6.Text = reader.Item("line2").ToString
                font2dd6.SelectedValue = reader.Item("line2_size").ToString
                color2tb6.Text = reader.Item("line2_color").ToString
                bold2cb6.Checked = reader.Item("line2_bold")
                line3tb6.Text = reader.Item("line3").ToString
                font3dd6.SelectedValue = reader.Item("line3_size").ToString
                color3tb6.Text = reader.Item("line3_color").ToString
                bold3cb6.Checked = reader.Item("line3_bold")
                line4tb6.Text = reader.Item("line4").ToString
                font4dd6.SelectedValue = reader.Item("line4_size").ToString
                color4tb6.Text = reader.Item("line4_color").ToString
                bold4cb6.Checked = reader.Item("line4_bold")
                line5tb6.Text = reader.Item("line5").ToString
                font5dd6.SelectedValue = reader.Item("line5_size").ToString
                color5tb6.Text = reader.Item("line5_color").ToString
                bold5cb6.Checked = reader.Item("line5_bold")
                line6tb6.Text = reader.Item("line6").ToString
                font6dd6.SelectedValue = reader.Item("line6_size").ToString
                color6tb6.Text = reader.Item("line6_color").ToString
                bold6cb6.Checked = reader.Item("line6_bold")
            End If
            reader.Close()
        End If
        If ad7 <> 0 Then
            commandString = "select * from t_ad where adID=@adID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@adID", ad7)
            reader = comm.ExecuteReader
            If reader.Read Then
                pID7.Text = reader.Item("productID")
                picturetb7.Text = reader.Item("pic").ToString
                sizetb7.Text = reader.Item("pic_size").ToString
                rb7.SelectedValue = reader.Item("pic_hw").ToString
                line1tb7.Text = reader.Item("line1").ToString
                font1dd7.SelectedValue = reader.Item("line1_size").ToString
                color1tb7.Text = reader.Item("line1_color").ToString
                bold1cb7.Checked = reader.Item("line1_bold")
                line2tb7.Text = reader.Item("line2").ToString
                font2dd7.SelectedValue = reader.Item("line2_size").ToString
                color2tb7.Text = reader.Item("line2_color").ToString
                bold2cb7.Checked = reader.Item("line2_bold")
                line3tb7.Text = reader.Item("line3").ToString
                font3dd7.SelectedValue = reader.Item("line3_size").ToString
                color3tb7.Text = reader.Item("line3_color").ToString
                bold3cb7.Checked = reader.Item("line3_bold")
                line4tb7.Text = reader.Item("line4").ToString
                font4dd7.SelectedValue = reader.Item("line4_size").ToString
                color4tb7.Text = reader.Item("line4_color").ToString
                bold4cb7.Checked = reader.Item("line4_bold")
                line5tb7.Text = reader.Item("line5").ToString
                font5dd7.SelectedValue = reader.Item("line5_size").ToString
                color5tb7.Text = reader.Item("line5_color").ToString
                bold5cb7.Checked = reader.Item("line5_bold")
                line6tb7.Text = reader.Item("line6").ToString
                font6dd7.SelectedValue = reader.Item("line6_size").ToString
                color6tb7.Text = reader.Item("line6_color").ToString
                bold6cb7.Checked = reader.Item("line6_bold")
            End If
            reader.Close()
        End If
        If ad8 <> 0 Then
            commandString = "select * from t_ad where adID=@adID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@adID", ad8)
            reader = comm.ExecuteReader
            If reader.Read Then
                pID8.Text = reader.Item("productID")
                picturetb8.Text = reader.Item("pic").ToString
                sizetb8.Text = reader.Item("pic_size").ToString
                rb8.SelectedValue = reader.Item("pic_hw").ToString
                line1tb8.Text = reader.Item("line1").ToString
                font1dd8.SelectedValue = reader.Item("line1_size").ToString
                color1tb8.Text = reader.Item("line1_color").ToString
                bold1cb8.Checked = reader.Item("line1_bold")
                line2tb8.Text = reader.Item("line2").ToString
                font2dd8.SelectedValue = reader.Item("line2_size").ToString
                color2tb8.Text = reader.Item("line2_color").ToString
                bold2cb8.Checked = reader.Item("line2_bold")
                line3tb8.Text = reader.Item("line3").ToString
                font3dd8.SelectedValue = reader.Item("line3_size").ToString
                color3tb8.Text = reader.Item("line3_color").ToString
                bold3cb8.Checked = reader.Item("line3_bold")
                line4tb8.Text = reader.Item("line4").ToString
                font4dd8.SelectedValue = reader.Item("line4_size").ToString
                color4tb8.Text = reader.Item("line4_color").ToString
                bold4cb8.Checked = reader.Item("line4_bold")
                line5tb8.Text = reader.Item("line5").ToString
                font5dd8.SelectedValue = reader.Item("line5_size").ToString
                color5tb8.Text = reader.Item("line5_color").ToString
                bold5cb8.Checked = reader.Item("line5_bold")
                line6tb8.Text = reader.Item("line6").ToString
                font6dd8.SelectedValue = reader.Item("line6_size").ToString
                color6tb8.Text = reader.Item("line6_color").ToString
                bold6cb7.Checked = reader.Item("line6_bold")
            End If
            reader.Close()
        End If

        conn.Close()
    End Sub

    Protected Function BuildTable() As String
        BuildTable = ""
        Dim ad1 As String = ""
        Dim ad2 As String = ""
        Dim ad3 As String = ""
        Dim ad4 As String = ""
        Dim ad5 As String = ""
        Dim ad6 As String = ""
        Dim ad7 As String = ""
        Dim ad8 As String = ""
        Dim header1 As String = "<img src='http://www.dfofilters.com/Images/dfo_email_banner.jpg' width='600' />"
        Dim footer1 As String = "<img src='" & headertb.Text & "' width='600' />"
        Dim message As String = messagetb.Text

        If ad1cb.Checked = True Then
            ad1 &= "<table style='width: 100%'>"
            ad1 &= "<tr><td style='vertical-align: bottom; height: 200px; text-align: center'>"
            ad1 &= "<a href='http://www.dfofilters.com/CatalogPage.aspx?productID=" & pID1.Text & "'>"
            ad1 &= "<img src='" & picturetb1.Text
            If rb1.SelectedValue = "H" Then
                ad1 &= "' height='" & sizetb1.Text & "' />"
            Else
                ad1 &= "' width='" & sizetb1.Text & "' />"
            End If
            ad1 &= "</a>"
            ad1 &= "</td></tr>"
            ad1 &= "<tr><td style='vertical-align: top'>"
            If bold1cb1.Checked = True Then
                ad1 &= "<font size='" & font1dd1.SelectedValue & "' color='" & color1tb1.Text & "'><b>" & line1tb1.Text & "</b></font><br/>"
            Else
                ad1 &= "<font size='" & font1dd1.SelectedValue & "' color='" & color1tb1.Text & "'>" & line1tb1.Text & "</font><br/>"
            End If
            If bold2cb1.Checked = True Then
                ad1 &= "<font size='" & font2dd1.SelectedValue & "' color='" & color2tb1.Text & "'><b>" & line2tb1.Text & "</b></font><br/>"
            Else
                ad1 &= "<font size='" & font2dd1.SelectedValue & "' color='" & color2tb1.Text & "'>" & line2tb1.Text & "</font><br/>"
            End If
            If bold3cb1.Checked = True Then
                ad1 &= "<font size='" & font3dd1.SelectedValue & "' color='" & color3tb1.Text & "'><b>" & line3tb1.Text & "</b></font><br/>"
            Else
                ad1 &= "<font size='" & font3dd1.SelectedValue & "' color='" & color3tb1.Text & "'>" & line3tb1.Text & "</font><br/>"
            End If
            If bold4cb1.Checked = True Then
                ad1 &= "<font size='" & font4dd1.SelectedValue & "' color='" & color4tb1.Text & "'><b>" & line4tb1.Text & "</b></font><br/>"
            Else
                ad1 &= "<font size='" & font4dd1.SelectedValue & "' color='" & color4tb1.Text & "'>" & line4tb1.Text & "</font><br/>"
            End If
            If bold5cb1.Checked = True Then
                ad1 &= "<font size='" & font5dd1.SelectedValue & "' color='" & color5tb1.Text & "'><b>" & line5tb1.Text & "</b></font><br/>"
            Else
                ad1 &= "<font size='" & font5dd1.SelectedValue & "' color='" & color5tb1.Text & "'>" & line5tb1.Text & "</font><br/>"
            End If
            If bold6cb1.Checked = True Then
                ad1 &= "<font size='" & font6dd1.SelectedValue & "' color='" & color6tb1.Text & "'><b>" & line6tb1.Text & "</b></font><br/>"
            Else
                ad1 &= "<font size='" & font6dd1.SelectedValue & "' color='" & color6tb1.Text & "'>" & line6tb1.Text & "</font><br/>"
            End If
            ad1 &= "</td></tr>"
            ad1 &= "</table>"
        End If

        If ad2cb.Checked = True Then
            ad2 &= "<table style='width: 100%'>"
            ad2 &= "<tr><td style='vertical-align: bottom; height: 200px; text-align: center'>"
            ad2 &= "<a href='http://www.desertfleetoutfitters.com/CatalogPage.aspx?productID=" & pID2.Text & "'>"
            ad2 &= "<img src='" & picturetb2.Text
            If rb2.SelectedValue = "H" Then
                ad2 &= "' height='" & sizetb2.Text & "' />"
            Else
                ad2 &= "' width='" & sizetb2.Text & "' />"
            End If
            ad2 &= "</a>"
            ad2 &= "</td></tr>"
            ad2 &= "<tr><td style='vertical-align: top'>"
            If bold1cb2.Checked = True Then
                ad2 &= "<font size='" & font1dd2.SelectedValue & "' color='" & color1tb2.Text & "'><b>" & line1tb2.Text & "</b></font><br/>"
            Else
                ad2 &= "<font size='" & font1dd2.SelectedValue & "' color='" & color1tb2.Text & "'>" & line1tb2.Text & "</font><br/>"
            End If
            If bold2cb2.Checked = True Then
                ad2 &= "<font size='" & font2dd2.SelectedValue & "' color='" & color2tb2.Text & "'><b>" & line2tb2.Text & "</b></font><br/>"
            Else
                ad2 &= "<font size='" & font2dd2.SelectedValue & "' color='" & color2tb2.Text & "'>" & line2tb2.Text & "</font><br/>"
            End If
            If bold3cb2.Checked = True Then
                ad2 &= "<font size='" & font3dd2.SelectedValue & "' color='" & color3tb2.Text & "'><b>" & line3tb2.Text & "</b></font><br/>"
            Else
                ad2 &= "<font size='" & font3dd2.SelectedValue & "' color='" & color3tb2.Text & "'>" & line3tb2.Text & "</font><br/>"
            End If
            If bold4cb2.Checked = True Then
                ad2 &= "<font size='" & font4dd2.SelectedValue & "' color='" & color4tb2.Text & "'><b>" & line4tb2.Text & "</b></font><br/>"
            Else
                ad2 &= "<font size='" & font4dd2.SelectedValue & "' color='" & color4tb2.Text & "'>" & line4tb2.Text & "</font><br/>"
            End If
            If bold5cb2.Checked = True Then
                ad2 &= "<font size='" & font5dd2.SelectedValue & "' color='" & color5tb2.Text & "'><b>" & line5tb2.Text & "</b></font><br/>"
            Else
                ad2 &= "<font size='" & font5dd2.SelectedValue & "' color='" & color5tb2.Text & "'>" & line5tb2.Text & "</font><br/>"
            End If
            If bold6cb2.Checked = True Then
                ad2 &= "<font size='" & font6dd2.SelectedValue & "' color='" & color6tb2.Text & "'><b>" & line6tb2.Text & "</b></font><br/>"
            Else
                ad2 &= "<font size='" & font6dd2.SelectedValue & "' color='" & color6tb2.Text & "'>" & line6tb2.Text & "</font><br/>"
            End If
            ad2 &= "</td></tr>"
            ad2 &= "</table>"
        End If

        If ad3cb.Checked = True Then
            ad3 &= "<table style='width: 100%'>"
            ad3 &= "<tr><td style='vertical-align: bottom; height: 200px; text-align: center'>"
            ad3 &= "<a href='http://www.desertfleetoutfitters.com/CatalogPage.aspx?productID=" & pID3.Text & "'>"
            ad3 &= "<img src='" & picturetb3.Text
            If rb2.SelectedValue = "H" Then
                ad3 &= "' height='" & sizetb3.Text & "' />"
            Else
                ad3 &= "' width='" & sizetb3.Text & "' />"
            End If
            ad3 &= "</a>"
            ad3 &= "</td></tr>"
            ad3 &= "<tr><td style='vertical-align: top'>"
            If bold1cb3.Checked = True Then
                ad3 &= "<font size='" & font1dd3.SelectedValue & "' color='" & color1tb3.Text & "'><b>" & line1tb3.Text & "</b></font><br/>"
            Else
                ad3 &= "<font size='" & font1dd3.SelectedValue & "' color='" & color1tb3.Text & "'>" & line1tb3.Text & "</font><br/>"
            End If
            If bold2cb3.Checked = True Then
                ad3 &= "<font size='" & font2dd3.SelectedValue & "' color='" & color2tb3.Text & "'><b>" & line2tb3.Text & "</b></font><br/>"
            Else
                ad3 &= "<font size='" & font2dd3.SelectedValue & "' color='" & color2tb3.Text & "'>" & line2tb3.Text & "</font><br/>"
            End If
            If bold3cb3.Checked = True Then
                ad3 &= "<font size='" & font3dd3.SelectedValue & "' color='" & color3tb3.Text & "'><b>" & line3tb3.Text & "</b></font><br/>"
            Else
                ad3 &= "<font size='" & font3dd3.SelectedValue & "' color='" & color3tb3.Text & "'>" & line3tb3.Text & "</font><br/>"
            End If
            If bold4cb3.Checked = True Then
                ad3 &= "<font size='" & font4dd3.SelectedValue & "' color='" & color4tb3.Text & "'><b>" & line4tb3.Text & "</b></font><br/>"
            Else
                ad3 &= "<font size='" & font4dd3.SelectedValue & "' color='" & color4tb3.Text & "'>" & line4tb3.Text & "</font><br/>"
            End If
            If bold5cb3.Checked = True Then
                ad3 &= "<font size='" & font5dd3.SelectedValue & "' color='" & color5tb3.Text & "'><b>" & line5tb3.Text & "</b></font><br/>"
            Else
                ad3 &= "<font size='" & font5dd3.SelectedValue & "' color='" & color5tb3.Text & "'>" & line5tb3.Text & "</font><br/>"
            End If
            If bold6cb3.Checked = True Then
                ad3 &= "<font size='" & font6dd3.SelectedValue & "' color='" & color6tb3.Text & "'><b>" & line6tb3.Text & "</b></font><br/>"
            Else
                ad3 &= "<font size='" & font6dd3.SelectedValue & "' color='" & color6tb3.Text & "'>" & line6tb3.Text & "</font><br/>"
            End If
            ad3 &= "</td></tr>"
            ad3 &= "</table>"
        End If

        If ad4cb.Checked = True Then
            ad4 &= "<table style='width: 100%'>"
            ad4 &= "<tr><td style='vertical-align: bottom; height: 200px; text-align: center'>"
            ad4 &= "<a href='http://www.desertfleetoutfitters.com/CatalogPage.aspx?productID=" & pID4.Text & "'>"
            ad4 &= "<img src='" & picturetb4.Text
            If rb4.SelectedValue = "H" Then
                ad4 &= "' height='" & sizetb4.Text & "' />"
            Else
                ad4 &= "' width='" & sizetb4.Text & "' />"
            End If
            ad4 &= "</a>"
            ad4 &= "</td></tr>"
            ad4 &= "<tr><td style='vertical-align: top'>"
            If bold1cb4.Checked = True Then
                ad4 &= "<font size='" & font1dd4.SelectedValue & "' color='" & color1tb4.Text & "'><b>" & line1tb4.Text & "</b></font><br/>"
            Else
                ad4 &= "<font size='" & font1dd4.SelectedValue & "' color='" & color1tb4.Text & "'>" & line1tb4.Text & "</font><br/>"
            End If
            If bold2cb4.Checked = True Then
                ad4 &= "<font size='" & font2dd4.SelectedValue & "' color='" & color2tb4.Text & "'><b>" & line2tb4.Text & "</b></font><br/>"
            Else
                ad4 &= "<font size='" & font2dd4.SelectedValue & "' color='" & color2tb4.Text & "'>" & line2tb4.Text & "</font><br/>"
            End If
            If bold3cb4.Checked = True Then
                ad4 &= "<font size='" & font3dd4.SelectedValue & "' color='" & color3tb4.Text & "'><b>" & line3tb4.Text & "</b></font><br/>"
            Else
                ad4 &= "<font size='" & font3dd4.SelectedValue & "' color='" & color3tb4.Text & "'>" & line3tb4.Text & "</font><br/>"
            End If
            If bold4cb4.Checked = True Then
                ad4 &= "<font size='" & font4dd4.SelectedValue & "' color='" & color4tb4.Text & "'><b>" & line4tb4.Text & "</b></font><br/>"
            Else
                ad4 &= "<font size='" & font4dd4.SelectedValue & "' color='" & color4tb4.Text & "'>" & line4tb4.Text & "</font><br/>"
            End If
            If bold5cb4.Checked = True Then
                ad4 &= "<font size='" & font5dd4.SelectedValue & "' color='" & color5tb4.Text & "'><b>" & line5tb4.Text & "</b></font><br/>"
            Else
                ad4 &= "<font size='" & font5dd4.SelectedValue & "' color='" & color5tb4.Text & "'>" & line5tb4.Text & "</font><br/>"
            End If
            If bold6cb4.Checked = True Then
                ad4 &= "<font size='" & font6dd4.SelectedValue & "' color='" & color6tb4.Text & "'><b>" & line6tb4.Text & "</b></font><br/>"
            Else
                ad4 &= "<font size='" & font6dd4.SelectedValue & "' color='" & color6tb4.Text & "'>" & line6tb4.Text & "</font><br/>"
            End If
            ad4 &= "</td></tr>"
            ad4 &= "</table>"
        End If

        If ad5cb.Checked = True Then
            ad5 &= "<table style='width: 100%'>"
            ad5 &= "<tr><td style='vertical-align: bottom; height: 200px; text-align: center'>"
            ad5 &= "<a href='http://www.desertfleetoutfitters.com/CatalogPage.aspx?productID=" & pID5.Text & "'>"
            ad5 &= "<img src='" & picturetb5.Text
            If rb5.SelectedValue = "H" Then
                ad5 &= "' height='" & sizetb5.Text & "' />"
            Else
                ad5 &= "' width='" & sizetb5.Text & "' />"
            End If
            ad5 &= "</a>"
            ad5 &= "</td></tr>"
            ad5 &= "<tr><td style='vertical-align: top'>"
            If bold1cb5.Checked = True Then
                ad5 &= "<font size='" & font1dd5.SelectedValue & "' color='" & color1tb5.Text & "'><b>" & line1tb5.Text & "</b></font><br/>"
            Else
                ad5 &= "<font size='" & font1dd5.SelectedValue & "' color='" & color1tb5.Text & "'>" & line1tb5.Text & "</font><br/>"
            End If
            If bold2cb5.Checked = True Then
                ad5 &= "<font size='" & font2dd5.SelectedValue & "' color='" & color2tb5.Text & "'><b>" & line2tb5.Text & "</b></font><br/>"
            Else
                ad5 &= "<font size='" & font2dd5.SelectedValue & "' color='" & color2tb5.Text & "'>" & line2tb5.Text & "</font><br/>"
            End If
            If bold3cb5.Checked = True Then
                ad5 &= "<font size='" & font3dd5.SelectedValue & "' color='" & color3tb5.Text & "'><b>" & line3tb5.Text & "</b></font><br/>"
            Else
                ad5 &= "<font size='" & font3dd5.SelectedValue & "' color='" & color3tb5.Text & "'>" & line3tb5.Text & "</font><br/>"
            End If
            If bold4cb5.Checked = True Then
                ad5 &= "<font size='" & font4dd5.SelectedValue & "' color='" & color4tb5.Text & "'><b>" & line4tb5.Text & "</b></font><br/>"
            Else
                ad5 &= "<font size='" & font4dd5.SelectedValue & "' color='" & color4tb5.Text & "'>" & line4tb5.Text & "</font><br/>"
            End If
            If bold5cb5.Checked = True Then
                ad5 &= "<font size='" & font5dd5.SelectedValue & "' color='" & color5tb5.Text & "'><b>" & line5tb5.Text & "</b></font><br/>"
            Else
                ad5 &= "<font size='" & font5dd5.SelectedValue & "' color='" & color5tb5.Text & "'>" & line5tb5.Text & "</font><br/>"
            End If
            If bold6cb5.Checked = True Then
                ad5 &= "<font size='" & font6dd5.SelectedValue & "' color='" & color6tb5.Text & "'><b>" & line6tb5.Text & "</b></font><br/>"
            Else
                ad5 &= "<font size='" & font6dd5.SelectedValue & "' color='" & color6tb5.Text & "'>" & line6tb5.Text & "</font><br/>"
            End If
            ad5 &= "</td></tr>"
            ad5 &= "</table>"
        End If

        If ad6cb.Checked = True Then
            ad6 &= "<table style='width: 100%'>"
            ad6 &= "<tr><td style='vertical-align: bottom; height: 200px; text-align: center'>"
            ad6 &= "<a href='http://www.desertfleetoutfitters.com/CatalogPage.aspx?productID=" & pID6.Text & "'>"
            ad6 &= "<img src='" & picturetb6.Text
            If rb6.SelectedValue = "H" Then
                ad6 &= "' height='" & sizetb6.Text & "' />"
            Else
                ad6 &= "' width='" & sizetb6.Text & "' />"
            End If
            ad6 &= "</a>"
            ad6 &= "</td></tr>"
            ad6 &= "<tr><td style='vertical-align: top'>"
            If bold1cb6.Checked = True Then
                ad6 &= "<font size='" & font1dd6.SelectedValue & "' color='" & color1tb6.Text & "'><b>" & line1tb6.Text & "</b></font><br/>"
            Else
                ad6 &= "<font size='" & font1dd6.SelectedValue & "' color='" & color1tb6.Text & "'>" & line1tb6.Text & "</font><br/>"
            End If
            If bold2cb6.Checked = True Then
                ad6 &= "<font size='" & font2dd6.SelectedValue & "' color='" & color2tb6.Text & "'><b>" & line2tb6.Text & "</b></font><br/>"
            Else
                ad6 &= "<font size='" & font2dd6.SelectedValue & "' color='" & color2tb6.Text & "'>" & line2tb6.Text & "</font><br/>"
            End If
            If bold3cb6.Checked = True Then
                ad6 &= "<font size='" & font3dd6.SelectedValue & "' color='" & color3tb6.Text & "'><b>" & line3tb6.Text & "</b></font><br/>"
            Else
                ad6 &= "<font size='" & font3dd6.SelectedValue & "' color='" & color3tb6.Text & "'>" & line3tb6.Text & "</font><br/>"
            End If
            If bold4cb6.Checked = True Then
                ad6 &= "<font size='" & font4dd6.SelectedValue & "' color='" & color4tb6.Text & "'><b>" & line4tb6.Text & "</b></font><br/>"
            Else
                ad6 &= "<font size='" & font4dd6.SelectedValue & "' color='" & color4tb6.Text & "'>" & line4tb6.Text & "</font><br/>"
            End If
            If bold5cb6.Checked = True Then
                ad6 &= "<font size='" & font5dd6.SelectedValue & "' color='" & color5tb6.Text & "'><b>" & line5tb6.Text & "</b></font><br/>"
            Else
                ad6 &= "<font size='" & font5dd6.SelectedValue & "' color='" & color5tb6.Text & "'>" & line5tb6.Text & "</font><br/>"
            End If
            If bold6cb6.Checked = True Then
                ad6 &= "<font size='" & font6dd6.SelectedValue & "' color='" & color6tb6.Text & "'><b>" & line6tb6.Text & "</b></font><br/>"
            Else
                ad6 &= "<font size='" & font6dd6.SelectedValue & "' color='" & color6tb6.Text & "'>" & line6tb6.Text & "</font><br/>"
            End If
            ad6 &= "</td></tr>"
            ad6 &= "</table>"
        End If

        If ad7cb.Checked = True Then
            ad7 &= "<table style='width: 100%'>"
            ad7 &= "<tr><td style='vertical-align: bottom; height: 200px; text-align: center'>"
            ad7 &= "<a href='http://www.desertfleetoutfitters.com/CatalogPage.aspx?productID=" & pID7.Text & "'>"
            ad7 &= "<img src='" & picturetb7.Text
            If rb7.SelectedValue = "H" Then
                ad7 &= "' height='" & sizetb7.Text & "' />"
            Else
                ad7 &= "' width='" & sizetb7.Text & "' />"
            End If
            ad7 &= "</a>"
            ad7 &= "</td></tr>"
            ad7 &= "<tr><td style='vertical-align: top'>"
            If bold1cb7.Checked = True Then
                ad7 &= "<font size='" & font1dd7.SelectedValue & "' color='" & color1tb7.Text & "'><b>" & line1tb7.Text & "</b></font><br/>"
            Else
                ad7 &= "<font size='" & font1dd7.SelectedValue & "' color='" & color1tb7.Text & "'>" & line1tb7.Text & "</font><br/>"
            End If
            If bold2cb7.Checked = True Then
                ad7 &= "<font size='" & font2dd7.SelectedValue & "' color='" & color2tb7.Text & "'><b>" & line2tb7.Text & "</b></font><br/>"
            Else
                ad7 &= "<font size='" & font2dd7.SelectedValue & "' color='" & color2tb7.Text & "'>" & line2tb7.Text & "</font><br/>"
            End If
            If bold3cb7.Checked = True Then
                ad7 &= "<font size='" & font3dd7.SelectedValue & "' color='" & color3tb7.Text & "'><b>" & line3tb7.Text & "</b></font><br/>"
            Else
                ad7 &= "<font size='" & font3dd7.SelectedValue & "' color='" & color3tb7.Text & "'>" & line3tb7.Text & "</font><br/>"
            End If
            If bold4cb7.Checked = True Then
                ad7 &= "<font size='" & font4dd7.SelectedValue & "' color='" & color4tb7.Text & "'><b>" & line4tb7.Text & "</b></font><br/>"
            Else
                ad7 &= "<font size='" & font4dd7.SelectedValue & "' color='" & color4tb7.Text & "'>" & line4tb7.Text & "</font><br/>"
            End If
            If bold5cb7.Checked = True Then
                ad7 &= "<font size='" & font5dd7.SelectedValue & "' color='" & color5tb7.Text & "'><b>" & line5tb7.Text & "</b></font><br/>"
            Else
                ad7 &= "<font size='" & font5dd7.SelectedValue & "' color='" & color5tb7.Text & "'>" & line5tb7.Text & "</font><br/>"
            End If
            If bold6cb7.Checked = True Then
                ad7 &= "<font size='" & font6dd7.SelectedValue & "' color='" & color6tb7.Text & "'><b>" & line6tb7.Text & "</b></font><br/>"
            Else
                ad7 &= "<font size='" & font6dd7.SelectedValue & "' color='" & color6tb7.Text & "'>" & line6tb7.Text & "</font><br/>"
            End If
            ad7 &= "</td></tr>"
            ad7 &= "</table>"
        End If

        If ad8cb.Checked = True Then
            ad8 &= "<table style='width: 100%'>"
            ad8 &= "<tr><td style='vertical-align: bottom; height: 200px; text-align: center'>"
            ad8 &= "<a href='http://www.desertfleetoutfitters.com/CatalogPage.aspx?productID=" & pID8.Text & "'>"
            ad8 &= "<img src='" & picturetb8.Text
            If rb8.SelectedValue = "H" Then
                ad8 &= "' height='" & sizetb8.Text & "' />"
            Else
                ad8 &= "' width='" & sizetb8.Text & "' />"
            End If
            ad8 &= "</a>"
            ad8 &= "</td></tr>"
            ad8 &= "<tr><td style='vertical-align: top'>"
            If bold1cb8.Checked = True Then
                ad8 &= "<font size='" & font1dd8.SelectedValue & "' color='" & color1tb8.Text & "'><b>" & line1tb8.Text & "</b></font><br/>"
            Else
                ad8 &= "<font size='" & font1dd8.SelectedValue & "' color='" & color1tb8.Text & "'>" & line1tb8.Text & "</font><br/>"
            End If
            If bold2cb8.Checked = True Then
                ad8 &= "<font size='" & font2dd8.SelectedValue & "' color='" & color2tb8.Text & "'><b>" & line2tb8.Text & "</b></font><br/>"
            Else
                ad8 &= "<font size='" & font2dd8.SelectedValue & "' color='" & color2tb8.Text & "'>" & line2tb8.Text & "</font><br/>"
            End If
            If bold3cb8.Checked = True Then
                ad8 &= "<font size='" & font3dd8.SelectedValue & "' color='" & color3tb8.Text & "'><b>" & line3tb8.Text & "</b></font><br/>"
            Else
                ad8 &= "<font size='" & font3dd8.SelectedValue & "' color='" & color3tb8.Text & "'>" & line3tb8.Text & "</font><br/>"
            End If
            If bold4cb8.Checked = True Then
                ad8 &= "<font size='" & font4dd8.SelectedValue & "' color='" & color4tb8.Text & "'><b>" & line4tb8.Text & "</b></font><br/>"
            Else
                ad8 &= "<font size='" & font4dd8.SelectedValue & "' color='" & color4tb8.Text & "'>" & line4tb8.Text & "</font><br/>"
            End If
            If bold5cb8.Checked = True Then
                ad8 &= "<font size='" & font5dd8.SelectedValue & "' color='" & color5tb8.Text & "'><b>" & line5tb8.Text & "</b></font><br/>"
            Else
                ad8 &= "<font size='" & font5dd8.SelectedValue & "' color='" & color5tb8.Text & "'>" & line5tb8.Text & "</font><br/>"
            End If
            If bold6cb8.Checked = True Then
                ad8 &= "<font size='" & font6dd8.SelectedValue & "' color='" & color6tb8.Text & "'><b>" & line6tb8.Text & "</b></font><br/>"
            Else
                ad8 &= "<font size='" & font6dd8.SelectedValue & "' color='" & color6tb8.Text & "'>" & line6tb8.Text & "</font><br/>"
            End If
            ad8 &= "</td></tr>"
            ad8 &= "</table>"
        End If

        Dim strTable As String = "<html><body>"
        strTable &= "<table style='border-style: solid; border-width: thin; width: 600px; padding: 5px; font-family: Arial, Helvetica, sans-serif'>"
        strTable &= "<tr><td colspan='2' style='text-align: center'><a href='http://www.dfofilters.com'>" & header1 & "</a></td></tr>"
        strTable &= "<tr><td colspan='2'>" & message & "</td></tr>"
        If Panel1.Visible = True Then
            strTable &= "<tr><td style='border-style: solid; border-width: thin; width: 300px; vertical-align: top'>" & ad1 & "</td><td style='border-style: solid; border-width: thin; width: 300px; vertical-align: top'>" & ad2 & "</td></tr>"
            strTable &= "<tr><td style='border-style: solid; border-width: thin; width: 300px; vertical-align: top'>" & ad3 & "</td><td style='border-style: solid; border-width: thin; width: 300px; vertical-align: top'>" & ad4 & "</td></tr>"
            strTable &= "<tr><td style='border-style: solid; border-width: thin; width: 300px; vertical-align: top'>" & ad5 & "</td><td style='border-style: solid; border-width: thin; width: 300px; vertical-align: top'>" & ad6 & "</td></tr>"
            strTable &= "<tr><td style='border-style: solid; border-width: thin; width: 300px; vertical-align: top'>" & ad7 & "</td><td style='border-style: solid; border-width: thin; width: 300px; vertical-align: top'>" & ad8 & "</td></tr>"
        End If
        strTable &= "<tr><td colspan='2' style='text-align: center'><a href='http://www.dfofilters.com'>" & footer1 & "</a></td></tr>"
        strTable &= "</table></font></body></html>"
        BuildTable = strTable
    End Function

    Protected Sub deletebtn_Click(sender As Object, e As EventArgs) Handles deletebtn.Click
        appcode.DeleteFlyer(Session("flyerID"))
        Session("flyerID") = "0"
        Response.Redirect("Flyer.aspx")
    End Sub

    Protected Sub showadscb_CheckedChanged(sender As Object, e As EventArgs) Handles showadscb.CheckedChanged
        If showadscb.Checked = True Then
            Panel1.Visible = True
        Else
            Panel1.Visible = False
        End If
    End Sub

End Class
