Imports System.Data.SqlClient
Imports System.IO

Partial Class main_EditEquipment
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("serviceprofileID") = ""
            If Request.QueryString("equipmentID") <> "" Then
                Session("equipmentID") = Request.QueryString("equipmentID")
            End If
            If Session("admin") = False And appcode.isCustomer(Session("companyID")) = True Then
                locationdd.DataSourceID = "SqlLocationsByUser"
                locationdd.DataBind()
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
                    Else
                        Panel3.Visible = False
                    End If
                    equipmentnumtb.Text = reader.Item("assetID").ToString
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
                    If reader.Item("equipment_oem").ToString <> "" Then
                        oemdd.SelectedValue = reader.Item("equipment_oem").ToString
                    End If
                    equipmenttb.Text = reader.Item("equipment_model").ToString
                    If reader.Item("engine_oem").ToString <> "" Then
                        engineoemdd.SelectedValue = reader.Item("engine_oem").ToString
                    End If
                    descriptiontb.Text = reader.Item("equipment_description").ToString
                    enginetb.Text = reader.Item("engine_model").ToString
                    optionstb.Text = reader.Item("equipment_options").ToString
                    notestb.Text = reader.Item("notes").ToString
                    irootlbl.Text = reader.Item("interval_root")
                    If reader.Item("equipment_year").ToString <> "" Then
                        yeardd.SelectedValue = reader.Item("equipment_year").ToString
                    End If
                    vintb.Text = reader.Item("equipment_vin").ToString
                    If appcode.IsLocationID(reader.Item("companyID"), reader.Item("locationID")) = True Then
                        locationdd.SelectedValue = reader.Item("locationID")
                    Else
                        locationdd.SelectedValue = "0"
                    End If
                    hours_milestb.Text = reader.Item("hours_miles").ToString
                    If IsNumeric(reader.Item("hours_miles").ToString) = False Then
                        hours_milestb.Text = "0"
                    End If
                    intervallbl.Text = "Current " & reader.Item("interval_type")
                    intervalrb.SelectedValue = reader.Item("interval_type")
                    ophourslbl.Text = reader.Item("interval_type") & "/Week"
                    ophourstb.Text = reader.Item("ophours")
                End If
                Dim lastserviceprofileID As Integer = 0
                reader.Close()
                conn.Close()
                pagelbl.Text = Page.Title
            Else
                equipmenttb.Text = "New Equipment"
                hours_milestb.Text = "0"
                irootlbl.Text = "250"
                cancelbtn.Visible = False
                Panel3.Visible = False
                Panel4.Visible = False
                intervalrb.SelectedValue = "Hours"
                pagelbl.Text = "New Equipment Profile"
            End If
        End If
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        If equipmentnumtb.Text <> "" Then
            If Session("equipmentID").ToString <> "" Then
                If IsNumeric(irootlbl.Text) = False Then
                    irootlbl.Text = "250"
                End If
                appcode.UpdateEquipment(Session("equipmentID"), equipmentnumtb.Text, oemdd.SelectedValue, equipmenttb.Text, descriptiontb.Text, optionstb.Text, yeardd.SelectedValue, vintb.Text, engineoemdd.SelectedValue, enginetb.Text, notestb.Text, False, locationdd.SelectedValue, hours_milestb.Text, intervalrb.SelectedValue, "NA", "NA", irootlbl.Text)
                'check equipment hours
                Dim lasthours As Double = appcode.GetLastKitHoursMiles(Session("equipmentID"))
                If (hours_milestb.Text - lasthours) > irootlbl.Text Then
                    appcode.UpdateServiceFlagA(Session("equipmentID"), True)
                End If
            Else
                Dim equipmentID As Integer = appcode.InsertEquipment(Session("selected_companyID"), equipmentnumtb.Text, oemdd.SelectedValue, equipmenttb.Text, descriptiontb.Text, optionstb.Text, yeardd.SelectedValue, vintb.Text, engineoemdd.SelectedValue, enginetb.Text, notestb.Text, False, locationdd.SelectedValue, hours_milestb.Text, intervalrb.SelectedValue, "NA", "NA", irootlbl.Text)
                Session("equipmentID") = equipmentID
            End If
            Dim ophours As Double = 0
            If IsNumeric(ophourstb.Text) Then
                ophours = ophourstb.Text
            End If
            appcode.UpdateEquipmentOpHours(Session("equipmentID"), ophours)
            Response.Redirect("EquipmentProfile.aspx")
        End If
    End Sub

    Protected Sub cancelbtn_Click(sender As Object, e As EventArgs) Handles cancelbtn.Click
        If File.Exists(Server.MapPath("~/Images/Equipment/" & Session("equipmentID").ToString & ".jpg")) = True Then
            File.Delete(Server.MapPath("~/Images/Equipment/" & Session("equipmentID").ToString & ".jpg"))
        End If
        appcode.DeleteEquipment(Session("equipmentID"))
        Response.Redirect("Assets.aspx")
    End Sub

    Protected Sub uploadButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles uploadButton.Click
        If FileUpload1.HasFile Then
            Dim picture As String = Session("equipmentID") & ".jpg"
            FileUpload1.SaveAs(Server.MapPath("~/Images/Equipment/" & picture))
            msglbl.Text = "File " & picture & " uploaded."
        Else
            msglbl.Text = "No File Uploaded."
        End If
        Response.Redirect("EditEquipment.aspx")
    End Sub

    Protected Sub deletebtn_Click(sender As Object, e As EventArgs) Handles deletebtn.Click
        If File.Exists(Server.MapPath("~/Images/Equipment/" & Session("equipmentID").ToString & ".jpg")) = True Then
            File.Delete(Server.MapPath("~/Images/Equipment/" & Session("equipmentID").ToString & ".jpg"))
            Response.Redirect("EquipmentProfile.aspx")
        End If
    End Sub

    Protected Sub intervalrb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles intervalrb.SelectedIndexChanged
        appcode.UpdateIntervalType(intervalrb.SelectedValue, Session("equipmentID"))
        Response.Redirect("EditEquipment.aspx")
    End Sub

End Class
