using Estacionamento.Application.Dto;

namespace Estacionamento.Contracts.Services
{
    public interface IVagaService
    {
        public Task Cadastrar(List<VagaRequestDto> vagaRequest);
        public Task<VagaResponseDto> Consultar();
        public Task<int> ConsultarVagasRestantes();
        public Task<int> ConsultarTotalVagas();
        public Task<List<VagaVeiculoResponseDto>> ListarVeiculos();
    }
}
