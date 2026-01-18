using System.ComponentModel.DataAnnotations;

namespace WaterQualityMonitoring.Models
{
    public class User
    {


        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username required")]
        [StringLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [Phone(ErrorMessage = "Incorrect phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Preservation is required")]
        public string Governorate { get; set; }


    }


}
