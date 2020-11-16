use WorkerCompanyPet;
go
create trigger onInsertOrUpdateWorker
on [Worker]
instead of
update
as
begin
set nocount on;
	insert into [Worker](Id, [Name], DOB, TimeUpdated, CompanyId)
	select Id, [Name], DOB, GETDATE(), CompanyId
	from inserted
set nocount off;
end;
go