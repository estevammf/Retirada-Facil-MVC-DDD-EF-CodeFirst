using System;

namespace APP.StoreManager.Infra.CrossCutting.Identity.Model
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

    }
}
