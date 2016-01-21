using System.ComponentModel.DataAnnotations;

namespace APP.Store.Mvc.Models
{
    public class PermissaoAcessoViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Forneça um nome para a Permissão de Acesso")]
        [MaxLength(128, ErrorMessage = "Tamanho máximo {0} excedido")]
        [Display(Name = "Nome da Permissão de Acesso")]
        public string Name { get; set; }
    }
}
