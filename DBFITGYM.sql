CREATE DATABASE FitGym;
GO
USE FitGym;
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
	NivelDificultad VARCHAR(15) CHECK (NivelDificultad IN ('F�cil', 'Intermedio', 'Dif�cil'))
);
GO
CREATE TABLE Ejercicios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(255) NOT NULL,
    GrupoMuscular VARCHAR(255) NOT NULL
);
GO

CREATE TABLE RutinaEjercicio (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdRutina INT NOT NULL,
    IdEjercicio INT NOT NULL,
    FOREIGN KEY (IdRutina) REFERENCES Rutinas(Id),
    FOREIGN KEY (IdEjercicio) REFERENCES Ejercicios(Id)
);
GO


CREATE TABLE Clases(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(100),
	HorarioInicio TIME,
	HorarioFin TIME,
	CuposLimites INT,
	Descripcion VARCHAR(100),
	Fecha VARCHAR(10)
	CHECK (Fecha IN ('Lunes', 'Martes', 'Mi�rcoles', 'Jueves', 'Viernes', 'S�bado', 'Domingo')),
	CHECK (CuposLimites >= 0)
);
GO

CREATE TABLE Reservas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT FOREIGN KEY (IdUsuario) REFERENCES Usuarios(Id),
    IdClase INT FOREIGN KEY (IdClase) REFERENCES Clases(Id),
    CONSTRAINT UQ_Reservas_IdUsuario_IdClase UNIQUE (IdUsuario, IdClase) 
);
GO




INSERT INTO Usuarios (Cedula, Nombre, Telefono, Correo, FechaNacimiento) VALUES
('1001001001', 'Laura G�mez', '3115551234', 'laura@example.com', '1995-04-15'),
('1001001002', 'Carlos P�rez', '3104445678', 'carlos@example.com', '1989-09-10'),
('1001001003', 'Daniela Restrepo', '3123331122', 'daniela@example.com', '2000-01-20'),
('1001001004', 'Juan Torres', '3007778899', 'juan@example.com', '1992-07-05'),
('1001001005', 'Mar�a �lvarez', '3016667788', 'maria@example.com', '1997-11-30');
GO

INSERT INTO Rutinas (Nombre, Descripcion, NivelDificultad) VALUES
('Cardio B�sico', 'Rutina de calentamiento general', 'F�cil'),
('Fuerza Intermedia', 'Entrenamiento de resistencia con pesas', 'Intermedio'),
('HIIT Avanzado', 'Entrenamiento de intervalos de alta intensidad', 'Dif�cil'),
('Rutina Full Body', 'Entrenamiento para todo el cuerpo', 'Intermedio'),
('Estiramiento', 'Movilidad y flexibilidad', 'F�cil');
GO

INSERT INTO Ejercicios (Nombre, GrupoMuscular) VALUES
('Sentadillas', 'Piernas'),
('Flexiones', 'Pecho'),
('Burpees', 'Cuerpo Completo'),
('Plancha', 'Abdomen'),
('Press de Banca', 'Pecho'),
('Peso Muerto', 'Espalda'),
('Remo con Barra', 'Espalda'),
('Curl de B�ceps', 'Brazos'),
('Extensi�n de Tr�ceps', 'Brazos'),
('Zancadas', 'Piernas');
GO

INSERT INTO RutinaEjercicio (IdRutina, IdEjercicio) VALUES
(1, 3), -- Cardio B�sico incluye Burpees
(1, 4), -- Cardio B�sico incluye Plancha
(2, 5), -- Fuerza Intermedia incluye Press de Banca
(2, 6), -- Fuerza Intermedia incluye Peso Muerto
(2, 7), -- Fuerza Intermedia incluye Remo con Barra
(3, 3), -- HIIT Avanzado incluye Burpees
(3, 1), -- HIIT Avanzado incluye Sentadillas
(4, 1), -- Full Body incluye Sentadillas
(4, 2), -- Full Body incluye Flexiones
(4, 8), -- Full Body incluye Curl de B�ceps
(5, 10); -- Estiramiento incluye Zancadas
GO


INSERT INTO Clases (Nombre, Fecha, HorarioInicio, HorarioFin, CuposLimites, Descripcion) VALUES
('Zumba', 'Lunes', '09:00', '10:00', 2, 'Clase de baile cardiovascular'),
('Crossfit', 'Martes', '11:00', '12:00', 10, 'Entrenamiento funcional intenso'),
('Yoga', 'Mi�rcoles', '08:00', '09:00', 12, 'Clase de relajaci�n y estiramiento'),
('Spinning', 'Jueves', '18:00', '19:00', 20, 'Clase en bicicleta estacionaria'),
('Pilates', 'Viernes', '07:00', '08:00', 10, 'Entrenamiento de control corporal');
GO

INSERT INTO Reservas (IdUsuario, IdClase) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);
GO


CREATE PROCEDURE sp_ListarUsuarios AS SELECT * FROM Usuarios
GO
CREATE PROCEDURE sp_ObtenerUsuario(@Id INT) AS SELECT * FROM Usuarios WHERE Id = @Id
GO
CREATE PROCEDURE sp_BuscarUsuarioPorCedula
    @Cedula NVARCHAR(50)
AS
BEGIN
    SELECT * 
    FROM Usuarios
    WHERE Cedula = @Cedula;
END
GO

CREATE PROCEDURE sp_GuardarUsuario(@Cedula VARCHAR(15), @Nombre VARCHAR(100), @Telefono VARCHAR(15), @Correo NVARCHAR(100), @FechaNacimiento DATE)
AS
BEGIN
	INSERT INTO Usuarios(Cedula, Nombre, Telefono, Correo, FechaNacimiento)
	VALUES (@Cedula, @Nombre, @Telefono, @Correo, @FechaNacimiento)
END
GO
CREATE PROCEDURE sp_EditarUsuario(@Id INT, @Cedula VARCHAR(15), @Nombre VARCHAR(100), @Telefono VARCHAR(15), @Correo NVARCHAR(100), @FechaNacimiento DATE)
AS
BEGIN
	UPDATE Usuarios SET Cedula = @Cedula, Nombre = @Nombre, Telefono = @Telefono, Correo = @Correo, FechaNacimiento = @FechaNacimiento WHERE Id = @Id
END
GO
CREATE PROCEDURE sp_EliminarUsuario(@Id INT) AS DELETE FROM Usuarios WHERE Id = @Id
GO


CREATE PROCEDURE sp_ListarRutinas AS SELECT * FROM Rutinas
GO
CREATE PROCEDURE sp_ObtenerRutina(@Id INT) AS SELECT * FROM Rutinas WHERE Id = @Id
GO
CREATE PROCEDURE sp_GuardarRutina(@Nombre VARCHAR(100), @Descripcion VARCHAR(100), @NivelDificultad VARCHAR(15))
AS
BEGIN
	INSERT INTO Rutinas(Nombre, Descripcion, NivelDificultad)
	VALUES (@Nombre, @Descripcion, @NivelDificultad)
	SELECT SCOPE_IDENTITY(); -- Retornar el ID nuevo (para el tema de ejercicios)
END
GO
CREATE PROCEDURE sp_EditarRutina(@Id INT, @Nombre VARCHAR(100), @Descripcion VARCHAR(100), @NivelDificultad VARCHAR(15))
AS
BEGIN
	UPDATE Rutinas SET Nombre = @Nombre, Descripcion = @Descripcion, NivelDificultad = @NivelDificultad WHERE Id = @Id
END
GO
CREATE PROCEDURE sp_EliminarRutina(@Id INT) AS DELETE FROM Rutinas WHERE Id = @Id
GO


CREATE PROCEDURE sp_ListarEjercicios
AS
SELECT * FROM Ejercicios
GO
CREATE PROCEDURE sp_ObtenerEjercicio
    @Id INT
AS
SELECT * FROM Ejercicios WHERE Id = @Id
GO

CREATE PROCEDURE sp_GuardarEjercicio
    @Nombre VARCHAR(255),
    @GrupoMuscular VARCHAR(255)
AS
BEGIN
    INSERT INTO Ejercicios (Nombre, GrupoMuscular)
    VALUES (@Nombre, @GrupoMuscular)
END
GO

CREATE PROCEDURE sp_EditarEjercicio
    @Id INT,
    @Nombre VARCHAR(255),
    @GrupoMuscular VARCHAR(255)
AS
BEGIN
    UPDATE Ejercicios
    SET Nombre = @Nombre,
        GrupoMuscular = @GrupoMuscular
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_EliminarEjercicio
    @Id INT
AS
DELETE FROM Ejercicios WHERE Id = @Id
GO

CREATE PROCEDURE sp_ListarRutinaEjercicios
AS
SELECT * FROM RutinaEjercicio
GO

CREATE PROCEDURE sp_ObtenerRutinaEjercicio
    @Id INT
AS
SELECT * FROM RutinaEjercicio WHERE Id = @Id
GO

CREATE PROCEDURE sp_GuardarRutinaEjercicio
    @IdRutina INT,
    @IdEjercicio INT
AS
BEGIN
    INSERT INTO RutinaEjercicio (IdRutina, IdEjercicio)
    VALUES (@IdRutina, @IdEjercicio)
END
GO

CREATE PROCEDURE sp_EditarRutinaEjercicio
    @Id INT,
    @IdRutina INT,
    @IdEjercicio INT
AS
BEGIN
    UPDATE RutinaEjercicio
    SET IdRutina = @IdRutina,
        IdEjercicio = @IdEjercicio
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_EliminarRutinaEjercicio
    @Id INT
AS
DELETE FROM RutinaEjercicio WHERE Id = @Id
GO


CREATE PROCEDURE sp_ListarClases AS SELECT * FROM Clases
GO
CREATE PROCEDURE sp_ObtenerClase(@Id INT) AS SELECT * FROM Clases WHERE Id = @Id
GO

CREATE PROCEDURE sp_BuscarClasePorNombre
    @Nombre NVARCHAR(100)
AS
BEGIN
    SELECT * 
    FROM Clases
    WHERE Nombre = @Nombre;
END
GO

CREATE PROCEDURE sp_GuardarClase(@Nombre VARCHAR(100), @Fecha VARCHAR(10), @HorarioInicio TIME, @HorarioFin TIME, @CuposLimites INT, @Descripcion VARCHAR(100))
AS
BEGIN
	INSERT INTO Clases (Nombre, Fecha, HorarioInicio, HorarioFin, CuposLimites, Descripcion)
	VALUES (@Nombre, @Fecha, @HorarioInicio, @HorarioFin, @CuposLimites, @Descripcion)
END
GO
CREATE PROCEDURE sp_EditarClase(@Id INT, @Nombre VARCHAR(100), @Fecha VARCHAR(10), @HorarioInicio TIME, @HorarioFin TIME, @CuposLimites INT, @Descripcion VARCHAR(100))
AS
BEGIN
	UPDATE Clases SET Nombre = @Nombre, Fecha = @Fecha, HorarioInicio = @HorarioInicio, HorarioFin = @HorarioFin, CuposLimites = @CuposLimites, Descripcion = @Descripcion WHERE Id = @Id
END
GO
CREATE PROCEDURE sp_EliminarClase(@Id INT) AS DELETE FROM Clases WHERE Id = @Id
GO


CREATE PROCEDURE sp_ListarReservas AS SELECT * FROM Reservas
GO
CREATE PROCEDURE sp_ObtenerReserva(@Id INT) AS SELECT * FROM Reservas WHERE Id = @Id
GO
CREATE PROCEDURE sp_GuardarReserva(@IdUsuario INT, @IdClase INT)
AS
BEGIN
	INSERT INTO Reservas(IdUsuario, IdClase)
	VALUES (@IdUsuario, @IdClase)
	UPDATE Clases
        SET CuposLimites = CuposLimites-1
        WHERE Id = @IdClase;
END
GO
CREATE PROCEDURE sp_EditarReserva(@Id INT, @IdUsuario INT, @IdClase INT)
AS
BEGIN
	UPDATE Reservas SET IdUsuario = @IdUsuario, IdClase = @IdClase WHERE Id = @Id
END
GO
CREATE PROCEDURE sp_EliminarReserva(@Id INT) AS DELETE FROM Reservas WHERE Id = @Id
GO


select * from Clases

SELECT 
  Id,
  Nombre,
  CuposLimites,
  Fecha,
  [HorarioInicio],
  [HorarioFin],
  Descripcion
FROM 
  Clases


