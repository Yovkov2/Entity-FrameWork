BEGIN TRANSACTION;
GO

ALTER TABLE [Students] ADD [Adress] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241004180547_AdressAdded', N'6.0.31');
GO

COMMIT;
GO

