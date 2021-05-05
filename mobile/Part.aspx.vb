Imports System.Data.SqlClient

Partial Class mobile_Part
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
                pagelbl.Text = equipmentname
            End If
            reader.Close()
            If Session("mobile_partID").ToString <> "" Then
                commandString = "select * from t_parts where partID=@partID"
                comm = New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@partID", Session("mobile_partID"))
                reader = comm.ExecuteReader
                If reader.Read Then
                    If reader.Item("manufacturer").ToString <> "" And reader.Item("partnumber").ToString <> "" Then
                        luberfinerpn.Value = appcode.GetLuberfinerPN(reader.Item("manufacturer"), reader.Item("partnumber"))
                    ElseIf reader.Item("oem_manufacturer").ToString <> "" And reader.Item("oem_partnumber").ToString <> "" Then
                        luberfinerpn.Value = appcode.GetLuberfinerPN(reader.Item("oem_manufacturer"), reader.Item("oem_partnumber"))
                    Else
                        luberfinerpn.Value = appcode.GetLuberfinerPN(reader.Item("alt_manufacturer"), reader.Item("alt_partnumber"))
                    End If
                    mfrlbl.Text = reader.Item("manufacturer").ToString.ToUpper
                    partnumberlbl.Text = reader.Item("partnumber").ToString
                    If reader.Item("alt_manufacturer").ToString <> "" Then
                        altmfrlbl.Text = reader.Item("alt_manufacturer").ToString.ToUpper
                    End If
                    altpnlbl.Text = reader.Item("alt_partnumber").ToString
                    If reader.Item("oem_manufacturer").ToString <> "" Then
                        oemmfrlbl.Text = reader.Item("oem_manufacturer").ToString
                    End If
                    oempnlbl.Text = reader.Item("oem_partnumber").ToString
                    descriptionlbl.Text = reader.Item("description").ToString
                    quantitylbl.Text = reader.Item("quantity").ToString
                End If
                reader.Close()
            End If
            conn.Close()
        End If
    End Sub

    Protected Sub backbtn_Click(sender As Object, e As EventArgs) Handles backbtn.Click
        Response.Redirect("Order.aspx")
    End Sub
End Class
