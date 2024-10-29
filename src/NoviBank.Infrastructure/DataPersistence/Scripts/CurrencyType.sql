﻿CREATE TYPE dbo.CurrencyType AS TABLE
    (
    Id NVARCHAR(36),
    Name NVARCHAR(10),
    Rate DECIMAL(5,5),
    Date DATETIME
    );