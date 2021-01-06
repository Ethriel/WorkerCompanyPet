using Microsoft.AspNetCore.Identity;

namespace WorkerCompany.DAL.Models
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public int? WorkerId { get; set; }
        public virtual Worker Worker { get; set; }
        public int? AppUserProfileId { get; set; }
        public virtual AppUserProfile AppUserProfile { get; set; }
        public AppUser()
        {

        }
        public AppUser(string displayName, string userName, int? workerId, int? appUserProfileId) : base(userName)
        {
            DisplayName = displayName;
            WorkerId = workerId;
            AppUserProfileId = appUserProfileId;
        }
        public AppUser(string displayName, string userName, int? workerId, AppUserProfile appUserProfile) : base(userName)
        {
            DisplayName = displayName;
            WorkerId = workerId;
            AppUserProfile = appUserProfile;
        }
    }
}
