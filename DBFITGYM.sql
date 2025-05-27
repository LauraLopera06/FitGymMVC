CREATE DATABASE FitGym;
GO
USE FitGym;
GO


CREATE TABLE Usuarios(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Cedula VARCHAR(15)UNIQUE,
	Nombre VARCHAR(100),
	Telefono VARCHAR(15),
	Correo NVARCHAR(100)UNIQUE,
	FechaNacimiento DATE,
	TipoUsuario VARCHAR(15) CHECK (TipoUsuario IN ('Cliente','Administrador','Entrenador')),
	Contraseña VARCHAR(100)
);
GO
CREATE TABLE Clientes (
    Id INT PRIMARY KEY, 
    Altura DECIMAL(5,2),
    Peso DECIMAL(5,2),
    PorcentajeGrasa INT,
    EstadoMembresia BIT,
    FOREIGN KEY (Id) REFERENCES Usuarios(Id)
);
GO

CREATE TABLE Rutinas(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(100),
	Descripcion VARCHAR(100),
	NivelDificultad VARCHAR(15) CHECK (NivelDificultad IN ('Fácil', 'Intermedio', 'Difícil'))
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


CREATE TABLE Clases (
	Id INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(100),
	HorarioInicio TIME,
	HorarioFin TIME,
	CuposLimites INT,
	CedulaEntrenador VARCHAR(15),
	Descripcion VARCHAR(100),
	Fecha VARCHAR(10),
	Estado VARCHAR(15) DEFAULT 'Programada', -- Estado por defecto

	CHECK (Fecha IN ('Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado', 'Domingo')),
	CHECK (CuposLimites >= 0),
	CHECK (Estado IN ('Programada', 'EnCurso', 'Cancelada')) -- Validación de estados
);
GO


CREATE TABLE Reservas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT FOREIGN KEY (IdUsuario) REFERENCES Usuarios(Id),
    IdClase INT FOREIGN KEY (IdClase) REFERENCES Clases(Id),
    CONSTRAINT UQ_Reservas_IdUsuario_IdClase UNIQUE (IdUsuario, IdClase) 
);
GO


INSERT INTO Usuarios (Cedula, Nombre, Telefono, Correo, FechaNacimiento, TipoUsuario, Contraseña) VALUES
('1', 'Laura Lopera', '3115551234', 'Lala0604lm@gmail.com', '2004-01-01', 'Cliente', 'password123'),
('2', 'Emanuel Cardona', '3104445678', 'ema795ps4@gmail.com', '2004-01-01', 'Entrenador', 'adminpass456'),
('3', 'Kevin Castaño', '3123331122', 'kevin105066@gmail.com', '2004-09-25', 'Administrador', 'trainer789'),
('1001001004', 'Juan Torres', '3007778899', 'juan@example.com', '1992-07-05', 'Cliente', 'juanpass123'),
('1001001005', 'María Álvarez', '3016667788', 'maria@example.com', '1997-11-30', 'Entrenador', 'mariapass456');
GO

INSERT INTO Clientes (Id, Altura, Peso, PorcentajeGrasa, EstadoMembresia) VALUES
(1, 1.65, 58.5, 22, 1),  
(4, 1.78, 74.2, 18, 1);  
GO


INSERT INTO Rutinas (Nombre, Descripcion, NivelDificultad) VALUES
('Cardio Básico', 'Rutina de calentamiento general', 'Fácil'),
('Fuerza Intermedia', 'Entrenamiento de resistencia con pesas', 'Intermedio'),
('HIIT Avanzado', 'Entrenamiento de intervalos de alta intensidad', 'Difícil'),
('Rutina Full Body', 'Entrenamiento para todo el cuerpo', 'Intermedio'),
('Estiramiento', 'Movilidad y flexibilidad', 'Fácil');
GO

INSERT INTO Ejercicios (Nombre, GrupoMuscular) VALUES
('Sentadillas', 'Piernas'),
('Flexiones', 'Pecho'),
('Burpees', 'Cuerpo Completo'),
('Plancha', 'Abdomen'),
('Press de Banca', 'Pecho'),
('Peso Muerto', 'Espalda'),
('Remo con Barra', 'Espalda'),
('Curl de Bíceps', 'Brazos'),
('Extensión de Tríceps', 'Brazos'),
('Zancadas', 'Piernas');
GO

INSERT INTO RutinaEjercicio (IdRutina, IdEjercicio) VALUES
(1, 3), -- Cardio Básico incluye Burpees
(1, 4), -- Cardio Básico incluye Plancha
(2, 5), -- Fuerza Intermedia incluye Press de Banca
(2, 6), -- Fuerza Intermedia incluye Peso Muerto
(2, 7), -- Fuerza Intermedia incluye Remo con Barra
(3, 3), -- HIIT Avanzado incluye Burpees
(3, 1), -- HIIT Avanzado incluye Sentadillas
(4, 1), -- Full Body incluye Sentadillas
(4, 2), -- Full Body incluye Flexiones
(4, 8), -- Full Body incluye Curl de Bíceps
(5, 10); -- Estiramiento incluye Zancadas
GO


INSERT INTO Clases (Nombre, Fecha, HorarioInicio, HorarioFin, CuposLimites, Descripcion, CedulaEntrenador, Estado) 
VALUES 
('Zumba', 'Lunes', '09:00', '10:00', 2, 'Clase de baile cardiovascular', '1001001005', 'Programada'),
('Crossfit', 'Martes', '11:00', '12:00', 10, 'Entrenamiento funcional intenso', '2', 'Programada'),
('Yoga', 'Miércoles', '08:00', '09:00', 12, 'Clase de relajación y estiramiento', '1001001005', 'Programada'),
('Spinning', 'Jueves', '18:00', '19:00', 20, 'Clase en bicicleta estacionaria', '2', 'Programada'),
('Pilates', 'Viernes', '10:05', '12:00', 10, 'Entrenamiento de control corporal', '2', 'Programada');


INSERT INTO Reservas (IdUsuario, IdClase) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);
GO
------------
--USUARIOS--
------------
CREATE PROCEDURE sp_ListarUsuarios
AS
SELECT Id, Cedula, Nombre, Telefono, Correo, FechaNacimiento, TipoUsuario
FROM Usuarios
GO

CREATE PROCEDURE sp_ObtenerUsuario(@Id INT) AS SELECT * FROM Usuarios WHERE Id = @Id
GO

CREATE PROCEDURE sp_BuscarUsuarioPorCedula
    @Cedula NVARCHAR(50)
AS
BEGIN
    SELECT Id, Cedula, Nombre, Telefono, Correo, FechaNacimiento, TipoUsuario
    FROM Usuarios
    WHERE Cedula = @Cedula;
END
GO

CREATE PROCEDURE sp_GuardarUsuario
(
    @Cedula VARCHAR(15),
    @Nombre VARCHAR(100),
    @Telefono VARCHAR(15),
    @Correo NVARCHAR(100),
    @FechaNacimiento DATE,
    @Contraseña VARCHAR(100)
)
AS
BEGIN
    INSERT INTO Usuarios(Cedula, Nombre, Telefono, Correo, FechaNacimiento, TipoUsuario, Contraseña)
    VALUES (@Cedula, @Nombre, @Telefono, @Correo, @FechaNacimiento, 'Cliente', @Contraseña)

    DECLARE @NuevoId INT = SCOPE_IDENTITY() --devuelve el Id autogenerado por el último INSERT

    INSERT INTO Clientes(Id, Altura, Peso, PorcentajeGrasa, EstadoMembresia)
    VALUES (@NuevoId, NULL, NULL, NULL, NULL)
END
GO

CREATE PROCEDURE sp_EditarUsuario
    @Id INT,
    @Cedula VARCHAR(15),
    @Nombre VARCHAR(100),
    @Telefono VARCHAR(15),
    @Correo NVARCHAR(100),
    @FechaNacimiento DATE,
    @TipoUsuario VARCHAR(50)
AS
BEGIN
    UPDATE Usuarios
    SET Cedula = @Cedula,
        Nombre = @Nombre,
        Telefono = @Telefono,
        Correo = @Correo,
        FechaNacimiento = @FechaNacimiento,
        TipoUsuario = @TipoUsuario
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_EliminarUsuario(@Id INT) AS DELETE FROM Usuarios WHERE Id = @Id
GO

CREATE PROCEDURE sp_ValidarUsuario --para el login
    @Correo NVARCHAR(100),
    @Contraseña NVARCHAR(100) 
AS
BEGIN
    SELECT Id, Correo, TipoUsuario
    FROM Usuarios
    WHERE Correo = @Correo AND Contraseña = @Contraseña; 
END
GO

CREATE PROCEDURE sp_AgregarCliente
    @Id INT
AS
BEGIN
    INSERT INTO Clientes (Id, Altura, Peso, PorcentajeGrasa, EstadoMembresia)
    VALUES (@Id, 0.0, 0.0, 0, 0)
END
GO


CREATE PROCEDURE sp_EliminarCliente(@Id INT)--para el cambio de rol
AS
BEGIN
    DELETE FROM Clientes
    WHERE Id = @Id;
END
GO


-----------
--RUTINAS--
-----------
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

--------------
--EJERCICIOS--
--------------
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

------------------------
--RUTINAS X EJERCICIOS--
------------------------
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

CREATE PROCEDURE sp_ListarRutinasConEjercicios
AS
BEGIN
    SELECT 
        R.Id AS IdRutina,
        R.Nombre AS NombreRutina,
        R.Descripcion,
        R.NivelDificultad,
        E.Nombre AS NombreEjercicio
    FROM Rutinas R
    INNER JOIN RutinaEjercicio RE ON R.Id = RE.IdRutina
    INNER JOIN Ejercicios E ON RE.IdEjercicio = E.Id
    ORDER BY R.Id;
END;
GO

----------
--CLASES-- 
----------
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

CREATE PROCEDURE sp_GuardarClase(
	@Nombre VARCHAR(100), 
	@Fecha VARCHAR(10), 
	@HorarioInicio TIME, 
	@HorarioFin TIME, 
	@CuposLimites INT, 
	@Descripcion VARCHAR(100),
	@CedulaEntrenador VARCHAR(15)
)
AS
BEGIN
	INSERT INTO Clases (Nombre, Fecha, HorarioInicio, HorarioFin, CuposLimites, Descripcion, CedulaEntrenador, Estado)
	VALUES (@Nombre, @Fecha, @HorarioInicio, @HorarioFin, @CuposLimites, @Descripcion, @CedulaEntrenador, 'Programada')
END
GO

CREATE PROCEDURE sp_EditarClase(
	@Id INT, 
	@Nombre VARCHAR(100), 
	@Fecha VARCHAR(10), 
	@HorarioInicio TIME, 
	@HorarioFin TIME, 
	@CuposLimites INT, 
	@Descripcion VARCHAR(100),
	@CedulaEntrenador VARCHAR(15)
)
AS
BEGIN
	UPDATE Clases 
	SET Nombre = @Nombre, 
		Fecha = @Fecha, 
		HorarioInicio = @HorarioInicio, 
		HorarioFin = @HorarioFin, 
		CuposLimites = @CuposLimites, 
		Descripcion = @Descripcion,
		CedulaEntrenador = @CedulaEntrenador
	WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_CambiarEstadoClase
    @Id INT,
    @NuevoEstado VARCHAR(15)
AS
BEGIN
    UPDATE Clases
    SET Estado = @NuevoEstado
    WHERE Id = @Id;
END
GO


CREATE PROCEDURE sp_EliminarClase(@Id INT) AS DELETE FROM Clases WHERE Id = @Id
GO

------------
--RESERVAS--
------------
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


