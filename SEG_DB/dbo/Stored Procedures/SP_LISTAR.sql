CREATE PROCEDURE [dbo].[SP_LISTAR]
@table AS VARCHAR(MAX)

AS

BEGIN 
	DECLARE @Query VARCHAR(MAX)
	
	SET @Query = 'SELECT * FROM ' + QUOTENAME(@table);

	EXEC sp_sqlexec @Query

END