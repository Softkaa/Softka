-- Active: 1720588487963@@brb7cf2qxxih75x8siuv-mysql.services.clever-cloud.com@3306

--Table Users
create table Users(
    Id int auto_increment PRIMARY KEY,
    Names varchar(45) not null,
    LastNames varchar(45) not null,
    TypeDocument enum("CC", "CE", "TI", "Passport") not null,
    Document varchar(20) not null,
    Email varchar(150) not null unique,
    Age int not null,
    Password varchar(150) unique,
    DateRegister date
);

--Insert information table Users
INSERT INTO Users (Names, LastNames, TypeDocument, Document, Email, Age, Password, DateRegister)
VALUES 
('Juan', 'Perez', 'CC', '12345678', 'juan.perez@example.com', 30, 'password1', '2023-01-01'),
('Maria', 'Lopez', 'CE', '87654321', 'maria.lopez@example.com', 25, 'password2', '2023-02-01'),
('Carlos', 'Gomez', 'TI', '11223344', 'carlos.gomez@example.com', 28, 'password3', '2023-03-01'),
('Ana', 'Martinez', 'Passport', '99887766', 'ana.martinez@example.com', 32, 'password4', '2023-04-01'),
('Luis', 'Garcia', 'CC', '55667788', 'luis.garcia@example.com', 27, 'password5', '2023-05-01');

create table Authentications(
    Id int auto_increment PRIMARY KEY,
    Token text not null,
    RefreshToken text not null,
    IssueDate date not null,
    ExpirationDate date not null,   
    IsActive bool not null,
    IsRefreshed bool DEFAULT false,
    UserId int,
    Foreign Key (UserId) REFERENCES Users(Id)
);

--Insert information table Authentications
INSERT INTO Authentications (Token, RefreshToken, IssueDate, ExpirationDate, IsActive, UserId)
VALUES 
('jwtToken1', 'refreshToken1', '2023-01-01', '2024-01-01', TRUE, 1),
('jwtToken2', 'refreshToken2', '2023-02-01', '2024-02-01', TRUE, 2),
('jwtToken3', 'refreshToken3', '2023-03-01', '2024-03-01', TRUE, 3),
('jwtToken4', 'refreshToken4', '2023-04-01', '2024-04-01', TRUE, 4),
('jwtToken5', 'refreshToken5', '2023-05-01', '2024-05-01', TRUE, 5);

--Table UserRoles
create table UserRoles(
    Id int auto_increment PRIMARY KEY,
    UserId int,
    RoleId int,
    Foreign Key (UserId) REFERENCES Users(Id),
    Foreign Key (RoleId) REFERENCES Roles(Id)
);

--Insert information table UserRoles
INSERT INTO UserRoles (UserId, RoleId)
VALUES 
(1, 1), (2, 2), (3, 3), (4, 4), (5, 5);

--Table Roles
create table Roles(
    Id int auto_increment PRIMARY KEY,
    Name enum("Admin", "User") not null
);

--Insert information table Roles
INSERT INTO Roles (Name) VALUES ('Admin'), ('User');

--Table Curriculums
create table Curriculums(
    Id int auto_increment PRIMARY KEY,
    Age int not null,
    Nationality varchar(45) not null,
    Photo text not null,
    CivilStatus enum("soltero", "casado", "viudo") not null,
    Phone varchar(20) not null,
    UserId int,
    Foreign Key (UserId) REFERENCES Users(Id)
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
create table Skills(
    Id int auto_increment PRIMARY KEY,
    Name text not null,
    CurriculumsId int,
    Foreign Key (CurriculumsId) REFERENCES Curriculums(Id)
);

--Insert information table Skills
INSERT INTO Skills (Name, CurriculumsId)
VALUES 
('Python', 1), ('JavaScript', 2), ('HTML', 3), ('CSS', 4), ('SQL', 5);

--Table Educations
create table Educations(
    Id int auto_increment PRIMARY KEY,
    Institution varchar(100) not null,
    EducationalTitle varchar(100) not null,
    CurriculumsId int,
    Foreign Key (CurriculumsId) REFERENCES Curriculums(Id)
);

--Insert information table Educations
INSERT INTO Educations (Institution, EducationalTitle, CurriculumsId)
VALUES 
('Universidad Nacional', 'Ingeniería de Sistemas', 1),
('Universidad de los Andes', 'Ingeniería de Software', 2),
('Universidad Javeriana', 'Ingeniería Informática', 3),
('Universidad del Rosario', 'Ingeniería Electrónica', 4),
('Universidad EAFIT', 'Ingeniería de Telecomunicaciones', 5);

--Table WorkExperiences
create table WorkExperiences(
    Id int auto_increment PRIMARY KEY,
    Company varchar(100) not null,
    Year date not null,
    CurriculumsId int,
    Foreign Key (CurriculumsId) REFERENCES Curriculums(Id)
);

--Insert information table WorkExperiences
INSERT INTO WorkExperiences (Company, Year, CurriculumsId)
VALUES 
('Company1', '2020-01-01', 1),
('Company2', '2019-01-01', 2),
('Company3', '2018-01-01', 3),
('Company4', '2017-01-01', 4),
('Company5', '2016-01-01', 5);

--Table WorkReferences
create table WorkReferences(
    Id int auto_increment PRIMARY KEY,
    Name varchar(45) not null,
    Email varchar(150) not null,
    Phone varchar(20) not null,
    Curriculums int,
    Foreign Key (Curriculums) REFERENCES Curriculums(Id)
);

--Insert information table WorkReferences
INSERT INTO WorkReferences (Name, Email, Phone, Curriculums)
VALUES 
('Ref1', 'ref1@example.com', '3101234567', 1),
('Ref2', 'ref2@example.com', '3107654321', 2),
('Ref3', 'ref3@example.com', '3109876543', 3),
('Ref4', 'ref4@example.com', '3105678901', 4),
('Ref5', 'ref5@example.com', '3106789012', 5);


SELECT * FROM Users;