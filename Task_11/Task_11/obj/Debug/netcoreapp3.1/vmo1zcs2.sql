IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Doctor] (
    [IdDoctor] int NOT NULL IDENTITY,
    [FirstName] nvarchar(100) NOT NULL,
    [LastName] nvarchar(100) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Doctor] PRIMARY KEY ([IdDoctor])
);

GO

CREATE TABLE [Medicament] (
    [IdMedicament] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(100) NOT NULL,
    [Type] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Medicament] PRIMARY KEY ([IdMedicament])
);

GO

CREATE TABLE [Patient] (
    [IdPatient] int NOT NULL IDENTITY,
    [FirstName] nvarchar(100) NOT NULL,
    [LastName] nvarchar(100) NOT NULL,
    [Birthdate] datetime2 NOT NULL,
    CONSTRAINT [PK_Patient] PRIMARY KEY ([IdPatient])
);

GO

CREATE TABLE [Prescription] (
    [IdPrescription] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [DueDate] datetime2 NOT NULL,
    [IdDoctor] int NOT NULL,
    [IdPatient] int NOT NULL,
    CONSTRAINT [PK_Prescription] PRIMARY KEY ([IdPrescription]),
    CONSTRAINT [FK_Prescription_Doctor_IdDoctor] FOREIGN KEY ([IdDoctor]) REFERENCES [Doctor] ([IdDoctor]) ON DELETE CASCADE,
    CONSTRAINT [FK_Prescription_Patient_IdPatient] FOREIGN KEY ([IdPatient]) REFERENCES [Patient] ([IdPatient]) ON DELETE CASCADE
);

GO

CREATE TABLE [Prescription_Medicament] (
    [IdMedicament] int NOT NULL,
    [IdPrescription] int NOT NULL,
    [Dose] int NULL,
    [Details] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Prescription_Medicament] PRIMARY KEY ([IdMedicament], [IdPrescription]),
    CONSTRAINT [FK_Prescription_Medicament_Medicament_IdMedicament] FOREIGN KEY ([IdMedicament]) REFERENCES [Medicament] ([IdMedicament]) ON DELETE CASCADE,
    CONSTRAINT [FK_Prescription_Medicament_Prescription_IdPrescription] FOREIGN KEY ([IdPrescription]) REFERENCES [Prescription] ([IdPrescription]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Prescription_IdDoctor] ON [Prescription] ([IdDoctor]);

GO

CREATE INDEX [IX_Prescription_IdPatient] ON [Prescription] ([IdPatient]);

GO

CREATE INDEX [IX_Prescription_Medicament_IdPrescription] ON [Prescription_Medicament] ([IdPrescription]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200523175946_AddedTables', N'3.1.4');

GO

