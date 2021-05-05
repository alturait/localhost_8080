
Partial Class main_QuickCart
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        Page.MasterPageFile = appcode.GetMasterPage(Session("companyID"))
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

    Sub AddToCart()
        If pntb1.Text <> "" Then
            If qtytb1.Text <> "" Then
                Dim linecreated As Boolean = appcode.InsertQuickCartLine(Session("selected_companyID"), Session("userID"), Session("vendorID"), mfrdd1.SelectedValue, pntb1.Text, qtytb1.Text, Session("ssID"))
            End If
        End If
        If pntb2.Text <> "" Then
            If qtytb2.Text <> "" Then
                Dim linecreated As Boolean = appcode.InsertQuickCartLine(Session("selected_companyID"), Session("userID"), Session("vendorID"), mfrdd2.SelectedValue, pntb2.Text, qtytb2.Text, Session("ssID"))
            End If
        End If
        If pntb3.Text <> "" Then
            If qtytb3.Text <> "" Then
                Dim linecreated As Boolean = appcode.InsertQuickCartLine(Session("selected_companyID"), Session("userID"), Session("vendorID"), mfrdd3.SelectedValue, pntb3.Text, qtytb3.Text, Session("ssID"))
            End If
        End If
        If pntb4.Text <> "" Then
            If qtytb4.Text <> "" Then
                Dim linecreated As Boolean = appcode.InsertQuickCartLine(Session("selected_companyID"), Session("userID"), Session("vendorID"), mfrdd4.SelectedValue, pntb4.Text, qtytb4.Text, Session("ssID"))
            End If
        End If
        If pntb5.Text <> "" Then
            If qtytb5.Text <> "" Then
                Dim linecreated As Boolean = appcode.InsertQuickCartLine(Session("selected_companyID"), Session("userID"), Session("vendorID"), mfrdd5.SelectedValue, pntb5.Text, qtytb5.Text, Session("ssID"))
            End If
        End If
        If pntb6.Text <> "" Then
            If qtytb6.Text <> "" Then
                Dim linecreated As Boolean = appcode.InsertQuickCartLine(Session("selected_companyID"), Session("userID"), Session("vendorID"), mfrdd6.SelectedValue, pntb6.Text, qtytb6.Text, Session("ssID"))
            End If
        End If
        If pntb7.Text <> "" Then
            If qtytb7.Text <> "" Then
                Dim linecreated As Boolean = appcode.InsertQuickCartLine(Session("selected_companyID"), Session("userID"), Session("vendorID"), mfrdd7.SelectedValue, pntb7.Text, qtytb7.Text, Session("ssID"))
            End If
        End If
        If pntb8.Text <> "" Then
            If qtytb8.Text <> "" Then
                Dim linecreated As Boolean = appcode.InsertQuickCartLine(Session("selected_companyID"), Session("userID"), Session("vendorID"), mfrdd8.SelectedValue, pntb8.Text, qtytb8.Text, Session("ssID"))
            End If
        End If
        If pntb9.Text <> "" Then
            If qtytb9.Text <> "" Then
                Dim linecreated As Boolean = appcode.InsertQuickCartLine(Session("selected_companyID"), Session("userID"), Session("vendorID"), mfrdd9.SelectedValue, pntb9.Text, qtytb9.Text, Session("ssID"))
            End If
        End If
        If pntb10.Text <> "" Then
            If qtytb10.Text <> "" Then
                Dim linecreated As Boolean = appcode.InsertQuickCartLine(Session("selected_companyID"), Session("userID"), Session("vendorID"), mfrdd10.SelectedValue, pntb10.Text, qtytb10.Text, Session("ssID"))
            End If
        End If
    End Sub

    Protected Sub cartbtn_Click(sender As Object, e As EventArgs) Handles cartbtn.Click
        AddToCart()
        Response.Redirect("Cart.aspx")
    End Sub

    Protected Sub resetbtn_Click(sender As Object, e As EventArgs) Handles resetbtn.Click
        Response.Redirect("QuickOrder.aspx")
    End Sub

    Protected Sub pntb1_TextChanged(sender As Object, e As EventArgs) Handles pntb1.TextChanged
        If appcode.IsPartnumber(mfrdd1.SelectedValue, pntb1.Text) = True Then
            status1.Text = "Confirmed"
        Else
            status1.Text = "Not Found"
        End If
        qtytb1.Focus()
    End Sub

    Protected Sub pntb2_TextChanged(sender As Object, e As EventArgs) Handles pntb2.TextChanged
        If appcode.IsPartnumber(mfrdd2.SelectedValue, pntb2.Text) = True Then
            status2.Text = "Confirmed"
        Else
            status2.Text = "Not Found"
        End If
        qtytb2.Focus()
    End Sub

    Protected Sub pntb3_TextChanged(sender As Object, e As EventArgs) Handles pntb3.TextChanged
        If appcode.IsPartnumber(mfrdd3.SelectedValue, pntb3.Text) = True Then
            status3.Text = "Confirmed"
        Else
            status3.Text = "Not Found"
        End If
        qtytb3.Focus()
    End Sub

    Protected Sub pntb4_TextChanged(sender As Object, e As EventArgs) Handles pntb4.TextChanged
        If appcode.IsPartnumber(mfrdd4.SelectedValue, pntb4.Text) = True Then
            status4.Text = "Confirmed"
        Else
            status4.Text = "Not Found"
        End If
        qtytb4.Focus()
    End Sub

    Protected Sub pntb5_TextChanged(sender As Object, e As EventArgs) Handles pntb5.TextChanged
        If appcode.IsPartnumber(mfrdd5.SelectedValue, pntb5.Text) = True Then
            status5.Text = "Confirmed"
        Else
            status5.Text = "Not Found"
        End If
        qtytb5.Focus()
    End Sub

    Protected Sub pntb6_TextChanged(sender As Object, e As EventArgs) Handles pntb6.TextChanged
        If appcode.IsPartnumber(mfrdd6.SelectedValue, pntb6.Text) = True Then
            status6.Text = "Confirmed"
        Else
            status6.Text = "Not Found"
        End If
        qtytb6.Focus()
    End Sub

    Protected Sub pntb7_TextChanged(sender As Object, e As EventArgs) Handles pntb7.TextChanged
        If appcode.IsPartnumber(mfrdd7.SelectedValue, pntb7.Text) = True Then
            status7.Text = "Confirmed"
        Else
            status7.Text = "Not Found"
        End If
        qtytb7.Focus()
    End Sub

    Protected Sub pntb8_TextChanged(sender As Object, e As EventArgs) Handles pntb8.TextChanged
        If appcode.IsPartnumber(mfrdd8.SelectedValue, pntb8.Text) = True Then
            status8.Text = "Confirmed"
        Else
            status8.Text = "Not Found"
        End If
        qtytb8.Focus()
    End Sub

    Protected Sub pntb9_TextChanged(sender As Object, e As EventArgs) Handles pntb9.TextChanged
        If appcode.IsPartnumber(mfrdd9.SelectedValue, pntb9.Text) = True Then
            status9.Text = "Confirmed"
        Else
            status9.Text = "Not Found"
        End If
        qtytb9.Focus()
    End Sub

    Protected Sub pntb10_TextChanged(sender As Object, e As EventArgs) Handles pntb10.TextChanged
        If appcode.IsPartnumber(mfrdd10.SelectedValue, pntb10.Text) = True Then
            status10.Text = "Confirmed"
        Else
            status10.Text = "Not Found"
        End If
        qtytb10.Focus()
    End Sub

    Protected Sub continuebtn_Click(sender As Object, e As EventArgs) Handles continuebtn.Click
        AddToCart()
        Response.Redirect("QuickOrder.aspx")
    End Sub

End Class
