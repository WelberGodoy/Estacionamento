using Estacionamento.Domain.Enums;

namespace Estacionamento.Application.Dto
{
    public class VagaRequestDto
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public TipoVaga TipoVaga { get; set; }
    }
}
