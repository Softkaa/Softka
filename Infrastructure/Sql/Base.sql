--Table Users
CREATE TABLE Users(
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Names VARCHAR(45) NOT NULL,
    LastNames VARCHAR(45) NOT NULL,
    TypeDocument ENUM("CC", "CE", "TI", "Passport") NOT NULL,
    Document VARCHAR(20) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    Age INT NOT NULL,
    Password VARCHAR(150) UNIQUE,
    DateRegister DATE
);

--Insert information table Users
INSERT INTO Users (Names, LastNames, TypeDocument, Document, Email, Age, Password, DateRegister)
VALUES 
('Juan', 'Perez', 'CC', '12345678', 'juan.perez@example.com', 30, 'password1', '2023-01-01'),
('Maria', 'Lopez', 'CE', '87654321', 'maria.lopez@example.com', 25, 'password2', '2023-02-01'),
('Carlos', 'Gomez', 'TI', '11223344', 'carlos.gomez@example.com', 28, 'password3', '2023-03-01'),
('Ana', 'Martinez', 'Passport', '99887766', 'ana.martinez@example.com', 32, 'password4', '2023-04-01'),
('Luis', 'Garcia', 'CC', '55667788', 'luis.garcia@example.com', 27, 'password5', '2023-05-01');

--Table Authentications
CREATE TABLE Authentications(
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Token TEXT NOT NULL,
    RefreshToken TEXT NOT NULL,
    IssueDate DATE NOT NULL,
    ExpirationDate DATE NOT NULL,
    IsActive BOOLEAN NOT NULL,
    IsRefreshed BOOLEAN DEFAULT FALSE,
    UserId INT,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

--Insert information table Authentications
INSERT INTO Authentications (Token, RefreshToken, IssueDate, ExpirationDate, IsActive, UserId)
VALUES 
('jwtToken1', 'refreshToken1', '2023-01-01', '2024-01-01', TRUE, 1),
('jwtToken2', 'refreshToken2', '2023-02-01', '2024-02-01', TRUE, 2),
('jwtToken3', 'refreshToken3', '2023-03-01', '2024-03-01', TRUE, 3),
('jwtToken4', 'refreshToken4', '2023-04-01', '2024-04-01', TRUE, 4),
('jwtToken5', 'refreshToken5', '2023-05-01', '2024-05-01', TRUE, 5);

--Table Roles
CREATE TABLE Roles(
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name ENUM("Admin", "User") NOT NULL
);

--Insert information table Roles
INSERT INTO Roles (Name) VALUES ('Admin'), ('User');

--Table UserRoles
CREATE TABLE UserRoles(
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT,
    RoleId INT,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);

--Insert information table UserRoles
INSERT INTO UserRoles (UserId, RoleId)
VALUES 
(1, 1), (2, 2), (3, 2), (4, 2), (5, 2);

--Table Curriculums
CREATE TABLE Curriculums(
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Age INT NOT NULL,
    Nationality VARCHAR(45) NOT NULL,
    Photo TEXT NOT NULL,
    CivilStatus ENUM("soltero", "casado", "viudo") NOT NULL,
    Phone VARCHAR(20) NOT NULL,
    UserId INT,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

--Insert information table Curriculums
INSERT INTO Curriculums (Age, Nationality, Photo, CivilStatus, Phone, UserId)
VALUES 
(30, 'Colombiana', 'photo1.jpg', 'soltero', '3001234567', 1),
(25, 'Colombiana', 'photo2.jpg', 'casado', '3007654321', 2),
(28, 'Colombiana', 'photo3.jpg', 'viudo', '3009876543', 3),
(32, 'Colombiana', 'photo4.jpg', 'soltero', '3005678901', 4),
(27, 'Colombiana', 'photo5.jpg', 'casado', '3006789012', 5);

--Table Skills
CREATE TABLE Skills(
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name TEXT NOT NULL,
    Description TEXT NOT NULL,
    CurriculumsId INT,
    FOREIGN KEY (CurriculumsId) REFERENCES Curriculums(Id)
);

--Insert information table Skills
INSERT INTO Skills (Name, Description, CurriculumsId)
VALUES 
('Python', 'Lenguaje de programación', 1),
('JavaScript', 'Lenguaje de programación', 2),
('HTML', 'Lenguaje de marcado', 3),
('CSS', 'Lenguaje de estilos', 4),
('SQL', 'Lenguaje de bases de datos', 5);

--Table Educations
CREATE TABLE Educations(
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Institution VARCHAR(100) NOT NULL,
    EducationalTitle VARCHAR(100) NOT NULL,
    YearStart DATE,
    YearEnd DATE,
    CurriculumsId INT,
    FOREIGN KEY (CurriculumsId) REFERENCES Curriculums(Id)
);

--Insert information table Educations
INSERT INTO Educations (Institution, EducationalTitle, YearStart, YearEnd, CurriculumsId)
VALUES 
('Universidad Nacional', 'Ingeniería de Sistemas', '2010-01-01', '2015-01-01', 1),
('Universidad de los Andes', 'Ingeniería de Software', '2011-01-01', '2016-01-01', 2),
('Universidad Javeriana', 'Ingeniería Informática', '2012-01-01', '2017-01-01', 3),
('Universidad del Rosario', 'Ingeniería Electrónica', '2013-01-01', '2018-01-01', 4),
('Universidad EAFIT', 'Ingeniería de Telecomunicaciones', '2014-01-01', '2019-01-01', 5);

--Table WorkExperiences
CREATE TABLE WorkExperiences(
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Company VARCHAR(100),
    Year DATE,
    Description TEXT,
    CurriculumsId INT,
    FOREIGN KEY (CurriculumsId) REFERENCES Curriculums(Id)
);

--Insert information table WorkExperiences
INSERT INTO WorkExperiences (Company, Year, Description, CurriculumsId)
VALUES 
('Company1', '2020-01-01', 'Desarrollador de software', 1),
('Company2', '2019-01-01', 'Ingeniero de software', 2),
('Company3', '2018-01-01', 'Desarrollador frontend', 3),
('Company4', '2017-01-01', 'Desarrollador backend', 4),
('Company5', '2016-01-01', 'Analista de sistemas', 5);

--Table WorkReferences
CREATE TABLE WorkReferences(
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(45) NOT NULL,
    Email VARCHAR(150) NOT NULL,
    Phone VARCHAR(20) NOT NULL,
    Curriculums INT,
    FOREIGN KEY (CurriculumsId) REFERENCES Curriculums(Id)
);

--Insert information table WorkReferences
INSERT INTO WorkReferences (Name, Email, Phone, Curriculums)
VALUES 
('Ref1', 'ref1@example.com', '3101234567', 1),
('Ref2', 'ref2@example.com', '3107654321', 2),
('Ref3', 'ref3@example.com', '3109876543', 3),
('Ref4', 'ref4@example.com', '3105678901', 4),
('Ref5', 'ref5@example.com', '3106789012', 5);

select * from Users;
