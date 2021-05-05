Imports System.Data.SqlClient

Partial Class main_AddOrderLine
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("manufacturer") <> "" And Request.QueryString("partnumber") <> "" Then
                Dim conn As New SqlConnection(appcode.ConnectionString)
                Dim commandString As String
                conn.Open()
                commandString = "select * from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
                Dim comm As New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@manufacturer", Request.QueryString("manufacturer"))
                comm.Parameters.AddWithValue("@partnumber", Request.QueryString("partnumber"))
                Dim reader As SqlDataReader = comm.ExecuteReader
                If reader.Read Then
                    itemlbl.Text = reader.Item("item")
                    pricelbl.Text = appcode.GetCompanyPrice(Session("selected_companyID"), reader.Item("manufacturer"), reader.Item("partnumber"))
                    pricetb.Text = appcode.GetCompanyPrice(Session("selected_companyID"), reader.Item("manufacturer"), reader.Item("partnumber"))
                    manufacturerdd.SelectedValue = reader.Item("manufacturer")
                    partnumberdd.SelectedValue = reader.Item("partnumber")
                    uomlbl.Text = reader.Item("uom")
                End If
                reader.Close()
                conn.Close()
            End If
        End If
    End Sub

    Protected Sub submitbtn_Click(sender As Object, e As EventArgs) Handles submitbtn.Click
        If IsNumeric(quantitytb.Text) = True Then
            If IsNumeric(pricetb.Text) = True Then
                Dim lineID As Integer = appcode.InsertOrderLine(Session("orderID"), 0, 0, manufacturerdd.SelectedValue, partnumberdd.SelectedValue, itemlbl.Text, quantitytb.Text, pricetb.Text, uomlbl.Text, "", False, 0)
            End If
        End If
        Response.Redirect("EditOrder.aspx")
    End Sub

    Protected Sub partnumberdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles partnumberdd.SelectedIndexChanged
        Response.Redirect("AddOrderLine.aspx?manufacturer=" & manufacturerdd.SelectedValue & "&partnumber=" & partnumberdd.SelectedValue)
    End Sub

End Class
