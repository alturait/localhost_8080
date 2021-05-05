Imports System.Data.SqlClient

Partial Class main_EditOrderLine
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("lineID").ToString <> "" Then
                'edit line
                Dim conn As New SqlConnection(appcode.ConnectionString)
                Dim commandString As String
                conn.Open()
                commandString = "select * from t_order_line where lineID=@lineID"
                Dim comm As New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@lineID", Session("lineID"))
                Dim reader As SqlDataReader = comm.ExecuteReader
                If reader.Read Then
                    itemtb.Text = reader.Item("item").ToString
                    pricetb.Text = reader.Item("price").ToString
                    mfrtb.Text = reader.Item("manufacturer").ToString
                    pntb.Text = reader.Item("partnumber").ToString
                    quantitytb.Text = reader.Item("quantity").ToString
                    availabilitytb.Text = reader.Item("availability").ToString
                    uomtb.Text = reader.Item("uom").ToString
                    costlbl.Text = appcode.GetCost(reader.Item("manufacturer"), reader.Item("partnumber"))
                    kitIDlbl.Text = reader.Item("kitID")
                    luberfinerpn.Value = appcode.GetLuberfinerPN(reader.Item("manufacturer"), reader.Item("partnumber"))

                End If
                reader.Close()
                conn.Close()
            Else
                'new line

            End If
        End If
    End Sub

    Protected Sub submitbtn_Click(sender As Object, e As EventArgs) Handles submitbtn.Click
        'save line
        appcode.UpdateOrderLine(Session("lineID"), mfrtb.Text, pntb.Text, itemtb.Text, quantitytb.Text, uomtb.Text, availabilitytb.Text, pricetb.Text)
        Response.Redirect("EditOrder.aspx")
    End Sub

    Protected Sub pntb_TextChanged(sender As Object, e As EventArgs) Handles pntb.TextChanged
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", mfrtb.Text)
        comm.Parameters.AddWithValue("@partnumber", pntb.Text)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            itemtb.Text = reader.Item("item")
            pricetb.Text = appcode.GetCompanyPrice(Session("selected_companyID"), mfrtb.Text, pntb.Text)
            mfrtb.Text = reader.Item("manufacturer")
            pntb.Text = reader.Item("partnumber")
            uomtb.Text = reader.Item("uom")
            costlbl.Text = reader.Item("cost")
        End If
        reader.Close()
        conn.Close()
    End Sub

End Class
