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
	@role_name varchar(20)
)
as
begin
	insert into Roles(role_name)
	values(@role_name)
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

--get account details
CREATE PROC sp_get_account_detail_by_id
(
	@account_id INT
)
AS
BEGIN
	select a.account_id, username, password, full_name, address, phone_number, email, gender
	from Account a inner join AccountDetails ad on a.account_id = ad.account_id
	where a.account_id = @account_id
END
GO


--get account by username
create proc sp_get_account_details_by_username
(
	@username nvarchar(500)
)
as
begin
	select top 1 a.account_id, username, password, full_name, address, phone_number, email, gender
	from Account a inner join AccountDetails ad on a.account_id = ad.account_id
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
CREATE PROC sp_signup_account
(
	@username nvarchar(500),
	@password varchar(256)
)
AS
BEGIN
	insert into Account(username, password)
	VALUES(@username, @password)
END
GO

--create account
create proc sp_create_account
(
	@username nvarchar(500),
	@password varchar(256),
	@role_id int,
	@list_json_account_details NVARCHAR(MAX)
)
as
begin
	DECLARE @account_id int;
	INSERT into Account(
		username,
		password,
		role_id
	)
	VALUES(@username, @password, @role_id);
	SET @account_id = (select SCOPE_IDENTITY())
	IF(@list_json_account_details is not null)
	BEGIN
		INSERT into AccountDetails(
			account_id,
			full_name,
			address,
			phone_number,
			email,
			gender
		)
			select @account_id,
					JSON_VALUE(l.value, '$.full_name'),
					JSON_VALUE(l.value, '$.address'),
					JSON_VALUE(l.value, '$.phone_number'),
					JSON_VALUE(l.value, '$.email'),
					JSON_VALUE(l.value, '$.gender')
			from openjson(@list_json_account_details) as l;
	END
	SELECT '';
end
go

--update account
create proc sp_update_account
(
	@account_id int,
	@username nvarchar(500),
	@password varchar(256),
	@role_id int,
	@list_json_account_details NVARCHAR(MAX)
)
as
begin
	update Account
	set
		username = @username,
		password = @password,
		role_id = @role_id
	where account_id = @account_id

	if(@list_json_account_details is not null)
	BEGIN
		select
			JSON_VALUE(l.value, '$.accDetail_id') as accDetail_id,
			JSON_VALUE(l.value, '$.account_id') as account_id,
			JSON_VALUE(l.value, '$.full_name') as full_name,
			JSON_VALUE(l.value, '$.address') as address,
			JSON_VALUE(l.value, '$.phone_number') as phone_number,
			JSON_VALUE(l.value, '$.email') as email,
			JSON_VALUE(l.value, '$.gender') as gender,
			JSON_VALUE(l.value, '$.status') as status
			into #Results
		from openjson(@list_json_account_details) as l;

		--insert if status = 1
		INSERT into AccountDetails(
			account_id,
			full_name,
			address,
			phone_number,
			email,
			gender
		)
		SELECT 
			@account_id,
			r.full_name,
			r.address,
			r.phone_number,
			r.email,
			r.gender
		from #Results r
		where r.status = '1'

		--update if status = 2
		UPDATE AccountDetails
		SET
			full_name = CASE WHEN r.full_name IS NOT NULL AND r.full_name <> 'null' AND r.full_name <> 'string' THEN r.full_name ELSE AccountDetails.full_name END,
			address = CASE WHEN r.address IS NOT NULL AND r.address <> 'null' AND r.address <> 'string' THEN r.address ELSE AccountDetails.address END,
			phone_number = CASE WHEN r.phone_number IS NOT NULL AND r.phone_number <> 'null' AND r.phone_number <> 'string' AND r.phone_number NOT LIKE '%[^0-9]%' THEN 
			r.phone_number ELSE AccountDetails.phone_number END,
			gender = CASE WHEN r.gender IS NOT NULL AND r.gender <> 'null' AND r.gender <> 'string' THEN r.gender ELSE AccountDetails.gender END
		from #Results r
		where AccountDetails.accDetail_id = r.accDetail_id and r.status = '2';

		--delete if status = 3
		delete ad 
		from AccountDetails ad
		inner join #Results r
			on ad.accDetail_id = r.accDetail_id
		where r.status = '3'

		DROP TABLE #Result
	END;
	SELECT '';
end
go


--delete account
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

--get all products by brand name
CREATE proc sp_get_product_by_brand
(
	@page_index int,
	@page_size int,
	@brand_name nvarchar(100)
)
AS
BEGIN
	DECLARE @RecordCount bigint;
	if (@page_size <> 0)
	BEGIN
		set NOCOUNT ON;
			select(ROW_NUMBER() OVER(order by product_id asc)) as RowNumber,
			p.*
			into #Results1
			from Brands as b inner join Products as p on b.brand_id = p.brand_id
			where (@brand_name = '' or b.brand_name like N'%' + @brand_name + '%')
			select @RecordCount = COUNT(*)
			from #Results1;
			SELECT *, @RecordCount as RecordCount
			from #Results1
			WHERE ROWNUMBER BETWEEN(@page_index - 1) * @page_size + 1 and (((@page_index - 1) * @page_size + 1) + @page_size) - 1
				or @page_index = -1;
			drop TABLE #Results1;
	END
	ELSE
	BEGIN
		
		set NOCOUNT ON;
			select(ROW_NUMBER() OVER(order by product_id asc)) as RowNumber,
			p.*
			into #Results2
			from Brands b inner join Products p on b.brand_id = p.brand_id
			where (@brand_name = '' or b.brand_name like N'%' + @brand_name + '%')
			select @RecordCount = COUNT(*)
			from #Results2;
			SELECT *, @RecordCount as RecordCount
			from #Results2
			drop TABLE #Results2;
	END
END
GO
			

--create brands
CREATE PROC sp_create_brands
(
    @brand_name nvarchar(100)
)
AS
BEGIN
	insert into Brands(brand_name)
	VALUES(@brand_name)
END
GO

--update brands
create proc sp_update_brand
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


--category main
--create
create PROC sp_create_category
(
	@categoryMain_name NVARCHAR(100),
	@list_json_sub_category NVARCHAR(MAX)
)
AS
BEGIN
	DECLARE @categoryMain_id int;
	insert into CategoryMain(
		categoryMain_name
	)
	VALUES(@categoryMain_name)
	set @categoryMain_id = (select SCOPE_IDENTITY())
	if(@list_json_sub_category is not null)
	BEGIN
		insert into SubCategories(
			subCategory_name,
			categoryMain_id
		)
		select JSON_VALUE(l.value, '$.subCategory_name'),
				@categoryMain_id
		from openjson(@list_json_sub_category) as l;
	END
	select '';
END
GO


--UPDATE
CREATE proc sp_update_category(
	@categoryMain_id int,
	@categoryMain_name NVARCHAR(100),
	@list_json_sub_category NVARCHAR(MAX)

)
AS
BEGIN
	UPDATE CategoryMain
	SET
		categoryMain_name = CASE WHEN @categoryMain_name IS NOT NULL AND @categoryMain_name <> 'null' AND @categoryMain_name <> 'string' THEN @categoryMain_name ELSE categoryMain_name END
	where categoryMain_id = @categoryMain_id

	if(@list_json_sub_category is not null)
	BEGIN
		select 
				JSON_VALUE(l.value, '$.subCategory_id') as subCategory_id,
				JSON_VALUE(l.value, '$.subCategory_name') as subCategory_name,
				JSON_VALUE(l.value, '$.categoryMain_id') as categoryMain_id,
				JSON_VALUE(l.value, '$.status') as status
				into #Results
		from openjson(@list_json_sub_category) as l;

		--insert if status = 1
		INSERT into SubCategories(subCategory_name, categoryMain_id)
		SELECT 
			#Results.subCategory_name,
			@categoryMain_id
		from #Results
		WHERE #Results.status = '1'

		--update if status = 2
		update SubCategories
		set
			subCategory_name = #Results.subCategory_name,
			categoryMain_id = #Results.categoryMain_id
		from #Results
		where SubCategories.subCategory_id = #Results.subCategory_id and #Results.status = '2'

		--delete if status = 3
		DELETE s
		from SubCategories s
		INNER JOIN #Results r on s.subCategory_id = r.subCategory_id 
		where r.status = '3'
		drop TABLE #Results
	END
	select ''
		
END
GO


--DELETE
create PROC sp_delete_category_main
(
	@categoryMain_id int
)
AS
BEGIN
	DELETE from CategoryMain
	WHERE categoryMain_id = @categoryMain_id
END
GO

--get all product by category
CREATE proc sp_get_product_by_cate
(
	@page_index int,
	@page_size int,
	@subCategory_name nvarchar(350)
)
AS
BEGIN
	DECLARE @RecordCount bigint;
	if (@page_size <> 0)
	BEGIN
		set NOCOUNT ON;
			select(ROW_NUMBER() OVER(order by product_id asc)) as RowNumber,
			p.*
			into #Results1
			from SubCategories as s inner join Products as p on p.subCategory_id = s.subCategory_id
			where (@subCategory_name = '' or s.subCategory_name like N'%' + @subCategory_name + '%')
			select @RecordCount = COUNT(*)
			from #Results1;
			SELECT *, @RecordCount as RecordCount
			from #Results1
			WHERE ROWNUMBER BETWEEN(@page_index - 1) * @page_size + 1 and (((@page_index - 1) * @page_size + 1) + @page_size) - 1
				or @page_index = -1;
			drop TABLE #Results1;
	END
	ELSE
	BEGIN
		
		set NOCOUNT ON;
			select(ROW_NUMBER() OVER(order by product_id asc)) as RowNumber,
			p.*
			into #Results2
			from SubCategories as s inner join Products as p on p.subCategory_id = s.subCategory_id
			where (@subCategory_name = '' or s.subCategory_name like N'%' + @subCategory_name + '%')
			select @RecordCount = COUNT(*)
			from #Results2;
			SELECT *, @RecordCount as RecordCount
			from #Results2
			drop TABLE #Results2;
	END
END
GO

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

--get all details of product by id
CREATE proc sp_get_all_details_of_product_by_id
(
	@product_id INT
)
AS
BEGIN
	select p.product_id, p.product_name, p.price, p.discount, p.product_quantity, product_description, b.brand_name
	from Products p INNER JOIN Product_details pd on p.product_id = pd.product_id
	INNER join Brands b on p.brand_id = b.brand_id
	WHERE p.product_id = @product_id
end
GO

--search product by name
CREATE proc sp_search_product
(
	@page_index int,
	@page_size int,
	@product_name nvarchar(500)
)
AS
BEGIN
	DECLARE @RecordCount BIGINT;
	if(@page_size <> 0)
	BEGIN
		set NOCOUNT on;
			SELECT(ROW_NUMBER() OVER(
				order by product_id asc
			)) as RowNumber,
				p.product_name,
				p.price,
				p.discount,
				p.image_avatar,
				p.product_quantity
				into #Results1
			from Products as p
			where (@product_name = '' or p.product_name like N'%' + @product_name + '%');
			select @RecordCount = COUNT(*)
			from #Results1;
			SELECT *, @RecordCount as RecordCount
			from #Results1
			WHERE ROWNUMBER BETWEEN (@page_index - 1) * @page_size +1 and (((@page_index - 1) * @page_size + 1) + @page_size) - 1
				or @page_index = -1;
			drop TABLE #Results1;

	END
	ELSE
	begin
		set NOCOUNT on;
			SELECT(ROW_NUMBER() OVER(
				order by product_id asc
			)) as RowNumber,
				p.product_id,
				p.product_name,
				p.price,
				p.discount,
				p.image_avatar,
				p.product_quantity
				into #Results2
			from Products as p
			where (@product_name = '' or p.product_name like N'%' + @product_name + '%');
			select @RecordCount = COUNT(*)
			from #Results2;
			SELECT *, @RecordCount as RecordCount
			from #Results2
			drop TABLE #Results2;
	END
end
GO

--create proc search products by category and brand
CREATE PROC sp_search_product_by_cate_and_brand
(
	@page_index int,
	@page_size int,
	@subCategory_name nvarchar(350),
	@brand_name nvarchar(100)
)
AS
BEGIN
	DECLARE @RecordCount BIGINT;
	if(@page_size <> 0)
	BEGIN
		set NOCOUNT on;
			SELECT(ROW_NUMBER() OVER(
				order by product_id asc
			)) as RowNumber,
				p.product_id,
				p.product_name,
				p.price,
				p.discount,
				p.image_avatar,
				p.product_quantity
				into #Results1
			from Products as p inner join Brands as b on p.brand_id = b.brand_id
			inner join SubCategories as s on p.subCategory_id = s.subCategory_id
			where (@subCategory_name = '' or s.subCategory_name like N'%' + @subCategory_name + '%')
			and (@brand_name = '' or b.brand_name like N'%' + @brand_name + '%');
			select @RecordCount = COUNT(*)
			from #Results1;
			SELECT *, @RecordCount as RecordCount
			from #Results1
			WHERE ROWNUMBER BETWEEN (@page_index - 1) * @page_size +1 and (((@page_index - 1) * @page_size + 1) + @page_size) - 1
				or @page_index = -1;
			drop TABLE #Results1;
	END
	ELSE
	BEGIN
		set NOCOUNT on;
			SELECT(ROW_NUMBER() OVER(
				order by product_id asc
			)) as RowNumber,
				p.product_id,
				p.product_name,
				p.price,
				p.discount,
				p.image_avatar,
				p.product_quantity
				into #Results2
			from Products as p inner join Brands as b on p.brand_id = b.brand_id
			inner join SubCategories as s on p.subCategory_id = s.subCategory_id
			where (@subCategory_name = '' or s.subCategory_name like N'%' + @subCategory_name + '%')
			and (@brand_name = '' or b.brand_name like N'%' + @brand_name + '%');
			select @RecordCount = COUNT(*)
			from #Results2;
			SELECT *, @RecordCount as RecordCount
			from #Results2
			drop TABLE #Results2;
	END
END
GO

--search product by price range
CREATE PROC sp_search_product_by_price_range
(
	@page_index int,
	@page_size int,
	@fr_price DECIMAL(18, 0),
	@to_price DECIMAL(18, 0)
)
AS
BEGIN
	DECLARE @RecordCount bigint;
	if(@page_size <> 0)
		BEGIN
			SET NOCOUNT ON;
			SELECT(ROW_NUMBER() OVER(
				order by product_id asc)) as RowNumber,
				product_id,
				product_name
				price,
				discount,
				image_avatar,
				product_quantity,
				subCategory_name,
				brand_name
			into #Results1
			from Products p INNER JOIN SubCategories s ON p.subCategory_id = s.subCategory_id
			INNER JOIN Brands b on p.brand_id = b.brand_id
			WHERE ((@fr_price is null
				and @to_price is null)
				or (@fr_price is not null
					and @to_price is null
					and price >= @fr_price)
				or (@fr_price is null
					and @to_price is not null
					and price <= @to_price)
				or (price BETWEEN @fr_price and @to_price))
			SELECT @RecordCount = COUNT(*)
			from #Results1;
			SELECT *, @RecordCount as RecordCount
			from #Results1
			where ROWNUMBER BETWEEN(@page_index - 1) * @page_size + 1 and (((@page_index - 1) * @page_size + 1) + @page_size) - 1
				or @page_index = -1
			drop TABLE #Results1;
		END
		ELSE
		BEGIN
			SET NOCOUNT ON;
			SELECT(ROW_NUMBER() OVER(
				order by product_id asc)) as RowNumber,
				product_id,
				product_name
				price,
				discount,
				image_avatar,
				product_quantity,
				subCategory_name,
				brand_name
			into #Results2
			from Products p INNER JOIN SubCategories s ON p.subCategory_id = s.subCategory_id
			INNER JOIN Brands b on p.brand_id = b.brand_id
			WHERE ((@fr_price is null
				and @to_price is null)
				or (@fr_price is not null
					and @to_price is null
					and price >= @fr_price)
				or (@fr_price is null
					and @to_price is not null
					and price <= @to_price)
				or (price BETWEEN @fr_price and @to_price))
			SELECT @RecordCount = COUNT(*)
			from #Results2;
			SELECT *, @RecordCount as RecordCount
			from #Results2
			drop TABLE #Results2;
		END
END
GO

--search product by date range
CREATE PROC sp_search_product_by_date_range
(
	@page_index int,
	@page_size int,
	@fr_date datetime,
	@to_date DATETIME
)
AS
BEGIN
	DECLARE @RecordCount bigint;
	if(@page_size <> 0)
		BEGIN
			SET NOCOUNT ON;
			SELECT(ROW_NUMBER() OVER(
				order by product_id asc)) as RowNumber,
				product_id,
				product_name
				price,
				discount,
				image_avatar,
				product_quantity,
				subCategory_name,
				brand_name
			into #Results1
			from Products p INNER JOIN SubCategories s ON p.subCategory_id = s.subCategory_id
			INNER JOIN Brands b on p.brand_id = b.brand_id
			WHERE ((@fr_date is null
				and @to_date is null)
				or (@fr_date is not null
					and @to_date is null
					and created_date >= @fr_date)
				or (@fr_date is null
					and @to_date is not null
					and created_date <= @to_date)
				or (created_date BETWEEN @fr_date and @to_date))
			SELECT @RecordCount = COUNT(*)
			from #Results1;
			SELECT *, @RecordCount as RecordCount
			from #Results1
			where ROWNUMBER BETWEEN(@page_index - 1) * @page_size + 1 and (((@page_index - 1) * @page_size + 1) + @page_size) - 1
				or @page_index = -1
			drop TABLE #Results1;
		END
		ELSE
		BEGIN
			SET NOCOUNT ON;
			SELECT(ROW_NUMBER() OVER(
				order by product_id asc)) as RowNumber,
				product_id,
				product_name
				price,
				discount,
				image_avatar,
				product_quantity,
				subCategory_name,
				brand_name
			into #Results2
			from Products p INNER JOIN SubCategories s ON p.subCategory_id = s.subCategory_id
			INNER JOIN Brands b on p.brand_id = b.brand_id
			WHERE ((@fr_date is null
				and @to_date is null)
				or (@fr_date is not null
					and @to_date is null
					and created_date >= @fr_date)
				or (@fr_date is null
					and @to_date is not null
					and created_date <= @to_date)
				or (created_date BETWEEN @fr_date and @to_date))
			SELECT @RecordCount = COUNT(*)
			from #Results2;
			SELECT *, @RecordCount as RecordCount
			from #Results2
			drop TABLE #Results2;
		END
END
GO


--get new products
create proc sp_get_new_products
AS
BEGIN
	select top 12 product_id, product_name, price, discount, image_avatar, product_quantity, created_date, brand_name, subCategory_name from Products p
	inner join Brands b on p.brand_id = b.brand_id
	inner join SubCategories s on p.subCategory_id = s.subCategory_id
	ORDER BY created_date
end
GO
--ran

--not run
--get all product
create proc sp_get_all_product
as
begin
	select * from Products
end
go


--create product
create proc sp_create_product
(
	@product_name nvarchar(500),
	@price DECIMAL(18, 0),
	@discount DECIMAL(18, 0),
	@image_avatar varchar(500),
	@product_quantity int,
	@subCategory_id int,
	@brand_id int,
	@list_json_product_details NVARCHAR(MAX)
)
AS
BEGIN
	DECLARE @product_id int;
	insert into Products
			(
				product_name,
				price,
				discount,
				image_avatar,
				product_quantity,
				subCategory_id,
				brand_id
			)
			VALUES (
				@product_name,
				@price,
				@image_avatar,
				@product_quantity,
				@subCategory_id,
				@brand_id
			)
			set @product_id = (select SCOPE_IDENTITY())
			if(@list_json_product_details is not null)
			BEGIN
				insert into Product_details
				(
					product_id,
					product_description
				)
				select @product_id,
					JSON_VALUE(p.value, '$.product_description')
				from openjson(@list_json_account_details) as l;
			END
		SELECT '';
END
GO

--update products
CREATE PROC sp_update_product
(
	@product_id int,
	@product_name nvarchar(500),
	@price DECIMAL(18, 0),
	@discount DECIMAL(18, 0),
	@image_avatar varchar(500),
	@product_quantity int,
	@updated_date datetime,
	@subCategory_id int,
	@brand_id int,
	@list_json_product_details NVARCHAR(MAX)
)
AS
BEGIN
	UPDATE Products
	SET
		product_name = CASE WHEN @product_name IS NOT NULL AND @product_name <> 'null' AND @product_name <> 'string' THEN @product_name ELSE product_name END,
		price = CASE WHEN @price IS NOT NULL AND @price <> 'null' AND @price <> 'string' THEN @price ELSE price END,
		discount = CASE WHEN @discount IS NOT NULL AND @discount <> 'null' AND @discount <> 'string' THEN @discount ELSE discount END,
		image_avatar = CASE WHEN @image_avatar IS NOT NULL AND @image_avatar <> 'null' AND @image_avatar <> 'string' THEN @image_avatar ELSE image_avatar END,
		product_quantity = CASE WHEN @product_quantity IS NOT NULL AND @product_quantity <> 'null' AND @product_quantity <> 'string' THEN @product_quantity ELSE product_quantity END,
		updated_date = CAST(GETDATE() as date),
		subCategory_id = CASE WHEN @subCategory_id IS NOT NULL AND @subCategory_id <> 'null' AND @subCategory_id <> 'string' THEN @subCategory_id ELSE subCategory_id END,
		brand_id = CASE WHEN @brand_id IS NOT NULL AND @brand_id <> 'null' AND @brand_id <> 'string' THEN @brand_id ELSE brand_id END
	WHERE product_id = @product_id
	IF(@list_json_product_details is not null)
	BEGIN
		SELECT
			JSON_VALUE(l.value, '$.product_detail_id') as product_detail_id,
			JSON_VALUE(l.value, '$.product_id') as product_id,
			JSON_VALUE(l.value, '$.product_description') as product_description,
			JSON_VALUE(l.value, '$.status') as status
		into #Results
		FROM openjson(@list_json_account_details) as l;

		--insert if status = 1
		INSERT into Product_details(
			product_id,
			product_description
		)
			SELECT 
				@product_id,
				#Results.product_description
			from #Results
			WHERE #Results.status = '1'

		--update if status = 2
		UPDATE Product_details
		SET
			product_description = #Results.product_description
		from #Results
		WHERE Product_details.product_description = #Results.product_description and #Results.status = '2'

		--delete if status = 3
		DELETE pd
		from Product_details pd
		inner join #Results r on pd.product_detail_id = r.product_detail_id
		WHERE r.status = '3'
		drop TABLE #Results
	END
	SELECT '';
END
GO

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


--voucher
--get voucher by date
create proc sp_get_voucher_by_date
(
	@fr_date datetime,
	@to_date datetime
)
AS
BEGIN
	SELECT* from Vouchers
	WHERE ((@fr_date is null
		and @to_date is null)
		or (@fr_date is not null
			and @to_date is null
			and time_start >= @fr_date)
		or (@fr_date is null
			and @to_date is not null
			and time_start <= @to_date)
		or (time_start BETWEEN @fr_date and @to_date))


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