using System.Threading.Tasks;
using Transacao.Domain.Entities;

namespace Transacao.Domain.Interfaces.Repositories
{
    public interface IContaRepository : IRepositoryBase<ContaEntity>
    {
        Task<ContaEntity> ObterPorConta(int agencia, int numero, int digito);
    }
}
