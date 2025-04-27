CREATE DATABASE FitGym;
GO
USE  FitGym;
GO
	CREATE TABLE Usuarios(
		Id INT PRIMARY KEY IDENTITY(1,1),
		Cedula VARCHAR(15),
		Nombre VARCHAR(100),
		Telefono VARCHAR(15),
		Correo NVARCHAR(100),
		FechaNacimiento DATE
	);
GO
	CREATE TABLE Rutinas(
		Id INT PRIMARY KEY IDENTITY(1,1),
		Nombre VARCHAR(100),
		Descripcion VARCHAR(100),
		NivelDificultad VARCHAR(15) CHECK (NivelDificultad IN ('Fácil', 'Intermedio', 'Difícil'))
	);
GO
	CREATE TABLE Clases(
		Id INT PRIMARY KEY IDENTITY(1,1),
		Nombre VARCHAR(100),
		Horario TIME,
		CuposLimites INT,
		Descripcion VARCHAR(100),
		Fecha VARCHAR(10)
    CHECK (Fecha IN ('Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado', 'Domingo'))
	);

	GO
	CREATE TABLE Reservas(
		Id INT PRIMARY KEY IDENTITY(1,1),
		cuposDisponibles INT CHECK (cuposDisponibles >= 0),
		Disponibilidad BIT NOT NULL,
		IdUsuario INT  FOREIGN KEY (IdUsuario) REFERENCES Usuarios(Id),
        IdClase INT FOREIGN KEY (IdClase) REFERENCES Clases(Id)
		);
		GO
INSERT INTO Usuarios (Cedula, Nombre, Telefono, Correo, FechaNacimiento) VALUES
('1001001001', 'Laura Gómez', '3115551234', 'laura@example.com', '1995-04-15'),
('1001001002', 'Carlos Pérez', '3104445678', 'carlos@example.com', '1989-09-10'),
('1001001003', 'Daniela Restrepo', '3123331122', 'daniela@example.com', '2000-01-20'),
('1001001004', 'Juan Torres', '3007778899', 'juan@example.com', '1992-07-05'),
('1001001005', 'María Álvarez', '3016667788', 'maria@example.com', '1997-11-30');
GO
INSERT INTO Rutinas (Nombre, Descripcion, NivelDificultad) VALUES
('Cardio Básico', 'Rutina de calentamiento general', 'Fácil'),
('Fuerza Intermedia', 'Entrenamiento de resistencia con pesas', 'Intermedio'),
('HIIT Avanzado', 'Entrenamiento de intervalos de alta intensidad', 'Difícil'),
('Rutina Full Body', 'Entrenamiento para todo el cuerpo', 'Intermedio'),
('Estiramiento', 'Movilidad y flexibilidad', 'Fácil');
GO
INSERT INTO Clases (Nombre, Fecha, Horario, CuposLimites, Descripcion) VALUES
('Zumba', 'Lunes', '09:00', 15, 'Clase de baile cardiovascular'),
('Crossfit', 'Martes', '11:00', 10, 'Entrenamiento funcional intenso'),
('Yoga', 'Miércoles', '08:00', 12, 'Clase de relajación y estiramiento'),
('Spinning', 'Jueves', '18:00', 20, 'Clase en bicicleta estacionaria'),
('Pilates', 'Viernes', '07:00', 10, 'Entrenamiento de control corporal');

INSERT INTO Reservas (cuposDisponibles, Disponibilidad, IdUsuario, IdClase) VALUES
(10, 1, 1, 1),
(5, 1, 2, 2),
(3, 1, 3, 3),
(0, 0, 4, 4), 
(8, 1, 5, 5);
GO

CREATE PROCEDURE sp_ListarUsuarios
AS
BEGIN
    SELECT * FROM Usuarios
END
GO

CREATE PROCEDURE sp_ObtenerUsuario(
    @Id INT
)
AS
BEGIN
    SELECT * FROM Usuarios WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_GuardarUsuario(
    @Cedula VARCHAR(15),
    @Nombre VARCHAR(100),
    @Telefono VARCHAR(15),
    @Correo NVARCHAR(100),
    @FechaNacimiento DATE
)
AS
BEGIN
    INSERT INTO Usuarios(Cedula, Nombre, Telefono, Correo, FechaNacimiento)
    VALUES (@Cedula, @Nombre, @Telefono, @Correo, @FechaNacimiento)
END
GO

CREATE PROCEDURE sp_EditarUsuario(
    @Id INT,
    @Cedula VARCHAR(15),
    @Nombre VARCHAR(100),
    @Telefono VARCHAR(15),
    @Correo NVARCHAR(100),
    @FechaNacimiento DATE
)
AS
BEGIN
    UPDATE Usuarios SET
        Cedula = @Cedula,
        Nombre = @Nombre,
        Telefono = @Telefono,
        Correo = @Correo,
        FechaNacimiento = @FechaNacimiento
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_EliminarUsuario(
    @Id INT
)
AS
BEGIN
    DELETE FROM Usuarios WHERE Id = @Id
END
GO
CREATE PROCEDURE sp_ListarRutinas
AS
BEGIN
    SELECT * FROM Rutinas
END
GO

CREATE PROCEDURE sp_ObtenerRutina(
    @Id INT
)
AS
BEGIN
    SELECT * FROM Rutinas WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_GuardarRutina(
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(100),
    @NivelDificultad VARCHAR(15)
)
AS
BEGIN
    INSERT INTO Rutinas(Nombre, Descripcion, NivelDificultad)
    VALUES (@Nombre, @Descripcion, @NivelDificultad)
END
GO

CREATE PROCEDURE sp_EditarRutina(
    @Id INT,
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(100),
    @NivelDificultad VARCHAR(15)
)
AS
BEGIN
    UPDATE Rutinas SET
        Nombre = @Nombre,
        Descripcion = @Descripcion,
        NivelDificultad = @NivelDificultad
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_EliminarRutina(
    @Id INT
)
AS
BEGIN
    DELETE FROM Rutinas WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_ListarClases
AS
BEGIN
    SELECT * FROM Clases
END
GO

CREATE PROCEDURE sp_ObtenerClase(
    @Id INT
)
AS
BEGIN
    SELECT * FROM Clases WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_GuardarClase(
    @Nombre VARCHAR(100),
    @Fecha VARCHAR(10),
    @Horario TIME,
    @CuposLimites INT,
    @Descripcion VARCHAR(100)
)
AS
BEGIN
    INSERT INTO Clases (Nombre, Fecha, Horario, CuposLimites, Descripcion)
    VALUES (@Nombre, @Fecha, @Horario, @CuposLimites, @Descripcion)
END
GO


CREATE PROCEDURE sp_EditarClase(
    @Id INT,
    @Nombre VARCHAR(100),
    @Fecha VARCHAR(10),
    @Horario TIME,
    @CuposLimites INT,
    @Descripcion VARCHAR(100)
)
AS
BEGIN
    UPDATE Clases SET
        Nombre = @Nombre,
        Fecha = @Fecha,
        Horario = @Horario,
        CuposLimites = @CuposLimites,
        Descripcion = @Descripcion
    WHERE Id = @Id
END
GO


CREATE PROCEDURE sp_EliminarClase(
    @Id INT
)
AS
BEGIN
    DELETE FROM Clases WHERE Id = @Id
END
GO
-- Listar reservas
CREATE PROCEDURE sp_ListarReservas
AS
BEGIN
    SELECT * FROM Reservas
END
GO

-- Obtener una reserva por ID
CREATE PROCEDURE sp_ObtenerReserva(
    @Id INT
)
AS
BEGIN
    SELECT * FROM Reservas WHERE Id = @Id
END
GO

-- Guardar una nueva reserva
CREATE PROCEDURE sp_GuardarReserva(
    @cuposDisponibles INT,
    @Disponibilidad BIT,
    @IdUsuario INT,
    @IdClase INT
)
AS
BEGIN
    INSERT INTO Reservas(cuposDisponibles, Disponibilidad, IdUsuario, IdClase)
    VALUES (@cuposDisponibles, @Disponibilidad, @IdUsuario, @IdClase)
END
GO

-- Editar una reserva existente
CREATE PROCEDURE sp_EditarReserva(
    @Id INT,
    @cuposDisponibles INT,
    @Disponibilidad BIT,
    @IdUsuario INT,
    @IdClase INT
)
AS
BEGIN
    UPDATE Reservas SET
        cuposDisponibles = @cuposDisponibles,
        Disponibilidad = @Disponibilidad,
        IdUsuario = @IdUsuario,
        IdClase = @IdClase
    WHERE Id = @Id
END
GO

-- Eliminar una reserva
CREATE PROCEDURE sp_EliminarReserva(
    @Id INT
)
AS
BEGIN
    DELETE FROM Reservas WHERE Id = @Id
END
GO
