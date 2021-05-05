Imports System.Data.SqlClient

Partial Class main_Discounts
    Inherits System.Web.UI.Page

    Protected Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "Set" Then
            Dim discount As Double
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim discounttb As TextBox = GridView2.Rows(index).FindControl("discounttb")
            Dim manufacturerlbl As Label = GridView2.Rows(index).FindControl("manufacturerlbl")
            Dim categorylbl As Label = GridView2.Rows(index).FindControl("categorylbl")
            If IsNumeric(discounttb.Text) = False Then
                discount = 0
            Else
                discount = discounttb.Text / 100
            End If
            If appcode.HasDiscount(Session("selected_companyID"), Session("vendorID"), manufacturerlbl.Text, categorylbl.Text) = True Then
                appcode.UpdateDiscount(Session("selected_companyID"), Session("vendorID"), manufacturerlbl.Text, categorylbl.Text, discount)
            Else
                Dim discountID As Integer = appcode.InsertDiscount(Session("selected_companyID"), Session("vendorID"), manufacturerlbl.Text, categorylbl.Text, discount)
            End If
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub UpdateKitsbtn_Click(sender As Object, e As EventArgs) Handles UpdateKitsbtn.Click
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_kitparts where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("companyID", Session("selected_companyID"))
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            appcode.UpdatePartPrice(reader.Item("partID"), appcode.GetCompanyPrice(Session("selected_companyID"), reader.Item("manufacturer"), reader.Item("partnumber")))
        End While
        reader.Close()
        conn.Close()
    End Sub
End Class
