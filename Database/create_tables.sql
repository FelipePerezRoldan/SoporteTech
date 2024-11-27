-- Crear Base de Datos
CREATE DATABASE SoporteTechDB;
GO

USE SoporteTechDB;
GO

-- Tabla: Usuarios
CREATE TABLE Usuarios (
    UsuarioID INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(255) UNIQUE NOT NULL,
    ContraseñaHash NVARCHAR(255) NOT NULL,
    Salt NVARCHAR(255) NOT NULL,
    RolID INT NOT NULL,
    Activo BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RolID) REFERENCES Roles(RolID)
);

-- Tabla: Roles
CREATE TABLE Roles (
    RolID INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL UNIQUE
);

-- Tabla: Permisos
CREATE TABLE Permisos (
    PermisoID INT IDENTITY PRIMARY KEY,
    Recurso NVARCHAR(100) NOT NULL UNIQUE,
    Metodo NVARCHAR(10) DEFAULT 'GET'
);

-- Tabla: RolesPermisos
CREATE TABLE RolesPermisos (
    RolID INT NOT NULL,
    PermisoID INT NOT NULL,
    PRIMARY KEY (RolID, PermisoID),
    FOREIGN KEY (RolID) REFERENCES Roles(RolID),
    FOREIGN KEY (PermisoID) REFERENCES Permisos(PermisoID)
);

-- Tabla: TokensActivos
CREATE TABLE TokensActivos (
    TokenID INT IDENTITY PRIMARY KEY,
    UsuarioID INT NOT NULL,
    Token NVARCHAR(512) NOT NULL,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    Expiracion DATETIME NOT NULL,
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
);

-- Tabla: Clientes
CREATE TABLE Clientes (
    ClienteID INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(255) UNIQUE NOT NULL,
    Telefono NVARCHAR(15),
    Direccion NVARCHAR(255)
);

-- Tabla: Equipos
CREATE TABLE Equipos (
    EquipoID INT IDENTITY PRIMARY KEY,
    ClienteID INT NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Modelo NVARCHAR(100),
    Serie NVARCHAR(100),
    FechaRegistro DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID)
);

-- Tabla: Tickets
CREATE TABLE Tickets (
    TicketID INT IDENTITY PRIMARY KEY,
    ClienteID INT NOT NULL,
    EquipoID INT NOT NULL,
    Descripcion NVARCHAR(MAX) NOT NULL,
    Estado NVARCHAR(50) DEFAULT 'Abierto',
    TecnicoID INT,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FechaCierre DATETIME,
    FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID),
    FOREIGN KEY (EquipoID) REFERENCES Equipos(EquipoID),
    FOREIGN KEY (TecnicoID) REFERENCES Usuarios(UsuarioID)
);

-- Tabla: HistorialTickets
CREATE TABLE HistorialTickets (
    HistorialID INT IDENTITY PRIMARY KEY,
    TicketID INT NOT NULL,
    UsuarioID INT NOT NULL,
    Fecha DATETIME DEFAULT GETDATE(),
    Comentario NVARCHAR(MAX),
    EstadoAnterior NVARCHAR(50),
    EstadoNuevo NVARCHAR(50),
    FOREIGN KEY (TicketID) REFERENCES Tickets(TicketID),
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
);

-- Insertar Roles Iniciales
INSERT INTO Roles (Nombre) VALUES ('Administrador'), ('Técnico'), ('Cliente');

-- Insertar Permisos Iniciales
INSERT INTO Permisos (Recurso, Metodo) VALUES 
('/tickets', 'GET'),
('/tickets', 'POST'),
('/usuarios', 'GET'),
('/usuarios', 'POST'),
('/equipos', 'GET'),
('/equipos', 'POST');
