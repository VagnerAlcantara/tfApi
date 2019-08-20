using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transacao.Domain.Entities;
using Transacao.Domain.Interfaces;
using Transacao.Domain.Interfaces.Repositories;
using Transacao.Domain.Interfaces.Services;
using Transacao.Domain.Notifications;

namespace Transacao.Domain.Services
{
    public class ServiceBase<T> : IDisposable, IServiceBase<T> where T : Entity
    {
        private readonly INotificador _notificador;
        private readonly IRepositoryBase<T> _repository;

        public ServiceBase(IRepositoryBase<T> repository, INotificador notificador)
        {
            _repository = repository;
            _notificador = notificador;
        }
        #region| Validation
        protected void Notificar(string mensagem)
        {
            //Método que ira propagar erro até a API
            _notificador.Handle(new Notificacao(mensagem));
        }
        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }
        protected bool ExecutarValidacao<TValidacao, TEntidade>(TValidacao validacao, TEntidade entidade)
            where TValidacao : AbstractValidator<TEntidade>
            where TEntidade : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
        #endregion
        public virtual async Task Adicionar(T entity)
        {
            await _repository.Adicionar(entity);
        }
        public async Task<T> ObterPorId(int id)
        {
            return await _repository.ObterPorId(id);
        }
        public async Task<List<T>> ObterTodos()
        {
            return await _repository.ObterTodos();
        }
        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
