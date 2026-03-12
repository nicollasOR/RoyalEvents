CREATE DATABASE EventsRoyalOneSir;
GO
USE EventsRoyalOneSir;
GO

CREATE TABLE Evento (
    EventoId INT PRIMARY KEY IDENTITY (1,1),
    Nome NVARCHAR(40) NOT NULL,
    DataEvento DATETIME2(0) NOT NULL,
    Localizacao NVARCHAR(50) NOT NULL
);
GO



CREATE TABLE TipoUsuario
(
TipoUsuarioId INT PRIMARY KEY IDENTITY (1,1),
Tipo_de_Usuario NVARCHAR(40) NOT NULL
)

GO

CREATE TABLE Usuario
(
UsuarioId INT PRIMARY KEY IDENTITY (1,1),
TipoUsuario INT,
Nome NVARCHAR(70) NOT NULL,
Email NVARCHAR(100) NOT NULL UNIQUE,
Senha VARBINARY(32) NOT NULL,
Especialidade NVARCHAR(70) NULL,
CONSTRAINT TipoUsuario_FK FOREIGN KEY (TipoUsuario) REFERENCES TipoUsuario(TipoUsuarioId)
)

GO


CREATE TABLE Inscrição (
    InscriçãoId INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    EventoId INT NOT NULL,
    DataInscrição DATETIME2(0) DEFAULT GETDATE(),
    
    CONSTRAINT FK_Inscricao_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuario(UsuarioId),
    CONSTRAINT FK_Inscricao_Evento FOREIGN KEY (EventoId) REFERENCES Evento(EventoId)
);
GO

INSERT INTO Evento (Nome, DataEvento, Localizacao) VALUES 
('Royal Tech Summit', '2026-05-15 09:00:00', 'Auditório Principal'),
('Workshop SQL Master', '2026-06-20 14:00:00', 'Sala de Treinamento B');

GO

INSERT INTO TipoUsuario
VALUES
('Administrador'),
('Palestrante'),
('Participante')
SELECT * FROM TipoUsuario
GO

INSERT INTO Usuario(TipoUsuario, Email, Senha, Nome, Especialidade)
VALUES
('1', 'shoulds@gmail.com', HASHBYTES('SHA2_256', '123456'), 'Nicollas', 'nenhuma')
SELECT * FROM Usuario



/*

Scaffold-DbContext "Server=Server=(localdb)\\MSSQLLocalDB;Database=EventsRoyalOneSir;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Domains -ContextDir Contexts -UseDatabaseNames -NoPluralize

*/