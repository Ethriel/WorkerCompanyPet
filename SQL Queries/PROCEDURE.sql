use WorkerCompanyPet;
go
create procedure sp_GetAllWorkersByCompanyId @Id int
as
	select * from Worker
	where CompanyId = @Id
go