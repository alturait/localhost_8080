
Partial Class main_PurchasingHome
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'pendingposlbl.Text = appcode.GetPendingPOCount()
            purchasesmtdlbl.Text = FormatCurrency(appcode.GetPurchases(Month(Now()), Year(Now())), 2)
            'totalposlbl.Text = appcode.GetPOCount(Month(Now()), Year(Now()))
            'requisitionslbl.Text = appcode.GetRequisitionsCount("")
            'openreqlbl.Text = appcode.GetAllOpenRequisitionsCount()
            'inventoryitemslbl.Text = appcode.GetInventoryCount(0)
            'valuelbl.Text = FormatCurrency(appcode.GetInventoryValue(), 2)
            'resalelbl.Text = FormatCurrency(appcode.GetInventoryResaleValue(), 2)
            pagelbl.Text = "Purchasing Home"
        End If
    End Sub

End Class
