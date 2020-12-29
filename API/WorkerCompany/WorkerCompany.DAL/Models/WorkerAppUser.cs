namespace WorkerCompany.DAL.Models
{
    public class WorkerAppUser
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public virtual Worker Worker { get; set; }
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
