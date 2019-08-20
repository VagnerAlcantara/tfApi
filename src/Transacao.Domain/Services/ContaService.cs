using System;
using System.Threading.Tasks;
using Transacao.Domain.Entities;
using Transacao.Domain.Interfaces;
using Transacao.Domain.Interfaces.Repositories;
using Transacao.Domain.Interfaces.Services;

namespace Transacao.Domain.Services
{
    public class ContaService : ServiceBase<ContaEntity>, IContaService
    {
        private readonly IContaRepository _contaRepository;
        private readonly INotificador _notificador;

        public ContaService(IContaRepository contaRepository, INotificador notificador) : base(contaRepository, notificador)
        {
            _contaRepository = contaRepository;
            _notificador = notificador;
        }

        public async Task<ContaEntity> ObterPorConta(int agencia, int numero, int digito)
        {
            return await _contaRepository.ObterPorConta(agencia, numero, digito);
        }

        private bool PossuiSaldoSuficiente(decimal saldo, decimal valorNecessario)
        {
            return (saldo >= valorNecessario);
        }

        public async Task<ContaEntity> ValidarAptidaoContaDestinoTransacao(int agencia, int numero, int digito, decimal valorCreditado)
        {
            ContaEntity contaDestino = await ObterPorConta(agencia, numero, digito);

            if (contaDestino == null)
                contaDestino = await _contaRepository.Adicionar(new ContaEntity() { Agencia = agencia, Numero = numero, Digito = digito });

            contaDestino.AdicionarSaldo(valorCreditado);

            return contaDestino;
        }

        public async Task<ContaEntity> ValidarAptidaoContaOrigemTransacao(int agencia, int numero, int digito, decimal valorDebitado)
        {
            ContaEntity contaOrigem = await ObterPorConta(agencia, numero, digito);

            if (contaOrigem == null)
            {
                Notificar("Conta Origem não encontrada");
                return null;
            }

            bool possuiSaldo = PossuiSaldoSuficiente(contaOrigem.Saldo, valorDebitado);

            if (!possuiSaldo)
            {
                Notificar("Saldo insuficiente");
                return null;
            }

            contaOrigem.SubtrairSaldo(valorDebitado);

            return contaOrigem;
        }
    }
}
