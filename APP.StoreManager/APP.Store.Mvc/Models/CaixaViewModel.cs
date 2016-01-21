using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using APP.StoreManager.Application.Interface;
using APP.StoreManager.Domain.Entities;

namespace APP.Store.Mvc.Models
{
    public class CaixaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Identificação do Caixa é obrigatório.")]
        [Display(Name = "Identificador do Caixa")]
        public string NumeroIdentificador { get; set; }

         [Required(ErrorMessage = "Campo Funcionário Responsável é obrigatório.")]
        [Display(Name = "Funcionário Responsável")]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public virtual FuncionarioViewModel Operador { get; set; }
        public virtual ICollection<RetiradaViewModel> Retiradas { get; set; }

        public decimal TotalRetiradas
        {
            get
            {
                decimal soma = 0;

                if (Retiradas != null)
                {
                   soma = Retiradas.Aggregate<RetiradaViewModel, decimal>(0, (current, retirada) => current + retirada.Valor);
                }

                return soma;
            }
        }

    }
}
