using System.ComponentModel.DataAnnotations;

namespace APP.Store.Mvc.Models
{
    public class UsuarioPermissaoAcessoViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Nome da Permissão é obrigatório.")]
        [Display(Name = "Nome da Permissão")]
        public string Type { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Valor da Permissão é obrigatório.")]
        [Display(Name = "Valor da Permissão")]
        public string Value { get; set; }
    }
}
