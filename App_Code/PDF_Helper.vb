Imports Microsoft.VisualBasic
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class PDF_Helper
    Inherits PdfPageEventHelper

    Public Overrides Sub OnEndPage(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document)
        Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
        Dim cb As PdfContentByte = writer.DirectContent
        cb.BeginText()
        cb.SetFontAndSize(bf, 12)
        cb.SetTextMatrix(50, 30)
        cb.ShowText("Page " & writer.PageNumber)
        cb.EndText()
    End Sub

End Class
