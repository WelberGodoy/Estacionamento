using Estacionamento.Application.Dto;
using Estacionamento.Domain.Enums;

namespace Estacionamento.Contracts.Services
{
    public interface IVeiculoService
    {
        public TipoVeiculo TipoVeiculo { get; }
        Task CadastrarAsync(VeiculoRequestDto veiculoRequest);
        Task AtualizarAsync(VeiculoRequestDto veiculoRequest, int id);
        Task<List<VeiculoResponseDto>> ListarAsync();
    }
}
