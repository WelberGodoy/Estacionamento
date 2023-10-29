namespace Estacionamento.Application.Dto
{
    public class VeiculoResponseDto
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string TipoVeiculo { get; set; }
        public DateTimeOffset Entrada { get; set; }
        public DateTimeOffset? Saida { get; set; }
    }
}
