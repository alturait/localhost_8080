
Partial Class main_AccountingHome
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            invoicedsaleslbl.Text = FormatCurrency(appcode.GetInvoices(0, Month(Now()), Year(Now())), 2)
            invoiceslbl.Text = appcode.GetInvoiceCount(0, Month(Now()), Year(Now()))
            rtilbl.Text = appcode.GetBillingCount(0)
            pagelbl.Text = Page.Title & " - " & MonthName(Month(Now()))
        End If
    End Sub

End Class
