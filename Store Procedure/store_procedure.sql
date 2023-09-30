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

--create brands
create proc sp_create_brand
(
    @brand_name nvarchar(100)
)
as
begin
    insert into Brands(brand_name)
    values(@brand_name)
end
go

--update brands
create proc sp_update_brand
(
    @brand_id int,
    @brand_name nvarchar(100)
)
as
begin
    if @brand_name is not null and @brand_name <> 'string'
    begin
        update Brands
        set brand_name = @brand_name
        where brand_id = @brand_id
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

--Customers
--get customer by id
create proc sp_get_customer_by_id
(
	@customer_id int
)
as
begin
	select * from Customers
	where customer_id = @customer_id
end
go

--get all customer
create proc sp_get_all_customer
as
begin
	select * from Customers
end
go

--create customer
create proc sp_create_customer
(
	@username nvarchar(500),
	@password varchar(256),
	@customer_name nvarchar(255),
	@phone_number varchar(20),
	@address nvarchar(500),
	@email varchar(150),
	@role_id int
)
as
begin
	insert into Customers(username, password, customer_name, phone_number, address, email, role_id)
	values(@username, @password, @customer_name, @phone_number, @address, @email, @role_id)
end
go

--update customer
create proc sp_update_customer
(
	@customer_id int,
	@username nvarchar(500),
	@password varchar(256),
	@customer_name nvarchar(255),
	@phone_number varchar(20),
	@address nvarchar(500),
	@email varchar(150),
	@role_id int
)
as
begin
	update Customers
	set
		username = CASE WHEN @username IS NOT NULL AND @username <> 'null' AND @username <> 'string' THEN @username ELSE username END,
		password = CASE WHEN @password IS NOT NULL AND @password <> 'null' AND @password <> 'string' THEN @password ELSE password END,
		customer_name = CASE WHEN @customer_name IS NOT NULL AND @customer_name <> 'null' AND @customer_name <> 'string' THEN @customer_name ELSE customer_name END,
		phone_number = CASE WHEN @phone_number IS NOT NULL AND @phone_number <> 'null' AND @phone_number <> 'string' AND @phone_number NOT LIKE '%[^0-9]%' THEN @phone_number ELSE phone_number END,
		address = CASE WHEN @address IS NOT NULL AND @address <> 'null' AND @address <> 'string' THEN @address ELSE address END,
		email = CASE WHEN @email IS NOT NULL AND @email <> 'null' AND @email <> 'string' THEN @email ELSE email END
	where customer_id = @customer_id
end
go

--delete customer
create proc sp_delete_customer
(
	@customer_id int
)
as
begin
	delete Customers
	where customer_id = @customer_id
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

--create product
--get product by id
create proc sp_create_product
(
	@product_name nvarchar(500),
	@price int,
	@discount int,
	@image_link varchar(500),
	@description nvarchar(500),
	@product_quantity int,
	@created_date datetime,
	@updated_date datetime,
	@category_id int,
	@brand_id int
)
as
begin
	insert into Products(product_name, price, discount, image_link, description, product_quantity, created_date, updated_date, category_id, brand_id)
	values(@product_name, @price, @discount, @image_link, @description, @product_quantity, @created_date, @updated_date, @category_id, @brand_id)
end
go

--update products
create proc sp_update_product
(
	@product_id int,
	@product_name nvarchar(500),
	@price int,
	@discount int,
	@image_link varchar(500),
	@description nvarchar(500),
	@product_quantity int,
	@category_id int,
	@brand_id int
)
as
begin
	update Products
	set
		product_name = CASE WHEN @product_name IS NOT NULL AND @product_name <> 'null' AND @product_name <> 'string' THEN @product_name ELSE product_name END,
		price = CASE WHEN @price IS NOT NULL AND @price <> 'null' AND @price <> 'string' THEN @price ELSE price END,
		discount = CASE WHEN @discount IS NOT NULL AND @discount <> 'null' AND @discount <> 'string' THEN @discount ELSE discount END,
		image_link = CASE WHEN @image_link IS NOT NULL AND @image_link <> 'null' AND @image_link <> 'string' THEN @image_link ELSE image_link END,
		description = CASE WHEN @description IS NOT NULL AND @description <> 'null' AND @description <> 'string' THEN @description ELSE description END,
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
