using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Store.Mvc.Models
{
    public class EmpresaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Nome da Empresa é obrigatório.")]
        [DisplayName("Nome da Empresa")]
        [StringLength(100, ErrorMessage = "O nome da Empresa deve conter no máximo {0} e no  mínimo {2}", MinimumLength = 6)]
        public string Nome { get; set; }

        [DisplayName("Nome Fantasia")]
        [StringLength(100, ErrorMessage = "O nome da Empresa deve conter no máximo {0} e no  mínimo {2}", MinimumLength = 6)]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "Campo CNPJ é obrigatório.")]
        [DisplayName("CNPJ")]
        [StringLength(11, ErrorMessage = "O CNPJ deve conter {2} caracteres.", MinimumLength = 11)]
        public string Cnpj { get; set; }

         [Required(ErrorMessage = "Campo Telefone é obrigatório.")]
        [DisplayName("Telefone")]
        [StringLength(11, ErrorMessage = "O Telefone deve conter {2} caracteres.", MinimumLength = 10)]
        public string Telefone { get; set; }

        [DisplayName("Estado")]
        [StringLength(50, ErrorMessage = "O nome do Estado deve conter no máximo {0} e no  mínimo {2}", MinimumLength = 2)]
        public string Estado { get; set; }

         [Required(ErrorMessage = "Campo Cidade é obrigatório.")]
        [DisplayName("Cidade")]
        [StringLength(50, ErrorMessage = "O nome da Cidade deve conter no máximo {0} e no  mínimo {2}", MinimumLength = 4)]
        public string Cidade { get; set; }

        [DisplayName("Cep")]
        [StringLength(7, ErrorMessage = "O CEP deve conter {0} caracteres", MinimumLength = 7)]
        public string Cep { get; set; }

        [DisplayName("Número")]
        public int? Numero { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
