-- Insertar Roles Iniciales
INSERT INTO Roles (Nombre) VALUES ('Administrador'), ('Técnico'), ('Cliente');

-- Insertar Permisos Iniciales
INSERT INTO Permisos (Recurso, Metodo) VALUES 
('/tickets', 'GET'),
('/tickets', 'POST'),
('/tickets', 'PUT'),
('/tickets', 'DELETE'),
('/usuarios', 'GET'),
('/usuarios', 'POST'),
('/equipos', 'GET'),
('/equipos', 'POST');

-- Insertar Usuarios Iniciales (con contraseñas ficticias)
INSERT INTO Usuarios (Nombre, Correo, ContraseñaHash, Salt, RolID) 
VALUES 
('Admin Usuario', 'admin@soportetech.com', 'hashed_password_1', 'salt_1', 1),  -- Rol de Administrador
('Tecnico Usuario', 'tecnico@soportetech.com', 'hashed_password_2', 'salt_2', 2),  -- Rol de Técnico
('Cliente Usuario', 'cliente@soportetech.com', 'hashed_password_3', 'salt_3', 3); -- Rol de Cliente

-- Insertar RolesPermisos (Administrador tiene todos los permisos)
INSERT INTO RolesPermisos (RolID, PermisoID)
SELECT 1, PermisoID FROM Permisos;  -- Administrador tiene todos los permisos

-- Insertar RolesPermisos para Técnico (Solo permisos para Tickets y Equipos)
INSERT INTO RolesPermisos (RolID, PermisoID)
SELECT 2, PermisoID 
FROM Permisos 
WHERE Recurso IN ('/tickets', '/equipos');  -- Técnico tiene permisos para tickets y equipos

-- Insertar RolesPermisos para Cliente (Solo permisos para ver Tickets)
INSERT INTO RolesPermisos (RolID, PermisoID)
SELECT 3, PermisoID
FROM Permisos 
WHERE Recurso = '/tickets';  -- Cliente solo puede ver tickets

-- Insertar Clientes
INSERT INTO Clientes (Nombre, Correo, Telefono, Direccion)
VALUES 
('Cliente Uno', 'clienteuno@soportetech.com', '123456789', 'Calle Ficticia 123'),
('Cliente Dos', 'clientedos@soportetech.com', '987654321', 'Calle Ficticia 456');

-- Insertar Equipos para los clientes
INSERT INTO Equipos (ClienteID, Nombre, Modelo, Serie)
VALUES 
(1, 'Equipo Cliente Uno', 'Modelo 1', 'S/N 12345'),
(2, 'Equipo Cliente Dos', 'Modelo 2', 'S/N 67890');

-- Insertar Tickets
INSERT INTO Tickets (ClienteID, EquipoID, Descripcion, Estado, TecnicoID)
VALUES 
(1, 1, 'El equipo no enciende', 'Abierto', 2),  -- Ticket para el Cliente 1, asignado al Técnico 2
(2, 2, 'Pantalla rota', 'Abierto', 2);  -- Ticket para el Cliente 2, asignado al Técnico 2

-- Insertar Historial de Tickets
INSERT INTO HistorialTickets (TicketID, UsuarioID, Comentario, EstadoAnterior, EstadoNuevo)
VALUES 
(1, 1, 'Se ha reportado que el equipo no enciende.', 'Abierto', 'En Proceso'),
(2, 2, 'Pantalla rota, se requiere cambiar la pantalla.', 'Abierto', 'En Proceso');

-- Insertar Tokens Activos
-- Para fines de ejemplo, generamos un token simple y expiración ficticia
INSERT INTO TokensActivos (UsuarioID, Token, Expiracion)
VALUES 
(1, 'token_ficticio_admin_123456', DATEADD(HOUR, 1, GETDATE())),  -- Token para Admin, válido por 1 hora
(2, 'token_ficticio_tecnico_123456', DATEADD(HOUR, 2, GETDATE())),  -- Token para Técnico, válido por 2 horas
(3, 'token_ficticio_cliente_123456', DATEADD(HOUR, 3, GETDATE()));  -- Token para Cliente, válido por 3 horas

-- Verificación
SELECT * FROM Clientes;
SELECT * FROM Equipos;
SELECT * FROM HistorialTickets;
SELECT * FROM Permisos;
SELECT * FROM Roles;
SELECT * FROM RolesPermisos;
SELECT * FROM Tickets;
SELECT * FROM TokensActivos;
SELECT * FROM Usuarios;

