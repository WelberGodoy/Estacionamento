using AutoMapper;
using Estacionamento.Application.Dto;
using Estacionamento.Contracts.Repositories;
using Estacionamento.Contracts.Services;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Enums;

namespace Estacionamento.Application.Services
{
    public class MotoService : IVeiculoService
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IVagaRepository _vagaRepository;
        private readonly IMapper _mapper;

        public TipoVeiculo TipoVeiculo => TipoVeiculo.Moto;

        public MotoService(IVeiculoRepository veiculoRepository, IVagaRepository vagaRepository, IMapper mapper)
        {
            _veiculoRepository = veiculoRepository;
            _vagaRepository = vagaRepository;
            _mapper = mapper;
        }

        public async Task CadastrarAsync(VeiculoRequestDto veiculoRequest)
        {
            var tipoVagas = new List<TipoVaga>() { TipoVaga.Moto, TipoVaga.Carro, TipoVaga.Grande };

            var vagas = await _vagaRepository.ConsultarPorTipos(tipoVagas);

            if (!vagas.Any())
                throw new Exception("Solicitar criação do estacionamento de motos");

            var temVaga = VerificaVagas(vagas);

            if (!temVaga.Item1)
                throw new Exception("Não há vagas para motos");

            var veiculo = MontarVeiculo(veiculoRequest, temVaga.Item2);

            await _veiculoRepository.CadastrarVeiculoAsync(veiculo);
        }

        public async Task AtualizarAsync(VeiculoRequestDto veiculoRequest, int id)
        {
            if (veiculoRequest.Id != id)
                throw new Exception("Moto não encontrada");

            var veiculo = await _veiculoRepository.GetById(id);

            if (veiculo is null)
                throw new Exception("Moto não encontrada");

            veiculo.Placa = veiculoRequest.Placa;
            veiculo.Saida = DateTime.Now;

            await _veiculoRepository.AtulizarVeiculoAsync(veiculo);
        }

        public async Task<List<VeiculoResponseDto>> ListarAsync()
        {
            var veiculos = await _veiculoRepository.GetByTipo(TipoVeiculo.Moto);

            return veiculos.Select(c => _mapper.Map<VeiculoResponseDto>(c)).ToList();
        }

        private Veiculo MontarVeiculo(VeiculoRequestDto veiculoRequest, int idVaga)
        {
            var veiculo = _mapper.Map<Veiculo>(veiculoRequest);

            veiculo.Entrada = DateTime.Now;
            veiculo.TipoVeiculo = TipoVeiculo.Moto;
            veiculo.IdVaga = idVaga;

            return veiculo;
        }

        private static (bool, int) VerificaVagas(List<Vaga> vagas)
        {
            var tipoVagas = new List<TipoVaga>() { TipoVaga.Moto, TipoVaga.Carro, TipoVaga.Grande };
            var temVaga = false;
            var idVaga = 0;

            foreach (var tipo in tipoVagas)
            {
                var vagaEspecifica = vagas.Where(vaga => vaga.TipoVaga == tipo).First();

                var totalVagas = vagaEspecifica.Quantidade;
                var vagasEmUso = vagaEspecifica.Veiculos.Where(ve => ve.Saida is null).Count();

                if (vagasEmUso < totalVagas)
                {
                    temVaga = true;
                    idVaga = vagaEspecifica.Id;
                    break;
                }
            }

            return (temVaga, idVaga);
        }
    }
}
