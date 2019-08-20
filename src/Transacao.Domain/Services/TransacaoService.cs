using System.Threading.Tasks;
using Transacao.Domain.Entities;
using Transacao.Domain.Interfaces;
using Transacao.Domain.Interfaces.Repositories;
using Transacao.Domain.Interfaces.Services;
using Transacao.Domain.Validation;

namespace Transacao.Domain.Services
{
    public class TransacaoService : ServiceBase<TransacaoEntity>, ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IContaService _contaService;
        public TransacaoService(ITransacaoRepository transacaoRepository, IContaService contaService, INotificador notificador)
            : base(transacaoRepository, notificador)
        {
            _transacaoRepository = transacaoRepository;
            _contaService = contaService;
        }

        public override async Task Adicionar(TransacaoEntity entity)
        {
            //validar o estado da entidade
            if (!ExecutarValidacao(new TransacaoValidation(), entity)) return;

            entity.ContaOrigem = await _contaService.ValidarAptidaoContaOrigemTransacao(entity.ContaOrigem.Agencia, entity.ContaOrigem.Numero, entity.ContaOrigem.Digito,entity.Valor);

            entity.ContaDestino = await _contaService.ValidarAptidaoContaDestinoTransacao(entity.ContaDestino.Agencia, entity.ContaDestino.Numero, entity.ContaDestino.Digito, entity.Valor);

            if (entity.ContaOrigem.Id == entity.ContaDestino.Id)
            {
                Notificar("Não é permitido fazer transação entre contas iguais");
                return;
            }

            await _transacaoRepository.Adicionar(entity);
        }
    }
}
