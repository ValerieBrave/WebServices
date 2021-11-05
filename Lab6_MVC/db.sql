create database WS_SVV;

use WS_SVV;
go

create table Student
(
	Id int identity(1,1) primary key,
	[Name] nvarchar(max)
)

create table Note
(
	Id int identity(1,1) primary key,
	Subj nvarchar(max),
	Note int null,
	StudentId int not null foreign key 
	references Student(Id)
)

insert into Student values 
('Danil'), ('Misha'), ('Maksim')

select * from Student;

insert into Note values
('OOP', 4, 3),
('OOP', 8, 2),
('OOP', 5, 1),
('PWS', 10, 1),
('PWS', 8, 2),
('PWS', 4, 3),
('PSCA', 9, 1),
('PSCA', 8, 2),
('PSCA', 8, 3)