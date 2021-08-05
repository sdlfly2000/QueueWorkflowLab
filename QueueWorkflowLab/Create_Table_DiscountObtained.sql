-- For MySql
Create Table DiscountObtained(
	discountObtainedId varchar(255),
	discountId varchar(255),
	workflowName varchar(255),
	Primary Key (discountObtainedId),
	Foreign Key (discountId) References Discounts(discountId)
)