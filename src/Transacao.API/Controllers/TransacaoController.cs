using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transacao.API.Commands;
using Transacao.Application.Interfaces;
using Transacao.Domain.Entities;
using Transacao.Domain.Interfaces;

namespace Transacao.API.Controllers
{
    [Route("api/v1/transacao")]
    [ApiController]
    public class TransacaoController : TransacaoBaseController, IDisposable
    {
        private readonly ITransacaoAppService _transacaoAppService;
        private readonly IMapper _mapper;
        private readonly INotificador _notificador;
        private readonly ILogger _logger;
        public TransacaoController(
            ITransacaoAppService transacaoAppService,
            IMapper mapper,
            INotificador notificador,
            ILoggerFactory loggerFactory) : base(notificador, loggerFactory)
        {
            _transacaoAppService = transacaoAppService;
            _mapper = mapper;
            _notificador = notificador;
            _logger = _loggerFactory.CreateLogger("LoggerTransacao");
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                _logger.LogInformation("Requisição para obter todos as transações");

                return Response(true, _mapper.Map<List<TransacaoResponse>>(await _transacaoAppService.ObterTodos()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro na requisição para todas as transações", ex);

                return Response(false, ex.Message);
            }
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                _logger.LogInformation($"Requisição para obter a transação com id {id}");

                var response = await _transacaoAppService.ObterPorId(id);

                if (response == null)
                    return NotFound();

                return Response(true, _mapper.Map<TransacaoResponse>(response));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro na requisição para todas as transações", ex);

                return Response(false, ex);
            }
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]TransacaoInclusaoRequest request)
        {
            try
            {
                _logger.LogInformation($"Requisição para inclusão de uma nova transação", request);

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Request com dados inválidos para inclusão de nova transação", request);
                    return Response(false, new BadRequestObjectResult(ModelState.Values));
                }
                var transacaoEntity = _mapper.Map<TransacaoEntity>(request);

                await _transacaoAppService.Adicionar(transacaoEntity);

                if (!OperacaoValida())
                {
                    _logger.LogInformation($"Request com dados inválidos para inclusão de nova transação", request);

                    return Response(false, _notificador.ObterNotificacoes());
                }

                _logger.LogInformation($"Transação incluída com sucesso", request);

                return Response(true, _mapper.Map<TransacaoResponse>(transacaoEntity));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro na requisição para inclusão de uma nova transação", ex);

                return Response(false, ex);
            }
        }

        public void Dispose()
        {
            _transacaoAppService?.Dispose();
        }
    }
}