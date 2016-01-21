using System;
using System.ComponentModel.DataAnnotations;

namespace APP.Store.Mvc.Models
{
    public class RetiradaViewModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "O valor do envelope é obrigatório")]
        [Display(Name = "Informe o valor contido no envelope")]
        public string ValorString { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal Valor { get; set; }
        
        [Required(ErrorMessage = "O identificador do envelope é obrigatório")]
        [Display(Name = "Informe o identificador do envelope")]
        public string NumeroEnvelope { get; set; }

        [Required(ErrorMessage = "O caixa que está efetuando a retirada é obrigatório")]
        [Display(Name = "Informe o valor contido no envelope")]
        public int CaixaId { get; set; }

        public virtual CaixaViewModel Caixa { get; set; }
        public DateTime DataHoraRegistro { get; set; }
    }
}
