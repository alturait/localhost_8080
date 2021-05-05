﻿Imports System.Data.SqlClient
Imports System.IO

Partial Class EST_EditService
    Inherits System.Web.UI.Page
    Dim labor_rate As Double = 25.0

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("serviceprofileID") <> "" Then
                Session("equipmentID") = Request.QueryString("equipmentID")
                Session("serviceprofileID") = Request.QueryString("serviceprofileID")
            End If
            Dim equipmentname As String = ""
            Dim conn As New SqlConnection(appcode.ConnectionString)
            Dim commandString As String
            Dim comm As New SqlCommand
            Dim reader As SqlDataReader
            conn.Open()
            commandString = "select * from t_equipment where equipmentID=@equipmentID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@equipmentID", Session("equipmentID"))
            reader = comm.ExecuteReader
            If reader.Read Then
                equipmentname = Page.Title & " - "
                If reader.Item("equipment_year").ToString <> "" Then
                    equipmentname &= reader.Item("equipment_year") & " "
                End If
                If reader.Item("equipment_model").ToString <> "" Then
                    equipmentname &= " " & reader.Item("equipment_model")
                End If
                If reader.Item("equipment_description").ToString <> "" Then
                    equipmentname &= " " & reader.Item("equipment_description")
                End If
                equipmentname &= " (#" & reader.Item("assetID").ToString & ")"
                pagelbl.Text = equipmentname
                intervalrb.SelectedValue = reader.Item("interval_type").ToString
            End If
            reader.Close()
            If Session("serviceprofileID").ToString <> "0" Then
                commandString = "select * from t_serviceprofile where serviceprofileID=@serviceprofileID"
                comm = New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@serviceprofileID", Session("serviceprofileID"))
                reader = comm.ExecuteReader
                If reader.Read Then
                    kitcode.Value = reader.Item("kitcode").ToString
                    kitIDlbl.Text = appcode.GetKitID(reader.Item("kitcode"))
                    servicenametb.Text = reader.Item("name").ToString
                    intervaltb.Text = reader.Item("interval").ToString
                    intervalrb.SelectedValue = reader.Item("interval_type").ToString
                    hourslbl.Text = reader.Item("interval_type").ToString
                    servicenotestb.Text = reader.Item("servicenotes").ToString
                End If
                reader.Close()
                Dim totalparts As Double = 0
                For Each row In GridView1.Rows
                    Dim selectcb As CheckBox = row.FindControl("selectcb")
                    Dim pricelbl As Label = row.FindControl("pricelbl")
                    Dim quantitylbl As Label = row.FindControl("quantitylbl")
                    If selectcb.Checked = True Then
                        totalparts += (CDbl(pricelbl.Text) * CDbl(quantitylbl.Text))
                    End If
                Next
                totalpartstb.Text = FormatCurrency(totalparts, 2)
            Else
                GridView1.DataSourceID = "SqlEquipmentParts"
                GridView1.DataBind()
                showcb.Visible = False
                deletebtn.Visible = False
                orderbtn.Visible = False
                printbtn.Visible = False
                Page.Title = "New Service Kit"
            End If
            conn.Close()
        End If
    End Sub

    Protected Function save_profile() As Integer
        Dim serviceprofileID As Integer = appcode.InsertServiceProfile(Session("equipmentID"), servicenametb.Text, intervaltb.Text, intervalrb.SelectedValue, servicenotestb.Text, "Lube")
        Dim kitcode As String = ""
        For Each row In GridView1.Rows
            Dim selectcb As CheckBox = row.FindControl("selectcb")
            Dim partIDlbl As Label = row.FindControl("partIDlbl")
            Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
            manufacturerlbl.Text = manufacturerlbl.Text.ToUpper
            Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
            partnumberlbl.Text = partnumberlbl.Text.ToUpper
            Dim quantitylbl As Label = row.FindControl("quantitylbl")
            Dim servicepartID As Integer = appcode.InsertServicePart(partIDlbl.Text, serviceprofileID, selectcb.Checked, "")
            If selectcb.Checked = True Then
                kitcode &= Trim(manufacturerlbl.Text) & Trim(partnumberlbl.Text) & Trim(quantitylbl.Text)
            End If
        Next
        appcode.UpdateKitCode(kitcode, serviceprofileID)
        save_profile = serviceprofileID
    End Function

    Protected Sub update_profile()
        appcode.UpdateServiceProfile(Session("serviceprofileID"), servicenametb.Text, intervaltb.Text, intervalrb.SelectedValue, servicenotestb.Text, "Lube")
        appcode.DeleteServiceParts(Session("serviceprofileID"))
        Dim kitcode As String = ""
        For Each row In GridView1.Rows
            Dim selectcb As CheckBox = row.FindControl("selectcb")
            Dim partIDlbl As Label = row.FindControl("partIDlbl")
            Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
            Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
            Dim quantitylbl As Label = row.FindControl("quantitylbl")
            Dim servicepartID As Integer = appcode.InsertServicePart(partIDlbl.Text, Session("serviceprofileID"), selectcb.Checked, "")
            If selectcb.Checked = True Then
                kitcode &= Trim(manufacturerlbl.Text) & Trim(partnumberlbl.Text) & Trim(quantitylbl.Text)
            End If
        Next
        appcode.UpdateKitCode(kitcode, Session("serviceprofileID"))
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        If IsNumeric(intervaltb.Text) = True Then
            If Session("serviceprofileID") <> "0" Then
                update_profile()
            Else
                Session("serviceprofileID") = save_profile()
            End If
            Response.Redirect("EquipmentProfile.aspx")
        Else
            interval_errorlbl.Visible = True
        End If
    End Sub

    Protected Sub deletebtn_Click(sender As Object, e As EventArgs) Handles deletebtn.Click
        appcode.DeleteServiceProfile(Session("serviceprofileID"))
        Response.Redirect("EquipmentProfile.aspx")
    End Sub

    Protected Sub printbtn_Click(sender As Object, e As EventArgs) Handles printbtn.Click
        If IsNumeric(intervaltb.Text) = True Then
            update_profile()
            Dim applicationpath As String = Request.PhysicalApplicationPath
            Dim filename As String = "PDF/Kit_" & Session("serviceprofileID") & ".pdf"
            Dim imagefile As String = ""
            If File.Exists(Server.MapPath("~/Images/Banners/" & Session("selected_companyID").ToString & ".jpg")) = True Then
                imagefile = "Images/Banners/" & Session("selected_companyID").ToString & ".jpg"
            Else
                imagefile = "Images/bannerlogo.jpg"
            End If
            appcode.ServiceKitPDF(Session("serviceprofileID"), applicationpath, filename, imagefile, True)
            Response.Redirect("../" & filename)
        Else
            interval_errorlbl.Visible = True
        End If
    End Sub

    Protected Sub backbtn_Click(sender As Object, e As EventArgs) Handles backbtn.Click
        Response.Redirect("EquipmentProfile.aspx")
    End Sub

    Protected Sub orderbtn_Click(sender As Object, e As EventArgs) Handles orderbtn.Click
        Dim productID As Integer
        Dim conn As New SqlConnection(appcode.ConnectionString)
        conn.Open()
        Dim commandString As String = "select * from v_serviceparts where serviceprofileID=@serviceprofileID and selected=@selected"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", Session("serviceprofileID"))
        comm.Parameters.AddWithValue("@selected", True)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            productID = appcode.GetProductID(reader.Item("manufacturer"), reader.Item("partnumber"))
            If productID = 0 Then
                'insert part number
                productID = appcode.InsertProduct(Session("vendorID"), 0, reader.Item("manufacturer"), reader.Item("partnumber"), reader.Item("description"), "", 0, 0, reader.Item("price"), reader.Item("price"), reader.Item("uom"), 1, False, "NONE", 0, "", True, False, False, 0, False, 0, 0)
            End If
            Dim price As Double = appcode.GetCompanyPrice(Session("selected_companyID"), reader.Item("manufacturer"), reader.Item("partnumber"))
            Dim cartID = appcode.InsertCartItem(Session("selected_companyID"), Session("userID"), Session("vendorID"), productID, reader.Item("manufacturer").ToString, reader.Item("partnumber").ToString, reader.Item("description").ToString, reader.Item("quantity"), price, reader.Item("uom"), 0, Session("serviceprofileID"))
        End While
        reader.Close()
        conn.Close()
        If IsNumeric(hourstb.Text) = False Then
            hourstb.Text = "0"
        End If
        appcode.UpdateHoursMiles(Session("equipmentID"), hourstb.Text)
        Session("hours_miles") = hourstb.Text
        Session("kitID") = Session("serviceprofileID")
        appcode.UpdateServiceFlagA(Session("equipmentID"), False)
        Response.Redirect("Cart.aspx")
    End Sub

    Protected Sub tasklistbtn_Click(sender As Object, e As EventArgs) Handles tasklistbtn.Click
        Response.Redirect("TaskList.aspx")
    End Sub

    Protected Sub servicebtn_Click(sender As Object, e As EventArgs) Handles servicebtn.Click
        Dim productID As Integer
        Dim conn As New SqlConnection(appcode.ConnectionString)
        conn.Open()
        Dim commandString As String = "select * from v_serviceparts where serviceprofileID=@serviceprofileID and selected=@selected"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", Session("serviceprofileID"))
        comm.Parameters.AddWithValue("@selected", True)
        Dim reader As SqlDataReader = comm.ExecuteReader
        Dim cartID As Integer = 0
        Dim price As Double = 0
        While reader.Read
            productID = appcode.GetProductID(reader.Item("manufacturer"), reader.Item("partnumber"))
            If productID = 0 Then
                'insert part number
                productID = appcode.InsertProduct(Session("vendorID"), 0, reader.Item("manufacturer"), reader.Item("partnumber"), reader.Item("description"), "", 0, 0, reader.Item("price"), reader.Item("price"), reader.Item("uom"), 1, False, "NONE", 0, "", True, False, False, 0, False, 0, 0)
            End If
            price = appcode.GetCompanyPrice(Session("selected_companyID"), reader.Item("manufacturer"), reader.Item("partnumber"))
            cartID = appcode.InsertCartItem(Session("selected_companyID"), Session("userID"), Session("vendorID"), productID, reader.Item("manufacturer").ToString, reader.Item("partnumber").ToString, reader.Item("description").ToString, reader.Item("quantity"), price, reader.Item("uom"), 0, Session("serviceprofileID"))
        End While

        Dim ServiceRequired As Boolean = True
        If ServiceRequired = True Then
            Dim servicecharge As Double = 0
            For Each row In GridView2.Rows
                Dim pricelbl As Label = row.findControl("pricelbl")
                servicecharge += CDbl(pricelbl.Text)
            Next
            cartID = appcode.InsertCartItem(Session("selected_companyID"), Session("userID"), Session("vendorID"), productID, "DFO", "SVCCHG", "Service Charge", "1", servicecharge, "EACH", 0, Session("serviceprofileID"))
        End If

        reader.Close()
        conn.Close()
        If IsNumeric(hourstb.Text) = False Then
            hourstb.Text = "0"
        End If

        'ADD SERVICE REQUEST, SCHEDULE SERVICE, CONFIRM SERVICE

        appcode.UpdateHoursMiles(Session("equipmentID"), hourstb.Text)
        Session("hours_miles") = hourstb.Text
        Session("kitID") = Session("serviceprofileID")
        appcode.UpdateServiceFlagA(Session("equipmentID"), False)
        Response.Redirect("Cart.aspx")
    End Sub
End Class
