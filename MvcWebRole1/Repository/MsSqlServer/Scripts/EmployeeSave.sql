SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
BEGIN TRANSACTION;
UPDATE dbo.Employee 
	SET 
		Name = @Name,
		Location = @Location,
		Email = @Email,
		Phone = @Phone,
		Title = @Title,
		Roles = @Roles
	WHERE Id = @Id;
IF @@ROWCOUNT = 0
BEGIN
  INSERT INTO dbo.Employee(Name, Location, Email, Phone, Title, Roles)
  VALUES(@Name, @Location, @Email, @Phone, @Title, @Roles);
END
COMMIT TRANSACTION;
SELECT * FROM dbo.Employee WHERE Id = @@IDENTITY or Id = @Id;