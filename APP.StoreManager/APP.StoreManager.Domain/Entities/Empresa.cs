using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.StoreManager.Domain.Entities
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string  Cep { get; set; }
        public int? Numero { get; set; }
        public DateTime DataCadastro { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
        //public virtual ICollection<Fornecedor> Fornecedores { get; set; }
        //public virtual ICollection<Funcionario> Clientes { get; set; }
    }
}
