DELIMITER // 
Drop procedure InitializeDiscount;

Create PROCEDURE `InitializeDiscount`(In dataCount int)
BEGIN
	declare i int;
    Set i = dataCount;    
 	While i > 0 do
 		Insert into Discounts values (uuid(), now(), 0);
 		Set i = i - 1;
 	End While;
END