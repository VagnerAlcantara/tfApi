using Transacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Transacao.Application.Interfaces
{
    public interface IAppServiceBase<T> where T : Entity
    {
        Task Adicionar(T entity);
        Task<T> ObterPorId(int id);
        Task<List<T>> ObterTodos();
        void Dispose();
    }
}
