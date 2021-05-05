Imports System.Data.SqlClient
Imports System.IO

Partial Class mobile_Fluid
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim equipmentname As String = ""
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            Dim comm As New SqlCommand
            Dim reader As SqlDataReader
            commandString = "select * from t_equipment where equipmentID=@equipmentID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@equipmentID", Session("mobile_equipmentID"))
            reader = comm.ExecuteReader
            If reader.Read Then
                If reader.Item("equipment_year").ToString <> "" Then
                    equipmentname &= reader.Item("equipment_year") & " "
                End If
                equipmentname &= reader.Item("equipment_oem")
                If reader.Item("equipment_model").ToString <> " " Then
                    equipmentname &= " " & reader.Item("equipment_model")
                End If
                If reader.Item("equipment_description").ToString <> " " Then
                    equipmentname &= " " & reader.Item("equipment_description")
                End If
                equipmentname &= " - " & reader.Item("assetID")
                equipmentlbl.Text = equipmentname
            End If
            reader.Close()
            conn.Close()
        End If
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        If IsNumeric(qtytb.Text) = True Then
            If IsNumeric(hourstb.Text) = False Then
                hourstb.Text = 0
            End If
            appcode.InsertEquipmentFluid(Session("mobile_equipmentID"), fluiddd.SelectedValue, uomdd.SelectedValue, qtytb.Text, Session("mobile_userID"), hourstb.Text, notetb.Text)
            Response.Redirect("Order.aspx")
        End If
    End Sub
End Class
