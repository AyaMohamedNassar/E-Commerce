using System.ComponentModel.DataAnnotations;

namespace E_CommerceAPI.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d\s]).{6,10}$",
            //ErrorMessage = "Password must have at least 1 uppercase letter, 1 lowercase letter, 1 number, 1 non-alphanumeric character, and be at least 6 characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
