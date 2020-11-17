using System;

namespace WorkerCompany.BLL.DTO
{
    public class WorkerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Dob { get; set; }
        public string TimeUpdated { get; set; }
        public string CompanyName { get; set; }
        public int? CompanyId { get; set; }
    }
}
