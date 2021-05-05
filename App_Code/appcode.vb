Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class appcode

    Public Shared ConnectionString As String = "Data Source=localhost;Initial Catalog=SQL2012_972956_desertfleet;Persist Security Info=True;User ID=SQL2012_972956_desertfleet_user;Password=Ar46bCX18"

    Public Shared Sub AddAllServices(ByVal companyID As Integer, ByVal verified As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_equipment where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If CountEquipmentServiceProfiles(reader.Item("equipmentID")) > 0 Then
                UpdatePM(reader.Item("equipmentID"), verified)
            End If
        End While
        reader.Close()
        conn.Close()
    End Sub

    Public Shared Function CheckTotalKitStockEfficiency(ByVal companyID As Integer) As Double
        CheckTotalKitStockEfficiency = 0
        Dim haveonhand As Integer = 0
        Dim numpartnumbers As Integer = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select manufacturer,partnumber from v_part_summary where companyID=@companyID group by manufacturer,partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            Dim onhand = GetOnHand(reader.Item("manufacturer"), reader.Item("partnumber"))
            If onhand > 0 Then
                haveonhand += 1

            End If
            numpartnumbers += 1
        End While
        CheckTotalKitStockEfficiency = haveonhand / numpartnumbers
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function CheckEquipmentKitStockEfficiency(ByVal companyID As Integer, ByVal equipment_description As String) As Double
        CheckEquipmentKitStockEfficiency = 0
        Dim haveonhand As Integer = 0
        Dim numpartnumbers As Integer = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select manufacturer,partnumber from v_part_summary where companyID=@companyID and equipment_description=@equipment_description group by manufacturer,partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@equipment_description", equipment_description)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            Dim onhand = GetOnHand(reader.Item("manufacturer"), reader.Item("partnumber"))
            If onhand > 0 Then
                haveonhand += 1

            End If
            numpartnumbers += 1
        End While
        CheckEquipmentKitStockEfficiency = haveonhand / numpartnumbers
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function CheckLocationKitStockEfficiency(ByVal companyID As Integer, ByVal locationID As Integer) As Double
        CheckLocationKitStockEfficiency = 0
        Dim haveonhand As Integer = 0
        Dim numpartnumbers As Integer = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select manufacturer,partnumber from v_part_summary where companyID=@companyID and locationID=@locationID group by manufacturer,partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@locationID", locationID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            Dim onhand = GetOnHand(reader.Item("manufacturer"), reader.Item("partnumber"))
            If onhand > 0 Then
                haveonhand += 1

            End If
            numpartnumbers += 1
        End While
        CheckLocationKitStockEfficiency = haveonhand / numpartnumbers
        reader.Close()
        conn.Close()
    End Function

    Public Shared Sub AdjustInventory(ByVal shipmentID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_shipment_lines where shipmentID=@shipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            Dim onhand = GetOnHand(reader.Item("manufacturer"), reader.Item("partnumber"))
            UpdateOnHand(reader.Item("manufacturer"), reader.Item("partnumber"), onhand + reader.Item("ship_qty"))
        End While
        reader.Close()
        conn.Close()
    End Sub

    Public Shared Function ConvertGuestCart(ByVal ssID As String, ByVal vendorID As Integer, ByVal companyID As Integer, ByVal userID As Integer) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_cart set vendorID=@vendorID,companyID=@companyID,userID=@userID where ssID=@ssID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@vendorID", vendorID)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@ssID", ssID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Function

    Public Shared Function CopyQuoteToOrder(ByVal quoteID As Integer) As Integer
        CopyQuoteToOrder = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_quote where quoteID=@quoteID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@quoteID", quoteID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            Dim repID As Integer = GetRepID(reader.Item("companyID"))
            CopyQuoteToOrder = InsertOrder("", reader.Item("companyID"), reader.Item("company").ToString, reader.Item("vendorID"), reader.Item("vendor").ToString, reader.Item("v_phone").ToString, reader.Item("v_fax").ToString, FormatDateTime(Now(), DateFormat.ShortDate), "", False, False, False, "", reader.Item("remitto").ToString, reader.Item("r_address1").ToString, reader.Item("r_address2").ToString, reader.Item("r_city").ToString, reader.Item("r_state").ToString, reader.Item("r_zipcode").ToString, reader.Item("b_address1").ToString, reader.Item("b_address2").ToString, reader.Item("b_city").ToString, reader.Item("b_state").ToString, reader.Item("b_zipcode").ToString, reader.Item("shipID"), reader.Item("shipto").ToString, reader.Item("s_address1").ToString, reader.Item("s_address2").ToString, reader.Item("s_address3").ToString, reader.Item("s_city").ToString, reader.Item("s_state").ToString, reader.Item("s_zipcode").ToString, reader.Item("userID"), reader.Item("c_contact").ToString, reader.Item("c_phone").ToString, reader.Item("c_fax").ToString, reader.Item("notes").ToString, "Local Delivery", "Ship & BO", reader.Item("is_kit"), reader.Item("serviceID"), reader.Item("serviceprofileID"), "", False, repID)
        End If
        reader.Close()
        commandString = "select * from t_quote_line where quoteID=@quoteID"
        comm = New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@quoteID", quoteID)
        reader = comm.ExecuteReader
        While reader.Read
            InsertOrderLine(CopyQuoteToOrder, reader.Item("partID"), reader.Item("assetID"), reader.Item("manufacturer").ToString, reader.Item("partnumber").ToString, reader.Item("item").ToString, reader.Item("quantity"), reader.Item("price"), reader.Item("uom").ToString, reader.Item("availability").ToString, reader.Item("quote"), reader.Item("kitID"))
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function CountPMAgreements(ByVal companyID As Integer) As Integer
        CountPMAgreements = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select count(equipmentID) as pmcount from t_equipment where companyID=@companyID and verified=@verified"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@verified", True)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            CountPMAgreements = reader.Item("pmcount")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function CountEquipmentServiceProfiles(ByVal equipmentID As Integer) As Integer
        CountEquipmentServiceProfiles = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select count(serviceprofileID) as servicecount from v_serviceprofile where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@complete", True)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            CountEquipmentServiceProfiles = reader.Item("servicecount")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Sub DeleteStockItemPricing(ByVal companyID As Integer, ByVal manufacturer As String, ByVal partnumber As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_pricing where companyID=@companyID and manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteAllPricing(ByVal companyID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_pricing where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteAllEquipment(ByVal companyID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_equipment where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            DeleteEquipment(reader.Item("equipmentID"))
        End While
        reader.Close()
        conn.Close()
    End Sub

    Public Shared Sub DeleteGuestCartItems(ByVal ssID As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_cart where ssID=@ssID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@ssID", ssID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteCartItems(ByVal companyID As Integer, ByVal vendorID As Integer, ByVal userID As Integer, ByVal kitID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_cart where companyID=@companyID and vendorID=@vendorID and userID=@userID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@vendorID", vendorID)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteCartItem(ByVal cartID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_cart where cartID=@cartID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@cartID", cartID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteCompany(ByVal companyID As Integer)
        DeleteUsers(companyID)
        DeleteShipTos(companyID)
        DeleteAllEquipment(companyID)
        DeleteCompanyUsers(companyID)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "delete from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteCompanyUsers(ByVal companyID As Integer)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "delete from t_user_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteEquipment(ByVal equipmentID As Integer)
        DeleteParts(equipmentID)
        DeleteEquipmentServiceProfiles(equipmentID)
        'delete services
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_equipment where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteEquipmentServiceProfiles(ByVal equipmentID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_serviceprofile where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            DeleteServiceProfile(reader.Item("serviceprofileID"))
        End While
        reader.Close()
        conn.Close()
    End Sub

    Public Shared Sub DeleteListItem(ByVal listitemID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_listitem where listitemID=@listitemID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@listitemID", listitemID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeletePart(ByVal partID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_parts where partID=@partID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@partID", partID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteParts(ByVal equipmentID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_parts where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteFuelService(ByVal fuelserviceID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_fuelservice where fuelserviceID=@fuelserviceID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@fuelserviceID", fuelserviceID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteOrder(ByVal orderID As Integer)
        DeleteOrderLines(orderID)
        DeleteShipments(orderID)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_order where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteOrderLine(ByVal lineID As Integer)
        'delete from a purchase order
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_order_line where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        comm.ExecuteNonQuery()
        conn.Close()
        DeleteRequisition(lineID)
    End Sub

    Public Shared Sub DeleteOrderLines(ByVal orderID As Integer)
        'delete from a purchase order
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_order_line where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.ExecuteNonQuery()
        conn.Close()
        DeleteRequisitions(orderID)
    End Sub

    Public Shared Sub DeletePO(ByVal poID As Integer)
        DeletePOLines(poID)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_po where poID=@poID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@poID", poID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeletePOLine(ByVal polineID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_poline where polineID=@polineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@polineID", polineID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeletePOLines(ByVal poID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_poline where poID=@poID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@poID", poID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            DeletePOLine(reader.Item("polineID"))
            UpdateRequisitionOnPO(reader.Item("lineID"), False)
        End While
        conn.Close()
    End Sub

    Public Shared Sub DeleteProduct(ByVal productID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_product where productID=@productID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@productID", productID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            DeleteAttributes(reader.Item("manufacturer"), reader.Item("partnumber"))
            DeleteVersions(reader.Item("manufacturer"), reader.Item("partnumber"))
        End If
        reader.Close()
        commandString = "delete from t_product where productID=@productID"
        comm = New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@productID", productID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteQuote(ByVal quoteID As Integer)
        DeleteQuoteLines(quoteID)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_quote where quoteID=@quoteID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@quoteID", quoteID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteQuoteLine(ByVal lineID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_quote_line where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteQuoteLines(ByVal quoteID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_quote_line where quoteID=@quoteID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@quoteID", quoteID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteRequisition(ByVal lineID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_requisition where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteRequisitions(ByVal orderID As Integer)
        'delete from a purchase order
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_requisition where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            DeleteRequisition(reader.Item("lineID"))
        End While
        reader.Close()
        conn.Close()
    End Sub

    Public Shared Sub DeleteService(ByVal serviceID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_service where serviceID=@serviceID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceID", serviceID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteServiceParts(ByVal serviceprofileID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_serviceparts where serviceprofileID=@serviceprofileID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteServiceProfile(ByVal serviceprofileID As Integer)
        DeleteServiceParts(serviceprofileID)
        DeleteServices(serviceprofileID)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_serviceprofile where serviceprofileID=@serviceprofileID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteServices(ByVal serviceprofileID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_service where serviceprofileID=@serviceprofileID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteShipment(ByVal shipmentID As Integer)
        AdjustInventory(shipmentID)
        DeleteShipmentLines(shipmentID)
        UpdateOrderComplete(GetShipmentOrderID(shipmentID), False)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_shipment where shipmentID=@shipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteShipmentLines(ByVal shipmentID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_shipment_line where shipmentID=@shipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteShipments(ByVal orderID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_shipment where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            DeleteShipmentLines(reader.Item("shipmentID"))
            DeleteShipment(reader.Item("shipmentID"))
        End While
        reader.Close()
        conn.Close()
    End Sub

    Public Shared Sub DeleteShipTo(ByVal shipID As Integer)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "delete from t_ship where shipID=@shipID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipID", shipID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteShipTos(ByVal companyID As Integer)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "delete from t_ship where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteUserLocations(ByVal userID As Integer)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "delete from t_user_location where userID=@userID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteUser(ByVal userID As Integer)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "delete from t_user where userID=@userID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteUserCompany(ByVal usercompanyID As Integer)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "delete from t_user_company where usercompanyID=@usercompanyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@usercompanyID", usercompanyID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteUserCompanies(ByVal companyID As Integer)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "delete from t_user_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteUsers(ByVal companyID As Integer)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "delete from t_user where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteWorkDescription(ByVal descriptionID As Integer)
        DeleteWorkListItems(descriptionID)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "delete from t_workdescription where descriptionID=@descriptionID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@descriptionID", descriptionID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteWorkListItems(ByVal descriptionID As Integer)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "delete from t_workline where descriptionID=@descriptionID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@descriptionID", descriptionID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteWorkListItem(ByVal worklineID As Integer)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "delete from t_workline where worklineID=@worklineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@worklineID", worklineID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Function GetMin(ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetMin = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select min from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetMin = reader.Item("min")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetMax(ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetMax = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select max from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetMax = reader.Item("max")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetReorder(ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetReorder = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select reorder from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetReorder = reader.Item("reorder")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetBO(ByVal lineID As Integer) As Double
        GetBO = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select lineID,quantity,sum(ship_qty) as ship_qty from v_shipment_lines where lineID=@lineID group by lineID,quantity"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        comm.Parameters.AddWithValue("@shipped", True)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetBO = reader.Item("quantity") - reader.Item("ship_qty")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetBranchID(ByVal companyID As Integer) As Integer
        GetBranchID = "0"
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetBranchID = reader.Item("branchID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetBranchLocation(ByVal shipID As Integer) As String
        GetBranchLocation = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_ship where shipID=@shipID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipID", shipID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetBranchLocation = GetCompany(reader.Item("companyID")) & "<br/>"
            GetBranchLocation &= reader.Item("s_address1") & "<br/>"
            If reader.Item("s_address2").ToString <> "" Then
                GetBranchLocation &= reader.Item("s_address2") & "<br/>"
            End If
            GetBranchLocation &= reader.Item("s_city") & ", " & reader.Item("s_state") & " " & reader.Item("s_zipcode")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetCompany(ByVal companyID As Integer) As String
        GetCompany = "None"
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetCompany = reader.Item("company")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetCompanyBillingEmail(ByVal companyID As Integer) As String
        GetCompanyBillingEmail = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetCompanyBillingEmail = reader.Item("billing_email")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetCompanyContactEmail(ByVal companyID As Integer) As String
        GetCompanyContactEmail = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetCompanyContactEmail = reader.Item("contact_email")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetCompanyWarehouseEmail(ByVal companyID As Integer) As String
        GetCompanyWarehouseEmail = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetCompanyWarehouseEmail = reader.Item("warehouse_email")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetShipOptions(ByVal orderID As Integer) As String
        GetShipOptions = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetShipOptions = reader.Item("ship_options")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function SinglePN(ByVal partnumber As String) As Boolean
        SinglePN = False
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim commandString As String = "select count(productID) as pncount from t_product where partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("pncount") < 2 Then
                SinglePN = True
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function ispn(ByVal partnumber As String) As Boolean
        ispn = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_product where partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            ispn = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function isNormalStock(ByVal manufacturer As String, ByVal partnumber As String) As Boolean
        isNormalStock = False
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim commandString As String = "select * from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            isNormalStock = reader.Item("nStock")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function isOrderRTS(ByVal orderID As Integer) As Boolean
        isOrderRTS = True
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim commandString As String = "select * from t_order_line where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            Dim onhand = GetOnHand(reader.Item("manufacturer"), reader.Item("partnumber"))
            If IsOrderLineOpen(reader.Item("lineID")) = True Then
                If onhand < reader.Item("quantity") Then
                    If GetShipOptions(orderID) = "Ship Complete" Then
                        isOrderRTS = False
                    End If
                End If
            End If
        End While
        If IsSample(orderID) = True Then
            If IsSampleComplete(orderID) = False Then
                isOrderRTS = False
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsSample(ByVal orderID As Integer) As Boolean
        IsSample = False
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim commandString As String = "select sample from t_order where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsSample = reader.Item("sample")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsSampleComplete(ByVal orderID As Integer) As Boolean
        IsSampleComplete = False
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim commandString As String = "select sample_complete from t_order where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsSampleComplete = reader.Item("sample_complete")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsWarehouseItem(ByVal manufacturer As String, ByVal partnumber As String, ByVal companyID As Integer) As Boolean
        IsWarehouseItem = False
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim commandString As String = "select * from t_warehouse where manufacturer=@manufacturer and partnumber=@partnumber and companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsWarehouseItem = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function isKitComplete(ByVal serviceprofileID As Integer, ByVal orderID As Integer) As Boolean
        isKitComplete = True
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim commandString As String = "select * from v_serviceparts where serviceprofileID=@serviceprofileID and selected=@selected"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.Parameters.AddWithValue("@selected", True)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            Dim onhand = GetOnHand(reader.Item("manufacturer"), reader.Item("partnumber"))
            If onhand < reader.Item("quantity") Then
                If GetShipOptions(orderID) = "Ship Complete" Then
                    isKitComplete = False
                End If
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsNeed(ByVal manufacturer As String, ByVal partnumber As String) As Boolean
        IsNeed = False
        If GetNeedOnHand(manufacturer, partnumber) > 0 Then
            IsNeed = True
        End If
    End Function

    Public Shared Function GetPDFAttachment(ByVal flyerID As Integer) As String
        GetPDFAttachment = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select pdf_attachment from t_flyer where flyerID=@flyerID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@flyerID", flyerID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetPDFAttachment = reader.Item("pdf_attachment")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetPartnumberByProductID(ByVal productID As Integer) As String
        GetPartnumberByProductID = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select partnumber from t_product where productID=@productID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@productID", productID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetPartnumberByProductID = reader.Item("partnumber")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetNeedsPO() As Integer
        GetNeedsPO = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select count(orderID) as pocount from t_order where needspo=@needspo"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@needspo", True)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetNeedsPO = reader.Item("pocount")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetManufacturerByProductID(ByVal productID As Integer) As String
        GetManufacturerByProductID = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select manufacturer from t_product where productID=@productID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@productID", productID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetManufacturerByProductID = reader.Item("manufacturer")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetNeedOnHand(ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetNeedOnHand = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select manufacturer,partnumber,sum(quantity) as quantity from v_requisitions where manufacturer=@manufacturer and partnumber=@partnumber group by manufacturer,partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetNeedOnHand = reader.Item("quantity") - GetOnHand(manufacturer, partnumber)
            If GetNeedOnHand < 0 Then
                GetNeedOnHand = 0
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetNeed(ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetNeed = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select manufacturer,partnumber,sum(quantity) as quantity from v_requisitions where manufacturer=@manufacturer and partnumber=@partnumber group by manufacturer,partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetNeed = reader.Item("quantity") - (GetOnHand(manufacturer, partnumber) + GetOnPO(manufacturer, partnumber))
            If GetNeed < 0 Then
                GetNeed = 0
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetCost(ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetCost = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select cost from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetCost = reader.Item("cost")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetExtended(ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetExtended = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select cost,package from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetExtended = reader.Item("cost") * reader.Item("package")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetSPA(ByVal companyID As Integer, ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetSPA = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select price from t_pricing where companyID=@companyID and manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetSPA = reader.Item("price")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function isRebate(ByVal manufacturer As String, ByVal partnumber As String) As Boolean
        isRebate = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            isRebate = reader.Item("rebate")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetCompanyPrice(ByVal companyID As Integer, ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetCompanyPrice = 0
        Dim spa As Double = GetSPA(companyID, manufacturer, partnumber)
        If spa <> 0 Then
            GetCompanyPrice = spa
        Else
            Dim msrp As Double = GetProductMSRP(manufacturer, partnumber)
            Dim saleprice As Double = GetSalePrice(manufacturer, partnumber)
            Dim price_category As String = GetPriceCategory(manufacturer, partnumber)
            Dim discount_price As Double = saleprice
            If isRebate(manufacturer, partnumber) = True Then
                discount_price = msrp * (1 - GetDiscount(companyID, manufacturer, price_category))
            End If
            If discount_price < saleprice Then
                GetCompanyPrice = discount_price
            Else
                GetCompanyPrice = saleprice
            End If
        End If
        GetCompanyPrice = FormatNumber(GetCompanyPrice, 2)
    End Function

    Public Shared Function GetEquipment(ByVal equipmentID As Integer) As String
        GetEquipment = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_equipment where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetEquipment = reader.Item("equipment_oem")
            If reader.Item("equipment_model").ToString <> "" Then
                GetEquipment &= " " & reader.Item("equipment_model")
            End If
            If reader.Item("equipment_description") <> "" Then
                GetEquipment &= " " & reader.Item("equipment_description")
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetPriceCategory(ByVal manufacturer As String, ByVal partnumber As String) As String
        GetPriceCategory = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select price_category from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetPriceCategory = reader.Item("price_category")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetProductID(ByVal manufacturer As String, ByVal partnumber As String) As String
        GetProductID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select productID from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetProductID = reader.Item("productID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetDefaultUserID(ByVal companyID As Integer) As Integer
        GetDefaultUserID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_user where companyID=@companyID order by name"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetDefaultUserID = reader.Item("userID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetDiscount(ByVal companyID As Integer, ByVal manufacturer As String, ByVal price_category As String) As Double
        GetDiscount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select discount from t_discount where companyID=@companyID and manufacturer=@manufacturer and price_category=@price_category"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@price_category", price_category)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetDiscount = reader.Item("discount")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function InsertLogin(ByVal userID As Integer) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_logins (userID) values(@userID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertLogin = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertXref(ByVal ref_manufacturer As String, ByVal ref_partnumber As String, ByVal xref_manufacturer As String, ByVal xref_partnumber As String) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_xref (ref_manufacturer,ref_partnumber,xref_manufacturer,xref_partnumber) values(@ref_manufacturer,@ref_partnumber,@xref_manufacturer,@xref_partnumber)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@ref_manufacturer", ref_manufacturer)
        comm.Parameters.AddWithValue("@ref_partnumber", ref_partnumber)
        comm.Parameters.AddWithValue("@xref_manufacturer", xref_manufacturer)
        comm.Parameters.AddWithValue("@xref_partnumber", xref_partnumber)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertXref = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Sub DeleteDiscount(ByVal multiplierID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_discount where multiplierID=@multiplierID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@multiplierID", multiplierID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub RemoveCatalogItem(ByVal productID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_product set category=0 where productID=@productID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@productID", productID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub RemoveXref(ByVal ref_manufacturer As String, ByVal ref_partnumber As String, ByVal xref_manufacturer As String, ByVal xref_partnumber As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_xref where ref_manufacturer=@ref_manufacturer and ref_partnumber=@ref_partnumber and xref_manufacturer=@xref_manufacturer and xref_partnumber=@xref_partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@ref_manufacturer", ref_manufacturer)
        comm.Parameters.AddWithValue("@ref_partnumber", ref_partnumber)
        comm.Parameters.AddWithValue("@xref_manufacturer", xref_manufacturer)
        comm.Parameters.AddWithValue("@xref_partnumber", xref_partnumber)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Function InsertEquipmentFluid(ByVal equipmentID As Integer, ByVal fluidID As Integer, ByVal uom As String, ByVal quantity As Double, ByVal mobileuserID As Integer, ByVal hours_miles As Double, ByVal note As String) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_efluid (equipmentID,fluidID,uom,quantity,mobileuserID,hours_miles,note) values(@equipmentID,@fluidID,@uom,@quantity,@mobileuserID,@hours_miles,@note)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.Parameters.AddWithValue("@fluidID", fluidID)
        comm.Parameters.AddWithValue("@uom", uom)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.Parameters.AddWithValue("@mobileuserID", mobileuserID)
        comm.Parameters.AddWithValue("@hours_miles", hours_miles)
        comm.Parameters.AddWithValue("@note", note)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertEquipmentFluid = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertDiscount(ByVal companyID As Integer, ByVal vendorID As Integer, ByVal manufacturer As String, ByVal price_category As String, ByVal discount As Double) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_discount (companyID,vendorID,manufacturer,price_category,discount) values(@companyID,@vendorID,@manufacturer,@price_category,@discount)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@vendorID", vendorID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@price_category", price_category)
        comm.Parameters.AddWithValue("@discount", discount)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertDiscount = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertCompanyFluid(ByVal companyID As Integer, ByVal fluid As String) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_fluid (companyID,fluid) values(@companyID,@fluid)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@fluid", fluid)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertCompanyFluid = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Sub UpdateServiceFlagA(ByVal equipmentID As Integer, ByVal serviceFlagA As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_equipment set serviceFlagA=@serviceFlagA where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.Parameters.AddWithValue("@serviceFlagA", serviceFlagA)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateCompanyFluid(ByVal fluidID As Integer, ByVal fluid As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_fluid set fluid=@fluid where fluidID=@fluidID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@fluid", fluid)
        comm.Parameters.AddWithValue("@fluidID", fluidID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteCompanyFluid(ByVal fluidID As Integer)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "delete from t_fluid where fluidID=@fluidID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@fluidID", fluidID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateMailingList(ByVal listID As Integer, ByVal listname As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_lists set listname=@listname where listID=@listID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@listID", listID)
        comm.Parameters.AddWithValue("@listname", listname)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateOrderPO(ByVal orderID As Integer, ByVal purchaseorder As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_order set purchaseorder=@purchaseorder where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.Parameters.AddWithValue("@purchaseorder", purchaseorder)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateDeliverBy(ByVal orderID As Integer, ByVal deliverby_date As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_order set deliverby_date=@deliverby_date where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.Parameters.AddWithValue("@deliverby_date", deliverby_date)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateNeedsPO(ByVal orderID As Integer, ByVal needspo As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_order set needspo=@needspo where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.Parameters.AddWithValue("@needspo", needspo)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateOrderChargeTax(ByVal orderID As Integer, ByVal chargetax As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_order set chargetax=@chargetax where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.Parameters.AddWithValue("@chargetax", chargetax)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateDiscount(ByVal companyID As Integer, ByVal vendorID As Integer, ByVal manufacturer As String, ByVal price_category As String, ByVal discount As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_discount set discount=@discount where companyID=@companyID and vendorID=@vendorID and manufacturer=@manufacturer and price_category=@price_category"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@vendorID", vendorID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@price_category", price_category)
        comm.Parameters.AddWithValue("@discount", discount)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Function HasOilSample(ByVal orderID As Integer) As Boolean
        HasOilSample = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order_line where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If reader.Item("manufacturer") = "EMPIRE-CAT" And Left(reader.Item("partnumber"), 3) = "OA1" Then
                HasOilSample = True
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function HasDiscount(ByVal companyID As Integer, ByVal vendorID As Integer, ByVal manufacturer As String, ByVal price_category As String) As Boolean
        HasDiscount = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_discount where companyID=@companyID and vendorID=@vendorID and manufacturer=@manufacturer and price_category=@price_category"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@vendorID", vendorID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@price_category", price_category)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            HasDiscount = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Sub DeleteAllWarehouseItems(ByVal companyID As Integer)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "delete from t_warehouse where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteWarehouseItem(ByVal warehouseID As Integer)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "delete from t_warehouse where warehouseID=@warehouseID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@warehouseID", warehouseID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Function InsertWarehouseItem(ByVal location As String, ByVal manufacturer As String, ByVal partnumber As String, ByVal companyID As Integer, ByVal onhand As Double, ByVal min As Double, ByVal max As Double) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_warehouse (location,companyID,manufacturer,partnumber,onhand,min,max) values(@location,@companyID,@manufacturer,@partnumber,@onhand,@min,@max)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@location", location)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@onhand", onhand)
        comm.Parameters.AddWithValue("@min", min)
        comm.Parameters.AddWithValue("@max", max)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertWarehouseItem = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Sub UpdateWarehouseItem(ByVal warehouseID As Integer, ByVal location As String, ByVal onhand As Double, ByVal min As Double, ByVal max As Double, ByVal nStock As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_warehouse set location=@location,onhand=@onhand,min=@min,max=@max,nStock=@nStock where warehouseID=@warehouseID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@warehouseID", warehouseID)
        comm.Parameters.AddWithValue("@location", location)
        comm.Parameters.AddWithValue("@min", min)
        comm.Parameters.AddWithValue("@max", max)
        comm.Parameters.AddWithValue("@onhand", onhand)
        comm.Parameters.AddWithValue("@nStock", nStock)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateWarehouseItem1(ByVal warehouseID As Integer, ByVal onhand As Double, ByVal min As Double, ByVal max As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_warehouse set onhand=@onhand,min=@min,max=@max where warehouseID=@warehouseID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@warehouseID", warehouseID)
        comm.Parameters.AddWithValue("@min", min)
        comm.Parameters.AddWithValue("@max", max)
        comm.Parameters.AddWithValue("@onhand", onhand)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Function GetCatalogProductID(ByVal manufacturer As String, ByVal partnumber As String, ByVal companyID As Integer, ByVal vendorID As Integer) As Integer
        GetCatalogProductID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select productID from t_product where manufacturer=@manufacturer and partnumber=@partnumber and (companyID=@companyID or companyID=@vendorID or companyID=0)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@vendorID", vendorID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetCatalogProductID = reader.Item("productID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetCompanyShipToAddress(ByVal shipID As Integer) As String
        GetCompanyShipToAddress = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_ship where shipID=@shipID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipID", shipID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetCompanyShipToAddress = reader.Item("shipto") & "<br/>" & reader.Item("s_address1") & "<br/>"
            If reader.Item("s_address2").ToString <> "" Then
                GetCompanyShipToAddress &= reader.Item("s_address2") & "<br/>"
            End If
            If reader.Item("s_address3").ToString <> "" Then
                GetCompanyShipToAddress &= reader.Item("s_address3") & "<br/>"
            End If
            GetCompanyShipToAddress &= reader.Item("s_city") & ", " & reader.Item("s_state") & " " & reader.Item("s_zipcode")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetCoreID(ByVal productID As String) As Integer
        GetCoreID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_product where productID=@productID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@productID", productID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetCoreID = reader.Item("coreID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetCoreCharge(ByVal productID As String) As Double
        GetCoreCharge = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_product where productID=@productID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@productID", productID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetCoreCharge = reader.Item("msrp")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetDefaultServiceProfileID(ByVal equipmentID As Integer) As Integer
        GetDefaultServiceProfileID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_serviceprofile where equipmentID=@equipmentID order by weight"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetDefaultServiceProfileID = reader.Item("serviceprofileID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetDefaultShipID(ByVal companyID As Integer) As Integer
        GetDefaultShipID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_ship where companyID=@companyID order by shipto"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetDefaultShipID = reader.Item("shipID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetEstimatedDelivery(ByVal orderID As Integer) As String
        GetEstimatedDelivery = ""
        Dim LongestLeadTime As String = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_order_lines where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If IsOrderLineOpen(reader.Item("lineID")) = True Then
                If GetEstimatedDelivery <> "Open" Then
                    GetEstimatedDelivery = GetEstimatedArrival(reader.Item("manufacturer"), reader.Item("partnumber"), reader.Item("quantity"))
                    If GetEstimatedDelivery <> "Stock" Then
                        If IsDate(GetEstimatedDelivery) = True Then
                            If LongestLeadTime <> "" Then
                                If GetEstimatedDelivery > LongestLeadTime Then
                                    LongestLeadTime = GetEstimatedDelivery
                                End If
                            Else
                                LongestLeadTime = GetEstimatedDelivery
                            End If
                        End If
                    End If
                End If
            End If
        End While
        reader.Close()
        conn.Close()
        If LongestLeadTime <> "" Then
            GetEstimatedDelivery = LongestLeadTime
        End If
    End Function

    Public Shared Function GetEstimatedComplete(ByVal orderID As Integer) As String
        GetEstimatedComplete = FormatDateTime(Now(), DateFormat.ShortDate)
        Dim EstimatedArrival As String
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select manufacturer, partnumber,quantity from t_order_line where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            EstimatedArrival = GetEstimatedArrival(reader.Item("manufacturer"), reader.Item("partnumber"), reader.Item("quantity"))
            If FormatDateTime(EstimatedArrival, DateFormat.ShortDate) > GetEstimatedComplete Then
                GetEstimatedComplete = EstimatedArrival
            End If
        End While
        reader.Close()
        conn.Close()
        UpdateDeliverBy(orderID, GetEstimatedComplete)
    End Function

    Public Shared Function GetEstimatedArrival(ByVal manufacturer As String, ByVal partnumber As String, ByVal quantity_needed As Double) As String
        GetEstimatedArrival = FormatDateTime(Now().AddDays(7), DateFormat.ShortDate)
        Dim onhand As Double = GetOnHand(manufacturer, partnumber)
        If onhand >= quantity_needed Then
            GetEstimatedArrival = FormatDateTime(Now(), DateFormat.ShortDate)
        Else
            Dim conn As New SqlConnection(ConnectionString)
            Dim commandString As String
            conn.Open()
            commandString = "select estimated_arrival from v_poline where manufacturer=@manufacturer and partnumber=@partnumber and complete='False' and quantity > received order by date_submitted asc"
            Dim comm As New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@manufacturer", manufacturer)
            comm.Parameters.AddWithValue("@partnumber", partnumber)
            Dim reader As SqlDataReader = comm.ExecuteReader
            If reader.Read Then
                GetEstimatedArrival = reader.Item("estimated_arrival").ToString
            End If
            reader.Close()
            conn.Close()
        End If
    End Function

    Public Shared Function GetInvoiceTotal(ByVal shipmentID As Integer) As Double
        GetInvoiceTotal = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select ship_charge,sales_tax,sum(ship_qty*price) as subtotal from v_shipment_lines where shipmentID=@shipmentID group by ship_charge,sales_tax"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetInvoiceTotal = reader.Item("subtotal") + reader.Item("sales_tax") + reader.Item("ship_charge")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetItemName(ByVal manufacturer As String, ByVal partnumber As String) As String
        GetItemName = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select item from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetItemName = reader.Item("item")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetKitCode(ByVal serviceprofileID As Integer) As String
        GetKitCode = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select manufacturer,partnumber,quantity,selected from v_serviceparts where serviceprofileID=@serviceprofileID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If reader.Item("selected") = True Then
                GetKitCode &= reader.Item("manufacturer") & reader.Item("partnumber") & reader.Item("quantity")
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetFilterList(ByVal serviceprofileID As Integer) As String
        GetFilterList = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_servicepart where serviceprofileID=@serviceprofileID order by partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        Dim x As Integer = 0
        While reader.Read
            If reader.Item("selected") = True Then
                If x > 0 Then
                    GetFilterList &= ", "
                End If
                GetFilterList &= reader.Item("manufacturer") & " " & reader.Item("partnumber")
                x += 1
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetKitCost(ByVal serviceprofileID As Integer) As Double
        GetKitCost = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select price,quantity,selected from v_kitparts where serviceprofileID=@serviceprofileID group by price,quantity,selected"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If reader.Item("selected") = True Then
                GetKitCost += (reader.Item("price") * reader.Item("quantity"))
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetKitServiceProfileID(ByVal kitcode As String, ByVal equipmentID As Integer) As Double
        GetKitServiceProfileID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_kitparts where equipmentID=@equipmentID and kitcode=@kitcode"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.Parameters.AddWithValue("@kitcode", kitcode)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetKitServiceProfileID = reader.Item("serviceprofileID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetKitEquipmentID(ByVal kitcode As String) As Double
        GetKitEquipmentID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select equipmentID from v_kitparts where kitcode=@kitcode group by equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@kitcode", kitcode)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetKitEquipmentID = reader.Item("equipmentID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetKitContents(ByVal kitcode As String, ByVal companyID As Integer) As String
        GetKitContents = ""
        Dim x As Integer = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select manufacturer,partnumber,quantity from v_kitparts where kitcode=@kitcode and companyID=@companyID and selected=@selected group by manufacturer,partnumber,quantity order by manufacturer,partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@kitcode", kitcode)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@selected", True)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If x <> 0 Then
                GetKitContents &= ", "
            End If
            GetKitContents &= reader.Item("manufacturer") & " " & reader.Item("partnumber") & "(" & reader.Item("quantity") & ")"
            x += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetAssets(ByVal kitcode As String, ByVal companyID As Integer) As String
        GetAssets = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_serviceprofile where kitcode=@kitcode and companyID=@companyID order by assetID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@kitcode", kitcode)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            Dim equipmentname As String = ""
            If reader.Item("equipment_year").ToString <> "" Then
                equipmentname &= reader.Item("equipment_year") & " "
            End If
            equipmentname &= reader.Item("equipment_oem")
            If reader.Item("equipment_model").ToString <> "" Then
                equipmentname &= " " & reader.Item("equipment_model")
            End If
            If reader.Item("equipment_description").ToString <> "" Then
                equipmentname &= " " & reader.Item("equipment_description")
            End If
            equipmentname &= " (" & reader.Item("name") & ")"
            GetAssets &= "<a href='EquipmentProfile.aspx?equipmentID=" & reader.Item("equipmentID") & "'>" & reader.Item("assetID") & "</a> - " & equipmentname & "<br/>"
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function getDefaultEquipmentID(ByVal companyID As Integer) As String
        getDefaultEquipmentID = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select equipmentID,assetID from v_serviceprofile where companyID=@companyID group by equipmentID,assetID order by assetID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            getDefaultEquipmentID = reader.Item("equipmentID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function getAsset(ByVal serviceprofileID As Integer) As String
        getAsset = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_serviceprofile where serviceprofileID=@serviceprofileID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            getAsset &= reader.Item("assetID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function getAssetID(ByVal serviceprofileID As String) As String
        getAssetID = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_serviceprofile where serviceprofileID=@serviceprofileID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            getAssetID = reader.Item("assetID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOtherAssets(ByVal kitcode As String, ByVal companyID As Integer, ByVal assetID As String) As String
        GetOtherAssets = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_serviceprofile where kitcode=@kitcode and companyID=@companyID and assetID<>@assetID order by assetID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@kitcode", kitcode)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@assetID", assetID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetOtherAssets &= reader.Item("assetID") & "<br/>"
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetKitID(ByVal kitcode As String) As String
        Dim kitID As String = kitcode.GetHashCode()
        GetKitID = Replace(kitID, "-", "N")
    End Function

    Public Shared Function GetPopCode(ByVal manufacturer As String, ByVal partnumber As String) As String
        GetPopCode = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetPopCode = reader.Item("pop_code").ToString
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetPackage(ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetPackage = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetPackage = reader.Item("package")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetHoursMiles(ByVal equipmentID As Integer) As Double
        GetHoursMiles = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_equipment where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetHoursMiles = reader.Item("hours_miles")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetLastKitHoursMiles(ByVal equipmentID As Integer) As Double
        GetLastKitHoursMiles = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select hours_miles,order_date from v_kitorders where equipmentID=@equipmentID and complete=@complete group by hours_miles,order_date order by order_date desc"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.Parameters.AddWithValue("@complete", True)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetLastKitHoursMiles = reader.Item("hours_miles")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetLastServiceHoursMiles(ByVal equipmentID As Integer) As Double
        GetLastServiceHoursMiles = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_service where equipmentID=@equipmentID and complete=@complete and service_type=@service_type order by hours_miles desc"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.Parameters.AddWithValue("@complete", True)
        comm.Parameters.AddWithValue("@service_type", "Lube")
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetLastServiceHoursMiles = reader.Item("hours_miles")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetUninvoicedShipments() As Integer
        GetUninvoicedShipments = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT count(shipmentID) as shipments FROM [v_shipments] WHERE shipped=@shipped and invoiced=@invoiced and qbo=@qbo"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipped", True)
        comm.Parameters.AddWithValue("@invoiced", True)
        comm.Parameters.AddWithValue("@qbo", False)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetUninvoicedShipments = reader.Item("shipments")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetCustomerOpen(ByVal manufacturer As String, ByVal partnumber As String, quantity As Double) As Double
        GetCustomerOpen = 0
        Dim onhand As Double = GetOnHand(manufacturer, partnumber)
        Dim onpo As Double = GetOnPO(manufacturer, partnumber)
        Dim open = quantity - (onhand + onpo)
        If open < 0 Then
            open = 0
        End If
        GetCustomerOpen = open
    End Function

    Public Shared Function GetOnPO(ByVal manufacturer As String, ByVal partnumber As String) As Integer
        GetOnPO = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select poID,sum(quantity) as quantity from v_poline where manufacturer=@manufacturer and partnumber=@partnumber and complete=@complete GROUP BY poID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@complete", False)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If reader.Item("quantity").ToString <> "" Then
                GetOnPO += reader.Item("quantity")

            End If
            'check to see how many received on this po
            Dim received As Double = GetReceivedPOLineQuantity(reader.Item("poID"), manufacturer, partnumber)
            GetOnPO -= received
        End While
        If reader.Read Then
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function CalculateOrderQuantity(ByVal manufacturer As String, ByVal partnumber As String, ByVal min As Double, ByVal max As Double, ByVal onhand As Double) As Double
        CalculateOrderQuantity = 0
        Dim OnPO As Double = GetOnPOAmount(manufacturer, partnumber)
        Dim pkg As Double = GetPackage(manufacturer, partnumber)
        Dim onorder As Double = GetOnOrderAmount(manufacturer, partnumber)
        Dim total As Double = onhand + OnPO - onorder
        If total <= min Then
            CalculateOrderQuantity = max - total
        End If
        If CalculateOrderQuantity <> pkg And pkg <> 1 Then
            'CalculateOrderQuantity = ((CalculateOrderQuantity \ pkg) + 1) * pkg
        End If
    End Function

    Public Shared Function GetOnOrderAmount(ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetOnOrderAmount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select sum(quantity-ship_qty) as onorder from v_shipment_lines where manufacturer=@manufacturer and partnumber=@partnumber and complete=@complete"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@complete", False)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("onorder").ToString <> "" Then
                GetOnOrderAmount = reader.Item("onorder")
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOnPOAmount(ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetOnPOAmount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select sum(quantity-received) as onpo from v_poline where manufacturer=@manufacturer and partnumber=@partnumber and complete=@complete"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@complete", False)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("onpo").ToString <> "" Then
                GetOnPOAmount = reader.Item("onpo")
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetLastUpdated(ByVal manufacturer As String, ByVal partnumber As String) As String
        GetLastUpdated = "1/1/1900"
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select last_updated from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("last_updated").ToString <> "" Then
                GetLastUpdated = FormatDateTime(reader.Item("last_updated"), DateFormat.ShortDate).ToString
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetCustomerRequirement(ByVal manufacturer As String, ByVal partnumber As String, ByVal quantity As Double) As Double
        GetCustomerRequirement = 0
        Dim onhand As Double = GetOnHand(manufacturer, partnumber)
        If onhand < 0 Then
            onhand = 0
        End If
        If onhand < quantity Then
            GetCustomerRequirement = quantity - onhand
        End If
    End Function

    Public Shared Function GetOnHand(ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetOnHand = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select onhand from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetOnHand = reader.Item("onhand")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetLineOrderID(ByVal lineID As Integer) As Integer
        GetLineOrderID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order_line where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetLineOrderID = reader.Item("orderID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetLocation(ByVal locationID As Integer) As String
        GetLocation = "Unknown"
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_ship where shipID=@shipID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipID", locationID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetLocation = reader.Item("shipto")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetLocationID(ByVal companyID As Integer, ByVal shipto As String) As Integer
        GetLocationID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_ship where companyID=@companyID and shipto=@shipto"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@shipto", shipto)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetLocationID = reader.Item("shipID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetMasterPage(ByVal companyID As Integer) As String
        If companyID <> 0 Then
            If isCustomer(companyID) = True Then
                GetMasterPage = "Customer.master"
            Else
                GetMasterPage = "Vendor.master"
            End If
        Else
            GetMasterPage = "Anonymous.master"
        End If
    End Function

    Public Shared Function GetMSRP(ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetMSRP = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select msrp from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetMSRP = reader.Item("msrp")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOrderUserID(ByVal orderID As Integer) As Integer
        GetOrderUserID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetOrderUserID = reader.Item("userID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOrderCompanyID(ByVal orderID As Integer) As Integer
        GetOrderCompanyID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetOrderCompanyID = reader.Item("companyID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOrderManufacturer(ByVal lineID As Integer) As String
        GetOrderManufacturer = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order_line where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetOrderManufacturer = reader.Item("manufacturer")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOrderPartNumber(ByVal lineID As Integer) As String
        GetOrderPartNumber = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order_line where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetOrderPartNumber = reader.Item("partnumber")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOrderVendorID(ByVal orderID As Integer) As Integer
        GetOrderVendorID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetOrderVendorID = reader.Item("vendorID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOpenOrderSubTotal(ByVal orderID As Integer) As Double
        GetOpenOrderSubTotal = 0
        Dim shipqty As Double = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order_line where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            shipqty = GetShipQty(reader.Item("lineID"))
            GetOpenOrderSubTotal += (reader.Item("quantity") - shipqty) * reader.Item("price")
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOrderSubTotal(ByVal orderID As Integer) As Double
        GetOrderSubTotal = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order_line where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetOrderSubTotal += reader.Item("quantity") * reader.Item("price")
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOrderTotal(ByVal orderID As Integer, ByVal companyID As Integer, ByVal vendorID As Integer) As Double
        GetOrderTotal = 0
        Dim tax As Double
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order_line where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetOrderTotal += reader.Item("quantity") * reader.Item("price")
        End While
        tax = GetSalesTax(companyID, vendorID) * GetOrderTotal
        GetOrderTotal += tax
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOrderDate(ByVal orderID As Integer) As String
        GetOrderDate = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetOrderDate = reader.Item("order_date")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetPickQty(ByVal lineID As Integer) As Double
        GetPickQty = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select lineID,sum(ship_qty) as ship_qty from v_shipment_lines where lineID=@lineID group by lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        comm.Parameters.AddWithValue("@shipped", True)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetPickQty = reader.Item("ship_qty")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetPO(ByVal manufacturer As String, ByVal partnumber As String) As Integer
        GetPO = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_poline where manufacturer=@manufacturer and partnumber=@partnumber and complete=@complete"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@complete", False)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetPO = reader.Item("poID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetUOM(ByVal manufacturer As String, ByVal partnumber As String) As String
        GetUOM = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetUOM = reader.Item("uom")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetPOID(ByVal lineID As Integer) As Integer
        GetPOID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_poline where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetPOID = reader.Item("poID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetPOLineQuantity(ByVal poID As Integer, ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetPOLineQuantity = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_poline where poID=@poID and manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@poID", poID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetPOLineQuantity = reader.Item("quantity")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetEquipmentIDFromKitID(ByVal serviceprofileID As Integer) As Integer
        GetEquipmentIDFromKitID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT equipmentID FROM [t_serviceprofile] WHERE ([serviceprofileID] = @serviceprofileID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetEquipmentIDFromKitID = reader.Item("equipmentID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetLastKitName(ByVal equipmentID As Integer) As String
        GetLastKitName = "None"
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT name,order_date FROM [v_kitorders] WHERE ([equipmentID] = @equipmentID) group by name,order_date order by order_date desc"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetLastKitName = reader.Item("name")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetLastKitInterval(ByVal equipmentID As Integer) As String
        GetLastKitInterval = "None"
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT interval,interval_type,order_date FROM [v_kitorders] WHERE ([equipmentID] = @equipmentID) group by interval,interval_type,order_date order by order_date desc"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetLastKitInterval = reader.Item("interval") & " " & reader.Item("interval_type")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetLastKitDate(ByVal equipmentID As Integer) As String
        GetLastKitDate = "None"
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT order_date FROM [v_kitorders] WHERE ([equipmentID] = @equipmentID) group by order_date order by order_date desc"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetLastKitDate = FormatDateTime(reader.Item("order_date"), DateFormat.ShortDate)
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetTotalKitsSoldYTD(ByVal sale_year As Integer) As Integer
        GetTotalKitsSoldYTD = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT count(orderID) as kitcount FROM [t_order] WHERE ([serviceprofileID] <> @serviceprofileID) and YEAR(order_date)=@sale_year"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@sale_year", sale_year)
        comm.Parameters.AddWithValue("@serviceprofileID", 0)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetTotalKitsSoldYTD = reader.Item("kitcount")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetNumKits(ByVal equipmentID As Integer) As Integer
        GetNumKits = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT orderID FROM [v_kitorders] WHERE ([equipmentID] = @equipmentID) group by orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetNumKits += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetPriPartNumber(ByVal manufacturer2 As String, ByVal partnumber2 As String) As String
        GetPriPartNumber = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select manufacturer1,partnumber1 from t_product_xref where  (partnumber2 = @partnumber2) AND (UPPER(manufacturer2) = UPPER(@manufacturer2))"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@partnumber2", partnumber2)
        comm.Parameters.AddWithValue("@manufacturer2", manufacturer2)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetPriPartNumber = reader.Item("manufacturer1") & " " & reader.Item("partnumber1")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetProductCost(ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetProductCost = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select cost from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetProductCost = reader.Item("cost")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetProductMSRP(ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetProductMSRP = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select msrp from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetProductMSRP = reader.Item("msrp")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetProductPackage(ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetProductPackage = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select package from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetProductPackage = reader.Item("package")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetPSSR(ByVal companyID As Integer) As String
        GetPSSR = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_user_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetPSSR = GetUserName(reader.Item("userID"))
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetRepID(ByVal companyID As Integer) As Integer
        GetRepID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select repID from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetRepID = reader.Item("repID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetCategoryAndParentID(ByVal categoryID As Integer) As String
        GetCategoryAndParentID = "None"
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_category where categoryID=@categoryID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@categoryID", categoryID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetCategoryAndParentID = reader.Item("category") & "(" & reader.Item("parentID") & ")"
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetPSSRID(ByVal companyID As Integer) As Integer
        GetPSSRID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_user_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetPSSRID = reader.Item("userID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetQuoteSubTotal(ByVal quoteID As Integer) As Double
        GetQuoteSubTotal = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_quote_line where quoteID=@quoteID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@quoteID", quoteID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetQuoteSubTotal += reader.Item("quantity") * reader.Item("price")
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetReceivedPOLineQuantity(ByVal poID As Integer, ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetReceivedPOLineQuantity = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select sum(received) as quantity from t_receipt where poID=@poID and manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@poID", poID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("quantity").ToString <> "" Then
                GetReceivedPOLineQuantity = reader.Item("quantity")
            Else
                GetReceivedPOLineQuantity = 0
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetRequisitionQuantity(ByVal lineID As Integer) As Integer
        GetRequisitionQuantity = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select quantity from t_requisition where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetRequisitionQuantity = reader.Item("quantity")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetCompanySalesQtyYTD(ByVal companyID As Integer, ByVal manufacturer As String, ByVal partnumber As String, ByVal salesyear As String) As Double
        GetCompanySalesQtyYTD = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select sum(quantity) as quantity from v_order_lines where companyID=@companyID and manufacturer=@manufacturer and partnumber=@partnumber and YEAR(order_date)=@salesyear"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@salesyear", salesyear)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("quantity").ToString <> "" Then
                GetCompanySalesQtyYTD = reader.Item("quantity")
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetSalesQtyYTD(ByVal manufacturer As String, ByVal partnumber As String, ByVal salesyear As String) As Double
        GetSalesQtyYTD = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select sum(quantity) as quantity from v_order_lines where manufacturer=@manufacturer and partnumber=@partnumber and YEAR(order_date)=@salesyear"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@salesyear", salesyear)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("quantity").ToString <> "" Then
                GetSalesQtyYTD = reader.Item("quantity")
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetShipments(ByVal companyID As Integer, ByVal omonth As Integer, ByVal oyear As Integer) As Double
        GetShipments = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            If omonth <> 0 Then
                commandString = "select sum(quantity * price) as shipments from v_shipment_lines where companyID=@companyID and MONTH(shipment_date)=@omonth and YEAR(shipment_date)=@oyear"
            Else
                commandString = "select sum(quantity * price) as shipments from v_shipment_lines where companyID=@companyID and YEAR(shipment_date)=@oyear"
            End If
        Else
            If omonth <> 0 Then
                commandString = "select sum(quantity * price) as shipments from v_shipment_lines where MONTH(shipment_date)=@omonth and YEAR(shipment_date)=@oyear"
            Else
                commandString = "select sum(quantity * price) as shipments from v_shipment_lines where YEAR(shipment_date)=@oyear"
            End If
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
            If omonth <> 0 Then
                comm.Parameters.AddWithValue("@omonth", omonth)
            End If
        Else
            If omonth <> 0 Then
                comm.Parameters.AddWithValue("@omonth", omonth)
            End If
        End If
        comm.Parameters.AddWithValue("@oyear", oyear)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("shipments").ToString <> "" Then
                GetShipments = reader.Item("shipments")
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetPurchases(ByVal omonth As Integer, ByVal oyear As Integer) As Double
        GetPurchases = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select sum(quantity * cost) as purchases from v_poline where MONTH(date_submitted)=@omonth and YEAR(date_submitted)=@oyear"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@oyear", oyear)
        comm.Parameters.AddWithValue("@omonth", omonth)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("purchases").ToString <> "" Then
                GetPurchases = reader.Item("purchases")
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOrdersMTD(ByVal companyID As Integer, ByVal salesmonth As Integer, ByVal salesyear As Integer) As Double
        GetOrdersMTD = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "select sum(quantity * price) as sales from v_order_lines where companyID=@companyID and MONTH(order_date)=@salesmonth and YEAR(order_date)=@salesyear AND isReturn='No'"
        Else
            commandString = "select sum(quantity * price) as sales from v_order_lines where MONTH(order_date)=@salesmonth and YEAR(order_date)=@salesyear AND isReturn='No'"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        comm.Parameters.AddWithValue("@salesyear", salesyear)
        comm.Parameters.AddWithValue("@salesmonth", salesmonth)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("sales").ToString <> "" Then
                GetOrdersMTD = reader.Item("sales")
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetSalesMTD(ByVal companyID As Integer, ByVal salesmonth As Integer, ByVal salesyear As Integer) As Double
        GetSalesMTD = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "select sum(ship_qty * price) as sales from v_order_lines where companyID=@companyID and MONTH(shipment_date)=@salesmonth and YEAR(shipment_date)=@salesyear AND isReturn='No'"
        Else
            commandString = "select sum(ship_qty * price) as sales from v_order_lines where MONTH(shipment_date)=@salesmonth and YEAR(shipment_date)=@salesyear AND isReturn='No'"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        comm.Parameters.AddWithValue("@salesyear", salesyear)
        comm.Parameters.AddWithValue("@salesmonth", salesmonth)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("sales").ToString <> "" Then
                GetSalesMTD = reader.Item("sales")
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetPOCount(ByVal omonth As Integer, ByVal oyear As Integer) As Double
        GetPOCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If omonth <> 0 Then
            commandString = "select poID from t_po where MONTH(date_submitted)=@omonth and YEAR(date_submitted)=@oyear"
        Else
            commandString = "select poID from t_po where YEAR(date_submitted)=@oyear"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If omonth <> 0 Then
            comm.Parameters.AddWithValue("@omonth", omonth)
        End If
        comm.Parameters.AddWithValue("@oyear", oyear)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetPOCount += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetPendingPOCount() As Double
        GetPendingPOCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select poID from t_po where submitted='False'"
        Dim comm As New SqlCommand(commandString, conn)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetPendingPOCount += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOrderCountYTD(ByVal companyID As Integer, ByVal salesyear As Integer) As Double
        GetOrderCountYTD = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "select orderID from t_order where companyID=@companyID and YEAR(order_date)=@salesyear"
        Else
            commandString = "select orderID from t_order where YEAR(order_date)=@salesyear"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        comm.Parameters.AddWithValue("@salesyear", salesyear)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetOrderCountYTD += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetShipmentCountMTD(ByVal companyID As Integer, ByVal salesmonth As Integer, ByVal salesyear As Integer) As Double
        GetShipmentCountMTD = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "select shipmentID from t_shipment where companyID=@companyID and MONTH(shipment_date)=@salesmonth and YEAR(shipment_date)=@salesyear"
        Else
            commandString = "select shipmentID from t_shipment where MONTH(shipment_date)=@salesmonth and YEAR(shipment_date)=@salesyear"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        comm.Parameters.AddWithValue("@salesmonth", salesmonth)
        comm.Parameters.AddWithValue("@salesyear", salesyear)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetShipmentCountMTD += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOrderCountMTD(ByVal companyID As Integer, ByVal salesmonth As Integer, ByVal salesyear As Integer) As Double
        GetOrderCountMTD = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "select orderID from t_order where companyID=@companyID and MONTH(order_date)=@salesmonth and YEAR(order_date)=@salesyear"
        Else
            commandString = "select orderID from t_order where MONTH(order_date)=@salesmonth and YEAR(order_date)=@salesyear"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        comm.Parameters.AddWithValue("@salesmonth", salesmonth)
        comm.Parameters.AddWithValue("@salesyear", salesyear)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetOrderCountMTD += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetRepSalesYTD(ByVal repID As Integer, ByVal salesyear As Integer) As Double
        GetRepSalesYTD = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select sum(quantity * price) as sales from v_order_lines where repID=@repID and YEAR(order_date)=@salesyear"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@repID", repID)
        comm.Parameters.AddWithValue("@salesyear", salesyear)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("sales").ToString <> "" Then
                GetRepSalesYTD = reader.Item("sales")
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetSalesYTD(ByVal companyID As Integer, ByVal salesyear As Integer) As Double
        GetSalesYTD = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "select sum(quantity * price) as sales from v_order_lines where companyID=@companyID and YEAR(order_date)=@salesyear"
        Else
            commandString = "select sum(quantity * price) as sales from v_order_lines where YEAR(order_date)=@salesyear"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        comm.Parameters.AddWithValue("@salesyear", salesyear)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("sales").ToString <> "" Then
                GetSalesYTD = reader.Item("sales")
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetAnnualAverageOrderLines(ByVal companyID As Integer, ByVal oyear As Integer) As Double
        GetAnnualAverageOrderLines = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "select orderID,count(lineID) as linecount from v_order_lines where companyID=@companyID and YEAR(order_date)=@oyear GROUP BY orderID"
        Else
            commandString = "select orderID,count(lineID) as linecount from v_order_lines where YEAR(order_date)=@oyear GROUP BY orderID"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        comm.Parameters.AddWithValue("@oyear", oyear)
        Dim reader As SqlDataReader = comm.ExecuteReader
        Dim totallines As Integer = 0
        Dim ordercount As Integer = 0
        While reader.Read
            totallines += reader.Item("linecount")
            ordercount += 1
        End While
        reader.Close()
        conn.Close()
        If ordercount > 0 Then
            GetAnnualAverageOrderLines = totallines / ordercount
        End If
    End Function

    Public Shared Function GetMonthAverageOrderLines(ByVal companyID As Integer, ByVal omonth As Integer, ByVal oyear As Integer) As Double
        GetMonthAverageOrderLines = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "select orderID,count(lineID) as linecount from v_order_lines where companyID=@companyID and MONTH(order_date)=@omonth and YEAR(order_date)=@oyear GROUP BY orderID"
        Else
            commandString = "select orderID,count(lineID) as linecount from v_order_lines where MONTH(order_date)=@omonth and YEAR(order_date)=@oyear GROUP BY orderID"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        comm.Parameters.AddWithValue("@omonth", omonth)
        comm.Parameters.AddWithValue("@oyear", oyear)
        Dim reader As SqlDataReader = comm.ExecuteReader
        Dim totallines As Integer = 0
        Dim ordercount As Integer = 0
        While reader.Read
            totallines += reader.Item("linecount")
            ordercount += 1
        End While
        reader.Close()
        conn.Close()
        If ordercount > 0 Then
            GetMonthAverageOrderLines = totallines / ordercount
        End If
    End Function

    Public Shared Function GetMonthAverageOrderDollars(ByVal companyID As Integer, ByVal omonth As Integer, ByVal oyear As Integer) As Double
        GetMonthAverageOrderDollars = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "select orderID,sum(quantity * price) as orderdollars from v_order_lines where companyID=@companyID and MONTH(order_date)=@omonth and YEAR(order_date)=@oyear GROUP BY orderID"
        Else
            commandString = "select orderID,sum(quantity * price) as orderdollars from v_order_lines where MONTH(order_date)=@omonth and YEAR(order_date)=@oyear GROUP BY orderID"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        comm.Parameters.AddWithValue("@omonth", omonth)
        comm.Parameters.AddWithValue("@oyear", oyear)
        Dim reader As SqlDataReader = comm.ExecuteReader
        Dim totaldollars As Double = 0
        Dim ordercount As Integer = 0
        While reader.Read
            totaldollars += reader.Item("orderdollars")
            ordercount += 1
        End While
        reader.Close()
        conn.Close()
        If ordercount > 0 Then
            GetMonthAverageOrderDollars = totaldollars / ordercount
        End If
    End Function

    Public Shared Function GetAnnualAverageOrderDollars(ByVal companyID As Integer, ByVal oyear As Integer) As Double
        GetAnnualAverageOrderDollars = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "select orderID,sum(quantity * price) as orderdollars from v_order_lines where companyID=@companyID and YEAR(order_date)=@oyear GROUP BY orderID"
        Else
            commandString = "select orderID,sum(quantity * price) as orderdollars from v_order_lines where YEAR(order_date)=@oyear GROUP BY orderID"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        comm.Parameters.AddWithValue("@oyear", oyear)
        Dim reader As SqlDataReader = comm.ExecuteReader
        Dim totaldollars As Double = 0
        Dim ordercount As Integer = 0
        While reader.Read
            totaldollars += reader.Item("orderdollars")
            ordercount += 1
        End While
        reader.Close()
        conn.Close()
        If ordercount > 0 Then
            GetAnnualAverageOrderDollars = totaldollars / ordercount
        End If
    End Function

    Public Shared Function GetAnnualAverageLineDollars(ByVal companyID As Integer, ByVal oyear As Integer) As Double
        GetAnnualAverageLineDollars = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "select companyID,AVG(quantity * price) as averagedollarsperline from v_order_lines where companyID=@companyID and YEAR(order_date)=@oyear GROUP BY companyID"
        Else
            commandString = "select companyID,AVG(quantity * price) as averagedollarsperline from v_order_lines where YEAR(order_date)=@oyear GROUP BY companyID"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        comm.Parameters.AddWithValue("@oyear", oyear)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetAnnualAverageLineDollars = reader.Item("averagedollarsperline")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetMonthAverageLineDollars(ByVal companyID As Integer, ByVal omonth As Integer, ByVal oyear As Integer) As Double
        GetMonthAverageLineDollars = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "select companyID,AVG(quantity * price) as averagedollarsperline from v_order_lines where companyID=@companyID and MONTH(order_date)=@omonth and YEAR(order_date)=@oyear GROUP BY companyID"
        Else
            commandString = "select companyID,AVG(quantity * price) as averagedollarsperline from v_order_lines where MONTH(order_date)=@omonth and YEAR(order_date)=@oyear GROUP BY companyID"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        comm.Parameters.AddWithValue("@omonth", omonth)
        comm.Parameters.AddWithValue("@oyear", oyear)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetMonthAverageLineDollars = reader.Item("averagedollarsperline")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetSalePrice(ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetSalePrice = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select saleprice from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetSalePrice = reader.Item("saleprice")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetSalesTax(ByVal companyID As Integer, ByVal vendorID As Integer) As Double
        GetSalesTax = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("chargetax") = True Then
                GetSalesTax = reader.Item("salestax")
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetSameAs(ByVal kitcode As String, ByVal companyID As Integer) As String
        GetSameAs = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_serviceprofile where kitcode=@kitcode and companyID=@companyID order by assetID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@kitcode", kitcode)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetSameAs &= "<a href='EditService.aspx?equipmentID=" & reader.Item("equipmentID") & "&serviceprofileID=" & reader.Item("serviceprofileID") & "'>" & reader.Item("assetID") & " - " & reader.Item("interval") & " " & reader.Item("interval_type") & "</a><br/>"
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetServiceAssetID(ByVal serviceID As Integer) As String
        GetServiceAssetID = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_service where serviceID=@serviceID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceID", serviceID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetServiceAssetID = reader.Item("assetID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetServiceProfileAssetID(ByVal serviceprofileID As Integer) As String
        GetServiceProfileAssetID = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_serviceprofile where serviceprofileID=@serviceprofileID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetServiceProfileAssetID = reader.Item("assetID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetIntervalType(ByVal equipmentID As Integer) As String
        GetIntervalType = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_equipment where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetIntervalType = reader.Item("interval_type").ToString
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetServiceIntervalType(ByVal serviceprofileID As Integer) As String
        GetServiceIntervalType = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_serviceprofile where serviceprofileID=@serviceprofileID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetServiceIntervalType = reader.Item("interval_type").ToString
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetServiceName(ByVal serviceprofileID As Integer) As String
        GetServiceName = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_serviceprofile where serviceprofileID=@serviceprofileID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetServiceName = reader.Item("name").ToString
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetServiceProfileID(ByVal companyID As Integer, ByVal userID As Integer) As Integer
        GetServiceProfileID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_cart where companyID=@companyID and userID=@userID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@userID", userID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("kitID") <> 0 And reader.Item("kitID").ToString <> "" Then
                GetServiceProfileID = reader.Item("kitID")
            Else
                GetServiceProfileID = 0
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetShipQty(ByVal lineID As Integer) As Double
        GetShipQty = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select lineID,sum(ship_qty) as ship_qty from v_shipment_lines where lineID=@lineID group by lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetShipQty = reader.Item("ship_qty")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetShipmentOrderID(ByVal shipmentID As Integer) As Double
        GetShipmentOrderID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_shipment where shipmentID=@shipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetShipmentOrderID = reader.Item("orderID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetUserCompanyID(ByVal userID As Integer) As Integer
        GetUserCompanyID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_user where userID=@userID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetUserCompanyID = reader.Item("companyID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetUserEmail(ByVal userID As Integer) As String
        GetUserEmail = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_user where userID=@userID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetUserEmail = reader.Item("email")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetUserName(ByVal userID As Integer) As String
        GetUserName = "Unknown"
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_user where userID=@userID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetUserName = reader.Item("name")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetUserPassword(ByVal userID As Integer) As String
        GetUserPassword = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_user where userID=@userID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetUserPassword = reader.Item("password")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetUserPhone(ByVal userID As Integer) As String
        GetUserPhone = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_user where userID=@userID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetUserPhone = reader.Item("c_phone")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetVendorCost(ByVal companyID As Integer, ByVal vendorID As Integer, ByVal manufacturer As String, ByVal partnumber As String) As Double
        GetVendorCost = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetVendorCost = reader.Item("cost")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetVendorLogo(ByVal companyID As Integer) As String
        GetVendorLogo = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetVendorLogo = reader.Item("logo")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Sub SetSampleFlag(ByVal orderID As Integer, sample As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_order set sample=@sample where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@sample", sample)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub SetSampleCompleteFlag(ByVal orderID As Integer, sample_complete As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_order set sample_complete=@sample_complete where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@sample_complete", sample_complete)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub


    Public Shared Function GetVendorPurchasing(ByVal companyID As Integer) As Boolean
        GetVendorPurchasing = True
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetVendorPurchasing = reader.Item("purchasing")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetVendorWebSite(ByVal companyID As Integer) As String
        GetVendorWebSite = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetVendorWebSite = reader.Item("website")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Sub UpdateActiveStatus(ByVal companyID As Integer, ByVal active As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_company set active=@active WHERE companyID = @companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@active", active)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateShipOptions(ByVal orderID As Integer, ByVal ship_options As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_order set ship_options=@ship_options WHERE orderID = @orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.Parameters.AddWithValue("@ship_options", ship_options)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub SetLubeServiceWeight(ByVal serviceprofileID As Integer, ByVal weight As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_serviceprofile set weight=@weight WHERE serviceprofileID = @serviceprofileID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@weight", weight)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Function GetServiceProfileCount(ByVal equipmentID As Integer) As Integer
        GetServiceProfileCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT count(serviceprofileID) as serviceprofilecount FROM [t_serviceprofile] WHERE ([equipmentID] = @equipmentID) and servicetype='Lube'"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetServiceProfileCount = reader.Item("serviceprofilecount")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Sub MoveLubeServiceWeightUp(ByVal equipmentID As Integer, ByVal currentweight As Integer)
        Dim x As Integer = 1
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT * FROM [t_serviceprofile] WHERE ([equipmentID] = @equipmentID) and servicetype='Lube' ORDER BY weight"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If currentweight <> 1 Then
                If reader.Item("weight") = currentweight - 1 Then
                    SetLubeServiceWeight(reader.Item("serviceprofileID"), reader.Item("weight") + 1)
                End If
                If reader.Item("weight") = currentweight Then
                    SetLubeServiceWeight(reader.Item("serviceprofileID"), reader.Item("weight") - 1)
                End If
            End If
        End While
        reader.Close()
        conn.Close()
    End Sub

    Public Shared Sub MoveLubeServiceWeightDown(ByVal equipmentID As Integer, ByVal currentweight As Integer)
        Dim x As Integer = 1
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT * FROM [t_serviceprofile] WHERE ([equipmentID] = @equipmentID) and servicetype='Lube' ORDER BY weight"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If currentweight <> GetServiceProfileCount(equipmentID) Then
                If reader.Item("weight") = currentweight Then
                    SetLubeServiceWeight(reader.Item("serviceprofileID"), reader.Item("weight") + 1)
                End If
                If reader.Item("weight") = currentweight + 1 Then
                    SetLubeServiceWeight(reader.Item("serviceprofileID"), reader.Item("weight") - 1)
                End If
            End If
        End While
        reader.Close()
        conn.Close()
    End Sub

    Public Shared Function InCatalog(ByVal manufacturer As String, ByVal price_category As String) As Integer
        InCatalog = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT manufacturer,price_category,count(productID) as incatalog FROM [t_product] WHERE manufacturer=@manufacturer and price_category=@price_category and category <> 0 group by manufacturer,price_category"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@price_category", price_category)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            InCatalog = reader.Item("incatalog")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function AddToCart(ByVal productID As Integer, ByVal quantity As Double, ByVal companyID As Integer, ByVal userID As Integer, ByVal vendorID As Integer, ByVal ssID As String) As Integer
        AddToCart = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT * from t_product where productID=@productID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@productID", productID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        'ssID is now kit ID
        If reader.Read Then
            AddToCart = InsertCartItem(companyID, userID, vendorID, productID, reader.Item("manufacturer"), reader.Item("partnumber"), reader.Item("item"), quantity, GetCompanyPrice(companyID, reader.Item("manufacturer"), reader.Item("partnumber")), reader.Item("uom"), reader.Item("weight"), ssID)
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function InsertPONote(ByVal poID As Integer, ByVal date_entered As String, ByVal author As String, ByVal note As String) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_po_notes (poID,date_entered,author,note) values(@poID,@date_entered,@author,@note)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@poID", poID)
        comm.Parameters.AddWithValue("@date_entered", date_entered)
        comm.Parameters.AddWithValue("@author", author)
        comm.Parameters.AddWithValue("@note", note)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertPONote = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertAlert(ByVal equipmentID As Integer, ByVal date_entered As String, ByVal author As String, ByVal note As String) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_alert (equipmentID,date_entered,author,note) values(@equipmentID,@date_entered,@author,@note)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.Parameters.AddWithValue("@date_entered", date_entered)
        comm.Parameters.AddWithValue("@author", author)
        comm.Parameters.AddWithValue("@note", note)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertAlert = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Sub UpdateWorkOrder(ByVal repairID As Integer, ByVal userID As Integer, ByVal date_received As String, ByVal date_return_estimate As String, ByVal date_return_actual As String, ByVal customer_companyID As Integer, ByVal customer_userID As Integer, ByVal manufacturer As String, ByVal model As String, ByVal part_number As String, ByVal serial_number As String, ByVal description As String, ByVal estimate As Double, ByVal supplier_companyID As Integer, ByVal supplier_userID As Integer, ByVal senton As String, ByVal returnedon As String, ByVal hours As Double, ByVal labor As Double, ByVal parts As Double, ByVal total As Double, ByVal work_requested As String, ByVal work_complete As Boolean, ByVal work_performed As String, ByVal work_approved As Boolean, ByVal work_approvedby As String, ByVal purchase_order As String, ByVal complete As Boolean, ByVal orderID As Integer, ByVal invoice_amount As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_repair set userID=@userID,date_received=@date_received,date_return_estimate=@date_return_estimate,date_return_actual=@date_return_actual,customer_companyID=@customer_companyID,customer_userID=@customer_userID,manufacturer=@manufacturer,model=@model,part_number=@part_number,serial_number=@serial_number,description=@description,estimate=@estimate,supplier_companyID=@supplier_companyID,supplier_userID=@supplier_userID,senton=@senton,returnedon=@returnedon,hours=@hours,labor=@labor,parts=@parts,total=@total,work_requested=@work_requested,work_complete=@work_complete,work_performed=@work_performed,work_approved=@work_approved,work_approvedby=@work_approvedby,purchase_order=@purchase_order,complete=@complete,orderID=@orderID,invoice_amount=@invoice_amount where repairID=@repairID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@repairID", repairID)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@date_received", date_received)
        comm.Parameters.AddWithValue("@date_return_estimate", date_return_estimate)
        comm.Parameters.AddWithValue("@date_return_actual", date_return_actual)
        comm.Parameters.AddWithValue("@customer_companyID", customer_companyID)
        comm.Parameters.AddWithValue("@customer_userID", customer_userID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@model", model)
        comm.Parameters.AddWithValue("@part_number", part_number)
        comm.Parameters.AddWithValue("@serial_number", serial_number)
        comm.Parameters.AddWithValue("@description", description)
        comm.Parameters.AddWithValue("@estimate", estimate)
        comm.Parameters.AddWithValue("@supplier_companyID", supplier_companyID)
        comm.Parameters.AddWithValue("@supplier_userID", supplier_userID)
        comm.Parameters.AddWithValue("@senton", senton)
        comm.Parameters.AddWithValue("@returnedon", returnedon)
        comm.Parameters.AddWithValue("@hours", hours)
        comm.Parameters.AddWithValue("@labor", labor)
        comm.Parameters.AddWithValue("@parts", parts)
        comm.Parameters.AddWithValue("@total", total)
        comm.Parameters.AddWithValue("@work_requested", work_requested)
        comm.Parameters.AddWithValue("@work_complete", work_complete)
        comm.Parameters.AddWithValue("@work_performed", work_performed)
        comm.Parameters.AddWithValue("@work_approved", work_approved)
        comm.Parameters.AddWithValue("@work_approvedby", work_approvedby)
        comm.Parameters.AddWithValue("@purchase_order", purchase_order)
        comm.Parameters.AddWithValue("@complete", complete)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.Parameters.AddWithValue("@invoice_amount", invoice_amount)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Function InsertWorkOrder(ByVal userID As Integer, ByVal date_received As String, ByVal date_return_estimate As String, ByVal date_return_actual As String, ByVal customer_companyID As Integer, ByVal customer_userID As Integer, ByVal manufacturer As String, ByVal model As String, ByVal part_number As String, ByVal serial_number As String, ByVal description As String, ByVal estimate As Double, ByVal supplier_companyID As Integer, ByVal supplier_userID As Integer, ByVal senton As String, ByVal returnedon As String, ByVal hours As Double, ByVal labor As Double, ByVal parts As Double, ByVal total As Double, ByVal work_requested As String, ByVal work_complete As Boolean, ByVal work_performed As String, ByVal work_approved As Boolean, ByVal work_approvedby As String, ByVal purchase_order As String, ByVal complete As Boolean, ByVal orderID As Integer, ByVal invoice_amount As Double) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_repair (userID,date_received,date_return_estimate,date_return_actual,customer_companyID,customer_userID,manufacturer,model,part_number,serial_number,description,estimate,supplier_companyID,supplier_userID,senton,returnedon,hours,labor,parts,total,work_requested,work_complete,work_performed,work_approved,work_approvedby,purchase_order,complete,orderID,invoice_amount) values(@userID,@date_received,@date_return_estimate,@date_return_actual,@customer_companyID,@customer_userID,@manufacturer,@model,@part_number,@serial_number,@description,@estimate,@supplier_companyID,@supplier_userID,@senton,@returnedon,@hours,@labor,@parts,@total,@work_requested,@work_complete,@work_performed,@work_approved,@work_approvedby,@purchase_order,@complete,@orderID,@invoice_amount)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@date_received", date_received)
        comm.Parameters.AddWithValue("@date_return_estimate", date_return_estimate)
        comm.Parameters.AddWithValue("@date_return_actual", date_return_actual)
        comm.Parameters.AddWithValue("@customer_companyID", customer_companyID)
        comm.Parameters.AddWithValue("@customer_userID", customer_userID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@model", model)
        comm.Parameters.AddWithValue("@part_number", part_number)
        comm.Parameters.AddWithValue("@serial_number", serial_number)
        comm.Parameters.AddWithValue("@description", description)
        comm.Parameters.AddWithValue("@estimate", estimate)
        comm.Parameters.AddWithValue("@supplier_companyID", supplier_companyID)
        comm.Parameters.AddWithValue("@supplier_userID", supplier_userID)
        comm.Parameters.AddWithValue("@senton", senton)
        comm.Parameters.AddWithValue("@returnedon", returnedon)
        comm.Parameters.AddWithValue("@hours", hours)
        comm.Parameters.AddWithValue("@labor", labor)
        comm.Parameters.AddWithValue("@parts", parts)
        comm.Parameters.AddWithValue("@total", total)
        comm.Parameters.AddWithValue("@work_requested", work_requested)
        comm.Parameters.AddWithValue("@work_complete", work_complete)
        comm.Parameters.AddWithValue("@work_performed", work_performed)
        comm.Parameters.AddWithValue("@work_approved", work_approved)
        comm.Parameters.AddWithValue("@work_approvedby", work_approvedby)
        comm.Parameters.AddWithValue("@purchase_order", purchase_order)
        comm.Parameters.AddWithValue("@complete", complete)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.Parameters.AddWithValue("@invoice_amount", invoice_amount)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertWorkOrder = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Sub DeleteWorkOrder(ByVal repairID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_repair where repairID=@repairID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@repairID", repairID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteAlert(ByVal alertID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_alert where alertID=@alertID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@alertID", alertID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeletePONote(ByVal noteID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_po_notes where noteID=@noteID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@noteID", noteID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Function InsertError(ByVal userID As Integer, ByVal error_msg As String, ByVal error_page As String) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_error (userID,error_msg,error_page) values(@userID,@error_msg,@error_page)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@error_page", error_page)
        comm.Parameters.AddWithValue("@error_msg", error_msg)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertError = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertCategory(ByVal category As String, ByVal parentID As Integer) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_category (category,parentID) values(@category,@parentID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@category", category)
        comm.Parameters.AddWithValue("@parentID", parentID)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertCategory = comm.ExecuteScalar()
        conn.Close()
    End Function


    Public Shared Sub DeleteCategory(ByVal categoryID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_category where categoryID=@categoryID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@categoryID", categoryID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Function InsertCartItem(ByVal companyID As Integer, ByVal userID As Integer, ByVal vendorID As Integer, ByVal productID As Integer, ByVal manufacturer As String, ByVal partnumber As String, ByVal item As String, ByVal quantity As Double, ByVal price As Double, ByVal uom As String, ByVal weight As Double, ByVal kitID As String) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_cart (companyID,userID,vendorID,productID,manufacturer,partnumber,item,quantity,price,uom,weight,kitID) values(@companyID,@userID,@vendorID,@productID,@manufacturer,@partnumber,@item,@quantity,@price,@uom,@weight,@kitID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@vendorID", vendorID)
        comm.Parameters.AddWithValue("@productID", productID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@item", item)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.Parameters.AddWithValue("@price", price)
        comm.Parameters.AddWithValue("@uom", uom)
        comm.Parameters.AddWithValue("@weight", weight)
        comm.Parameters.AddWithValue("@kitID", kitID)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertCartItem = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertCompany(ByVal company As String, ByVal address1 As String, ByVal address2 As String, ByVal city As String, ByVal state As String, ByVal zipcode As String, ByVal c_phone As String, ByVal c_fax As String, ByVal customer As Boolean, ByVal logo As String, ByVal vendorID As Integer, ByVal branchID As Integer) As Integer
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "insert into t_company (company,address1,address2,city,state,zipcode,c_phone,c_fax,customer,logo,vendorID,branchID) values(@company,@address1,@address2,@city,@state,@zipcode,@c_phone,@c_fax,@customer,@logo,@vendorID,@branchID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@company", company)
        comm.Parameters.AddWithValue("@address1", address1)
        comm.Parameters.AddWithValue("@address2", address2)
        comm.Parameters.AddWithValue("@city", city)
        comm.Parameters.AddWithValue("@state", state)
        comm.Parameters.AddWithValue("@zipcode", zipcode)
        comm.Parameters.AddWithValue("@c_phone", c_phone)
        comm.Parameters.AddWithValue("@c_fax", c_fax)
        comm.Parameters.AddWithValue("@customer", customer)
        comm.Parameters.AddWithValue("@logo", logo)
        comm.Parameters.AddWithValue("@vendorID", vendorID)
        comm.Parameters.AddWithValue("@branchID", branchID)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertCompany = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertEquipment(ByVal companyID As Integer, ByVal assetID As String, ByVal equipment_oem As String, ByVal equipment_model As String, ByVal equipment_description As String, ByVal equipment_options As String, ByVal equipment_year As String, ByVal equipment_vin As String, ByVal engine_oem As String, ByVal engine_model As String, ByVal notes As String, ByVal verified As Boolean, ByVal locationID As Integer, ByVal hours_miles As String, ByVal interval_type As String, ByVal fuel_type As String, ByVal def As String, ByVal interval_root As Double) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_equipment (companyID,assetID,equipment_oem,equipment_model,equipment_description,equipment_options,equipment_year,equipment_vin,engine_oem,engine_model,notes,verified,locationID,hours_miles,interval_type,fuel_type,def,interval_root) values(@companyID,@assetID,@equipment_oem,@equipment_model,@equipment_description,@equipment_options,@equipment_year,@equipment_vin,@engine_oem,@engine_model,@notes,@verified,@locationID,@hours_miles,@interval_type,@fuel_type,@def,@interval_root)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@assetID", assetID)
        comm.Parameters.AddWithValue("@equipment_oem", equipment_oem.ToUpper)
        comm.Parameters.AddWithValue("@equipment_model", equipment_model.ToUpper)
        comm.Parameters.AddWithValue("@equipment_description", equipment_description.ToUpper)
        comm.Parameters.AddWithValue("@equipment_options", equipment_options.ToUpper)
        comm.Parameters.AddWithValue("@equipment_year", equipment_year)
        comm.Parameters.AddWithValue("@equipment_vin", equipment_vin)
        comm.Parameters.AddWithValue("@engine_oem", engine_oem.ToUpper)
        comm.Parameters.AddWithValue("@engine_model", engine_model.ToUpper)
        comm.Parameters.AddWithValue("@notes", notes)
        comm.Parameters.AddWithValue("@verified", verified)
        comm.Parameters.AddWithValue("@locationID", locationID)
        comm.Parameters.AddWithValue("@hours_miles", hours_miles)
        comm.Parameters.AddWithValue("@interval_type", interval_type)
        comm.Parameters.AddWithValue("@fuel_type", fuel_type)
        comm.Parameters.AddWithValue("@def", def)
        comm.Parameters.AddWithValue("@interval_root", interval_root)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertEquipment = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertFlyer(ByVal title As String, ByVal header As String, ByVal message As String, ByVal html As String, ByVal ad1 As Integer, ByVal ad2 As Integer, ByVal ad3 As Integer, ByVal ad4 As Integer, ByVal ad5 As Integer, ByVal ad6 As Integer, ByVal ad7 As Integer, ByVal ad8 As Integer, ByVal pdf_attachment As String) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_flyer (title,header,message,html,ad1,ad2,ad3,ad4,ad5,ad6,ad7,ad8,pdf_attachment) values(@title,@header,@message,@html,@ad1,@ad2,@ad3,@ad4,@ad5,@ad6,@ad7,@ad8,@pdf_attachment)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@header", header)
        comm.Parameters.AddWithValue("@message", message)
        comm.Parameters.AddWithValue("@title", title)
        comm.Parameters.AddWithValue("@html", html)
        comm.Parameters.AddWithValue("@ad1", ad1)
        comm.Parameters.AddWithValue("@ad2", ad2)
        comm.Parameters.AddWithValue("@ad3", ad3)
        comm.Parameters.AddWithValue("@ad4", ad4)
        comm.Parameters.AddWithValue("@ad5", ad5)
        comm.Parameters.AddWithValue("@ad6", ad6)
        comm.Parameters.AddWithValue("@ad7", ad7)
        comm.Parameters.AddWithValue("@ad8", ad8)
        comm.Parameters.AddWithValue("@pdf_attachment", pdf_attachment)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertFlyer = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Sub UpdateFlyer(ByVal flyerID As Integer, ByVal title As String, ByVal header As String, ByVal message As String, ByVal html As String, ByVal ad1 As Integer, ByVal ad2 As Integer, ByVal ad3 As Integer, ByVal ad4 As Integer, ByVal ad5 As Integer, ByVal ad6 As Integer, ByVal ad7 As Integer, ByVal ad8 As Integer, ByVal pdf_attachment As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_flyer set title=@title,header=@header,message=@message,html=@html,ad1=@ad1,ad2=@ad2,ad3=@ad3,ad4=@ad4,ad5=@ad5,ad6=@ad6,ad7=@ad7,ad8=@ad8,pdf_attachment=@pdf_attachment where flyerID=@flyerID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@flyerID", flyerID)
        comm.Parameters.AddWithValue("@title", title)
        comm.Parameters.AddWithValue("@header", header)
        comm.Parameters.AddWithValue("@message", message)
        comm.Parameters.AddWithValue("@html", html)
        comm.Parameters.AddWithValue("@ad1", ad1)
        comm.Parameters.AddWithValue("@ad2", ad2)
        comm.Parameters.AddWithValue("@ad3", ad3)
        comm.Parameters.AddWithValue("@ad4", ad4)
        comm.Parameters.AddWithValue("@ad5", ad5)
        comm.Parameters.AddWithValue("@ad6", ad6)
        comm.Parameters.AddWithValue("@ad7", ad7)
        comm.Parameters.AddWithValue("@ad8", ad8)
        comm.Parameters.AddWithValue("@pdf_attachment", pdf_attachment)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteFlyer(ByVal flyerID As Integer)
        DeleteAds(flyerID)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_flyer where flyerID=@flyerID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@flyerID", flyerID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteAds(ByVal flyerID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_flyer where flyerID=@flyerID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@flyerID", flyerID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            DeleteAd(reader.Item("ad1"))
            DeleteAd(reader.Item("ad2"))
            DeleteAd(reader.Item("ad3"))
            DeleteAd(reader.Item("ad4"))
            DeleteAd(reader.Item("ad5"))
            DeleteAd(reader.Item("ad6"))
            DeleteAd(reader.Item("ad7"))
            DeleteAd(reader.Item("ad8"))
        End If
        reader.Close()
        conn.Close()
    End Sub

    Public Shared Function IsPOLineComplete(ByVal quantity As Double, ByVal received As Double) As Boolean
        IsPOLineComplete = False
        If received >= quantity Then
            IsPOLineComplete = True
        End If
    End Function

    Public Shared Function IsPOBO(ByVal poID As Integer) As Boolean
        IsPOBO = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_poline where poID=@poID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@poID", poID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If reader.Item("received").ToString <> "" Then
                If reader.Item("received") > 0 Then
                    IsPOBO = True
                End If
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsOrderBO(ByVal orderID As Integer) As Boolean
        IsOrderBO = False
        Dim boqty As Double
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select orderID,quantity,sum(ship_qty) as ship_qty from v_order_lines where orderID=@orderID group by orderID,quantity"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If reader.Item("ship_qty").ToString <> "" Then
                boqty = reader.Item("quantity") - reader.Item("ship_qty")
                If boqty > 0 Then
                    IsOrderBO = True
                End If
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsBO(ByVal orderID As Integer) As Boolean
        IsBO = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_order_lines where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If reader.Item("ship_qty").ToString <> "" Then
                If reader.Item("ship_qty") > 0 Then
                    IsBO = True
                End If
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsOrderLate(ByVal orderID As Integer) As Boolean
        IsOrderLate = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If FormatDateTime(Now(), DateFormat.ShortDate) >= FormatDateTime(reader.Item("deliverby_date"), DateFormat.ShortDate) Then
                IsOrderLate = True
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsPOLate(ByVal poID As Integer) As Boolean
        IsPOLate = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_po where poID=@poID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@poID", poID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("estimated_arrival").ToString <> "" Then
                If FormatDateTime(Now(), DateFormat.ShortDate) >= FormatDateTime(reader.Item("estimated_arrival"), DateFormat.ShortDate) Then
                    IsPOLate = True
                End If
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetWorklineID(ByVal serviceprofileID As Integer, ByVal descriptionID As Integer) As Integer
        GetWorklineID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_workline where serviceprofileID=@serviceprofileID and descriptionID=@descriptionID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.Parameters.AddWithValue("@descriptionID", descriptionID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetWorklineID = reader.Item("worklineID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsTaskSelected(ByVal serviceprofileID As Integer, ByVal descriptionID As Integer) As Boolean
        IsTaskSelected = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_workline where serviceprofileID=@serviceprofileID and descriptionID=@descriptionID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.Parameters.AddWithValue("@descriptionID", descriptionID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsTaskSelected = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsKitItemSelected(ByVal serviceprofileID As Integer, ByVal manufacturer As String, ByVal partnumber As String) As Boolean
        IsKitItemSelected = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_kitparts where serviceprofileID=@serviceprofileID and manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("selected") = True Then
                IsKitItemSelected = True
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsKitOrderItem(ByVal lineID As Integer) As Boolean
        IsKitOrderItem = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order_line where lineID=@lineID and kitID<>'0'"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsKitOrderItem = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetDefaultCompanyID() As Integer
        GetDefaultCompanyID = 14
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_company order by company"
        Dim comm As New SqlCommand(commandString, conn)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetDefaultCompanyID = reader.Item("companyID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetKitIDInCart(ByVal companyID As Integer, ByVal userID As Integer) As Integer
        GetKitIDInCart = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_cart where companyID=@companyID and userID=@userID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@userID", userID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If reader.Item("kitID") <> "0" Then
                GetKitIDInCart = reader.Item("kitID")
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetRepDefaultCompanyID(ByVal userID As Integer) As Integer
        GetRepDefaultCompanyID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_company where repID=@repID order by company"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@repID", userID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetRepDefaultCompanyID = reader.Item("companyID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOrderItemKitID(ByVal lineID As Integer) As String
        GetOrderItemKitID = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order_line where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("kitID").ToString <> "0" Then
                GetOrderItemKitID = " (Kit " & reader.Item("kitID") & ")"
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetItemKitID(ByVal cartID As Integer) As String
        GetItemKitID = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_cart where cartID=@cartID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@cartID", cartID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("kitID").ToString <> "0" Then
                GetItemKitID = " (Kit " & reader.Item("kitID") & ")"
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOrderKits(ByVal orderID As Integer) As String
        GetOrderKits = ""
        Dim LastKit As String = ""
        Dim x As Integer = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order_line where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If reader.Item("kitID") <> LastKit Then
                If x > 0 Then
                    GetOrderKits = "," & reader.Item("kitID")
                Else
                    GetOrderKits = reader.Item("kitID")
                End If
            End If
            LastKit = reader.Item("kitID")
            x += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetServiceProfileIDFromOrderID(ByVal orderID As Integer) As Integer
        GetServiceProfileIDFromOrderID = 0
        Dim LastKit As String = ""
        Dim x As Integer = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order_line where orderID=@orderID order by kitID desc"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetServiceProfileIDFromOrderID = reader.Item("kitID")
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetHoursMilesFromCart(ByVal userID As Integer, ByVal companyID As Integer) As Integer
        GetHoursMilesFromCart = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select hours_miles from t_cart where userID=@userID and companyID=@companyID order by kitID desc"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetHoursMilesFromCart = reader.Item("hours_miles")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetServiceProfileIDFromCart(ByVal userID As Integer, ByVal companyID As Integer) As Integer
        GetServiceProfileIDFromCart = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select kitID from t_cart where userID=@userID and companyID=@companyID order by kitID desc"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetServiceProfileIDFromCart = reader.Item("kitID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetKits(ByVal userID As Integer, ByVal companyID As Integer) As String
        GetKits = "0"
        Dim LastKit As String = ""
        Dim x As Integer = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select kitID from t_cart where userID=@userID and companyID=@companyID order by kitID desc"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            LastKit = reader.Item("kitID")
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsOnPO(ByVal manufacturer As String, ByVal partnumber As String) As Boolean
        IsOnPO = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_poline where manufacturer=@manufacturer and partnumber=@partnumber and complete=@complete"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@complete", False)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsOnPO = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsKitItem(ByVal cartID As Integer) As Boolean
        IsKitItem = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_cart where cartID=@cartID and kitID<>'0'"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@cartID", cartID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsKitItem = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsFavorite(ByVal productID As Integer, ByVal companyID As Integer) As Boolean
        IsFavorite = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_favorites where productID=@productID and companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@productID", productID)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsFavorite = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function InsertFavorite(ByVal productID As Integer, ByVal userID As Integer, ByVal companyID As Integer) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_favorites (productID,userID,companyID) values(@productID,@userID,@companyID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@productID", productID)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertFavorite = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Sub DeleteFavorite(ByVal favoriteID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_favorites where favoriteID=@favoriteID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@favoriteID", favoriteID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Function InsertKitItem(ByVal companyID As Integer, ByVal userID As Integer, ByVal vendorID As Integer, ByVal productID As Integer, ByVal manufacturer As String, ByVal partnumber As String, ByVal item As String, ByVal quantity As Double, ByVal price As Double, ByVal uom As String, ByVal weight As Double, ByVal kitID As Integer) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        Dim reader As SqlDataReader
        conn.Open()
        commandString = "insert into t_cart (companyID,userID,vendorID,productID,manufacturer,partnumber,item,quantity,price,uom,weight,kitID) values(@companyID,@userID,@vendorID,@productID,@manufacturer,@partnumber,@item,@quantity,@price,@uom,@weight,@kitID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@vendorID", vendorID)
        comm.Parameters.AddWithValue("@productID", productID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@item", item)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.Parameters.AddWithValue("@price", price)
        comm.Parameters.AddWithValue("@uom", uom)
        comm.Parameters.AddWithValue("@weight", weight)
        comm.Parameters.AddWithValue("@kitID", kitID)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertKitItem = comm.ExecuteScalar()
        Dim coreID As Integer = GetCoreID(productID)
        If coreID > 0 Then
            commandString = "select * from t_product where productID=@productID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@productID", coreID)
            reader = comm.ExecuteReader
            If reader.Read Then
                Dim cartID As Integer = InsertKitItem(companyID, userID, vendorID, reader.Item("productID"), reader.Item("manufacturer"), reader.Item("partnumber"), reader.Item("item"), quantity, reader.Item("msrp"), reader.Item("uom"), 0, kitID)
            End If
            reader.Close()
        End If
        conn.Close()
    End Function

    Public Shared Function InsertAd(ByVal productID As Integer, ByVal pic As String, ByVal pic_size As String, ByVal pic_hw As String, ByVal line1 As String, ByVal line1_size As String, ByVal line1_color As String, ByVal line1_bold As Boolean, ByVal line2 As String, ByVal line2_size As String, ByVal line2_color As String, ByVal line2_bold As Boolean, ByVal line3 As String, ByVal line3_size As String, ByVal line3_color As String, ByVal line3_bold As Boolean, ByVal line4 As String, ByVal line4_size As String, ByVal line4_color As String, ByVal line4_bold As Boolean, ByVal line5 As String, ByVal line5_size As String, ByVal line5_color As String, ByVal line5_bold As Boolean, ByVal line6 As String, ByVal line6_size As String, ByVal line6_color As String, ByVal line6_bold As Boolean) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_ad (productID,pic,pic_size,pic_hw,line1,line1_size,line1_color,line1_bold,line2,line2_size,line2_color,line2_bold,line3,line3_size,line3_color,line3_bold,line4,line4_size,line4_color,line4_bold,line5,line5_size,line5_color,line5_bold,line6,line6_size,line6_color,line6_bold) values(@productID,@pic,@pic_size,@pic_hw,@line1,@line1_size,@line1_color,@line1_bold,@line2,@line2_size,@line2_color,@line2_bold,@line3,@line3_size,@line3_color,@line3_bold,@line4,@line4_size,@line4_color,@line4_bold,@line5,@line5_size,@line5_color,@line5_bold,@line6,@line6_size,@line6_color,@line6_bold)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@productID", productID)
        comm.Parameters.AddWithValue("@pic", pic)
        comm.Parameters.AddWithValue("@pic_size", pic_size)
        comm.Parameters.AddWithValue("@pic_hw", pic_hw)
        comm.Parameters.AddWithValue("@line1", line1)
        comm.Parameters.AddWithValue("@line1_size", line1_size)
        comm.Parameters.AddWithValue("@line1_color", line1_color)
        comm.Parameters.AddWithValue("@line1_bold", line1_bold)
        comm.Parameters.AddWithValue("@line2", line2)
        comm.Parameters.AddWithValue("@line2_size", line2_size)
        comm.Parameters.AddWithValue("@line2_color", line2_color)
        comm.Parameters.AddWithValue("@line2_bold", line2_bold)
        comm.Parameters.AddWithValue("@line3", line3)
        comm.Parameters.AddWithValue("@line3_size", line3_size)
        comm.Parameters.AddWithValue("@line3_color", line3_color)
        comm.Parameters.AddWithValue("@line3_bold", line3_bold)
        comm.Parameters.AddWithValue("@line4", line4)
        comm.Parameters.AddWithValue("@line4_size", line4_size)
        comm.Parameters.AddWithValue("@line4_color", line4_color)
        comm.Parameters.AddWithValue("@line4_bold", line4_bold)
        comm.Parameters.AddWithValue("@line5", line5)
        comm.Parameters.AddWithValue("@line5_size", line5_size)
        comm.Parameters.AddWithValue("@line5_color", line5_color)
        comm.Parameters.AddWithValue("@line5_bold", line5_bold)
        comm.Parameters.AddWithValue("@line6", line6)
        comm.Parameters.AddWithValue("@line6_size", line6_size)
        comm.Parameters.AddWithValue("@line6_color", line6_color)
        comm.Parameters.AddWithValue("@line6_bold", line6_bold)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertAd = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Sub UpdateAd(ByVal adID As Integer, ByVal productID As Integer, ByVal pic As String, ByVal pic_size As String, ByVal pic_hw As String, ByVal line1 As String, ByVal line1_size As String, ByVal line1_color As String, ByVal line1_bold As Boolean, ByVal line2 As String, ByVal line2_size As String, ByVal line2_color As String, ByVal line2_bold As Boolean, ByVal line3 As String, ByVal line3_size As String, ByVal line3_color As String, ByVal line3_bold As Boolean, ByVal line4 As String, ByVal line4_size As String, ByVal line4_color As String, ByVal line4_bold As Boolean, ByVal line5 As String, ByVal line5_size As String, ByVal line5_color As String, ByVal line5_bold As Boolean, ByVal line6 As String, ByVal line6_size As String, ByVal line6_color As String, ByVal line6_bold As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_ad set productID=@productID,pic=@pic,pic_size=@pic_size,pic_hw=@pic_hw,line1=@line1,line1_size=@line1_size,line1_color=@line1_color,line1_bold=@line1_bold,line2=@line2,line2_size=@line2_size,line2_color=@line2_color,line2_bold=@line2_bold,line3=@line3,line3_size=@line3_size,line3_color=@line3_color,line3_bold=@line3_bold,line4=@line4,line4_size=@line4_size,line4_color=@line4_color,line4_bold=@line4_bold,line5=@line5,line5_size=@line5_size,line5_color=@line5_color,line5_bold=@line5_bold,line6=@line6,line6_size=@line6_size,line6_color=@line6_color,line6_bold=@line6_bold where adID=@adID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@adID", adID)
        comm.Parameters.AddWithValue("@productID", productID)
        comm.Parameters.AddWithValue("@pic", pic)
        comm.Parameters.AddWithValue("@pic_size", pic_size)
        comm.Parameters.AddWithValue("@pic_hw", pic_hw)
        comm.Parameters.AddWithValue("@line1", line1)
        comm.Parameters.AddWithValue("@line1_size", line1_size)
        comm.Parameters.AddWithValue("@line1_color", line1_color)
        comm.Parameters.AddWithValue("@line1_bold", line1_bold)
        comm.Parameters.AddWithValue("@line2", line2)
        comm.Parameters.AddWithValue("@line2_size", line2_size)
        comm.Parameters.AddWithValue("@line2_color", line2_color)
        comm.Parameters.AddWithValue("@line2_bold", line2_bold)
        comm.Parameters.AddWithValue("@line3", line3)
        comm.Parameters.AddWithValue("@line3_size", line3_size)
        comm.Parameters.AddWithValue("@line3_color", line3_color)
        comm.Parameters.AddWithValue("@line3_bold", line3_bold)
        comm.Parameters.AddWithValue("@line4", line4)
        comm.Parameters.AddWithValue("@line4_size", line4_size)
        comm.Parameters.AddWithValue("@line4_color", line4_color)
        comm.Parameters.AddWithValue("@line4_bold", line4_bold)
        comm.Parameters.AddWithValue("@line5", line5)
        comm.Parameters.AddWithValue("@line5_size", line5_size)
        comm.Parameters.AddWithValue("@line5_color", line5_color)
        comm.Parameters.AddWithValue("@line5_bold", line5_bold)
        comm.Parameters.AddWithValue("@line6", line6)
        comm.Parameters.AddWithValue("@line6_size", line6_size)
        comm.Parameters.AddWithValue("@line6_color", line6_color)
        comm.Parameters.AddWithValue("@line6_bold", line6_bold)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteAd(ByVal adID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_ad where adID=@adID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@adID", adID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Function UpdateList(ByVal email As String, ByVal name As String, ByVal company As String, ByVal listitemID As Integer) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_listitem set name=@name,company=@company,email=@email where listitemID=@listitemID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@email", email)
        comm.Parameters.AddWithValue("@name", name)
        comm.Parameters.AddWithValue("@company", company)
        comm.Parameters.AddWithValue("@listitemID", listitemID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Function

    Public Shared Function InsertList(ByVal email As String, ByVal name As String, ByVal company As String, ByVal listID As Integer) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_listitem (email,name,company,listID) values(@email,@name,@company,@listID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@email", email)
        comm.Parameters.AddWithValue("@name", name)
        comm.Parameters.AddWithValue("@company", company)
        comm.Parameters.AddWithValue("@listID", listID)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertList = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertOrder(ByVal purchaseorder As String, ByVal companyID As Integer, ByVal company As String, ByVal vendorID As Integer, ByVal vendor As String, ByVal v_phone As String, ByVal v_fax As String, ByVal order_date As String, ByVal confirm_date As String, ByVal confirmed As Boolean, ByVal complete As Boolean, ByVal placed As Boolean, ByVal placedby As String, ByVal remitto As String, ByVal r_address1 As String, ByVal r_address2 As String, ByVal r_city As String, ByVal r_state As String, ByVal r_zipcode As String, ByVal b_address1 As String, ByVal b_address2 As String, ByVal b_city As String, ByVal b_state As String, ByVal b_zipcode As String, ByVal shipID As Integer, ByVal shipto As String, ByVal s_address1 As String, ByVal s_address2 As String, ByVal s_address3 As String, ByVal s_city As String, ByVal s_state As String, ByVal s_zipcode As String, ByVal userID As Integer, ByVal c_contact As String, ByVal c_phone As String, ByVal c_fax As String, ByVal notes As String, ByVal ship_method As String, ByVal ship_options As String, ByVal is_kit As Boolean, ByVal serviceID As Integer, ByVal serviceprofileID As String, ByVal reference As String, ByVal rush As Boolean, ByVal repID As String) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_order (purchaseorder,companyID,company,vendorID,vendor,v_phone,v_fax,order_date,confirm_date,confirmed,complete,placed,placedby,remitto,r_address1,r_address2,r_city,r_state,r_zipcode,b_address1,b_address2,b_city,b_state,b_zipcode,shipID,shipto,s_address1,s_address2,s_address3,s_city,s_state,s_zipcode,userID,c_contact,c_phone,c_fax,notes,ship_method,ship_options,is_kit,serviceID,serviceprofileID,reference,repID) values(@purchaseorder,@companyID,@company,@vendorID,@vendor,@v_phone,@v_fax,@order_date,@confirm_date,@confirmed,@complete,@placed,@placedby,@remitto,@r_address1,@r_address2,@r_city,@r_state,@r_zipcode,@b_address1,@b_address2,@b_city,@b_state,@b_zipcode,@shipID,@shipto,@s_address1,@s_address2,@s_address3,@s_city,@s_state,@s_zipcode,@userID,@c_contact,@c_phone,@c_fax,@notes,@ship_method,@ship_options,@is_kit,@serviceID,@serviceprofileID,@reference,@repID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@purchaseorder", purchaseorder)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@company", company)
        comm.Parameters.AddWithValue("@vendorID", vendorID)
        comm.Parameters.AddWithValue("@vendor", vendor)
        comm.Parameters.AddWithValue("@v_phone", v_phone)
        comm.Parameters.AddWithValue("@v_fax", v_fax)
        comm.Parameters.AddWithValue("@order_date", order_date)
        comm.Parameters.AddWithValue("@confirm_date", confirm_date)
        comm.Parameters.AddWithValue("@confirmed", confirmed)
        comm.Parameters.AddWithValue("@complete", complete)
        comm.Parameters.AddWithValue("@placed", placed)
        comm.Parameters.AddWithValue("@placedby", placedby)
        comm.Parameters.AddWithValue("@remitto", remitto)
        comm.Parameters.AddWithValue("@r_address1", r_address1)
        comm.Parameters.AddWithValue("@r_address2", r_address2)
        comm.Parameters.AddWithValue("@r_city", r_city)
        comm.Parameters.AddWithValue("@r_state", r_state)
        comm.Parameters.AddWithValue("@r_zipcode", r_zipcode)
        comm.Parameters.AddWithValue("@b_address1", b_address1)
        comm.Parameters.AddWithValue("@b_address2", b_address2)
        comm.Parameters.AddWithValue("@b_city", b_city)
        comm.Parameters.AddWithValue("@b_state", b_state)
        comm.Parameters.AddWithValue("@b_zipcode", b_zipcode)
        comm.Parameters.AddWithValue("@shipID", shipID)
        comm.Parameters.AddWithValue("@shipto", shipto)
        comm.Parameters.AddWithValue("@s_address1", s_address1)
        comm.Parameters.AddWithValue("@s_address2", s_address2)
        comm.Parameters.AddWithValue("@s_address3", s_address3)
        comm.Parameters.AddWithValue("@s_city", s_city)
        comm.Parameters.AddWithValue("@s_state", s_state)
        comm.Parameters.AddWithValue("@s_zipcode", s_zipcode)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@c_contact", c_contact)
        comm.Parameters.AddWithValue("@c_phone", c_phone)
        comm.Parameters.AddWithValue("@c_fax", c_fax)
        comm.Parameters.AddWithValue("@notes", notes)
        comm.Parameters.AddWithValue("@ship_method", ship_method)
        comm.Parameters.AddWithValue("@ship_options", ship_options)
        comm.Parameters.AddWithValue("@is_kit", is_kit)
        comm.Parameters.AddWithValue("@serviceID", serviceID)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.Parameters.AddWithValue("@reference", reference)
        comm.Parameters.AddWithValue("@rush", rush)
        comm.Parameters.AddWithValue("@repID", repID)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertOrder = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertOrderLine(ByVal orderID As Integer, ByVal partID As Integer, ByVal assetID As String, ByVal manufacturer As String, ByVal partnumber As String, ByVal item As String, ByVal quantity As Double, ByVal price As Double, ByVal uom As String, ByVal availability As String, quote As Boolean, ByVal kitID As String) As Integer
        Dim cost As Double = GetCost(manufacturer, partnumber)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_order_line (orderID,partID,assetID,manufacturer,partnumber,item,quantity,cost,price,uom,quote,kitID) values(@orderID,@partID,@assetID,@manufacturer,@partnumber,@item,@quantity,@cost,@price,@uom,@quote,@kitID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.Parameters.AddWithValue("@partID", partID)
        comm.Parameters.AddWithValue("@assetID", assetID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@item", item)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.Parameters.AddWithValue("@cost", cost)
        comm.Parameters.AddWithValue("@price", price)
        comm.Parameters.AddWithValue("@uom", uom)
        comm.Parameters.AddWithValue("@quote", quote)
        comm.Parameters.AddWithValue("@kitID", kitID)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertOrderLine = comm.ExecuteScalar()
        conn.Close()
        Dim vendorID As Integer = GetOrderVendorID(orderID)
        Dim companyID As Integer = GetOrderCompanyID(orderID)
        Dim order_date As String = GetOrderDate(orderID)
        If Left(partnumber, 4) <> "CORE" Then
            Dim requisitionID As Integer = InsertRequisition(vendorID, orderID, order_date, InsertOrderLine, manufacturer, partnumber, item, quantity, cost, uom)
        End If
    End Function

    Public Shared Function InsertPart(ByVal equipmentID As Integer, ByVal part_type As String, ByVal manufacturer As String, ByVal partnumber As String, ByVal description As String, ByVal alt_manufacturer As String, ByVal alt_partnumber As String, ByVal oem_manufacturer As String, ByVal oem_partnumber As String, ByVal quantity As Double, ByVal uom As String, ByVal price As Double, ByVal notes As String) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_parts (equipmentID,part_type,manufacturer,partnumber,description,alt_manufacturer,alt_partnumber,oem_manufacturer,oem_partnumber,quantity,uom,price,notes) values(@equipmentID,@part_type,@manufacturer,@partnumber,@description,@alt_manufacturer,@alt_partnumber,@oem_manufacturer,@oem_partnumber,@quantity,@uom,@price,@notes)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.Parameters.AddWithValue("@part_type", part_type)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@alt_manufacturer", alt_manufacturer)
        comm.Parameters.AddWithValue("@alt_partnumber", alt_partnumber)
        comm.Parameters.AddWithValue("@oem_manufacturer", oem_manufacturer)
        comm.Parameters.AddWithValue("@oem_partnumber", oem_partnumber)
        comm.Parameters.AddWithValue("@description", description)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.Parameters.AddWithValue("@uom", uom)
        comm.Parameters.AddWithValue("@price", price)
        comm.Parameters.AddWithValue("@notes", notes)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertPart = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertPO(ByVal vendorID As Integer, ByVal vendor As String, ByVal date_submitted As String, ByVal userID As Integer, ByVal username As String) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_po (vendorID,vendor,date_submitted,userID,username) values(@vendorID,@vendor,@date_submitted,@userID,@username)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@vendorID", vendorID)
        comm.Parameters.AddWithValue("@vendor", vendor)
        comm.Parameters.AddWithValue("@date_submitted", date_submitted)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@username", username)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertPO = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertPOLine(ByVal poID As Integer, ByVal manufacturer As String, ByVal partnumber As String, ByVal quantity As Double, ByVal cost As Double, ByVal uom As String, ByVal rebate As Boolean, ByVal lineID As Integer) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_poline (poID,manufacturer,partnumber,quantity,cost,uom,rebate,lineID) values(@poID,@manufacturer,@partnumber,@quantity,@cost,@uom,@rebate,@lineID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@poID", poID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.Parameters.AddWithValue("@cost", cost)
        comm.Parameters.AddWithValue("@uom", uom)
        comm.Parameters.AddWithValue("@rebate", rebate)
        comm.Parameters.AddWithValue("@lineID", lineID)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertPOLine = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertQuote(ByVal companyID As Integer, ByVal company As String, ByVal vendorID As Integer, ByVal vendor As String, ByVal v_phone As String, ByVal v_fax As String, ByVal request_date As String, ByVal response_date As String, ByVal complete As Boolean, ByVal submitted As Boolean, ByVal submittedby As String, ByVal remitto As String, ByVal r_address1 As String, ByVal r_address2 As String, ByVal r_city As String, ByVal r_state As String, ByVal r_zipcode As String, ByVal b_address1 As String, ByVal b_address2 As String, ByVal b_city As String, ByVal b_state As String, ByVal b_zipcode As String, ByVal shipID As Integer, ByVal shipto As String, ByVal s_address1 As String, ByVal s_address2 As String, ByVal s_address3 As String, ByVal s_city As String, ByVal s_state As String, ByVal s_zipcode As String, ByVal userID As Integer, ByVal c_contact As String, ByVal c_phone As String, ByVal c_fax As String, ByVal is_kit As Boolean, ByVal serviceID As Integer, ByVal serviceprofileID As Integer) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_quote (companyID,company,vendorID,vendor,v_phone,v_fax,request_date,response_date,complete,submitted,submittedby,remitto,r_address1,r_address2,r_city,r_state,r_zipcode,b_address1,b_address2,b_city,b_state,b_zipcode,shipID,shipto,s_address1,s_address2,s_address3,s_city,s_state,s_zipcode,userID,c_contact,c_phone,c_fax,is_kit,serviceID,serviceprofileID) values(@companyID,@company,@vendorID,@vendor,@v_phone,@v_fax,@request_date,@response_date,@complete,@submitted,@submittedby,@remitto,@r_address1,@r_address2,@r_city,@r_state,@r_zipcode,@b_address1,@b_address2,@b_city,@b_state,@b_zipcode,@shipID,@shipto,@s_address1,@s_address2,@s_address3,@s_city,@s_state,@s_zipcode,@userID,@c_contact,@c_phone,@c_fax,@is_kit,@serviceID,@serviceprofileID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@company", company)
        comm.Parameters.AddWithValue("@vendorID", vendorID)
        comm.Parameters.AddWithValue("@vendor", vendor)
        comm.Parameters.AddWithValue("@v_phone", v_phone)
        comm.Parameters.AddWithValue("@v_fax", v_fax)
        comm.Parameters.AddWithValue("@request_date", request_date)
        comm.Parameters.AddWithValue("@response_date", response_date)
        comm.Parameters.AddWithValue("@complete", complete)
        comm.Parameters.AddWithValue("@submitted", submitted)
        comm.Parameters.AddWithValue("@submittedby", submittedby)
        comm.Parameters.AddWithValue("@remitto", remitto)
        comm.Parameters.AddWithValue("@r_address1", r_address1)
        comm.Parameters.AddWithValue("@r_address2", r_address2)
        comm.Parameters.AddWithValue("@r_city", r_city)
        comm.Parameters.AddWithValue("@r_state", r_state)
        comm.Parameters.AddWithValue("@r_zipcode", r_zipcode)
        comm.Parameters.AddWithValue("@b_address1", b_address1)
        comm.Parameters.AddWithValue("@b_address2", b_address2)
        comm.Parameters.AddWithValue("@b_city", b_city)
        comm.Parameters.AddWithValue("@b_state", b_state)
        comm.Parameters.AddWithValue("@b_zipcode", b_zipcode)
        comm.Parameters.AddWithValue("@shipID", shipID)
        comm.Parameters.AddWithValue("@shipto", shipto)
        comm.Parameters.AddWithValue("@s_address1", s_address1)
        comm.Parameters.AddWithValue("@s_address2", s_address2)
        comm.Parameters.AddWithValue("@s_address3", s_address3)
        comm.Parameters.AddWithValue("@s_city", s_city)
        comm.Parameters.AddWithValue("@s_state", s_state)
        comm.Parameters.AddWithValue("@s_zipcode", s_zipcode)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@c_contact", c_contact)
        comm.Parameters.AddWithValue("@c_phone", c_phone)
        comm.Parameters.AddWithValue("@c_fax", c_fax)
        comm.Parameters.AddWithValue("@is_kit", is_kit)
        comm.Parameters.AddWithValue("@serviceID", serviceID)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertQuote = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertQuickCartLine(ByVal companyID As Integer, ByVal userID As Integer, ByVal vendorID As Integer, ByVal manufacturer As String, ByVal partnumber As String, ByVal quantity As Double, ByVal ssID As String) As Boolean
        Dim productID As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            Dim cartID As Integer = InsertCartItem(companyID, userID, vendorID, reader.Item("productID"), reader.Item("manufacturer"), reader.Item("partnumber"), reader.Item("item"), quantity, GetCompanyPrice(companyID, reader.Item("manufacturer"), partnumber), reader.Item("uom"), reader.Item("weight"), ssID)
        End If
        reader.Close()
        conn.Close()
        InsertQuickCartLine = True
    End Function

    Public Shared Function InsertQuoteLine(ByVal quoteID As Integer, ByVal partID As Integer, ByVal assetID As String, ByVal manufacturer As String, ByVal partnumber As String, ByVal item As String, ByVal quantity As Double, ByVal price As Double, ByVal uom As String, ByVal availability As String, ByVal quote As Boolean, ByVal kitID As String) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_quote_line (quoteID,partID,assetID,manufacturer,partnumber,item,quantity,price,uom,availability,quote,kitID) values(@quoteID,@partID,@assetID,@manufacturer,@partnumber,@item,@quantity,@price,@uom,@availability,@quote,@kitID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@quoteID", quoteID)
        comm.Parameters.AddWithValue("@partID", partID)
        comm.Parameters.AddWithValue("@assetID", assetID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@item", item)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.Parameters.AddWithValue("@price", price)
        comm.Parameters.AddWithValue("@uom", uom)
        comm.Parameters.AddWithValue("@availability", availability)
        comm.Parameters.AddWithValue("@quote", quote)
        comm.Parameters.AddWithValue("@kitID", kitID)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertQuoteLine = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Sub UpdateOrderServiceProfileID(ByVal orderID As Integer, ByVal serviceprofileID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_order set serviceprofileID=@serviceprofileID where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdatePricing(ByVal companyID As Integer, ByVal manufacturer As String, ByVal partnumber As String, ByVal price As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_pricing set price=@price where companyID=@companyID and manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@price", price)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Function InsertPricing(ByVal companyID As Integer, ByVal manufacturer As String, ByVal partnumber As String, ByVal price As Double) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_pricing (companyID,manufacturer,partnumber,price) values(@companyID,@manufacturer,@partnumber,@price)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@price", price)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertPricing = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertReceipt(ByVal poID As Integer, ByVal manufacturer As String, ByVal partnumber As String, ByVal received As Double) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_receipt (poID,manufacturer,partnumber,received) values(@poID,@manufacturer,@partnumber,@received)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@poID", poID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@received", received)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertReceipt = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertRequisition(ByVal vendorID As Integer, ByVal orderID As Integer, ByVal order_date As String, ByVal lineID As Integer, ByVal manufacturer As String, ByVal partnumber As String, ByVal item As String, ByVal quantity As Double, ByVal cost As Double, ByVal uom As String) As Integer
        'add to a purchase order
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_requisition (vendorID,orderID,order_date,lineID,manufacturer,partnumber,item,quantity,cost,uom) values(@vendorID,@orderID,@order_date,@lineID,@manufacturer,@partnumber,@item,@quantity,@cost,@uom)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@vendorID", vendorID)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.Parameters.AddWithValue("@order_date", order_date)
        comm.Parameters.AddWithValue("@lineID", lineID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@item", item)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.Parameters.AddWithValue("@cost", cost)
        comm.Parameters.AddWithValue("@uom", uom)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertRequisition = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertReturn(ByVal companyID As Integer, ByVal company As String, ByVal vendorID As Integer, ByVal vendor As String, ByVal v_phone As String, ByVal v_fax As String, ByVal order_date As String, ByVal confirm_date As String, ByVal confirmed As Boolean, ByVal complete As Boolean, ByVal placed As Boolean, ByVal placedby As String, ByVal remitto As String, ByVal r_address1 As String, ByVal r_address2 As String, ByVal r_city As String, ByVal r_state As String, ByVal r_zipcode As String, ByVal b_address1 As String, ByVal b_address2 As String, ByVal b_city As String, ByVal b_state As String, ByVal b_zipcode As String, ByVal shipID As Integer, ByVal shipto As String, ByVal s_address1 As String, ByVal s_address2 As String, ByVal s_address3 As String, ByVal s_city As String, ByVal s_state As String, ByVal s_zipcode As String, ByVal userID As Integer, ByVal c_contact As String, ByVal c_phone As String, ByVal c_fax As String, ByVal notes As String, ByVal ship_method As String, ByVal ship_options As String, ByVal is_kit As Boolean, ByVal serviceID As Integer, ByVal serviceprofileID As Integer) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_order (companyID,company,vendorID,vendor,v_phone,v_fax,order_date,confirm_date,confirmed,complete,placed,placedby,remitto,r_address1,r_address2,r_city,r_state,r_zipcode,b_address1,b_address2,b_city,b_state,b_zipcode,shipID,shipto,s_address1,s_address2,s_address3,s_city,s_state,s_zipcode,userID,c_contact,c_phone,c_fax,notes,ship_method,ship_options,is_kit,serviceID,serviceprofileID,isReturn) values(@companyID,@company,@vendorID,@vendor,@v_phone,@v_fax,@order_date,@confirm_date,@confirmed,@complete,@placed,@placedby,@remitto,@r_address1,@r_address2,@r_city,@r_state,@r_zipcode,@b_address1,@b_address2,@b_city,@b_state,@b_zipcode,@shipID,@shipto,@s_address1,@s_address2,@s_address3,@s_city,@s_state,@s_zipcode,@userID,@c_contact,@c_phone,@c_fax,@notes,@ship_method,@ship_options,@is_kit,@serviceID,@serviceprofileID,@isReturn)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@company", company)
        comm.Parameters.AddWithValue("@vendorID", vendorID)
        comm.Parameters.AddWithValue("@vendor", vendor)
        comm.Parameters.AddWithValue("@v_phone", v_phone)
        comm.Parameters.AddWithValue("@v_fax", v_fax)
        comm.Parameters.AddWithValue("@order_date", order_date)
        comm.Parameters.AddWithValue("@confirm_date", confirm_date)
        comm.Parameters.AddWithValue("@confirmed", confirmed)
        comm.Parameters.AddWithValue("@complete", complete)
        comm.Parameters.AddWithValue("@placed", placed)
        comm.Parameters.AddWithValue("@placedby", placedby)
        comm.Parameters.AddWithValue("@remitto", remitto)
        comm.Parameters.AddWithValue("@r_address1", r_address1)
        comm.Parameters.AddWithValue("@r_address2", r_address2)
        comm.Parameters.AddWithValue("@r_city", r_city)
        comm.Parameters.AddWithValue("@r_state", r_state)
        comm.Parameters.AddWithValue("@r_zipcode", r_zipcode)
        comm.Parameters.AddWithValue("@b_address1", b_address1)
        comm.Parameters.AddWithValue("@b_address2", b_address2)
        comm.Parameters.AddWithValue("@b_city", b_city)
        comm.Parameters.AddWithValue("@b_state", b_state)
        comm.Parameters.AddWithValue("@b_zipcode", b_zipcode)
        comm.Parameters.AddWithValue("@shipID", shipID)
        comm.Parameters.AddWithValue("@shipto", shipto)
        comm.Parameters.AddWithValue("@s_address1", s_address1)
        comm.Parameters.AddWithValue("@s_address2", s_address2)
        comm.Parameters.AddWithValue("@s_address3", s_address3)
        comm.Parameters.AddWithValue("@s_city", s_city)
        comm.Parameters.AddWithValue("@s_state", s_state)
        comm.Parameters.AddWithValue("@s_zipcode", s_zipcode)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@c_contact", c_contact)
        comm.Parameters.AddWithValue("@c_phone", c_phone)
        comm.Parameters.AddWithValue("@c_fax", c_fax)
        comm.Parameters.AddWithValue("@notes", notes)
        comm.Parameters.AddWithValue("@ship_method", ship_method)
        comm.Parameters.AddWithValue("@ship_options", ship_options)
        comm.Parameters.AddWithValue("@is_kit", is_kit)
        comm.Parameters.AddWithValue("@serviceID", serviceID)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.Parameters.AddWithValue("@isReturn", "Yes")
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertReturn = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertService(ByVal serviceprofileID As Integer, ByVal scheduledby As String, ByVal technician As String, ByVal email As String, ByVal schedule_date As String, ByVal location As String, ByVal address As String, ByVal address2 As String, ByVal address3 As String, ByVal city As String, ByVal state As String, ByVal zipcode As String, ByVal instructions As String, ByVal hours_miles As String, ByVal workorder As String, ByVal fuel_gallons As Double, ByVal def_gallons As Double, ByVal service_type As String) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_service (serviceprofileID,scheduledby,technician,email,schedule_date,location,address,address2,address3,city,state,zipcode,instructions,hours_miles,workorder,fuel_gallons,def_gallons,service_type) values(@serviceprofileID,@scheduledby,@technician,@email,@schedule_date,@location,@address,@address2,@address3,@city,@state,@zipcode,@instructions,@hours_miles,@workorder,@fuel_gallons,@def_gallons,@service_type)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.Parameters.AddWithValue("@scheduledby", scheduledby)
        comm.Parameters.AddWithValue("@technician", technician)
        comm.Parameters.AddWithValue("@email", email)
        comm.Parameters.AddWithValue("@schedule_date", schedule_date)
        comm.Parameters.AddWithValue("@location", location)
        comm.Parameters.AddWithValue("@address", address)
        comm.Parameters.AddWithValue("@address2", address2)
        comm.Parameters.AddWithValue("@address3", address3)
        comm.Parameters.AddWithValue("@city", city)
        comm.Parameters.AddWithValue("@state", state)
        comm.Parameters.AddWithValue("@zipcode", zipcode)
        comm.Parameters.AddWithValue("@instructions", instructions)
        comm.Parameters.AddWithValue("@hours_miles", hours_miles)
        comm.Parameters.AddWithValue("@workorder", workorder)
        comm.Parameters.AddWithValue("@fuel_gallons", fuel_gallons)
        comm.Parameters.AddWithValue("@def_gallons", def_gallons)
        comm.Parameters.AddWithValue("@service_type", service_type)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertService = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertServicePart(ByVal partID As Integer, ByVal serviceprofileID As Integer, ByVal selected As Boolean, ByVal partnotes As String) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_serviceparts (partID,serviceprofileID,selected,partnotes) values(@partID,@serviceprofileID,@selected,@partnotes)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@partID", partID)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.Parameters.AddWithValue("@selected", selected)
        comm.Parameters.AddWithValue("@partnotes", partnotes)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertServicePart = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertServiceProfile(ByVal equipmentID As Integer, ByVal name As String, ByVal interval As String, ByVal interval_type As String, ByVal servicenotes As String, ByVal servicetype As String) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_serviceprofile (equipmentID,name,interval,interval_type,servicenotes,servicetype) values(@equipmentID,@name,@interval,@interval_type,@servicenotes,@servicetype)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.Parameters.AddWithValue("@name", name)
        comm.Parameters.AddWithValue("@interval", interval)
        comm.Parameters.AddWithValue("@interval_type", interval_type)
        comm.Parameters.AddWithValue("@servicenotes", servicenotes)
        comm.Parameters.AddWithValue("@servicetype", servicetype)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertServiceProfile = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertShipment(ByVal orderID As Integer, ByVal pick_date As String) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_shipment (orderID,pick_date) values(@orderID,@pick_date)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.Parameters.AddWithValue("@pick_date", pick_date)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertShipment = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertShipmentLine(ByVal shipmentID As Integer, ByVal lineID As Integer, ByVal ship_qty As Double) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_shipment_line (shipmentID,lineID,ship_qty) values(@shipmentID,@lineID,@ship_qty)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        comm.Parameters.AddWithValue("@lineID", lineID)
        comm.Parameters.AddWithValue("@ship_qty", ship_qty)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertShipmentLine = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertShipTo(ByVal companyID As Integer, ByVal shipto As String, ByVal s_address1 As String, ByVal s_address2 As String, ByVal s_address3 As String, ByVal s_city As String, ByVal s_state As String, ByVal s_zipcode As String, ByVal s_phone As String) As Integer
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "insert into t_ship (companyID,shipto,s_address1,s_address2,s_address3,s_city,s_state,s_zipcode,s_phone) values(@companyID,@shipto,@s_address1,@s_address2,@s_address3,@s_city,@s_state,@s_zipcode,@s_phone)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@shipto", shipto)
        comm.Parameters.AddWithValue("@s_address1", s_address1)
        comm.Parameters.AddWithValue("@s_address2", s_address2)
        comm.Parameters.AddWithValue("@s_address3", s_address3)
        comm.Parameters.AddWithValue("@s_city", s_city)
        comm.Parameters.AddWithValue("@s_state", s_state)
        comm.Parameters.AddWithValue("@s_zipcode", s_zipcode)
        comm.Parameters.AddWithValue("@s_phone", s_phone)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertShipTo = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertSupplier(ByVal company As String, ByVal address1 As String, ByVal address2 As String, ByVal city As String, ByVal state As String, ByVal zipcode As String, ByVal c_phone As String, ByVal c_fax As String, ByVal supplier As Boolean, ByVal logo As String, ByVal vendorID As Integer, ByVal branchID As Integer) As Integer
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "insert into t_company (company,address1,address2,city,state,zipcode,c_phone,c_fax,supplier,logo,vendorID,branchID) values(@company,@address1,@address2,@city,@state,@zipcode,@c_phone,@c_fax,@supplier,@logo,@vendorID,@branchID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@company", company)
        comm.Parameters.AddWithValue("@address1", address1)
        comm.Parameters.AddWithValue("@address2", address2)
        comm.Parameters.AddWithValue("@city", city)
        comm.Parameters.AddWithValue("@state", state)
        comm.Parameters.AddWithValue("@zipcode", zipcode)
        comm.Parameters.AddWithValue("@c_phone", c_phone)
        comm.Parameters.AddWithValue("@c_fax", c_fax)
        comm.Parameters.AddWithValue("@supplier", supplier)
        comm.Parameters.AddWithValue("@logo", logo)
        comm.Parameters.AddWithValue("@vendorID", vendorID)
        comm.Parameters.AddWithValue("@branchID", branchID)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertSupplier = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertUser(ByVal name As String, ByVal title As String, ByVal c_phone As String, ByVal c_fax As String, ByVal email As String, ByVal password As String, ByVal companyID As Integer, ByVal administrator As Boolean, ByVal username As String) As Integer
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "insert into t_user (name,title,c_phone,c_fax,email,password,companyID,administrator,username) values(@name,@title,@c_phone,@c_fax,@email,@password,@companyID,@administrator,@username)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@name", name)
        comm.Parameters.AddWithValue("@title", title)
        comm.Parameters.AddWithValue("@c_phone", c_phone)
        comm.Parameters.AddWithValue("@c_fax", c_fax)
        comm.Parameters.AddWithValue("@email", email)
        comm.Parameters.AddWithValue("@password", password)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@administrator", administrator)
        comm.Parameters.AddWithValue("@username", username)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertUser = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertUserCompany(ByVal userID As Integer, ByVal companyID As Integer) As Integer
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "insert into t_user_company (userID,companyID) values(@userID,@companyID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertUserCompany = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertUserLocation(ByVal userID As Integer, ByVal locationID As Integer) As Integer
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "insert into t_user_location (userID,locationID) values(@userID,@locationID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@locationID", locationID)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertUserLocation = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertVendorSupplier(ByVal supplierID As Integer, ByVal vendorID As Integer) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_vendor_supplier (supplierID,vendorID) values(@supplierID,@vendorID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@supplierID", supplierID)
        comm.Parameters.AddWithValue("@vendorID", vendorID)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertVendorSupplier = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertComponent(ByVal component As String) As Integer
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "insert into t_component (component) values(@component)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@component", component)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertComponent = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertWorkDescription(ByVal description As String, ByVal cost As Double, ByVal price As Double, ByVal componentID As Integer) As Integer
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "insert into t_workdescription (description,componentID,cost,price) values(@description,@componentID,@cost,@price)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@description", description)
        comm.Parameters.AddWithValue("@componentID", componentID)
        comm.Parameters.AddWithValue("@cost", cost)
        comm.Parameters.AddWithValue("@price", price)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertWorkDescription = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertWorkListItem(ByVal serviceprofileID As Integer, ByVal descriptionID As Integer) As Integer
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "insert into t_workline (serviceprofileID,descriptionID) values(@serviceprofileID,@descriptionID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.Parameters.AddWithValue("@descriptionID", descriptionID)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertWorkListItem = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertProduct(ByVal companyID As Integer, ByVal category As Integer, ByVal manufacturer As String, ByVal partnumber As String, ByVal item As String, ByVal description As String, ByVal coreID As Integer, ByVal cost As Double, ByVal msrp As Double, ByVal saleprice As Double, ByVal uom As String, ByVal package As Double, ByVal rebate As Boolean, ByVal price_category As String, ByVal weight As Double, ByVal upc As String, ByVal is_tax As Boolean, ByVal featured As Boolean, ByVal clearance As Boolean, ByVal onhand As Double, ByVal nStock As Boolean, ByVal min As Double, ByVal max As Double) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_product (companyID,category,manufacturer,partnumber,item,description,coreID,cost,msrp,saleprice,uom,package,rebate,price_category,weight,upc,is_tax,featured,clearance,onhand,nStock,min,max) values(@companyID,@category,@manufacturer,@partnumber,@item,@description,@coreID,@cost,@msrp,@saleprice,@uom,@package,@rebate,@price_category,@weight,@upc,@is_tax,@featured,@clearance,@onhand,@nStock,@min,@max)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@category", category)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@item", item)
        comm.Parameters.AddWithValue("@description", description)
        comm.Parameters.AddWithValue("@coreID", coreID)
        comm.Parameters.AddWithValue("@cost", cost)
        comm.Parameters.AddWithValue("@msrp", msrp)
        comm.Parameters.AddWithValue("@saleprice", saleprice)
        comm.Parameters.AddWithValue("@uom", uom)
        comm.Parameters.AddWithValue("@package", package)
        comm.Parameters.AddWithValue("@rebate", rebate)
        comm.Parameters.AddWithValue("@price_category", price_category)
        comm.Parameters.AddWithValue("@weight", weight)
        comm.Parameters.AddWithValue("@upc", upc)
        comm.Parameters.AddWithValue("@is_tax", is_tax)
        comm.Parameters.AddWithValue("@featured", featured)
        comm.Parameters.AddWithValue("@clearance", clearance)
        comm.Parameters.AddWithValue("@onhand", onhand)
        comm.Parameters.AddWithValue("@nStock", nStock)
        comm.Parameters.AddWithValue("@min", min)
        comm.Parameters.AddWithValue("@max", max)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertProduct = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Function InsertVersion(ByVal manufacturer As String, ByVal partnumber As String, ByVal version As String, ByVal version1 As String, ByVal alt_partnumber1 As String, ByVal version2 As String, ByVal alt_partnumber2 As String, ByVal version3 As String, ByVal alt_partnumber3 As String) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_product_version (manufacturer,partnumber,version,version1,alt_partnumber1,version2,alt_partnumber2,version3,alt_partnumber3) values(@manufacturer,@partnumber,@version,@version1,@alt_partnumber1,@version2,@alt_partnumber2,@version3,@alt_partnumber3)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@version", version)
        comm.Parameters.AddWithValue("@version1", version1)
        comm.Parameters.AddWithValue("@alt_partnumber1", alt_partnumber1)
        comm.Parameters.AddWithValue("@version2", version2)
        comm.Parameters.AddWithValue("@alt_partnumber2", alt_partnumber2)
        comm.Parameters.AddWithValue("@version3", version3)
        comm.Parameters.AddWithValue("@alt_partnumber3", alt_partnumber3)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertVersion = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Sub UpdateVersion(ByVal versionID As Integer, ByVal version As String, ByVal version1 As String, ByVal alt_partnumber1 As String, ByVal version2 As String, ByVal alt_partnumber2 As String, ByVal version3 As String, ByVal alt_partnumber3 As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_product_version set version=@version,version1=@version1,alt_partnumber1=@alt_partnumber1,version2=@version2,alt_partnumber2=@alt_partnumber2,version3=@version3,alt_partnumber3=@alt_partnumber3 where versionID=@versionID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@versionID", versionID)
        comm.Parameters.AddWithValue("@version", version)
        comm.Parameters.AddWithValue("@version1", version1)
        comm.Parameters.AddWithValue("@alt_partnumber1", alt_partnumber1)
        comm.Parameters.AddWithValue("@version2", version2)
        comm.Parameters.AddWithValue("@alt_partnumber2", alt_partnumber2)
        comm.Parameters.AddWithValue("@version3", version3)
        comm.Parameters.AddWithValue("@alt_partnumber3", alt_partnumber3)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteVersions(ByVal manufacturer As String, ByVal partnumber As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_product_version where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteVersion(ByVal versionID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_product_version where versionID=@versionID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@versionID", versionID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Function InsertAttribute(ByVal manufacturer As String, ByVal partnumber As String, ByVal attribute As String, ByVal attribute_value As String) As Integer
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "insert into t_product_attribute (manufacturer,partnumber,attribute,attribute_value) values(@manufacturer,@partnumber,@attribute,@attribute_value)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@attribute", attribute)
        comm.Parameters.AddWithValue("@attribute_value", attribute_value)
        comm.ExecuteNonQuery()
        comm.CommandText = "select @@IDENTITY"
        InsertAttribute = comm.ExecuteScalar()
        conn.Close()
    End Function

    Public Shared Sub UpdateAttribute(ByVal attributeID As Integer, ByVal attribute As String, ByVal attribute_value As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_product_attribute set attribute=@attribute,attribute_value=@attribute_value where attributeID=@attributeID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@attributeID", attributeID)
        comm.Parameters.AddWithValue("@attribute", attribute)
        comm.Parameters.AddWithValue("@attribute_value", attribute_value)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteAttributes(ByVal manufacturer As String, ByVal partnumber As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_product_attribute where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub DeleteAttribute(ByVal attributeID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "delete from t_product_attribute where attributeID=@attributeID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@attributeID", attributeID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Function HasKitsInCart(ByVal userID As Integer, ByVal companyID As Integer, ByVal vendorID As Integer) As Boolean
        HasKitsInCart = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_cart where userID=@userID and companyID=@companyID and vendorID=@vendorID and kitID <> '0'"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@vendorID", vendorID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            HasKitsInCart = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function HasChildren(ByVal categoryID As Integer) As Boolean
        HasChildren = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_category where parentID=@categoryID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@categoryID", categoryID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            HasChildren = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsTaxable(ByVal manufacturer As String, ByVal partnumber As String) As Boolean
        IsTaxable = True
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_product where partnumber=@partnumber and manufacturer=@manufacturer"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsTaxable = reader.Item("is_tax")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetCartKits(ByVal userID As Integer, ByVal companyID As Integer, ByVal vendorID As Integer) As String
        GetCartKits = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select kitID from t_cart where userID=@userID and companyID=@companyID and vendorID=@vendorID group by kitID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@vendorID", vendorID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        Dim x As Integer = 0
        While reader.Read
            If x > 0 Then
                GetCartKits &= ","
            End If
            GetCartKits &= reader.Item("kitID").ToString
            x += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetXRefText(ByVal ref_manufacturer As String, ByVal ref_partnumber As String) As String
        GetXRefText = ""
        Dim x As Integer = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_xref where ref_manufacturer=@ref_manufacturer and ref_partnumber=@ref_partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@ref_manufacturer", ref_manufacturer)
        comm.Parameters.AddWithValue("@ref_partnumber", ref_partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If x <> 0 Then
                GetXRefText = GetXRefText & ", "
            End If
            If GetOnHand(reader.Item("xref_manufacturer"), reader.Item("xref_partnumber")) > 0 Then
                GetXRefText = GetXRefText & reader.Item("xref_manufacturer") & " " & reader.Item("xref_partnumber") & "(" & GetOnHand(reader.Item("xref_manufacturer"), reader.Item("xref_partnumber")) & ")"
                x += 1
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetXRefManufacturer(ByVal ref_manufacturer As String, ByVal ref_partnumber As String) As String
        GetXRefManufacturer = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_xref where ref_manufacturer=@ref_manufacturer and ref_partnumber=@ref_partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@ref_manufacturer", ref_manufacturer)
        comm.Parameters.AddWithValue("@ref_partnumber", ref_partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetXRefManufacturer = reader.Item("xref_manufacturer")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetXRefPartNumber(ByVal ref_manufacturer As String, ByVal ref_partnumber As String) As String
        GetXRefPartNumber = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_xref where ref_manufacturer=@ref_manufacturer and ref_partnumber=@ref_partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@ref_manufacturer", ref_manufacturer)
        comm.Parameters.AddWithValue("@ref_partnumber", ref_partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetXRefPartNumber = reader.Item("xref_partnumber")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetXRefOnHand(ByVal ref_manufacturer As String, ByVal ref_partnumber As String) As String
        GetXRefOnHand = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_xref where ref_manufacturer=@ref_manufacturer and ref_partnumber=@ref_partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@ref_manufacturer", ref_manufacturer)
        comm.Parameters.AddWithValue("@ref_partnumber", ref_partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetXRefOnHand = reader.Item("xref_manufacturer") & " " & reader.Item("xref_partnumber") & " - " & GetOnHand(reader.Item("xref_manufacturer"), reader.Item("xref_partnumber")) & " / " & GetOnPOAmount(reader.Item("xref_manufacturer"), reader.Item("xref_partnumber"))
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetCartCount(ByVal userID As Integer, ByVal companyID As Integer) As Integer
        GetCartCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select count(cartID) as cartcount from t_cart where userID=@userID and companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetCartCount = reader.Item("cartcount")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetGPDollars(ByVal cost As Double, ByVal price As Double, ByVal quantity As Double) As Double
        GetGPDollars = (price * quantity) - (cost * quantity)
    End Function

    Public Shared Function GetGPPercent(ByVal cost As Double, price As Double) As Double
        GetGPPercent = 1 - cost / price
    End Function

    Public Shared Function GetFavoriteCount(ByVal companyID As Integer) As Integer
        GetFavoriteCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select count(favoriteID) as favoritecount from t_favorites where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetFavoriteCount = reader.Item("favoritecount")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetAssetCount(ByVal companyID As Integer) As Integer
        GetAssetCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select count(equipmentID) as eqcount from t_equipment where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetAssetCount = reader.Item("eqcount")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOpenPOs() As Integer
        GetOpenPOs = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select count(poID) as openpos from t_po where complete=@complete"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@complete", False)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetOpenPOs = reader.Item("openpos")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Sub ConvertAllParts(ByVal manufacturer As String, ByVal companyID As Integer)
        Dim pn As String = ""
        Dim results_mfr As String = ""
        Dim results_pn As String = ""
        Dim primary_mfr As String = ""
        Dim primary_pn As String = ""
        Dim alt_mfr As String = ""
        Dim alt_pn As String = ""
        Dim oem_mfr As String = ""
        Dim oem_pn As String = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_parts where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            primary_mfr = reader.Item("manufacturer").ToString
            pn = reader.Item("partnumber").ToString
            primary_pn = Trim(Replace(pn, "-", ""))
            alt_mfr = reader.Item("alt_manufacturer").ToString
            pn = reader.Item("alt_partnumber").ToString
            alt_pn = Trim(Replace(pn, "-", ""))
            oem_mfr = reader.Item("oem_manufacturer").ToString
            pn = reader.Item("oem_partnumber").ToString
            oem_pn = Trim(Replace(pn, "-", ""))
            If primary_mfr <> manufacturer Then
                If oem_mfr <> "" And oem_pn <> "" Then
                    results_mfr = manufacturer
                    results_pn = GetCrossReference(oem_mfr, oem_pn, manufacturer)
                Else
                    If primary_mfr <> "" And primary_pn <> "" Then
                        results_mfr = manufacturer
                        results_pn = GetCrossReference(primary_mfr, primary_mfr, manufacturer)
                    Else
                        If alt_mfr <> "" And alt_pn <> "" Then
                            results_mfr = manufacturer
                            results_pn = GetCrossReference(alt_mfr, alt_pn, manufacturer)
                        End If
                    End If
                End If
                If primary_mfr <> "" And primary_pn <> "" Then
                    alt_mfr = primary_mfr
                    alt_pn = primary_pn
                Else
                    alt_mfr = ""
                    alt_pn = ""
                End If
                If results_mfr <> "" And results_pn <> "" Then
                    UpdatePart(results_mfr, results_pn, alt_mfr, alt_pn, reader.Item("description"), reader.Item("partID"), reader.Item("companyID"))
                End If
            End If
        End While
        reader.Close()
        conn.Close()
    End Sub

    Public Shared Sub ConvertParts(ByVal manufacturer As String, ByVal equipmentID As Integer)
        Dim pn As String = ""
        Dim results_mfr As String = ""
        Dim results_pn As String = ""
        Dim primary_mfr As String = ""
        Dim primary_pn As String = ""
        Dim alt_mfr As String = ""
        Dim alt_pn As String = ""
        Dim oem_mfr As String = ""
        Dim oem_pn As String = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_parts where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            primary_mfr = reader.Item("manufacturer").ToString
            pn = reader.Item("partnumber").ToString
            primary_pn = Trim(Replace(pn, "-", ""))
            alt_mfr = reader.Item("alt_manufacturer").ToString
            pn = reader.Item("alt_partnumber").ToString
            alt_pn = Trim(Replace(pn, "-", ""))
            oem_mfr = reader.Item("oem_manufacturer").ToString
            pn = reader.Item("oem_partnumber").ToString
            oem_pn = Trim(Replace(pn, "-", ""))
            If primary_mfr <> manufacturer Then
                'if there is an oem part listed, cross reference that
                If oem_mfr <> "" And oem_pn <> "" Then
                    results_mfr = manufacturer
                    results_pn = GetCrossReference(oem_mfr, oem_pn, manufacturer)
                Else
                    'else cross reference the primary part number
                    If primary_mfr <> "" And primary_pn <> "" Then
                        results_mfr = manufacturer
                        results_pn = GetCrossReference(primary_mfr, primary_pn, manufacturer)
                    Else
                        'or if primary is empty cross alternate....this will probably never happen
                        If alt_mfr <> "" And alt_pn <> "" Then
                            results_mfr = manufacturer
                            results_pn = GetCrossReference(alt_mfr, alt_pn, manufacturer)
                        End If
                    End If
                End If
                'swap primary to alternate

                If primary_mfr <> "" And primary_pn <> "" Then
                    alt_mfr = primary_mfr
                    alt_pn = primary_pn
                Else
                    alt_mfr = ""
                    alt_pn = ""
                End If
                'only update part if there was a cross reference found
                If results_mfr <> "" And results_pn <> "" Then
                    UpdatePart(results_mfr, results_pn, alt_mfr, alt_pn, reader.Item("description"), reader.Item("partID"), reader.Item("companyID"))
                End If
            End If
        End While
        reader.Close()
        conn.Close()
    End Sub

    Public Shared Function GetLuberfinerPN1(ByVal partnumber As String) As String
        GetLuberfinerPN1 = ""
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_luberfiner_xref where partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetLuberfinerPN1 = reader.Item("luberfiner_pn").ToString
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetLuberfinerPN(ByVal manufacturer As String, ByVal partnumber As String) As String
        GetLuberfinerPN = ""
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_luberfiner_xref where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetLuberfinerPN = reader.Item("luberfiner_pn").ToString
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetCrossReference(ByVal manufacturer As String, ByVal partnumber As String, ByVal new_manufacturer As String) As String
        GetCrossReference = ""
        Dim luberfiner_pn As String = GetLuberfinerPN(manufacturer, partnumber)
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_luberfiner_xref where luberfiner_pn=@luberfiner_pn and manufacturer=@manufacturer"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", new_manufacturer)
        comm.Parameters.AddWithValue("@luberfiner_pn", luberfiner_pn)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetCrossReference = reader.Item("partnumber").ToString
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Sub UpdatePart(ByVal manufacturer As String, ByVal partnumber As String, ByVal alt_manufacturer As String, ByVal alt_partnumber As String, ByVal description As String, ByVal partID As Integer, ByVal companyID As Integer)
        Dim this_description As String = GetItemName(manufacturer, partnumber)
        If this_description <> "" Then
            description = this_description
        End If
        Dim price As Double = GetCompanyPrice(companyID, manufacturer, partnumber)
        Dim conn As New SqlConnection(appcode.ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_parts set manufacturer=@manufacturer,partnumber=@partnumber,description=@description,price=@price where partID=@partID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@alt_manufacturer", alt_manufacturer)
        comm.Parameters.AddWithValue("@alt_partnumber", alt_partnumber)
        comm.Parameters.AddWithValue("@description", description)
        comm.Parameters.AddWithValue("@price", price)
        comm.Parameters.AddWithValue("@partID", partID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Function GetCategoryName(ByVal categoryID As String) As String
        GetCategoryName = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT category FROM t_category WHERE categoryID=@categoryID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@categoryID", categoryID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetCategoryName = reader.Item("category")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetUsage(ByVal manufacturer As String, ByVal partnumber As String, ByVal companyID As Integer, ByVal orderyear As Integer) As Integer
        GetUsage = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT sum(quantity) as quantity FROM [v_order_lines] WHERE companyID=@companyID and manufacturer=@manufacturer and partnumber=@partnumber and YEAR(order_date)=@orderyear"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@orderyear", orderyear)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("quantity").ToString <> "" Then
                GetUsage = reader.Item("quantity")
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetFrequency(ByVal manufacturer As String, ByVal partnumber As String, ByVal companyID As Integer) As Integer
        GetFrequency = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT count(partnumber) as numlines FROM v_part_summary WHERE manufacturer=@manufacturer and partnumber=@partnumber and companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetFrequency = reader.Item("numlines")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetMfrItemsToOrder(ByVal manufacturer As String) As Integer
        GetMfrItemsToOrder = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT * FROM t_product where nStock='True' and manufacturer=@manufacturer"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            Dim onpo As Double = GetOnPOAmount(manufacturer, reader.Item("partnumber"))
            If reader.Item("onhand") + onpo < reader.Item("min") Then
                GetMfrItemsToOrder += 1
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetInventoryResaleValue() As Integer
        GetInventoryResaleValue = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT sum(onhand*saleprice) as value FROM t_product"
        Dim comm As New SqlCommand(commandString, conn)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetInventoryResaleValue = reader.Item("value")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetInventoryValue() As Integer
        GetInventoryValue = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT sum(onhand*cost) as value FROM t_product"
        Dim comm As New SqlCommand(commandString, conn)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetInventoryValue = reader.Item("value")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetInventoryCount(ByVal companyID As Integer) As Integer
        GetInventoryCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "SELECT warehouseID FROM t_warehouse where nStock='True' and companyID=@companyID"
        Else
            commandString = "SELECT productID FROM t_product where nStock='True'"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetInventoryCount += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetReturnsCount(ByVal companyID As Integer) As Integer
        GetReturnsCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "SELECT orderID FROM t_order where isReturn='Yes' and confirmed='True' and complete='False' and companyID=@companyID"
        Else
            commandString = "SELECT orderID FROM t_order where isReturn='Yes' and confirmed='True' and complete='False'"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetReturnsCount += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetNewOrdersCount(ByVal companyID As Integer) As Integer
        GetNewOrdersCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "SELECT orderID FROM t_order where confirmed='False' and isReturn='No' and companyID=@companyID"
        Else
            commandString = "SELECT orderID FROM t_order where confirmed='False' and isReturn='No'"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetNewOrdersCount += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetQuotesCount(ByVal companyID As Integer) As Integer
        GetQuotesCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "SELECT * FROM t_quote WHERE complete=@complete and DATEADD(day,30,response_date) > getdate() and companyID=@companyID ORDER BY quoteID"
        Else
            commandString = "SELECT * FROM t_quote WHERE complete=@complete and DATEADD(day,30,response_date) > getdate() ORDER BY quoteID"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        comm.Parameters.AddWithValue("@complete", True)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetQuotesCount += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetBillingCount(ByVal companyID As Integer) As Integer
        GetBillingCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "SELECT shipmentID FROM v_shipments WHERE shipped=@shipped and invoiced=@invoiced and qbo=@qbo and companyID=@companyID ORDER BY shipmentID"
        Else
            commandString = "SELECT shipmentID FROM v_shipments WHERE shipped=@shipped and invoiced=@invoiced and qbo=@qbo ORDER BY shipmentID"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        comm.Parameters.AddWithValue("@shipped", True)
        comm.Parameters.AddWithValue("@Invoiced", True)
        comm.Parameters.AddWithValue("@qbo", False)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetBillingCount += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetInvoicesCount(ByVal companyID As Integer) As Integer
        GetInvoicesCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "SELECT shipmentID FROM v_shipments WHERE shipped=@shipped and invoiced=@invoiced and companyID=@companyID ORDER BY shipmentID"
        Else
            commandString = "SELECT shipmentID FROM v_shipments WHERE shipped=@shipped and invoiced=@invoiced ORDER BY shipmentID"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        comm.Parameters.AddWithValue("@shipped", True)
        comm.Parameters.AddWithValue("@Invoiced", False)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetInvoicesCount += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetShipmentsCount(ByVal companyID As Integer) As Integer
        GetShipmentsCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "SELECT shipmentID FROM v_shipments WHERE shipped = @shipped and companyID = @companyID AND order_date>= dateadd(day,-30,getdate())"
        Else
            commandString = "SELECT shipmentID FROM v_shipments WHERE shipped=@shipped ORDER BY shipmentID"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        comm.Parameters.AddWithValue("@shipped", True)
        comm.Parameters.AddWithValue("@Invoiced", False)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetShipmentsCount += 1
        End While
        reader.Close()
        conn.Close()
    End Function


    Public Shared Function GetInvoices(ByVal companyID As Integer, ByVal omonth As Integer, ByVal oyear As Integer) As Double
        GetInvoices = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            If omonth <> 0 Then
                commandString = "SELECT sum(quantity*price) as invoices FROM v_shipment_lines WHERE qbo=@qbo and companyID=@companyID and MONTH(invoice_date)=@omonth and YEAR(invoice_date)=@oyear"
            Else
                commandString = "SELECT sum(quantity*price) as invoices FROM v_shipment_lines WHERE qbo=@qbo and companyID=@companyID and YEAR(invoice_date)=@oyear"
            End If
        Else
            If omonth <> 0 Then
                commandString = "SELECT sum(quantity*price) as invoices FROM v_shipment_lines WHERE qbo=@qbo and MONTH(invoice_date)=@omonth and YEAR(invoice_date)=@oyear"
            Else
                commandString = "SELECT sum(quantity*price) as invoices FROM v_shipment_lines WHERE qbo=@qbo and YEAR(invoice_date)=@oyear"
            End If
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
            If omonth <> 0 Then
                comm.Parameters.AddWithValue("@omonth", omonth)
            End If
        Else
            If omonth <> 0 Then
                comm.Parameters.AddWithValue("@omonth", omonth)
            End If
        End If
        comm.Parameters.AddWithValue("@oyear", oyear)
        comm.Parameters.AddWithValue("@qbo", True)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("invoices").ToString <> "" Then
                GetInvoices = reader.Item("invoices")
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetInvoiceCount(ByVal companyID As Integer, ByVal omonth As Integer, ByVal oyear As Integer) As Double
        GetInvoiceCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            If omonth <> 0 Then
                commandString = "SELECT shipmentID FROM v_shipments WHERE qbo=@qbo and companyID=@companyID and MONTH(invoice_date)=@omonth and YEAR(invoice_date)=@oyear ORDER BY shipmentID"
            Else
                commandString = "SELECT shipmentID FROM v_shipments WHERE qbo=@qbo and companyID=@companyID and YEAR(invoice_date)=@oyear ORDER BY shipmentID"
            End If
        Else
            If omonth <> 0 Then
                commandString = "SELECT shipmentID FROM v_shipments WHERE qbo=@qbo and MONTH(invoice_date)=@omonth and YEAR(invoice_date)=@oyear ORDER BY shipmentID"
            Else
                commandString = "SELECT shipmentID FROM v_shipments WHERE qbo=@qbo and YEAR(invoice_date)=@oyear ORDER BY shipmentID"
            End If
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
            If omonth <> 0 Then
                comm.Parameters.AddWithValue("@omonth", omonth)
            End If
        Else
            If omonth <> 0 Then
                comm.Parameters.AddWithValue("@omonth", omonth)
            End If
        End If
        comm.Parameters.AddWithValue("@oyear", oyear)
        comm.Parameters.AddWithValue("@qbo", True)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetInvoiceCount += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetShipmentCount(ByVal companyID As Integer, ByVal omonth As Integer, ByVal oyear As Integer) As Double
        GetShipmentCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            If omonth <> 0 Then
                commandString = "SELECT shipmentID FROM v_shipments WHERE shipped=@shipped and companyID=@companyID and MONTH(shipment_date)=@omonth and YEAR(shipment_date)=@oyear ORDER BY shipmentID"
            Else
                commandString = "SELECT shipmentID FROM v_shipments WHERE shipped=@shipped and companyID=@companyID and YEAR(shipment_date)=@oyear ORDER BY shipmentID"
            End If
        Else
            If omonth <> 0 Then
                commandString = "SELECT shipmentID FROM v_shipments WHERE shipped=@shipped and MONTH(shipment_date)=@omonth and YEAR(shipment_date)=@oyear ORDER BY shipmentID"
            Else
                commandString = "SELECT shipmentID FROM v_shipments WHERE shipped=@shipped and YEAR(shipment_date)=@oyear ORDER BY shipmentID"
            End If
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
            If omonth <> 0 Then
                comm.Parameters.AddWithValue("@omonth", omonth)
            End If
        Else
            If omonth <> 0 Then
                comm.Parameters.AddWithValue("@omonth", omonth)
            End If
        End If
        comm.Parameters.AddWithValue("@oyear", oyear)
        comm.Parameters.AddWithValue("@shipped", True)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetShipmentCount += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetReadyToPickCount(ByVal companyID As Integer) As Integer
        GetReadyToPickCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "SELECT orderID FROM t_order where confirmed='True' and complete='False' and companyID=@companyID"
        Else
            commandString = "SELECT orderID FROM t_order where confirmed='True' and complete='False'"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If isOrderRTS(reader.Item("orderID")) = True Then
                GetReadyToPickCount += 1
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetBackOrdersCount(ByVal companyID As Integer) As Integer
        GetBackOrdersCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "SELECT orderID FROM t_order where confirmed='True' and complete='False' and companyID=@companyID"
        Else
            commandString = "SELECT orderID FROM t_order where confirmed='True' and complete='False'"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If IsBO(reader.Item("orderID")) = True Then
                GetBackOrdersCount += 1
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOpenOrdersCount(ByVal companyID As Integer) As Integer
        GetOpenOrdersCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If companyID <> 0 Then
            commandString = "SELECT count(orderID) as ordercount FROM t_order where confirmed='True' and complete='False' and companyID=@companyID"
        Else
            commandString = "SELECT count(orderID) as ordercount FROM t_order where confirmed='True' and complete='False'"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If companyID <> 0 Then
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetOpenOrdersCount = reader.Item("ordercount")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetRequisitionsCount(ByVal manufacturer As String) As Integer
        GetRequisitionsCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If manufacturer <> "" Then
            commandString = "SELECT manufacturer,partnumber,sum(quantity) as quantity FROM [v_requisitions] WHERE manufacturer=@manufacturer and isReturn='No' GROUP BY manufacturer,partnumber ORDER BY manufacturer,partnumber"
        Else
            commandString = "SELECT manufacturer,partnumber,sum(quantity) as quantity FROM [v_requisitions] WHERE isReturn='No' GROUP BY manufacturer,partnumber ORDER BY manufacturer,partnumber"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If manufacturer <> "" Then
            comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        End If
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetRequisitionsCount += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOpenLineQuantity(ByVal lineID As Integer) As Double
        GetOpenLineQuantity = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_order_lines where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("ship_qty").ToString <> "" Then
                GetOpenLineQuantity = reader.Item("quantity") - reader.Item("ship_qty")
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOpenQuantity(ByVal required As Double, ByVal onhand As Double, onpo As Double) As Double
        GetOpenQuantity = 0
        If onhand + onpo < required Then
            GetOpenQuantity = required - (onhand + onpo)
        End If
    End Function

    Public Shared Function GetNeededQuantity(ByVal required As Double, ByVal onhand As Double) As Double
        GetNeededQuantity = 0
        If onhand < required Then
            GetNeededQuantity = required - onhand
        End If
    End Function

    Public Shared Function GetNeededRequisitionsCount(ByVal manufacturer As String) As Integer
        GetNeededRequisitionsCount = 0
        Dim onpo As Double = 0
        Dim onhand As Double = 0
        Dim need As Double = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        If manufacturer <> "" Then
            commandString = "SELECT manufacturer,partnumber,sum(quantity) as quantity FROM [v_requisitions] WHERE manufacturer=@manufacturer and isReturn='No' GROUP BY manufacturer,partnumber ORDER BY manufacturer,partnumber"
        Else
            commandString = "SELECT manufacturer,partnumber,sum(quantity) as quantity FROM [v_requisitions] WHERE isReturn='No' GROUP BY manufacturer,partnumber ORDER BY manufacturer,partnumber"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If manufacturer <> "" Then
            comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        End If
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            onhand = GetOnHand(manufacturer, reader.Item("partnumber"))
            need = GetNeedOnHand(manufacturer, reader.Item("partnumber"))
            If need > 0 Then
                GetNeededRequisitionsCount += 1
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetAllOpenRequisitionsCount() As Integer
        GetAllOpenRequisitionsCount = 0
        Dim onpo As Double = 0
        Dim onhand As Double = 0
        Dim available As Double = 0
        Dim need As Double = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT manufacturer,partnumber,sum(quantity) as quantity FROM [v_requisitions] WHERE isReturn='No' GROUP BY manufacturer,partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            onpo = GetOnPO(reader.Item("manufacturer"), reader.Item("partnumber"))
            onhand = GetOnHand(reader.Item("manufacturer"), reader.Item("partnumber"))
            available = onpo + onhand
            If reader.Item("quantity") > available Then
                GetAllOpenRequisitionsCount += 1
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetOpenRequisitionsCount(ByVal manufacturer As String) As Integer
        GetOpenRequisitionsCount = 0
        Dim onpo As Double = 0
        Dim onhand As Double = 0
        Dim available As Double = 0
        Dim need As Double = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT manufacturer,partnumber,sum(quantity) as quantity FROM [v_requisitions] WHERE manufacturer=@manufacturer GROUP BY manufacturer,partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        If manufacturer <> "" Then
            comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        End If
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            onpo = GetOnPOAmount(manufacturer, reader.Item("partnumber"))
            onhand = GetOnHand(manufacturer, reader.Item("partnumber"))
            available = onpo + onhand
            If reader.Item("quantity") > available Then
                GetOpenRequisitionsCount += 1
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetParentID(ByVal categoryID As Integer) As Integer
        GetParentID = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT parentID from t_category where categoryID=@categoryID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@categoryID", categoryID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetParentID = reader.Item("parentID")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetWarehouseValue(ByVal companyID As Integer) As Integer
        GetWarehouseValue = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT * from t_warehouse where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetWarehouseValue += reader.Item("onhand") * GetCompanyPrice(companyID, reader.Item("manufacturer").ToString, reader.Item("partnumber").ToString)
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetWarehouseCount(ByVal companyID As Integer) As Integer
        GetWarehouseCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT count(warehouseID) as stockcount from t_warehouse where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetWarehouseCount = reader.Item("stockcount")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetPartsCount(ByVal companyID As Integer) As Integer
        GetPartsCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT manufacturer,partnumber,alt_partnumber,description,count(partnumber) as numlines FROM v_part_summary WHERE companyID=@companyID GROUP BY manufacturer,partnumber,alt_partnumber,description ORDER BY [manufacturer], [partnumber]"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetPartsCount += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetKitName(ByVal serviceprofileID As Integer) As String
        GetKitName = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT name FROM [t_serviceprofile] WHERE ([serviceprofileID] = @serviceprofileID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetKitName = reader.Item("name")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetEqupmentEmail(ByVal companyID As Integer) As String
        GetEqupmentEmail = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT equipment_email FROM [t_company] WHERE ([companyID] = @companyID)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetEqupmentEmail = reader.Item("equipment_email")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetKitCount(ByVal companyID As Integer) As Integer
        GetKitCount = 0
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT kitcode, interval, interval_type FROM [v_serviceprofile] WHERE ([companyID] = @companyID) GROUP BY kitcode, interval, interval_type ORDER BY interval"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            GetKitCount += 1
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function GetPriPartNumber(ByVal partnumber2 As String) As String
        GetPriPartNumber = ""
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select partnumber1 from t_product_xref where  (partnumber2 = @partnumber2)"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@partnumber2", partnumber2)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            GetPriPartNumber = reader.Item("partnumber1")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function isCore(ByVal partnumber As String) As Boolean
        isCore = False
        If Left(partnumber, 4) = "CORE" Then
            isCore = True
        End If
    End Function

    Public Shared Function isCustomer(ByVal companyID As Integer) As Boolean
        isCustomer = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            isCustomer = reader.Item("customer")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function isSendKitStatus(ByVal companyID As Integer) As Boolean
        isSendKitStatus = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select sendkitstatus from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            isSendKitStatus = reader.Item("sendkitstatus")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsActive(ByVal companyID As Integer) As Boolean
        IsActive = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select active from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsActive = reader.Item("active")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsUsernameAvailable(ByVal username As String, ByVal userID As Integer) As Boolean
        IsUsernameAvailable = True
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select userID from t_user where username=@username and userID<>@userID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@username", username)
        comm.Parameters.AddWithValue("@userID", userID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsUsernameAvailable = False
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsEmailAvailable(ByVal email As String, ByVal userID As String) As Boolean
        IsEmailAvailable = True
        If userID = "" Then userID = "0"
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select userID from t_user where email=@email and userID<>@userID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@email", email)
        comm.Parameters.AddWithValue("@userID", userID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If userID <> reader.Item("userID") Then
                IsEmailAvailable = False
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsGuestCart(ByVal ssID As String) As Boolean
        IsGuestCart = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_cart where ssID=@ssID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@ssID", ssID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsGuestCart = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsServiceProfilePartSelected(ByVal serviceprofileID As Integer, ByVal partID As Integer) As Boolean
        IsServiceProfilePartSelected = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select selected from t_serviceparts where serviceprofileID=@serviceprofileID and partID=@partID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.Parameters.AddWithValue("@partID", partID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsServiceProfilePartSelected = reader.Item("selected")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsLastUser(ByVal companyID As Integer) As Boolean
        IsLastUser = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select count(userID) as usercount from t_user where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("usercount") < 2 Then
                IsLastUser = True
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsLocation(ByVal companyID As Integer, ByVal shipto As String) As Boolean
        IsLocation = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_ship where companyID=@companyID and shipto=@shipto"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@shipto", shipto)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsLocation = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsLocationID(ByVal companyID As Integer, ByVal shipID As Integer) As Boolean
        IsLocationID = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_ship where companyID=@companyID and shipID=@shipID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@shipID", shipID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsLocationID = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsOrderComplete(ByVal orderID As String) As Boolean
        IsOrderComplete = True
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order_line where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If GetPickQty(reader.Item("lineID")) < reader.Item("quantity") Then
                IsOrderComplete = False
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsPOLineOpen(ByVal lineID As Integer) As Boolean
        IsPOLineOpen = True
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_poline where polineID=@polineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If reader.Item("received") >= reader.Item("quantity") Then
                IsPOLineOpen = False
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsPO(ByVal po As String) As Boolean
        IsPO = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_po where po=@po or poID=@po"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@po", po)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsPO = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsInvoiceID(ByVal shipmentID As Integer) As Boolean
        IsInvoiceID = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_shipment where shipmentID=@shipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsInvoiceID = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsOrderID(ByVal orderID As Integer) As Boolean
        IsOrderID = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsOrderID = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsOrderLineOpen(ByVal lineID As Integer) As Boolean
        IsOrderLineOpen = True
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order_line where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            If GetPickQty(lineID) >= reader.Item("quantity") Then
                IsOrderLineOpen = False
            End If
        End While
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsPartnumber(ByVal manufacturer As String, ByVal partnumber As String) As Boolean
        IsPartnumber = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsPartnumber = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsPOSubmitted(ByVal poID As Integer) As Boolean
        IsPOSubmitted = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select submitted from t_po where poID=@poID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@poID", poID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsPOSubmitted = reader.Item("submitted")
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsNotPOLineComplete(ByVal poID As Integer, ByVal manufacturer As String, ByVal partnumber As String) As Boolean
        IsNotPOLineComplete = True
        Dim received As Double = GetReceivedPOLineQuantity(poID, manufacturer, partnumber)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select sum(quantity) as quantity from t_poline where poID=@poID and manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@poID", poID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If received >= reader.Item("quantity") Or IsPOSubmitted(poID) = False Then
                IsNotPOLineComplete = False
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsPrimaryProduct(ByVal manufacturer As String, ByVal partnumber As String) As Boolean
        IsPrimaryProduct = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_product where companyID=@companyID and partnumber=@partnumber and manufacturer=@manufacturer"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", 0)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsPrimaryProduct = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsReqOnPO(ByVal lineID As Integer) As Boolean
        IsReqOnPO = True
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_requisition where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            If reader.Item("onPO") = True Then
                IsReqOnPO = False
            End If
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsUserCompany(ByVal userID As Integer, ByVal companyID As Integer) As Boolean
        IsUserCompany = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_user_company where userID=@userID and companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@userID", userID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsUserCompany = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsUserID(ByVal companyID As Integer, ByVal userID As Integer) As Boolean
        IsUserID = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_user where companyID=@companyID and userID=@userID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@userID", userID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsUserID = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Function IsUserLocation(ByVal userID As Integer, ByVal locationID As Integer) As Boolean
        IsUserLocation = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_user_location where userID=@userID and locationID=@locationID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@locationID", locationID)
        comm.Parameters.AddWithValue("@userID", userID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            IsUserLocation = True
        End If
        reader.Close()
        conn.Close()
    End Function

    Public Shared Sub UpdateReviewed(ByVal orderID As Integer, ByVal reviewed As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_order set reviewed=@reviewed where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.Parameters.AddWithValue("@reviewed", reviewed)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateProductCategory(ByVal productID As Integer, ByVal categoryID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_product set category=@category where productID=@productID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@productID", productID)
        comm.Parameters.AddWithValue("@category", categoryID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateProductCategoryID(ByVal categoryID As Integer, ByVal new_categoryID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_product set category=@new_category where category=@category"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@category", categoryID)
        comm.Parameters.AddWithValue("@new_category", new_categoryID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateBranchID(ByVal companyID As Integer, ByVal branchID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_company set branchID=@branchID where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@branchID", branchID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateCartItem(ByVal cartID As Integer, ByVal quantity As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_cart set quantity=@quantity where cartID=@cartID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.Parameters.AddWithValue("@cartID", cartID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateBillingEmail(ByVal companyID As Integer, ByVal billing_email As String)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "update t_company set billing_email=@billing_email where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@billing_email", billing_email)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateCompany(ByVal companyID As Integer, ByVal company As String, ByVal address1 As String, ByVal address2 As String, ByVal city As String, ByVal state As String, ByVal zipcode As String, ByVal c_phone As String, ByVal c_fax As String, ByVal logo As String, ByVal labor_rate As Double, ByVal salestax As Double, ByVal chargetax As Boolean, ByVal equipment_email As String, ByVal contact_email As String, ByVal billing_email As String, ByVal warehouse_email As String, ByVal repID As Integer, ByVal sendkitstatus As Boolean)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "update t_company set company=@company,address1=@address1,address2=@address2,city=@city,state=@state,zipcode=@zipcode,c_phone=@c_phone,c_fax=@c_fax,logo=@logo,labor_rate=@labor_rate,salestax=@salestax,chargetax=@chargetax,equipment_email=@equipment_email,contact_email=@contact_email,billing_email=@billing_email,warehouse_email=@warehouse_email,repID=@repID,sendkitstatus=@sendkitstatus where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@company", company)
        comm.Parameters.AddWithValue("@address1", address1)
        comm.Parameters.AddWithValue("@address2", address2)
        comm.Parameters.AddWithValue("@city", city)
        comm.Parameters.AddWithValue("@state", state)
        comm.Parameters.AddWithValue("@zipcode", zipcode)
        comm.Parameters.AddWithValue("@c_phone", c_phone)
        comm.Parameters.AddWithValue("@c_fax", c_fax)
        comm.Parameters.AddWithValue("@logo", logo)
        comm.Parameters.AddWithValue("@labor_rate", labor_rate)
        comm.Parameters.AddWithValue("@salestax", salestax)
        comm.Parameters.AddWithValue("@chargetax", chargetax)
        comm.Parameters.AddWithValue("@sendkitstatus", sendkitstatus)
        comm.Parameters.AddWithValue("@equipment_email", equipment_email)
        comm.Parameters.AddWithValue("@contact_email", contact_email)
        comm.Parameters.AddWithValue("@billing_email", billing_email)
        comm.Parameters.AddWithValue("@warehouse_email", warehouse_email)
        comm.Parameters.AddWithValue("@repID", repID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateEquipment(ByVal equipmentID As Integer, ByVal assetID As String, ByVal equipment_oem As String, ByVal equipment_model As String, ByVal equipment_description As String, ByVal equipment_options As String, ByVal equipment_year As String, ByVal equipment_vin As String, ByVal engine_oem As String, ByVal engine_model As String, ByVal notes As String, ByVal verified As Boolean, ByVal locationID As Integer, ByVal hours_miles As String, ByVal interval_type As String, ByVal fuel_type As String, ByVal def As String, ByVal interval_root As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_equipment set assetID=@assetID,equipment_oem=@equipment_oem,equipment_model=@equipment_model,equipment_description=@equipment_description,equipment_options=@equipment_options,equipment_year=@equipment_year,equipment_vin=@equipment_vin,engine_oem=@engine_oem,engine_model=@engine_model,notes=@notes,verified=@verified,locationID=@locationID,hours_miles=@hours_miles,interval_type=@interval_type,fuel_type=@fuel_type,def=@def,interval_root=@interval_root where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.Parameters.AddWithValue("@assetID", assetID)
        comm.Parameters.AddWithValue("@equipment_oem", equipment_oem)
        comm.Parameters.AddWithValue("@equipment_model", equipment_model)
        comm.Parameters.AddWithValue("@equipment_description", equipment_description)
        comm.Parameters.AddWithValue("@equipment_options", equipment_options)
        comm.Parameters.AddWithValue("@equipment_year", equipment_year)
        comm.Parameters.AddWithValue("@equipment_vin", equipment_vin)
        comm.Parameters.AddWithValue("@engine_oem", engine_oem)
        comm.Parameters.AddWithValue("@engine_model", engine_model)
        comm.Parameters.AddWithValue("@notes", notes)
        comm.Parameters.AddWithValue("@verified", verified)
        comm.Parameters.AddWithValue("@locationID", locationID)
        comm.Parameters.AddWithValue("@hours_miles", hours_miles)
        comm.Parameters.AddWithValue("@interval_type", interval_type)
        comm.Parameters.AddWithValue("@fuel_type", fuel_type)
        comm.Parameters.AddWithValue("@def", def)
        comm.Parameters.AddWithValue("@interval_root", interval_root)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateEquipmentOpHours(ByVal equipmentID As Integer, ByVal ophours As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_equipment set ophours=@ophours where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.Parameters.AddWithValue("@ophours", ophours)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateEquipmentDescription(ByVal equipmentID As Integer, ByVal equipment_description As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_equipment set equipment_description=@equipment_description where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.Parameters.AddWithValue("@equipment_description", equipment_description)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateEquipmentLocation(ByVal equipmentID As Integer, ByVal locationID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_equipment set locationID=@locationID where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.Parameters.AddWithValue("@locationID", locationID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateCartHoursMiles(ByVal userID As Integer, ByVal companyID As Integer, ByVal hours_miles As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_cart set hours_miles=@hours_miles where userID=@userID and companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@hours_miles", hours_miles)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateHoursMiles(ByVal equipmentID As Integer, ByVal hours_miles As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_equipment set hours_miles=@hours_miles where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.Parameters.AddWithValue("@hours_miles", hours_miles)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateInvoice(ByVal shipmentID As Integer, ByVal invoiceID As String, ByVal invoice_date As String, ByVal sales_tax As Double, ByVal invoiced As Boolean, ByVal surcharge As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_shipment set invoiceID=@invoiceID,invoice_date=@invoice_date,sales_tax=@sales_tax,invoiced=@invoiced,surcharge=@surcharge where shipmentID=@shipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        comm.Parameters.AddWithValue("@invoiceID", invoiceID)
        comm.Parameters.AddWithValue("@invoice_date", invoice_date)
        comm.Parameters.AddWithValue("@sales_tax", sales_tax)
        comm.Parameters.AddWithValue("@invoiced", invoiced)
        comm.Parameters.AddWithValue("@surcharge", surcharge)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateIntervalType(ByVal interval_type As String, ByVal equipmentID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_equipment set interval_type=@interval_type where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@interval_type", interval_type)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateKitCode(ByVal kitcode As String, ByVal serviceprofileID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_serviceprofile set kitcode=@kitcode where serviceprofileID=@serviceprofileID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@kitcode", kitcode)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateLocation(ByVal shipID As Integer, ByVal shipto As String, ByVal s_address1 As String, ByVal s_address2 As String, ByVal s_address3 As String, ByVal s_city As String, ByVal s_state As String, ByVal s_zipcode As String)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "update t_ship set shipto=@shipto,s_address1=@s_address1,s_address2=@s_address2,s_address3=@s_address3,s_city=@s_city,s_state=@s_state,s_zipcode=@s_zipcode where shipID=@shipID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipID", shipID)
        comm.Parameters.AddWithValue("@shipto", shipto)
        comm.Parameters.AddWithValue("@s_address1", s_address1)
        comm.Parameters.AddWithValue("@s_address2", s_address2)
        comm.Parameters.AddWithValue("@s_address3", s_address3)
        comm.Parameters.AddWithValue("@s_city", s_city)
        comm.Parameters.AddWithValue("@s_state", s_state)
        comm.Parameters.AddWithValue("@s_zipcode", s_zipcode)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateNormalStock(ByVal manufacturer As String, ByVal partnumber As String, ByVal nStock As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_product set nStock=@nStock where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@nStock", nStock)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateMin(ByVal manufacturer As String, ByVal partnumber As String, ByVal min As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_product set min=@min where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@min", min)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateMax(ByVal manufacturer As String, ByVal partnumber As String, ByVal max As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_product set max=@max where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@max", max)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateInventory(ByVal manufacturer As String, ByVal partnumber As String, ByVal onhand As Double, ByVal min As Double, ByVal max As Double, ByVal nStock As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_product set onhand=@onhand,max=@max,min=@min,last_updated=@last_updated,nStock=@nStock where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@nStock", nStock)
        comm.Parameters.AddWithValue("@onhand", onhand)
        comm.Parameters.AddWithValue("@max", max)
        comm.Parameters.AddWithValue("@min", min)
        comm.Parameters.AddWithValue("@last_updated", Now())
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateOnHand(ByVal manufacturer As String, ByVal partnumber As String, ByVal onhand As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_product set onhand=@onhand,last_updated=@last_updated where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@onhand", onhand)
        comm.Parameters.AddWithValue("@last_updated", Now())
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateLastLogin(ByVal mobileuserID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_mobileuser set last_login=getDate() where mobileuserID=@mobileuserID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@mobileuserID", mobileuserID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateMobileOrders(ByVal mobileuserID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_mobileuser set orders=orders+1 where mobileuserID=@mobileuserID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@mobileuserID", mobileuserID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateCorrections(ByVal mobileuserID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_mobileuser set corrections=corrections+1 where mobileuserID=@mobileuserID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@mobileuserID", mobileuserID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateStatusUpdates(ByVal mobileuserID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_mobileuser set updates=updates+1 where mobileuserID=@mobileuserID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@mobileuserID", mobileuserID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateLogins(ByVal mobileuserID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_mobileuser set logins=logins+1 where mobileuserID=@mobileuserID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@mobileuserID", mobileuserID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateEquipmentViews(ByVal mobileuserID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_mobileuser set equipment_views=equipment_views+1 where mobileuserID=@mobileuserID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@mobileuserID", mobileuserID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateMobileUserID(ByVal mobileuserID As Integer, ByVal orderID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_order set mobileuserID=@mobileuserID where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@mobileuserID", mobileuserID)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateOrderKitHours(ByVal hours_miles As String, ByVal orderID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_order set hours_miles=@hours_miles where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@hours_miles", hours_miles)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateOrder(ByVal orderID As Integer, ByVal purchaseorder As String, ByVal deliverby_date As String, ByVal confirm_date As String, ByVal confirmed As Boolean, ByVal placed As Boolean, ByVal placedby As String, ByVal shipID As Integer, ByVal shipto As String, ByVal s_address1 As String, ByVal s_address2 As String, ByVal s_address3 As String, ByVal s_city As String, ByVal s_state As String, ByVal s_zipcode As String, ByVal userID As Integer, ByVal c_contact As String, ByVal c_phone As String, ByVal c_fax As String, ByVal notes As String, ByVal ship_method As String, ByVal ship_options As String, ByVal is_kit As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_order set purchaseorder=@purchaseorder,deliverby_date=@deliverby_date,confirm_date=@confirm_date,confirmed=@confirmed,placed=@placed,placedby=@placedby,shipID=@shipID,shipto=@shipto,s_address1=@s_address1,s_address2=@s_address2,s_address3=@s_address3,s_city=@s_city,s_state=@s_state,s_zipcode=@s_zipcode,userID=@userID,c_contact=@c_contact,c_phone=@c_phone,c_fax=@c_fax,notes=@notes,ship_method=@ship_method,ship_options=@ship_options,is_kit=@is_kit where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.Parameters.AddWithValue("@purchaseorder", purchaseorder)
        comm.Parameters.AddWithValue("@deliverby_date", deliverby_date)
        comm.Parameters.AddWithValue("@confirm_date", confirm_date)
        comm.Parameters.AddWithValue("@confirmed", confirmed)
        comm.Parameters.AddWithValue("@placed", placed)
        comm.Parameters.AddWithValue("@placedby", placedby)
        comm.Parameters.AddWithValue("@shipID", shipID)
        comm.Parameters.AddWithValue("@shipto", shipto)
        comm.Parameters.AddWithValue("@s_address1", s_address1)
        comm.Parameters.AddWithValue("@s_address2", s_address2)
        comm.Parameters.AddWithValue("@s_address3", s_address3)
        comm.Parameters.AddWithValue("@s_city", s_city)
        comm.Parameters.AddWithValue("@s_state", s_state)
        comm.Parameters.AddWithValue("@s_zipcode", s_zipcode)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@c_contact", c_contact)
        comm.Parameters.AddWithValue("@c_phone", c_phone)
        comm.Parameters.AddWithValue("@c_fax", c_fax)
        comm.Parameters.AddWithValue("@notes", notes)
        comm.Parameters.AddWithValue("@ship_method", ship_method)
        comm.Parameters.AddWithValue("@ship_options", ship_options)
        comm.Parameters.AddWithValue("@is_kit", is_kit)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Function getOrderChargeTax(ByVal orderID As Integer) As Boolean
        getOrderChargeTax = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            getOrderChargeTax = reader.Item("chargetax")
        End If
        conn.Close()
    End Function

    Public Shared Function getChargeTax(ByVal companyID As Integer) As Boolean
        getChargeTax = False
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            getChargeTax = reader.Item("chargetax")
        End If
        conn.Close()
    End Function

    Public Shared Sub UpdateChargeTax(ByVal companyID As Integer, ByVal chargetax As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_company set chargetax=@chargetax where companyID=@companyID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@chargetax", chargetax)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateOrderComplete(ByVal orderID As Integer, ByVal complete As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_order set complete=@complete where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.Parameters.AddWithValue("@complete", complete)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateOrderLine(ByVal lineID As Integer, ByVal manufacturer As String, ByVal partnumber As String, ByVal item As String, ByVal quantity As Double, ByVal uom As String, ByVal availability As String, ByVal price As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_order_line set manufacturer=@manufacturer,partnumber=@partnumber,item=@item,quantity=@quantity,uom=@uom,availability=@availability,price=@price where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@item", item)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.Parameters.AddWithValue("@uom", uom)
        comm.Parameters.AddWithValue("@availability", availability)
        comm.Parameters.AddWithValue("@price", price)
        comm.ExecuteNonQuery()
        conn.Close()
        Dim orderID As Integer
        Dim vendorID As Integer = GetOrderVendorID(orderID)
        Dim companyID As Integer = GetOrderCompanyID(orderID)
        Dim cost As Double = GetVendorCost(companyID, vendorID, manufacturer, partnumber)
        UpdateRequisition(lineID, manufacturer, partnumber, item, quantity, cost)
    End Sub

    Public Shared Sub UpdateOrderLineQuantity(ByVal lineID As Integer, ByVal quantity As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_order_line set quantity=@quantity where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.ExecuteNonQuery()
        conn.Close()
        Dim orderID As Integer
        Dim vendorID As Integer = GetOrderVendorID(orderID)
        Dim companyID As Integer = GetOrderCompanyID(orderID)
        Dim manufacturer As String = GetOrderManufacturer(lineID)
        Dim partnumber As String = GetOrderPartNumber(lineID)
        Dim cost As Double = GetVendorCost(companyID, vendorID, manufacturer, partnumber)
        UpdateRequisitionQuantity(lineID, quantity, cost)
    End Sub

    Public Shared Sub UpdateOrderQuoteID(ByVal orderID As Integer, ByVal quoteID As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_order set quoteID=@quoteID where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@quoteID", quoteID)
        comm.Parameters.AddWithValue("@orderID", orderID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdatePart(ByVal partID As Integer, ByVal part_type As String, ByVal manufacturer As String, ByVal partnumber As String, ByVal description As String, ByVal alt_manufacturer As String, ByVal alt_partnumber As String, ByVal oem_manufacturer As String, ByVal oem_partnumber As String, ByVal quantity As Double, ByVal uom As String, ByVal price As Double, ByVal notes As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_parts set part_type=@part_type,manufacturer=@manufacturer,partnumber=@partnumber,description=@description,alt_manufacturer=@alt_manufacturer,alt_partnumber=@alt_partnumber,oem_manufacturer=@oem_manufacturer,oem_partnumber=@oem_partnumber,quantity=@quantity,uom=@uom,price=@price,notes=@notes where partID=@partID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@partID", partID)
        comm.Parameters.AddWithValue("@part_type", part_type)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@alt_manufacturer", alt_manufacturer)
        comm.Parameters.AddWithValue("@alt_partnumber", alt_partnumber)
        comm.Parameters.AddWithValue("@oem_manufacturer", oem_manufacturer)
        comm.Parameters.AddWithValue("@oem_partnumber", oem_partnumber)
        comm.Parameters.AddWithValue("@description", description)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.Parameters.AddWithValue("@uom", uom)
        comm.Parameters.AddWithValue("@price", price)
        comm.Parameters.AddWithValue("@notes", notes)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdatePartDescription(ByVal partID As Integer, ByVal description As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_parts set description=@description where partID=@partID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@partID", partID)
        comm.Parameters.AddWithValue("@description", description)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdatePartPrice(ByVal partID As Integer, ByVal price As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_parts set price=@price where partID=@partID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@partID", partID)
        comm.Parameters.AddWithValue("@price", price)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdatePO(ByVal poID As Integer, ByVal po As String, ByVal date_submitted As String, ByVal estimated_arrival As String, ByVal email_1 As String, ByVal email_2 As String, ByVal supplier As String, ByVal sup_address1 As String, ByVal sup_address2 As String, ByVal sup_city As String, ByVal sup_state As String, ByVal sup_zipcode As String, ByVal sup_contact As String, ByVal sup_phone As String, ByVal sup_fax As String, ByVal shipto As String, ByVal ship_address1 As String, ByVal ship_address2 As String, ByVal ship_city As String, ByVal ship_state As String, ByVal ship_zipcode As String, ByVal ship_contact As String, ByVal ship_phone As String, ByVal ship_fax As String, ByVal notes As String, ByVal submitted As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_po set po=@po,date_submitted=@date_submitted,estimated_arrival=@estimated_arrival,email_1=@email_1,email_2=@email_2,submitted=@submitted,supplier=@supplier,sup_address1=@sup_address1,sup_address2=@sup_address2,sup_city=@sup_city,sup_state=@sup_state,sup_zipcode=@sup_zipcode,sup_contact=@sup_contact,sup_phone=@sup_phone,sup_fax=@sup_fax,shipto=@shipto,ship_address1=@ship_address1,ship_address2=@ship_address2,ship_city=@ship_city,ship_state=@ship_state,ship_zipcode=@ship_zipcode,ship_contact=@ship_contact,ship_phone=@ship_phone,ship_fax=@ship_fax where poID=@poID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@poID", poID)
        comm.Parameters.AddWithValue("@po", po)
        comm.Parameters.AddWithValue("@date_submitted", date_submitted)
        comm.Parameters.AddWithValue("@estimated_arrival", estimated_arrival)
        comm.Parameters.AddWithValue("@email_1", email_1)
        comm.Parameters.AddWithValue("@email_2", email_2)
        comm.Parameters.AddWithValue("@submitted", submitted)
        comm.Parameters.AddWithValue("@supplier", supplier)
        comm.Parameters.AddWithValue("@sup_address1", sup_address1)
        comm.Parameters.AddWithValue("@sup_address2", sup_address2)
        comm.Parameters.AddWithValue("@sup_city", sup_city)
        comm.Parameters.AddWithValue("@sup_state", sup_state)
        comm.Parameters.AddWithValue("@sup_zipcode", sup_zipcode)
        comm.Parameters.AddWithValue("@sup_contact", sup_contact)
        comm.Parameters.AddWithValue("@sup_phone", sup_phone)
        comm.Parameters.AddWithValue("@sup_fax", sup_fax)
        comm.Parameters.AddWithValue("@shipto", shipto)
        comm.Parameters.AddWithValue("@ship_address1", ship_address1)
        comm.Parameters.AddWithValue("@ship_address2", ship_address2)
        comm.Parameters.AddWithValue("@ship_city", ship_city)
        comm.Parameters.AddWithValue("@ship_state", ship_state)
        comm.Parameters.AddWithValue("@ship_zipcode", ship_zipcode)
        comm.Parameters.AddWithValue("@ship_contact", ship_contact)
        comm.Parameters.AddWithValue("@ship_phone", ship_phone)
        comm.Parameters.AddWithValue("@ship_fax", ship_fax)
        comm.Parameters.AddWithValue("@notes", notes)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdatePOSubmitted(ByVal poID As Integer, ByVal submitted As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_po set submitted=@submitted where poID=@poID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@poID", poID)
        comm.Parameters.AddWithValue("@submitted", submitted)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdatePOComplete(ByVal poID As Integer, ByVal complete As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_po set complete=@complete where poID=@poID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@poID", poID)
        comm.Parameters.AddWithValue("@complete", complete)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdatePOLineCost(ByVal polineID As Integer, ByVal cost As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_poline set cost=@cost where polineID=@polineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@polineID", polineID)
        comm.Parameters.AddWithValue("@cost", cost)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdatePOLineQty(ByVal polineID As Integer, ByVal quantity As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_poline set quantity=@quantity where polineID=@polineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@polineID", polineID)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdatePOLineQuantity(ByVal poID As Integer, ByVal manufacturer As String, ByVal partnumber As String, ByVal quantity As Double)
        Dim qty As Double = GetPOLineQuantity(poID, manufacturer, partnumber)
        quantity += qty
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_poline set quantity=@quantity where poID=@poID and manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@poID", poID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdatePM(ByVal equipmentID As Integer, ByVal verified As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_equipment set verified=@verified where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.Parameters.AddWithValue("@verified", verified)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateProduct(ByVal productID As Integer, ByVal category As Integer, ByVal manufacturer As String, ByVal partnumber As String, ByVal item As String, ByVal description As String, ByVal coreID As Integer, ByVal cost As Double, ByVal msrp As Double, ByVal saleprice As Double, ByVal uom As String, ByVal package As Double, ByVal rebate As Boolean, ByVal price_category As String, ByVal weight As Double, ByVal upc As String, ByVal is_tax As Boolean, ByVal featured As Boolean, ByVal clearance As Boolean, ByVal onhand As Double, ByVal nStock As Boolean, ByVal min As Double, max As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_product set category=@category,manufacturer=@manufacturer,partnumber=@partnumber,item=@item,description=@description,coreID=@coreID,cost=@cost,msrp=@msrp,saleprice=@saleprice,uom=@uom,package=@package,rebate=@rebate,price_category=@price_category,weight=@weight,upc=@upc,is_tax=@is_tax,featured=@featured,clearance=@clearance,onhand=@onhand,nStock=@nStock,min=@min,max=@max where productID=@productID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@productID", productID)
        comm.Parameters.AddWithValue("@category", category)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@item", item)
        comm.Parameters.AddWithValue("@description", description)
        comm.Parameters.AddWithValue("@coreID", coreID)
        comm.Parameters.AddWithValue("@cost", cost)
        comm.Parameters.AddWithValue("@msrp", msrp)
        comm.Parameters.AddWithValue("@saleprice", saleprice)
        comm.Parameters.AddWithValue("@uom", uom)
        comm.Parameters.AddWithValue("@package", package)
        comm.Parameters.AddWithValue("@rebate", rebate)
        comm.Parameters.AddWithValue("@price_category", price_category)
        comm.Parameters.AddWithValue("@weight", weight)
        comm.Parameters.AddWithValue("@upc", upc)
        comm.Parameters.AddWithValue("@is_tax", is_tax)
        comm.Parameters.AddWithValue("@featured", featured)
        comm.Parameters.AddWithValue("@clearance", clearance)
        comm.Parameters.AddWithValue("@nStock", nStock)
        comm.Parameters.AddWithValue("@onhand", onhand)
        comm.Parameters.AddWithValue("@min", min)
        comm.Parameters.AddWithValue("@max", max)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateProductCost(ByVal manufacturer As String, ByVal partnumber As String, ByVal cost As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_product set cost=@cost where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@cost", cost)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateQuote(ByVal quoteID As Integer, ByVal response_date As String, ByVal deliverby_date As String, ByVal complete As Boolean, ByVal userID As Integer, ByVal c_contact As String, ByVal c_phone As String, ByVal c_fax As String, ByVal shipID As Integer, ByVal shipto As String, ByVal s_address1 As String, ByVal s_address2 As String, ByVal s_address3 As String, ByVal s_city As String, ByVal s_state As String, ByVal s_zipcode As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_quote set response_date=@response_date,complete=@complete,deliverby_date=@deliverby_date,shipID=@shipID,shipto=@shipto,s_address1=@s_address1,s_address2=@s_address2,s_address3=@s_address3,s_city=@s_city,s_state=@s_state,s_zipcode=@s_zipcode,userID=@userID,c_contact=@c_contact,c_phone=@c_phone,c_fax=@c_fax where quoteID=@quoteID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@quoteID", quoteID)
        comm.Parameters.AddWithValue("@deliverby_date", deliverby_date)
        comm.Parameters.AddWithValue("@response_date", response_date)
        comm.Parameters.AddWithValue("@complete", complete)
        comm.Parameters.AddWithValue("@shipID", shipID)
        comm.Parameters.AddWithValue("@shipto", shipto)
        comm.Parameters.AddWithValue("@s_address1", s_address1)
        comm.Parameters.AddWithValue("@s_address2", s_address2)
        comm.Parameters.AddWithValue("@s_address3", s_address3)
        comm.Parameters.AddWithValue("@s_city", s_city)
        comm.Parameters.AddWithValue("@s_state", s_state)
        comm.Parameters.AddWithValue("@s_zipcode", s_zipcode)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@c_contact", c_contact)
        comm.Parameters.AddWithValue("@c_phone", c_phone)
        comm.Parameters.AddWithValue("@c_fax", c_fax)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateQuoteLine(ByVal lineID As Integer, ByVal assetID As Integer, ByVal manufacturer As String, ByVal partnumber As String, ByVal item As String, ByVal quantity As Double, ByVal uom As String, ByVal price As Double, ByVal availability As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_quote_line set assetID=@assetID,manufacturer=@manufacturer,partnumber=@partnumber,item=@item,quantity=@quantity,uom=@uom,price=@price,availability=@availability where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        comm.Parameters.AddWithValue("@assetID", assetID)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@item", item)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.Parameters.AddWithValue("@uom", uom)
        comm.Parameters.AddWithValue("@price", price)
        comm.Parameters.AddWithValue("@availability", availability)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateRequisitionOnPO(ByVal lineID As Integer, OnPO As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_requisition set OnPO=@OnPO where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        comm.Parameters.AddWithValue("@OnPO", OnPO)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateRequisition(ByVal lineID As Integer, ByVal manufacturer As String, ByVal partnumber As String, ByVal item As String, ByVal quantity As Double, ByVal cost As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_requisition set manufacturer=@manufacturer,partnumber=@partnumber,item=@item,quantity=@quantity,cost=@cost where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        comm.Parameters.AddWithValue("@item", item)
        comm.Parameters.AddWithValue("@lineID", lineID)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.Parameters.AddWithValue("@cost", cost)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateRequisitionQuantity(ByVal lineID As Integer, ByVal quantity As Double, ByVal cost As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_requisition set quantity=@quantity,cost=@cost where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.Parameters.AddWithValue("@cost", cost)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateRequisitionQty(ByVal lineID As Integer, ByVal quantity As Double)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_requisition set quantity=@quantity where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub ReplacePartNumbers(ByVal companyID As Integer, ByVal manufacturer As String, ByVal partnumber As String, ByVal pri_manufacturer As String, ByVal pri_partnumber As String, ByVal equipmentID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_product where manufacturer=@manufacturer and partnumber=@partnumber"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        comm.Parameters.AddWithValue("@partnumber", partnumber)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            UpdateKits_ReplacePartNumber(equipmentID, pri_manufacturer, pri_partnumber, manufacturer, partnumber, reader.Item("item").ToString, FormatCurrency(GetCompanyPrice(companyID, manufacturer, partnumber), 2))
        End If
        reader.Close()
        conn.Close()
    End Sub

    Public Shared Sub UpdateKits_ReplacePartNumber(ByVal equipmentID As Integer, ByVal pri_manufacturer As String, ByVal pri_partnumber As String, ByVal rep_manufacturer As String, ByVal rep_partnumber As String, ByVal rep_description As String, ByVal rep_price As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_parts set partnumber=@rep_partnumber,manufacturer=@rep_manufacturer,description=@description,price=@price where manufacturer=@pri_manufacturer and partnumber=@pri_partnumber and equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        comm.Parameters.AddWithValue("@rep_partnumber", rep_partnumber)
        comm.Parameters.AddWithValue("@rep_manufacturer", rep_manufacturer)
        comm.Parameters.AddWithValue("@description", rep_description)
        comm.Parameters.AddWithValue("@price", rep_price)
        comm.Parameters.AddWithValue("@pri_manufacturer", pri_manufacturer)
        comm.Parameters.AddWithValue("@pri_partnumber", pri_partnumber)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateService(ByVal serviceID As Integer, ByVal technician As String, ByVal email As String, ByVal schedule_date As String, ByVal complete_date As String, ByVal report As String, ByVal location As String, ByVal address As String, ByVal address2 As String, ByVal address3 As String, ByVal city As String, ByVal state As String, ByVal zipcode As String, ByVal instructions As String, ByVal hours_miles As String, ByVal workorder As String, ByVal fuel_gallons As Double, ByVal def_gallons As Double, ByVal service_type As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_service set technician=@technician,email=@email,schedule_date=@schedule_date,complete_date=@complete_date,report=@report,instructions=@instructions,location=@location,address=@address, address2=@address2,address3=@address3,city=@city,state=@state,zipcode=@zipcode,hours_miles=@hours_miles,workorder=@workorder,fuel_gallons=@fuel_gallons,def_gallons=@def_gallons,service_type=@service_type where serviceID=@serviceID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceID", serviceID)
        comm.Parameters.AddWithValue("@technician", technician)
        comm.Parameters.AddWithValue("@email", email)
        comm.Parameters.AddWithValue("@schedule_date", schedule_date)
        comm.Parameters.AddWithValue("@complete_date", complete_date)
        comm.Parameters.AddWithValue("@report", report)
        comm.Parameters.AddWithValue("@instructions", instructions)
        comm.Parameters.AddWithValue("@location", location)
        comm.Parameters.AddWithValue("@address", address)
        comm.Parameters.AddWithValue("@address2", address2)
        comm.Parameters.AddWithValue("@address3", address3)
        comm.Parameters.AddWithValue("@city", city)
        comm.Parameters.AddWithValue("@state", state)
        comm.Parameters.AddWithValue("@zipcode", zipcode)
        comm.Parameters.AddWithValue("@hours_miles", hours_miles)
        comm.Parameters.AddWithValue("@workorder", workorder)
        comm.Parameters.AddWithValue("@fuel_gallons", fuel_gallons)
        comm.Parameters.AddWithValue("@def_gallons", def_gallons)
        comm.Parameters.AddWithValue("@service_type", service_type)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateServicePart(ByVal servicepartID As Integer, ByVal selected As Boolean, ByVal partnotes As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_serviceparts set selected=@selected,partnotes=@partnotes where servicepartID=@servicepartID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@servicepartID", servicepartID)
        comm.Parameters.AddWithValue("@selected", selected)
        comm.Parameters.AddWithValue("@partnotes", partnotes)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateServiceProfile(ByVal serviceprofileID As Integer, ByVal name As String, ByVal interval As String, ByVal interval_type As String, ByVal servicenotes As String, ByVal servicetype As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_serviceprofile set name=@name,interval=@interval,interval_type=@interval_type,servicenotes=@servicenotes,servicetype=@servicetype where serviceprofileID=@serviceprofileID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        comm.Parameters.AddWithValue("@name", name)
        comm.Parameters.AddWithValue("@interval", interval)
        comm.Parameters.AddWithValue("@interval_type", interval_type)
        comm.Parameters.AddWithValue("@servicenotes", servicenotes)
        comm.Parameters.AddWithValue("@servicetype", servicetype)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateShipment(ByVal shipmentID As Integer, ByVal carrier As String, ByVal ship_charge As Double, ByVal tracking As String, ByVal shipped As Boolean, ByVal shipment_date As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_shipment set carrier=@carrier,ship_charge=@ship_charge,tracking=@tracking,shipped=@shipped,shipment_date=@shipment_date where shipmentID=@shipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        comm.Parameters.AddWithValue("@shipment_date", shipment_date)
        comm.Parameters.AddWithValue("@carrier", carrier)
        comm.Parameters.AddWithValue("@ship_charge", ship_charge)
        comm.Parameters.AddWithValue("@tracking", tracking)
        comm.Parameters.AddWithValue("@shipped", shipped)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateInvoiceUserID(ByVal shipmentID As Integer, ByVal invoice_userID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_shipment set invoice_userID=@invoice_userID where shipmentID=@shipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        comm.Parameters.AddWithValue("@invoice_userID", invoice_userID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateShipUserID(ByVal shipmentID As Integer, ByVal ship_userID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_shipment set ship_userID=@ship_userID where shipmentID=@shipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        comm.Parameters.AddWithValue("@ship_userID", ship_userID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdatePickUserID(ByVal shipmentID As Integer, ByVal pick_userID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_shipment set pick_userID=@pick_userID where shipmentID=@shipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        comm.Parameters.AddWithValue("@pick_userID", pick_userID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateShipTo(ByVal shipID As Integer, ByVal shipto As String, ByVal s_address1 As String, ByVal s_address2 As String, ByVal s_address3 As String, ByVal s_city As String, ByVal s_state As String, ByVal s_zipcode As String, ByVal s_phone As String)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "update t_ship set shipto=@shipto,s_address1=@s_address1,s_address2=@s_address2,s_address3=@s_address3,s_city=@s_city,s_state=@s_state,s_zipcode=@s_zipcode,s_phone=@s_phone where shipID=@shipID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipID", shipID)
        comm.Parameters.AddWithValue("@shipto", shipto)
        comm.Parameters.AddWithValue("@s_address1", s_address1)
        comm.Parameters.AddWithValue("@s_address2", s_address2)
        comm.Parameters.AddWithValue("@s_address3", s_address3)
        comm.Parameters.AddWithValue("@s_city", s_city)
        comm.Parameters.AddWithValue("@s_state", s_state)
        comm.Parameters.AddWithValue("@s_zipcode", s_zipcode)
        comm.Parameters.AddWithValue("@s_phone", s_phone)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateUser(ByVal userID As Integer, ByVal name As String, ByVal title As String, ByVal c_phone As String, ByVal c_fax As String, ByVal email As String, ByVal password As String, ByVal administrator As Boolean, ByVal username As String)
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        commandString = "update t_user set name=@name,title=@title,c_phone=@c_phone,c_fax=@c_fax,email=@email,password=@password,administrator=@administrator,username=@username where userID=@userID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@userID", userID)
        comm.Parameters.AddWithValue("@name", name)
        comm.Parameters.AddWithValue("@title", title)
        comm.Parameters.AddWithValue("@c_phone", c_phone)
        comm.Parameters.AddWithValue("@c_fax", c_fax)
        comm.Parameters.AddWithValue("@email", email)
        comm.Parameters.AddWithValue("@password", password)
        comm.Parameters.AddWithValue("@administrator", administrator)
        comm.Parameters.AddWithValue("@username", username)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateVendorOrderLine(ByVal lineID As Integer, ByVal quantity As Double, ByVal price As Double, ByVal availability As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_order_line set quantity=@quantity,price=@price,availability=@availability where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.Parameters.AddWithValue("@price", price)
        comm.Parameters.AddWithValue("@availability", availability)
        comm.ExecuteNonQuery()
        conn.Close()
        Dim orderID As Integer = GetLineOrderID(lineID)
        Dim vendorID As Integer = GetOrderVendorID(orderID)
        Dim companyID As Integer = GetOrderCompanyID(orderID)
        Dim manufacturer As String = GetOrderManufacturer(lineID)
        Dim partnumber As String = GetOrderPartNumber(lineID)
        Dim cost As Double = GetVendorCost(companyID, vendorID, manufacturer, partnumber)
        UpdateRequisitionQuantity(lineID, quantity, cost)
    End Sub

    Public Shared Sub UpdateVendorQuoteLine(ByVal lineID As Integer, ByVal quantity As Double, ByVal price As Double, ByVal availability As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_quote_line set quantity=@quantity,price=@price,availability=@availability where lineID=@lineID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@lineID", lineID)
        comm.Parameters.AddWithValue("@quantity", quantity)
        comm.Parameters.AddWithValue("@price", price)
        comm.Parameters.AddWithValue("@availability", availability)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateQBO(ByVal shipmentID As Integer, ByVal qbo As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_shipment set qbo=@qbo where shipmentID=@shipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@qbo", qbo)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateComponent(ByVal componentID As Integer, ByVal component As String)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_component set component=@component where componentID=@componentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@component", component)
        comm.Parameters.AddWithValue("@componentID", componentID)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub UpdateWorkDescription(ByVal descriptionID As Integer, ByVal description As String, ByVal cost As Double, ByVal price As Double, ByVal componentID As Integer)
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "update t_workdescription set description=@description,componentID=@componentID,cost=@cost,price=@price where descriptionID=@descriptionID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@descriptionID", descriptionID)
        comm.Parameters.AddWithValue("@description", description)
        comm.Parameters.AddWithValue("@componentID", componentID)
        comm.Parameters.AddWithValue("@cost", cost)
        comm.Parameters.AddWithValue("@price", price)
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Shared Sub rgaPDF(ByVal shipmentID As Integer, ByVal applicationpath As String, ByVal imagefile As String, ByVal cimagefile As String, ByVal filename As String)

        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim commandString As String = "select * from v_shipments where shipmentID=@shipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            Dim doc As Document = New Document(PageSize.LETTER)
            Dim invoiceID As String = reader.Item("invoiceID").ToString
            Dim orderID As String = reader.Item("orderID").ToString
            Dim customer As String = reader.Item("company").ToString
            Dim companyID As Integer = reader.Item("companyID").ToString
            Dim vendorID As Integer = reader.Item("vendorID").ToString
            Dim placedby As String = reader.Item("placedby").ToString
            Dim shipment_date As String = reader.Item("shipment_date").ToString
            Dim due_date As String = DateAdd(DateInterval.Day, 30, reader.Item("shipment_date"))
            Dim purchaseorder As String = reader.Item("purchaseorder").ToString
            Dim billto As String = reader.Item("company").ToString
            Dim b_address1 As String = reader.Item("b_address1").ToString
            Dim b_address2 As String = reader.Item("b_address2").ToString
            Dim b_citystatezip As String = reader.Item("b_city") & ", " & reader.Item("b_state") & " " & reader.Item("b_zipcode")
            Dim shipto As String = reader.Item("shipto").ToString
            Dim s_address1 As String = reader.Item("s_address1").ToString
            Dim s_address2 As String = reader.Item("s_address2").ToString
            Dim s_address3 As String = reader.Item("s_address3").ToString
            Dim s_citystatezip As String = reader.Item("s_city") & ", " & reader.Item("s_state") & " " & reader.Item("s_zipcode")
            Dim c_contact As String = reader.Item("c_contact").ToString
            Dim c_phone As String = reader.Item("c_phone").ToString
            Dim c_fax As String = reader.Item("c_fax").ToString
            Dim chargetax As Boolean = reader.Item("chargetax")
            Dim ship_method As String = reader.Item("ship_method").ToString
            Dim ship_options As String = reader.Item("ship_options").ToString
            Dim carrier As String = reader.Item("carrier")
            Dim ship_charge As String = FormatCurrency(reader.Item("ship_charge"), 2)
            Dim tracking As String = reader.Item("tracking").ToString
            Dim assetID As String = ""
            Dim kitnum As Integer = 0

            reader.Close()
            Dim logo As Image = Image.GetInstance(applicationpath + imagefile)
            Dim basefont1 As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, False)
            Dim font1 As Font = FontFactory.GetFont("Century Gothic", 10)
            Dim font2 As Font = FontFactory.GetFont("Century Gothic", 10, Font.BOLD)
            Dim font3 As Font = FontFactory.GetFont("Century Gothic", 18, Font.BOLD)
            Dim font4 As Font = FontFactory.GetFont("Century Gothic", 8, Font.ITALIC)

            PdfWriter.GetInstance(doc, New FileStream(applicationpath + filename, FileMode.Create))
            doc.Open()
            doc.SetPageSize(PageSize.A4)
            doc.SetMargins(36, 36, 72, 72)
            doc.SetMarginMirroring(True)
            logo.Alignment = Element.ALIGN_LEFT
            doc.Add(logo)
            Dim x As Integer = 1
            If x = 1 Then
                Dim cell As PdfPCell
                'header table
                Dim table As PdfPTable = New PdfPTable(8)
                    table.WidthPercentage = 100

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase("RGA", font3))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase("Credit Memo", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(invoiceID, font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)

                    'header row 0
                    cell = New PdfPCell(New Phrase("Date Received", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(shipment_date, font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Remit To", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("DFO Filters & Equipment", font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" ", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("5002 S. 40th Street Ste C", font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" ", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Phoenix, AZ 85040", font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase("Bill To", font2))
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell()
                    cell.Colspan = 3
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.Border = 0
                    cell.AddElement(New Phrase(billto, font1))
                    cell.AddElement(New Phrase(b_address1, font1))
                    If b_address2 <> "" Then
                        cell.AddElement(New Phrase(b_address2, font1))
                    End If
                    cell.AddElement(New Phrase(b_citystatezip, font1))
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Ship To", font2))
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell()
                    cell.Colspan = 3
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.Border = 0
                    cell.AddElement(New Phrase(shipto, font1))
                    cell.AddElement(New Phrase(s_address1, font1))
                    If s_address2 <> "" Then
                        cell.AddElement(New Phrase(s_address2, font1))
                    End If
                    If s_address3 <> "" Then
                        cell.AddElement(New Phrase(s_address3, font1))
                    End If
                    cell.AddElement(New Phrase(s_citystatezip, font1))
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase("RGA ID", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(orderID, font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Assets", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(assetID, font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)

                cell = New PdfPCell(New Phrase("Request By", font2))
                cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(c_contact, font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Carrier", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(carrier, font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase("PO", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(purchaseorder, font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Tracking", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(tracking, font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 2
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)

                    doc.Add(table)
                    'end header table

                    commandString = "select * from v_shipmentlines where shipmentID=@shipmentID"
                    comm = New SqlCommand(commandString, conn)
                    comm.Parameters.AddWithValue("@shipmentID", shipmentID)
                    reader = comm.ExecuteReader

                    'lines table
                    table = New PdfPTable(12)
                    table.WidthPercentage = 100

                    'lines header row
                    cell = New PdfPCell(New Phrase("Qty", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    cell.HorizontalAlignment = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Item", font2))
                    cell.Colspan = 5
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Ship", font2))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font2))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Price", font2))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 2
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font2))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 2
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Extended", font2))
                    cell.Colspan = 2
                    cell.HorizontalAlignment = 2
                    cell.Border = 0
                    table.AddCell(cell)
                    'end header row

                    Dim manufacturer, partnumber, item, quantity, ship_qty, price, extended, uom As String
                    Dim subtotal As Double = 0
                    Dim tax_subtotal As Double = 0
                    Dim salestax As Double = 0
                    Dim total As Double = 0
                    While reader.Read
                        manufacturer = reader.Item("manufacturer")
                        partnumber = reader.Item("partnumber")
                        item = reader.Item("item")
                        quantity = reader.Item("quantity")
                        ship_qty = reader.Item("ship_qty")
                        price = FormatCurrency(reader.Item("price"), 2)
                        uom = reader.Item("uom")
                        extended = FormatCurrency(reader.Item("price") * reader.Item("ship_qty"), 2)
                        subtotal += extended
                        If IsTaxable(reader.Item("manufacturer"), reader.Item("partnumber")) = True Then
                            tax_subtotal = tax_subtotal + extended
                        End If

                        'start line row
                        cell = New PdfPCell(New Phrase(quantity, font1))
                        cell.Border = 0
                        cell.Colspan = 1
                        cell.HorizontalAlignment = 1
                        table.AddCell(cell)
                        cell = New PdfPCell(New Phrase(manufacturer & " " & partnumber, font1))
                        cell.Border = 0
                        cell.Colspan = 5
                        table.AddCell(cell)
                        cell = New PdfPCell(New Phrase(ship_qty, font1))
                        cell.Border = 0
                        cell.Colspan = 1
                        cell.HorizontalAlignment = 1
                        table.AddCell(cell)
                        cell = New PdfPCell(New Phrase(" ", font1))
                        cell.Border = 0
                        cell.Colspan = 1
                        cell.HorizontalAlignment = 1
                        table.AddCell(cell)
                        cell = New PdfPCell(New Phrase(price, font1))
                        cell.Border = 0
                        cell.Colspan = 1
                        cell.HorizontalAlignment = 2
                        table.AddCell(cell)
                        cell = New PdfPCell(New Phrase(uom, font1))
                        cell.Border = 0
                        cell.Colspan = 1
                        cell.HorizontalAlignment = 2
                        table.AddCell(cell)
                        cell = New PdfPCell(New Phrase(extended, font1))
                        cell.Border = 0
                        cell.Colspan = 2
                        cell.HorizontalAlignment = 2
                        table.AddCell(cell)

                        cell = New PdfPCell(New Phrase(" ", font1))
                        cell.Colspan = 1
                        cell.Border = 0
                        table.AddCell(cell)
                        cell = New PdfPCell(New Phrase(item, font1))
                        cell.Colspan = 11
                        cell.Border = 0
                        table.AddCell(cell)

                        cell = New PdfPCell(New Phrase(" ", font1))
                        cell.Colspan = 12
                        cell.Border = 0
                        table.AddCell(cell)

                        'end line row
                    End While

                    reader.Close()
                    If chargetax = True Then
                        salestax = tax_subtotal * GetSalesTax(companyID, vendorID)
                    Else
                        salestax = 0
                    End If
                    total = subtotal + salestax + ship_charge

                    table.HeaderRows = 1
                    doc.Add(table)

                    table = New PdfPTable(12)
                    table.WidthPercentage = 100

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Sub Total", font2))
                    cell.Colspan = 2
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(FormatCurrency(subtotal, 2), font2))
                    cell.HorizontalAlignment = 2
                    cell.Colspan = 2
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Freight", font2))
                    cell.Colspan = 2
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(ship_charge, font2))
                    cell.HorizontalAlignment = 2
                    cell.Colspan = 2
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Sales Tax", font2))
                    cell.Colspan = 2
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(FormatCurrency(salestax, 2), font2))
                    cell.HorizontalAlignment = 2
                    cell.Colspan = 2
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Total", font2))
                    cell.Colspan = 2
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(FormatCurrency(total, 2), font2))
                    cell.HorizontalAlignment = 2
                    cell.Colspan = 2
                    cell.Border = 0
                    table.AddCell(cell)
                    doc.Add(table)

                    doc.NewPage()
                Else
                    Dim cell As PdfPCell

                    'header table
                    Dim table As PdfPTable = New PdfPTable(8)
                    table.WidthPercentage = 100

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase("Packing Slip", font3))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase("Invoice", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(invoiceID, font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)

                    'header row 0
                    cell = New PdfPCell(New Phrase("Ship Date", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(shipment_date, font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("From", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("DFO Filters & Equipment", font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" ", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("5002 S 40th Street Ste C", font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" ", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Phoenix, AZ 85040", font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase("Bill To", font2))
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell()
                    cell.Colspan = 3
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.Border = 0
                    cell.AddElement(New Phrase(billto, font1))
                    cell.AddElement(New Phrase(b_address1, font1))
                    If b_address2 <> "" Then
                        cell.AddElement(New Phrase(b_address2, font1))
                    End If
                    cell.AddElement(New Phrase(b_citystatezip, font1))
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Ship To", font2))
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell()
                    cell.Colspan = 3
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.Border = 0
                    cell.AddElement(New Phrase(shipto, font1))
                    cell.AddElement(New Phrase(s_address1, font1))
                    If s_address2 <> "" Then
                        cell.AddElement(New Phrase(s_address2, font1))
                    End If
                    If s_address3 <> "" Then
                        cell.AddElement(New Phrase(s_address3, font1))
                    End If
                    cell.AddElement(New Phrase(s_citystatezip, font1))
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase("Order ID", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(orderID, font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Assets", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(assetID, font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase("Ordered By", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(c_contact, font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Carrier", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(carrier, font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase("PO", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(purchaseorder, font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Tracking", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(tracking, font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 2
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)

                    doc.Add(table)
                    'end header table

                    commandString = "select * from v_shipmentlines where shipmentID=@shipmentID"
                    comm = New SqlCommand(commandString, conn)
                    comm.Parameters.AddWithValue("@shipmentID", shipmentID)
                    reader = comm.ExecuteReader

                    'lines table
                    table = New PdfPTable(12)
                    table.WidthPercentage = 100

                    'lines header row
                    cell = New PdfPCell(New Phrase("Item", font2))
                    cell.Colspan = 5
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font2))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Qty", font2))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font2))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 2
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font2))
                    cell.Colspan = 2
                    cell.Border = 0
                    cell.HorizontalAlignment = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("Ship", font2))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("BO", font2))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    'end header row

                    Dim manufacturer, partnumber, item, quantity, ship_qty, price, extended, uom As String
                    Dim backorder As Double = 0
                    Dim subtotal As Double = 0
                    Dim tax_subtotal As Double = 0
                    Dim salestax As Double = 0
                    Dim total As Double = 0
                    While reader.Read
                        manufacturer = reader.Item("manufacturer")
                        partnumber = reader.Item("partnumber")
                        item = reader.Item("item")
                        quantity = reader.Item("quantity")
                        ship_qty = reader.Item("ship_qty")
                        backorder = appcode.GetBO(reader.Item("lineID"))
                        price = FormatCurrency(reader.Item("price"), 2)
                        uom = reader.Item("uom")
                        If ship_qty > 0 Or backorder > 0 Then
                            extended = FormatCurrency(reader.Item("price") * reader.Item("ship_qty"), 2)
                            subtotal += extended
                            If IsTaxable(reader.Item("manufacturer"), reader.Item("partnumber")) = True Then
                                tax_subtotal = tax_subtotal + extended
                            End If

                            'start line row
                            cell = New PdfPCell(New Phrase(manufacturer & " " & partnumber, font1))
                            cell.Border = 0
                            cell.Colspan = 5
                            table.AddCell(cell)
                            cell = New PdfPCell(New Phrase(" ", font1))
                            cell.Border = 0
                            cell.Colspan = 1
                            cell.HorizontalAlignment = 1
                            table.AddCell(cell)
                            cell = New PdfPCell(New Phrase(quantity, font1))
                            cell.Border = 0
                            cell.Colspan = 1
                            cell.HorizontalAlignment = 1
                            table.AddCell(cell)
                            cell = New PdfPCell(New Phrase(" ", font1))
                            cell.Border = 0
                            cell.Colspan = 2
                            cell.HorizontalAlignment = 1
                            table.AddCell(cell)
                            cell = New PdfPCell(New Phrase(" ", font1))
                            cell.Border = 0
                            cell.Colspan = 1
                            cell.HorizontalAlignment = 1
                            table.AddCell(cell)
                            cell = New PdfPCell(New Phrase(ship_qty, font1))
                            cell.Border = 0
                            cell.Colspan = 1
                            cell.HorizontalAlignment = 1
                            table.AddCell(cell)
                            cell = New PdfPCell(New Phrase(backorder, font1))
                            cell.Border = 0
                            cell.Colspan = 1
                            cell.HorizontalAlignment = 1
                            table.AddCell(cell)

                            cell = New PdfPCell(New Phrase(item, font1))
                            cell.Colspan = 12
                            cell.Border = 0
                            table.AddCell(cell)

                            cell = New PdfPCell(New Phrase(" ", font1))
                            cell.Colspan = 12
                            cell.Border = 0
                            table.AddCell(cell)

                            'end line row
                        End If
                    End While

                    reader.Close()
                    If chargetax = True Then
                        salestax = tax_subtotal * GetSalesTax(companyID, vendorID)
                    Else
                        salestax = 0
                    End If
                    total = subtotal + salestax + ship_charge

                    table.HeaderRows = 1
                    doc.Add(table)

                End If


            conn.Close()
            doc.Close()
        End If
    End Sub

    Public Shared Sub InvoicePDF(ByVal shipmentID As Integer, ByVal applicationpath As String, ByVal imagefile As String, ByVal cimagefile As String, ByVal filename As String)

        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim commandString As String = "select * from v_shipments where shipmentID=@shipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            Dim doc As Document = New Document(PageSize.LETTER)
            Dim invoiceID As String = reader.Item("invoiceID").ToString
            Dim orderID As String = reader.Item("orderID").ToString
            Dim customer As String = reader.Item("company").ToString
            Dim companyID As Integer = reader.Item("companyID").ToString
            Dim vendorID As Integer = reader.Item("vendorID").ToString
            Dim placedby As String = reader.Item("placedby").ToString
            Dim shipment_date As String = reader.Item("shipment_date").ToString
            Dim due_date As String = DateAdd(DateInterval.Day, 30, reader.Item("shipment_date"))
            Dim purchaseorder As String = reader.Item("purchaseorder").ToString
            Dim billto As String = reader.Item("company").ToString
            Dim b_address1 As String = reader.Item("b_address1").ToString
            Dim b_address2 As String = reader.Item("b_address2").ToString
            Dim b_citystatezip As String = reader.Item("b_city") & ", " & reader.Item("b_state") & " " & reader.Item("b_zipcode")
            Dim shipto As String = reader.Item("shipto").ToString
            Dim s_address1 As String = reader.Item("s_address1").ToString
            Dim s_address2 As String = reader.Item("s_address2").ToString
            Dim s_address3 As String = reader.Item("s_address3").ToString
            Dim s_citystatezip As String = reader.Item("s_city") & ", " & reader.Item("s_state") & " " & reader.Item("s_zipcode")
            Dim c_contact As String = reader.Item("c_contact").ToString
            Dim c_phone As String = reader.Item("c_phone").ToString
            Dim c_fax As String = reader.Item("c_fax").ToString
            Dim chargetax As Boolean = reader.Item("chargetax")
            Dim ship_method As String = reader.Item("ship_method").ToString
            Dim ship_options As String = reader.Item("ship_options").ToString
            Dim carrier As String = reader.Item("carrier")
            Dim ship_charge As String = FormatCurrency(reader.Item("ship_charge"), 2)
            Dim tracking As String = reader.Item("tracking").ToString
            Dim assetID As String = ""
            Dim kitnum As Integer = 0
            Dim kitID As Array
            If reader.Item("serviceprofileID").ToString <> "0" Then
                kitID = Split(reader.Item("serviceprofileID"), ",")
                Dim numkits As Integer = kitID.GetLength(0)
                Dim y As Integer = 0
                For x As Integer = 0 To numkits - 1
                    If y > 0 Then
                        assetID &= ","
                    End If
                    assetID &= appcode.getAssetID(kitID(x))
                    y += 1
                Next
                kitnum = kitID(0)
            Else
                assetID = "NA"
            End If
            Dim kit As String = "No"
            If reader.Item("is_kit") = True Then
                kit = "Yes"
            End If

            reader.Close()
            Dim logo As Image = Image.GetInstance(applicationpath + imagefile)
            Dim basefont1 As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, False)
            Dim font1 As Font = FontFactory.GetFont("Century Gothic", 10)
            Dim font2 As Font = FontFactory.GetFont("Century Gothic", 10, Font.BOLD)
            Dim font3 As Font = FontFactory.GetFont("Century Gothic", 18, Font.BOLD)
            Dim font4 As Font = FontFactory.GetFont("Century Gothic", 8, Font.ITALIC)

            PdfWriter.GetInstance(doc, New FileStream(applicationpath + filename, FileMode.Create))
            doc.Open()
            doc.SetPageSize(PageSize.A4)
            doc.SetMargins(36, 36, 72, 72)
            doc.SetMarginMirroring(True)
            For x = 1 To 2
                logo.Alignment = Element.ALIGN_LEFT
                doc.Add(logo)

                Dim cell As PdfPCell

                'header table
                Dim table As PdfPTable = New PdfPTable(8)
                table.WidthPercentage = 100

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("Packing Slip", font3))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("Invoice", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(invoiceID, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                'header row 0
                cell = New PdfPCell(New Phrase("Ship Date", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(shipment_date, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Remit To", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("DFO Filters & Equipment", font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" ", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("5002 S. 40th Street Ste C", font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" ", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Phoenix, AZ 85040", font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("Bill To", font2))
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell()
                cell.Colspan = 3
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                cell.Border = 0
                cell.AddElement(New Phrase(billto, font1))
                cell.AddElement(New Phrase(b_address1, font1))
                If b_address2 <> "" Then
                    cell.AddElement(New Phrase(b_address2, font1))
                End If
                cell.AddElement(New Phrase(b_citystatezip, font1))
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Ship To", font2))
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell()
                cell.Colspan = 3
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                cell.Border = 0
                cell.AddElement(New Phrase(shipto, font1))
                cell.AddElement(New Phrase(s_address1, font1))
                If s_address2 <> "" Then
                    cell.AddElement(New Phrase(s_address2, font1))
                End If
                If s_address3 <> "" Then
                    cell.AddElement(New Phrase(s_address3, font1))
                End If
                cell.AddElement(New Phrase(s_citystatezip, font1))
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("Order ID", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(orderID, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Assets", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(assetID, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("Ordered By", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(c_contact, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Carrier", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(carrier, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("PO", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(purchaseorder, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Tracking", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(tracking, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 2
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                doc.Add(table)
                'end header table

                commandString = "select * from v_shipmentlines where shipmentID=@shipmentID"
                comm = New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@shipmentID", shipmentID)
                reader = comm.ExecuteReader

                'lines table
                table = New PdfPTable(12)
                table.WidthPercentage = 100

                'lines header row
                cell = New PdfPCell(New Phrase("Qty", font2))
                cell.Colspan = 1
                cell.Border = 0
                cell.HorizontalAlignment = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Item", font2))
                cell.Colspan = 5
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Ship", font2))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("BO", font2))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Price", font2))
                cell.Colspan = 1
                cell.HorizontalAlignment = 2
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font2))
                cell.Colspan = 1
                cell.HorizontalAlignment = 2
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Extended", font2))
                cell.Colspan = 2
                cell.HorizontalAlignment = 2
                cell.Border = 0
                table.AddCell(cell)
                'end header row

                Dim manufacturer, partnumber, item, quantity, ship_qty, price, extended, uom As String
                Dim backorder As Double = 0
                Dim subtotal As Double = 0
                Dim tax_subtotal As Double = 0
                Dim salestax As Double = 0
                Dim total As Double = 0
                While reader.Read
                    manufacturer = reader.Item("manufacturer")
                    partnumber = reader.Item("partnumber")
                    item = reader.Item("item")
                    quantity = reader.Item("quantity")
                    ship_qty = reader.Item("ship_qty")
                    backorder = appcode.GetBO(reader.Item("lineID"))
                    price = FormatCurrency(reader.Item("price"), 2)
                    uom = reader.Item("uom")
                    If ship_qty > 0 Or backorder > 0 Then
                        extended = FormatCurrency(reader.Item("price") * reader.Item("ship_qty"), 2)
                        subtotal += extended
                        If IsTaxable(reader.Item("manufacturer"), reader.Item("partnumber")) = True Then
                            tax_subtotal = tax_subtotal + extended
                        End If

                        'start line row
                        cell = New PdfPCell(New Phrase(quantity, font1))
                        cell.Border = 0
                        cell.Colspan = 1
                        cell.HorizontalAlignment = 1
                        table.AddCell(cell)
                        cell = New PdfPCell(New Phrase(manufacturer & " " & partnumber, font1))
                        cell.Border = 0
                        cell.Colspan = 5
                        table.AddCell(cell)
                        cell = New PdfPCell(New Phrase(ship_qty, font1))
                        cell.Border = 0
                        cell.Colspan = 1
                        cell.HorizontalAlignment = 1
                        table.AddCell(cell)
                        cell = New PdfPCell(New Phrase(backorder, font1))
                        cell.Border = 0
                        cell.Colspan = 1
                        cell.HorizontalAlignment = 1
                        table.AddCell(cell)
                        cell = New PdfPCell(New Phrase(price, font1))
                        cell.Border = 0
                        cell.Colspan = 1
                        cell.HorizontalAlignment = 2
                        table.AddCell(cell)
                        cell = New PdfPCell(New Phrase(uom, font1))
                        cell.Border = 0
                        cell.Colspan = 1
                        cell.HorizontalAlignment = 2
                        table.AddCell(cell)
                        cell = New PdfPCell(New Phrase(extended, font1))
                        cell.Border = 0
                        cell.Colspan = 2
                        cell.HorizontalAlignment = 2
                        table.AddCell(cell)

                        cell = New PdfPCell(New Phrase(" ", font1))
                        cell.Colspan = 1
                        cell.Border = 0
                        table.AddCell(cell)
                        cell = New PdfPCell(New Phrase(item, font1))
                        cell.Colspan = 11
                        cell.Border = 0
                        table.AddCell(cell)

                        cell = New PdfPCell(New Phrase(" ", font1))
                        cell.Colspan = 12
                        cell.Border = 0
                        table.AddCell(cell)

                        'end line row
                    End If
                End While

                reader.Close()
                If chargetax = True Then
                    salestax = tax_subtotal * GetSalesTax(companyID, vendorID)
                Else
                    salestax = 0
                End If
                total = subtotal + salestax + ship_charge

                table.HeaderRows = 1
                doc.Add(table)

                table = New PdfPTable(12)
                table.WidthPercentage = 100

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Sub Total", font2))
                cell.Colspan = 2
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(FormatCurrency(subtotal, 2), font2))
                cell.HorizontalAlignment = 2
                cell.Colspan = 2
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Freight", font2))
                cell.Colspan = 2
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(ship_charge, font2))
                cell.HorizontalAlignment = 2
                cell.Colspan = 2
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Sales Tax", font2))
                cell.Colspan = 2
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(FormatCurrency(salestax, 2), font2))
                cell.HorizontalAlignment = 2
                cell.Colspan = 2
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Total", font2))
                cell.Colspan = 2
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(FormatCurrency(total, 2), font2))
                cell.HorizontalAlignment = 2
                cell.Colspan = 2
                cell.Border = 0
                table.AddCell(cell)
                doc.Add(table)
                If x = 1 Then
                    doc.NewPage()
                End If
            Next

            'If kitnum <> 0 Then
            If 1 < 0 Then
                doc.NewPage()
                commandString = "select * from v_serviceprofile where serviceprofileID=@serviceprofileID"
                comm = New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@serviceprofileID", kitnum)
                reader = comm.ExecuteReader
                If reader.Read Then
                    companyID = reader.Item("companyID")
                    Dim company As String = GetCompany(reader.Item("companyID"))
                    assetID = reader.Item("assetID").ToString
                    Dim engine_oem As String = reader.Item("engine_oem").ToString
                    Dim engine_model As String = reader.Item("engine_model").ToString
                    Dim engine As String = engine_oem & " " & engine_model
                    Dim notes As String = reader.Item("notes").ToString
                    Dim name As String = reader.Item("name").ToString
                    Dim interval As String = reader.Item("interval").ToString & " " & reader.Item("interval_type")
                    Dim interval_type As String = reader.Item("interval_type").ToString
                    Dim servicenotes As String = reader.Item("servicenotes").ToString
                    Dim location = reader.Item("shipto").ToString
                    Dim address1 As String = reader.Item("s_address1").ToString
                    Dim address2 As String = reader.Item("s_address2").ToString
                    Dim address3 As String = reader.Item("s_address3").ToString
                    Dim city As String = reader.Item("s_city").ToString
                    Dim state As String = reader.Item("s_state").ToString
                    Dim zipcode As String = reader.Item("s_zipcode").ToString()
                    Dim hours_miles As String = reader.Item("hours_miles").ToString
                    Dim servicetype As String = reader.Item("servicetype") & " Service"
                    Dim fuel_type As String = reader.Item("fuel_type").ToString
                    Dim def As String = reader.Item("def").ToString
                    Dim kitcode As String = reader.Item("kitcode").ToString
                End If
                reader.Close()
                logo = Image.GetInstance(applicationpath + cimagefile)
                logo.Alignment = Element.ALIGN_CENTER
                doc.Add(logo)

                'header table
                Dim table = New PdfPTable(8)
                table.WidthPercentage = 100

                Dim cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("REF #", font3))
                cell.Colspan = 2
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("__________", font3))
                cell.Colspan = 6
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                commandString = "select * from v_serviceprofile where serviceprofileID=@serviceprofileID"
                comm = New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@serviceprofileID", kitnum)
                reader = comm.ExecuteReader
                If reader.Read Then
                    Dim equipment_oem As String = reader.Item("equipment_oem").ToString
                    Dim equipment_model As String = reader.Item("equipment_model").ToString
                    Dim equipment_description As String = reader.Item("equipment_description").ToString
                    Dim equipment_options As String = reader.Item("equipment_options").ToString
                    Dim equipment_vin As String = reader.Item("equipment_vin").ToString
                    Dim equipment_year As String = reader.Item("equipment_year").ToString


                    Dim title As String = ""
                    If equipment_year <> "" Then
                        title &= " " & equipment_year
                    End If
                    If equipment_oem <> "" Then
                        title &= " " & equipment_oem
                    End If
                    If equipment_model <> "" Then
                        title &= " " & equipment_model
                    End If
                    If equipment_description <> "" Then
                        title &= " " & equipment_description
                    End If

                    cell = New PdfPCell(New Phrase("ASSET", font3))
                    cell.Colspan = 2
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(reader.Item("assetID"), font3))
                    cell.Colspan = 6
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase("EQUIPMENT", font3))
                    cell.Colspan = 2
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(title, font3))
                    cell.Colspan = 6
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase("VIN/SERIAL", font3))
                    cell.Colspan = 2
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(equipment_vin, font3))
                    cell.Colspan = 6
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase("INTERVAL", font3))
                    cell.Colspan = 2
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(reader.Item("interval") & " " & reader.Item("interval_type"), font3))
                    cell.Colspan = 6
                    cell.Border = 0
                    table.AddCell(cell)

                End If
                reader.Close()

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                doc.Add(table)
                'end header table

                commandString = "select * from v_serviceprofile_parts where serviceprofileID=@serviceprofileID and part_type='Filter' and selected='True' order by manufacturer,partnumber"
                comm = New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@serviceprofileID", kitnum)
                reader = comm.ExecuteReader

                'lines table
                table = New PdfPTable(15)
                table.WidthPercentage = 100

                cell = New PdfPCell(New Phrase("CONTENTS", font3))
                cell.Colspan = 15
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font1))
                cell.Colspan = 15
                cell.Border = 0
                table.AddCell(cell)

                'lines header row
                cell = New PdfPCell(New Phrase("Part Number", font2))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Replaced By", font2))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Description", font2))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Qty", font2))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 0
                table.AddCell(cell)
                'end header row

                Dim description, alt_partnumber, oem_partnumber As String
                Dim partnumber As String = ""
                Dim price As Double = 0
                Dim uom As String = ""
                Dim quantity As Double = 0
                Dim partID As Integer
                While reader.Read
                    If reader.Item("selected") = True Then
                        quantity = reader.Item("quantity")
                    Else
                        quantity = "0"
                    End If
                    partID = reader.Item("partID")
                    partnumber = reader.Item("partnumber")
                    description = Left(reader.Item("description").ToString, 50)
                    price = reader.Item("price")
                    uom = reader.Item("uom")
                    If reader.Item("alt_manufacturer").ToString <> "" And reader.Item("alt_partnumber").ToString <> "" Then
                        alt_partnumber = reader.Item("alt_partnumber")
                    Else
                        alt_partnumber = ""
                    End If
                    If reader.Item("oem_manufacturer").ToString <> "" And reader.Item("oem_partnumber").ToString <> "" Then
                        oem_partnumber = reader.Item("oem_partnumber")
                    Else
                        oem_partnumber = ""
                    End If

                    cell = New PdfPCell(New Phrase(partnumber, font2))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase("___________", font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(description, font1))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(quantity, font1))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 0
                    table.AddCell(cell)

                End While
                reader.Close()

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 15
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("To make changes or re-order, please call:", font1))
                cell.Colspan = 15
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 15
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("DFO Filters & Equipment", font3))
                cell.Colspan = 15
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("5002 S 40th Street Ste C", font1))
                cell.Colspan = 15
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Phoenix, AZ 85040", font1))
                cell.Colspan = 15
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("480-295-1676", font3))
                cell.Colspan = 15
                cell.Border = 0
                table.AddCell(cell)

                table.HeaderRows = 4
                doc.Add(table)
            End If

            conn.Close()
            doc.Close()
        End If
    End Sub

    Public Shared Sub PackingSlipPDF(ByVal shipmentID As Integer, ByVal applicationpath As String, ByVal imagefile As String, ByVal cimagefile As String, ByVal filename As String)

        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim commandString As String = "select * from v_shipments where shipmentID=@shipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            Dim doc As Document = New Document(PageSize.LETTER)
            Dim invoiceID As String = reader.Item("invoiceID").ToString
            Dim orderID As String = reader.Item("orderID").ToString
            Dim customer As String = reader.Item("company").ToString
            Dim companyID As Integer = reader.Item("companyID").ToString
            Dim vendorID As Integer = reader.Item("vendorID").ToString
            Dim placedby As String = reader.Item("placedby").ToString
            Dim shipment_date As String = reader.Item("shipment_date").ToString
            Dim due_date As String = DateAdd(DateInterval.Day, 30, reader.Item("shipment_date"))
            Dim purchaseorder As String = reader.Item("purchaseorder").ToString
            Dim billto As String = reader.Item("company").ToString
            Dim b_address1 As String = reader.Item("b_address1").ToString
            Dim b_address2 As String = reader.Item("b_address2").ToString
            Dim b_citystatezip As String = reader.Item("b_city") & ", " & reader.Item("b_state") & " " & reader.Item("b_zipcode")
            Dim shipto As String = reader.Item("shipto").ToString
            Dim s_address1 As String = reader.Item("s_address1").ToString
            Dim s_address2 As String = reader.Item("s_address2").ToString
            Dim s_address3 As String = reader.Item("s_address3").ToString
            Dim s_citystatezip As String = reader.Item("s_city") & ", " & reader.Item("s_state") & " " & reader.Item("s_zipcode")
            Dim c_contact As String = reader.Item("c_contact").ToString
            Dim c_phone As String = reader.Item("c_phone").ToString
            Dim c_fax As String = reader.Item("c_fax").ToString
            Dim chargetax As Boolean = reader.Item("chargetax")
            Dim ship_method As String = reader.Item("ship_method").ToString
            Dim ship_options As String = reader.Item("ship_options").ToString
            Dim carrier As String = reader.Item("carrier")
            Dim ship_charge As String = FormatCurrency(reader.Item("ship_charge"), 2)
            Dim tracking As String = reader.Item("tracking").ToString
            Dim assetID As String = ""
            Dim kitnum As Integer = 0
            Dim kitID As Array
            If reader.Item("serviceprofileID").ToString <> "0" Then
                kitID = Split(reader.Item("serviceprofileID"), ",")
                Dim numkits As Integer = kitID.GetLength(0)
                Dim y As Integer = 0
                For x As Integer = 0 To numkits - 1
                    If y > 0 Then
                        assetID &= ","
                    End If
                    assetID &= appcode.getAssetID(kitID(x))
                    y += 1
                Next
                kitnum = kitID(0)
            Else
                assetID = "NA"
            End If
            Dim kit As String = "No"
            If reader.Item("is_kit") = True Then
                kit = "Yes"
            End If

            reader.Close()
            Dim logo As Image = Image.GetInstance(applicationpath + imagefile)
            Dim basefont1 As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, False)
            Dim font1 As Font = FontFactory.GetFont("Century Gothic", 10)
            Dim font2 As Font = FontFactory.GetFont("Century Gothic", 10, Font.BOLD)
            Dim font3 As Font = FontFactory.GetFont("Century Gothic", 18, Font.BOLD)
            Dim font4 As Font = FontFactory.GetFont("Century Gothic", 8, Font.ITALIC)

            PdfWriter.GetInstance(doc, New FileStream(applicationpath + filename, FileMode.Create))
            doc.Open()
            doc.SetPageSize(PageSize.A4)
            doc.SetMargins(36, 36, 72, 72)
            doc.SetMarginMirroring(True)

            logo.Alignment = Element.ALIGN_LEFT
            doc.Add(logo)

            Dim cell As PdfPCell

            'header table
            Dim table As PdfPTable = New PdfPTable(8)
            table.WidthPercentage = 100

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Packing Slip", font3))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Invoice", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(invoiceID, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            'header row 0
            cell = New PdfPCell(New Phrase("Ship Date", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(shipment_date, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("From", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("DFO Filters & Equipment", font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" ", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("5002 S 40th Street Ste C", font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" ", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Phoenix, AZ 85040", font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Bill To", font2))
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell()
            cell.Colspan = 3
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Border = 0
            cell.AddElement(New Phrase(billto, font1))
            cell.AddElement(New Phrase(b_address1, font1))
            If b_address2 <> "" Then
                cell.AddElement(New Phrase(b_address2, font1))
            End If
            cell.AddElement(New Phrase(b_citystatezip, font1))
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Ship To", font2))
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell()
            cell.Colspan = 3
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Border = 0
            cell.AddElement(New Phrase(shipto, font1))
            cell.AddElement(New Phrase(s_address1, font1))
            If s_address2 <> "" Then
                cell.AddElement(New Phrase(s_address2, font1))
            End If
            If s_address3 <> "" Then
                cell.AddElement(New Phrase(s_address3, font1))
            End If
            cell.AddElement(New Phrase(s_citystatezip, font1))
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Order ID", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(orderID, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Assets", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(assetID, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Ordered By", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(c_contact, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Carrier", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(carrier, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("PO", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(purchaseorder, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Tracking", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(tracking, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 2
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            doc.Add(table)
            'end header table

            commandString = "select * from v_shipmentlines where shipmentID=@shipmentID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@shipmentID", shipmentID)
            reader = comm.ExecuteReader

            'lines table
            table = New PdfPTable(12)
            table.WidthPercentage = 100

            'lines header row
            cell = New PdfPCell(New Phrase("Item", font2))
            cell.Colspan = 5
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Qty", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font2))
            cell.Colspan = 2
            cell.Border = 0
            cell.HorizontalAlignment = 1
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Ship", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("BO", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            'end header row

            Dim manufacturer, partnumber, item, quantity, ship_qty, price, extended, uom As String
            Dim backorder As Double = 0
            Dim subtotal As Double = 0
                Dim tax_subtotal As Double = 0
                Dim salestax As Double = 0
                Dim total As Double = 0
                While reader.Read
                    manufacturer = reader.Item("manufacturer")
                    partnumber = reader.Item("partnumber")
                    item = reader.Item("item")
                    quantity = reader.Item("quantity")
                    ship_qty = reader.Item("ship_qty")
                    backorder = appcode.GetBO(reader.Item("lineID"))
                    price = FormatCurrency(reader.Item("price"), 2)
                    uom = reader.Item("uom")
                    If ship_qty > 0 Or backorder > 0 Then
                        extended = FormatCurrency(reader.Item("price") * reader.Item("ship_qty"), 2)
                        subtotal += extended
                        If IsTaxable(reader.Item("manufacturer"), reader.Item("partnumber")) = True Then
                            tax_subtotal = tax_subtotal + extended
                        End If

                    'start line row
                    cell = New PdfPCell(New Phrase(manufacturer & " " & partnumber, font1))
                    cell.Border = 0
                        cell.Colspan = 5
                        table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Border = 0
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(quantity, font1))
                    cell.Border = 0
                        cell.Colspan = 1
                        cell.HorizontalAlignment = 1
                        table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Border = 0
                    cell.Colspan = 2
                    cell.HorizontalAlignment = 1
                        table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Border = 0
                        cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(ship_qty, font1))
                    cell.Border = 0
                        cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(backorder, font1))
                    cell.Border = 0
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(item, font1))
                    cell.Colspan = 12
                    cell.Border = 0
                        table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" ", font1))
                        cell.Colspan = 12
                        cell.Border = 0
                        table.AddCell(cell)

                        'end line row
                    End If
                End While

                reader.Close()
                If chargetax = True Then
                    salestax = tax_subtotal * GetSalesTax(companyID, vendorID)
                Else
                    salestax = 0
                End If
                total = subtotal + salestax + ship_charge

                table.HeaderRows = 1
                doc.Add(table)


            conn.Close()
            doc.Close()
        End If
    End Sub

    Public Shared Sub ServiceKitPDF(ByVal serviceprofileID As Integer, ByVal applicationpath As String, ByVal filename As String, ByVal imagefile As String, ByVal all_parts As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim commandString As String = "select * from v_serviceprofile where serviceprofileID=@serviceprofileID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            Dim doc As Document = New Document(PageSize.LETTER)
            Dim companyID As Integer = reader.Item("companyID")
            Dim company As String = GetCompany(reader.Item("companyID"))
            Dim assetID As String = reader.Item("assetID").ToString
            Dim engine_oem As String = reader.Item("engine_oem").ToString
            Dim engine_model As String = reader.Item("engine_model").ToString
            Dim engine As String = engine_oem & " " & engine_model
            Dim notes As String = reader.Item("notes").ToString
            Dim name As String = reader.Item("name").ToString
            Dim interval As String = reader.Item("interval").ToString & " " & reader.Item("interval_type")
            Dim interval_type As String = reader.Item("interval_type").ToString
            Dim servicenotes As String = reader.Item("servicenotes").ToString
            Dim location = reader.Item("shipto").ToString
            Dim address1 As String = reader.Item("s_address1").ToString
            Dim address2 As String = reader.Item("s_address2").ToString
            Dim address3 As String = reader.Item("s_address3").ToString
            Dim city As String = reader.Item("s_city").ToString
            Dim state As String = reader.Item("s_state").ToString
            Dim zipcode As String = reader.Item("s_zipcode").ToString()
            Dim hours_miles As String = reader.Item("hours_miles").ToString
            Dim servicetype As String = reader.Item("servicetype") & " Service"
            Dim fuel_type As String = reader.Item("fuel_type").ToString
            Dim def As String = reader.Item("def").ToString
            Dim kitcode As String = reader.Item("kitcode").ToString
            reader.Close()
            Dim kitID As String = serviceprofileID

            PdfWriter.GetInstance(doc, New FileStream(applicationpath + filename, FileMode.Create))
            Dim basefont1 As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, False)
            Dim font1 As Font = FontFactory.GetFont("Century Gothic", 10)
            Dim font2 As Font = FontFactory.GetFont("Century Gothic", 10, Font.BOLD)
            Dim font3 As Font = FontFactory.GetFont("Century Gothic", 14, Font.BOLD)
            Dim font4 As Font = FontFactory.GetFont("Century Gothic", 14, Font.BOLD)

            doc.Open()
            doc.SetPageSize(PageSize.A4)
            doc.SetMargins(36, 36, 72, 114)
            doc.SetMarginMirroring(True)

            Dim logo As Image = Image.GetInstance(applicationpath + imagefile)
            logo.Alignment = Element.ALIGN_CENTER
            doc.Add(logo)

            Dim cell As PdfPCell

            'header table
            Dim table As PdfPTable = New PdfPTable(8)
            table.WidthPercentage = 100

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("REF #", font3))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("__________", font3))
            cell.Colspan = 6
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            commandString = "select * from v_serviceprofile where serviceprofileID=@serviceprofileID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
            reader = comm.ExecuteReader
            If reader.Read Then
                Dim equipment_oem As String = reader.Item("equipment_oem").ToString
                Dim equipment_model As String = reader.Item("equipment_model").ToString
                Dim equipment_description As String = reader.Item("equipment_description").ToString
                Dim equipment_options As String = reader.Item("equipment_options").ToString
                Dim equipment_vin As String = reader.Item("equipment_vin").ToString
                Dim equipment_year As String = reader.Item("equipment_year").ToString


                Dim title As String = ""
                If equipment_year <> "" Then
                    title &= " " & equipment_year
                End If
                If equipment_oem <> "" Then
                    title &= " " & equipment_oem
                End If
                If equipment_model <> "" Then
                    title &= " " & equipment_model
                End If
                If equipment_description <> "" Then
                    title &= " " & equipment_description
                End If

                cell = New PdfPCell(New Phrase("ASSET", font3))
                cell.Colspan = 2
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(reader.Item("assetID"), font3))
                cell.Colspan = 6
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("EQUIPMENT", font3))
                cell.Colspan = 2
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(title, font3))
                cell.Colspan = 6
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("VIN/SERIAL", font3))
                cell.Colspan = 2
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(equipment_vin, font3))
                cell.Colspan = 6
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("INTERVAL", font3))
                cell.Colspan = 2
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(reader.Item("interval") & " " & reader.Item("interval_type"), font3))
                cell.Colspan = 6
                cell.Border = 0
                table.AddCell(cell)

            End If
            reader.Close()

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            doc.Add(table)
            'end header table

            all_parts = False
            If all_parts = True Then
                commandString = "select * from v_serviceprofile_parts where serviceprofileID=@serviceprofileID and part_type='Filter' order by manufacturer,partnumber"
            Else
                commandString = "select * from v_serviceprofile_parts where serviceprofileID=@serviceprofileID and part_type='Filter' and selected='True' order by manufacturer,partnumber"
            End If
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
            reader = comm.ExecuteReader

            'lines table
            table = New PdfPTable(15)
            table.WidthPercentage = 100

            cell = New PdfPCell(New Phrase("CONTENTS", font3))
            cell.Colspan = 15
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font1))
            cell.Colspan = 15
            cell.Border = 0
            table.AddCell(cell)

            'lines header row
            cell = New PdfPCell(New Phrase("Part Number", font2))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Replaced By", font2))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Description", font2))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Qty", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            'end header row

            Dim partnumber, description, quantity, price, uom, alt_partnumber, oem_partnumber As String
            Dim partID As Integer
            While reader.Read
                If reader.Item("selected") = True Then
                    quantity = reader.Item("quantity")
                Else
                    quantity = "0"
                End If
                partID = reader.Item("partID")
                partnumber = reader.Item("partnumber")
                description = Left(reader.Item("description").ToString, 50)
                price = reader.Item("price")
                uom = reader.Item("uom")
                If reader.Item("alt_manufacturer").ToString <> "" And reader.Item("alt_partnumber").ToString <> "" Then
                    alt_partnumber = reader.Item("alt_partnumber")
                Else
                    alt_partnumber = ""
                End If
                If reader.Item("oem_manufacturer").ToString <> "" And reader.Item("oem_partnumber").ToString <> "" Then
                    oem_partnumber = reader.Item("oem_partnumber")
                Else
                    oem_partnumber = ""
                End If

                cell = New PdfPCell(New Phrase(partnumber, font2))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("___________", font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(description, font1))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(quantity, font1))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 0
                table.AddCell(cell)

            End While
            reader.Close()

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 15
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("To make changes or re-order, please call:", font1))
            cell.Colspan = 15
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 15
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("DFO Filters & Equipment", font3))
            cell.Colspan = 15
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("5002 S. 40th Street Ste C", font1))
            cell.Colspan = 15
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Phoenix, AZ 85040", font1))
            cell.Colspan = 15
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("480-295-1676", font3))
            cell.Colspan = 15
            cell.Border = 0
            table.AddCell(cell)

            table.HeaderRows = 4
            doc.Add(table)

            doc.Close()
            conn.Close()
        End If
    End Sub

    Public Shared Sub PickTicketPDF(ByVal orderID As Integer, ByVal assets As String, ByVal applicationpath As String, ByVal imagefile As String, ByVal filename As String)
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim commandString As String = "select * from t_order where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            Dim doc As Document = New Document(PageSize.LETTER)
            Dim customer As String = reader.Item("company")
            Dim placedby As String = reader.Item("placedby")
            Dim order_date As String = reader.Item("order_date")
            Dim deliverby_date As String = reader.Item("deliverby_date")
            Dim confirm_date As String = reader.Item("confirm_date")
            Dim purchaseorder As String = reader.Item("purchaseorder")
            Dim billto As String = reader.Item("company")
            Dim b_address1 As String = reader.Item("b_address1").ToString
            Dim b_address2 As String = reader.Item("b_address2").ToString
            Dim b_citystatezip As String = reader.Item("b_city") & ", " & reader.Item("b_state") & " " & reader.Item("b_zipcode")
            Dim shipto As String = reader.Item("shipto")
            Dim s_address1 As String = reader.Item("s_address1").ToString
            Dim s_address2 As String = reader.Item("s_address2").ToString
            Dim s_address3 As String = reader.Item("s_address3").ToString
            Dim s_citystatezip As String = reader.Item("s_city") & ", " & reader.Item("s_state") & " " & reader.Item("s_zipcode")
            Dim c_contact As String = reader.Item("c_contact").ToString
            Dim c_phone As String = reader.Item("c_phone").ToString
            Dim c_fax As String = reader.Item("c_fax").ToString
            Dim backorder As String = ""
            If IsBO(orderID) = True Then
                backorder = "BACKORDER"
            End If
            Dim salesrep As String = GetUserName(GetRepID(reader.Item("companyID")))
            Dim ship_method As String = reader.Item("ship_method").ToString
            Dim ship_options As String = reader.Item("ship_options").ToString
            Dim kit As String = "No"
            Dim isKit As Boolean = False
            If reader.Item("is_kit") = True Then
                kit = "Yes"
                isKit = True
            End If
            Dim serviceprofileID As Integer = reader.Item("serviceprofileID")
            Dim hours_miles As Double = reader.Item("hours_miles")
            Dim notes As String = reader.Item("notes")
            reader.Close()

            Dim equipmentID As Integer = GetEquipmentIDFromKitID(serviceprofileID)
            Dim assetID As String = getAssetID(serviceprofileID)
            Dim equipment As String = GetEquipment(equipmentID)
            Dim pm_name As String = ""
            Dim pm_interval As Double = 0

            commandString = "select * from t_serviceprofile where serviceprofileID=@serviceprofileID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileID)
            reader = comm.ExecuteReader
            If reader.Read Then
                pm_name = reader.Item("name")
                pm_interval = reader.Item("interval")
            End If
            reader.Close()

            PdfWriter.GetInstance(doc, New FileStream(applicationpath + filename, FileMode.Create))
            Dim basefont1 As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, False)
            Dim font1 As Font = FontFactory.GetFont("Century Gothic", 10)
            Dim font2 As Font = FontFactory.GetFont("Century Gothic", 10, Font.BOLD)
            Dim font3 As Font = FontFactory.GetFont("Century Gothic", 18, Font.BOLD)
            Dim font4 As Font = FontFactory.GetFont("Century Gothic", 12, Font.BOLD, BaseColor.RED)

            doc.Open()
            doc.SetPageSize(PageSize.A4)
            doc.SetMargins(36, 36, 72, 72)
            doc.SetMarginMirroring(True)

            'Dim logo As Image = Image.GetInstance(imagefile)
            'doc.Add(logo)

            Dim cell As PdfPCell

            'header table
            Dim table As PdfPTable = New PdfPTable(8)
            table.WidthPercentage = 100

            cell = New PdfPCell(New Phrase("Pick Ticket", font3))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(backorder, font4))
            cell.Colspan = 6
            cell.Border = 0
            table.AddCell(cell)


            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Notes"))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(notes, font1))
            cell.Colspan = 7
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            'header row 0
            cell = New PdfPCell(New Phrase("Order ID", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(orderID, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Placed By", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(placedby, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Ordered", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(order_date, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Sales Rep", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(salesrep, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Ship To", font2))
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell()
            cell.Colspan = 3
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Border = 0
            cell.AddElement(New Phrase(billto, font1))
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Carrier", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(ship_method & " (" & ship_options & ")", font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("", font2))
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell()
            cell.Colspan = 7
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Border = 0
            cell.AddElement(New Phrase(shipto, font1))
            cell.AddElement(New Phrase(s_address1, font1))
            If s_address2 <> "" Then
                cell.AddElement(New Phrase(s_address2, font1))
            End If
            If s_address3 <> "" Then
                cell.AddElement(New Phrase(s_address3, font1))
            End If
            cell.AddElement(New Phrase(s_citystatezip, font1))
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("PO", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(purchaseorder, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Asset ID", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(assetID, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Contact", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(c_contact, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Kit", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(pm_name, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Phone", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(c_phone, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Interval", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(pm_interval, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 4
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Hours/Miles", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(hours_miles, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            doc.Add(table)
            'end header table

            commandString = "select * from t_order_line where orderID=@orderID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@orderID", orderID)
            reader = comm.ExecuteReader

            'lines table
            table = New PdfPTable(12)
            table.WidthPercentage = 100

            'lines header row
            cell = New PdfPCell(New Phrase("Part Number", font2))
            cell.Colspan = 6
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Qty", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("BO", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Ship", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Price", font2))
            cell.Colspan = 2
            cell.HorizontalAlignment = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("UOM", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            'end header row

            Dim manufacturer, partnumber, item, quantity, price, uom As String
            Dim qty As Double = 0
            Dim shipqty As Double = 0
            Dim subtotal As Double = 0
            Dim openqty As Double = 0
            While reader.Read
                partnumber = reader.Item("manufacturer") & " " & reader.Item("partnumber")
                If reader.Item("kitID") <> "0" Then
                    partnumber &= " (Kit " & reader.Item("kitID") & ")"
                End If
                item = reader.Item("item")
                quantity = reader.Item("quantity")
                qty = quantity
                shipqty = GetShipQty(reader.Item("lineID"))
                openqty = qty - shipqty
                uom = reader.Item("uom")
                price = FormatCurrency(reader.Item("price"), 2)

                If openqty > 0 Then
                    quantity = CStr(openqty)
                    'start line row
                    cell = New PdfPCell(New Phrase(partnumber, font1))
                    cell.Colspan = 6
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(quantity, font1))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(price, font1))
                    cell.Colspan = 2
                    cell.HorizontalAlignment = 2
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(uom, font1))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 1
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(item, font1))
                    cell.Colspan = 12
                    cell.Border = 0
                    table.AddCell(cell)
                    'end line row
                End If
            End While

            reader.Close()

            table.HeaderRows = 1
            doc.Add(table)

            table = New PdfPTable(8)
            table.WidthPercentage = 100

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Picked By", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("________", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Invoice #", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("________", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Loaded By", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("________", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Invoice Amount", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("________", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Invoiced By", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("________", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Received By", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("______________________", font2))
            cell.Colspan = 6
            cell.Border = 0
            table.AddCell(cell)

            doc.Add(table)

            Dim req_on As Boolean = False

            If req_on = True Then
                doc.NewPage()
                doc.SetPageSize(PageSize.A4)
                doc.SetMargins(36, 36, 72, 72)
                doc.SetMarginMirroring(True)

                table = New PdfPTable(8)
                table.WidthPercentage = 100

                cell = New PdfPCell(New Phrase("Requisition", font3))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                'header row 0
                cell = New PdfPCell(New Phrase("Order ID", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(orderID, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Placed By", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(placedby, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("Ordered", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(order_date, font1))
                cell.Colspan = 7
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("Bill To", font2))
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell()
                cell.Colspan = 3
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                cell.Border = 0
                cell.AddElement(New Phrase(billto, font1))
                cell.AddElement(New Phrase(b_address1, font1))
                If b_address2 <> "" Then
                    cell.AddElement(New Phrase(b_address2, font1))
                End If
                cell.AddElement(New Phrase(b_citystatezip, font1))
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Ship To", font2))
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell()
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                cell.Colspan = 3
                cell.Border = 0
                cell.AddElement(New Phrase(shipto, font1))
                cell.AddElement(New Phrase(s_address1, font1))
                If s_address2 <> "" Then
                    cell.AddElement(New Phrase(s_address2, font1))
                End If
                If s_address3 <> "" Then
                    cell.AddElement(New Phrase(s_address3, font1))
                End If
                cell.AddElement(New Phrase(s_citystatezip, font1))
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("PO", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(purchaseorder, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Accepted", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(confirm_date, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("Contact", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(c_contact, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Ship Method", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(ship_method, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("Phone", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(c_phone, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Options", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(ship_options, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                doc.Add(table)
                'end header table

                commandString = "select * from t_order_line where orderID=@orderID"
                comm = New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@orderID", orderID)
                reader = comm.ExecuteReader

                'lines table
                table = New PdfPTable(12)
                table.WidthPercentage = 100

                'lines header row
                cell = New PdfPCell(New Phrase("Manufacturer", font2))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Part Number", font2))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Qty", font2))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("BO", font2))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Ship", font2))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Price", font2))
                cell.Colspan = 2
                cell.HorizontalAlignment = 2
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("UOM", font2))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 0
                table.AddCell(cell)
                'end header row

                subtotal = 0
                While reader.Read
                    manufacturer = reader.Item("manufacturer")
                    partnumber = reader.Item("partnumber")
                    item = reader.Item("item")
                    quantity = reader.Item("quantity")
                    uom = reader.Item("uom")
                    price = FormatCurrency(reader.Item("price"), 2)

                    'start line row
                    cell = New PdfPCell(New Phrase(manufacturer, font1))
                    cell.Colspan = 3
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(partnumber, font1))
                    cell.Colspan = 3
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(quantity, font1))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(price, font1))
                    cell.Colspan = 2
                    cell.HorizontalAlignment = 2
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(uom, font1))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 1
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(item, font1))
                    cell.Colspan = 9
                    cell.Border = 0
                    table.AddCell(cell)
                    'end line row

                End While

                reader.Close()
                table.HeaderRows = 1
                doc.Add(table)
            End If

            conn.Close()
            doc.Close()
        End If
    End Sub

    Public Shared Sub OriginalPickTicketPDF(ByVal orderID As Integer, ByVal assets As String, ByVal applicationpath As String, ByVal imagefile As String, ByVal filename As String)
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim commandString As String = "select * from t_order where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            Dim doc As Document = New Document(PageSize.LETTER)
            Dim customer As String = reader.Item("company")
            Dim placedby As String = reader.Item("placedby")
            Dim order_date As String = reader.Item("order_date")
            Dim deliverby_date As String = reader.Item("deliverby_date")
            Dim confirm_date As String = reader.Item("confirm_date")
            Dim purchaseorder As String = reader.Item("purchaseorder")
            Dim billto As String = reader.Item("company")
            Dim b_address1 As String = reader.Item("b_address1").ToString
            Dim b_address2 As String = reader.Item("b_address2").ToString
            Dim b_citystatezip As String = reader.Item("b_city") & ", " & reader.Item("b_state") & " " & reader.Item("b_zipcode")
            Dim shipto As String = reader.Item("shipto")
            Dim s_address1 As String = reader.Item("s_address1").ToString
            Dim s_address2 As String = reader.Item("s_address2").ToString
            Dim s_address3 As String = reader.Item("s_address3").ToString
            Dim s_citystatezip As String = reader.Item("s_city") & ", " & reader.Item("s_state") & " " & reader.Item("s_zipcode")
            Dim c_contact As String = reader.Item("c_contact").ToString
            Dim c_phone As String = reader.Item("c_phone").ToString
            Dim c_fax As String = reader.Item("c_fax").ToString
            Dim backorder As String = ""
            Dim salesrep As String = GetUserName(GetRepID(reader.Item("companyID")))
            Dim ship_method As String = reader.Item("ship_method").ToString
            Dim ship_options As String = reader.Item("ship_options").ToString
            Dim kit As String = "No"
            Dim isKit As Boolean = False
            If reader.Item("is_kit") = True Then
                kit = "Yes"
                isKit = True
            End If

            reader.Close()

            PdfWriter.GetInstance(doc, New FileStream(applicationpath + filename, FileMode.Create))
            Dim basefont1 As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, False)
            Dim font1 As Font = FontFactory.GetFont("Century Gothic", 10)
            Dim font2 As Font = FontFactory.GetFont("Century Gothic", 10, Font.BOLD)
            Dim font3 As Font = FontFactory.GetFont("Century Gothic", 18, Font.BOLD)
            Dim font4 As Font = FontFactory.GetFont("Century Gothic", 12, Font.BOLD, BaseColor.RED)

            doc.Open()
            doc.SetPageSize(PageSize.A4)
            doc.SetMargins(36, 36, 72, 72)
            doc.SetMarginMirroring(True)

            'Dim logo As Image = Image.GetInstance(imagefile)
            'doc.Add(logo)

            Dim cell As PdfPCell

            'header table
            Dim table As PdfPTable = New PdfPTable(8)
            table.WidthPercentage = 100

            cell = New PdfPCell(New Phrase("Pick Ticket", font3))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(backorder, font4))
            cell.Colspan = 6
            cell.Border = 0
            table.AddCell(cell)


            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            'header row 0
            cell = New PdfPCell(New Phrase("Order ID", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(orderID, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Placed By", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(placedby, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Ordered", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(order_date, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Sales Rep", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(salesrep, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Bill To", font2))
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell()
            cell.Colspan = 3
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Border = 0
            cell.AddElement(New Phrase(billto, font1))
            cell.AddElement(New Phrase(b_address1, font1))
            If b_address2 <> "" Then
                cell.AddElement(New Phrase(b_address2, font1))
            End If
            cell.AddElement(New Phrase(b_citystatezip, font1))
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Ship To", font2))
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell()
            cell.Colspan = 3
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Border = 0
            cell.AddElement(New Phrase(shipto, font1))
            cell.AddElement(New Phrase(s_address1, font1))
            If s_address2 <> "" Then
                cell.AddElement(New Phrase(s_address2, font1))
            End If
            If s_address3 <> "" Then
                cell.AddElement(New Phrase(s_address3, font1))
            End If
            cell.AddElement(New Phrase(s_citystatezip, font1))
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("PO", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(purchaseorder, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Assets", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(assets, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Contact", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(c_contact, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Ship Method", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(ship_method, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Phone", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(c_phone, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Options", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(ship_options, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            doc.Add(table)
            'end header table

            commandString = "select * from t_order_line where orderID=@orderID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@orderID", orderID)
            reader = comm.ExecuteReader

            'lines table
            table = New PdfPTable(12)
            table.WidthPercentage = 100

            'lines header row
            cell = New PdfPCell(New Phrase("Part Number", font2))
            cell.Colspan = 6
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Qty", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("BO", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Ship", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Price", font2))
            cell.Colspan = 2
            cell.HorizontalAlignment = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("UOM", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            'end header row

            Dim manufacturer, partnumber, item, quantity, price, uom As String
            Dim qty As Double = 0
            Dim shipqty As Double = 0
            Dim subtotal As Double = 0
            Dim openqty As Double = 0
            While reader.Read
                partnumber = reader.Item("manufacturer") & " " & reader.Item("partnumber")
                If reader.Item("kitID") <> "0" Then
                    partnumber &= " (Kit " & reader.Item("kitID") & ")"
                End If
                item = reader.Item("item")
                quantity = reader.Item("quantity")
                qty = quantity
                shipqty = GetShipQty(reader.Item("lineID"))
                openqty = qty - shipqty
                uom = reader.Item("uom")
                price = FormatCurrency(reader.Item("price"), 2)

                'start line row
                cell = New PdfPCell(New Phrase(partnumber, font1))
                cell.Colspan = 6
                cell.Border = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(quantity, font1))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font1))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font1))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(price, font1))
                cell.Colspan = 2
                cell.HorizontalAlignment = 2
                cell.Border = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(uom, font1))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 1
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(item, font1))
                cell.Colspan = 12
                cell.Border = 0
                table.AddCell(cell)
                'end line row
            End While

            reader.Close()

            table.HeaderRows = 1
            doc.Add(table)

            table = New PdfPTable(8)
            table.WidthPercentage = 100

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Picked By", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("________", font2))
            cell.Colspan = 6
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Invoice #", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("________", font2))
            cell.Colspan = 6
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Invoice Amount", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("________", font2))
            cell.Colspan = 6
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Shipped By", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("________", font2))
            cell.Colspan = 6
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Received By", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("______________________", font2))
            cell.Colspan = 6
            cell.Border = 0
            table.AddCell(cell)

            doc.Add(table)

            Dim req_on As Boolean = False

            If req_on = True Then
                doc.NewPage()
                doc.SetPageSize(PageSize.A4)
                doc.SetMargins(36, 36, 72, 72)
                doc.SetMarginMirroring(True)

                table = New PdfPTable(8)
                table.WidthPercentage = 100

                cell = New PdfPCell(New Phrase("Requisition", font3))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                'header row 0
                cell = New PdfPCell(New Phrase("Order ID", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(orderID, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Placed By", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(placedby, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("Ordered", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(order_date, font1))
                cell.Colspan = 7
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("Bill To", font2))
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell()
                cell.Colspan = 3
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                cell.Border = 0
                cell.AddElement(New Phrase(billto, font1))
                cell.AddElement(New Phrase(b_address1, font1))
                If b_address2 <> "" Then
                    cell.AddElement(New Phrase(b_address2, font1))
                End If
                cell.AddElement(New Phrase(b_citystatezip, font1))
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Ship To", font2))
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell()
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                cell.Colspan = 3
                cell.Border = 0
                cell.AddElement(New Phrase(shipto, font1))
                cell.AddElement(New Phrase(s_address1, font1))
                If s_address2 <> "" Then
                    cell.AddElement(New Phrase(s_address2, font1))
                End If
                If s_address3 <> "" Then
                    cell.AddElement(New Phrase(s_address3, font1))
                End If
                cell.AddElement(New Phrase(s_citystatezip, font1))
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("PO", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(purchaseorder, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Accepted", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(confirm_date, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("Contact", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(c_contact, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Ship Method", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(ship_method, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("Phone", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(c_phone, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Options", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(ship_options, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                doc.Add(table)
                'end header table

                commandString = "select * from t_order_line where orderID=@orderID"
                comm = New SqlCommand(commandString, conn)
                comm.Parameters.AddWithValue("@orderID", orderID)
                reader = comm.ExecuteReader

                'lines table
                table = New PdfPTable(12)
                table.WidthPercentage = 100

                'lines header row
                cell = New PdfPCell(New Phrase("Manufacturer", font2))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Part Number", font2))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Qty", font2))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("BO", font2))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Ship", font2))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Price", font2))
                cell.Colspan = 2
                cell.HorizontalAlignment = 2
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("UOM", font2))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 0
                table.AddCell(cell)
                'end header row

                subtotal = 0
                While reader.Read
                    manufacturer = reader.Item("manufacturer")
                    partnumber = reader.Item("partnumber")
                    item = reader.Item("item")
                    quantity = reader.Item("quantity")
                    uom = reader.Item("uom")
                    price = FormatCurrency(reader.Item("price"), 2)

                    'start line row
                    cell = New PdfPCell(New Phrase(manufacturer, font1))
                    cell.Colspan = 3
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(partnumber, font1))
                    cell.Colspan = 3
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(quantity, font1))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(price, font1))
                    cell.Colspan = 2
                    cell.HorizontalAlignment = 2
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(uom, font1))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 1
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(item, font1))
                    cell.Colspan = 9
                    cell.Border = 0
                    table.AddCell(cell)
                    'end line row

                End While

                reader.Close()
                table.HeaderRows = 1
                doc.Add(table)
            End If

            conn.Close()
            doc.Close()
        End If
    End Sub

    Public Shared Sub ReceivingTicketPDF(ByVal poID As Integer, ByVal applicationpath As String, ByVal imagefile As String, ByVal filename As String)
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim commandString As String = "select * from t_po where poID=@poID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@poID", poID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            Dim doc As Document = New Document(PageSize.LETTER)
            Dim supplier As String = reader.Item("supplier")
            Dim placedby As String = reader.Item("username")
            Dim po_date As String = reader.Item("date_submitted")
            Dim estimated_arrival As String = reader.Item("estimated_arrival")
            Dim po As String = reader.Item("po")
            Dim sup_address1 As String = reader.Item("sup_address1").ToString
            Dim sup_address2 As String = reader.Item("sup_address2").ToString
            Dim sup_citystatezip As String = reader.Item("sup_city") & ", " & reader.Item("sup_state") & " " & reader.Item("sup_zipcode")
            Dim sup_contact As String = reader.Item("sup_contact").ToString
            Dim sup_phone As String = reader.Item("sup_phone").ToString
            Dim sup_fax As String = reader.Item("sup_fax").ToString
            Dim shipto As String = reader.Item("shipto")
            Dim ship_address1 As String = reader.Item("ship_address1").ToString
            Dim ship_address2 As String = reader.Item("ship_address2").ToString
            Dim ship_citystatezip As String = reader.Item("ship_city") & ", " & reader.Item("ship_state") & " " & reader.Item("ship_zipcode")
            Dim ship_contact As String = reader.Item("ship_contact").ToString
            Dim ship_phone As String = reader.Item("ship_phone").ToString
            Dim ship_fax As String = reader.Item("ship_fax").ToString
            Dim backorder As String = ""
            If IsPOBO(poID) = True Then
                backorder = "BACKORDER"
            End If
            reader.Close()

            PdfWriter.GetInstance(doc, New FileStream(applicationpath + filename, FileMode.Create))
            Dim basefont1 As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, False)
            Dim font1 As Font = FontFactory.GetFont("Century Gothic", 10)
            Dim font2 As Font = FontFactory.GetFont("Century Gothic", 10, Font.BOLD)
            Dim font3 As Font = FontFactory.GetFont("Century Gothic", 18, Font.BOLD)
            Dim font4 As Font = FontFactory.GetFont("Century Gothic", 12, Font.BOLD, BaseColor.RED)

            doc.Open()
            doc.SetPageSize(PageSize.A4)
            doc.SetMargins(36, 36, 72, 72)
            doc.SetMarginMirroring(True)

            'Dim logo As Image = Image.GetInstance(imagefile)
            'doc.Add(logo)

            Dim cell As PdfPCell

            'header table
            Dim table As PdfPTable = New PdfPTable(8)
            table.WidthPercentage = 100

            cell = New PdfPCell(New Phrase("Receiving Ticket", font3))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)


            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            'header row 0
            cell = New PdfPCell(New Phrase("ID", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(poID, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Placed By", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(placedby, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Ordered", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(po_date, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Supplier", font2))
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell()
            cell.Colspan = 3
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Border = 0
            cell.AddElement(New Phrase(supplier, font1))
            cell.AddElement(New Phrase(sup_address1, font1))
            If sup_address2 <> "" Then
                cell.AddElement(New Phrase(sup_address2, font1))
            End If
            cell.AddElement(New Phrase(sup_citystatezip, font1))
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Ship To", font2))
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell()
            cell.Colspan = 3
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Border = 0
            cell.AddElement(New Phrase(shipto, font1))
            cell.AddElement(New Phrase(ship_address1, font1))
            If ship_address2 <> "" Then
                cell.AddElement(New Phrase(ship_address2, font1))
            End If
            cell.AddElement(New Phrase(ship_citystatezip, font1))
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("PO", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(po, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Contact", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(sup_contact, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Phone", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(sup_phone, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            doc.Add(table)
            'end header table

            commandString = "select * from t_poline where poID=@poID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@poID", poID)
            reader = comm.ExecuteReader

            'lines table
            table = New PdfPTable(12)
            table.WidthPercentage = 100

            'lines header row
            cell = New PdfPCell(New Phrase("Part Number", font2))
            cell.Colspan = 6
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Qty", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Open", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Rec", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Cost", font2))
            cell.Colspan = 2
            cell.HorizontalAlignment = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("UOM", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            'end header row

            Dim partnumber, item, uom As String
            Dim quantity As Double = 0
            Dim received As Double = 0
            Dim subtotal As Double = 0
            Dim openqty As Double = 0
            Dim cost As String
            While reader.Read
                partnumber = reader.Item("manufacturer") & " " & reader.Item("partnumber")
                item = appcode.GetItemName(reader.Item("manufacturer"), reader.Item("partnumber"))
                quantity = reader.Item("quantity")
                received = appcode.GetReceivedPOLineQuantity(poID, reader.Item("manufacturer"), reader.Item("partnumber"))
                openqty = quantity - received
                uom = reader.Item("uom")
                cost = FormatCurrency(reader.Item("cost"), 2)

                If openqty > 0 Then
                    quantity = CStr(openqty)
                    'start line row
                    cell = New PdfPCell(New Phrase(partnumber, font1))
                    cell.Colspan = 6
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(quantity, font1))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(openqty, font1))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(cost, font1))
                    cell.Colspan = 2
                    cell.HorizontalAlignment = 2
                    cell.Border = 1
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(uom, font1))
                    cell.Colspan = 1
                    cell.HorizontalAlignment = 1
                    cell.Border = 1
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(item, font1))
                    cell.Colspan = 12
                    cell.Border = 0
                    table.AddCell(cell)
                    'end line row
                End If
            End While

            reader.Close()

            table.HeaderRows = 1
            doc.Add(table)

            table = New PdfPTable(8)
            table.WidthPercentage = 100

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Reference #", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("________", font2))
            cell.Colspan = 6
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Received By", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("________", font2))
            cell.Colspan = 6
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Date", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("________", font2))
            cell.Colspan = 6
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            doc.Add(table)

            conn.Close()
            doc.Close()
        End If
    End Sub

    Public Shared Sub KitBookPDF(ByVal companyID As Integer, ByVal locationID As String, ByVal applicationpath As String, ByVal filename As String, ByVal imagefile As String, ByVal all_parts As Boolean)
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()

        Dim doc As Document = New Document(PageSize.LETTER)
        PdfWriter.GetInstance(doc, New FileStream(applicationpath + filename, FileMode.Create))
        Dim basefont1 As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, False)
        Dim font1 As Font = FontFactory.GetFont("Century Gothic", 10)
        Dim font2 As Font = FontFactory.GetFont("Century Gothic", 10, Font.BOLD)
        Dim font3 As Font = FontFactory.GetFont("Century Gothic", 14, Font.BOLD)
        Dim font4 As Font = FontFactory.GetFont("Century Gothic", 30, Font.BOLD)

        doc.Open()
        doc.SetPageSize(PageSize.A4)
        doc.SetMargins(36, 36, 72, 114)
        doc.SetMarginMirroring(True)

        Dim table As PdfPTable
        Dim cell As PdfPCell

        Dim p0 As New Paragraph(" ")

        For x = 1 To 15
            doc.Add(p0)
        Next

        Dim p1 As New Paragraph("Service Kit Book", font3)
        p1.Alignment = 1
        doc.Add(p1)
        doc.Add(p0)
        doc.Add(p0)
        p1 = New Paragraph("prepared for", font2)
        p1.Alignment = 1
        doc.Add(p1)
        doc.Add(p0)
        doc.Add(p0)
        p1 = New Paragraph(GetCompany(companyID), font4)
        p1.Alignment = 1
        doc.Add(p1)
        If locationID <> "0" Then
            p1 = New Paragraph(GetLocation(locationID), font2)
            p1.Alignment = 1
            doc.Add(p1)
        End If

        Dim equipmentIDlist(2000) As Integer
        Dim xx As Integer = 0
        Dim commandString As String
        commandString = "select * from t_equipment where companyID=@companyID order by assetID"
        If locationID <> 0 Then
            commandString = "select * from t_equipment where companyID=@companyID and locationID=@locationID order by assetID"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        If locationID <> 0 Then
            comm.Parameters.AddWithValue("@locationID", locationID)
        End If
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            equipmentIDlist(xx) = reader.Item("equipmentID")
            xx += 1
        End While
        reader.Close()

        Dim a As Integer = 0
        Dim serviceprofileIDlist(2000, 10) As Integer
        For yy As Integer = 0 To xx - 1
            a = 0
            commandString = "select * from t_serviceprofile where equipmentID=@equipmentID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@equipmentID", equipmentIDlist(yy))
            reader = comm.ExecuteReader
            While reader.Read
                serviceprofileIDlist(yy, a) = reader.Item("serviceprofileID")
                a += 1
            End While
            For c As Integer = a To 9
                serviceprofileIDlist(yy, a) = -1
            Next
            reader.Close()
        Next

        For yy = 0 To xx - 1
            For b As Integer = 0 To 9
                If serviceprofileIDlist(yy, b) <> -1 Then
                    commandString = "select * from v_serviceprofile where serviceprofileID=@serviceprofileID"
                    comm = New SqlCommand(commandString, conn)
                    comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileIDlist(yy, b))
                    reader = comm.ExecuteReader
                    If reader.Read Then
                        Dim assetID As String = reader.Item("assetID").ToString
                        Dim equipment As String = ""
                        Dim equipment_vin As String = reader.Item("equipment_vin").ToString
                        Dim kitname As String = reader.Item("name").ToString
                        Dim interval As String = reader.Item("interval").ToString & " " & reader.Item("interval_type").ToString
                        Dim kitID As String = GetKitID(reader.Item("kitcode"))

                        If reader.Item("equipment_year").ToString <> "" Then
                            equipment &= reader.Item("equipment_year").ToString & " "
                        End If
                        equipment &= reader.Item("equipment_oem").ToString
                        If reader.Item("equipment_model").ToString <> "" Then
                            equipment &= " " & reader.Item("equipment_model").ToString
                        End If
                        If reader.Item("equipment_description").ToString <> "" Then
                            equipment &= " " & reader.Item("equipment_description").ToString
                        End If
                        equipment &= " - ASSET #" & assetID
                        reader.Close()

                        doc.NewPage()

                        'header table
                        table = New PdfPTable(8)
                        table.WidthPercentage = 100

                        cell = New PdfPCell(New Phrase(equipment, font3))
                        cell.Colspan = 8
                        cell.Border = 0
                        table.AddCell(cell)

                        cell = New PdfPCell(New Phrase("Interval", font2))
                        cell.Colspan = 1
                        cell.Border = 0
                        table.AddCell(cell)

                        cell = New PdfPCell(New Phrase(interval, font2))
                        cell.Colspan = 7
                        cell.Border = 0
                        table.AddCell(cell)

                        cell = New PdfPCell(New Phrase("Kit ID", font2))
                        cell.Colspan = 1
                        cell.Border = 0
                        table.AddCell(cell)

                        cell = New PdfPCell(New Phrase(kitID, font2))
                        cell.Colspan = 7
                        cell.Border = 0
                        table.AddCell(cell)

                        cell = New PdfPCell(New Phrase(" ", font2))
                        cell.Colspan = 8
                        cell.Border = 0
                        table.AddCell(cell)

                        doc.Add(table)
                        'end header table

                        'print kit info
                        table = New PdfPTable(13)
                        table.WidthPercentage = 100

                        cell = New PdfPCell(New Phrase(" ", font2))
                        cell.Colspan = 2
                        cell.Border = 0
                        table.AddCell(cell)
                        cell = New PdfPCell(New Phrase(" ", font2))
                        cell.Colspan = 7
                        cell.Border = 0
                        table.AddCell(cell)
                        cell = New PdfPCell(New Phrase("Selected", font2))
                        cell.Colspan = 2
                        cell.HorizontalAlignment = 1
                        cell.Border = 0
                        table.AddCell(cell)
                        cell = New PdfPCell(New Phrase("Qty", font2))
                        cell.Colspan = 1
                        cell.HorizontalAlignment = 1
                        cell.Border = 0
                        table.AddCell(cell)
                        cell = New PdfPCell(New Phrase("UOM", font2))
                        cell.Colspan = 1
                        cell.HorizontalAlignment = 1
                        cell.Border = 0
                        table.AddCell(cell)

                        If all_parts = True Then
                            commandString = "select * from v_kitparts where serviceprofileID=@serviceprofileID order by manufacturer,partnumber"
                        Else
                            commandString = "select * from v_kitparts where serviceprofileID=@serviceprofileID and selected='True' order by manufacturer,partnumber"
                        End If
                        comm = New SqlCommand(commandString, conn)
                        comm.Parameters.AddWithValue("@serviceprofileID", serviceprofileIDlist(yy, b))
                        comm.Parameters.AddWithValue("@selected", True)
                        reader = comm.ExecuteReader
                        Dim partnumber, description, quantity, uom, alt_partnumber, oem_partnumber, selected As String
                        Dim partID As Integer
                        While reader.Read
                            If reader.Item("selected") = True Then
                                selected = "YES"
                            Else
                                selected = "NO"
                            End If
                            partID = reader.Item("partID")
                            partnumber = reader.Item("manufacturer") & " " & reader.Item("partnumber")
                            description = reader.Item("description").ToString
                            quantity = reader.Item("quantity")
                            uom = reader.Item("uom")
                            If reader.Item("alt_manufacturer").ToString <> "" And reader.Item("alt_partnumber").ToString <> "" Then
                                alt_partnumber = reader.Item("alt_manufacturer").ToString & " " & reader.Item("alt_partnumber")
                            Else
                                alt_partnumber = ""
                            End If
                            If reader.Item("oem_manufacturer").ToString <> "" And reader.Item("oem_partnumber").ToString <> "" Then
                                oem_partnumber = reader.Item("oem_manufacturer").ToString & " " & reader.Item("oem_partnumber")
                            Else
                                oem_partnumber = ""
                            End If

                            'line row
                            cell = New PdfPCell(New Phrase("Part Number", font2))
                            cell.Colspan = 2
                            cell.Border = 0
                            table.AddCell(cell)
                            cell = New PdfPCell(New Phrase(partnumber, font1))
                            cell.Colspan = 7
                            cell.Border = 0
                            table.AddCell(cell)
                            cell = New PdfPCell(New Phrase(selected, font1))
                            cell.Colspan = 2
                            cell.HorizontalAlignment = 1
                            cell.Border = 0
                            table.AddCell(cell)
                            cell = New PdfPCell(New Phrase(quantity, font1))
                            cell.Colspan = 1
                            cell.HorizontalAlignment = 1
                            cell.Border = 0
                            table.AddCell(cell)
                            cell = New PdfPCell(New Phrase(uom, font1))
                            cell.Colspan = 1
                            cell.HorizontalAlignment = 1
                            cell.Border = 0
                            table.AddCell(cell)

                            cell = New PdfPCell(New Phrase("Description", font2))
                            cell.Colspan = 2
                            cell.Border = 0
                            table.AddCell(cell)
                            cell = New PdfPCell(New Phrase(description, font1))
                            cell.Colspan = 11
                            cell.Border = 0
                            table.AddCell(cell)

                            If alt_partnumber <> "" Then
                                cell = New PdfPCell(New Phrase("Alternate", font2))
                                cell.Colspan = 2
                                cell.Border = 0
                                table.AddCell(cell)
                                cell = New PdfPCell(New Phrase(alt_partnumber, font1))
                                cell.Colspan = 11
                                cell.Border = 0
                                table.AddCell(cell)
                            End If
                            If oem_partnumber <> "" Then
                                cell = New PdfPCell(New Phrase("OEM", font2))
                                cell.Colspan = 2
                                cell.Border = 0
                                table.AddCell(cell)
                                cell = New PdfPCell(New Phrase(oem_partnumber, font1))
                                cell.Colspan = 11
                                cell.Border = 0
                                table.AddCell(cell)
                            End If
                            cell = New PdfPCell(New Phrase(" ", font1))
                            cell.Colspan = 13
                            cell.Border = 0
                            table.AddCell(cell)
                        End While
                        reader.Close()

                        table.HeaderRows = 1
                        doc.Add(table)
                    End If
                    reader.Close()
                End If
            Next
        Next
        conn.Close()
        doc.Close()
    End Sub

    Public Shared Sub EquipmentBookPDF(ByVal companyID As Integer, ByVal locationID As Integer, ByVal userID As Integer, ByVal admin As Boolean, ByVal rcompanyID As Integer, ByVal applicationpath As String, ByVal filename As String, ByVal imagefile As String)
        Dim equipmentIDlist(1000) As Integer
        Dim title As String = GetCompany(companyID)
        Dim location_name As String = ""
        If locationID = 0 Then
            location_name = "All Locations"
        Else
            location_name = GetLocation(locationID)
        End If
        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim commandString As String
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        If locationID <> 0 Then
            commandString = "select * from t_equipment where locationID=@locationID order by assetID"
        Else
            commandString = "select * from t_equipment where companyID=@companyID order by assetID"
        End If
        Dim comm As New SqlCommand(commandString, conn)
        If locationID <> 0 Then
            comm.Parameters.AddWithValue("@locationID", locationID)
            comm.Parameters.AddWithValue("@companyID", companyID)
        Else
            comm.Parameters.AddWithValue("@companyID", companyID)
        End If
        Dim reader As SqlDataReader = comm.ExecuteReader
        While reader.Read
            equipmentIDlist(x) = reader.Item("equipmentID")
            x += 1
        End While
        reader.Close()

        Dim doc As Document = New Document(PageSize.LETTER)
        Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(applicationpath + filename, FileMode.Create))
        Dim basefont1 As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, False)
        Dim font1 As Font = FontFactory.GetFont("Century Gothic", 10)
        Dim font2 As Font = FontFactory.GetFont("Century Gothic", 10, Font.BOLD)
        Dim font3 As Font = FontFactory.GetFont("Century Gothic", 30, Font.BOLD)
        Dim font4 As Font = FontFactory.GetFont("Century Gothic", 24, Font.BOLD)
        Dim font5 As Font = FontFactory.GetFont("Century Gothic", 18, Font.BOLD)
        doc.Open()
        doc.SetPageSize(PageSize.A4)
        doc.SetMargins(36, 72, 30, 50)
        doc.SetMarginMirroring(True)

        Dim p0 As New Paragraph(" ")
        For zz = 1 To 15
            doc.Add(p0)
        Next

        If imagefile <> "" Then
            Dim logo As Image = Image.GetInstance(applicationpath + imagefile)
            logo.Alignment = Element.ALIGN_CENTER
            doc.Add(logo)
        End If

        Dim table As PdfPTable
        Dim cell As PdfPCell

        Dim equipmentID As Integer = 0
        Dim equipment_oem As String = ""

        table = New PdfPTable(1)
        table.WidthPercentage = 100

        cell = New PdfPCell(New Phrase(title, font3))
        cell.Colspan = 1
        cell.HorizontalAlignment = 1
        cell.Border = 0
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase(location_name, font4))
        cell.Colspan = 1
        cell.HorizontalAlignment = 1
        cell.Border = 0
        table.AddCell(cell)
        doc.Add(table)

        For y = 0 To x - 1
            doc.NewPage()

            commandString = "select * from v_equipment where equipmentID=@equipmentID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@equipmentID", equipmentIDlist(y))
            reader = comm.ExecuteReader
            If reader.Read Then
                Dim company As String = GetCompany(reader.Item("companyID"))
                equipmentID = reader.Item("equipmentID")
                Dim assetID As String = reader.Item("assetID").ToString
                Dim equipment As String = reader.Item("equipment_oem").ToString
                If reader.Item("equipment_year").ToString <> "" Then
                    equipment &= " " & reader.Item("equipment_year").ToString
                End If
                If reader.Item("equipment_model").ToString <> "" Then
                    equipment &= " " & reader.Item("equipment_model").ToString
                End If
                If reader.Item("equipment_description").ToString <> "" Then
                    equipment &= " " & reader.Item("equipment_description").ToString
                End If
                Dim engine As String = reader.Item("engine_oem").ToString
                If reader.Item("engine_model").ToString <> "" Then
                    engine &= " " & reader.Item("engine_model").ToString
                End If

                Dim equipment_options As String = reader.Item("equipment_options").ToString
                Dim equipment_vin As String = reader.Item("equipment_vin").ToString
                Dim notes As String = reader.Item("notes").ToString
                Dim location As String
                Dim address1 As String
                Dim address2 As String
                Dim address3 As String
                Dim address4 As String
                If reader.Item("locationID") <> 0 Then
                    location = reader.Item("shipto").ToString
                    address1 = reader.Item("s_address1").ToString
                    address2 = reader.Item("s_address2").ToString
                    address3 = reader.Item("s_address3").ToString
                    address4 = reader.Item("s_city").ToString & ", " & reader.Item("s_state").ToString & " " & reader.Item("s_zipcode").ToString()
                Else
                    location = "None"
                    address1 = ""
                    address2 = ""
                    address3 = ""
                    address4 = ""
                End If

                'header table
                table = New PdfPTable(8)
                table.WidthPercentage = 100

                cell = New PdfPCell(New Phrase(equipment, font5))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("Company", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(company, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase("Location", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(location, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("Asset ID", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(assetID, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(address1, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase("VIN/Serial #", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(equipment_vin, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(address2, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                If address3 <> "" Then
                    cell = New PdfPCell(New Phrase(" ", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(" ", font2))
                    cell.Colspan = 1
                    cell.Border = 0
                    table.AddCell(cell)
                    cell = New PdfPCell(New Phrase(address3, font1))
                    cell.Colspan = 3
                    cell.Border = 0
                    table.AddCell(cell)
                End If

                cell = New PdfPCell(New Phrase("Engine", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(engine, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(address4, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)

                'header row 2
                cell = New PdfPCell(New Phrase("Options", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(equipment_options, font1))
                cell.Colspan = 7
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" "))
                cell.Colspan = 8
                cell.Border = 0
                table.AddCell(cell)

                If notes <> "" Then
                    cell = New PdfPCell(New Phrase("Equipment Notes", font2))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(notes, font1))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Phrase(" "))
                    cell.Colspan = 8
                    cell.Border = 0
                    table.AddCell(cell)
                End If

                doc.Add(table)
                'end header table
            End If
            reader.Close()

            'lines table
            table = New PdfPTable(10)
            table.WidthPercentage = 100

            cell = New PdfPCell(New Phrase("Primary", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Alternate", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("OEM", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Qty", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("UOM", font2))
            cell.Colspan = 2
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            'end header row

            commandString = "select * from t_parts where equipmentID=@equipmentID order by manufacturer,partnumber"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@equipmentID", equipmentID)
            reader = comm.ExecuteReader
            Dim partnumber, description, quantity, uom, alternate, oem_partnumber, itemnotes As String
            Dim partID As Integer
            While reader.Read
                partID = reader.Item("partID")
                partnumber = reader.Item("partnumber")
                description = "DESCRIPTION: " & reader.Item("description").ToString
                quantity = reader.Item("quantity")
                uom = reader.Item("uom")
                alternate = reader.Item("alt_partnumber").ToString
                oem_partnumber = equipment_oem & " " & reader.Item("oem_partnumber").ToString
                itemnotes = reader.Item("notes").ToString

                cell = New PdfPCell(New Phrase(partnumber, font1))
                cell.Colspan = 2
                cell.Border = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(alternate, font1))
                cell.Colspan = 2
                cell.Border = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(oem_partnumber, font1))
                cell.Colspan = 2
                cell.Border = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(quantity, font1))
                cell.Colspan = 2
                cell.Border = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(uom, font1))
                cell.Colspan = 2
                cell.HorizontalAlignment = 1
                cell.Border = 1
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(description, font1))
                cell.Colspan = 10
                cell.Border = 0
                table.AddCell(cell)

                If itemnotes <> "" Then
                    cell = New PdfPCell(New Phrase(itemnotes, font1))
                    cell.Colspan = 10
                    cell.Border = 0
                    table.AddCell(cell)
                End If
                'end line row
            End While
            reader.Close()

            table.HeaderRows = 1
            doc.Add(table)
        Next

        conn.Close()
        doc.Close()
    End Sub

    Public Shared Sub InventorySheetPDF(ByVal companyID As Integer, ByVal applicationpath As String, ByVal filename As String, ByVal imagefile As String)
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim doc As Document = New Document(PageSize.LETTER)
        Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(applicationpath + filename, FileMode.Create))
        Dim basefont1 As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, False)
        Dim font1 As Font = FontFactory.GetFont("Century Gothic", 10)
        Dim font2 As Font = FontFactory.GetFont("Century Gothic", 10, Font.BOLD)
        Dim font3 As Font = FontFactory.GetFont("Century Gothic", 18, Font.BOLD)
        Dim font4 As Font = FontFactory.GetFont("Century Gothic", 24, Font.BOLD)
        doc.Open()
        doc.SetPageSize(PageSize.A4)
        doc.SetMargins(36, 72, 30, 50)
        doc.SetMarginMirroring(True)

        Dim table As PdfPTable
        Dim cell As PdfPCell


        Dim company As String = ""
        Dim commandstring As String = "select * from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandstring, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            company = reader.Item("company")
        End If
        reader.Close()

        'header table
        table = New PdfPTable(13)
        table.WidthPercentage = 100

        cell = New PdfPCell(New Phrase(company, font3))
        cell.Colspan = 13
        cell.Border = 0
        table.AddCell(cell)

        cell = New PdfPCell(New Phrase(" "))
        cell.Colspan = 13
        cell.Border = 0
        table.AddCell(cell)

        'lines header row
        cell = New PdfPCell(New Phrase("Part Number", font2))
        cell.Colspan = 5
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase("YTD", font2))
        cell.Colspan = 2
        cell.HorizontalAlignment = 1
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase("Min", font2))
        cell.Colspan = 2
        cell.HorizontalAlignment = 1
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase("Max", font2))
        cell.Colspan = 2
        cell.HorizontalAlignment = 1
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase("On Hand", font2))
        cell.Colspan = 2
        cell.HorizontalAlignment = 1
        table.AddCell(cell)
        'end header row

        commandstring = "select * from t_warehouse where companyID=@companyID order by manufacturer,partnumber"
        comm = New SqlCommand(commandstring, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        reader = comm.ExecuteReader
        Dim partnumber, min, max As String
        Dim ytd As Double = 0
        While reader.Read
            partnumber = reader.Item("manufacturer").ToString & " " & reader.Item("partnumber").ToString
            min = reader.Item("min").ToString
            max = reader.Item("max").ToString
            ytd = GetCompanySalesQtyYTD(companyID, reader.Item("manufacturer"), reader.Item("partnumber"), Year(Now()))
            If CDbl(max) > 0 Then
                'line row
                cell = New PdfPCell(New Phrase(partnumber, font1))
                cell.Colspan = 5
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(ytd, font1))
                cell.Colspan = 2
                cell.HorizontalAlignment = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(min, font1))
                cell.Colspan = 2
                cell.HorizontalAlignment = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(max, font1))
                cell.Colspan = 2
                cell.HorizontalAlignment = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font1))
                cell.Colspan = 2
                cell.HorizontalAlignment = 1
                table.AddCell(cell)

                'end line row
            End If
        End While
        reader.Close()

        table.HeaderRows = 1
        doc.Add(table)

        conn.Close()
        doc.Close()
    End Sub

    Public Shared Sub PriceSheetPDF(ByVal companyID As Integer, ByVal applicationpath As String, ByVal filename As String, ByVal imagefile As String)
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim doc As Document = New Document(PageSize.LETTER)
        Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(applicationpath + filename, FileMode.Create))
        Dim basefont1 As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, False)
        Dim font1 As Font = FontFactory.GetFont("Century Gothic", 10)
        Dim font2 As Font = FontFactory.GetFont("Century Gothic", 10, Font.BOLD)
        Dim font3 As Font = FontFactory.GetFont("Century Gothic", 18, Font.BOLD)
        Dim font4 As Font = FontFactory.GetFont("Century Gothic", 24, Font.BOLD)
        doc.Open()
        doc.SetPageSize(PageSize.A4)
        doc.SetMargins(36, 72, 30, 50)
        doc.SetMarginMirroring(True)

        Dim table As PdfPTable
        Dim cell As PdfPCell


        Dim company As String = ""
        Dim commandstring As String = "select * from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandstring, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            company = reader.Item("company")
        End If
        reader.Close()

        'header table
        table = New PdfPTable(13)
        table.WidthPercentage = 100

        cell = New PdfPCell(New Phrase(company, font3))
        cell.Colspan = 13
        cell.Border = 0
        table.AddCell(cell)

        cell = New PdfPCell(New Phrase(" "))
        cell.Colspan = 13
        cell.Border = 0
        table.AddCell(cell)

        'lines header row
        cell = New PdfPCell(New Phrase("Part Number", font2))
        cell.Colspan = 5
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase("YTD", font2))
        cell.Colspan = 2
        cell.HorizontalAlignment = 1
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase("Min", font2))
        cell.Colspan = 2
        cell.HorizontalAlignment = 1
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase("Max", font2))
        cell.Colspan = 2
        cell.HorizontalAlignment = 1
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase("Price", font2))
        cell.Colspan = 2
        cell.HorizontalAlignment = 2
        table.AddCell(cell)
        'end header row

        commandstring = "select * from t_warehouse where companyID=@companyID order by manufacturer,partnumber"
        comm = New SqlCommand(commandstring, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        reader = comm.ExecuteReader
        Dim partnumber, min, max, price As String
        Dim ytd As Double = 0
        Dim linenum As Integer = 0
        While reader.Read
            partnumber = reader.Item("manufacturer").ToString & " " & reader.Item("partnumber").ToString
            min = reader.Item("min").ToString
            max = reader.Item("max").ToString
            ytd = GetCompanySalesQtyYTD(companyID, reader.Item("manufacturer"), reader.Item("partnumber"), Year(Now()))
            price = formatcurrency(GetCompanyPrice(companyID, reader.Item("manufacturer"), reader.Item("partnumber")),2)
            If CDbl(max) > 0 Then
                'line row
                cell = New PdfPCell(New Phrase(partnumber, font1))
                cell.Colspan = 5
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(ytd, font1))
                cell.Colspan = 2
                cell.HorizontalAlignment = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(min, font1))
                cell.Colspan = 2
                cell.HorizontalAlignment = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(max, font1))
                cell.Colspan = 2
                cell.HorizontalAlignment = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(price, font1))
                cell.Colspan = 2
                cell.HorizontalAlignment = 2
                table.AddCell(cell)
                linenum += 1
                'end line row
            End If
        End While
        reader.Close()

        table.HeaderRows = 1
        doc.Add(table)

        conn.Close()
        doc.Close()
    End Sub

    Public Shared Sub DFOInventorySheetPDF(ByVal manufacturer As String, ByVal applicationpath As String, ByVal filename As String, ByVal imagefile As String)
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim doc As Document = New Document(PageSize.LETTER)
        Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(applicationpath + filename, FileMode.Create))
        Dim basefont1 As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, False)
        Dim font1 As Font = FontFactory.GetFont("Century Gothic", 10)
        Dim font2 As Font = FontFactory.GetFont("Century Gothic", 10, Font.BOLD)
        Dim font3 As Font = FontFactory.GetFont("Century Gothic", 18, Font.BOLD)
        Dim font4 As Font = FontFactory.GetFont("Century Gothic", 24, Font.BOLD)
        doc.Open()
        doc.SetPageSize(PageSize.A4)
        doc.SetMargins(36, 72, 30, 50)
        doc.SetMarginMirroring(True)

        Dim table As PdfPTable
        Dim cell As PdfPCell

        Dim title As String = manufacturer & " Inventory"

        'header table
        table = New PdfPTable(13)
        table.WidthPercentage = 100

        cell = New PdfPCell(New Phrase(title, font3))
        cell.Colspan = 13
        cell.Border = 0
        table.AddCell(cell)

        cell = New PdfPCell(New Phrase(" "))
        cell.Colspan = 13
        cell.Border = 0
        table.AddCell(cell)

        'lines header row
        cell = New PdfPCell(New Phrase("Part Number", font2))
        cell.Colspan = 6
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase("Last", font2))
        cell.Colspan = 1
        cell.HorizontalAlignment = 1
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase("YTD", font2))
        cell.Colspan = 1
        cell.HorizontalAlignment = 1
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase("Min", font2))
        cell.Colspan = 1
        cell.HorizontalAlignment = 1
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase("Max", font2))
        cell.Colspan = 1
        cell.HorizontalAlignment = 1
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase("On Hand", font2))
        cell.Colspan = 3
        cell.HorizontalAlignment = 1
        table.AddCell(cell)
        'end header row

        Dim commandstring As String = "select * from t_product where (nstock=@nstock and manufacturer=@manufacturer) or (onhand<>0 and manufacturer=@manufacturer) order by partnumber"
        Dim comm As New SqlCommand(commandstring, conn)
        comm.Parameters.AddWithValue("@nstock", True)
        comm.Parameters.AddWithValue("@manufacturer", manufacturer)
        Dim reader As SqlDataReader = comm.ExecuteReader
        Dim partnumber, min, max, onhand As String
        Dim ytd As Double = 0
        While reader.Read
            partnumber = reader.Item("manufacturer").ToString & " " & reader.Item("partnumber").ToString
            min = reader.Item("min").ToString
            max = reader.Item("max").ToString
            ytd = GetSalesQtyYTD(manufacturer, reader.Item("partnumber"), Year(Now()))
            onhand = reader.Item("onhand").ToString
            If CDbl(max) > 0 Then
                'line row
                cell = New PdfPCell(New Phrase(partnumber, font1))
                cell.Colspan = 6
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(onhand, font1))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(ytd, font1))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(min, font1))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(max, font1))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font1))
                cell.Colspan = 3
                cell.HorizontalAlignment = 1
                table.AddCell(cell)

                'end line row
            End If
        End While
        reader.Close()

        table.HeaderRows = 1
        doc.Add(table)

        conn.Close()
        doc.Close()
    End Sub

    Public Shared Sub InventorySheet2PDF(ByVal companyID As Integer, ByVal locationID As String, ByVal applicationpath As String, ByVal filename As String, ByVal imagefile As String)
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim doc As Document = New Document(PageSize.LETTER)
        Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(applicationpath + filename, FileMode.Create))
        Dim basefont1 As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, False)
        Dim font1 As Font = FontFactory.GetFont("Century Gothic", 10)
        Dim font2 As Font = FontFactory.GetFont("Century Gothic", 10, Font.BOLD)
        Dim font3 As Font = FontFactory.GetFont("Century Gothic", 18, Font.BOLD)
        Dim font4 As Font = FontFactory.GetFont("Century Gothic", 24, Font.BOLD)
        doc.Open()
        doc.SetPageSize(PageSize.A4)
        doc.SetMargins(36, 72, 30, 50)
        doc.SetMarginMirroring(True)

        Dim table As PdfPTable
        Dim cell As PdfPCell

        Dim equipment_oem As String = ""

        Dim ev As New PDF_Helper
        pdfWrite.PageEvent = ev
        Dim company As String = ""
        Dim commandstring As String = "select * from t_company where companyID=@companyID"
        Dim comm As New SqlCommand(commandstring, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            company = reader.Item("company")
        End If
        reader.Close()
        Dim location As String = "All Locations"
        If locationID <> 0 Then
            commandstring = "select * from t_ship where shipID=@shipID"
            comm = New SqlCommand(commandstring, conn)
            comm.Parameters.AddWithValue("@shipID", locationID)
            reader = comm.ExecuteReader
            If reader.Read Then
                location = reader.Item("shipto")
            End If
            reader.Close()
        End If
        company = company & "(" & location & ")"

        'header table
        table = New PdfPTable(8)
        table.WidthPercentage = 100

        cell = New PdfPCell(New Phrase(company, font3))
        cell.Colspan = 8
        cell.Border = 0
        table.AddCell(cell)

        cell = New PdfPCell(New Phrase(" "))
        cell.Colspan = 8
        cell.Border = 0
        table.AddCell(cell)
        doc.Add(table)

        'lines table
        table = New PdfPTable(13)
        table.WidthPercentage = 100

        'lines header row
        cell = New PdfPCell(New Phrase("Part Number", font2))
        cell.Colspan = 4
        cell.Border = 0
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase("Price", font2))
        cell.Colspan = 3
        cell.HorizontalAlignment = 2
        cell.Border = 0
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase("Frequency", font2))
        cell.Colspan = 2
        cell.HorizontalAlignment = 1
        cell.Border = 0
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase(" ", font2))
        cell.Colspan = 4
        cell.HorizontalAlignment = 1
        cell.Border = 0
        table.AddCell(cell)
        'end header row

        commandstring = "select manufacturer,partnumber,price,uom,sum(quantity) as frequency from v_parts where companyID=@companyID group by manufacturer, partnumber,price,uom order by frequency desc"

        If locationID <> 0 Then
            commandstring = "select manufacturer,partnumber,price,uom,sum(quantity) as frequency from v_parts where companyID=@companyID and locationID=@locationID group by manufacturer, partnumber,price,uom order by frequency desc"
        End If
        comm = New SqlCommand(commandstring, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        If locationID <> 0 Then
            comm.Parameters.AddWithValue("@locationID", locationID)
        End If
        reader = comm.ExecuteReader
        Dim manufacturer, partnumber, frequency, price, uom As String
        Dim package As Double = 0
        Dim cost As Double = 0
        Dim value As Double = 0
        Dim totalvalue As Double = 0
        While reader.Read
            partnumber = reader.Item("manufacturer").ToString & " " & reader.Item("partnumber").ToString
            frequency = reader.Item("frequency").ToString
            price = FormatCurrency(reader.Item("price").ToString, 2) & " " & reader.Item("uom").ToString
            package = GetPackage(reader.Item("manufacturer"), reader.Item("partnumber"))
            cost = GetCost(reader.Item("manufacturer"), reader.Item("partnumber"))
            value = cost * package
            totalvalue += value
            'line row
            cell = New PdfPCell(New Phrase(partnumber, font1))
            cell.Colspan = 4
            cell.Border = 1
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(price, font1))
            cell.Colspan = 3
            cell.HorizontalAlignment = 2
            cell.Border = 1
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(frequency, font1))
            cell.Colspan = 2
            cell.HorizontalAlignment = 1
            cell.Border = 1
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font1))
            cell.Colspan = 4
            cell.HorizontalAlignment = 1
            cell.Border = 1
            table.AddCell(cell)

            'end line row
        End While
        reader.Close()

        table.HeaderRows = 1
        doc.Add(table)

        conn.Close()
        doc.Close()
    End Sub

    Public Shared Sub EquipmentSheetPDF(ByVal equipmentID As Integer, ByVal applicationpath As String, ByVal imagefile As String)
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim doc As Document = New Document(PageSize.LETTER)
        Dim file1name As String = "EquipmentSheets/Worksheet_" & equipmentID & ".pdf"
        Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(applicationpath + file1name, FileMode.Create))
        Dim basefont1 As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, False)
        Dim font1 As Font = FontFactory.GetFont("Century Gothic", 10)
        Dim font2 As Font = FontFactory.GetFont("Century Gothic", 10, Font.BOLD)
        Dim font3 As Font = FontFactory.GetFont("Century Gothic", 18, Font.BOLD)
        Dim font4 As Font = FontFactory.GetFont("Century Gothic", 24, Font.BOLD)
        doc.Open()
        doc.SetPageSize(PageSize.A4)
        doc.SetMargins(36, 72, 30, 50)
        doc.SetMarginMirroring(True)

        Dim table As PdfPTable
        Dim cell As PdfPCell

        Dim equipment_oem As String = ""

        Dim ev As New PDF_Helper
        pdfWrite.PageEvent = ev

        Dim commandstring As String = "select * from v_equipment where equipmentID=@equipmentID"
        Dim comm As New SqlCommand(commandstring, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            Dim company As String = GetCompany(reader.Item("companyID"))
            equipmentID = reader.Item("equipmentID")
            Dim assetID As String = reader.Item("assetID").ToString
            Dim equipment As String = reader.Item("equipment_oem").ToString
            If reader.Item("equipment_year").ToString <> "" Then
                equipment &= " " & reader.Item("equipment_year").ToString
            End If
            If reader.Item("equipment_model").ToString <> "" Then
                equipment &= " " & reader.Item("equipment_model").ToString
            End If
            If reader.Item("equipment_description").ToString <> "" Then
                equipment &= " " & reader.Item("equipment_description").ToString
            End If
            Dim engine As String = reader.Item("engine_oem").ToString
            If reader.Item("engine_model").ToString <> "" Then
                engine &= " " & reader.Item("engine_model").ToString
            End If

            Dim equipment_options As String = reader.Item("equipment_options").ToString
            Dim equipment_vin As String = reader.Item("equipment_vin").ToString
            Dim enginesn As String = ""
            Dim notes As String = reader.Item("notes").ToString
            Dim location As String
            Dim address1 As String
            Dim address2 As String
            Dim address3 As String
            Dim address4 As String
            If reader.Item("locationID") <> 0 Then
                location = reader.Item("shipto").ToString
                address1 = reader.Item("s_address1").ToString
                address2 = reader.Item("s_address2").ToString
                address3 = reader.Item("s_address3").ToString
                address4 = reader.Item("s_city").ToString & ", " & reader.Item("s_state").ToString & " " & reader.Item("s_zipcode").ToString()
            Else
                location = "None"
                address1 = ""
                address2 = ""
                address3 = ""
                address4 = ""
            End If

            'header table
            table = New PdfPTable(8)
            table.WidthPercentage = 100

            cell = New PdfPCell(New Phrase("Equipment Profile", font3))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Company", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(company, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Location", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(location, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Asset ID", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(assetID, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(address1, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            If address2 <> "" Then
                cell = New PdfPCell(New Phrase(" ", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(address2, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
            End If

            If address3 <> "" Then
                cell = New PdfPCell(New Phrase(" ", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(address3, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
            End If

            If address4 <> "" Then
                cell = New PdfPCell(New Phrase(" ", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font2))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(address4, font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
            End If

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            'header row 2
            cell = New PdfPCell(New Phrase("Equipment", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(equipment, font1))
            cell.Colspan = 7
            cell.Border = 0
            table.AddCell(cell)

            'header row 2
            cell = New PdfPCell(New Phrase("VIN/Serial #", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(equipment_vin, font1))
            cell.Colspan = 7
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Engine", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(engine, font1))
            cell.Colspan = 7
            cell.Border = 0
            table.AddCell(cell)

            'header row 2
            cell = New PdfPCell(New Phrase("Engine S/N", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(enginesn, font1))
            cell.Colspan = 7
            cell.Border = 0
            table.AddCell(cell)

            'header row 2
            cell = New PdfPCell(New Phrase("Options", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(equipment_options, font1))
            cell.Colspan = 7
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Equipment Notes", font2))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(notes, font1))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            doc.Add(table)
            'end header table
        End If
        reader.Close()

        'lines table
        table = New PdfPTable(13)
        table.WidthPercentage = 100

        'lines header row
        cell = New PdfPCell(New Phrase("Manufacturer", font2))
        cell.Colspan = 3
        cell.Border = 0
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase("Part Number", font2))
        cell.Colspan = 6
        cell.Border = 0
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase("Qty", font2))
        cell.Colspan = 2
        cell.Border = 0
        table.AddCell(cell)
        cell = New PdfPCell(New Phrase("UOM", font2))
        cell.Colspan = 2
        cell.HorizontalAlignment = 1
        cell.Border = 0
        table.AddCell(cell)
        'end header row

        commandstring = "select * from t_parts where equipmentID=@equipmentID order by manufacturer,partnumber"
        comm = New SqlCommand(commandstring, conn)
        comm.Parameters.AddWithValue("@equipmentID", equipmentID)
        reader = comm.ExecuteReader
        Dim partnumber, description, quantity, uom, alternate, oem_partnumber, itemnotes As String
        Dim partID As Integer
        While reader.Read
            partID = reader.Item("partID")
            partnumber = reader.Item("manufacturer") & " " & reader.Item("partnumber")
            description = reader.Item("description").ToString
            quantity = reader.Item("quantity")
            uom = reader.Item("uom")
            alternate = reader.Item("alt_manufacturer") & " " & reader.Item("alt_partnumber").ToString
            oem_partnumber = equipment_oem & " " & reader.Item("oem_partnumber").ToString
            itemnotes = reader.Item("notes").ToString

            'line row
            cell = New PdfPCell(New Phrase("Primary", font1))
            cell.Colspan = 3
            cell.Border = 1
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(partnumber, font1))
            cell.Colspan = 6
            cell.Border = 1
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(quantity, font1))
            cell.Colspan = 2
            cell.Border = 1
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(uom, font1))
            cell.Colspan = 2
            cell.HorizontalAlignment = 1
            cell.Border = 1
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Alternate", font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(alternate, font1))
            cell.Colspan = 10
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("OEM", font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(oem_partnumber, font1))
            cell.Colspan = 10
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Description", font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(description, font1))
            cell.Colspan = 10
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Notes", font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(itemnotes, font1))
            cell.Colspan = 10
            cell.Border = 0
            table.AddCell(cell)
            'end line row
        End While
        reader.Close()

        table.HeaderRows = 1
        doc.Add(table)

        conn.Close()
        doc.Close()
    End Sub

    Public Shared Sub OrderConfirmPDF(ByVal orderID As Integer, ByVal applicationpath As String, ByVal imagefile As String, ByVal filename As String)
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim commandString As String = "select * from t_order where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            Dim doc As Document = New Document(PageSize.LETTER)
            Dim customer As String = reader.Item("company")
            Dim companyID As Integer = reader.Item("companyID")
            Dim vendorID As Integer = reader.Item("vendorID")
            Dim placedby As String = reader.Item("placedby").ToString
            Dim order_date As String = reader.Item("order_date").ToString
            Dim deliverby_date As String = reader.Item("deliverby_date").ToString
            Dim confirm_date As String = reader.Item("confirm_date").ToString
            Dim purchaseorder As String = reader.Item("purchaseorder").ToString
            Dim billto As String = reader.Item("company").ToString
            Dim b_address1 As String = reader.Item("b_address1").ToString
            Dim b_address2 As String = reader.Item("b_address2").ToString
            Dim b_citystatezip As String = reader.Item("b_city") & ", " & reader.Item("b_state") & " " & reader.Item("b_zipcode")
            Dim shipto As String = reader.Item("shipto").ToString
            Dim s_address1 As String = reader.Item("s_address1").ToString
            Dim s_address2 As String = reader.Item("s_address2").ToString
            Dim s_address3 As String = reader.Item("s_address3").ToString
            Dim s_citystatezip As String = reader.Item("s_city") & ", " & reader.Item("s_state") & " " & reader.Item("s_zipcode")
            Dim c_contact As String = reader.Item("c_contact").ToString
            Dim c_phone As String = reader.Item("c_phone").ToString
            Dim c_fax As String = reader.Item("c_fax").ToString
            Dim chargetax As Boolean = reader.Item("chargetax")
            Dim ship_method As String = reader.Item("ship_method").ToString
            Dim ship_options As String = reader.Item("ship_options").ToString
            Dim kit As String = "No"
            If reader.Item("is_kit") = True Then
                kit = "Yes"
            End If

            reader.Close()

            PdfWriter.GetInstance(doc, New FileStream(applicationpath + filename, FileMode.Create))
            Dim basefont1 As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, False)
            Dim font1 As Font = FontFactory.GetFont("Century Gothic", 10)
            Dim font2 As Font = FontFactory.GetFont("Century Gothic", 10, Font.BOLD)
            Dim font3 As Font = FontFactory.GetFont("Century Gothic", 18, Font.BOLD)

            doc.Open()
            doc.SetPageSize(PageSize.A4)
            doc.SetMargins(36, 36, 72, 72)
            doc.SetMarginMirroring(True)

            'Dim logo As Image = Image.GetInstance(imagefile)
            'doc.Add(logo)

            Dim cell As PdfPCell

            'header table
            Dim table As PdfPTable = New PdfPTable(8)
            table.WidthPercentage = 100

            cell = New PdfPCell(New Phrase("Order Confirmation", font3))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            'header row 0
            cell = New PdfPCell(New Phrase("Order ID", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(orderID, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Placed By", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(placedby, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Ordered", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(order_date, font1))
            cell.Colspan = 7
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Bill To", font2))
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell()
            cell.Colspan = 3
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Border = 0
            cell.AddElement(New Phrase(billto, font1))
            cell.AddElement(New Phrase(b_address1, font1))
            If b_address2 <> "" Then
                cell.AddElement(New Phrase(b_address2, font1))
            End If
            cell.AddElement(New Phrase(b_citystatezip, font1))
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Ship To", font2))
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell()
            cell.Colspan = 3
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Border = 0
            cell.AddElement(New Phrase(shipto, font1))
            cell.AddElement(New Phrase(s_address1, font1))
            If s_address2 <> "" Then
                cell.AddElement(New Phrase(s_address2, font1))
            End If
            If s_address3 <> "" Then
                cell.AddElement(New Phrase(s_address3, font1))
            End If
            cell.AddElement(New Phrase(s_citystatezip, font1))
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("PO", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(purchaseorder, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Accepted", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(confirm_date, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Contact", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(c_contact, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Ship Method", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(ship_method, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Phone", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(c_phone, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Options", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(ship_options, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            doc.Add(table)
            'end header table

            commandString = "select * from t_order_line where orderID=@orderID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@orderID", orderID)
            reader = comm.ExecuteReader

            'lines table
            table = New PdfPTable(12)
            table.WidthPercentage = 100

            'lines header row
            cell = New PdfPCell(New Phrase("Manufacturer", font2))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Part Number", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Qty", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("BO", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Ship", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Price", font2))
            cell.Colspan = 2
            cell.HorizontalAlignment = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Extended", font2))
            cell.Colspan = 2
            cell.HorizontalAlignment = 2
            cell.Border = 0
            table.AddCell(cell)
            'end header row

            Dim manufacturer, partnumber, item, quantity, price, extended As String
            Dim subtotal As Double = 0
            Dim salestax As Double = 0
            Dim total As Double = 0
            While reader.Read
                manufacturer = reader.Item("manufacturer")
                partnumber = reader.Item("partnumber")
                item = reader.Item("item")
                quantity = reader.Item("quantity")
                price = FormatCurrency(reader.Item("price"), 2) & " " & reader.Item("uom")
                extended = FormatCurrency(reader.Item("price") * reader.Item("quantity"), 2)
                subtotal += extended

                'start line row
                cell = New PdfPCell(New Phrase(manufacturer, font1))
                cell.Colspan = 3
                cell.Border = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(partnumber, font1))
                cell.Colspan = 2
                cell.Border = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(quantity, font1))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font1))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(" ", font1))
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                cell.Border = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(price, font1))
                cell.Colspan = 2
                cell.HorizontalAlignment = 2
                cell.Border = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(extended, font1))
                cell.Colspan = 2
                cell.HorizontalAlignment = 2
                cell.Border = 1
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" ", font1))
                cell.Colspan = 3
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(item, font1))
                cell.Colspan = 9
                cell.Border = 0
                table.AddCell(cell)
                'end line row

            End While

            reader.Close()
            conn.Close()
            If chargetax = True Then
                salestax = subtotal * GetSalesTax(companyID, vendorID)
            Else
                salestax = 0
            End If
            total = subtotal + salestax

            table.HeaderRows = 1
            doc.Add(table)

            table = New PdfPTable(12)
            table.WidthPercentage = 100

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Sub Total", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(FormatCurrency(subtotal, 2), font2))
            cell.HorizontalAlignment = 2
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Freight", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("TBD", font2))
            cell.HorizontalAlignment = 2
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Sales Tax", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(FormatCurrency(salestax, 2), font2))
            cell.HorizontalAlignment = 2
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Total", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(FormatCurrency(total, 2), font2))
            cell.HorizontalAlignment = 2
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)

            doc.Add(table)


            doc.Close()
        End If
    End Sub

    Public Shared Sub CreditMemoPDF(ByVal shipmentID As Integer, ByVal applicationpath As String, ByVal imagefile As String, ByVal filename As String)
        Dim conn As New SqlConnection(ConnectionString)
        conn.Open()
        Dim commandString As String = "select * from v_shipments where shipmentID=@shipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            Dim doc As Document = New Document(PageSize.LETTER)
            Dim customer As String = reader.Item("company")
            Dim companyID As Integer = reader.Item("companyID")
            Dim vendorID As Integer = reader.Item("vendorID")
            Dim placedby As String = reader.Item("placedby").ToString
            Dim shipment_date As String = reader.Item("shipment_date").ToString
            Dim purchaseorder As String = reader.Item("purchaseorder").ToString
            Dim billto As String = reader.Item("company").ToString
            Dim b_address1 As String = reader.Item("b_address1").ToString
            Dim b_address2 As String = reader.Item("b_address2").ToString
            Dim b_citystatezip As String = reader.Item("b_city") & ", " & reader.Item("b_state") & " " & reader.Item("b_zipcode")
            Dim shipto As String = reader.Item("shipto").ToString
            Dim s_address1 As String = reader.Item("s_address1").ToString
            Dim s_address2 As String = reader.Item("s_address2").ToString
            Dim s_address3 As String = reader.Item("s_address3").ToString
            Dim s_citystatezip As String = reader.Item("s_city") & ", " & reader.Item("s_state") & " " & reader.Item("s_zipcode")
            Dim c_contact As String = reader.Item("c_contact").ToString
            Dim c_phone As String = reader.Item("c_phone").ToString
            Dim c_fax As String = reader.Item("c_fax").ToString

            Dim ship_method As String = reader.Item("ship_method").ToString
            Dim ship_options As String = reader.Item("ship_options").ToString
            Dim carrier As String = reader.Item("carrier")
            Dim ship_charge As String = FormatCurrency(reader.Item("ship_charge"), 2)
            Dim tracking As String = reader.Item("tracking")

            Dim kit As String = "No"
            If reader.Item("is_kit") = True Then
                kit = "Yes"
            End If

            reader.Close()

            PdfWriter.GetInstance(doc, New FileStream(applicationpath + filename, FileMode.Create))
            Dim basefont1 As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, False)
            Dim font1 As Font = FontFactory.GetFont("Century Gothic", 10)
            Dim font2 As Font = FontFactory.GetFont("Century Gothic", 10, Font.BOLD)
            Dim font3 As Font = FontFactory.GetFont("Century Gothic", 18, Font.BOLD)

            doc.Open()
            doc.SetPageSize(PageSize.A4)
            doc.SetMargins(36, 36, 72, 72)
            doc.SetMarginMirroring(True)

            Dim logo As Image = Image.GetInstance(applicationpath + imagefile)
            logo.Alignment = Element.ALIGN_LEFT
            doc.Add(logo)

            Dim cell As PdfPCell

            'header table
            Dim table As PdfPTable = New PdfPTable(8)
            table.WidthPercentage = 100

            cell = New PdfPCell(New Phrase("Credit Memo " & shipmentID, font3))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            'header row 0
            cell = New PdfPCell(New Phrase("PO", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(purchaseorder, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Remit To", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("DFO Filters & Equipment", font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Ordered By", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(c_contact, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("5002 S. 40th Street Ste C", font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Returned", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(shipment_date, font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(" ", font2))
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Phoenix, AZ 85036", font1))
            cell.Colspan = 3
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase("Bill To", font2))
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell()
            cell.Colspan = 3
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Border = 0
            cell.AddElement(New Phrase(billto, font1))
            cell.AddElement(New Phrase(b_address1, font1))
            If b_address2 <> "" Then
                cell.AddElement(New Phrase(b_address2, font1))
            End If
            cell.AddElement(New Phrase(b_citystatezip, font1))
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Ship To", font2))
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Colspan = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell()
            cell.Colspan = 3
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            cell.Border = 0
            cell.AddElement(New Phrase(shipto, font1))
            cell.AddElement(New Phrase(s_address1, font1))
            If s_address2 <> "" Then
                cell.AddElement(New Phrase(s_address2, font1))
            End If
            If s_address3 <> "" Then
                cell.AddElement(New Phrase(s_address3, font1))
            End If
            cell.AddElement(New Phrase(s_citystatezip, font1))
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 2
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)

            doc.Add(table)
            'end header table

            commandString = "select * from v_shipmentlines where shipmentID=@shipmentID"
            comm = New SqlCommand(commandString, conn)
            comm.Parameters.AddWithValue("@shipmentID", shipmentID)
            reader = comm.ExecuteReader

            'lines table
            table = New PdfPTable(12)
            table.WidthPercentage = 100

            'lines header row
            cell = New PdfPCell(New Phrase("Qty", font2))
            cell.Colspan = 1
            cell.Border = 0
            cell.HorizontalAlignment = 1
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Item", font2))
            cell.Colspan = 5
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Ship", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("BO", font2))
            cell.Colspan = 1
            cell.HorizontalAlignment = 1
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Price", font2))
            cell.Colspan = 2
            cell.HorizontalAlignment = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Extended", font2))
            cell.Colspan = 2
            cell.HorizontalAlignment = 2
            cell.Border = 0
            table.AddCell(cell)
            'end header row

            Dim manufacturer, partnumber, item, quantity, ship_qty, price, extended As String
            Dim backorder As Double = 0
            Dim subtotal As Double = 0
            Dim tax_subtotal As Double = 0
            Dim salestax As Double = 0
            Dim total As Double = 0
            While reader.Read
                manufacturer = reader.Item("manufacturer")
                partnumber = reader.Item("partnumber")
                item = reader.Item("item")
                quantity = reader.Item("quantity")
                ship_qty = reader.Item("ship_qty")
                backorder = appcode.GetBO(reader.Item("lineID"))
                price = FormatCurrency(reader.Item("price"), 2) & " " & reader.Item("uom")
                extended = FormatCurrency(reader.Item("price") * reader.Item("ship_qty"), 2)
                subtotal += extended
                If IsTaxable(reader.Item("manufacturer"), reader.Item("partnumber")) = True Then
                    tax_subtotal = tax_subtotal + extended
                End If

                'start line row
                cell = New PdfPCell(New Phrase(quantity, font1))
                cell.Border = 0
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(manufacturer & " " & partnumber, font1))
                cell.Border = 0
                cell.Colspan = 5
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(ship_qty, font1))
                cell.Border = 0
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(backorder, font1))
                cell.Border = 0
                cell.Colspan = 1
                cell.HorizontalAlignment = 1
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(price, font1))
                cell.Border = 0
                cell.Colspan = 2
                cell.HorizontalAlignment = 2
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(extended, font1))
                cell.Border = 0
                cell.Colspan = 2
                cell.HorizontalAlignment = 2
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" ", font1))
                cell.Colspan = 1
                cell.Border = 0
                table.AddCell(cell)
                cell = New PdfPCell(New Phrase(item, font1))
                cell.Colspan = 11
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell(New Phrase(" ", font1))
                cell.Colspan = 12
                cell.Border = 0
                table.AddCell(cell)

                'end line row

            End While

            reader.Close()
            conn.Close()

            salestax = tax_subtotal * GetSalesTax(companyID, vendorID)
            total = subtotal + salestax + ship_charge

            table.HeaderRows = 1
            doc.Add(table)

            table = New PdfPTable(12)
            table.WidthPercentage = 100

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Sub Total", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(FormatCurrency(subtotal, 2), font2))
            cell.HorizontalAlignment = 2
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Freight", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(ship_charge, font2))
            cell.HorizontalAlignment = 2
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Sales Tax", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(FormatCurrency(salestax, 2), font2))
            cell.HorizontalAlignment = 2
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell(New Phrase(" "))
            cell.Colspan = 8
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Total", font2))
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase(FormatCurrency(total, 2), font2))
            cell.HorizontalAlignment = 2
            cell.Colspan = 2
            cell.Border = 0
            table.AddCell(cell)

            doc.Add(table)

            doc.Close()
        End If
    End Sub

    Public Shared Function RGAConfirmationHTML(ByVal orderID As Integer, ByVal message As String) As String
        RGAConfirmationHTML = ""
        RGAConfirmationHTML &= "<html><body><table width='100%'>"
        Dim companyID, vendorID As Integer
        Dim notes As String = ""
        Dim subtotal As Double = 0
        Dim tax_subtotal As Double = 0

        'Vendor Section
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            companyID = reader.Item("companyID")
            vendorID = reader.Item("vendorID")

            Dim remitto As String = reader.Item("vendor").ToString & "<br/>"
            remitto &= reader.Item("r_address1").ToString & "<br/>"
            If reader.Item("r_address2").ToString <> "" Then
                remitto &= reader.Item("r_address2").ToString & "<br/>"
            End If
            remitto &= reader.Item("r_city").ToString & ", " & reader.Item("r_state").ToString & " " & reader.Item("r_zipcode").ToString

            Dim billto As String = reader.Item("b_address1").ToString & "<br/>"
            If reader.Item("b_address2").ToString <> "" Then
                billto &= reader.Item("b_address2").ToString & "<br/>"
            End If
            billto &= reader.Item("b_city").ToString & ", " & reader.Item("b_state").ToString & " " & reader.Item("b_zipcode").ToString

            Dim shipto As String = reader.Item("s_address1").ToString & "<br/>"
            If reader.Item("s_address2").ToString <> "" Then
                shipto &= reader.Item("s_address2").ToString & "<br/>"
            End If
            If reader.Item("s_address3").ToString <> "" Then
                shipto &= reader.Item("s_address3").ToString & "<br/>"
            End If
            shipto &= reader.Item("s_city").ToString & ", " & reader.Item("s_state").ToString & " " & reader.Item("s_zipcode").ToString

            notes = reader.Item("notes").ToString

            RGAConfirmationHTML &= "<tr><td colspan='4'>"
            RGAConfirmationHTML &= "<a href='http://www.desertfleetoutfitters.com'><img alt='Desert Fleet Outfitters' src='http://www.desertfleetoutfitters.com/Images/desertlogo.jpg' /></a>"
            RGAConfirmationHTML &= "</td></tr>"

            RGAConfirmationHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            If message <> "" Then
                RGAConfirmationHTML &= "<tr><td colspan='4'><p>" & message & "</p></td></tr>"
                RGAConfirmationHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
            End If

            RGAConfirmationHTML &= "<tr>"
            RGAConfirmationHTML &= "<td width='15%' valign='top'><b>RGA ID</b></td>"
            RGAConfirmationHTML &= "<td width='35%' valign='top'>" & orderID & "</td>"
            RGAConfirmationHTML &= "<td width='15%' valign='top'><b>Remit To</b></td>"
            RGAConfirmationHTML &= "<td width='35%'>" & remitto & "</td>"
            RGAConfirmationHTML &= "</tr>"

            RGAConfirmationHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            RGAConfirmationHTML &= "<tr>"
            RGAConfirmationHTML &= "<td><b>Purchase Order</b></td>"
            RGAConfirmationHTML &= "<td>" & reader.Item("purchaseorder").ToString & "</td>"
            RGAConfirmationHTML &= "<td><b>Date</b></td>"
            RGAConfirmationHTML &= "<td>" & reader.Item("order_date").ToString & "</td>"
            RGAConfirmationHTML &= "</tr>"

            RGAConfirmationHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
            'Bill To - Ship To Section

            RGAConfirmationHTML &= "<tr>"
            RGAConfirmationHTML &= "<td><b>Bill To</b></td>"
            RGAConfirmationHTML &= "<td>" & reader.Item("company").ToString & "</td>"
            RGAConfirmationHTML &= "<td><b>Ship To</b></td>"
            RGAConfirmationHTML &= "<td>" & reader.Item("shipto").ToString & "</td>"
            RGAConfirmationHTML &= "</tr>"

            RGAConfirmationHTML &= "<tr>"
            RGAConfirmationHTML &= "<td></td>"
            RGAConfirmationHTML &= "<td valign='top'>" & billto & "</td>"
            RGAConfirmationHTML &= "<td></td>"
            RGAConfirmationHTML &= "<td valign='top'>" & shipto & "</td>"
            RGAConfirmationHTML &= "</tr>"

            RGAConfirmationHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            'Contact Section
            RGAConfirmationHTML &= "<tr>"
            RGAConfirmationHTML &= "<td><b>Contact</b></td>"
            RGAConfirmationHTML &= "<td>" & reader.Item("c_contact").ToString & "</td>"
            RGAConfirmationHTML &= "<td><b>Phone</b></td>"
            RGAConfirmationHTML &= "<td>" & reader.Item("c_phone").ToString & "</td>"
            RGAConfirmationHTML &= "</tr>"

            RGAConfirmationHTML &= "<tr>"
            RGAConfirmationHTML &= "<td></td>"
            RGAConfirmationHTML &= "<td></td>"
            RGAConfirmationHTML &= "<td><b>Fax</b></td>"
            RGAConfirmationHTML &= "<td>" & reader.Item("c_fax").ToString & "</td>"
            RGAConfirmationHTML &= "</tr>"

            RGAConfirmationHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
        End If
        reader.Close()

        'Lines Header Section
        RGAConfirmationHTML &= "<tr>"
        RGAConfirmationHTML &= "<td colspan='4'>"

        RGAConfirmationHTML &= "<table style='width: 100%'>"

        RGAConfirmationHTML &= "<tr>"
        RGAConfirmationHTML &= "<td><b>Manufacturer</b></td>"
        RGAConfirmationHTML &= "<td><b>Part Number</b></td>"
        RGAConfirmationHTML &= "<td align='center'><b>Qty</b></td>"
        RGAConfirmationHTML &= "<td align='right'><b>Price</b></td>"
        RGAConfirmationHTML &= "<td align='center'><b>UOM</b></td>"
        RGAConfirmationHTML &= "<td><b>Availability</b></td>"
        RGAConfirmationHTML &= "<td align='right'><b>Extended</b></td>"
        RGAConfirmationHTML &= "</tr>"

        commandString = "select * from t_order_line where orderID=@orderID order by lineID"
        comm = New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        reader = comm.ExecuteReader
        While reader.Read
            'Lines Section
            RGAConfirmationHTML &= "<tr>"
            RGAConfirmationHTML &= "<td>" & reader.Item("manufacturer").ToString & "</td>"
            RGAConfirmationHTML &= "<td>" & reader.Item("partnumber").ToString & "</td>"
            RGAConfirmationHTML &= "<td align='center'>" & reader.Item("quantity").ToString & "</td>"
            RGAConfirmationHTML &= "<td align='right'>" & FormatCurrency(reader.Item("price"), 2) & "</td>"
            RGAConfirmationHTML &= "<td align='center'>" & reader.Item("uom").ToString & "</td>"
            RGAConfirmationHTML &= "<td>" & reader.Item("availability").ToString & "</td>"
            RGAConfirmationHTML &= "<td align='right'>" & FormatCurrency(reader.Item("price") * reader.Item("quantity")) & "</td>"
            RGAConfirmationHTML &= "</tr>"
            RGAConfirmationHTML &= "<tr>"
            RGAConfirmationHTML &= "<td colspan='7'>" & reader.Item("item").ToString & "</td>"
            RGAConfirmationHTML &= "</tr>"
            RGAConfirmationHTML &= "<tr><td colspan='7'><hr/></td></tr>"

            subtotal += reader.Item("price") * reader.Item("quantity")
            If IsTaxable(reader.Item("manufacturer"), reader.Item("partnumber")) = True Then
                tax_subtotal = tax_subtotal + (reader.Item("price") * reader.Item("quantity"))
            End If
        End While
        reader.Close()

        RGAConfirmationHTML &= "<tr><td colspan='8'><p> </p></td></tr>"

        'Footer Section
        Dim salestax As Double = GetSalesTax(companyID, vendorID) * tax_subtotal
        Dim total As Double = subtotal + salestax
        RGAConfirmationHTML &= "<tr>"
        RGAConfirmationHTML &= "<td></td>"
        RGAConfirmationHTML &= "<td></td>"
        RGAConfirmationHTML &= "<td></td>"
        RGAConfirmationHTML &= "<td></td>"
        RGAConfirmationHTML &= "<td colspan='2'><b>Sub-Total</b></td>"
        RGAConfirmationHTML &= "<td align='right'>" & FormatCurrency(subtotal, 2) & "</td>"
        RGAConfirmationHTML &= "</tr>"

        RGAConfirmationHTML &= "<tr>"
        RGAConfirmationHTML &= "<td></td>"
        RGAConfirmationHTML &= "<td></td>"
        RGAConfirmationHTML &= "<td></td>"
        RGAConfirmationHTML &= "<td></td>"
        RGAConfirmationHTML &= "<td colspan='2'><b>Sales Tax</b></td>"
        RGAConfirmationHTML &= "<td align='right'>" & FormatCurrency(salestax, 2) & "</td>"
        RGAConfirmationHTML &= "</tr>"

        RGAConfirmationHTML &= "<tr>"
        RGAConfirmationHTML &= "<td></td>"
        RGAConfirmationHTML &= "<td></td>"
        RGAConfirmationHTML &= "<td></td>"
        RGAConfirmationHTML &= "<td></td>"
        RGAConfirmationHTML &= "<td colspan='2'><b>Total</b></td>"
        RGAConfirmationHTML &= "<td align='right'>" & FormatCurrency(total, 2) & "</td>"
        RGAConfirmationHTML &= "</tr>"

        RGAConfirmationHTML &= "</table>"

        RGAConfirmationHTML &= "</td>"
        RGAConfirmationHTML &= "</tr>"

        RGAConfirmationHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

        'Notes Section
        RGAConfirmationHTML &= "<tr>"
        RGAConfirmationHTML &= "<td colspan='4'><b>Notes</b></td>"
        RGAConfirmationHTML &= "</tr>"

        RGAConfirmationHTML &= "<tr>"
        RGAConfirmationHTML &= "<td colspan='4'>" & notes & "</td>"
        RGAConfirmationHTML &= "</tr>"

        RGAConfirmationHTML &= "</table></body></html>"
    End Function

    Public Shared Function KitStatusReportHTML(ByVal companyID As Integer, ByVal imgfile As String, ByVal site_address As String) As String
        KitStatusReportHTML = ""
        KitStatusReportHTML &= "<html><body><table width='100%'>"
        Dim equipment As String = ""
        Dim kit As String = ""
        Dim equipmentID As Integer = 0
        Dim interval As String = ""
        Dim assetID As String = "0"
        Dim hours_miles As Double = 0
        Dim est_delivery As String = ""
        Dim rts As Boolean = False
        Dim orderID As Integer = 0

        'Vendor Section
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "SELECT orderID, kitID, assetID, equipmentID, serviceprofileID, interval, interval_type, hours_miles, companyID, company, order_date FROM v_kit_lines WHERE (complete = @complete) AND companyID=@companyID GROUP BY orderID, kitID, companyID, company, assetID, equipmentID, serviceprofileID, interval, interval_type, hours_miles, order_date ORDER BY company, assetID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@companyID", companyID)
        comm.Parameters.AddWithValue("@complete", False)
        Dim reader As SqlDataReader = comm.ExecuteReader

        KitStatusReportHTML &= "<tr><td colspan='6'>"
        KitStatusReportHTML &= "<a href='" & site_address & "'><img alt='DFO Filters & Equipment' src='" & imgfile & "'/></a>"
        KitStatusReportHTML &= "</td></tr>"

        KitStatusReportHTML &= "<tr><td colspan='6'><p> </p></td></tr>"

        KitStatusReportHTML &= "<tr><td colspan='6'><p>This is a status update for pm kits currently on order. For more detailed information, login to your account at <a:href='http://www.dfofilters.com'>www.dfofilters.com</a>. If you have questions, please reply to this email or contact us at 480-295-1676. Thank you and have a great day!</p></td></tr>"
        KitStatusReportHTML &= "<tr><td colspan='6'><p> </p></td></tr>"

        KitStatusReportHTML &= "<tr>"
        KitStatusReportHTML &= "<td><b>Asset ID</b></td>"
        KitStatusReportHTML &= "<td><b>Description</b></td>"
        KitStatusReportHTML &= "<td><b>Interval</b></td>"
        KitStatusReportHTML &= "<td><b>Hours/Miles</b></td>"
        KitStatusReportHTML &= "<td><b>Est. Delivery</b></td>"
        KitStatusReportHTML &= "<td><b>Order ID</b></td>"
        KitStatusReportHTML &= "</tr>"

        While reader.Read
            companyID = reader.Item("companyID")
            assetID = reader.Item("assetID")
            equipment = appcode.GetEquipment(reader.Item("equipmentID"))
            kit = appcode.GetKitName(reader.Item("serviceprofileID"))
            hours_miles = reader.Item("hours_miles")
            interval = reader.Item("interval") & " " & reader.Item("interval_type")
            est_delivery = GetEstimatedDelivery(reader.Item("orderID"))
            rts = isKitComplete(reader.Item("serviceprofileID"), reader.Item("orderID"))
            orderID = reader.Item("orderID")

            KitStatusReportHTML &= "<tr>"
            KitStatusReportHTML &= "<td>" & assetID & "</td>"
            KitStatusReportHTML &= "<td>" & equipment & "</td>"
            KitStatusReportHTML &= "<td>" & interval & "</td>"
            KitStatusReportHTML &= "<td>" & hours_miles & "</td>"
            KitStatusReportHTML &= "<td>" & est_delivery & "</td>"
            KitStatusReportHTML &= "<td>" & orderID & "</td>"
            KitStatusReportHTML &= "</tr>"

        End While
        reader.Close()

        KitStatusReportHTML &= "</table></body></html>"

    End Function

    Public Shared Function PORequestHTML(ByVal orderID As Integer, ByVal message As String, ByVal imgfile As String, ByVal site_address As String) As String
        PORequestHTML = ""
        PORequestHTML &= "<html><body><table width='100%'>"
        Dim companyID, vendorID As Integer
        Dim equipment As String = ""
        Dim kit As String = ""
        Dim equipmentID As Integer = 0
        Dim assetID As String = "0"
        Dim hours_miles As Double = 0
        Dim notes As String = ""
        Dim subtotal As Double = 0
        Dim tax_subtotal As Double = 0

        'Vendor Section
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        Dim chargetax As Boolean = False
        If reader.Read Then
            companyID = reader.Item("companyID")
            vendorID = reader.Item("vendorID")
            If reader.Item("serviceprofileID") <> 0 Then
                assetID = appcode.getAssetID(reader.Item("serviceprofileID"))
                equipment = appcode.GetEquipment(appcode.GetEquipmentIDFromKitID(reader.Item("serviceprofileID")))
                kit = appcode.GetKitName(reader.Item("serviceprofileID"))
                hours_miles = reader.Item("hours_miles")
            End If
            chargetax = reader.Item("chargetax")
            Dim remitto As String = "DFO Filters & Equipment<br/>"
            remitto &= reader.Item("r_address1").ToString & "<br/>"
            If reader.Item("r_address2").ToString <> "" Then
                remitto &= reader.Item("r_address2").ToString & "<br/>"
            End If
            remitto &= reader.Item("r_city").ToString & ", " & reader.Item("r_state").ToString & " " & reader.Item("r_zipcode").ToString

            Dim billto As String = reader.Item("b_address1").ToString & "<br/>"
            If reader.Item("b_address2").ToString <> "" Then
                billto &= reader.Item("b_address2").ToString & "<br/>"
            End If
            billto &= reader.Item("b_city").ToString & ", " & reader.Item("b_state").ToString & " " & reader.Item("b_zipcode").ToString

            Dim shipto As String = reader.Item("s_address1").ToString & "<br/>"
            If reader.Item("s_address2").ToString <> "" Then
                shipto &= reader.Item("s_address2").ToString & "<br/>"
            End If
            If reader.Item("s_address3").ToString <> "" Then
                shipto &= reader.Item("s_address3").ToString & "<br/>"
            End If
            shipto &= reader.Item("s_city").ToString & ", " & reader.Item("s_state").ToString & " " & reader.Item("s_zipcode").ToString

            notes = reader.Item("notes").ToString

            PORequestHTML &= "<tr><td colspan='4'>"
            PORequestHTML &= "<a href='" & site_address & "'><img alt='DFO Filters & Equipment' src='" & imgfile & "'/></a>"
            PORequestHTML &= "</td></tr>"

            PORequestHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            If message <> "" Then
                PORequestHTML &= "<tr><td colspan='4'><p>" & message & "</p></td></tr>"
                PORequestHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
            End If

            PORequestHTML &= "<tr><td colspan='4'><p>Thank you for placing your order with DFO Filters & Equipment. In order to process your order, we require a Purchase Order. Please reply with PO to this email or contact us at 480-295-1676. Thank you for your business!</p></td></tr>"
            PORequestHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            PORequestHTML &= "<tr>"
            PORequestHTML &= "<td width='15%' valign='top'><b>Order ID</b></td>"
            PORequestHTML &= "<td width='35%' valign='top'>" & orderID & "</td>"
            PORequestHTML &= "<td width='15%' valign='top'></td>"
            PORequestHTML &= "<td width='35%'></td>"
            PORequestHTML &= "</tr>"

            PORequestHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            PORequestHTML &= "<tr>"
            PORequestHTML &= "<td><b>Purchase Order</b></td>"
            PORequestHTML &= "<td>" & reader.Item("purchaseorder").ToString & "</td>"
            PORequestHTML &= "<td><b>Date</b></td>"
            PORequestHTML &= "<td>" & reader.Item("order_date").ToString & "</td>"
            PORequestHTML &= "</tr>"

            PORequestHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
            'Bill To - Ship To Section

            PORequestHTML &= "<tr>"
            PORequestHTML &= "<td><b>Bill To</b></td>"
            PORequestHTML &= "<td>" & reader.Item("company").ToString & "</td>"
            PORequestHTML &= "<td><b>Ship To</b></td>"
            PORequestHTML &= "<td>" & reader.Item("shipto").ToString & "</td>"
            PORequestHTML &= "</tr>"

            PORequestHTML &= "<tr>"
            PORequestHTML &= "<td></td>"
            PORequestHTML &= "<td valign='top'>" & billto & "</td>"
            PORequestHTML &= "<td></td>"
            PORequestHTML &= "<td valign='top'>" & shipto & "</td>"
            PORequestHTML &= "</tr>"

            PORequestHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            'Contact Section
            PORequestHTML &= "<tr>"
            PORequestHTML &= "<td><b>Contact</b></td>"
            PORequestHTML &= "<td>" & reader.Item("c_contact").ToString & "</td>"
            PORequestHTML &= "<td><b>Phone</b></td>"
            PORequestHTML &= "<td>" & reader.Item("c_phone").ToString & "</td>"
            PORequestHTML &= "</tr>"

            PORequestHTML &= "<tr>"
            PORequestHTML &= "<td></td>"
            PORequestHTML &= "<td></td>"
            PORequestHTML &= "<td><b>Fax</b></td>"
            PORequestHTML &= "<td>" & reader.Item("c_fax").ToString & "</td>"
            PORequestHTML &= "</tr>"

            PORequestHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            If reader.Item("serviceprofileID") > 0 Then
                PORequestHTML &= "<tr><td colspan='4'><table style='background-color: #CCCCCC; width: 100%'>"
                PORequestHTML &= "<tr>"
                PORequestHTML &= "<td width='15%'><b>Asset ID</b></td>"
                PORequestHTML &= "<td>" & assetID & "</td>"
                PORequestHTML &= "</tr>"

                PORequestHTML &= "<tr>"
                PORequestHTML &= "<td width='15%'><b>Equipment</b></td>"
                PORequestHTML &= "<td>" & equipment & "</td>"
                PORequestHTML &= "</tr>"

                PORequestHTML &= "<tr>"
                PORequestHTML &= "<td width='15%'><b>Kit</b></td>"
                PORequestHTML &= "<td>" & kit & "</td>"
                PORequestHTML &= "</tr>"

                PORequestHTML &= "<tr>"
                PORequestHTML &= "<td width='15%'><b>Hours/Miles</b></td>"
                PORequestHTML &= "<td>" & hours_miles & "</td>"
                PORequestHTML &= "</tr>"
                PORequestHTML &= "</table></td></tr>"

                PORequestHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
            End If
        End If
        reader.Close()

        'Lines Header Section
        PORequestHTML &= "<tr>"
        PORequestHTML &= "<td colspan='4'>"

        PORequestHTML &= "<table style='width: 100%'>"

        PORequestHTML &= "<tr>"
        PORequestHTML &= "<td><b>Manufacturer</b></td>"
        PORequestHTML &= "<td><b>Part Number</b></td>"
        PORequestHTML &= "<td align='center'><b>Qty</b></td>"
        PORequestHTML &= "<td align='right'><b>Price</b></td>"
        PORequestHTML &= "<td align='center'><b>UOM</b></td>"
        PORequestHTML &= "<td><b>Availability</b></td>"
        PORequestHTML &= "<td align='right'><b>Extended</b></td>"
        PORequestHTML &= "</tr>"

        commandString = "select * from t_order_line where orderID=@orderID order by lineID"
        comm = New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        reader = comm.ExecuteReader
        While reader.Read
            'Lines Section
            PORequestHTML &= "<tr>"
            PORequestHTML &= "<td>" & reader.Item("manufacturer").ToString & "</td>"
            PORequestHTML &= "<td>" & reader.Item("partnumber").ToString & "</td>"
            PORequestHTML &= "<td align='center'>" & reader.Item("quantity").ToString & "</td>"
            PORequestHTML &= "<td align='right'>" & FormatCurrency(reader.Item("price"), 2) & "</td>"
            PORequestHTML &= "<td align='center'>" & reader.Item("uom").ToString & "</td>"
            PORequestHTML &= "<td>" & reader.Item("availability").ToString & "</td>"
            PORequestHTML &= "<td align='right'>" & FormatCurrency(reader.Item("price") * reader.Item("quantity")) & "</td>"
            PORequestHTML &= "</tr>"
            PORequestHTML &= "<tr>"
            PORequestHTML &= "<td colspan='7'>" & reader.Item("item").ToString & "</td>"
            PORequestHTML &= "</tr>"
            PORequestHTML &= "<tr><td colspan='7'><hr/></td></tr>"

            subtotal += reader.Item("price") * reader.Item("quantity")
            If IsTaxable(reader.Item("manufacturer"), reader.Item("partnumber")) = True Then
                tax_subtotal = tax_subtotal + (reader.Item("price") * reader.Item("quantity"))
            End If
        End While
        reader.Close()

        PORequestHTML &= "<tr><td colspan='8'><p> </p></td></tr>"

        'Footer Section
        Dim salestax As Double = 0
        Dim total As Double = 0
        If chargetax = True Then
            salestax = GetSalesTax(companyID, vendorID) * tax_subtotal
        Else
            salestax = 0
        End If
        total = subtotal + salestax
        PORequestHTML &= "<tr>"
        PORequestHTML &= "<td></td>"
        PORequestHTML &= "<td></td>"
        PORequestHTML &= "<td></td>"
        PORequestHTML &= "<td></td>"
        PORequestHTML &= "<td colspan='2'><b>Sub-Total</b></td>"
        PORequestHTML &= "<td align='right'>" & FormatCurrency(subtotal, 2) & "</td>"
        PORequestHTML &= "</tr>"

        PORequestHTML &= "<tr>"
        PORequestHTML &= "<td></td>"
        PORequestHTML &= "<td></td>"
        PORequestHTML &= "<td></td>"
        PORequestHTML &= "<td></td>"
        PORequestHTML &= "<td colspan='2'><b>Sales Tax</b></td>"
        PORequestHTML &= "<td align='right'>" & FormatCurrency(salestax, 2) & "</td>"
        PORequestHTML &= "</tr>"

        PORequestHTML &= "<tr>"
        PORequestHTML &= "<td></td>"
        PORequestHTML &= "<td></td>"
        PORequestHTML &= "<td></td>"
        PORequestHTML &= "<td></td>"
        PORequestHTML &= "<td colspan='2'><b>Total</b></td>"
        PORequestHTML &= "<td align='right'>" & FormatCurrency(total, 2) & "</td>"
        PORequestHTML &= "</tr>"

        PORequestHTML &= "</table>"

        PORequestHTML &= "</td>"
        PORequestHTML &= "</tr>"

        PORequestHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

        'Notes Section
        PORequestHTML &= "<tr>"
        PORequestHTML &= "<td colspan='4'><b>Notes</b></td>"
        PORequestHTML &= "</tr>"

        PORequestHTML &= "<tr>"
        PORequestHTML &= "<td colspan='4'>" & notes & "</td>"
        PORequestHTML &= "</tr>"

        PORequestHTML &= "</table></body></html>"
    End Function

    Public Shared Function OrderConfirmationHTML(ByVal orderID As Integer, ByVal message As String, ByVal imgfile As String, ByVal site_address As String) As String
        OrderConfirmationHTML = ""
        OrderConfirmationHTML &= "<html><body><table width='100%'>"
        Dim companyID, vendorID As Integer
        Dim equipment As String = ""
        Dim kit As String = ""
        Dim equipmentID As Integer = 0
        Dim assetID As String = "0"
        Dim hours_miles As Double = 0
        Dim notes As String = ""
        Dim subtotal As Double = 0
        Dim tax_subtotal As Double = 0

        'Vendor Section
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        Dim chargetax As Boolean = False
        If reader.Read Then
            companyID = reader.Item("companyID")
            vendorID = reader.Item("vendorID")
            If reader.Item("serviceprofileID") <> 0 Then
                assetID = appcode.getAssetID(reader.Item("serviceprofileID"))
                equipment = appcode.GetEquipment(appcode.GetEquipmentIDFromKitID(reader.Item("serviceprofileID")))
                kit = appcode.GetKitName(reader.Item("serviceprofileID"))
                hours_miles = reader.Item("hours_miles")
            End If
            chargetax = reader.Item("chargetax")
            Dim remitto As String = "DFO Filters & Equipment<br/>"
            remitto &= reader.Item("r_address1").ToString & "<br/>"
            If reader.Item("r_address2").ToString <> "" Then
                remitto &= reader.Item("r_address2").ToString & "<br/>"
            End If
            remitto &= reader.Item("r_city").ToString & ", " & reader.Item("r_state").ToString & " " & reader.Item("r_zipcode").ToString

            Dim billto As String = reader.Item("b_address1").ToString & "<br/>"
            If reader.Item("b_address2").ToString <> "" Then
                billto &= reader.Item("b_address2").ToString & "<br/>"
            End If
            billto &= reader.Item("b_city").ToString & ", " & reader.Item("b_state").ToString & " " & reader.Item("b_zipcode").ToString

            Dim shipto As String = reader.Item("s_address1").ToString & "<br/>"
            If reader.Item("s_address2").ToString <> "" Then
                shipto &= reader.Item("s_address2").ToString & "<br/>"
            End If
            If reader.Item("s_address3").ToString <> "" Then
                shipto &= reader.Item("s_address3").ToString & "<br/>"
            End If
            shipto &= reader.Item("s_city").ToString & ", " & reader.Item("s_state").ToString & " " & reader.Item("s_zipcode").ToString

            notes = reader.Item("notes").ToString

            OrderConfirmationHTML &= "<tr><td colspan='4'>"
            OrderConfirmationHTML &= "<a href='" & site_address & "'><img alt='DFO Filters & Equipment' src='" & imgfile & "'/></a>"
            OrderConfirmationHTML &= "</td></tr>"

            OrderConfirmationHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            If message <> "" Then
                OrderConfirmationHTML &= "<tr><td colspan='4'><p>" & message & "</p></td></tr>"
                OrderConfirmationHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
            End If

            OrderConfirmationHTML &= "<tr><td colspan='4'><p>Thank you for placing your order with DFO Filters & Equipment. If you have questions or concerns regarding this order, please reply to this email or contact us immediately at 480-295-1676. Have a great day!</p></td></tr>"
            OrderConfirmationHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            OrderConfirmationHTML &= "<tr>"
            OrderConfirmationHTML &= "<td width='15%' valign='top'><b>Order ID</b></td>"
            OrderConfirmationHTML &= "<td width='35%' valign='top'>" & orderID & "</td>"
            OrderConfirmationHTML &= "<td width='15%' valign='top'></td>"
            OrderConfirmationHTML &= "<td width='35%'></td>"
            OrderConfirmationHTML &= "</tr>"

            OrderConfirmationHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            OrderConfirmationHTML &= "<tr>"
            OrderConfirmationHTML &= "<td><b>Purchase Order</b></td>"
            OrderConfirmationHTML &= "<td>" & reader.Item("purchaseorder").ToString & "</td>"
            OrderConfirmationHTML &= "<td><b>Date</b></td>"
            OrderConfirmationHTML &= "<td>" & reader.Item("order_date").ToString & "</td>"
            OrderConfirmationHTML &= "</tr>"

            OrderConfirmationHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
            'Bill To - Ship To Section

            OrderConfirmationHTML &= "<tr>"
            OrderConfirmationHTML &= "<td><b>Bill To</b></td>"
            OrderConfirmationHTML &= "<td>" & reader.Item("company").ToString & "</td>"
            OrderConfirmationHTML &= "<td><b>Ship To</b></td>"
            OrderConfirmationHTML &= "<td>" & reader.Item("shipto").ToString & "</td>"
            OrderConfirmationHTML &= "</tr>"

            OrderConfirmationHTML &= "<tr>"
            OrderConfirmationHTML &= "<td></td>"
            OrderConfirmationHTML &= "<td valign='top'>" & billto & "</td>"
            OrderConfirmationHTML &= "<td></td>"
            OrderConfirmationHTML &= "<td valign='top'>" & shipto & "</td>"
            OrderConfirmationHTML &= "</tr>"

            OrderConfirmationHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            'Contact Section
            OrderConfirmationHTML &= "<tr>"
            OrderConfirmationHTML &= "<td><b>Contact</b></td>"
            OrderConfirmationHTML &= "<td>" & reader.Item("c_contact").ToString & "</td>"
            OrderConfirmationHTML &= "<td><b>Phone</b></td>"
            OrderConfirmationHTML &= "<td>" & reader.Item("c_phone").ToString & "</td>"
            OrderConfirmationHTML &= "</tr>"

            OrderConfirmationHTML &= "<tr>"
            OrderConfirmationHTML &= "<td></td>"
            OrderConfirmationHTML &= "<td></td>"
            OrderConfirmationHTML &= "<td><b>Fax</b></td>"
            OrderConfirmationHTML &= "<td>" & reader.Item("c_fax").ToString & "</td>"
            OrderConfirmationHTML &= "</tr>"

            OrderConfirmationHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            If reader.Item("serviceprofileID") > 0 Then
                OrderConfirmationHTML &= "<tr><td colspan='4'><table style='background-color: #CCCCCC; width: 100%'>"
                OrderConfirmationHTML &= "<tr>"
                OrderConfirmationHTML &= "<td width='15%'><b>Asset ID</b></td>"
                OrderConfirmationHTML &= "<td>" & assetID & "</td>"
                OrderConfirmationHTML &= "</tr>"

                OrderConfirmationHTML &= "<tr>"
                OrderConfirmationHTML &= "<td width='15%'><b>Equipment</b></td>"
                OrderConfirmationHTML &= "<td>" & equipment & "</td>"
                OrderConfirmationHTML &= "</tr>"

                OrderConfirmationHTML &= "<tr>"
                OrderConfirmationHTML &= "<td width='15%'><b>Kit</b></td>"
                OrderConfirmationHTML &= "<td>" & kit & "</td>"
                OrderConfirmationHTML &= "</tr>"

                OrderConfirmationHTML &= "<tr>"
                OrderConfirmationHTML &= "<td width='15%'><b>Hours/Miles</b></td>"
                OrderConfirmationHTML &= "<td>" & hours_miles & "</td>"
                OrderConfirmationHTML &= "</tr>"
                OrderConfirmationHTML &= "</table></td></tr>"

                OrderConfirmationHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
            End If
        End If
        reader.Close()

        'Lines Header Section
        OrderConfirmationHTML &= "<tr>"
        OrderConfirmationHTML &= "<td colspan='4'>"

        OrderConfirmationHTML &= "<table style='width: 100%'>"

        OrderConfirmationHTML &= "<tr>"
        OrderConfirmationHTML &= "<td><b>Manufacturer</b></td>"
        OrderConfirmationHTML &= "<td><b>Part Number</b></td>"
        OrderConfirmationHTML &= "<td align='center'><b>Qty</b></td>"
        OrderConfirmationHTML &= "<td align='right'><b>Price</b></td>"
        OrderConfirmationHTML &= "<td align='center'><b>UOM</b></td>"
        OrderConfirmationHTML &= "<td><b>Availability</b></td>"
        OrderConfirmationHTML &= "<td align='right'><b>Extended</b></td>"
        OrderConfirmationHTML &= "</tr>"

        commandString = "select * from t_order_line where orderID=@orderID order by lineID"
        comm = New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        reader = comm.ExecuteReader
        While reader.Read
            'Lines Section
            OrderConfirmationHTML &= "<tr>"
            OrderConfirmationHTML &= "<td>" & reader.Item("manufacturer").ToString & "</td>"
            OrderConfirmationHTML &= "<td>" & reader.Item("partnumber").ToString & "</td>"
            OrderConfirmationHTML &= "<td align='center'>" & reader.Item("quantity").ToString & "</td>"
            OrderConfirmationHTML &= "<td align='right'>" & FormatCurrency(reader.Item("price"), 2) & "</td>"
            OrderConfirmationHTML &= "<td align='center'>" & reader.Item("uom").ToString & "</td>"
            OrderConfirmationHTML &= "<td>" & reader.Item("availability").ToString & "</td>"
            OrderConfirmationHTML &= "<td align='right'>" & FormatCurrency(reader.Item("price") * reader.Item("quantity")) & "</td>"
            OrderConfirmationHTML &= "</tr>"
            OrderConfirmationHTML &= "<tr>"
            OrderConfirmationHTML &= "<td colspan='7'>" & reader.Item("item").ToString & "</td>"
            OrderConfirmationHTML &= "</tr>"
            OrderConfirmationHTML &= "<tr><td colspan='7'><hr/></td></tr>"

            subtotal += reader.Item("price") * reader.Item("quantity")
            If IsTaxable(reader.Item("manufacturer"), reader.Item("partnumber")) = True Then
                tax_subtotal = tax_subtotal + (reader.Item("price") * reader.Item("quantity"))
            End If
        End While
        reader.Close()

        OrderConfirmationHTML &= "<tr><td colspan='8'><p> </p></td></tr>"

        'Footer Section
        Dim salestax As Double = 0
        Dim total As Double = 0
        If chargetax = True Then
            salestax = GetSalesTax(companyID, vendorID) * tax_subtotal
        Else
            salestax = 0
        End If
        total = subtotal + salestax
        OrderConfirmationHTML &= "<tr>"
        OrderConfirmationHTML &= "<td></td>"
        OrderConfirmationHTML &= "<td></td>"
        OrderConfirmationHTML &= "<td></td>"
        OrderConfirmationHTML &= "<td></td>"
        OrderConfirmationHTML &= "<td colspan='2'><b>Sub-Total</b></td>"
        OrderConfirmationHTML &= "<td align='right'>" & FormatCurrency(subtotal, 2) & "</td>"
        OrderConfirmationHTML &= "</tr>"

        OrderConfirmationHTML &= "<tr>"
        OrderConfirmationHTML &= "<td></td>"
        OrderConfirmationHTML &= "<td></td>"
        OrderConfirmationHTML &= "<td></td>"
        OrderConfirmationHTML &= "<td></td>"
        OrderConfirmationHTML &= "<td colspan='2'><b>Sales Tax</b></td>"
        OrderConfirmationHTML &= "<td align='right'>" & FormatCurrency(salestax, 2) & "</td>"
        OrderConfirmationHTML &= "</tr>"

        OrderConfirmationHTML &= "<tr>"
        OrderConfirmationHTML &= "<td></td>"
        OrderConfirmationHTML &= "<td></td>"
        OrderConfirmationHTML &= "<td></td>"
        OrderConfirmationHTML &= "<td></td>"
        OrderConfirmationHTML &= "<td colspan='2'><b>Total</b></td>"
        OrderConfirmationHTML &= "<td align='right'>" & FormatCurrency(total, 2) & "</td>"
        OrderConfirmationHTML &= "</tr>"

        OrderConfirmationHTML &= "</table>"

        OrderConfirmationHTML &= "</td>"
        OrderConfirmationHTML &= "</tr>"

        OrderConfirmationHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

        'Notes Section
        OrderConfirmationHTML &= "<tr>"
        OrderConfirmationHTML &= "<td colspan='4'><b>Notes</b></td>"
        OrderConfirmationHTML &= "</tr>"

        OrderConfirmationHTML &= "<tr>"
        OrderConfirmationHTML &= "<td colspan='4'>" & notes & "</td>"
        OrderConfirmationHTML &= "</tr>"

        OrderConfirmationHTML &= "</table></body></html>"
    End Function

    Public Shared Function WebOrderHTML(ByVal orderID As Integer, ByVal message As String, ByVal imgfile As String, ByVal site_address As String) As String
        WebOrderHTML = ""
        WebOrderHTML &= "<html><body><table width='100%'>"
        Dim companyID, vendorID As Integer
        Dim notes As String = ""
        Dim subtotal As Double = 0
        Dim tax_subtotal As Double = 0

        'Vendor Section
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_order where orderID=@orderID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        Dim chargetax As Boolean = False
        If reader.Read Then
            companyID = reader.Item("companyID")
            vendorID = reader.Item("vendorID")
            chargetax = reader.Item("chargetax")
            Dim remitto As String = "DFO Filters & Equipment<br/>"
            remitto &= reader.Item("r_address1").ToString & "<br/>"
            If reader.Item("r_address2").ToString <> "" Then
                remitto &= reader.Item("r_address2").ToString & "<br/>"
            End If
            remitto &= reader.Item("r_city").ToString & ", " & reader.Item("r_state").ToString & " " & reader.Item("r_zipcode").ToString

            Dim billto As String = reader.Item("b_address1").ToString & "<br/>"
            If reader.Item("b_address2").ToString <> "" Then
                billto &= reader.Item("b_address2").ToString & "<br/>"
            End If
            billto &= reader.Item("b_city").ToString & ", " & reader.Item("b_state").ToString & " " & reader.Item("b_zipcode").ToString

            Dim shipto As String = reader.Item("s_address1").ToString & "<br/>"
            If reader.Item("s_address2").ToString <> "" Then
                shipto &= reader.Item("s_address2").ToString & "<br/>"
            End If
            If reader.Item("s_address3").ToString <> "" Then
                shipto &= reader.Item("s_address3").ToString & "<br/>"
            End If
            shipto &= reader.Item("s_city").ToString & ", " & reader.Item("s_state").ToString & " " & reader.Item("s_zipcode").ToString

            notes = reader.Item("notes").ToString

            WebOrderHTML &= "<tr><td colspan='4'>"
            WebOrderHTML &= "<a href='" & site_address & "'><img alt='DFO Filters & Equipment' src='" & imgfile & "'/></a>"
            WebOrderHTML &= "</td></tr>"

            WebOrderHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            WebOrderHTML &= "<tr><td colspan='4'><p>The following order was placed and requires attention. Please log into the web site to review this request.</p></td></tr>"
            WebOrderHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            WebOrderHTML &= "<tr>"
            WebOrderHTML &= "<td width='15%' valign='top'><b>Order ID</b></td>"
            WebOrderHTML &= "<td width='35%' valign='top'>" & orderID & "</td>"
            WebOrderHTML &= "<td width='15%' valign='top'></td>"
            WebOrderHTML &= "<td width='35%'></td>"
            WebOrderHTML &= "</tr>"

            WebOrderHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            WebOrderHTML &= "<tr>"
            WebOrderHTML &= "<td><b>Purchase Order</b></td>"
            WebOrderHTML &= "<td>" & reader.Item("purchaseorder").ToString & "</td>"
            WebOrderHTML &= "<td><b>Date</b></td>"
            WebOrderHTML &= "<td>" & reader.Item("order_date").ToString & "</td>"
            WebOrderHTML &= "</tr>"

            WebOrderHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
            'Bill To - Ship To Section

            WebOrderHTML &= "<tr>"
            WebOrderHTML &= "<td><b>Bill To</b></td>"
            WebOrderHTML &= "<td>" & reader.Item("company").ToString & "</td>"
            WebOrderHTML &= "<td><b>Ship To</b></td>"
            WebOrderHTML &= "<td>" & reader.Item("shipto").ToString & "</td>"
            WebOrderHTML &= "</tr>"

            WebOrderHTML &= "<tr>"
            WebOrderHTML &= "<td></td>"
            WebOrderHTML &= "<td valign='top'>" & billto & "</td>"
            WebOrderHTML &= "<td></td>"
            WebOrderHTML &= "<td valign='top'>" & shipto & "</td>"
            WebOrderHTML &= "</tr>"

            WebOrderHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            'Contact Section
            WebOrderHTML &= "<tr>"
            WebOrderHTML &= "<td><b>Contact</b></td>"
            WebOrderHTML &= "<td>" & reader.Item("c_contact").ToString & "</td>"
            WebOrderHTML &= "<td><b>Phone</b></td>"
            WebOrderHTML &= "<td>" & reader.Item("c_phone").ToString & "</td>"
            WebOrderHTML &= "</tr>"

            WebOrderHTML &= "<tr>"
            WebOrderHTML &= "<td></td>"
            WebOrderHTML &= "<td></td>"
            WebOrderHTML &= "<td><b>Fax</b></td>"
            WebOrderHTML &= "<td>" & reader.Item("c_fax").ToString & "</td>"
            WebOrderHTML &= "</tr>"

            WebOrderHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
        End If
        reader.Close()

        'Lines Header Section
        WebOrderHTML &= "<tr>"
        WebOrderHTML &= "<td colspan='4'>"

        WebOrderHTML &= "<table style='width: 100%'>"

        WebOrderHTML &= "<tr>"
        WebOrderHTML &= "<td><b>Manufacturer</b></td>"
        WebOrderHTML &= "<td><b>Part Number</b></td>"
        WebOrderHTML &= "<td align='center'><b>Qty</b></td>"
        WebOrderHTML &= "<td align='right'><b>Price</b></td>"
        WebOrderHTML &= "<td align='center'><b>UOM</b></td>"
        WebOrderHTML &= "<td><b>Availability</b></td>"
        WebOrderHTML &= "<td align='right'><b>Extended</b></td>"
        WebOrderHTML &= "</tr>"

        commandString = "select * from t_order_line where orderID=@orderID order by lineID"
        comm = New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@orderID", orderID)
        reader = comm.ExecuteReader
        While reader.Read
            'Lines Section
            WebOrderHTML &= "<tr>"
            WebOrderHTML &= "<td>" & reader.Item("manufacturer").ToString & "</td>"
            WebOrderHTML &= "<td>" & reader.Item("partnumber").ToString & "</td>"
            WebOrderHTML &= "<td align='center'>" & reader.Item("quantity").ToString & "</td>"
            WebOrderHTML &= "<td align='right'>" & FormatCurrency(reader.Item("price"), 2) & "</td>"
            WebOrderHTML &= "<td align='center'>" & reader.Item("uom").ToString & "</td>"
            WebOrderHTML &= "<td>" & reader.Item("availability").ToString & "</td>"
            WebOrderHTML &= "<td align='right'>" & FormatCurrency(reader.Item("price") * reader.Item("quantity")) & "</td>"
            WebOrderHTML &= "</tr>"
            WebOrderHTML &= "<tr>"
            WebOrderHTML &= "<td colspan='7'>" & reader.Item("item").ToString & "</td>"
            WebOrderHTML &= "</tr>"
            WebOrderHTML &= "<tr><td colspan='7'><hr/></td></tr>"

            subtotal += reader.Item("price") * reader.Item("quantity")
            If IsTaxable(reader.Item("manufacturer"), reader.Item("partnumber")) = True Then
                tax_subtotal = tax_subtotal + (reader.Item("price") * reader.Item("quantity"))
            End If
        End While
        reader.Close()

        WebOrderHTML &= "<tr><td colspan='8'><p> </p></td></tr>"

        'Footer Section
        Dim salestax As Double = 0
        Dim total As Double = 0
        If chargetax = True Then
            salestax = GetSalesTax(companyID, vendorID) * tax_subtotal
        Else
            salestax = 0
        End If
        total = subtotal + salestax
        WebOrderHTML &= "<tr>"
        WebOrderHTML &= "<td></td>"
        WebOrderHTML &= "<td></td>"
        WebOrderHTML &= "<td></td>"
        WebOrderHTML &= "<td></td>"
        WebOrderHTML &= "<td colspan='2'><b>Sub-Total</b></td>"
        WebOrderHTML &= "<td align='right'>" & FormatCurrency(subtotal, 2) & "</td>"
        WebOrderHTML &= "</tr>"

        WebOrderHTML &= "<tr>"
        WebOrderHTML &= "<td></td>"
        WebOrderHTML &= "<td></td>"
        WebOrderHTML &= "<td></td>"
        WebOrderHTML &= "<td></td>"
        WebOrderHTML &= "<td colspan='2'><b>Sales Tax</b></td>"
        WebOrderHTML &= "<td align='right'>" & FormatCurrency(salestax, 2) & "</td>"
        WebOrderHTML &= "</tr>"

        WebOrderHTML &= "<tr>"
        WebOrderHTML &= "<td></td>"
        WebOrderHTML &= "<td></td>"
        WebOrderHTML &= "<td></td>"
        WebOrderHTML &= "<td></td>"
        WebOrderHTML &= "<td colspan='2'><b>Total</b></td>"
        WebOrderHTML &= "<td align='right'>" & FormatCurrency(total, 2) & "</td>"
        WebOrderHTML &= "</tr>"

        WebOrderHTML &= "</table>"

        WebOrderHTML &= "</td>"
        WebOrderHTML &= "</tr>"

        WebOrderHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

        'Notes Section
        WebOrderHTML &= "<tr>"
        WebOrderHTML &= "<td colspan='4'><b>Notes</b></td>"
        WebOrderHTML &= "</tr>"

        WebOrderHTML &= "<tr>"
        WebOrderHTML &= "<td colspan='4'>" & notes & "</td>"
        WebOrderHTML &= "</tr>"

        WebOrderHTML &= "</table></body></html>"
    End Function

    Public Shared Function ShippingNoticeHTML(ByVal shipmentID As Integer) As String
        ShippingNoticeHTML = ""
        ShippingNoticeHTML &= "<html><body><table width='100%'>"
        Dim companyID, vendorID As Integer
        Dim orderID As String = ""
        Dim notes As String = ""
        Dim ship_charge As Double

        'Vendor Section
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_shipments where shipmentID=@shipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            companyID = reader.Item("companyID")
            vendorID = reader.Item("vendorID")
            notes = reader.Item("notes").ToString
            ship_charge = reader.Item("ship_charge").ToString
            orderID = reader.Item("orderID").ToString

            Dim remitto As String = reader.Item("r_address1").ToString & "<br/>"
            If reader.Item("r_address2").ToString <> "" Then
                remitto &= reader.Item("r_address2").ToString & "<br/>"
            End If
            remitto &= reader.Item("r_city").ToString & ", " & reader.Item("r_state").ToString & " " & reader.Item("r_zipcode").ToString

            Dim billto As String = reader.Item("b_address1").ToString & "<br/>"
            If reader.Item("b_address2").ToString <> "" Then
                billto &= reader.Item("b_address2").ToString & "<br/>"
            End If
            billto &= reader.Item("b_city").ToString & ", " & reader.Item("b_state").ToString & " " & reader.Item("b_zipcode").ToString

            Dim shipto As String = reader.Item("s_address1").ToString & "<br/>"
            If reader.Item("s_address2").ToString <> "" Then
                shipto &= reader.Item("s_address2").ToString & "<br/>"
            End If
            If reader.Item("s_address3").ToString <> "" Then
                shipto &= reader.Item("s_address3").ToString & "<br/>"
            End If
            shipto &= reader.Item("s_city").ToString & ", " & reader.Item("s_state").ToString & " " & reader.Item("s_zipcode").ToString

            ShippingNoticeHTML &= "<tr>"
            ShippingNoticeHTML &= "<td width='15%'><b>Order ID</b></td>"
            ShippingNoticeHTML &= "<td width='35%'>" & orderID & "</td>"
            ShippingNoticeHTML &= "<td width='15%' valign='top'></td>"
            ShippingNoticeHTML &= "<td width='35%'></td>"
            ShippingNoticeHTML &= "</tr>"

            ShippingNoticeHTML &= "<tr>"
            ShippingNoticeHTML &= "<td width='15%'><b>Shipment ID</b></td>"
            ShippingNoticeHTML &= "<td width='35%'>" & shipmentID & "</td>"
            ShippingNoticeHTML &= "<td width='15%'></td>"
            ShippingNoticeHTML &= "<td width='35%'></td>"
            ShippingNoticeHTML &= "</tr>"

            ShippingNoticeHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            'Bill To - Ship To Section

            ShippingNoticeHTML &= "<tr>"
            ShippingNoticeHTML &= "<td><b>Bill To</b></td>"
            ShippingNoticeHTML &= "<td>" & reader.Item("company").ToString & "</td>"
            ShippingNoticeHTML &= "<td><b>Ship To</b></td>"
            ShippingNoticeHTML &= "<td>" & reader.Item("shipto").ToString & "</td>"
            ShippingNoticeHTML &= "</tr>"

            ShippingNoticeHTML &= "<tr>"
            ShippingNoticeHTML &= "<td></td>"
            ShippingNoticeHTML &= "<td valign='top'>" & billto & "</td>"
            ShippingNoticeHTML &= "<td></td>"
            ShippingNoticeHTML &= "<td valign='top'>" & shipto & "</td>"
            ShippingNoticeHTML &= "</tr>"

            ShippingNoticeHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            'Contact Section
            ShippingNoticeHTML &= "<tr>"
            ShippingNoticeHTML &= "<td><b>Contact</b></td>"
            ShippingNoticeHTML &= "<td>" & reader.Item("c_contact").ToString & "</td>"
            ShippingNoticeHTML &= "<td><b>Phone</b></td>"
            ShippingNoticeHTML &= "<td>" & reader.Item("c_phone").ToString & "</td>"
            ShippingNoticeHTML &= "</tr>"

            ShippingNoticeHTML &= "<tr>"
            ShippingNoticeHTML &= "<td></td>"
            ShippingNoticeHTML &= "<td></td>"
            ShippingNoticeHTML &= "<td><b>Fax</b></td>"
            ShippingNoticeHTML &= "<td>" & reader.Item("c_fax").ToString & "</td>"
            ShippingNoticeHTML &= "</tr>"

            ShippingNoticeHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
        End If
        reader.Close()

        'Lines Header Section
        ShippingNoticeHTML &= "<tr>"
        ShippingNoticeHTML &= "<td colspan='4'>"

        ShippingNoticeHTML &= "<table style='width: 100%'>"
        ShippingNoticeHTML &= "<tr>"
        ShippingNoticeHTML &= "<td><b>Manufacturer</b></td>"
        ShippingNoticeHTML &= "<td><b>Part Number</b></td>"
        ShippingNoticeHTML &= "<td align='center'><b>Shipped</b></td>"
        ShippingNoticeHTML &= "<td align='right'><b>Price</b></td>"
        ShippingNoticeHTML &= "<td align='center'><b>UOM</b></td>"
        ShippingNoticeHTML &= "<td align='right'><b>Extended</b></td>"
        ShippingNoticeHTML &= "</tr>"

        commandString = "select * from v_shipment_lines where shipmentID=@shipmentID order by lineID"
        comm = New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        reader = comm.ExecuteReader
        Dim subtotal As Double = 0
        While reader.Read
            'Lines Section
            ShippingNoticeHTML &= "<tr>"
            ShippingNoticeHTML &= "<td>" & reader.Item("manufacturer").ToString & "</td>"
            ShippingNoticeHTML &= "<td>" & reader.Item("partnumber").ToString & "</td>"
            ShippingNoticeHTML &= "<td align='center'>" & reader.Item("ship_qty").ToString & "</td>"
            ShippingNoticeHTML &= "<td align='right'>" & FormatCurrency(reader.Item("price"), 2) & "</td>"
            ShippingNoticeHTML &= "<td align='center'>" & reader.Item("uom").ToString & "</td>"
            ShippingNoticeHTML &= "<td align='right'>" & FormatCurrency(reader.Item("price") * reader.Item("ship_qty")) & "</td>"
            ShippingNoticeHTML &= "</tr>"
            ShippingNoticeHTML &= "<tr>"
            ShippingNoticeHTML &= "<td colspan='6'>" & reader.Item("item").ToString & "</td>"
            ShippingNoticeHTML &= "</tr>"
            ShippingNoticeHTML &= "<tr><td colspan='6'><hr/></td></tr>"
            subtotal += reader.Item("price") * reader.Item("ship_qty")
        End While
        reader.Close()

        ShippingNoticeHTML &= "<tr><td colspan='6'><p> </p></td></tr>"

        'Footer Section
        Dim salestax As Double = GetSalesTax(companyID, vendorID) * subtotal
        Dim total As Double = subtotal + salestax + ship_charge
        ShippingNoticeHTML &= "<tr>"
        ShippingNoticeHTML &= "<td></td>"
        ShippingNoticeHTML &= "<td></td>"
        ShippingNoticeHTML &= "<td></td>"
        ShippingNoticeHTML &= "<td colspan='2'><b>Sub-Total</b></td>"
        ShippingNoticeHTML &= "<td align='right'>" & FormatCurrency(subtotal, 2) & "</td>"
        ShippingNoticeHTML &= "</tr>"

        ShippingNoticeHTML &= "<tr>"
        ShippingNoticeHTML &= "<td></td>"
        ShippingNoticeHTML &= "<td></td>"
        ShippingNoticeHTML &= "<td></td>"
        ShippingNoticeHTML &= "<td colspan='2'><b>Sales Tax</b></td>"
        ShippingNoticeHTML &= "<td align='right'>" & FormatCurrency(salestax, 2) & "</td>"
        ShippingNoticeHTML &= "</tr>"

        ShippingNoticeHTML &= "<tr>"
        ShippingNoticeHTML &= "<td></td>"
        ShippingNoticeHTML &= "<td></td>"
        ShippingNoticeHTML &= "<td></td>"
        ShippingNoticeHTML &= "<td colspan='2'><b>Shipping</b></td>"
        ShippingNoticeHTML &= "<td align='right'>" & FormatCurrency(ship_charge, 2) & "</td>"
        ShippingNoticeHTML &= "</tr>"

        ShippingNoticeHTML &= "<tr>"
        ShippingNoticeHTML &= "<td></td>"
        ShippingNoticeHTML &= "<td></td>"
        ShippingNoticeHTML &= "<td></td>"
        ShippingNoticeHTML &= "<td colspan='2'><b>Total<b></td>"
        ShippingNoticeHTML &= "<td align='right'>" & FormatCurrency(total, 2) & "</td>"
        ShippingNoticeHTML &= "</tr>"

        ShippingNoticeHTML &= "</table>"

        ShippingNoticeHTML &= "</td>"
        ShippingNoticeHTML &= "</tr>"

        ShippingNoticeHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

        'Notes Section
        ShippingNoticeHTML &= "<tr>"
        ShippingNoticeHTML &= "<td colspan='4'><b>Notes</b></td>"
        ShippingNoticeHTML &= "</tr>"

        ShippingNoticeHTML &= "<tr>"
        ShippingNoticeHTML &= "<td colspan='4'>" & notes & "</td>"
        ShippingNoticeHTML &= "</tr>"

        ShippingNoticeHTML &= "</table></body></html>"
    End Function

    Public Shared Function CreditMemoHTML(ByVal shipmentID As Integer, ByVal message As String) As String
        CreditMemoHTML = ""
        CreditMemoHTML &= "<html><body><table width='100%'>"

        Dim companyID, vendorID As Integer
        Dim notes As String = ""
        Dim invoiceID As String = ""
        Dim orderID As String = ""
        Dim purchaseorder As String = ""
        Dim ship_charge As Double

        'Vendor Section
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_shipments where shipmentID=@shipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            companyID = reader.Item("companyID")
            vendorID = reader.Item("vendorID")
            notes = reader.Item("notes").ToString
            ship_charge = reader.Item("ship_charge").ToString
            invoiceID = reader.Item("invoiceID").ToString
            orderID = reader.Item("orderID").ToString
            purchaseorder = reader.Item("purchaseorder")

            Dim remitto As String = reader.Item("vendor").ToString & "<br/>"
            remitto &= reader.Item("r_address1").ToString & "<br/>"
            If reader.Item("r_address2").ToString <> "" Then
                remitto &= reader.Item("r_address2").ToString & "<br/>"
            End If
            remitto &= reader.Item("r_city").ToString & ", " & reader.Item("r_state").ToString & " " & reader.Item("r_zipcode").ToString

            Dim billto As String = reader.Item("b_address1").ToString & "<br/>"
            If reader.Item("b_address2").ToString <> "" Then
                billto &= reader.Item("b_address2").ToString & "<br/>"
            End If
            billto &= reader.Item("b_city").ToString & ", " & reader.Item("b_state").ToString & " " & reader.Item("b_zipcode").ToString

            Dim shipto As String = reader.Item("s_address1").ToString & "<br/>"
            If reader.Item("s_address2").ToString <> "" Then
                shipto &= reader.Item("s_address2").ToString & "<br/>"
            End If
            If reader.Item("s_address3").ToString <> "" Then
                shipto &= reader.Item("s_address3").ToString & "<br/>"
            End If
            shipto &= reader.Item("s_city").ToString & ", " & reader.Item("s_state").ToString & " " & reader.Item("s_zipcode").ToString

            CreditMemoHTML &= "<tr><td colspan='4'>"
            CreditMemoHTML &= "<a href='http://www.desertfleetoutfitters.com'><img alt='Desert Fleet Outfitters' src='http://www.desertfleetoutfitters.com/Images/desertlogo.jpg' /></a>"
            CreditMemoHTML &= "</td></tr>"

            CreditMemoHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            If message <> "" Then
                CreditMemoHTML &= "<tr><td colspan='4'><p>" & message & "</p></td></tr>"
                CreditMemoHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
            End If

            CreditMemoHTML &= "<tr>"
            CreditMemoHTML &= "<td width='15%'><b>Credit Memo ID</b></td>"
            CreditMemoHTML &= "<td width='35%'>" & shipmentID & "</td>"
            CreditMemoHTML &= "<td width='15%' valign='top' rowspan='3'><b>Remit To</b></td>"
            CreditMemoHTML &= "<td width='35%' rowspan='3'>" & remitto & "</td>"
            CreditMemoHTML &= "</tr>"

            CreditMemoHTML &= "<tr>"
            CreditMemoHTML &= "<td><b>Purchase Order</b></td>"
            CreditMemoHTML &= "<td valign='top'>" & purchaseorder & "</td>"
            CreditMemoHTML &= "</tr>"

            CreditMemoHTML &= "<tr>"
            CreditMemoHTML &= "<td><b>Ordered By</b></td>"
            CreditMemoHTML &= "<td valign='top'>" & reader.Item("c_contact").ToString & "</td>"
            CreditMemoHTML &= "</tr>"

            CreditMemoHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            'Bill To - Ship To Section

            CreditMemoHTML &= "<tr>"
            CreditMemoHTML &= "<td><b>Bill To</b></td>"
            CreditMemoHTML &= "<td>" & reader.Item("company").ToString & "</td>"
            CreditMemoHTML &= "<td><b>Ship To</b></td>"
            CreditMemoHTML &= "<td>" & reader.Item("shipto").ToString & "</td>"
            CreditMemoHTML &= "</tr>"

            CreditMemoHTML &= "<tr>"
            CreditMemoHTML &= "<td></td>"
            CreditMemoHTML &= "<td valign='top'>" & billto & "</td>"
            CreditMemoHTML &= "<td></td>"
            CreditMemoHTML &= "<td valign='top'>" & shipto & "</td>"
            CreditMemoHTML &= "</tr>"

            CreditMemoHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            CreditMemoHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
            CreditMemoHTML &= "<tr><td colspan='4'<hr/></td></tr>"
            CreditMemoHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
        End If
        reader.Close()

        'Lines Header Section
        CreditMemoHTML &= "<tr>"
        CreditMemoHTML &= "<td colspan='4'>"

        CreditMemoHTML &= "<table style='width: 100%'>"

        CreditMemoHTML &= "<tr>"
        CreditMemoHTML &= "<td><b>Manufacturer</b></td>"
        CreditMemoHTML &= "<td><b>Part Number</b></td>"
        CreditMemoHTML &= "<td align='center'><b>Shipped</b></td>"
        CreditMemoHTML &= "<td align='right'><b>Price</b></td>"
        CreditMemoHTML &= "<td align='center'><b>UOM</b></td>"
        CreditMemoHTML &= "<td align='right'><b>Extended</b></td>"
        CreditMemoHTML &= "</tr>"

        commandString = "select * from v_shipment_lines where shipmentID=@shipmentID order by lineID"
        comm = New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        reader = comm.ExecuteReader
        Dim subtotal As Double = 0
        While reader.Read
            'Lines Section
            CreditMemoHTML &= "<tr>"
            CreditMemoHTML &= "<td>" & reader.Item("manufacturer").ToString & "</td>"
            CreditMemoHTML &= "<td>" & reader.Item("partnumber").ToString & "</td>"
            CreditMemoHTML &= "<td align='center'>" & reader.Item("ship_qty").ToString & "</td>"
            CreditMemoHTML &= "<td align='right'>" & FormatCurrency(reader.Item("price"), 2) & "</td>"
            CreditMemoHTML &= "<td align='center'>" & reader.Item("uom").ToString & "</td>"
            CreditMemoHTML &= "<td align='right'>" & FormatCurrency(reader.Item("price") * reader.Item("ship_qty")) & "</td>"
            CreditMemoHTML &= "</tr>"
            CreditMemoHTML &= "<tr>"
            CreditMemoHTML &= "<td colspan='6'>" & reader.Item("item").ToString & "</td>"
            CreditMemoHTML &= "</tr>"
            CreditMemoHTML &= "<tr><td colspan='6'><hr/></td></tr>"
            subtotal += reader.Item("price") * reader.Item("ship_qty")
        End While
        reader.Close()

        CreditMemoHTML &= "<tr><td colspan='6'><p> </p></td></tr>"

        'Footer Section
        Dim salestax As Double = GetSalesTax(companyID, vendorID) * subtotal
        Dim total As Double = subtotal + salestax + ship_charge
        CreditMemoHTML &= "<tr>"
        CreditMemoHTML &= "<td></td>"
        CreditMemoHTML &= "<td></td>"
        CreditMemoHTML &= "<td></td>"
        CreditMemoHTML &= "<td colspan='2'><b>Sub-Total</b></td>"
        CreditMemoHTML &= "<td align='right'>" & FormatCurrency(subtotal, 2) & "</td>"
        CreditMemoHTML &= "</tr>"

        CreditMemoHTML &= "<tr>"
        CreditMemoHTML &= "<td></td>"
        CreditMemoHTML &= "<td></td>"
        CreditMemoHTML &= "<td></td>"
        CreditMemoHTML &= "<td colspan='2'><b>Sales Tax</b></td>"
        CreditMemoHTML &= "<td align='right'>" & FormatCurrency(salestax, 2) & "</td>"
        CreditMemoHTML &= "</tr>"

        CreditMemoHTML &= "<tr>"
        CreditMemoHTML &= "<td></td>"
        CreditMemoHTML &= "<td></td>"
        CreditMemoHTML &= "<td></td>"
        CreditMemoHTML &= "<td colspan='2'><b>Shipping</b></td>"
        CreditMemoHTML &= "<td align='right'>" & FormatCurrency(ship_charge, 2) & "</td>"
        CreditMemoHTML &= "</tr>"

        CreditMemoHTML &= "<tr>"
        CreditMemoHTML &= "<td></td>"
        CreditMemoHTML &= "<td></td>"
        CreditMemoHTML &= "<td></td>"
        CreditMemoHTML &= "<td colspan='2'><b>Total</b></td>"
        CreditMemoHTML &= "<td align='right'>" & FormatCurrency(total, 2) & "</td>"
        CreditMemoHTML &= "</tr>"

        CreditMemoHTML &= "</table>"

        CreditMemoHTML &= "</td>"
        CreditMemoHTML &= "</tr>"

        CreditMemoHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

        'Notes Section
        CreditMemoHTML &= "<tr>"
        CreditMemoHTML &= "<td colspan='4'><b>Notes</b></td>"
        CreditMemoHTML &= "</tr>"

        CreditMemoHTML &= "<tr>"
        CreditMemoHTML &= "<td colspan='4'>" & notes & "</td>"
        CreditMemoHTML &= "</tr>"

        CreditMemoHTML &= "</table></body></html>"
    End Function

    Public Shared Function InvoiceHTML(ByVal shipmentID As Integer, ByVal message As String) As String
        InvoiceHTML = ""
        InvoiceHTML &= "<html><body><table width='100%'>"

        Dim companyID, vendorID As Integer
        Dim notes As String = ""
        Dim subtotal As Double = 0
        Dim tax_subtotal As Double = 0
        Dim invoiceID As String = ""
        Dim orderID As String = ""
        Dim purchaseorder As String = ""
        Dim ship_charge As Double
        Dim chargetax As Boolean = False

        'Vendor Section
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_shipments where shipmentID=@shipmentID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            companyID = reader.Item("companyID")
            vendorID = reader.Item("vendorID")
            notes = reader.Item("notes").ToString
            ship_charge = reader.Item("ship_charge").ToString
            invoiceID = reader.Item("invoiceID").ToString
            orderID = reader.Item("orderID").ToString
            purchaseorder = reader.Item("purchaseorder")
            chargetax = reader.Item("chargetax")

            Dim remitto As String = "DFO Filters & Equipment<br/>"
            remitto &= reader.Item("r_address1").ToString & "<br/>"
            If reader.Item("r_address2").ToString <> "" Then
                remitto &= reader.Item("r_address2").ToString & "<br/>"
            End If
            remitto &= reader.Item("r_city").ToString & ", " & reader.Item("r_state").ToString & " " & reader.Item("r_zipcode").ToString

            Dim billto As String = reader.Item("b_address1").ToString & "<br/>"
            If reader.Item("b_address2").ToString <> "" Then
                billto &= reader.Item("b_address2").ToString & "<br/>"
            End If
            billto &= reader.Item("b_city").ToString & ", " & reader.Item("b_state").ToString & " " & reader.Item("b_zipcode").ToString

            Dim shipto As String = reader.Item("s_address1").ToString & "<br/>"
            If reader.Item("s_address2").ToString <> "" Then
                shipto &= reader.Item("s_address2").ToString & "<br/>"
            End If
            If reader.Item("s_address3").ToString <> "" Then
                shipto &= reader.Item("s_address3").ToString & "<br/>"
            End If
            shipto &= reader.Item("s_city").ToString & ", " & reader.Item("s_state").ToString & " " & reader.Item("s_zipcode").ToString

            InvoiceHTML &= "<tr><td colspan='4'>"
            InvoiceHTML &= "<a href='http://www.desertfleetoutfitters.com'><img alt='Desert Fleet Outfitters' src='http://www.desertfleetoutfitters.com/Images/dfo_banner3.jpg' /></a>"
            InvoiceHTML &= "</td></tr>"

            InvoiceHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            If message <> "" Then
                InvoiceHTML &= "<tr><td colspan='4'><p>" & message & "</p></td></tr>"
                InvoiceHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
            End If

            InvoiceHTML &= "<tr>"
            InvoiceHTML &= "<td width='15%'><b>Invoice ID</b></td>"
            InvoiceHTML &= "<td width='35%'>" & invoiceID & "</td>"
            InvoiceHTML &= "<td width='15%' valign='top' rowspan='3'><b>Remit To</b></td>"
            InvoiceHTML &= "<td width='35%' rowspan='3'>" & remitto & "</td>"
            InvoiceHTML &= "</tr>"

            InvoiceHTML &= "<tr>"
            InvoiceHTML &= "<td><b>Purchase Order</b></td>"
            InvoiceHTML &= "<td valign='top'>" & purchaseorder & "</td>"
            InvoiceHTML &= "</tr>"

            InvoiceHTML &= "<tr>"
            InvoiceHTML &= "<td><b>Ordered By</b></td>"
            InvoiceHTML &= "<td valign='top'>" & reader.Item("c_contact").ToString & "</td>"
            InvoiceHTML &= "</tr>"

            InvoiceHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            'Bill To - Ship To Section

            InvoiceHTML &= "<tr>"
            InvoiceHTML &= "<td><b>Bill To</b></td>"
            InvoiceHTML &= "<td>" & reader.Item("company").ToString & "</td>"
            InvoiceHTML &= "<td><b>Ship To</b></td>"
            InvoiceHTML &= "<td>" & reader.Item("shipto").ToString & "</td>"
            InvoiceHTML &= "</tr>"

            InvoiceHTML &= "<tr>"
            InvoiceHTML &= "<td></td>"
            InvoiceHTML &= "<td valign='top'>" & billto & "</td>"
            InvoiceHTML &= "<td></td>"
            InvoiceHTML &= "<td valign='top'>" & shipto & "</td>"
            InvoiceHTML &= "</tr>"

            InvoiceHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            InvoiceHTML &= "<tr>"
            InvoiceHTML &= "<td>Carrier</td>"
            InvoiceHTML &= "<td valign='top'>" & reader.Item("carrier") & "</td>"
            InvoiceHTML &= "<td>Tracking</td>"
            InvoiceHTML &= "<td valign='top'>" & reader.Item("tracking") & "</td>"
            InvoiceHTML &= "</tr>"

            InvoiceHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
            InvoiceHTML &= "<tr><td colspan='4'<hr/></td></tr>"
            InvoiceHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
        End If
        reader.Close()

        'Lines Header Section
        InvoiceHTML &= "<tr>"
        InvoiceHTML &= "<td colspan='4'>"

        InvoiceHTML &= "<table style='width: 100%'>"

        InvoiceHTML &= "<tr>"
        InvoiceHTML &= "<td><b>Manufacturer</b></td>"
        InvoiceHTML &= "<td><b>Part Number</b></td>"
        InvoiceHTML &= "<td align='center'><b>Shipped</b></td>"
        InvoiceHTML &= "<td align='right'><b>Price</b></td>"
        InvoiceHTML &= "<td align='center'><b>UOM</b></td>"
        InvoiceHTML &= "<td align='right'><b>Extended</b></td>"
        InvoiceHTML &= "</tr>"

        commandString = "select * from v_shipment_lines where shipmentID=@shipmentID order by lineID"
        comm = New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@shipmentID", shipmentID)
        reader = comm.ExecuteReader
        While reader.Read
            'Lines Section
            InvoiceHTML &= "<tr>"
            InvoiceHTML &= "<td>" & reader.Item("manufacturer").ToString & "</td>"
            InvoiceHTML &= "<td>" & reader.Item("partnumber").ToString & "</td>"
            InvoiceHTML &= "<td align='center'>" & reader.Item("ship_qty").ToString & "</td>"
            InvoiceHTML &= "<td align='right'>" & FormatCurrency(reader.Item("price"), 2) & "</td>"
            InvoiceHTML &= "<td align='center'>" & reader.Item("uom").ToString & "</td>"
            InvoiceHTML &= "<td align='right'>" & FormatCurrency(reader.Item("price") * reader.Item("ship_qty")) & "</td>"
            InvoiceHTML &= "</tr>"
            InvoiceHTML &= "<tr>"
            InvoiceHTML &= "<td colspan='6'>" & reader.Item("item").ToString & "</td>"
            InvoiceHTML &= "</tr>"
            InvoiceHTML &= "<tr><td colspan='6'><hr/></td></tr>"
            subtotal += reader.Item("price") * reader.Item("ship_qty")
            If IsTaxable(reader.Item("manufacturer"), reader.Item("partnumber")) = True Then
                tax_subtotal = tax_subtotal + (reader.Item("price") * reader.Item("quantity"))
            End If
        End While
        reader.Close()

        InvoiceHTML &= "<tr><td colspan='6'><p> </p></td></tr>"

        'Footer Section
        Dim salestax As Double = 0
        If chargetax = True Then
            salestax = GetSalesTax(companyID, vendorID) * tax_subtotal
        Else
            salestax = 0
        End If
        Dim total As Double = subtotal + salestax + ship_charge
        InvoiceHTML &= "<tr>"
        InvoiceHTML &= "<td></td>"
        InvoiceHTML &= "<td></td>"
        InvoiceHTML &= "<td></td>"
        InvoiceHTML &= "<td colspan='2'><b>Sub-Total</b></td>"
        InvoiceHTML &= "<td align='right'>" & FormatCurrency(subtotal, 2) & "</td>"
        InvoiceHTML &= "</tr>"

        InvoiceHTML &= "<tr>"
        InvoiceHTML &= "<td></td>"
        InvoiceHTML &= "<td></td>"
        InvoiceHTML &= "<td></td>"
        InvoiceHTML &= "<td colspan='2'><b>Sales Tax</b></td>"
        InvoiceHTML &= "<td align='right'>" & FormatCurrency(salestax, 2) & "</td>"
        InvoiceHTML &= "</tr>"

        InvoiceHTML &= "<tr>"
        InvoiceHTML &= "<td></td>"
        InvoiceHTML &= "<td></td>"
        InvoiceHTML &= "<td></td>"
        InvoiceHTML &= "<td colspan='2'><b>Shipping</b></td>"
        InvoiceHTML &= "<td align='right'>" & FormatCurrency(ship_charge, 2) & "</td>"
        InvoiceHTML &= "</tr>"

        InvoiceHTML &= "<tr>"
        InvoiceHTML &= "<td></td>"
        InvoiceHTML &= "<td></td>"
        InvoiceHTML &= "<td></td>"
        InvoiceHTML &= "<td colspan='2'><b>Total</b></td>"
        InvoiceHTML &= "<td align='right'>" & FormatCurrency(total, 2) & "</td>"
        InvoiceHTML &= "</tr>"

        InvoiceHTML &= "</table>"

        InvoiceHTML &= "</td>"
        InvoiceHTML &= "</tr>"

        InvoiceHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

        'Notes Section
        InvoiceHTML &= "<tr>"
        InvoiceHTML &= "<td colspan='4'><b>Notes</b></td>"
        InvoiceHTML &= "</tr>"

        InvoiceHTML &= "<tr>"
        InvoiceHTML &= "<td colspan='4'>" & notes & "</td>"
        InvoiceHTML &= "</tr>"

        InvoiceHTML &= "</table></body></html>"
    End Function

    Public Shared Function POHTML(ByVal poID As Integer, message As String, ByVal imgfile As String, ByVal site_address As String) As String
        POHTML = ""
        POHTML &= "<html><body><table width='100%'>"
        Dim vendorID As Integer
        Dim notes As String = ""
        Dim subtotal As Double = 0

        'Vendor Section
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from v_po where poID=@poID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@poID", poID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            vendorID = reader.Item("vendorID")
            Dim supplier As String = reader.Item("supplier")
            Dim sup_contact As String = reader.Item("sup_contact")
            Dim sup_phone As String = reader.Item("sup_phone")

            Dim billto As String = reader.Item("address1").ToString & "<br/>"
            If reader.Item("address2").ToString <> "" Then
                billto &= reader.Item("address2").ToString & "<br/>"
            End If
            billto &= reader.Item("city").ToString & ", " & reader.Item("state").ToString & " " & reader.Item("zipcode").ToString

            Dim shipto As String = reader.Item("ship_address1").ToString & "<br/>"
            If reader.Item("ship_address2").ToString <> "" Then
                shipto &= reader.Item("ship_address2").ToString & "<br/>"
            End If
            shipto &= reader.Item("ship_city").ToString & ", " & reader.Item("ship_state").ToString & " " & reader.Item("ship_zipcode").ToString

            notes = reader.Item("notes").ToString

            POHTML &= "<tr><td colspan='4'>"
            POHTML &= "<a href=" & site_address & "><img alt='DFO Filters & Equipment' src=" & imgfile & "' /></a>"
            POHTML &= "</td></tr>"

            POHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            If message <> "" Then
                POHTML &= "<tr><td colspan='4'><p>" & message & "</p></td></tr>"
                POHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
            End If
            POHTML &= "<tr><td colspan='4'><p>Thank you for your service. For questions or to update pricing, please reply to this email. For payment information, please email us at payables@dfofilters.com</p><p>For immediate assistance at contact us at 480-295-1676.</p></td></tr>"
            POHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            POHTML &= "<tr>"
            POHTML &= "<td width='15%'><b>Supplier</b></td>"
            POHTML &= "<td width='35%'>" & supplier & "</td>"
            POHTML &= "<td width='15%'><b>Contact</b></td>"
            POHTML &= "<td width='35%'>" & sup_contact & "</td>"
            POHTML &= "</tr>"

            POHTML &= "<tr>"
            POHTML &= "<td width='15%'></td>"
            POHTML &= "<td width='35%'></td>"
            POHTML &= "<td width='15%'><b>Phone</b></td>"
            POHTML &= "<td width='35%'>" & sup_phone & "</td>"
            POHTML &= "</tr>"

            POHTML &= "<tr>"
            POHTML &= "<td width='15%'><b>PO ID</b></td>"
            POHTML &= "<td width='35%'>" & poID & "</td>"
            POHTML &= "<td width='15%'></td>"
            POHTML &= "<td width='35%'></td>"
            POHTML &= "</tr>"

            POHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            'Bill To - Ship To Section

            POHTML &= "<tr>"
            POHTML &= "<td><b>Bill To</b></td>"
            POHTML &= "<td>" & reader.Item("vendor").ToString & "</td>"
            POHTML &= "<td><b>Ship To</b></td>"
            POHTML &= "<td>" & reader.Item("shipto").ToString & "</td>"
            POHTML &= "</tr>"

            POHTML &= "<tr>"
            POHTML &= "<td></td>"
            POHTML &= "<td>" & billto & "</td>"
            POHTML &= "<td></td>"
            POHTML &= "<td>" & shipto & "</td>"
            POHTML &= "</tr>"

            POHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            'Contact Section
            POHTML &= "<tr>"
            POHTML &= "<td><b>Contact</b></td>"
            POHTML &= "<td>" & reader.Item("ship_contact").ToString & "</td>"
            POHTML &= "<td></td>"
            POHTML &= "<td></td>"
            POHTML &= "</tr>"

            POHTML &= "<tr>"
            POHTML &= "<td><b>Phone</b></td>"
            POHTML &= "<td>" & reader.Item("ship_phone").ToString & "</td>"
            POHTML &= "<td></td>"
            POHTML &= "<td></td>"
            POHTML &= "</tr>"

            POHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
        End If
        reader.Close()

        'Lines Header Section
        POHTML &= "<tr>"
        POHTML &= "<td colspan='4'>"

        POHTML &= "<table style='width: 100%'>"

        POHTML &= "<tr>"
        POHTML &= "<td><b>Manufacturer</b></td>"
        POHTML &= "<td><b>Part Number<b></td>"
        POHTML &= "<td align='center'><b>Qty</b></td>"
        POHTML &= "<td align='right'><b>Cost</b></td>"
        POHTML &= "<td align='center'><b>UOM</b></td>"
        POHTML &= "<td align='right'><b>Extended</b></td>"
        POHTML &= "</tr>"

        commandString = "select * from t_poline where poID=@poID order by polineID"
        comm = New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@poID", poID)
        reader = comm.ExecuteReader
        While reader.Read
            'Lines Section
            POHTML &= "<tr>"
            POHTML &= "<td>" & reader.Item("manufacturer").ToString & "</td>"
            POHTML &= "<td>" & reader.Item("partnumber").ToString & "</td>"
            POHTML &= "<td align='center'>" & reader.Item("quantity").ToString & "</td>"
            POHTML &= "<td align='right'>" & FormatCurrency(reader.Item("cost"), 2) & "</td>"
            POHTML &= "<td align='center'>" & reader.Item("uom").ToString & "</td>"
            POHTML &= "<td align='right'>" & FormatCurrency(reader.Item("cost") * reader.Item("quantity"), 2) & "</td>"
            POHTML &= "</tr>"
            'POHTML &= "<tr><td align='left'>Description: " & reader.Item("item") & "</td></tr>"
            subtotal += reader.Item("cost") * reader.Item("quantity")
        End While
        reader.Close()

        POHTML &= "<tr><td colspan='6'></td></tr>"

        'Footer Section
        POHTML &= "<tr>"
        POHTML &= "<td></td>"
        POHTML &= "<td></td>"
        POHTML &= "<td></td>"
        POHTML &= "<td colspan='2'><b>Sub-Total</b></td>"
        POHTML &= "<td align='right'>" & FormatCurrency(subtotal, 2) & "</td>"
        POHTML &= "</tr>"

        POHTML &= "</table>"

        POHTML &= "</td>"
        POHTML &= "</tr>"

        POHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

        'Notes Section
        POHTML &= "<tr>"
        POHTML &= "<td colspan='4'><b>Notes</b></td>"
        POHTML &= "</tr>"

        POHTML &= "<tr>"
        POHTML &= "<td colspan='4'>" & notes & "</td>"
        POHTML &= "</tr>"

        POHTML &= "</table></body></html>"
        conn.Close()
    End Function

    Public Shared Function QuoteHTML(ByVal quoteID As Integer, ByVal message As String) As String
        Dim companyID, vendorID As Integer
        Dim notes As String = ""
        QuoteHTML = ""
        QuoteHTML &= "<table width='100%'>"
        'Vendor Section
        Dim conn As New SqlConnection(ConnectionString)
        Dim commandString As String
        conn.Open()
        commandString = "select * from t_quote where quoteID=@quoteID"
        Dim comm As New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@quoteID", quoteID)
        Dim reader As SqlDataReader = comm.ExecuteReader
        If reader.Read Then
            companyID = reader.Item("companyID")
            vendorID = reader.Item("vendorID")
            notes = reader.Item("notes").ToString

            Dim remitto As String = reader.Item("vendor").ToString & "<br/>"
            remitto &= reader.Item("r_address1").ToString & "<br/>"
            If reader.Item("r_address2").ToString <> "" Then
                remitto &= reader.Item("r_address2").ToString & "<br/>"
            End If
            remitto &= reader.Item("r_city").ToString & ", " & reader.Item("r_state").ToString & " " & reader.Item("r_zipcode").ToString

            Dim billto As String = reader.Item("b_address1").ToString & "<br/>"
            If reader.Item("b_address2").ToString <> "" Then
                billto &= reader.Item("b_address2").ToString & "<br/>"
            End If
            billto &= reader.Item("b_city").ToString & ", " & reader.Item("b_state").ToString & " " & reader.Item("b_zipcode").ToString

            Dim shipto As String = reader.Item("s_address1").ToString & "<br/>"
            If reader.Item("s_address2").ToString <> "" Then
                shipto &= reader.Item("s_address2").ToString & "<br/>"
            End If
            If reader.Item("s_address3").ToString <> "" Then
                shipto &= reader.Item("s_address3").ToString & "<br/>"
            End If
            shipto &= reader.Item("s_city").ToString & ", " & reader.Item("s_state").ToString & " " & reader.Item("s_zipcode").ToString

            QuoteHTML &= "<tr><td colspan='4'>"
            QuoteHTML &= "<a href='http://www.desertfleetoutfitters.com'><img alt='Desert Fleet Outfitters' src='http://www.desertfleetoutfitters.com/Images/desertlogo.jpg' /></a>"
            QuoteHTML &= "</td></tr>"

            QuoteHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            If message <> "" Then
                QuoteHTML &= "<tr><td colspan='4'><p>" & message & "</p></td></tr>"
                QuoteHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
            End If

            QuoteHTML &= "<tr>"
            QuoteHTML &= "<td width='15%' valign='top'><b>Quote ID</b></td>"
            QuoteHTML &= "<td width='35%' valign='top'>" & quoteID & "</td>"
            QuoteHTML &= "<td width='15%' valign='top'><b>Remit To</b></td>"
            QuoteHTML &= "<td width='35%'>" & remitto & "</td>"
            QuoteHTML &= "</tr>"

            QuoteHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            'Bill To - Ship To Section

            QuoteHTML &= "<tr>"
            QuoteHTML &= "<td><b>Bill To</b></td>"
            QuoteHTML &= "<td>" & reader.Item("company").ToString & "</td>"
            QuoteHTML &= "<td><b>Ship To</b></td>"
            QuoteHTML &= "<td>" & reader.Item("shipto").ToString & "</td>"
            QuoteHTML &= "</tr>"

            QuoteHTML &= "<tr>"
            QuoteHTML &= "<td></td>"
            QuoteHTML &= "<td valign='top'>" & billto & "</td>"
            QuoteHTML &= "<td></td>"
            QuoteHTML &= "<td valign='top'>" & shipto & "</td>"
            QuoteHTML &= "</tr>"

            QuoteHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

            'Contact Section
            QuoteHTML &= "<tr>"
            QuoteHTML &= "<td><b>Contact</b></td>"
            QuoteHTML &= "<td>" & reader.Item("c_contact").ToString & "</td>"
            QuoteHTML &= "<td><b>Phone</b></td>"
            QuoteHTML &= "<td>" & reader.Item("c_phone").ToString & "</td>"
            QuoteHTML &= "</tr>"

            QuoteHTML &= "<tr>"
            QuoteHTML &= "<td></td>"
            QuoteHTML &= "<td></td>"
            QuoteHTML &= "<td><b>Fax</b></td>"
            QuoteHTML &= "<td>" & reader.Item("c_fax").ToString & "</td>"
            QuoteHTML &= "</tr>"

            QuoteHTML &= "<tr><td colspan='4'><p> </p></td></tr>"
        End If
        reader.Close()

        'Lines Header Section
        QuoteHTML &= "<tr>"
        QuoteHTML &= "<td colspan='4'>"

        QuoteHTML &= "<table width='100%'>"
        QuoteHTML &= "<tr>"
        QuoteHTML &= "<td><b>Manufacturer</b></td>"
        QuoteHTML &= "<td><b>Part Number</b></td>"
        QuoteHTML &= "<td align='center'><b>Qty</b></td>"
        QuoteHTML &= "<td align='right'><b>Price</b></td>"
        QuoteHTML &= "<td align='center'><b>UOM</b></td>"
        QuoteHTML &= "<td><b>Availability</b></td>"
        QuoteHTML &= "<td align='right'><b>Extended</b></td>"
        QuoteHTML &= "</tr>"

        commandString = "select * from t_quote_line where quoteID=@quoteID order by lineID"
        comm = New SqlCommand(commandString, conn)
        comm.Parameters.AddWithValue("@quoteID", quoteID)
        reader = comm.ExecuteReader
        Dim subtotal As Double = 0
        While reader.Read
            'Lines Section
            QuoteHTML &= "<tr>"
            QuoteHTML &= "<td>" & reader.Item("manufacturer").ToString & "</td>"
            QuoteHTML &= "<td>" & reader.Item("partnumber").ToString & "</td>"
            QuoteHTML &= "<td align='center'>" & reader.Item("quantity").ToString & "</td>"
            QuoteHTML &= "<td align='right'>" & FormatCurrency(reader.Item("price"), 2) & "</td>"
            QuoteHTML &= "<td align='center'>" & reader.Item("uom").ToString & "</td>"
            QuoteHTML &= "<td>" & reader.Item("availability").ToString & "</td>"
            QuoteHTML &= "<td align='right'>" & FormatCurrency(reader.Item("price") * reader.Item("quantity"), 2) & "</td>"
            QuoteHTML &= "</tr>"

            QuoteHTML &= "<tr>"
            QuoteHTML &= "<td colspan='7'>" & reader.Item("item").ToString & "</td>"
            QuoteHTML &= "</tr>"
            QuoteHTML &= "<tr><td colspan='7'><hr/></td></tr>"

            subtotal += reader.Item("price") * reader.Item("quantity")
        End While
        reader.Close()

        QuoteHTML &= "<tr><td colspan='7'><p> </p></td></tr>"

        'Footer Section
        Dim salestax As Double = GetSalesTax(companyID, vendorID) * subtotal
        Dim total As Double = subtotal + salestax
        QuoteHTML &= "<tr>"
        QuoteHTML &= "<td></td>"
        QuoteHTML &= "<td></td>"
        QuoteHTML &= "<td></td>"
        QuoteHTML &= "<td></td>"
        QuoteHTML &= "<td colspan='2'><b>Sub-Total</b></td>"
        QuoteHTML &= "<td align='right'>" & FormatCurrency(subtotal, 2) & "</td>"
        QuoteHTML &= "</tr>"

        QuoteHTML &= "<tr>"
        QuoteHTML &= "<td></td>"
        QuoteHTML &= "<td></td>"
        QuoteHTML &= "<td></td>"
        QuoteHTML &= "<td></td>"
        QuoteHTML &= "<td colspan='2'><b>Sales Tax</b></td>"
        QuoteHTML &= "<td align='right'>" & FormatCurrency(salestax, 2) & "</td>"
        QuoteHTML &= "</tr>"

        QuoteHTML &= "<tr>"
        QuoteHTML &= "<td></td>"
        QuoteHTML &= "<td></td>"
        QuoteHTML &= "<td></td>"
        QuoteHTML &= "<td></td>"
        QuoteHTML &= "<td colspan='2'><b>Total</b></td>"
        QuoteHTML &= "<td align='right'>" & FormatCurrency(total, 2) & "</td>"
        QuoteHTML &= "</tr>"

        QuoteHTML &= "</table>"

        QuoteHTML &= "</td>"
        QuoteHTML &= "</tr>"

        QuoteHTML &= "<tr><td colspan='4'><p> </p></td></tr>"

        'Notes Section
        QuoteHTML &= "<tr>"
        QuoteHTML &= "<td colspan='4'><b>Notes</b></td>"
        QuoteHTML &= "</tr>"

        QuoteHTML &= "<tr>"
        QuoteHTML &= "<td colspan='4'>" & notes & "</td>"
        QuoteHTML &= "</tr>"

        QuoteHTML &= "</table>"
    End Function

End Class
