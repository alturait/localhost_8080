Imports System.Data.SqlClient

Partial Class main_KitList2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("serviceprofiileID") = ""
            pagelbl.Text = Page.Title
            If Request.QueryString("equipmentID") <> "" Then
                GridView1.DataSourceID = "SqlKitsByEquipment"
                GridView1.DataBind()
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
                    equipmentname &= " (" & reader.Item("assetID") & ")"
                    pagelbl.Text = equipmentname
                End If
                reader.Close()
                conn.Close()
            End If
        End If
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "kit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim kitcodelbl As Label = GridView1.Rows(index).FindControl("kitcodelbl")
            Dim equipmentID As Integer = appcode.GetKitEquipmentID(kitcodelbl.Text)
            Dim serviceprofileID As Integer = appcode.GetKitServiceProfileID(kitcodelbl.Text, equipmentID)
            Session("equipmentID") = equipmentID
            Session("serviceprofileID") = serviceprofileID
            Response.Redirect("ServiceKit.aspx")
        End If
    End Sub

End Class
