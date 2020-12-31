namespace WorkerCompany.Authentication.Models.Auth
{
    public class SignUpModel
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public int? WorkerId { get; set; }
    }
}
