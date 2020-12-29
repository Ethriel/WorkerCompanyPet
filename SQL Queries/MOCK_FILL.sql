use WorkerCompanyPet;
go
insert into Company
values
('Company 1', 'Company 1 description'),
('Company 2', 'Company 2 description'),
('Company 3', 'Company 3 description'),
('Company 4', 'Company 4 description'),
('Company 5', 'Company 5 description')
go
insert into Worker([Name], DOB, CompanyId)
values
('Admin Worker 1', '1990-10-10', 1),
('Test Worker 2', '1992-11-12', 1),
('Test Manager 3', '1993-12-13', 1),
('Worker 4', '1994-01-14', 2),
('Worker 5', '1995-02-15', 2),
('Worker 6', '2000-03-16', 2),
('Worker 7', '1997-05-17', 3),
('Worker 8', '1985-07-18', 3),
('Worker 9', '1989-09-19', 3),
('Worker 10', '1990-08-20', 3),
('Worker 11', '1991-03-22', 4),
('Worker 12', '1999-12-24', 4),
('Worker 13', '1998-11-23', 4),
('Worker 14', '1991-11-11', 4),
('Worker 15', '1995-12-11', 5),
('Worker 16', '1987-01-01', 5),
('Worker 17', '1986-02-12', 5),
('Worker 18', '2001-03-13', 1),
('Worker 19', '1983-06-14', 2),
('Worker 20', '1991-08-17', 3),
('Worker 21', '1992-09-18', 4),
('Worker 22', '1993-04-26', 5)
go