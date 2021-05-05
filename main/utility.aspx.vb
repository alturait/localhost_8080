Imports System.Data.SqlClient

Partial Class main_utility
    Inherits System.Web.UI.Page

    Protected Sub abtn_Click(sender As Object, e As EventArgs) Handles abtn.Click
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_equipment where locationID=@locationID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@locationID", DropDownList2.SelectedValue)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            DeleteParts(reader.Item("equipmentID"))
            DeleteKits(reader.Item("equipmentID"))
            DeleteEquipment(reader.Item("equipmentID"))
        End While
        reader.Close()
        conn.Close()
    End Sub

    Sub DeleteParts(ByVal equipmentID As Integer)
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_parts where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Sub DeleteKits(ByVal equipmentID As Integer)
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_serviceprofile where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            DeleteServiceParts(reader.Item("serviceprofileID"))
            DeleteServiceProfile(reader.Item("serviceprofileID"))
        End While
        conn.Close()
    End Sub

    Sub DeleteServiceParts(ByVal serviceprofileID As Integer)
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_serviceparts where serviceprofileID=@serviceprofileID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Sub DeleteServiceProfile(ByVal serviceprofileID As Integer)
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_serviceprofile where serviceprofileID=@serviceprofileID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Sub DeleteEquipment(ByVal equipmentID As Integer)
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_equipment where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub
End Class
