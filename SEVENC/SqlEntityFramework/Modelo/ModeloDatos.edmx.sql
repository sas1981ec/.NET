
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/01/2018 16:03:00
-- Generated from EDMX file: E:\Proyectos\SEVENC\SqlEntityFramework\Modelo\ModeloDatos.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SEVENC_BD];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_EmpresaSucursal_Empresa]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmpresaSucursal] DROP CONSTRAINT [FK_EmpresaSucursal_Empresa];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpresaSucursal_Sucursal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmpresaSucursal] DROP CONSTRAINT [FK_EmpresaSucursal_Sucursal];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpresaUsuario_Empresa]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmpresaUsuario] DROP CONSTRAINT [FK_EmpresaUsuario_Empresa];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpresaUsuario_Usuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmpresaUsuario] DROP CONSTRAINT [FK_EmpresaUsuario_Usuario];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuarioRol_Usuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsuarioRol] DROP CONSTRAINT [FK_UsuarioRol_Usuario];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuarioRol_Rol]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsuarioRol] DROP CONSTRAINT [FK_UsuarioRol_Rol];
GO
IF OBJECT_ID(N'[dbo].[FK_RolOperacion_Rol]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RolOperacion] DROP CONSTRAINT [FK_RolOperacion_Rol];
GO
IF OBJECT_ID(N'[dbo].[FK_RolOperacion_Operacion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RolOperacion] DROP CONSTRAINT [FK_RolOperacion_Operacion];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuarioSesionUsuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SesionesUsuarios] DROP CONSTRAINT [FK_UsuarioSesionUsuario];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Empresas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Empresas];
GO
IF OBJECT_ID(N'[dbo].[Sucursales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sucursales];
GO
IF OBJECT_ID(N'[dbo].[Usuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuarios];
GO
IF OBJECT_ID(N'[dbo].[Errores]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Errores];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Operaciones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Operaciones];
GO
IF OBJECT_ID(N'[dbo].[SesionesUsuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SesionesUsuarios];
GO
IF OBJECT_ID(N'[dbo].[EmpresaSucursal]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmpresaSucursal];
GO
IF OBJECT_ID(N'[dbo].[EmpresaUsuario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmpresaUsuario];
GO
IF OBJECT_ID(N'[dbo].[UsuarioRol]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsuarioRol];
GO
IF OBJECT_ID(N'[dbo].[RolOperacion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RolOperacion];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Empresas'
CREATE TABLE [dbo].[Empresas] (
    [IdEmpresa] tinyint IDENTITY(1,1) NOT NULL,
    [RazonSocial] nvarchar(255)  NOT NULL,
    [NombreComercial] nvarchar(150)  NOT NULL,
    [RUC] nvarchar(13)  NOT NULL,
    [IdRepresentanteLegal] nvarchar(13)  NOT NULL,
    [NombreRepresentanteLegal] nvarchar(200)  NOT NULL,
    [EstaActiva] bit  NOT NULL,
    [EstaEliminada] bit  NOT NULL,
    [Concurrencia] binary(8)  NOT NULL
);
GO

-- Creating table 'Sucursales'
CREATE TABLE [dbo].[Sucursales] (
    [IdSucursal] smallint IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(150)  NOT NULL,
    [Direccion] nvarchar(512)  NOT NULL,
    [EstaActiva] bit  NOT NULL,
    [EstaEliminada] bit  NOT NULL,
    [Concurrencia] binary(8)  NOT NULL,
    [EsMatriz] bit  NOT NULL
);
GO

-- Creating table 'Usuarios'
CREATE TABLE [dbo].[Usuarios] (
    [IdUsuario] int IDENTITY(1,1) NOT NULL,
    [Nombres] nvarchar(100)  NOT NULL,
    [Apellidos] nvarchar(100)  NOT NULL,
    [Email] nvarchar(100)  NOT NULL,
    [UserName] nvarchar(20)  NOT NULL,
    [Contrasena] nvarchar(max)  NOT NULL,
    [Foto] varbinary(max)  NOT NULL,
    [EstaActivo] bit  NOT NULL,
    [EstaBloqueado] bit  NOT NULL,
    [EstaEliminado] bit  NOT NULL,
    [EsSistema] bit  NOT NULL,
    [Concurrencia] binary(8)  NOT NULL
);
GO

-- Creating table 'Errores'
CREATE TABLE [dbo].[Errores] (
    [IdError] int IDENTITY(1,1) NOT NULL,
    [Tipo] nvarchar(1)  NOT NULL,
    [Fecha] datetime  NOT NULL,
    [Mensaje] nvarchar(512)  NOT NULL,
    [Detalle] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [IdRol] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(25)  NOT NULL,
    [Descripcion] nvarchar(255)  NOT NULL,
    [EstaActivo] bit  NOT NULL,
    [EsSistema] bit  NOT NULL,
    [Concurrencia] binary(8)  NOT NULL,
    [EstaEliminado] bit  NOT NULL
);
GO

-- Creating table 'Operaciones'
CREATE TABLE [dbo].[Operaciones] (
    [IdOperacion] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(30)  NOT NULL,
    [EstaActiva] bit  NOT NULL,
    [EsAuditable] bit  NOT NULL
);
GO

-- Creating table 'SesionesUsuarios'
CREATE TABLE [dbo].[SesionesUsuarios] (
    [IdSesion] bigint IDENTITY(1,1) NOT NULL,
    [FechaInicio] datetime  NOT NULL,
    [FechaFin] datetime  NULL,
    [Ip] nvarchar(20)  NOT NULL,
    [IdUsuario] int  NOT NULL
);
GO

-- Creating table 'Auditorias'
CREATE TABLE [dbo].[Auditorias] (
    [IdAuditoria] int IDENTITY(1,1) NOT NULL,
    [Fecha] datetime  NOT NULL,
    [IdUsuario] int  NOT NULL,
    [IdOperacion] int  NOT NULL
);
GO

-- Creating table 'DetallesAuditorias'
CREATE TABLE [dbo].[DetallesAuditorias] (
    [IdDetalleAuditoria] bigint IDENTITY(1,1) NOT NULL,
    [Entidad] nvarchar(50)  NOT NULL,
    [ClaveEntidad] nvarchar(30)  NOT NULL,
    [Campo] nvarchar(30)  NOT NULL,
    [ValorAntiguo] nvarchar(max)  NOT NULL,
    [ValorNuevo] nvarchar(max)  NOT NULL,
    [IdAuditoria] int  NOT NULL
);
GO

-- Creating table 'EmpresaSucursal'
CREATE TABLE [dbo].[EmpresaSucursal] (
    [Empresas_IdEmpresa] tinyint  NOT NULL,
    [Sucursales_IdSucursal] smallint  NOT NULL
);
GO

-- Creating table 'EmpresaUsuario'
CREATE TABLE [dbo].[EmpresaUsuario] (
    [Empresas_IdEmpresa] tinyint  NOT NULL,
    [Usuarios_IdUsuario] int  NOT NULL
);
GO

-- Creating table 'UsuarioRol'
CREATE TABLE [dbo].[UsuarioRol] (
    [Usuarios_IdUsuario] int  NOT NULL,
    [Roles_IdRol] int  NOT NULL
);
GO

-- Creating table 'RolOperacion'
CREATE TABLE [dbo].[RolOperacion] (
    [Roles_IdRol] int  NOT NULL,
    [Operaciones_IdOperacion] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdEmpresa] in table 'Empresas'
ALTER TABLE [dbo].[Empresas]
ADD CONSTRAINT [PK_Empresas]
    PRIMARY KEY CLUSTERED ([IdEmpresa] ASC);
GO

-- Creating primary key on [IdSucursal] in table 'Sucursales'
ALTER TABLE [dbo].[Sucursales]
ADD CONSTRAINT [PK_Sucursales]
    PRIMARY KEY CLUSTERED ([IdSucursal] ASC);
GO

-- Creating primary key on [IdUsuario] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [PK_Usuarios]
    PRIMARY KEY CLUSTERED ([IdUsuario] ASC);
GO

-- Creating primary key on [IdError] in table 'Errores'
ALTER TABLE [dbo].[Errores]
ADD CONSTRAINT [PK_Errores]
    PRIMARY KEY CLUSTERED ([IdError] ASC);
GO

-- Creating primary key on [IdRol] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([IdRol] ASC);
GO

-- Creating primary key on [IdOperacion] in table 'Operaciones'
ALTER TABLE [dbo].[Operaciones]
ADD CONSTRAINT [PK_Operaciones]
    PRIMARY KEY CLUSTERED ([IdOperacion] ASC);
GO

-- Creating primary key on [IdSesion] in table 'SesionesUsuarios'
ALTER TABLE [dbo].[SesionesUsuarios]
ADD CONSTRAINT [PK_SesionesUsuarios]
    PRIMARY KEY CLUSTERED ([IdSesion] ASC);
GO

-- Creating primary key on [IdAuditoria] in table 'Auditorias'
ALTER TABLE [dbo].[Auditorias]
ADD CONSTRAINT [PK_Auditorias]
    PRIMARY KEY CLUSTERED ([IdAuditoria] ASC);
GO

-- Creating primary key on [IdDetalleAuditoria] in table 'DetallesAuditorias'
ALTER TABLE [dbo].[DetallesAuditorias]
ADD CONSTRAINT [PK_DetallesAuditorias]
    PRIMARY KEY CLUSTERED ([IdDetalleAuditoria] ASC);
GO

-- Creating primary key on [Empresas_IdEmpresa], [Sucursales_IdSucursal] in table 'EmpresaSucursal'
ALTER TABLE [dbo].[EmpresaSucursal]
ADD CONSTRAINT [PK_EmpresaSucursal]
    PRIMARY KEY CLUSTERED ([Empresas_IdEmpresa], [Sucursales_IdSucursal] ASC);
GO

-- Creating primary key on [Empresas_IdEmpresa], [Usuarios_IdUsuario] in table 'EmpresaUsuario'
ALTER TABLE [dbo].[EmpresaUsuario]
ADD CONSTRAINT [PK_EmpresaUsuario]
    PRIMARY KEY CLUSTERED ([Empresas_IdEmpresa], [Usuarios_IdUsuario] ASC);
GO

-- Creating primary key on [Usuarios_IdUsuario], [Roles_IdRol] in table 'UsuarioRol'
ALTER TABLE [dbo].[UsuarioRol]
ADD CONSTRAINT [PK_UsuarioRol]
    PRIMARY KEY CLUSTERED ([Usuarios_IdUsuario], [Roles_IdRol] ASC);
GO

-- Creating primary key on [Roles_IdRol], [Operaciones_IdOperacion] in table 'RolOperacion'
ALTER TABLE [dbo].[RolOperacion]
ADD CONSTRAINT [PK_RolOperacion]
    PRIMARY KEY CLUSTERED ([Roles_IdRol], [Operaciones_IdOperacion] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Empresas_IdEmpresa] in table 'EmpresaSucursal'
ALTER TABLE [dbo].[EmpresaSucursal]
ADD CONSTRAINT [FK_EmpresaSucursal_Empresa]
    FOREIGN KEY ([Empresas_IdEmpresa])
    REFERENCES [dbo].[Empresas]
        ([IdEmpresa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Sucursales_IdSucursal] in table 'EmpresaSucursal'
ALTER TABLE [dbo].[EmpresaSucursal]
ADD CONSTRAINT [FK_EmpresaSucursal_Sucursal]
    FOREIGN KEY ([Sucursales_IdSucursal])
    REFERENCES [dbo].[Sucursales]
        ([IdSucursal])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpresaSucursal_Sucursal'
CREATE INDEX [IX_FK_EmpresaSucursal_Sucursal]
ON [dbo].[EmpresaSucursal]
    ([Sucursales_IdSucursal]);
GO

-- Creating foreign key on [Empresas_IdEmpresa] in table 'EmpresaUsuario'
ALTER TABLE [dbo].[EmpresaUsuario]
ADD CONSTRAINT [FK_EmpresaUsuario_Empresa]
    FOREIGN KEY ([Empresas_IdEmpresa])
    REFERENCES [dbo].[Empresas]
        ([IdEmpresa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Usuarios_IdUsuario] in table 'EmpresaUsuario'
ALTER TABLE [dbo].[EmpresaUsuario]
ADD CONSTRAINT [FK_EmpresaUsuario_Usuario]
    FOREIGN KEY ([Usuarios_IdUsuario])
    REFERENCES [dbo].[Usuarios]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpresaUsuario_Usuario'
CREATE INDEX [IX_FK_EmpresaUsuario_Usuario]
ON [dbo].[EmpresaUsuario]
    ([Usuarios_IdUsuario]);
GO

-- Creating foreign key on [Usuarios_IdUsuario] in table 'UsuarioRol'
ALTER TABLE [dbo].[UsuarioRol]
ADD CONSTRAINT [FK_UsuarioRol_Usuario]
    FOREIGN KEY ([Usuarios_IdUsuario])
    REFERENCES [dbo].[Usuarios]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Roles_IdRol] in table 'UsuarioRol'
ALTER TABLE [dbo].[UsuarioRol]
ADD CONSTRAINT [FK_UsuarioRol_Rol]
    FOREIGN KEY ([Roles_IdRol])
    REFERENCES [dbo].[Roles]
        ([IdRol])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioRol_Rol'
CREATE INDEX [IX_FK_UsuarioRol_Rol]
ON [dbo].[UsuarioRol]
    ([Roles_IdRol]);
GO

-- Creating foreign key on [Roles_IdRol] in table 'RolOperacion'
ALTER TABLE [dbo].[RolOperacion]
ADD CONSTRAINT [FK_RolOperacion_Rol]
    FOREIGN KEY ([Roles_IdRol])
    REFERENCES [dbo].[Roles]
        ([IdRol])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Operaciones_IdOperacion] in table 'RolOperacion'
ALTER TABLE [dbo].[RolOperacion]
ADD CONSTRAINT [FK_RolOperacion_Operacion]
    FOREIGN KEY ([Operaciones_IdOperacion])
    REFERENCES [dbo].[Operaciones]
        ([IdOperacion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RolOperacion_Operacion'
CREATE INDEX [IX_FK_RolOperacion_Operacion]
ON [dbo].[RolOperacion]
    ([Operaciones_IdOperacion]);
GO

-- Creating foreign key on [IdUsuario] in table 'SesionesUsuarios'
ALTER TABLE [dbo].[SesionesUsuarios]
ADD CONSTRAINT [FK_UsuarioSesionUsuario]
    FOREIGN KEY ([IdUsuario])
    REFERENCES [dbo].[Usuarios]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioSesionUsuario'
CREATE INDEX [IX_FK_UsuarioSesionUsuario]
ON [dbo].[SesionesUsuarios]
    ([IdUsuario]);
GO

-- Creating foreign key on [IdUsuario] in table 'Auditorias'
ALTER TABLE [dbo].[Auditorias]
ADD CONSTRAINT [FK_UsuarioAuditoria]
    FOREIGN KEY ([IdUsuario])
    REFERENCES [dbo].[Usuarios]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioAuditoria'
CREATE INDEX [IX_FK_UsuarioAuditoria]
ON [dbo].[Auditorias]
    ([IdUsuario]);
GO

-- Creating foreign key on [IdOperacion] in table 'Auditorias'
ALTER TABLE [dbo].[Auditorias]
ADD CONSTRAINT [FK_OperacionAuditoria]
    FOREIGN KEY ([IdOperacion])
    REFERENCES [dbo].[Operaciones]
        ([IdOperacion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OperacionAuditoria'
CREATE INDEX [IX_FK_OperacionAuditoria]
ON [dbo].[Auditorias]
    ([IdOperacion]);
GO

-- Creating foreign key on [IdAuditoria] in table 'DetallesAuditorias'
ALTER TABLE [dbo].[DetallesAuditorias]
ADD CONSTRAINT [FK_AuditoriaDetalleAuditoria]
    FOREIGN KEY ([IdAuditoria])
    REFERENCES [dbo].[Auditorias]
        ([IdAuditoria])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuditoriaDetalleAuditoria'
CREATE INDEX [IX_FK_AuditoriaDetalleAuditoria]
ON [dbo].[DetallesAuditorias]
    ([IdAuditoria]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------