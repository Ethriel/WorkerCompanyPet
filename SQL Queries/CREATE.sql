use master;
go
create database WorkerCompanyPet;
go
use WorkerCompanyPet;
go
create table Company
(Id int primary key identity,
CompanyName nvarchar(50) not null,
Description nvarchar(500) not null);
go
create table Worker
(Id int primary key identity,
[Name] nvarchar(75) not null,
DOB datetime not null,
TimeUpdated datetime default GETDATE() not null,
CompanyId int foreign key references Company(Id));
go