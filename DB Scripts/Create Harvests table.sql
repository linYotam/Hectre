CREATE TABLE Harvests (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OrchardId UNIQUEIDENTIFIER NOT NULL,
    SupervisorId UNIQUEIDENTIFIER NOT NULL,
    PickerId UNIQUEIDENTIFIER NOT NULL,
    PickingDate DATETIME NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    BinCount INT NOT NULL,
    HourlyWageRate DECIMAL(10, 2) NOT NULL,
    HoursWorked DECIMAL(10, 2) NOT NULL,
    Variety NVARCHAR(100) NOT NULL,
    CONSTRAINT FK_Harvest_Orchard FOREIGN KEY (OrchardId) REFERENCES Orchards(Id)
);
