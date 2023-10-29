using Estacionamento.Application.Dto;
using Estacionamento.Application.Factories;
using Estacionamento.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Estacionamento.Presentation.Controllers
{
    [ApiController]
    [Route("api/veiculos")]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoServiceFactory _veiculoServiceFactory;
        private readonly ILogger<VeiculoController> _logger;

        public VeiculoController(IVeiculoServiceFactory veiculoServiceFactory, ILogger<VeiculoController> logger)
        {
            _veiculoServiceFactory = veiculoServiceFactory ?? throw new ArgumentNullException(nameof(veiculoServiceFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] VeiculoRequestDto veiculoRequest)
        {
            try
            {
                var servico = _veiculoServiceFactory.Create(veiculoRequest.TipoVeiculo);
                await servico.CadastrarAsync(veiculoRequest);

                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogWarning($"Erro no cadastro de veículos: {exception.Message}");

                return StatusCode((int)HttpStatusCode.InternalServerError,
                  new
                  {
                      message = "Erro no cadastro de veículos",
                      detail = exception.Message
                  });
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([FromBody] VeiculoRequestDto veiculoRequest, int id)
        {
            try
            {
                var servico = _veiculoServiceFactory.Create(veiculoRequest.TipoVeiculo);
                await servico.AtualizarAsync(veiculoRequest, id);

                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogWarning($"Erro na atualização de veículos: {exception.Message}");

                return StatusCode((int)HttpStatusCode.InternalServerError,
                  new
                  {
                      message = "Erro na atualização de veículos",
                      detail = exception.Message
                  });
            }
        }

        [HttpGet("{tipoVeiculo}")]
        public async Task<IActionResult> ListarVeiculos(TipoVeiculo tipoVeiculo)
        {
            try
            {
                var servico = _veiculoServiceFactory.Create(tipoVeiculo);
                return Ok(await servico.ListarAsync());
            }
            catch (Exception exception)
            {
                _logger.LogWarning($"Erro na consulta de veículos: {exception.Message}");

                return StatusCode((int)HttpStatusCode.InternalServerError,
                  new
                  {
                      message = "Erro na consulta de veículos",
                      detail = exception.Message
                  });
            }
        }
    }
}
