CREATE TABLE Employee (
ID int NOT NULL PRIMARY KEY IDENTITY(1,1),
Name varchar(255) NOT NULL,
LastName varchar(255) NOT NULL,
RFC varchar(13) NOT NULL UNIQUE,
BornDate datetime NOT NULL,
Status varchar(255) NOT NULL,
CHECK (Status IN ('NotSet', 'Active', 'Inactive'))
);


ALTER TABLE Employee
ADD CHECK (dbo.CheckRFC(RFC) = 1);

INSERT INTO Employee ( Name, LastName, RFC, BornDate, Status)
VALUES ('Jane', 'Doe', 'EFGH789012VWZ', '1981-01-01', 'Active'),
       ('Bob', 'Smith', 'IJKL345678MNO', '1982-01-01', 'Active'),
       ('Alice', 'Smith', 'PQRS901234TUV', '1983-01-01', 'Active'),
       ('Mike', 'Williams', 'WXYZ567890GHI', '1984-01-01', 'Active'),
       ('Sally', 'Williams', 'JKLM123456DEF', '1985-01-01', 'Active'),
       ('Steve', 'Jones', 'NOPQ789012RST', '1986-01-01', 'Active'),
       ('Rachel', 'Jones','UVWX345678YZA', '1987-01-01', 'Active'),
       ('Tom', 'Brown', 'CDEF901212345', '1988-01-01', 'Active'),
       ('Emma', 'Brown', 'GHIJ567890123', '1989-01-01', 'Active');