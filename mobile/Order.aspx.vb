Imports System.Data.SqlClient
Imports aspNetEmail
Imports System.IO

Partial Class mobile_Order
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("mobile_equipmentID") <> "" Then
                assetdd.SelectedValue = Session("mobile_equipmentID")
                kitdd.SelectedValue = Session("mobile_serviceprofileID")
                Dim assetID As String = ""
                Dim equipment_oem As String = ""
                Dim equipment_year As String = ""
                Dim equipment_model As String = ""
                Dim equipment_description As String = ""
                Dim equipment_vin As String = ""
                Dim kitname As String = ""
                Dim interval As String = ""
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
                    assetID = reader.Item("assetID")
                    Session("mobile_assetID") = reader.Item("assetID")
                    equipment_oem = reader.Item("equipment_oem")
                    If equipment_oem = "" Then
                        equipment_oem = "Not Available"
                    End If
                    equipment_year = reader.Item("equipment_year")
                    If equipment_year = "" Then
                        equipment_year = "Not Available"
                    End If
                    If equipment_model = "" Then
                        equipment_model = "Not Available"
                    End If
                    equipment_description = reader.Item("equipment_description")
                    If equipment_description = "" Then
                        equipment_description = "Not Available"
                    End If
                    equipment_vin = reader.Item("equipment_vin")
                    If equipment_vin = "" Then
                        equipment_vin = "Not Available"
                    End If
                    If File.Exists(Server.MapPath("~/Images/Equipment/" & reader.Item("equipmentID").ToString & ".jpg")) = True Then
                        equipmentImage.ImageUrl = "~/Images/Equipment/" & reader.Item("equipmentID").ToString & ".jpg?"
                    Else
                        equipmentImage.ImageUrl = "~/Images/picturena.jpg?"
                    End If
                    locationdd.SelectedValue = reader.Item("locationID")
                End If
                reader.Close()
                commandString = "select * from t_serviceprofile where serviceprofileID=@serviceprofileID"
                comm = New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@serviceprofileID", Session("mobile_serviceprofileID"))
                reader = comm.ExecuteReader
                If reader.Read Then
                    kitname = reader.Item("name")
                    interval = reader.Item("interval")
                End If
                reader.Close()

                conn.Close()
                equipment_oemlbl.Text = equipment_oem
                equipment_modellbl.Text = equipment_model
                equipment_descriptionlbl.Text = equipment_description
                equipment_yearlbl.Text = equipment_year
                equipment_vinlbl.Text = equipment_vin
                intervallbl.Text = interval
            End If
        End If
        'pagelbl.Text = Page.Title

    End Sub

    Protected Sub orderbtn_Click(sender As Object, e As EventArgs) Handles orderbtn.Click

        Session("mobile_kitID") = kitdd.SelectedValue
        Session("mobile_interval") = intervallbl.Text
        Response.Redirect("ConfirmOrder.aspx")

    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "View" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim partIDlbl As Label = GridView1.Rows(index).FindControl("partIDlbl")
            Session("mobile_partID") = partIDlbl.Text
            Response.Redirect("Part.aspx")
        End If
    End Sub

    Protected Sub assetdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles assetdd.SelectedIndexChanged
        Session("mobile_equipmentID") = assetdd.SelectedValue
        Session("mobile_serviceprofileID") = appcode.GetDefaultServiceProfileID(Session("mobile_equipmentID"))
        appcode.UpdateEquipmentViews(Session("mobile_userID"))
        Response.Redirect("Order.aspx")
    End Sub

    Protected Sub kitdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles kitdd.SelectedIndexChanged
        Session("mobile_serviceprofileID") = kitdd.SelectedValue
        Response.Redirect("Order.aspx")
    End Sub

    Protected Sub uploadButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles uploadButton.Click
        If FileUpload1.HasFile Then
            Dim picture As String = Session("mobile_equipmentID").ToString & ".jpg"
            FileUpload1.SaveAs(Server.MapPath("~/Images/Equipment/" & picture))
            msglbl.Text = "File " & picture & " uploaded."
        Else
            msglbl.Text = "No File Uploaded."
        End If
        Response.Redirect("Order.aspx")
    End Sub

    Protected Sub updatebtn_Click(sender As Object, e As EventArgs) Handles updatebtn.Click
        Response.Redirect("SendUpdate.aspx")
    End Sub

    Protected Sub correctbtn_Click(sender As Object, e As EventArgs) Handles correctbtn.Click
        Response.Redirect("SendCorrections.aspx")
    End Sub

    Protected Sub locationdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles locationdd.SelectedIndexChanged
        appcode.UpdateEquipmentLocation(Session("mobile_equipmentID"), locationdd.SelectedValue)
    End Sub

    Protected Sub fluidbtn_Click(sender As Object, e As EventArgs) Handles fluidbtn.Click
        Response.Redirect("Fluid.aspx")
    End Sub
    Protected Sub pendingbtn_Click(sender As Object, e As EventArgs) Handles pendingbtn.Click
        Response.Redirect("Pending.aspx")
    End Sub
    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        If File.Exists(Server.MapPath("~/Images/Equipment/" & Session("mobile_equipmentID").ToString & ".jpg")) = True Then
            File.Delete(Server.MapPath("~/Images/Equipment/" & Session("mobile_equipmentID").ToString & ".jpg"))
            Response.Redirect("Order.aspx")
        End If
    End Sub
End Class
