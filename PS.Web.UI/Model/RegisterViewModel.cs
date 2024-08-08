using System.ComponentModel.DataAnnotations;

namespace PS.Web.UI.Model
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "UserName")]

        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Eposta")]
        public string? UserEmail { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "{0} alanına en az {2} karakter giriniz", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Parolanız ilk Parolayla eşleşmedi")]
        [Display(Name = "Password Confirm")]
        public string? ConfirmPassword { get; set; }


    }
}
