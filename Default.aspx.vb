Imports System.IO
Imports AjaxControlToolkit
Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Collections.Generic

Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    <System.Web.Services.WebMethod, System.Web.Script.Services.ScriptMethod> Public Shared Function GetSlides() As AjaxControlToolkit.Slide()
        Dim imgSlide(3) As AjaxControlToolkit.Slide
        imgSlide(0) = New AjaxControlToolkit.Slide("Images/active.jpg", "Active Radiator", "Active Radiator")
        imgSlide(1) = New AjaxControlToolkit.Slide("Images/alemite.jpg", "Alemite", "Alemite")
        imgSlide(2) = New AjaxControlToolkit.Slide("Images/baldwin.jpg", "Baldwin Filters", "Baldwin Filters")
        Return (imgSlide)
    End Function

End Class
