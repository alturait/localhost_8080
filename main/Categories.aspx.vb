Imports System.Data.SqlClient
Imports System.IO

Partial Class main_Categories
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("mode") = "delete" Then
                Dim haschildren As Boolean = appcode.HasChildren(Request.QueryString("categoryID"))
                If haschildren = False Then
                    Dim parentID As Integer = appcode.GetParentID(Request.QueryString("categoryID"))
                    'update categoryIDs of all products that use this categoryID
                    appcode.UpdateProductCategoryID(Request.QueryString("categoryID"), 0)
                    appcode.DeleteCategory(Request.QueryString("categoryID"))
                    Response.Redirect("Categories.aspx?categoryID=" & parentID)
                Else
                    emessagelbl.Text = "The category is not empty. Delete all child categories first."
                End If
            Else
                If Request.QueryString("categoryID") <> "0" Then
                    Dim conn As New SqlConnection(appcode.ConnectionString)
                    Dim commandString As String
                    conn.Open()
                    Dim comm As New SqlCommand
                    Dim reader As SqlDataReader
                    commandString = "select * from t_category where categoryID=@categoryID"
                    comm = New SqlCommand(commandString, conn)
                    comm.Parameters.AddWithValue("@categoryID", Request.QueryString("categoryID"))
                    reader = comm.ExecuteReader
                    If reader.Read Then
                        addtolbl.Text = reader.Item("category") & " - " & reader.Item("categoryID")
                        parentIDlbl.Value = reader.Item("parentID")
                        If File.Exists(Server.MapPath("~/Images/Catalog/catID" & reader.Item("categoryID").ToString & ".jpg")) = True Then
                            Image1.ImageUrl = "~/Images/Catalog/catID" & reader.Item("categoryID").ToString & ".jpg"
                            picturelbl.Text = "catID" & reader.Item("categoryID").ToString & ".jpg"
                        Else
                            Image1.ImageUrl = "~/Images/Catalog/blank.jpg"
                            picturelbl.Text = "blank.jpg"
                        End If
                    Else
                        backbtn.Enabled = False
                    End If
                    reader.Close()
                    conn.Close()
                Else
                    parentIDlbl.Value = "0"
                    addtolbl.Text = "Root Menu"
                End If
            End If
        End If
        pagelbl.Text = Page.Title
    End Sub

    Protected Sub backbtn_Click(sender As Object, e As EventArgs) Handles backbtn.Click
        Response.Redirect("Categories.aspx?categoryID=" & parentIDlbl.Value)
    End Sub

    Protected Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        If categorytb.Text <> "" Then
            Dim categoryID = appcode.InsertCategory(categorytb.Text, Request.QueryString("categoryID"))
            Response.Redirect("Categories.aspx?categoryID=" & Request.QueryString("categoryID"))
        End If
    End Sub

    Protected Sub uploadButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles uploadButton.Click
        If FileUpload1.HasFile Then
            Dim picture As String = "catID" & Request.QueryString("categoryID") & ".jpg"
            FileUpload1.SaveAs(Server.MapPath("~/Images/Catalog/" & picture))
            msglbl.Text = "File " & picture & " uploaded."
        Else
            msglbl.Text = "No File Uploaded."
        End If
        Response.Redirect("Categories.aspx?categoryID=" & Request.QueryString("categoryID"))
    End Sub

End Class
