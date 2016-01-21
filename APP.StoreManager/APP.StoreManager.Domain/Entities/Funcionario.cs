using System;

namespace APP.StoreManager.Domain.Entities
{
    public class Funcionario : EntidadeBase
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCelular { get; set; }
        public string Email { get; set; }
    }
}
