using PS.Entity;
using System.ComponentModel.DataAnnotations;
namespace PS.Web.UI.Model

{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Eposta")]
        public string UserEmail { get; set; }

        [Required]
        [StringLength(15,ErrorMessage ="{0} alanına en az {2} karakter giriniz",MinimumLength =4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
