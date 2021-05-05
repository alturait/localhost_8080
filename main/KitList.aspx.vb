
Partial Class main_KitList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("equipmentID") = ""
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        Page.MasterPageFile = appcode.GetMasterPage(Session("companyID"))
    End Sub

End Class
