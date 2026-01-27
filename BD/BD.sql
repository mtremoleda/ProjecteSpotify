CREATE DATABASE Spotify;
USE Spotify;


CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    nom VARCHAR(50) NOT NULL,
    contrasenya VARCHAR(255) NOT NULL,
    salt VARCHAR(5) NOT NULL

);

CREATE TABLE Songs (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    titol VARCHAR(100) NOT NULL,
    artista VARCHAR(100),
    album VARCHAR(100),
    durada Decimal(6,2)

);

CREATE TABLE Files_quality(
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    id_song UNIQUEIDENTIFIER  NOT NULL,
    format VARCHAR(10),
    bitrate INT,
    mida DECIMAL(6,2),
    FOREIGN KEY (id_song) REFERENCES Songs(Id)
        
 
);

CREATE TABLE Playlist(
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    id_user UNIQUEIDENTIFIER  NOT NULL,
    nom VARCHAR(100) NOT NULL,
    FOREIGN KEY (id_user) REFERENCES Users(Id)
        
);

CREATE TABLE Playlist_song (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    id_song UNIQUEIDENTIFIER  NOT NULL,
    id_playlist UNIQUEIDENTIFIER  NOT NULL,
    FOREIGN KEY (id_playlist) REFERENCES Playlist(Id),
    FOREIGN KEY (id_song) REFERENCES Songs(Id)
        
);

CREATE TABLE Rols (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Codi NVARCHAR(5) NOT NULL,
    Nom NVARCHAR(50) NOT NULL,
    Descripcio NVARCHAR(255) NULL
);

CREATE TABLE Permisos (
    Id  UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Codi NVARCHAR(10) NOT NULL,
    Nom NVARCHAR(100) NOT NULL,
    Descripcio NVARCHAR(255) NULL
);

CREATE TABLE RolPermisos (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    RolId UNIQUEIDENTIFIER NOT NULL,
    PermisosId UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY (RolId) REFERENCES Rols(Id),
    FOREIGN KEY (PermisosId) REFERENCES Permisos(Id)
);

CREATE TABLE UsersRols (
	Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	UserId UNIQUEIDENTIFIER NOT NULL,
    RolId UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (RolId) REFERENCES Rols(Id)

);




INSERT INTO Users (Id, nom, contrasenya, salt) VALUES
('7A1F6C93-3C41-4E9A-B1E7-2A8BD12A5C01', 'marc', 'hash123456', 'A1B2C'),
('3C9B7720-4046-4D4E-A7C1-57FE8730B221', 'laia', 'pass987654', 'Q9W8E'),
('991CE034-1E72-4C73-9D25-65A7F98F0B10', 'jordi', 'secure5234', 'Z7X6C'),
('E5B42A01-9C27-4FB2-905D-7C6A18F0A010', 'maria', 'pw12345', 'T6R4S'),
('5A9D103F-A1D4-4AE3-8DA7-6F1E93BF1234', 'anna', 'key56789', 'M9N8B');


INSERT INTO Songs (Id, titol, artista, album, durada) VALUES
('111F6C93-AAAA-4E9A-B1E7-2A8BD12A5C01', 'Blinding Lights', 'The Weeknd', 'After Hours', 3.20),
('222B7720-BBBB-4D4E-A7C1-57FE8730B221', 'Shape of You', 'Ed Sheeran', 'Divide', 4.10),
('333CE034-CCCC-4C73-9D25-65A7F98F0B10', 'Bad Guy', 'Billie Eilish', 'When We All Fall Asleep', 3.14),
('44442A01-DDDD-4FB2-905D-7C6A18F0A010', 'Levitating', 'Dua Lipa', 'Future Nostalgia', 3.40),
('555D103F-EEEE-4AE3-8DA7-6F1E93BF1234', 'Yellow', 'Coldplay', 'Parachutes', 4.05);


INSERT INTO Files_quality (Id, id_song, format, bitrate, mida) VALUES
('A1A1A1A1-1111-4AAA-8888-ABCDEF000001', '111F6C93-AAAA-4E9A-B1E7-2A8BD12A5C01', 'mp3', 320, 8.50),
('A2A2A2A2-2222-4BBB-9999-ABCDEF000002', '222B7720-BBBB-4D4E-A7C1-57FE8730B221', 'flac', 1000, 25.30),
('A3A3A3A3-3333-4CCC-AAAA-ABCDEF000003', '333CE034-CCCC-4C73-9D25-65A7F98F0B10', 'mp3', 256, 7.90),
('A4A4A4A4-4444-4DDD-BBBB-ABCDEF000004', '44442A01-DDDD-4FB2-905D-7C6A18F0A010', 'aac', 320, 9.10),
('A5A5A5A5-5555-4EEE-CCCC-ABCDEF000005', '555D103F-EEEE-4AE3-8DA7-6F1E93BF1234', 'wav', 1400, 30.00);


INSERT INTO Playlist (Id, id_user, nom) VALUES
('10AA10AA-AAAA-4444-8222-AABBCCDDEE01', '7A1F6C93-3C41-4E9A-B1E7-2A8BD12A5C01', 'Favorits Marc'),
('20BB20BB-BBBB-5555-8333-AABBCCDDEE02', '3C9B7720-4046-4D4E-A7C1-57FE8730B221', 'Top Hits Laia'),
('30CC30CC-CCCC-6666-8444-AABBCCDDEE03', '991CE034-1E72-4C73-9D25-65A7F98F0B10', 'Relax Jordi'),
('40DD40DD-DDDD-7777-8555-AABBCCDDEE04', 'E5B42A01-9C27-4FB2-905D-7C6A18F0A010', 'Party Maria'),
('50EE50EE-EEEE-8888-8666-AABBCCDDEE05', '5A9D103F-A1D4-4AE3-8DA7-6F1E93BF1234', 'Acústiques Anna');



INSERT INTO Playlist_song (Id, id_song, id_playlist) VALUES
('A10F6C93-1000-4E9A-B1E7-2A8BD12A50001', '111F6C93-AAAA-4E9A-B1E7-2A8BD12A5C01', '10AA10AA-AAAA-4444-8222-AABBCCDDEE01'),
('B20F6C93-2000-4E9A-B1E7-2A8BD12A50002', '222B7720-BBBB-4D4E-A7C1-57FE8730B221', '20BB20BB-BBBB-5555-8333-AABBCCDDEE02'),
('C30F6C93-3000-4E9A-B1E7-2A8BD12A50003', '333CE034-CCCC-4C73-9D25-65A7F98F0B10', '30CC30CC-CCCC-6666-8444-AABBCCDDEE03'),
('D40F6C93-4000-4E9A-B1E7-2A8BD12A50004', '44442A01-DDDD-4FB2-905D-7C6A18F0A010', '40DD40DD-DDDD-7777-8555-AABBCCDDEE04'),
('E50F6C93-5000-4E9A-B1E7-2A8BD12A50005', '555D103F-EEEE-4AE3-8DA7-6F1E93BF1234', '50EE50EE-EEEE-8888-8666-AABBCCDDEE05');



INSERT INTO Rols (Id, Codi, Nom, Descripcio) VALUES
('a7c1e3b2-8e47-4a91-b7f1-2241ea0c1fc1', 'ADM', 'Administrador', 'Control total del sistema'),
('b3f9c12e-5cbe-4389-9c13-4d7e3f76a932', 'EDT', 'Editor', 'Gestió completa del catàleg musical'),
('c8d2a1af-7e19-4784-a4e1-e216a7c43c72', 'ART', 'Artista', 'Gestió del propi contingut musical'),
('d21b84c4-9c55-441a-8e8f-9a7cd2f6b112', 'USR', 'Usuari', 'Oient de la plataforma'),
('ec7f93e3-2da3-45fd-87f8-07b382e92051', 'MOD', 'Moderador', 'Supervisió de comentaris i comunitat');





INSERT INTO Permisos (Id, Codi, Nom, Descripcio) VALUES
-- ADMINISTRADOR
('f81d4fae-7dec-11d0-a765-00a0c91e6bf6', 'UC', 'Crear usuaris', 'Permet crear nous usuaris'),
('f81d4faf-7dec-11d0-a765-00a0c91e6bf6', 'UE', 'Editar usuaris', 'Permet editar usuaris existents'),
('f81d4fb0-7dec-11d0-a765-00a0c91e6bf6', 'UD', 'Eliminar usuaris', 'Permet eliminar usuaris'),
('f81d4fb1-7dec-11d0-a765-00a0c91e6bf6', 'RA', 'Assignar rols', 'Permet assignar rols als usuaris'),
('f81d4fb2-7dec-11d0-a765-00a0c91e6bf6', 'SV', 'Veure estadístiques', 'Accés a estadístiques generals'),

-- EDITOR / GESTOR DE CONTINGUT
('f81d4fb3-7dec-11d0-a765-00a0c91e6bf6', 'SC', 'Crear cançons', 'Afegir noves cançons'),
('f81d4fb4-7dec-11d0-a765-00a0c91e6bf6', 'SE', 'Editar cançons', 'Modificar cançons existents'),
('f81d4fb5-7dec-11d0-a765-00a0c91e6bf6', 'SD', 'Eliminar cançons', 'Eliminar cançons inapropiades'),
('f81d4fb6-7dec-11d0-a765-00a0c91e6bf6', 'AC', 'Crear artistes', 'Afegir artistes'),
('f81d4fb7-7dec-11d0-a765-00a0c91e6bf6', 'AE', 'Editar artistes', 'Modificar artistes existents'),
('f81d4fb8-7dec-11d0-a765-00a0c91e6bf6', 'AM', 'Gestionar àlbums', 'Afegir o editar àlbums i llistes oficials'),

-- ARTISTA
('f81d4fb9-7dec-11d0-a765-00a0c91e6bf6', 'SOC', 'Crear cançons pròpies', 'Afegir les seves pròpies cançons'),
('f81d4fba-7dec-11d0-a765-00a0c91e6bf6', 'SOE', 'Editar cançons pròpies', 'Modificar les seves pròpies cançons'),
('f81d4fbb-7dec-11d0-a765-00a0c91e6bf6', 'AMO', 'Gestionar àlbums propis', 'Afegir o editar els seus àlbums propis'),
('f81d4fbc-7dec-11d0-a765-00a0c91e6bf6', 'SVP', 'Veure estadístiques pròpies', 'Veure nombre d’escoltes, likes, etc.'),

-- USUARI
('f81d4fbd-7dec-11d0-a765-00a0c91e6bf6', 'SL', 'Escoltar cançons', 'Escoltar cançons de la plataforma'),
('f81d4fbe-7dec-11d0-a765-00a0c91e6bf6', 'PC', 'Crear playlists', 'Crear i gestionar playlists pròpies'),
('f81d4fbf-7dec-11d0-a765-00a0c91e6bf6', 'PE', 'Editar playlists', 'Modificar playlists pròpies'),
('f81d4fc0-7dec-11d0-a765-00a0c91e6bf6', 'SR', 'Valorar cançons', 'Puntuar cançons'),
('f81d4fc1-7dec-11d0-a765-00a0c91e6bf6', 'AF', 'Seguir artistes', 'Seguir artistes favorits'),
('f81d4fc2-7dec-11d0-a765-00a0c91e6bf6', 'CA', 'Comentar cançons', 'Deixar comentaris a cançons'),

-- MODERADOR
('f81d4fc3-7dec-11d0-a765-00a0c91e6bf6', 'CD', 'Eliminar comentaris', 'Eliminar comentaris inapropiats'),
('f81d4fc4-7dec-11d0-a765-00a0c91e6bf6', 'US', 'Suspensió temporal', 'Suspensió temporal d’usuaris per mal ús');

ALTER TABLE Users
ALTER COLUMN salt NVARCHAR(100);

CREATE TABLE URLSongs (
	Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	id_song UNIQUEIDENTIFIER  NOT NULL,
	url NVARCHAR(255) NOT NULL,
	FOREIGN KEY (id_song) REFERENCES Songs(Id)
);
