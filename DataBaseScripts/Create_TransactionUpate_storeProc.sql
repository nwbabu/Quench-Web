CREATE PROC UpdateTrancationStatus(
 @TransactionId varchar(200)
)
AS
Begin
 BEGIN TRANSACTION;
 SAVE TRANSACTION MySavePoint;
 DECLARE  @orderId int
 DECLARE  @cust_code int
  BEGIN TRY;

    UPDATE App_Manage_Payment_Transanction SET Delivert_Transanction_Status='Success', Payment_Date=GETDATE()
	      WHERE Transaction_id=@TransactionId

    SELECT @orderId = Order_Id, @cust_code= Cust_Id FROM App_Manage_Payment_Transanction 
	      WHERE Transaction_id=@TransactionId
    UPDATE App_Manage_Order SET Payment_Transaction_Status='Success'
    WHERE order_id=@orderId and cust_id=@cust_code

  COMMIT TRANSACTION 

  END TRY
  BEGIN CATCH
  END CATCH
 
End 

