
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/14/2017 15:17:23
-- Generated from EDMX file: C:\Domosti\AccesoDatos\Modelos\ModeloDomosti.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [pruebas_Domosti];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CiudadelaUsuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Usuarios] DROP CONSTRAINT [FK_CiudadelaUsuario];
GO
IF OBJECT_ID(N'[dbo].[FK_CiudadelaVivienda]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Viviendas] DROP CONSTRAINT [FK_CiudadelaVivienda];
GO
IF OBJECT_ID(N'[dbo].[FK_ResidenteFotoResidente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personas_Residente] DROP CONSTRAINT [FK_ResidenteFotoResidente];
GO
IF OBJECT_ID(N'[dbo].[FK_ResidenteUsuarioApp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personas_Residente] DROP CONSTRAINT [FK_ResidenteUsuarioApp];
GO
IF OBJECT_ID(N'[dbo].[FK_ResidenteVisitante]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personas_Visitante] DROP CONSTRAINT [FK_ResidenteVisitante];
GO
IF OBJECT_ID(N'[dbo].[FK_ResidenteVivienda_Residente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResidenteVivienda] DROP CONSTRAINT [FK_ResidenteVivienda_Residente];
GO
IF OBJECT_ID(N'[dbo].[FK_ResidenteVivienda_Vivienda]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResidenteVivienda] DROP CONSTRAINT [FK_ResidenteVivienda_Vivienda];
GO
IF OBJECT_ID(N'[dbo].[FK_DispositivoUsuarioApp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dispositivos] DROP CONSTRAINT [FK_DispositivoUsuarioApp];
GO
IF OBJECT_ID(N'[dbo].[FK_ViviendaDispositivoResidenteVivienda]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DispositivoResidenteViviendas] DROP CONSTRAINT [FK_ViviendaDispositivoResidenteVivienda];
GO
IF OBJECT_ID(N'[dbo].[FK_ResidenteDispositivoResidenteVivienda]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DispositivoResidenteViviendas] DROP CONSTRAINT [FK_ResidenteDispositivoResidenteVivienda];
GO
IF OBJECT_ID(N'[dbo].[FK_DispositivoDispositivoResidenteVivienda]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DispositivoResidenteViviendas] DROP CONSTRAINT [FK_DispositivoDispositivoResidenteVivienda];
GO
IF OBJECT_ID(N'[dbo].[FK_DispositivoResidenteViviendaAuditoriaDrv]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuditoriaDrvs] DROP CONSTRAINT [FK_DispositivoResidenteViviendaAuditoriaDrv];
GO
IF OBJECT_ID(N'[dbo].[FK_CabeceraPermisoPermiso]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Permisos] DROP CONSTRAINT [FK_CabeceraPermisoPermiso];
GO
IF OBJECT_ID(N'[dbo].[FK_DispositivoPermiso]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Permisos] DROP CONSTRAINT [FK_DispositivoPermiso];
GO
IF OBJECT_ID(N'[dbo].[FK_VisitantePermiso]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Permisos] DROP CONSTRAINT [FK_VisitantePermiso];
GO
IF OBJECT_ID(N'[dbo].[FK_ResidentePermiso]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Permisos] DROP CONSTRAINT [FK_ResidentePermiso];
GO
IF OBJECT_ID(N'[dbo].[FK_MotivoAccesoPermiso]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Permisos] DROP CONSTRAINT [FK_MotivoAccesoPermiso];
GO
IF OBJECT_ID(N'[dbo].[FK_PermisoAuditoriaPermiso]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuditoriaPermisos] DROP CONSTRAINT [FK_PermisoAuditoriaPermiso];
GO
IF OBJECT_ID(N'[dbo].[FK_ResidentePermisoManual]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PermisosManuales] DROP CONSTRAINT [FK_ResidentePermisoManual];
GO
IF OBJECT_ID(N'[dbo].[FK_PermisoAcceso]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accesos] DROP CONSTRAINT [FK_PermisoAcceso];
GO
IF OBJECT_ID(N'[dbo].[FK_PermisoManualMotivoAcceso]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PermisosManuales] DROP CONSTRAINT [FK_PermisoManualMotivoAcceso];
GO
IF OBJECT_ID(N'[dbo].[FK_ResidenteNotificacion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notificaciones] DROP CONSTRAINT [FK_ResidenteNotificacion];
GO
IF OBJECT_ID(N'[dbo].[FK_PermisoManualVivienda]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PermisosManuales] DROP CONSTRAINT [FK_PermisoManualVivienda];
GO
IF OBJECT_ID(N'[dbo].[FK_Residente_inherits_Persona]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personas_Residente] DROP CONSTRAINT [FK_Residente_inherits_Persona];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuarioApp_inherits_Persona]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personas_UsuarioApp] DROP CONSTRAINT [FK_UsuarioApp_inherits_Persona];
GO
IF OBJECT_ID(N'[dbo].[FK_Visitante_inherits_Persona]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personas_Visitante] DROP CONSTRAINT [FK_Visitante_inherits_Persona];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Ciudadelas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ciudadelas];
GO
IF OBJECT_ID(N'[dbo].[Usuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuarios];
GO
IF OBJECT_ID(N'[dbo].[Viviendas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Viviendas];
GO
IF OBJECT_ID(N'[dbo].[Personas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Personas];
GO
IF OBJECT_ID(N'[dbo].[FotosResidentes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FotosResidentes];
GO
IF OBJECT_ID(N'[dbo].[Dispositivos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dispositivos];
GO
IF OBJECT_ID(N'[dbo].[DispositivoResidenteViviendas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DispositivoResidenteViviendas];
GO
IF OBJECT_ID(N'[dbo].[AuditoriaDrvs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuditoriaDrvs];
GO
IF OBJECT_ID(N'[dbo].[CabecerasPermisos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CabecerasPermisos];
GO
IF OBJECT_ID(N'[dbo].[Permisos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Permisos];
GO
IF OBJECT_ID(N'[dbo].[MotivosAccesos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MotivosAccesos];
GO
IF OBJECT_ID(N'[dbo].[AuditoriaPermisos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuditoriaPermisos];
GO
IF OBJECT_ID(N'[dbo].[PermisosManuales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PermisosManuales];
GO
IF OBJECT_ID(N'[dbo].[Accesos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accesos];
GO
IF OBJECT_ID(N'[dbo].[Errores]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Errores];
GO
IF OBJECT_ID(N'[dbo].[Notificaciones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notificaciones];
GO
IF OBJECT_ID(N'[dbo].[Personas_Residente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Personas_Residente];
GO
IF OBJECT_ID(N'[dbo].[Personas_UsuarioApp]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Personas_UsuarioApp];
GO
IF OBJECT_ID(N'[dbo].[Personas_Visitante]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Personas_Visitante];
GO
IF OBJECT_ID(N'[dbo].[ResidenteVivienda]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ResidenteVivienda];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Ciudadelas'
CREATE TABLE [dbo].[Ciudadelas] (
    [IdCiudadela] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(50)  NOT NULL,
    [Tipo] nvarchar(1)  NOT NULL
);
GO

-- Creating table 'Usuarios'
CREATE TABLE [dbo].[Usuarios] (
    [IdUsuario] int IDENTITY(1,1) NOT NULL,
    [Tipo] nchar(1)  NOT NULL,
    [UserName] nvarchar(20)  NOT NULL,
    [Contrasena] nvarchar(max)  NOT NULL,
    [IdCiudadela] int  NOT NULL
);
GO

-- Creating table 'Viviendas'
CREATE TABLE [dbo].[Viviendas] (
    [IdVivienda] int IDENTITY(1,1) NOT NULL,
    [Manzana] smallint  NOT NULL,
    [Villa] smallint  NOT NULL,
    [Calle] nvarchar(50)  NOT NULL,
    [Telefono] nvarchar(10)  NOT NULL,
    [EstaEliminada] bit  NOT NULL,
    [EsSistema] bit  NOT NULL,
    [IdCiudadela] int  NOT NULL,
    [UpdateToken] binary(8)  NOT NULL
);
GO

-- Creating table 'Personas'
CREATE TABLE [dbo].[Personas] (
    [IdPersona] int IDENTITY(1,1) NOT NULL,
    [TipoIdentificacion] nchar(1)  NOT NULL,
    [Identificacion] nvarchar(15)  NOT NULL,
    [Nombres] nvarchar(50)  NOT NULL,
    [Apellidos] nvarchar(50)  NOT NULL,
    [Email] nvarchar(30)  NOT NULL,
    [UpdateToken] binary(8)  NOT NULL
);
GO

-- Creating table 'FotosResidentes'
CREATE TABLE [dbo].[FotosResidentes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Foto] varbinary(max)  NOT NULL,
    [UpdateToken] binary(8)  NOT NULL
);
GO

-- Creating table 'Dispositivos'
CREATE TABLE [dbo].[Dispositivos] (
    [IdDispositivo] int IDENTITY(1,1) NOT NULL,
    [IdDevice] nvarchar(50)  NOT NULL,
    [Imei] nvarchar(50)  NOT NULL,
    [IccId] nvarchar(50)  NOT NULL,
    [Nombre] nvarchar(50)  NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    [IdUsuarioApp] int  NOT NULL,
    [UpdateToken] binary(8)  NOT NULL,
    [Token] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DispositivoResidenteViviendas'
CREATE TABLE [dbo].[DispositivoResidenteViviendas] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Estado] nchar(1)  NOT NULL,
    [IdVivienda] int  NOT NULL,
    [IdResidente] int  NOT NULL,
    [IdDispositivo] int  NOT NULL
);
GO

-- Creating table 'AuditoriaDrvs'
CREATE TABLE [dbo].[AuditoriaDrvs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Estado] nchar(1)  NOT NULL,
    [Fecha] datetime  NOT NULL,
    [IdDispositivoResidenteVivienda] bigint  NOT NULL
);
GO

-- Creating table 'CabecerasPermisos'
CREATE TABLE [dbo].[CabecerasPermisos] (
    [IdCabeceraPermiso] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Permisos'
CREATE TABLE [dbo].[Permisos] (
    [IdPermiso] int IDENTITY(1,1) NOT NULL,
    [FechaInicial] datetime  NOT NULL,
    [FechaFin] datetime  NOT NULL,
    [FechaCreacion] datetime  NOT NULL,
    [Estado] nchar(1)  NOT NULL,
    [PermisoContinuo] bit  NOT NULL,
    [FechaUltimaActualizacion] datetime  NOT NULL,
    [DetalleAdicional] nvarchar(max)  NOT NULL,
    [IdCabeceraPermiso] int  NOT NULL,
    [IdDispositivo] int  NOT NULL,
    [IdVisitante] int  NOT NULL,
    [IdResidente] int  NOT NULL,
    [IdMotivoAcceso] smallint  NOT NULL,
    [UpdateToken] binary(8)  NOT NULL,
    [IdVivienda] int  NOT NULL
);
GO

-- Creating table 'MotivosAccesos'
CREATE TABLE [dbo].[MotivosAccesos] (
    [IdMotivoAcceso] smallint IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'AuditoriaPermisos'
CREATE TABLE [dbo].[AuditoriaPermisos] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Campo] nvarchar(30)  NOT NULL,
    [ValorAntiguo] nvarchar(max)  NOT NULL,
    [ValorNuevo] nvarchar(max)  NOT NULL,
    [FechaCambio] datetime  NOT NULL,
    [IdPermiso] int  NOT NULL
);
GO

-- Creating table 'PermisosManuales'
CREATE TABLE [dbo].[PermisosManuales] (
    [IdPermisoManual] int IDENTITY(1,1) NOT NULL,
    [FechaIngreso] datetime  NOT NULL,
    [Observaciones] nvarchar(max)  NOT NULL,
    [CedulaVisitante] nvarchar(15)  NOT NULL,
    [NombreVisitante] nvarchar(100)  NOT NULL,
    [IdResidente] int  NOT NULL,
    [IdMotivoAcceso] smallint  NOT NULL,
    [IdVivienda] int  NOT NULL
);
GO

-- Creating table 'Accesos'
CREATE TABLE [dbo].[Accesos] (
    [IdAcceso] bigint IDENTITY(1,1) NOT NULL,
    [FechaAcceso] datetime  NOT NULL,
    [Observaciones] nvarchar(max)  NOT NULL,
    [IdPermiso] int  NOT NULL
);
GO

-- Creating table 'Errores'
CREATE TABLE [dbo].[Errores] (
    [IdError] int IDENTITY(1,1) NOT NULL,
    [Tipo] nchar(1)  NOT NULL,
    [Fecha] datetime  NOT NULL,
    [Mensaje] nvarchar(255)  NOT NULL,
    [Detalle] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Notificaciones'
CREATE TABLE [dbo].[Notificaciones] (
    [IdNotificacion] bigint IDENTITY(1,1) NOT NULL,
    [Mensaje] nvarchar(max)  NOT NULL,
    [Fecha] datetime  NOT NULL,
    [IdResidente] int  NOT NULL,
    [Tipo] char(1)  NOT NULL
);
GO

-- Creating table 'Personas_Residente'
CREATE TABLE [dbo].[Personas_Residente] (
    [FechaNacimiento] datetime  NOT NULL,
    [TelefonoMovil] nvarchar(13)  NOT NULL,
    [EstaActivo] bit  NOT NULL,
    [PuedeUsarApp] bit  NOT NULL,
    [EstaEliminado] bit  NOT NULL,
    [IdUsuarioApp] int  NULL,
    [PoseeDeudas] bit  NOT NULL,
    [IdPersona] int  NOT NULL,
    [FotoResidente_Id] int  NOT NULL
);
GO

-- Creating table 'Personas_UsuarioApp'
CREATE TABLE [dbo].[Personas_UsuarioApp] (
    [FechaNacimiento] datetime  NOT NULL,
    [Contrasena] nvarchar(max)  NOT NULL,
    [IdPersona] int  NOT NULL
);
GO

-- Creating table 'Personas_Visitante'
CREATE TABLE [dbo].[Personas_Visitante] (
    [Telefono] nvarchar(15)  NOT NULL,
    [EstaEliminado] bit  NOT NULL,
    [IdResidente] int  NOT NULL,
    [IdPersona] int  NOT NULL
);
GO

-- Creating table 'ResidenteVivienda'
CREATE TABLE [dbo].[ResidenteVivienda] (
    [Residentes_IdPersona] int  NOT NULL,
    [Viviendas_IdVivienda] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdCiudadela] in table 'Ciudadelas'
ALTER TABLE [dbo].[Ciudadelas]
ADD CONSTRAINT [PK_Ciudadelas]
    PRIMARY KEY CLUSTERED ([IdCiudadela] ASC);
GO

-- Creating primary key on [IdUsuario] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [PK_Usuarios]
    PRIMARY KEY CLUSTERED ([IdUsuario] ASC);
GO

-- Creating primary key on [IdVivienda] in table 'Viviendas'
ALTER TABLE [dbo].[Viviendas]
ADD CONSTRAINT [PK_Viviendas]
    PRIMARY KEY CLUSTERED ([IdVivienda] ASC);
GO

-- Creating primary key on [IdPersona] in table 'Personas'
ALTER TABLE [dbo].[Personas]
ADD CONSTRAINT [PK_Personas]
    PRIMARY KEY CLUSTERED ([IdPersona] ASC);
GO

-- Creating primary key on [Id] in table 'FotosResidentes'
ALTER TABLE [dbo].[FotosResidentes]
ADD CONSTRAINT [PK_FotosResidentes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [IdDispositivo] in table 'Dispositivos'
ALTER TABLE [dbo].[Dispositivos]
ADD CONSTRAINT [PK_Dispositivos]
    PRIMARY KEY CLUSTERED ([IdDispositivo] ASC);
GO

-- Creating primary key on [Id] in table 'DispositivoResidenteViviendas'
ALTER TABLE [dbo].[DispositivoResidenteViviendas]
ADD CONSTRAINT [PK_DispositivoResidenteViviendas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AuditoriaDrvs'
ALTER TABLE [dbo].[AuditoriaDrvs]
ADD CONSTRAINT [PK_AuditoriaDrvs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [IdCabeceraPermiso] in table 'CabecerasPermisos'
ALTER TABLE [dbo].[CabecerasPermisos]
ADD CONSTRAINT [PK_CabecerasPermisos]
    PRIMARY KEY CLUSTERED ([IdCabeceraPermiso] ASC);
GO

-- Creating primary key on [IdPermiso] in table 'Permisos'
ALTER TABLE [dbo].[Permisos]
ADD CONSTRAINT [PK_Permisos]
    PRIMARY KEY CLUSTERED ([IdPermiso] ASC);
GO

-- Creating primary key on [IdMotivoAcceso] in table 'MotivosAccesos'
ALTER TABLE [dbo].[MotivosAccesos]
ADD CONSTRAINT [PK_MotivosAccesos]
    PRIMARY KEY CLUSTERED ([IdMotivoAcceso] ASC);
GO

-- Creating primary key on [Id] in table 'AuditoriaPermisos'
ALTER TABLE [dbo].[AuditoriaPermisos]
ADD CONSTRAINT [PK_AuditoriaPermisos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [IdPermisoManual] in table 'PermisosManuales'
ALTER TABLE [dbo].[PermisosManuales]
ADD CONSTRAINT [PK_PermisosManuales]
    PRIMARY KEY CLUSTERED ([IdPermisoManual] ASC);
GO

-- Creating primary key on [IdAcceso] in table 'Accesos'
ALTER TABLE [dbo].[Accesos]
ADD CONSTRAINT [PK_Accesos]
    PRIMARY KEY CLUSTERED ([IdAcceso] ASC);
GO

-- Creating primary key on [IdError] in table 'Errores'
ALTER TABLE [dbo].[Errores]
ADD CONSTRAINT [PK_Errores]
    PRIMARY KEY CLUSTERED ([IdError] ASC);
GO

-- Creating primary key on [IdNotificacion] in table 'Notificaciones'
ALTER TABLE [dbo].[Notificaciones]
ADD CONSTRAINT [PK_Notificaciones]
    PRIMARY KEY CLUSTERED ([IdNotificacion] ASC);
GO

-- Creating primary key on [IdPersona] in table 'Personas_Residente'
ALTER TABLE [dbo].[Personas_Residente]
ADD CONSTRAINT [PK_Personas_Residente]
    PRIMARY KEY CLUSTERED ([IdPersona] ASC);
GO

-- Creating primary key on [IdPersona] in table 'Personas_UsuarioApp'
ALTER TABLE [dbo].[Personas_UsuarioApp]
ADD CONSTRAINT [PK_Personas_UsuarioApp]
    PRIMARY KEY CLUSTERED ([IdPersona] ASC);
GO

-- Creating primary key on [IdPersona] in table 'Personas_Visitante'
ALTER TABLE [dbo].[Personas_Visitante]
ADD CONSTRAINT [PK_Personas_Visitante]
    PRIMARY KEY CLUSTERED ([IdPersona] ASC);
GO

-- Creating primary key on [Residentes_IdPersona], [Viviendas_IdVivienda] in table 'ResidenteVivienda'
ALTER TABLE [dbo].[ResidenteVivienda]
ADD CONSTRAINT [PK_ResidenteVivienda]
    PRIMARY KEY CLUSTERED ([Residentes_IdPersona], [Viviendas_IdVivienda] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdCiudadela] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [FK_CiudadelaUsuario]
    FOREIGN KEY ([IdCiudadela])
    REFERENCES [dbo].[Ciudadelas]
        ([IdCiudadela])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CiudadelaUsuario'
CREATE INDEX [IX_FK_CiudadelaUsuario]
ON [dbo].[Usuarios]
    ([IdCiudadela]);
GO

-- Creating foreign key on [IdCiudadela] in table 'Viviendas'
ALTER TABLE [dbo].[Viviendas]
ADD CONSTRAINT [FK_CiudadelaVivienda]
    FOREIGN KEY ([IdCiudadela])
    REFERENCES [dbo].[Ciudadelas]
        ([IdCiudadela])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CiudadelaVivienda'
CREATE INDEX [IX_FK_CiudadelaVivienda]
ON [dbo].[Viviendas]
    ([IdCiudadela]);
GO

-- Creating foreign key on [FotoResidente_Id] in table 'Personas_Residente'
ALTER TABLE [dbo].[Personas_Residente]
ADD CONSTRAINT [FK_ResidenteFotoResidente]
    FOREIGN KEY ([FotoResidente_Id])
    REFERENCES [dbo].[FotosResidentes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResidenteFotoResidente'
CREATE INDEX [IX_FK_ResidenteFotoResidente]
ON [dbo].[Personas_Residente]
    ([FotoResidente_Id]);
GO

-- Creating foreign key on [IdUsuarioApp] in table 'Personas_Residente'
ALTER TABLE [dbo].[Personas_Residente]
ADD CONSTRAINT [FK_ResidenteUsuarioApp]
    FOREIGN KEY ([IdUsuarioApp])
    REFERENCES [dbo].[Personas_UsuarioApp]
        ([IdPersona])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResidenteUsuarioApp'
CREATE INDEX [IX_FK_ResidenteUsuarioApp]
ON [dbo].[Personas_Residente]
    ([IdUsuarioApp]);
GO

-- Creating foreign key on [IdResidente] in table 'Personas_Visitante'
ALTER TABLE [dbo].[Personas_Visitante]
ADD CONSTRAINT [FK_ResidenteVisitante]
    FOREIGN KEY ([IdResidente])
    REFERENCES [dbo].[Personas_Residente]
        ([IdPersona])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResidenteVisitante'
CREATE INDEX [IX_FK_ResidenteVisitante]
ON [dbo].[Personas_Visitante]
    ([IdResidente]);
GO

-- Creating foreign key on [Residentes_IdPersona] in table 'ResidenteVivienda'
ALTER TABLE [dbo].[ResidenteVivienda]
ADD CONSTRAINT [FK_ResidenteVivienda_Residente]
    FOREIGN KEY ([Residentes_IdPersona])
    REFERENCES [dbo].[Personas_Residente]
        ([IdPersona])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Viviendas_IdVivienda] in table 'ResidenteVivienda'
ALTER TABLE [dbo].[ResidenteVivienda]
ADD CONSTRAINT [FK_ResidenteVivienda_Vivienda]
    FOREIGN KEY ([Viviendas_IdVivienda])
    REFERENCES [dbo].[Viviendas]
        ([IdVivienda])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResidenteVivienda_Vivienda'
CREATE INDEX [IX_FK_ResidenteVivienda_Vivienda]
ON [dbo].[ResidenteVivienda]
    ([Viviendas_IdVivienda]);
GO

-- Creating foreign key on [IdUsuarioApp] in table 'Dispositivos'
ALTER TABLE [dbo].[Dispositivos]
ADD CONSTRAINT [FK_DispositivoUsuarioApp]
    FOREIGN KEY ([IdUsuarioApp])
    REFERENCES [dbo].[Personas_UsuarioApp]
        ([IdPersona])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DispositivoUsuarioApp'
CREATE INDEX [IX_FK_DispositivoUsuarioApp]
ON [dbo].[Dispositivos]
    ([IdUsuarioApp]);
GO

-- Creating foreign key on [IdVivienda] in table 'DispositivoResidenteViviendas'
ALTER TABLE [dbo].[DispositivoResidenteViviendas]
ADD CONSTRAINT [FK_ViviendaDispositivoResidenteVivienda]
    FOREIGN KEY ([IdVivienda])
    REFERENCES [dbo].[Viviendas]
        ([IdVivienda])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ViviendaDispositivoResidenteVivienda'
CREATE INDEX [IX_FK_ViviendaDispositivoResidenteVivienda]
ON [dbo].[DispositivoResidenteViviendas]
    ([IdVivienda]);
GO

-- Creating foreign key on [IdResidente] in table 'DispositivoResidenteViviendas'
ALTER TABLE [dbo].[DispositivoResidenteViviendas]
ADD CONSTRAINT [FK_ResidenteDispositivoResidenteVivienda]
    FOREIGN KEY ([IdResidente])
    REFERENCES [dbo].[Personas_Residente]
        ([IdPersona])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResidenteDispositivoResidenteVivienda'
CREATE INDEX [IX_FK_ResidenteDispositivoResidenteVivienda]
ON [dbo].[DispositivoResidenteViviendas]
    ([IdResidente]);
GO

-- Creating foreign key on [IdDispositivo] in table 'DispositivoResidenteViviendas'
ALTER TABLE [dbo].[DispositivoResidenteViviendas]
ADD CONSTRAINT [FK_DispositivoDispositivoResidenteVivienda]
    FOREIGN KEY ([IdDispositivo])
    REFERENCES [dbo].[Dispositivos]
        ([IdDispositivo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DispositivoDispositivoResidenteVivienda'
CREATE INDEX [IX_FK_DispositivoDispositivoResidenteVivienda]
ON [dbo].[DispositivoResidenteViviendas]
    ([IdDispositivo]);
GO

-- Creating foreign key on [IdDispositivoResidenteVivienda] in table 'AuditoriaDrvs'
ALTER TABLE [dbo].[AuditoriaDrvs]
ADD CONSTRAINT [FK_DispositivoResidenteViviendaAuditoriaDrv]
    FOREIGN KEY ([IdDispositivoResidenteVivienda])
    REFERENCES [dbo].[DispositivoResidenteViviendas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DispositivoResidenteViviendaAuditoriaDrv'
CREATE INDEX [IX_FK_DispositivoResidenteViviendaAuditoriaDrv]
ON [dbo].[AuditoriaDrvs]
    ([IdDispositivoResidenteVivienda]);
GO

-- Creating foreign key on [IdCabeceraPermiso] in table 'Permisos'
ALTER TABLE [dbo].[Permisos]
ADD CONSTRAINT [FK_CabeceraPermisoPermiso]
    FOREIGN KEY ([IdCabeceraPermiso])
    REFERENCES [dbo].[CabecerasPermisos]
        ([IdCabeceraPermiso])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CabeceraPermisoPermiso'
CREATE INDEX [IX_FK_CabeceraPermisoPermiso]
ON [dbo].[Permisos]
    ([IdCabeceraPermiso]);
GO

-- Creating foreign key on [IdDispositivo] in table 'Permisos'
ALTER TABLE [dbo].[Permisos]
ADD CONSTRAINT [FK_DispositivoPermiso]
    FOREIGN KEY ([IdDispositivo])
    REFERENCES [dbo].[Dispositivos]
        ([IdDispositivo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DispositivoPermiso'
CREATE INDEX [IX_FK_DispositivoPermiso]
ON [dbo].[Permisos]
    ([IdDispositivo]);
GO

-- Creating foreign key on [IdVisitante] in table 'Permisos'
ALTER TABLE [dbo].[Permisos]
ADD CONSTRAINT [FK_VisitantePermiso]
    FOREIGN KEY ([IdVisitante])
    REFERENCES [dbo].[Personas_Visitante]
        ([IdPersona])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VisitantePermiso'
CREATE INDEX [IX_FK_VisitantePermiso]
ON [dbo].[Permisos]
    ([IdVisitante]);
GO

-- Creating foreign key on [IdResidente] in table 'Permisos'
ALTER TABLE [dbo].[Permisos]
ADD CONSTRAINT [FK_ResidentePermiso]
    FOREIGN KEY ([IdResidente])
    REFERENCES [dbo].[Personas_Residente]
        ([IdPersona])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResidentePermiso'
CREATE INDEX [IX_FK_ResidentePermiso]
ON [dbo].[Permisos]
    ([IdResidente]);
GO

-- Creating foreign key on [IdMotivoAcceso] in table 'Permisos'
ALTER TABLE [dbo].[Permisos]
ADD CONSTRAINT [FK_MotivoAccesoPermiso]
    FOREIGN KEY ([IdMotivoAcceso])
    REFERENCES [dbo].[MotivosAccesos]
        ([IdMotivoAcceso])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MotivoAccesoPermiso'
CREATE INDEX [IX_FK_MotivoAccesoPermiso]
ON [dbo].[Permisos]
    ([IdMotivoAcceso]);
GO

-- Creating foreign key on [IdPermiso] in table 'AuditoriaPermisos'
ALTER TABLE [dbo].[AuditoriaPermisos]
ADD CONSTRAINT [FK_PermisoAuditoriaPermiso]
    FOREIGN KEY ([IdPermiso])
    REFERENCES [dbo].[Permisos]
        ([IdPermiso])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PermisoAuditoriaPermiso'
CREATE INDEX [IX_FK_PermisoAuditoriaPermiso]
ON [dbo].[AuditoriaPermisos]
    ([IdPermiso]);
GO

-- Creating foreign key on [IdResidente] in table 'PermisosManuales'
ALTER TABLE [dbo].[PermisosManuales]
ADD CONSTRAINT [FK_ResidentePermisoManual]
    FOREIGN KEY ([IdResidente])
    REFERENCES [dbo].[Personas_Residente]
        ([IdPersona])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResidentePermisoManual'
CREATE INDEX [IX_FK_ResidentePermisoManual]
ON [dbo].[PermisosManuales]
    ([IdResidente]);
GO

-- Creating foreign key on [IdPermiso] in table 'Accesos'
ALTER TABLE [dbo].[Accesos]
ADD CONSTRAINT [FK_PermisoAcceso]
    FOREIGN KEY ([IdPermiso])
    REFERENCES [dbo].[Permisos]
        ([IdPermiso])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PermisoAcceso'
CREATE INDEX [IX_FK_PermisoAcceso]
ON [dbo].[Accesos]
    ([IdPermiso]);
GO

-- Creating foreign key on [IdMotivoAcceso] in table 'PermisosManuales'
ALTER TABLE [dbo].[PermisosManuales]
ADD CONSTRAINT [FK_PermisoManualMotivoAcceso]
    FOREIGN KEY ([IdMotivoAcceso])
    REFERENCES [dbo].[MotivosAccesos]
        ([IdMotivoAcceso])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PermisoManualMotivoAcceso'
CREATE INDEX [IX_FK_PermisoManualMotivoAcceso]
ON [dbo].[PermisosManuales]
    ([IdMotivoAcceso]);
GO

-- Creating foreign key on [IdResidente] in table 'Notificaciones'
ALTER TABLE [dbo].[Notificaciones]
ADD CONSTRAINT [FK_ResidenteNotificacion]
    FOREIGN KEY ([IdResidente])
    REFERENCES [dbo].[Personas_Residente]
        ([IdPersona])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResidenteNotificacion'
CREATE INDEX [IX_FK_ResidenteNotificacion]
ON [dbo].[Notificaciones]
    ([IdResidente]);
GO

-- Creating foreign key on [IdVivienda] in table 'PermisosManuales'
ALTER TABLE [dbo].[PermisosManuales]
ADD CONSTRAINT [FK_PermisoManualVivienda]
    FOREIGN KEY ([IdVivienda])
    REFERENCES [dbo].[Viviendas]
        ([IdVivienda])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PermisoManualVivienda'
CREATE INDEX [IX_FK_PermisoManualVivienda]
ON [dbo].[PermisosManuales]
    ([IdVivienda]);
GO

-- Creating foreign key on [IdVivienda] in table 'Permisos'
ALTER TABLE [dbo].[Permisos]
ADD CONSTRAINT [FK_ViviendaPermiso]
    FOREIGN KEY ([IdVivienda])
    REFERENCES [dbo].[Viviendas]
        ([IdVivienda])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ViviendaPermiso'
CREATE INDEX [IX_FK_ViviendaPermiso]
ON [dbo].[Permisos]
    ([IdVivienda]);
GO

-- Creating foreign key on [IdPersona] in table 'Personas_Residente'
ALTER TABLE [dbo].[Personas_Residente]
ADD CONSTRAINT [FK_Residente_inherits_Persona]
    FOREIGN KEY ([IdPersona])
    REFERENCES [dbo].[Personas]
        ([IdPersona])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IdPersona] in table 'Personas_UsuarioApp'
ALTER TABLE [dbo].[Personas_UsuarioApp]
ADD CONSTRAINT [FK_UsuarioApp_inherits_Persona]
    FOREIGN KEY ([IdPersona])
    REFERENCES [dbo].[Personas]
        ([IdPersona])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IdPersona] in table 'Personas_Visitante'
ALTER TABLE [dbo].[Personas_Visitante]
ADD CONSTRAINT [FK_Visitante_inherits_Persona]
    FOREIGN KEY ([IdPersona])
    REFERENCES [dbo].[Personas]
        ([IdPersona])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------