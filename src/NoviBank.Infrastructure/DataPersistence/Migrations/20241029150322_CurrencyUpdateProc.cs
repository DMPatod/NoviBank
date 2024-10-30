using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECB.Infrastructure.DataPersistence.Migrations
{
    /// <inheritdoc />
    public partial class CurrencyUpdateProc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                 CREATE TYPE dbo.TT_CurrencyType AS TABLE
                                 (
                                     Id   UNIQUEIDENTIFIER,
                                     Name NVARCHAR(10),
                                     Rate DECIMAL(5, 5),
                                     Date DATETIME
                                 );
                                 """);
            migrationBuilder.Sql("""
                                 CREATE PROCEDURE dbo.SP_CurrencyUpdateRange
                                 @Currencies dbo.TT_CurrencyType READONLY
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
                                 """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TYPE IF EXISTS TT_CurrencyType");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_CurrencyUpdateRange");
        }
    }
}
