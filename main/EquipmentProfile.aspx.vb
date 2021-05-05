Imports System.Data.SqlClient
Imports System.IO

Partial Class MyEquipment
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("serviceprofileID") = ""
            If Request.QueryString("equipmentID") <> "" Then
                Session("equipmentID") = Request.QueryString("equipmentID")
                Response.Redirect("EquipmentProfile.aspx")
            End If
            If Session("equipmentID").ToString <> "" Then
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
                    If File.Exists(Server.MapPath("~/Images/Equipment/" & reader.Item("equipmentID").ToString & ".jpg")) = True Then
                        equipmentImage.ImageUrl = "~/Images/Equipment/" & reader.Item("equipmentID").ToString & ".jpg"
                    End If
                    assetdd.SelectedValue = Session("equipmentID")
                    Dim equipmentname As String = ""
                    If reader.Item("equipment_year").ToString <> "" Then
                        equipmentname &= reader.Item("equipment_year") & " "
                    End If
                    equipmentname &= reader.Item("equipment_oem")
                    If reader.Item("equipment_model").ToString <> "" Then
                        equipmentname &= " " & reader.Item("equipment_model")
                    End If
                    If reader.Item("equipment_description").ToString <> "" Then
                        equipmentname &= " " & reader.Item("equipment_description")
                    End If
                    enginetb.Text = reader.Item("engine_oem").ToString & " " & reader.Item("engine_model").ToString
                    optionstb.Text = reader.Item("equipment_options").ToString
                    notestb.Text = reader.Item("notes").ToString
                    vintb.Text = reader.Item("equipment_vin").ToString
                    locationtb.Text = appcode.GetLocation(reader.Item("locationID"))
                    hours_milestb.Text = reader.Item("hours_miles").ToString
                    intervallbl.Text = "Current " & reader.Item("interval_type").ToString
                    ophourstb.Text = reader.Item("ophours").ToString
                    rootintervallbl.Text = reader.Item("interval_root")
                    pagelbl.Text = equipmentname
                End If
                Dim lastserviceprofileID As Integer = 0
                reader.Close()
                conn.Close()
            End If
        End If
    End Sub

    Protected Sub servicebtn_Click(sender As Object, e As EventArgs) Handles servicebtn.Click
        Session("serviceprofileID") = "0"
        Response.Redirect("ServiceKit.aspx")
    End Sub

    Protected Sub copybtn_Click(sender As Object, e As EventArgs) Handles copybtn.Click
        Response.Redirect("CopyEquipment.aspx")
    End Sub

    Protected Sub servicedd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles servicedd.SelectedIndexChanged
        Session("serviceprofileID") = servicedd.SelectedValue
        Response.Redirect("ServiceKit.aspx")
    End Sub

    Protected Sub editbtn_Click(sender As Object, e As EventArgs) Handles editbtn.Click
        Response.Redirect("EditEquipment.aspx")
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Edit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim partIDlbl As Label = GridView1.Rows(index).FindControl("partIDlbl")
            Dim parttypelbl As Label = GridView1.Rows(index).FindControl("parttypelbl")
            Session("partID") = partIDlbl.Text
            Session("part_type") = parttypelbl.Text
            Response.Redirect("Part.aspx")
        ElseIf e.CommandName = "Remove" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim partIDlbl As Label = GridView1.Rows(index).FindControl("partIDlbl")
            appcode.DeletePart(partIDlbl.Text)
            Response.Redirect("EquipmentProfile.aspx")
        End If
    End Sub

    Protected Sub addfilterbtn_Click(sender As Object, e As EventArgs) Handles addfilterbtn.Click
        Session("partID") = ""
        Session("part_type") = "Filter"
        Response.Redirect("Part.aspx")
    End Sub

    Protected Sub addfluidbtn_Click(sender As Object, e As EventArgs) Handles addfluidbtn.Click
        Session("partID") = ""
        Session("part_type") = "Fluid"
        Response.Redirect("Part.aspx")
    End Sub

    Protected Sub addbelthosebtn_Click(sender As Object, e As EventArgs) Handles addbelthosebtn.Click
        Session("partID") = ""
        Session("part_type") = "Belt-Hose"
        Response.Redirect("Part.aspx")
    End Sub

    Protected Sub addpartbtn_Click(sender As Object, e As EventArgs) Handles addpartbtn.Click
        Session("partID") = ""
        Session("part_type") = "Other"
        Response.Redirect("Part.aspx")
    End Sub

    Protected Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If e.CommandName = "Edit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim partIDlbl As Label = GridView2.Rows(index).FindControl("partIDlbl0")
            Dim parttypelbl As Label = GridView2.Rows(index).FindControl("parttypelbl0")
            Session("partID") = partIDlbl.Text
            Session("part_type") = parttypelbl.Text
            Response.Redirect("Part.aspx")
        ElseIf e.CommandName = "Remove" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim partIDlbl As Label = GridView2.Rows(index).FindControl("partIDlbl0")
            appcode.DeletePart(partIDlbl.Text)
            Response.Redirect("EquipmentProfile.aspx")
        End If
    End Sub

    Protected Sub GridView3_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView3.RowCommand
        If e.CommandName = "Edit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim partIDlbl As Label = GridView3.Rows(index).FindControl("partIDlbl1")
            Dim parttypelbl As Label = GridView3.Rows(index).FindControl("parttypelbl1")
            Session("partID") = partIDlbl.Text
            Session("part_type") = parttypelbl.Text
            Response.Redirect("Part.aspx")
        ElseIf e.CommandName = "Remove" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim partIDlbl As Label = GridView3.Rows(index).FindControl("partIDlbl1")
            appcode.DeletePart(partIDlbl.Text)
            Response.Redirect("EquipmentProfile.aspx")
        End If
    End Sub

    Protected Sub GridView4_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView4.RowCommand
        If e.CommandName = "Edit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim partIDlbl As Label = GridView4.Rows(index).FindControl("partIDlbl2")
            Dim parttypelbl As Label = GridView4.Rows(index).FindControl("parttypelbl2")
            Session("partID") = partIDlbl.Text
            Session("part_type") = parttypelbl.Text
            Response.Redirect("Part.aspx")
        ElseIf e.CommandName = "Remove" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim partIDlbl As Label = GridView4.Rows(index).FindControl("partIDlbl2")
            appcode.DeletePart(partIDlbl.Text)
            Response.Redirect("EquipmentProfile.aspx")
        End If
    End Sub

    Protected Sub convertbtn_Click(sender As Object, e As EventArgs) Handles convertbtn.Click
        appcode.ConvertParts(manufacturerdd.SelectedValue, Session("equipmentID"))
        Response.Redirect("EquipmentProfile.aspx")
    End Sub

    Protected Sub assetdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles assetdd.SelectedIndexChanged
        If Session("equipmentID") <> assetdd.SelectedValue Then
            Session("equipmentID") = assetdd.SelectedValue
            Response.Redirect("EquipmentProfile.aspx")
        End If
    End Sub

    Protected Sub GridView6_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView6.RowCommand
        If e.CommandName = "Delete" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim alertIDlbl As Label = GridView6.Rows(index).FindControl("alertIDlbl")
            appcode.DeleteAlert(alertIDlbl.Text)
            Response.Redirect("EquipmentProfile.aspx")
        End If
    End Sub

    Protected Sub alertbtn_Click(sender As Object, e As EventArgs) Handles alertbtn.Click
        Session("alertID") = ""
        Response.Redirect("Alert.aspx")
    End Sub
End Class
