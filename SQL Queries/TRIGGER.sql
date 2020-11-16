use WorkerCompanyPet;
go
create trigger onInsertOrUpdateWorker
on [Worker]
after
update
as
begin
set nocount on;
	update Worker
	set TimeUpdated = GETDATE()
	from Worker as W
	inner join inserted as I on W.Id=I.Id
set nocount off;
end;
go