namespace Estacionamento.Application.Dto
{
    public class VagaVeiculoResponseDto
    {
        public int Quantidade { get; set; }
        public string TipoVaga { get; set; }
        public List<VeiculoResponseDto> Veiculos { get; set; }
    }
}
