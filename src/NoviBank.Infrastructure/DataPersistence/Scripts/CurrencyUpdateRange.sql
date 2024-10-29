CREATE PROCEDURE dbo.CurrencyUpdateRange
    @Currencies dbo.CurrencyType READONLY
    AS
    BEGIN
        SET NOCOUNT ON;
        
        MERGE INTO dbo.Currencies AS target
        USING @Currencies AS source
        ON target.Name = source.Name
        WHEN MATCHED THEN 
            UPDATE SET
                target.Rate = source.Rate
        WHEN NOT MATCHED THEN 
            INSERT (Id, Name, Rate, Updated_At)
            VALUES (source.Id, source.Name, source.Rate, source.Date);
    END;