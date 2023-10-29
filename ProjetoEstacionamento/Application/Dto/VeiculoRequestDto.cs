using Estacionamento.Domain.Enums;

namespace Estacionamento.Application.Dto
{
    public class VeiculoRequestDto
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public TipoVeiculo TipoVeiculo { get; set; }
    }
}
