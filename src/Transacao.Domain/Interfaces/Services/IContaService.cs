using System;
using System.Threading.Tasks;
using Transacao.Domain.Entities;

namespace Transacao.Domain.Interfaces.Services
{
    public interface IContaService : IServiceBase<ContaEntity>
    {
        Task<ContaEntity> ObterPorConta(int agencia, int numero, int digito);
        Task<ContaEntity> ValidarAptidaoContaOrigemTransacao(int agencia, int numero, int digito, decimal valorDebitado);
        Task<ContaEntity> ValidarAptidaoContaDestinoTransacao(int agencia, int numero, int digito, decimal valorCreditado);
    }
}
