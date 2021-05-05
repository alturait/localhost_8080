Imports System.Data.SqlClient
Imports System.IO

Partial Class customer_Default
    Inherits System.Web.UI.Page

    Protected Function GetPicture(ByVal partnumber As String) As String
        GetPicture = "blank"
        If File.Exists(Server.MapPath("~/Images/Catalog/" & partnumber & ".jpg")) = True Then
            GetPicture = partnumber
        End If
    End Function

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("this_locationID") = "0"
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub DataList2_ItemCommand(source As Object, e As DataListCommandEventArgs) Handles DataList2.ItemCommand
        If e.CommandName = "more" Then
            Dim productID As Integer = Convert.ToInt32(e.CommandArgument)
            Response.Redirect("CatalogPage.aspx?productID=" & productID)
        End If
    End Sub

End Class
