--Roles
--get_all_roles
create proc sp_get_all_role
as
begin
	select * from Roles
end
go


--create role
create proc sp_create_role
(
	@role_id int,
	@role_name varchar(20)
)
as
begin
	if not exists (select 1 from Roles where role_id = @role_id)
	begin
		insert into Roles(role_name)
		values(@role_name)
	end
end
go

--update roles
create proc sp_update_role
(
    @role_id int,
    @role_name varchar(20)
)
as
begin
    if @role_name is not null and @role_name <> 'string' and @role_name <> 'null'
    begin
        update Roles
        set role_name = @role_name
        where role_id = @role_id
    end
end
go

--delete role 
create proc sp_delete_role
(
	@role_id int
)
as
begin
	delete Roles
	where role_id = @role_id
end
go



--Brands
--get brand by id
create proc sp_get_brand_by_id(@brand_id int)
as
begin
    select * from Brands
    where brand_id = @brand_id
end
go

--get all brands
create proc sp_get_all_brand
as
begin
    select * from Brands
end
go

--create or update brands
create proc sp_create_or_update_brand
(
	@brand_id int,
    @brand_name nvarchar(100)
)
as
begin
	if not exists (select 1 from Brands where brand_id = @brand_id)
	begin
		insert into Brands(brand_name)
		values(@brand_name)
	end
	else
	begin
		if @brand_name is not null and @brand_name <> 'string'
		begin
			update Brands
			set brand_name = @brand_name
			where brand_id = @brand_id
		end
	end
end
go


--delete brands
create proc sp_delete_brand
(
    @brand_id int
)
as
begin
    delete from Brands
    where brand_id = @brand_id
end
go

--categories
--get category by id
create proc sp_get_category_by_id
(
    @category_id int
)
as
begin
    select * from Categories
    where category_id = @category_id
end
go

--get all category
create proc sp_get_all_category
as
begin
    select * from Categories
end
go

--create category
create proc sp_create_category
(
    @category_name nvarchar(350)
)
as
begin
	insert into Categories(category_name)
	values(@category_name)
end
go

--update category
create proc sp_update_category
(
    @category_id int,
    @category_name nvarchar(350)
)
as
begin
    if @category_name is not null and @category_name <> 'string'
    begin
        update Categories
        set category_name = @category_name
        where category_id = @category_id
    end
end
go

--delete category
create proc sp_delete_category
(
    @category_id int
)
as
begin
    delete Categories
    where category_id = @category_id
end
go

--Accounts
--get account by id
create proc sp_get_account_by_id
(
	@account_id int
)
as
begin
	select * from Account
	where account_id = @account_id
end
go

--get account by username
create proc sp_get_account_by_username
(
	@username nvarchar(500)
)
as
begin
	select top 1 * from Account
	where username = @username
end
go

--login account
create proc sp_login(@username nvarchar(500), @password varchar(256))
as
begin
	select * from Account
	where username = @username and password = @password
end
go

--create account
create proc sp_create_account
(
	@username nvarchar(500),
	@password varchar(256),
	@address nvarchar(500),
	@full_name nvarchar(500),
	@phone_number varchar(20),
	@email varchar(150),
	@gender nvarchar(50),
	@role_id int
)
as
begin
	insert into Account(username, password, address, full_name, phone_number, email, gender, role_id)
	values(@username, @password, @address, @full_name, @phone_number, @email, @gender, @role_id)
end
go

--update customer
create proc sp_update_account
(
	@account_id int,
	@username nvarchar(500),
	@password varchar(256),
	@address nvarchar(500),
	@full_name nvarchar(500),
	@phone_number varchar(20),
	@email varchar(150),
	@gender nvarchar(50),
	@role_id int
)
as
begin
	update Account
	set
		username = CASE WHEN @username IS NOT NULL AND @username <> 'null' AND @username <> 'string' THEN @username ELSE username END,
		password = CASE WHEN @password IS NOT NULL AND @password <> 'null' AND @password <> 'string' THEN @password ELSE password END,
		address = CASE WHEN @address IS NOT NULL AND @address <> 'null' AND @address <> 'string' THEN @address ELSE address END,
		full_name = CASE WHEN @full_name IS NOT NULL AND @full_name <> 'null' AND @full_name <> 'string' THEN @full_name ELSE full_name END,
		phone_number = CASE WHEN @phone_number IS NOT NULL AND @phone_number <> 'null' AND @phone_number <> 'string' AND @phone_number NOT LIKE '%[^0-9]%' THEN @phone_number ELSE phone_number END,
		email = CASE WHEN @email IS NOT NULL AND @email <> 'null' AND @email <> 'string' THEN @email ELSE email END,
		gender = CASE WHEN @gender IS NOT NULL AND @gender <> 'null' AND @gender <> 'string' THEN @gender else gender end,
		role_id = CASE WHEN @role_id IS NOT NULL AND @role_id <> 'null' AND @role_id <> 'string' THEN @role_id else role_id end
	where account_id = @account_id
end
go

--delete customer
create proc sp_delete_account
(
	@account_id int
)
as
begin
	delete Account
	where account_id = @account_id
end
go


--Products
--get product by id
create proc sp_get_product_by_id
(
	@product_id int
)
as
begin
	select * from Products
	where product_id = @product_id
end
go


--get all product
create proc sp_get_all_product
as
begin
	select * from Products
end
go

--get product by brand
create proc sp_get_product_by_brand
(
	@brand_name nvarchar(350)
)
as
begin
	select * from Products p inner join Brands b
	on p.brand_id = b.brand_id
	where brand_name = @brand_name
end
go


--create product
--get product by id
create proc sp_create_product
(
	@product_name nvarchar(500),
	@description nvarchar(500),
	@price int,
	@discount int,
	@image_link varchar(500),
	@product_quantity int,
	@updated_date datetime,
	@category_id int,
	@brand_id int
)
as
begin
	insert into Products(product_name, description, price, discount, image_link, product_quantity, updated_date, category_id, brand_id)
	values(@product_name, @description, @price, @discount, @image_link, @product_quantity, @updated_date, @category_id, @brand_id)
end
go

--update products
create proc sp_update_product
(
	@product_id int,
	@product_name nvarchar(500),
	@description nvarchar(500),
	@price int,
	@discount int,
	@image_link varchar(500),
	@product_quantity int,
	@updated_date datetime,
	@category_id int,
	@brand_id int
)
as
begin
	update Products
	set
		product_name = CASE WHEN @product_name IS NOT NULL AND @product_name <> 'null' AND @product_name <> 'string' THEN @product_name ELSE product_name END,
		description = CASE WHEN @description IS NOT NULL AND @description <> 'null' AND @description <> 'string' THEN @description ELSE description END,
		price = CASE WHEN @price IS NOT NULL AND @price <> 'null' AND @price <> 'string' THEN @price ELSE price END,
		discount = CASE WHEN @discount IS NOT NULL AND @discount <> 'null' AND @discount <> 'string' THEN @discount ELSE discount END,
		image_link = CASE WHEN @image_link IS NOT NULL AND @image_link <> 'null' AND @image_link <> 'string' THEN @image_link ELSE image_link END,
		product_quantity = CASE WHEN @product_quantity IS NOT NULL AND @product_quantity <> 'null' AND @product_quantity <> 'string' THEN @product_quantity ELSE product_quantity END,
		updated_date = GETDATE(),
		category_id = CASE WHEN @category_id IS NOT NULL AND @category_id <> 'null' AND @category_id <> 'string' THEN @category_id ELSE category_id END,
		brand_id = CASE WHEN @brand_id IS NOT NULL AND @brand_id <> 'null' AND @brand_id <> 'string' THEN @brand_id ELSE brand_id END
	where product_id = @product_id
end
go

--delete product
create proc sp_delete_product
(
	@product_id int
)
as
begin
	delete Products
	where product_id = @product_id
end
go

--search product by name products or price range
CREATE PROCEDURE [dbo].[sp_product_search] (@page_index  INT, 
                                       @page_size   INT,
									   @product_name nvarchar(500),
									   @fr_price int,
									   @to_price int)
AS
    BEGIN
        DECLARE @RecordCount BIGINT;
        IF(@page_size <> 0)
            BEGIN
                SET NOCOUNT ON;
                        SELECT(ROW_NUMBER() OVER(
                              ORDER BY product_id ASC)) AS RowNumber, 
                              p.product_id,
							  p.product_name,
							  p.price,
							  p.discount,
							  p.product_quantity,
							  p.created_date
                        INTO #Results1
                        FROM [Products] AS p
					    WHERE (@product_name = '' or p.product_name like N'%' + @product_name +'%') and
						((@fr_price IS NULL
                        AND @to_price IS NULL)
                        OR (@fr_price IS NOT NULL
                            AND @to_price IS NULL
                            AND p.price >= @fr_price)
                        OR (@fr_price IS NULL
                            AND @to_price IS NOT NULL
                            AND p.price <= @to_price)
                        OR (p.price BETWEEN @fr_price AND @to_price))
                        SELECT @RecordCount = COUNT(*)
                        FROM #Results1;
                        SELECT *, 
                               @RecordCount AS RecordCount
                        FROM #Results1
                        WHERE ROWNUMBER BETWEEN(@page_index - 1) * @page_size + 1 AND(((@page_index - 1) * @page_size + 1) + @page_size) - 1
                              OR @page_index = -1;
                        DROP TABLE #Results1; 
            END;
            ELSE
            BEGIN
                SET NOCOUNT ON;
                        SELECT(ROW_NUMBER() OVER(
                              ORDER BY product_id ASC)) AS RowNumber, 
                              p.product_id,
							  p.product_name,
							  p.price,
							  p.discount,
							  p.product_quantity,
							  p.created_date
                        INTO #Results2
                        FROM [Products] AS p
					    WHERE (@product_name = '' or p.product_name like N'%' + @product_name +'%') and
						((@fr_price IS NULL
                        AND @to_price IS NULL)
                        OR (@fr_price IS NOT NULL
                            AND @to_price IS NULL
                            AND p.price >= @fr_price)
                        OR (@fr_price IS NULL
                            AND @to_price IS NOT NULL
                            AND p.price <= @to_price)
                        OR (p.price BETWEEN @fr_price AND @to_price))
                        SELECT @RecordCount = COUNT(*)
                        FROM #Results2;
                        SELECT *, 
                               @RecordCount AS RecordCount
                        FROM #Results2
						DROP TABLE #Results2;
        END;
    END;
go


--Import
--create import
create proc [dbo].[sp_create_import]
(@category_id       int, 
 @total_money       int, 
 @list_json_import_details NVARCHAR(MAX)
)
AS
    BEGIN
		DECLARE @import_id INT;
        INSERT INTO Import
                (category_id, 
                 total_money               
                )
                VALUES
                (@category_id, 
                 @total_money
				 );
				SET @import_id = (select scope_identity())
				declare @total_import int;
				set @total_import = 0;
                IF(@list_json_import_details IS NOT NULL)
                    BEGIN
                        INSERT INTO Import_details
                        (import_id, 
                         brand_id, 
                         product_id, 
                         quantity,
						 import_price,
						 total_money
                        )
                               SELECT @import_id, 
                                      JSON_VALUE(l.value, '$.brand_id'), 
                                      JSON_VALUE(l.value, '$.product_id'), 
                                      JSON_VALUE(l.value, '$.quantity'),
                                      JSON_VALUE(l.value, '$.import_price'),    
                                      cast(JSON_VALUE(l.value, '$.import_price') as int) * cast(JSON_VALUE(l.value, '$.quantity') as int)
                               FROM OPENJSON(@list_json_import_details) AS l;
						select @total_import = SUM(total_money)
						from Import_details
						where import_id = @import_id

						update Import
						set total_money = @total_import
						where import_id = @import_id
                END;
        SELECT '';
    END;
go


--Shipping Details
--get shipping detail by id
create proc sp_get_shipping_detail_by_id
(
	@shippingDetail_id int
)
as
begin
	select * from Shipping_details
	where shippingDetail_id = @shippingDetail_id
end
go

--get shipping detail by consignee name
create proc sp_get_shipping_detail_by_name
(
	@consignee_name NVARCHAR(255)
)
as
begin
	select * from Shipping_details
	where consignee_name = @consignee_name
end
go

--create shipping detail
create proc sp_create_shipping_detail
(
	@consignee_name nvarchar(255),
	@delivery_address nvarchar(255),
	@phone_number varchar(20),
	@shipping_note nvarchar(350)
)
as
begin
	insert into Shipping_details(consignee_name, delivery_address, phone_number, shipping_note)
	values(@consignee_name, @delivery_address, @phone_number, @shipping_note)
end
go

--update shipping detail
create proc sp_update_shipping_detail
(
	@shippingDetail_id int,
	@consignee_name nvarchar(255),
	@delivery_address nvarchar(255),
	@phone_number varchar(20),
	@shipping_note nvarchar(350)
)
as
begin
	update Shipping_details
	set
		consignee_name = CASE WHEN @consignee_name IS NOT NULL AND @consignee_name <> 'null' AND @consignee_name <> 'string' THEN @consignee_name ELSE consignee_name END,
		delivery_address = CASE WHEN @delivery_address IS NOT NULL AND @delivery_address <> 'null' AND @delivery_address <> 'string' THEN @delivery_address ELSE delivery_address END,
		phone_number = CASE WHEN @phone_number IS NOT NULL AND @phone_number <> 'null' AND @phone_number <> 'string' AND @phone_number NOT LIKE '%[^0-9]%' THEN @phone_number ELSE phone_number END,
		shipping_note = CASE WHEN @shipping_note IS NOT NULL AND @shipping_note <> 'null' AND @shipping_note <> 'string' THEN @shipping_note ELSE shipping_note END
	where shippingDetail_id = @shippingDetail_id
end
go

--delete shipping details
create proc sp_delete_shipping_detail
(
	@shippingDetail_id int
)
as
begin
	delete Shipping_details
	where shippingDetail_id = @shippingDetail_id
end
go

--feedback
--get feedback by id
create proc sp_get_feedback_by_id
(
	@feedback_id int
)
as
begin
	select * from Feedbacks
	where feedback_id = @feedback_id
end
go

--create feedback
create proc sp_create_feedback
(
	@first_name nvarchar(50),
	@last_name nvarchar(50),
	@email varchar(150),
	@phone_number varchar(20),
	@subject_name nvarchar(100),
	@product_id int,
	@feedback_content nvarchar(500)
)
as
begin
	insert into Feedbacks(first_name, last_name, email, phone_number, subject_name, product_id, feedback_content)
	values(@first_name, @last_name, @email, @phone_number, @subject_name, @product_id, @feedback_content)
end
go

--update feedback
create proc sp_update_feedback
(
	@feedback_id int,
	@first_name nvarchar(50),
	@last_name nvarchar(50),
	@email varchar(150),
	@phone_number varchar(20),
	@subject_name nvarchar(100),
	@product_id int,
	@feedback_content nvarchar(500)
)
as
begin
	update Feedbacks
	set
		first_name = CASE WHEN @first_name IS NOT NULL AND @first_name <> 'null' AND @first_name <> 'string' THEN @first_name ELSE first_name END,
		last_name = CASE WHEN @last_name IS NOT NULL AND @last_name <> 'null' AND @last_name <> 'string' THEN @last_name ELSE last_name END,
		email = CASE WHEN @email IS NOT NULL AND @email <> 'null' AND @email <> 'string' THEN @email ELSE email END,
		phone_number = CASE WHEN @phone_number IS NOT NULL AND @phone_number <> 'null' AND @phone_number <> 'string' AND @phone_number NOT LIKE '%[^0-9]%' THEN @phone_number ELSE phone_number END,
		subject_name = CASE WHEN @subject_name IS NOT NULL AND @subject_name <> 'null' AND @subject_name <> 'string' THEN @subject_name ELSE subject_name END,
		product_id = CASE WHEN @product_id IS NOT NULL AND @product_id <> 'null' AND @product_id <> 'string' THEN @product_id ELSE product_id END,
		feedback_content = CASE WHEN @feedback_content IS NOT NULL AND @feedback_content <> 'null' AND @feedback_content <> 'string'  THEN @feedback_content ELSE feedback_content END
	where feedback_id = @feedback_id
end
go

--delete feedback
create proc sp_delete_feedback
(
	@feedback_id int
)
as
begin
	delete Feedbacks
	where feedback_id = @feedback_id
end
go

--order
--create order
CREATE PROC [dbo].[sp_create_order]
(
    @account_id INT,
    @shippingDetail_id INT,
    @status INT,
    @list_json_order_details NVARCHAR(MAX)
)
AS
BEGIN
    DECLARE @order_id INT;
    
    -- Thêm hóa đơn mới
    INSERT INTO Order_invoices
    (account_id, shippingDetail_id, status)
    VALUES
    (@account_id, @shippingDetail_id, @status);
    
    SET @order_id = SCOPE_IDENTITY();
    
    -- Bảng tạm để tính tổng tiền
    CREATE TABLE #TempOrderDetails
    (
        product_id INT,
        quantity INT,
        voucher_id INT,
        total_details_money INT
    );

    -- Thêm chi tiết hóa đơn vào bảng tạm
    IF (@list_json_order_details IS NOT NULL)
    BEGIN
        INSERT INTO #TempOrderDetails (product_id, quantity, voucher_id, total_details_money)
        SELECT
            JSON_VALUE(l.value, '$.product_id'),
            JSON_VALUE(l.value, '$.quantity'),
            JSON_VALUE(l.value, '$.voucher_id'),
            CAST(JSON_VALUE(l.value, '$.total_details_money') AS INT)
        FROM OPENJSON(@list_json_order_details) AS l;
        
        -- Cập nhật tổng tiền trong bảng tạm
        UPDATE #TempOrderDetails
        SET total_details_money = (SELECT 
                                (P.price * TD.quantity - ISNULL(V.voucher_value, 0))
                            FROM Products AS P
                            INNER JOIN #TempOrderDetails AS TD ON P.product_id = TD.product_id
                            inner JOIN Vouchers AS V ON TD.voucher_id = V.voucher_id
                            WHERE TD.product_id = P.product_id);
        
        -- Thêm chi tiết hóa đơn từ bảng tạm vào bảng Order_details
        INSERT INTO Order_details ( product_id, quantity, voucher_id, total_details_money)
        SELECT product_id, quantity, voucher_id, total_details_money FROM #TempOrderDetails;

		UPDATE p
		set product_quantity = product_quantity - quantity
		from Products p inner join #TempOrderDetails td on p.product_id = td.product_id
		where p.product_id in (select td.product_id from #TempOrderDetails);
        
        -- Tính tổng tiền cho hóa đơn và cập nhật vào Order_invoices
        DECLARE @total_order_money INT;
        SET @total_order_money = (SELECT SUM(total_details_money) FROM #TempOrderDetails);
        
        UPDATE Order_invoices
        SET total_order_money = @total_order_money
        WHERE order_id = @order_id;
        
        -- Xóa bảng tạm
        DROP TABLE #TempOrderDetails;
    END;
    
    SELECT '';
END;
go