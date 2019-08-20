using Transacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Transacao.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T> : IDisposable where T : Entity
    {
        Task<T> Adicionar(T entity);
        Task<T> ObterPorId(int id);
        Task<List<T>> ObterTodos();
        Task<int> SaveChanges();
    }
}
