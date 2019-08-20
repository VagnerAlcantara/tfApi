using Transacao.Domain.Entities;
using Transacao.Domain.Interfaces.Repositories;
using Transacao.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Transacao.Infra.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : Entity, new()
    {
        protected readonly TransacaoContext Db;
        protected readonly DbSet<T> DbSet;

        public RepositoryBase(TransacaoContext db)
        {
            Db = db;
            DbSet = Db.Set<T>();
        }
        public virtual async Task<T> Adicionar(T entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
            return entity;
        }
        public virtual async Task<T> ObterPorId(int id)
        {
            return await DbSet.FindAsync(id);
        }
        public virtual async Task<List<T>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }
        public void Dispose()
        {
            Db?.Dispose();
        }
        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }
    }
}
