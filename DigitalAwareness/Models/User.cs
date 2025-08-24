using System.ComponentModel.DataAnnotations;

namespace DigitalAwareness.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string FatherName { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Designation { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Village { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Post { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Block { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string State { get; set; } = string.Empty;

        [Required]
        [StringLength(6)]
        public string Pincode { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        public int ReferralNumber { get; set; }
        public string ReferralCode { get; set; } = string.Empty;

        public bool IsAdmin { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
