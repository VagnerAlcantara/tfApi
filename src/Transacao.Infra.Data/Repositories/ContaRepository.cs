using Dapper;
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
    public class ContaRepository : RepositoryBase<ContaEntity>, IContaRepository
    {
        private IConfiguration _config;
        public ContaRepository(TransacaoContext context, IConfiguration config) : base(context)
        {
            _config = config;
        }

        #region| Obter todos
        public string SelectObterTodos
        {
            get
            {
                return
                    @"SELECT 
                           [Id]
                          ,[Agencia]
                          ,[Numero]
                          ,[Digito]
                          ,[Saldo]
                      FROM [dbo].[TbConta]";
            }
        }
        public override async Task<List<ContaEntity>> ObterTodos()
        {
            IEnumerable<ContaEntity> response;

            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("connTransacao")))
            {
                response = await conexao.QueryAsync<ContaEntity>(SelectObterTodos, null, commandType: CommandType.Text);

            }
            return response.ToList();
        }
        #endregion

        #region| Obter por ID
        public string SelectObterPorId
        {
            get
            {
                return
                    @"SELECT 
                           [Id]
                          ,[Agencia]
                          ,[Numero]
                          ,[Digito]
                          ,[Saldo]
                      FROM [dbo].[TbConta]
                      WHERE
	                   Id = @id";
            }
        }
        public override async Task<ContaEntity> ObterPorId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("connTransacao")))
            {
                return await conexao.QueryFirstOrDefaultAsync<ContaEntity>(SelectObterPorId, new { id = id }, commandType: CommandType.Text);
            }
        }
        #endregion

        #region| Obter por conta
        public string SelectObterPorConta
        {
            get
            {
                return
                    @"SELECT 
                           [Id]
                          ,[Agencia]
                          ,[Numero]
                          ,[Digito]
                          ,[Saldo]
                      FROM [dbo].[TbConta]
                      WHERE
	                    Agencia = @agencia
                      AND
                        Numero = @numero
                      AND
                        Digito = @digito";
            }
        }
        public async Task<ContaEntity> ObterPorConta(int agencia, int numero, int digito)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("connTransacao")))
            {
                return await conexao.QueryFirstOrDefaultAsync<ContaEntity>(SelectObterPorConta, new { @agencia = agencia, @numero = numero, @digito = digito }, commandType: CommandType.Text);
            }
        }
        #endregion
    }
}
