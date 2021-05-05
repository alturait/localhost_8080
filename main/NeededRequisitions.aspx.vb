Imports System.Data.SqlClient

Partial Class main_NeededRequisitions
    Inherits System.Web.UI.Page

    Protected Sub manufacturerdd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles manufacturerdd.SelectedIndexChanged
        If manufacturerdd.SelectedValue <> "0" Then
            GridView1.DataSourceID = "SqlMfrRequisitions"
            GridView1.DataBind()
        End If
    End Sub

    Protected Sub addPObtn_Click(sender As Object, e As EventArgs) Handles addPObtn.Click
        Dim poID As Integer = podd.SelectedValue
        If podd.SelectedValue <> "0" Then
            For Each row In GridView1.Rows
                Dim polineID As Integer
                Dim rebate As Boolean
                Dim selectcb As CheckBox = row.FindControl("selectcb")
                Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
                Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
                Dim quantitylbl As Label = row.FindControl("quantitylbl")
                Dim needlbl As Label = row.FindControl("needlbl")
                Dim costlbl As Label = row.FindControl("costlbl")
                Dim uomlbl As Label = row.FindControl("uomlbl")
                If selectcb.Checked = True Then
                    rebate = False
                    polineID = appcode.InsertPOLine(poID, manufacturerlbl.Text, partnumberlbl.Text, needlbl.Text, costlbl.Text, uomlbl.Text, rebate, 0)
                End If
            Next
        Else
            Dim count As Integer = 0
            For Each row In GridView1.Rows
                Dim polineID As Integer
                Dim rebate As Boolean
                Dim selectcb As CheckBox = row.FindControl("selectcb")
                Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
                Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
                Dim quantitylbl As Label = row.FindControl("quantitylbl")
                Dim needlbl As Label = row.FindControl("needlbl")
                Dim costlbl As Label = row.FindControl("costlbl")
                Dim uomlbl As Label = row.FindControl("uomlbl")
                If selectcb.Checked = True Then
                    If count = 0 Then
                        poID = appcode.InsertPO(Session("vendorID"), appcode.GetCompany(Session("vendorID")), FormatDateTime(Now(), DateFormat.ShortDate), Session("userID"), appcode.GetUserName(Session("userID")))
                    End If
                    rebate = False
                    polineID = appcode.InsertPOLine(poID, manufacturerlbl.Text, partnumberlbl.Text, needlbl.Text, costlbl.Text, uomlbl.Text, rebate, 0)
                    count += 1
                End If
            Next
            Session("poID") = poID
            Response.Redirect("PurchaseOrder.aspx")
        End If
        Session("poID") = poID
        Response.Redirect("PurchaseOrder.aspx")
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("imfr").ToString <> "" Then
                manufacturerdd.SelectedValue = Session("imfr")
            End If
        End If
        Session("imfr") = ""
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub updatebtn_Click(sender As Object, e As EventArgs) Handles updatebtn.Click
        For Each row In GridView1.Rows
            Dim manufacturerlbl As Label = row.FindControl("manufacturerlbl")
            Dim partnumberlbl As Label = row.FindControl("partnumberlbl")
            Dim nstockcb As CheckBox = row.FindControl("nstockcb")
            Dim onhandtb As TextBox = row.FindControl("onhandtb")
            appcode.UpdateOnHand(manufacturerlbl.Text, partnumberlbl.Text, onhandtb.Text)
            appcode.UpdateNormalStock(manufacturerlbl.Text, partnumberlbl.Text, nstockcb.Checked)
        Next
        'Response.Redirect("Requisitions.aspx")
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Edit" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim manufacturerlbl As Label = GridView1.Rows(index).FindControl("manufacturerlbl")
            Dim partnumberlbl As Label = GridView1.Rows(index).FindControl("partnumberlbl")
            Dim productID As Integer = appcode.GetProductID(manufacturerlbl.Text, partnumberlbl.Text)
            Session("lastpage") = "Requisitions"
            Response.Redirect("Product.aspx?productID=" & productID.ToString)
        ElseIf e.CommandName = "Orders" Then
            Dim index = Convert.ToInt32(e.CommandArgument)
            Dim manufacturerlbl As Label = GridView1.Rows(index).FindControl("manufacturerlbl")
            Dim partnumberlbl As Label = GridView1.Rows(index).FindControl("partnumberlbl")
            Session("lastpage") = "Requisitions"
            Response.Redirect("RequisitionOrders.aspx?manufacturer=" & manufacturerlbl.Text & "&partnumber=" & partnumberlbl.Text)
        End If
    End Sub

    Protected Sub selectallcb_CheckedChanged(sender As Object, e As EventArgs) Handles selectallcb.CheckedChanged
        For Each row In GridView1.Rows
            Dim selectcb As CheckBox = row.FindControl("selectcb")
            If selectcb.Enabled = True Then
                If selectallcb.Checked = True Then
                    selectcb.Checked = True
                Else
                    selectcb.Checked = False
                End If
            End If
        Next
    End Sub

End Class
