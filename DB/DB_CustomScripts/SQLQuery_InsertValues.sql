USE [WebApp]
GO

declare @i int
set @i=0 
while (@i<100000)

Begin


	BEGIN
		DECLARE @name VARCHAR(55)
		DECLARE @length INT
		SELECT @name = ''
		SET @length = CAST(RAND() * 55 as INT)
		WHILE @length <> 0
			BEGIN
			SELECT @name = @name + CHAR(CAST(RAND() * 96 + 32 as INT))
			SET @length = @length - 1
			END
		print(@name)
	END
	
	BEGIN
		DECLARE @description VARCHAR(254)
		DECLARE @length_desc INT
		SELECT @description = ''
		SET @length_desc = CAST(RAND() * 254 as INT)
		WHILE @length_desc <> 0
			BEGIN
			SELECT @description = @description + CHAR(CAST(RAND() * 96 + 32 as INT))
			SET @length_desc = @length_desc - 1
			END
		print(@description)
	END

INSERT INTO [dbo].[Product] ([Name],[Description]) VALUES (@name, @description);
	SET @i = @i +1
end

GO