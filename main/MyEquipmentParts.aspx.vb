Imports System.Data.SqlClient

Partial Class main_MyEquipmentParts
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        Page.MasterPageFile = appcode.GetMasterPage(Session("companyID"))
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("partID") = "0"
            Dim conn As New SqlConnection(lubetracker.ConnectionString)
            Dim commandString As String
            conn.Open()
            Dim comm As New SqlCommand
            Dim reader As SqlDataReader
            commandString = "select * from t_equipment where equipmentID=@equipmentID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@equipmentID", Session("equipmentID"))
            reader = comm.ExecuteReader
            If reader.Read Then
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
                equipmenttb.Text = equipmentname
            End If
            reader.Close()
            conn.Close()
        End If
        pagelbl.Text = Page.Title
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
            Response.Redirect("MyEquipmentParts.aspx")
        End If
    End Sub

    Protected Sub addpartbtn_Click(sender As Object, e As EventArgs) Handles addpartbtn.Click
        Session("partID") = ""
        Session("part_type") = parttypedd.SelectedValue
        Response.Redirect("Part.aspx")
    End Sub

    Protected Sub backbtn_Click(sender As Object, e As EventArgs) Handles backbtn.Click
        Response.Redirect("MyEquipment.aspx")
    End Sub
End Class
