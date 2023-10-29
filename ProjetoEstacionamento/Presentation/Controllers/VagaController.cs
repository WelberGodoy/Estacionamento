using Estacionamento.Application.Dto;
using Estacionamento.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace Estacionamento.Presentation.Controllers
{
    [ApiController]
    [Route("api/vagas")]
    public class VagaController : ControllerBase
    {
        private readonly ILogger<VagaController> _logger;
        private readonly IVagaService _vagaService;

        public VagaController(ILogger<VagaController> logger, IVagaService vagaService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _vagaService = vagaService ?? throw new ArgumentNullException(nameof(vagaService));
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] List<VagaRequestDto> vagaRequest)
        {
            try
            {
                await _vagaService.Cadastrar(vagaRequest);
                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogWarning($"Erro ao cadastrar vagas: {exception.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError,
                  new
                  {
                      message = "Erro ao cadastrar vagas",
                      detail = exception.Message
                  });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Consultar()
        {
            try
            {
                var result = await _vagaService.Consultar();
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning($"Erro na consulta de vagas detalhadas: {exception.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError,
                  new
                  {
                      message = "Erro na consulta de vagas detalhadas",
                      detail = exception.Message
                  });
            }
        }

        [HttpGet("vagas-restantes")]
        public async Task<IActionResult> ConsultarVagasRestantes()
        {
            try
            {
                var result = await _vagaService.ConsultarVagasRestantes();
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning($"Erro na consulta de vagas restantes: {exception.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError,
                  new
                  {
                      message = "Erro na consulta de vagas restantes",
                      detail = exception.Message
                  });
            }
        }

        [HttpGet("total-vagas")]
        public async Task<IActionResult> ConsultarTotalVagas()
        {
            try
            {
                var result = await _vagaService.ConsultarTotalVagas();
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning($"Erro na consulta total de vagas: {exception.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError,
                  new
                  {
                      message = "Erro na consulta total de vagas",
                      detail = exception.Message
                  });
            }
        }

        [HttpGet("total-veiculos")]
        public async Task<IActionResult> ListarVeiculos()
        {
            try
            {
                var result = await _vagaService.ListarVeiculos();
                return Ok(result);
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
