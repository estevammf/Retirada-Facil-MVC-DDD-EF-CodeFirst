using System.ComponentModel.DataAnnotations;

namespace APP.Store.Mvc.Models
{
    public class FuncionarioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(120, ErrorMessage = "O nome do funcionário deve conter entre {2} e {0} caracteres.", MinimumLength = 10)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo CPF é obrigatório.")]
        [Display(Name = "CPF")]
        [StringLength(11, ErrorMessage = "O CPF deve conter {2} caracteres.", MinimumLength = 11)]
        public string Cpf { get; set; }

        [Display(Name = "Telefone Fixo")]
        [StringLength(10, ErrorMessage = "O número do telefone deve conter {2} caracteres.", MinimumLength = 10)]
        public string TelefoneFixo { get; set; }

        [Required(ErrorMessage = "Campo Telefone Celular é obrigatório.")]
        [Display(Name = "Telefone Celular")]
        [StringLength(11, ErrorMessage = "O nome do funcionário deve conter entre {2} e {0} caracteres.", MinimumLength = 11)]
        public string TelefoneCelular { get; set; }


        [Required(ErrorMessage = "Campo Email é obrigatório.")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
