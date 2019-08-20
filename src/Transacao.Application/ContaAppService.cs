using Transacao.Application.Interfaces;
using Transacao.Domain.Entities;
using Transacao.Domain.Interfaces.Services;

namespace Transacao.Application
{
    public class ContaAppService : AppServiceBase<ContaEntity>, IContaAppService
    {
        private readonly IContaService _contaService;

        public ContaAppService(IContaService contaService) : base(contaService)
        {
            _contaService = contaService;
        }
    }
}
