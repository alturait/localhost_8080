﻿Imports System.Data.SqlClient

Partial Class main_CustomerRequisitions
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            
        End If
        pagelbl.Text = Page.Title
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

End Class
