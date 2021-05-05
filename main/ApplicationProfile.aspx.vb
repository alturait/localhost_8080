Imports System.Data.SqlClient

Partial Class ApplicationProfile
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        Page.MasterPageFile = appcode.GetMasterPage(Session("companyID"))
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            conn.Open()
            Dim comm As New SqlCommand
            Dim reader As SqlDataReader
            commandString = "select * from t_application_data where equipment_oem=@equipment_oem and equipment_model=@equipment_model and engine_oem=@engine_oem and engine_model=@engine_model and equipment_year=@equipment_year and equipment_vin=@equipment_vin"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@equipment_oem", Request.QueryString("oem"))
            comm.Parameters.AddWithValue("@equipment_model", Request.QueryString("model"))
            comm.Parameters.AddWithValue("@engine_oem", Request.QueryString("eoem"))
            comm.Parameters.AddWithValue("@engine_model", Request.QueryString("emodel"))
            comm.Parameters.AddWithValue("@equipment_year", Request.QueryString("year"))
            comm.Parameters.AddWithValue("@equipment_vin", Request.QueryString("vin"))
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
                equiplbl.Text = equipmentname
                oemtb.Text = reader.Item("equipment_oem").ToString
                equipmenttb.Text = reader.Item("equipment_model").ToString
                yeartb.Text = reader.Item("equipment_year").ToString
                engineoemtb.Text = reader.Item("engine_oem").ToString
                descriptiontb.Text = reader.Item("equipment_description").ToString
                enginetb.Text = reader.Item("engine_model").ToString
                optionstb.Text = reader.Item("equipment_option").ToString
                vintb.Text = reader.Item("equipment_vin").ToString
            End If
            reader.Close()
            conn.Close()
            If appcode.isCustomer(Session("companyID")) = True And Session("admin") = False Then
                locationdd.DataSourceID = "SqlLocationsByUser"
                locationdd.DataBind()
            End If
        End If
    End Sub

    Protected Sub copybtn_Click(sender As Object, e As EventArgs) Handles copybtn.Click
        If assetIDtb.Text <> "" Then
            'Dim equipmentID As Integer = appcode.InsertEquipment(Session("selected_companyID"), assetIDtb.Text, oemtb.Text, equipmenttb.Text, descriptiontb.Text, optionstb.Text, yeartb.Text, "", engineoemtb.Text, enginetb.Text, "", False, locationdd.SelectedValue, "0", "Hours", "Red Diesel", "No")
            Dim equipmentID As Integer = appcode.InsertEquipment(Session("selected_companyID"), assetIDtb.Text, oemtb.Text, equipmenttb.Text, descriptiontb.Text, optionstb.Text, yeartb.Text, "", engineoemtb.Text, enginetb.Text, "", False, locationdd.SelectedValue, "0", "Hours", "Red Diesel", "No", 250)
            Dim partID As Integer
            For Each row In GridView1.Rows
                Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
                Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
                Dim descriptionlbl As Label = row.FindControl("descriptionlbl")
                Dim oempnlbl As Label = row.FindControl("oempnlbl")
                Dim quantitylbl As Label = row.FindControl("quantitylbl")
                Dim uomlbl As Label = row.FindControl("uomlbl")
                Dim price As Double = appcode.GetCompanyPrice(Session("selected_companyID"), manufacturerlbl.Text, partnumberlbl.Text)
                partID = appcode.InsertPart(equipmentID, "Filter", manufacturerlbl.Text, partnumberlbl.Text, descriptionlbl.Text, "", "", oemtb.Text, oempnlbl.Text, quantitylbl.Text, uomlbl.Text, price, "")
            Next
            Session("equipmentID") = equipmentID
            Response.Redirect("MyEquipment.aspx")
        End If
    End Sub

End Class
