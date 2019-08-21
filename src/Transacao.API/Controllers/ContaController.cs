using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transacao.API.Commands;
using Transacao.Application.Interfaces;
using Transacao.Domain.Interfaces;

namespace Transacao.API.Controllers
{
    [Route("api/v1/conta")]
    [ApiController]
    public class ContaController : TransacaoBaseController, IDisposable
    {
        private readonly IContaAppService _contaAppService;
        private readonly INotificador _notificador;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ContaController(IContaAppService contaAppService, IMapper mapper, INotificador notificador, ILoggerFactory loggerFactory) : base(notificador, loggerFactory)
        {
            _contaAppService = contaAppService;
            _mapper = mapper;
            _notificador = notificador;
            _logger = _loggerFactory.CreateLogger("LoggerConta");
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                _logger.LogInformation("Requisição para obter todos as contas");

                return Response(true, _mapper.Map<List<ContaResponse>>(await _contaAppService.ObterTodos()));
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na requisição para obter todos as contas - erro: ${ex.Message}");

                return Response(false, ex.Message);
            }
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                _logger.LogInformation($"Requisição para obter a conta com id {id}");

                var response = await _contaAppService.ObterPorId(id);

                if (response == null)
                    return NotFound();

                return Response(true, _mapper.Map<ContaResponse>(response));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro na requisição para obter a conta com id {id} - erro: ${ex.Message}");

                return Response(false, ex);
            }
        }

        public void Dispose()
        {
            _contaAppService?.Dispose();
        }
    }
}