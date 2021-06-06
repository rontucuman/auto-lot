IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210606024140_Initial')
BEGIN
    CREATE TABLE [Customer] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] varchar(50) NOT NULL,
        [LastName] varchar(50) NOT NULL,
        [TimeStamp] rowversion NULL,
        CONSTRAINT [PK_Customer] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210606024140_Initial')
BEGIN
    CREATE TABLE [Make] (
        [Id] int NOT NULL IDENTITY,
        [Name] varchar(50) NOT NULL,
        [TimeStamp] rowversion NULL,
        CONSTRAINT [PK_Make] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210606024140_Initial')
BEGIN
    CREATE TABLE [CreditRisk] (
        [Id] int NOT NULL IDENTITY,
        [CustomerId] int NOT NULL,
        [FirstName] varchar(50) NOT NULL,
        [LastName] varchar(50) NOT NULL,
        [TimeStamp] rowversion NULL,
        CONSTRAINT [PK_CreditRisk] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Customer_CreditRisk] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210606024140_Initial')
BEGIN
    CREATE TABLE [Inventory] (
        [Id] int NOT NULL IDENTITY,
        [MakeId] int NOT NULL,
        [Color] varchar(50) NOT NULL,
        [PetName] varchar(50) NOT NULL,
        [TimeStamp] rowversion NULL,
        CONSTRAINT [PK_Inventory] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Make_Inventory] FOREIGN KEY ([MakeId]) REFERENCES [Make] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210606024140_Initial')
BEGIN
    CREATE TABLE [Order] (
        [Id] int NOT NULL IDENTITY,
        [CarId] int NOT NULL,
        [CustomerId] int NOT NULL,
        [TimeStamp] rowversion NULL,
        CONSTRAINT [PK_Order] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Customer_Order] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Inventory_Order] FOREIGN KEY ([CarId]) REFERENCES [Inventory] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210606024140_Initial')
BEGIN
    CREATE INDEX [IX_FK_Customer_CreditRisk] ON [CreditRisk] ([CustomerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210606024140_Initial')
BEGIN
    CREATE INDEX [IX_FK_Make_Inventory] ON [Inventory] ([MakeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210606024140_Initial')
BEGIN
    CREATE INDEX [IX_FK_Customer_Order] ON [Order] ([CustomerId], [CarId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210606024140_Initial')
BEGIN
    CREATE INDEX [IX_FK_Inventory_Order] ON [Order] ([CarId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210606024140_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210606024140_Initial', N'5.0.6');
END;
GO

COMMIT;
GO

