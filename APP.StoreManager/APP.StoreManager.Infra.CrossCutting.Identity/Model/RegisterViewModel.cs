using System.ComponentModel.DataAnnotations;

namespace APP.StoreManager.Infra.CrossCutting.Identity.Model
{
    public class RegisterViewModel
    {
        [Display(Name = "Usuário da Empresa")]
        public int EmpresaId { get; set; }
        //public virtual Empresa Empresa { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

       
    }
}