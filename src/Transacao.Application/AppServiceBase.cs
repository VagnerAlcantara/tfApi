using Transacao.Application.Interfaces;
using Transacao.Domain.Entities;
using Transacao.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Transacao.Application
{
    public class AppServiceBase<T> : IDisposable, IAppServiceBase<T> where T : Entity
    {
        private readonly IServiceBase<T> _serviceBase;

        public AppServiceBase(IServiceBase<T> serviceBase)
        {
            _serviceBase = serviceBase;
        }
        public virtual async Task Adicionar(T entity)
        {
            await _serviceBase.Adicionar(entity);
        }
        public async Task<T> ObterPorId(int id)
        {
            return await _serviceBase.ObterPorId(id);
        }

        public async Task<List<T>> ObterTodos()
        {
            return await _serviceBase.ObterTodos();
        }
        public void Dispose()
        {
            _serviceBase?.Dispose();
        }
    }
}
