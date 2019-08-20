using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Transacao.Domain.Entities;
using Transacao.Domain.Interfaces.Repositories;
using Transacao.Infra.Data.Context;

namespace Transacao.Infra.Data.Repositories
{
    public class TransacaoRepository : RepositoryBase<TransacaoEntity>, ITransacaoRepository
    {
        private IConfiguration _config;
        TransacaoContext _context;
        public TransacaoRepository(TransacaoContext context, IConfiguration config) : base(context)
        {
            _config = config;
            _context = context;
        }

        public string SelectObterTodos
        {
            get
            {
                return
                    @"SELECT
                       [Id]
                      ,[DataTransacao]
                      ,[ContaOrigemId]
                      ,[ContaDestinoId]
                      ,[Valor]
                    FROM [dbo].[TbTransacao]";
            }
        }
        public string SelectObterPorId
        {
            get
            {
                return
                    @"SELECT 
                       [Id]
                      ,[DataTransacao]
                      ,[ContaOrigemId]
                      ,[ContaDestinoId]
                      ,[Valor]
                    FROM [dbo].[TbTransacao]
                    WHERE
	                   Id = @id";
            }
        }
        public override async Task<List<TransacaoEntity>> ObterTodos()
        {
            IEnumerable<TransacaoEntity> response;

            using (SqlConnection conexao = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                response = await conexao.QueryAsync<TransacaoEntity>(SelectObterTodos, null, commandType: CommandType.Text);

            }

            return response.ToList();
        }
        public override async Task<TransacaoEntity> ObterPorId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                return await conexao.QueryFirstOrDefaultAsync<TransacaoEntity>(SelectObterPorId, new { id = id }, commandType: CommandType.Text);
            }
        }
        public override async Task<TransacaoEntity> Adicionar(TransacaoEntity entity)
        {
            Db.Set<ContaEntity>().Update(entity.ContaOrigem);
            Db.Set<ContaEntity>().Update(entity.ContaDestino);
            Db.Set<TransacaoEntity>().Add(entity);
            await SaveChanges();
            return entity;
        }
    }
}
