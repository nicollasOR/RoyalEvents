CREATE DATABASE EventsRoyalOneSir;
GO
USE EventsRoyalOneSir;
GO

CREATE TABLE Evento (
    EventoId INT PRIMARY KEY IDENTITY (1,1),
    Nome NVARCHAR(40) NOT NULL,
    DataEvento DATETIME2(0) NOT NULL,
    Localizacao NVARCHAR(50) NOT NULL,
    StatusEvento BIT DEFAULT 1
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
StatusUsuario BIT DEFAULT 1,
CONSTRAINT TipoUsuario_FK FOREIGN KEY (TipoUsuario) REFERENCES TipoUsuario(TipoUsuarioId)
)

GO


CREATE TABLE Inscricao (
    InscricaoId INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    EventoId INT NOT NULL,
    DataInscricao DATETIME2(0) DEFAULT GETDATE(),
    
    CONSTRAINT FK_Inscricao_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuario(UsuarioId),
    CONSTRAINT FK_Inscricao_Evento FOREIGN KEY (EventoId) REFERENCES Evento(EventoId)
);
GO

INSERT INTO Evento (Nome, DataEvento, Localizacao) VALUES 
('Royal Tech Summit', '2026-05-15 09:00:00', 'Audit�rio Principal'),
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


CREATE TRIGGER trg_exclusaoUsuario
ON Usuario
INSTEAD OF DELETE
AS BEGIN 
    UPDATE usr SET StatusUsuario = 0
    FROM Usuario usr
    INNER JOIN deleted d ON d.UsuarioId = usr.UsuarioId
    END
    GO


CREATE TRIGGER trg_desabilitarEvento
ON Evento
INSTEAD OF DELETE 
 AS BEGIN 
  UPDATE Events SET StatusEvento = 0
  FROM Evento Events 
  INNER JOIN DELETED d on d.EventoId = Events.EventoId
  END 
  GO



/*

Scaffold-DbContext "Server=Server=(localdb)\\MSSQLLocalDB;Database=EventsRoyalOneSir;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Domains -ContextDir Contexts -UseDatabaseNames -NoPluralize

*/