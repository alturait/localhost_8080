Imports System.Data.SqlClient

Partial Class customer_Part
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
            comm.Parameters.AddWithValue("@equipmentID", Session("equipmentID"))
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
                equiplbl.Text = equipmentname
            End If
            reader.Close()
            typelbl.Text = Session("part_type")
            If Session("partID").ToString <> "" Then
                commandString = "select * from t_parts where partID=@partID"
                comm = New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@partID", Session("partID"))
                reader = comm.ExecuteReader
                If reader.Read Then
                    If reader.Item("oem_manufacturer").ToString <> "" And reader.Item("oem_partnumber").ToString <> "" Then
                        luberfinerpn.Value = appcode.GetLuberfinerPN(reader.Item("oem_manufacturer"), reader.Item("oem_partnumber"))
                    Else
                        If reader.Item("manufacturer").ToString <> "" And reader.Item("partnumber").ToString <> "" Then
                            luberfinerpn.Value = appcode.GetLuberfinerPN(reader.Item("manufacturer"), reader.Item("partnumber"))
                        ElseIf reader.Item("alt_manufacturer").ToString <> "" And reader.Item("alt_partnumber").ToString <> "" Then
                            luberfinerpn.Value = appcode.GetLuberfinerPN(reader.Item("alt_manufacturer"), reader.Item("alt_partnumber"))
                        End If
                    End If
                    manufacturerdd.SelectedValue = reader.Item("manufacturer").ToString.ToUpper
                    partnumbertb.Text = reader.Item("partnumber").ToString
                    If reader.Item("alt_manufacturer").ToString <> "" Then
                        altmfrdd.SelectedValue = reader.Item("alt_manufacturer").ToString.ToUpper
                    End If
                    altpntb.Text = reader.Item("alt_partnumber").ToString
                    If reader.Item("oem_manufacturer").ToString <> "" Then
                        oemmfrdd.SelectedValue = reader.Item("oem_manufacturer").ToString
                    End If
                    oempntb.Text = reader.Item("oem_partnumber").ToString
                    descriptiontb.Text = reader.Item("description").ToString
                    quantitytb.Text = reader.Item("quantity").ToString
                    uomtb.Text = reader.Item("uom").ToString
                    If reader.Item("price").ToString <> "" Or reader.Item("price") = 0 Then
                        pricetb.Text = reader.Item("price")
                    Else
                        pricetb.Text = appcode.GetCompanyPrice(Session("selected_companyID"), reader.Item("manufacturer").ToString, reader.Item("partnumber").ToString)
                    End If
                    notestb.Text = reader.Item("notes").ToString
                End If
                reader.Close()
            Else
                cancelbtn.Visible = False
            End If
            conn.Close()
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub submitbtn_Click(sender As Object, e As EventArgs) Handles submitbtn.Click
        If IsNumeric(quantitytb.Text) = False Then
            quantitytb.Text = "1"
        End If
        Dim price As Double
        If IsNumeric(pricetb.Text) = False Then
            pricetb.Text = appcode.GetCompanyPrice(Session("selected_companyID"), manufacturerdd.SelectedValue, partnumbertb.Text)
        End If
        manufacturerdd.SelectedValue = manufacturerdd.SelectedValue.ToUpper
        partnumbertb.Text = partnumbertb.Text.ToUpper
        If Session("partID").ToString <> "" Then
            appcode.UpdatePart(Session("partID"), Session("part_type"), Trim(manufacturerdd.SelectedValue), Trim(partnumbertb.Text), descriptiontb.Text, altmfrdd.SelectedValue, altpntb.Text, oemmfrdd.SelectedValue, oempntb.Text, Trim(quantitytb.Text), uomtb.Text, pricetb.Text, notestb.Text)
        Else
            Dim partID As Integer = appcode.InsertPart(Session("equipmentID"), Session("part_type"), Trim(manufacturerdd.SelectedValue), Trim(partnumbertb.Text), descriptiontb.Text, altmfrdd.SelectedValue, altpntb.Text, oemmfrdd.SelectedValue, oempntb.Text, Trim(quantitytb.Text), uomtb.Text, pricetb.Text, notestb.Text)
        End If
        Response.Redirect("EquipmentProfile.aspx")
    End Sub

    Protected Sub cancelbtn_Click(sender As Object, e As EventArgs) Handles cancelbtn.Click
        appcode.DeletePart(Session("partID"))
        Response.Redirect("EquipmentProfile.aspx")
    End Sub

    Protected Sub partnumbertb_TextChanged(sender As Object, e As EventArgs) Handles partnumbertb.TextChanged
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        Dim comm As New SqlCommand
        Dim reader As SqlDataReader
        commandString = "select * from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        comm = New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturerdd.SelectedValue)
        comm.Parameters.AddWithValue("@partnumber", partnumbertb.Text)
        reader = comm.ExecuteReader
        If reader.Read Then
            descriptiontb.Text = reader.Item("item")
            uomtb.Text = reader.Item("uom")
            quantitytb.Text = "1"
            pricetb.Text = FormatNumber(appcode.GetCompanyPrice(Session("selected_companyID"), manufacturerdd.SelectedValue, partnumbertb.Text), 2)
        End If
        reader.Close()
        conn.Close()
    End Sub

    Protected Sub backbtn_Click(sender As Object, e As EventArgs) Handles backbtn.Click
        Response.Redirect("EquipmentProfile.aspx")
    End Sub

    Protected Sub GridView3_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView3.RowCommand
        If e.CommandName = "ViewProfile" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim equipmentIDlbl As Label = GridView3.Rows(index).FindControl("equipmentIDlbl")
            Session("equipmentID") = equipmentIDlbl.Text
            Response.Redirect("EquipmentProfile.aspx")
        End If
    End Sub

End Class
