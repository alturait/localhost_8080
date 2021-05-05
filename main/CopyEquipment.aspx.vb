Imports System.Data.SqlClient

Partial Class CopyEquipment
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("admin") = False Then
                locationdd.DataSourceID = "SqlLocationsByUser"
                locationdd.DataBind()
            End If
            If Session("equipmentID").ToString <> "" Then
                Dim equipmentname As String = ""
                Dim conn As New SqlConnection(appcode.ConnectionString)
                Dim commandString As String
                conn.Open()
                Dim comm As New SqlCommand
                Dim reader As SqlDataReader
                commandString = "select * from t_equipment where equipmentID=@equipmentID"
                comm = New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@equipmentID", Session("equipmentID"))
                reader = comm.ExecuteReader
                If reader.Read Then
                    equipmentname &= reader.Item("equipment_oem")
                    If reader.Item("equipment_model").ToString <> "" Then
                        equipmentname &= " " & reader.Item("equipment_model")
                    End If
                    If reader.Item("equipment_description").ToString <> "" Then
                        equipmentname &= " " & reader.Item("equipment_description")
                    End If
                    equiplbl.Text = equipmentname
                    equipmentoemlbl.Text = reader.Item("equipment_oem").ToString
                    equipmenttb.Text = reader.Item("equipment_model").ToString
                    engineoemlbl.Text = reader.Item("engine_oem").ToString
                    descriptiontb.Text = reader.Item("equipment_description").ToString
                    enginetb.Text = reader.Item("engine_model").ToString
                    optionstb.Text = reader.Item("equipment_options").ToString
                    notestb.Text = reader.Item("notes").ToString
                    yearlbl.Text = reader.Item("equipment_year").ToString
                    vintb.Text = reader.Item("equipment_vin").ToString
                    intervalrb.SelectedValue = reader.Item("interval_type").ToString
                    fuelrb.SelectedValue = reader.Item("fuel_type").ToString
                    defrb.SelectedValue = reader.Item("def").ToString
                    irootlbl.Text = reader.Item("interval_root").ToString
                End If
                reader.Close()
                conn.Close()
            End If
        End If
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        If equipmentnumtb.Text <> "" Then
            Dim equipmentID As Integer = appcode.InsertEquipment(Session("selected_companyID"), equipmentnumtb.Text, equipmentoemlbl.Text, equipmenttb.Text, descriptiontb.Text, optionstb.Text, yearlbl.Text, "", engineoemlbl.Text, enginetb.Text, notestb.Text, False, locationdd.SelectedValue, "0", intervalrb.SelectedValue, fuelrb.SelectedValue, defrb.SelectedValue, irootlbl.Text)
            For Each row In GridView1.Rows
                Dim part_typelbl As Label = row.FindControl("part_typelbl")
                Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
                Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
                Dim alt_partnumberlbl As Label = row.FindControl("alt_partnumberlbl")
                Dim alt_manufacturerlbl As Label = row.FindControl("alt_manufacturerlbl")
                Dim descriptionlbl As Label = row.FindControl("descriptionlbl")
                Dim oemlbl As Label = row.FindControl("oemlbl")
                Dim oem_partnumberlbl As Label = row.FindControl("oem_partnumberlbl")
                Dim oem_manufacturerlbl As Label = row.FindControl("oem_manufacturerlbl")
                Dim quantitylbl As Label = row.FindControl("quantitylbl")
                Dim uomlbl As Label = row.FindControl("uomlbl")
                Dim pricelbl As Label = row.FindControl("pricelbl")
                Dim partID As Integer = appcode.InsertPart(equipmentID, part_typelbl.Text, manufacturerlbl.Text, partnumberlbl.Text, descriptionlbl.Text, alt_manufacturerlbl.Text, alt_partnumberlbl.Text, oem_manufacturerlbl.Text, oem_partnumberlbl.Text, quantitylbl.Text, uomlbl.Text, pricelbl.Text, "")
            Next
            Session("equipmentID") = equipmentID
            Response.Redirect("EquipmentProfile.aspx")
        End If
    End Sub

End Class
