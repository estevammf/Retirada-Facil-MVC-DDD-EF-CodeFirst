using System.ComponentModel;

namespace APP.StoreManager.Domain.Enum
{
    public enum TipoTransacaoBanesfacilEnum
    {
        [Description("Recebimento de Conta")]
        RecebimentoConta,
        [Description("Recebimento de Escelsa")]
        RecebimentoEscelsa,
        [Description("Saques")]
        Saques,
        [Description("Depósito")]
        Deposito
    }
}
