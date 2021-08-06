-- For MySql
Create Table Discounts
(
	discountId varchar(255) Not NUll,
	isOccupied bit Default 0,
	rowVersion Datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    Primary Key (discountId)
);