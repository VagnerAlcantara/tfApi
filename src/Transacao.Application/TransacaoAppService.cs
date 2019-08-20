using Transacao.Application.Interfaces;
using Transacao.Domain.Entities;
using Transacao.Domain.Interfaces.Services;

namespace Transacao.Application
{
    public class TransacaoAppService : AppServiceBase<TransacaoEntity>, ITransacaoAppService
    {
        private readonly ITransacaoService _transacaoService;

        public TransacaoAppService(ITransacaoService transacaoService) : base(transacaoService)
        {
            _transacaoService = transacaoService;
        }
    }
}
