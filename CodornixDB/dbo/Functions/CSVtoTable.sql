
-- =============================================
-- Author:        <Kumar Pankaj Verma>
-- Create date: <05-Apr-2011>
-- Description:    <Convert CSV to Table>
-- =============================================
CREATE FUNCTION [dbo].[CSVtoTable]
(
    @LIST varchar(7000),
    @Delimeter varchar(10)
)
RETURNS @RET1 TABLE (RESULT varchar(100))
AS
BEGIN
    DECLARE @RET TABLE(RESULT varchar(100))
    
    IF LTRIM(RTRIM(@LIST))='' RETURN  

    DECLARE @START BIGINT
    DECLARE @LASTSTART BIGINT
    SET @LASTSTART=0
    SET @START=CHARINDEX(@Delimeter,@LIST,0)

    IF @START=0
    INSERT INTO @RET VALUES(SUBSTRING(@LIST,0,LEN(@LIST)+1))

    WHILE(@START >0)
    BEGIN
        INSERT INTO @RET VALUES(SUBSTRING(@LIST,@LASTSTART,@START-@LASTSTART))
        SET @LASTSTART=@START+1
        SET @START=CHARINDEX(@Delimeter,@LIST,@START+1)
        IF(@START=0)
        INSERT INTO @RET VALUES(SUBSTRING(@LIST,@LASTSTART,LEN(@LIST)+1))
    END
    
    INSERT INTO @RET1 SELECT * FROM @RET
    RETURN 
END

