Imports System.Data.SqlClient
Imports aspNetEmail
Imports System.IO

Partial Class customer_Asset
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
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
                    equipmentlbl.Text = equipmentname
                    equipmentdd.SelectedValue = Session("equipmentID")
                    hoursmileslbl.Text = reader.Item("interval_type")
                    vinlbl.Text = reader.Item("equipment_vin")
                    locationlbl.Text = appcode.GetLocation(reader.Item("locationID"))
                    choursmileslbl.Text = reader.Item("hours_miles")
                    hpwlbl.Text = reader.Item("ophours")
                    datelbl.Text = appcode.GetLastKitDate(reader.Item("equipmentID"))
                    pmlbl.Text = appcode.GetLastKitName(reader.Item("equipmentID")) & "/" & appcode.GetLastKitInterval(reader.Item("equipmentID"))
                    If File.Exists(Server.MapPath("~/Images/Equipment/" & reader.Item("equipmentID").ToString & ".jpg")) = True Then
                        equipmentImage.ImageUrl = "~/Images/Equipment/" & reader.Item("equipmentID").ToString & ".jpg"
                    End If
                    reader.Close()
                    conn.Close()
                End If
            Else
                Session("equipmentID") = appcode.getDefaultEquipmentID(Session("selected_companyID"))
                Response.Redirect("Asset.aspx")
            End If
        End If
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "edit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim serviceprofileIDlbl As Label = GridView1.Rows(index).FindControl("serviceprofileIDlbl")
            Session("serviceprofileID") = serviceprofileIDlbl.Text
            Response.Redirect("ServiceKit.aspx")
        ElseIf e.CommandName = "label" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim serviceprofileIDlbl As Label = GridView1.Rows(index).FindControl("serviceprofileIDlbl")
            Dim applicationpath As String = Request.PhysicalApplicationPath
            Dim filename As String = "PDF/Kit_" & Session("serviceprofileID") & ".pdf"
            Dim imagefile As String = ""
            If File.Exists(Server.MapPath("~/Images/Banners/" & Session("selected_companyID").ToString & ".jpg")) = True Then
                imagefile = "Images/Banners/" & Session("selected_companyID").ToString & ".jpg"
            Else
                imagefile = "Images/bannerlogo.jpg"
            End If
            appcode.ServiceKitPDF(serviceprofileIDlbl.Text, applicationpath, filename, imagefile, True)
            Response.Redirect("../" & filename)
        End If
    End Sub

    Protected Sub equipmentdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles equipmentdd.SelectedIndexChanged
        Session("equipmentID") = equipmentdd.SelectedValue
        Response.Redirect("Asset.aspx")
    End Sub

    Protected Sub profilebtn_Click(sender As Object, e As EventArgs) Handles profilebtn.Click
        Response.Redirect("EditEquipment.aspx")
    End Sub

    Protected Sub newkitbtn_Click(sender As Object, e As EventArgs) Handles newkitbtn.Click
        Session("serviceprofileID") = "0"
        Response.Redirect("ServiceKit.aspx")
    End Sub

    Protected Sub GridView6_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView6.RowCommand
        If e.CommandName = "Delete" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim alertIDlbl As Label = GridView6.Rows(index).FindControl("alertIDlbl")
            appcode.DeleteAlert(alertIDlbl.Text)
            Response.Redirect("Asset.aspx")
        End If
    End Sub

    Protected Sub alertbtn_Click(sender As Object, e As EventArgs) Handles alertbtn.Click
        Session("alertID") = ""
        Response.Redirect("Alert.aspx")
    End Sub

    Protected Sub fullprobtn_Click(sender As Object, e As EventArgs) Handles fullprobtn.Click
        Response.Redirect("EquipmentProfile.aspx")
    End Sub
End Class
