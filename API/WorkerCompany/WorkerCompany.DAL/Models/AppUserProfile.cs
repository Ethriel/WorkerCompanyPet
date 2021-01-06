using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkerCompany.DAL.Models
{
    public class AppUserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public bool MarriageStatus { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public AppUserProfile()
        {

        }
        public AppUserProfile(string firstName, string lastName, bool marriageStatus, string gender, string dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            MarriageStatus = marriageStatus;
            Gender = gender;
            DateOfBirth = DateTime.Parse(dateOfBirth);
        }
    }
}
