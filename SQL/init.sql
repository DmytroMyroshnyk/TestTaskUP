CREATE TABLE Company (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(100),
    Address VARCHAR(200),
    Phone VARCHAR(20) NULL,
    Description TEXT NULL
);

CREATE TABLE Department (
    Id int PRIMARY KEY,
    Name VARCHAR(100)
);

CREATE TABLE Position (
    Id int PRIMARY KEY,
    Name VARCHAR(100)
);

CREATE TABLE Employee (
    Id SERIAL PRIMARY KEY,
    --FullName VARCHAR(100),
    FirstName VARCHAR(20),
    MiddleName VARCHAR(20) NULL,
    LastName VARCHAR(20),

    Address VARCHAR (200) NULL,
    Phone VARCHAR(20) NULL,
    BirthDate DATE ,
    HireDate DATE,
    Salary NUMERIC(10,2),
    DepartmentId INT REFERENCES Department(Id),
    PositionId INT REFERENCES Position(Id),
    CompanyId int NOT NULL REFERENCES Company(Id)
);

INSERT INTO Company (Name, Address, Phone, Description) VALUES 
('Ukr Post', 'Khreshchatyk 22, Kyiv', '044 467 8223', 'Holovposhtamt Kyyiv. Holovna Poshta Krayiny');

INSERT INTO Department (id, Name) VALUES
(10, 'IT'),
(20, 'Human Resources'),
(30, 'Education'),
(40, 'Admin');

INSERT INTO Position (id, Name) VALUES
(1, 'Backend Developer'),
(2, 'FrontEnd Developer'),
(3, 'IOS Developer'),
(4, 'Android Developer'),
(5, 'Flutter Developer'),
(6, 'Python Developer'),
(7, 'JAVA Developer'),
(8, '.Net Developer'),
(21, 'HR Coordinator'),
(22, 'HR Manager'),
(23, 'Recruiter'),
(31, 'English Teacher'),
(41, 'CEO');

INSERT INTO Employee (FirstName, MiddleName, LastName, Address, Phone, BirthDate, HireDate, Salary, DepartmentId, PositionId, CompanyId)
VALUES 
('Andrii', 'Ivanovych', 'Shevchenko', 'Lviv, Svobody Ave 10', '+380631234567', '1985-03-15', '2015-04-01', 55000.00, 10, 1, 1),
('Olena', NULL, 'Pavlenko', 'Kyiv, Khreshchatyk 22', '+380671112233', '1990-07-22', '2018-06-12', 60000.00, 10, 2, 1),
('Ihor', 'Serhiyovych', 'Melnyk', 'Dnipro, Gagarina 101', '+380931234567', '1978-09-30', '2011-10-01', 47000.00, 10, 8, 1),
('Viktoriya', NULL, 'Lysenko', 'Odesa, Deribasivska 5', '+380661234567', '1995-01-15', '2020-03-05', 40000.00, 20, 21, 1),
('Taras', NULL, 'Kovalchuk', 'Kharkiv, Sumskaya 33', '+380501234567', '1980-11-10', '2010-11-11', 75000.00, 10, 7, 1),
('Maria', 'Oleksandrivna', 'Zhuk', 'Poltava, Sobornosti 12', '+380639876543', '1992-06-20', '2019-05-15', 39000.00, 20, 23, 1),
('Sofiia', NULL, 'Tkachenko', 'Chernihiv, Peremohy 17', '+380501119988', '1987-04-18', '2014-02-01', 35000.00, 20, 22, 1),
('Mykhailo', NULL, 'Hnatyuk', 'Ivano-Frankivsk, Nezalezhnosti 30', '+380667771122', '1982-12-05', '2012-01-20', 72000.00, 10, 6, 1),
('Nadiia', NULL, 'Polishchuk', 'Lutsk, Voli Ave 1', '+380633332211', '1991-03-25', '2016-08-30', 37000.00, 30, 31, 1),
('Petro', NULL, 'Radchenko', 'Vinnytsia, Soborna 45', '+380991234555', '1983-08-19', '2013-09-25', 46000.00, 30, 31, 1),

('Anton', NULL, 'Danyliuk', 'Lviv, Halytska 9', '+380671111111', '1984-05-01', '2011-02-02', 45000.00, 10, 8, 1),
('Daria', NULL, 'Levchenko', 'Kyiv, Vozdvyzhenska 12', '+380951234321', '1996-07-17', '2022-05-18', 42000.00, 10, 2, 1),
('Serhii', NULL, 'Bondarenko', 'Rivne, Soborna 8', '+380991111222', '1975-02-10', '2009-03-15', 80000.00, 10, 7, 1),
('Oksana', 'Petrovna', 'Herasymchuk', 'Chernivtsi, Holovna 16', '+380681234567', '1989-10-12', '2017-04-04', 39000.00, 20, 22, 1),
('Vadym', NULL, 'Sytnyk', 'Zhytomyr, Kyivska 30', '+380931110000', '1993-01-09', '2021-07-01', 43000.00, 10, 5, 1),
('Iryna', NULL, 'Yuriychuk', 'Sumy, Kharkivska 99', '+380667890123', '1986-11-20', '2013-08-09', 36000.00, 20, 21, 1),
('Roman', NULL, 'Karpenko', 'Ternopil, Ruska 55', '+380961112233', '1990-12-28', '2015-11-10', 59000.00, 10, 3, 1),
('Kateryna', NULL, 'Bezuhla', 'Kyiv, Lvivska Sq 14', '+380639999000', '1995-09-14', '2020-10-10', 41000.00, 10, 4, 1),
('Bohdan', NULL, 'Oliynyk', 'Zaporizhzhia, Soborna 21', '+380503456789', '1981-06-11', '2010-12-12', 75000.00, 10, 6, 1),
('Liliia', NULL, 'Stasenko', 'Mykolaiv, Admiralska 33', '+380678888000', '1994-03-03', '2019-06-01', 33000.00, 30, 31, 1),


('Volodymyr', NULL, 'Tymoshchuk', 'Kyiv, Bohdana Khmelnytskoho 3', '+380931112299', '1988-08-08', '2015-07-07', 67000.00, 40, 41, 1),
('Yuliia', NULL, 'Dziuba', 'Kherson, Perekopska 2', '+380661100900', '1992-02-20', '2016-09-15', 35000.00, 30, 31, 1),
('Artur', NULL, 'Sydorenko', 'Cherkasy, Shevchenka 1', '+380997700123', '1985-04-05', '2011-01-20', 71000.00, 10, 1, 1),
('Alina', NULL, 'Tkach', 'Uzhhorod, Soborna 11', '+380635551122', '1990-06-30', '2014-12-01', 43000.00, 20, 23, 1)
;