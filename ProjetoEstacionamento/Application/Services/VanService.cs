using AutoMapper;
using Estacionamento.Application.Dto;
using Estacionamento.Contracts.Repositories;
using Estacionamento.Contracts.Services;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Enums;

namespace Estacionamento.Application.Services
{
    public class VanService : IVeiculoService
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IVagaRepository _vagaRepository;
        private readonly IMapper _mapper;

        public TipoVeiculo TipoVeiculo => TipoVeiculo.Van;

        public VanService(IVeiculoRepository veiculoRepository, IVagaRepository vagaRepository, IMapper mapper)
        {
            _veiculoRepository = veiculoRepository;
            _vagaRepository = vagaRepository;
            _mapper = mapper;
        }

        public async Task CadastrarAsync(VeiculoRequestDto veiculoRequest)
        {
            var i = 0;
            var dataEntrada = DateTime.Now;
            var tipoVagas = new List<TipoVaga>() { TipoVaga.Carro, TipoVaga.Grande };

            var vagas = await _vagaRepository.ConsultarPorTipos(tipoVagas);

            if (!vagas.Any())
                throw new Exception("Solicitar criação do estacionamento de vans");

            var temVaga = VerificaVagas(vagas);

            if (!temVaga.Item1)
                throw new Exception("Não há vagas para vans");

            while (i < temVaga.Item3)
            {
                var veiculo = MontarVeiculo(veiculoRequest, temVaga.Item2, dataEntrada);
                await _veiculoRepository.CadastrarVeiculoAsync(veiculo);
                i++;
            }
        }

        public async Task AtualizarAsync(VeiculoRequestDto veiculoRequest, int id)
        {
            if (veiculoRequest.Id != id)
                throw new Exception("Van não encontrada");

            var veiculo = await _veiculoRepository.GetById(id);

            if (veiculo is null)
                throw new Exception("Van não encontrada");

            var vans = await _veiculoRepository.GetByEntrada(veiculo.Entrada);

            foreach (var van in vans)
            {
                van.Placa = veiculo.Placa;
                van.Saida = DateTime.Now;
                await _veiculoRepository.AtulizarVeiculoAsync(veiculo);
            }
        }

        public async Task<List<VeiculoResponseDto>> ListarAsync()
        {
            var veiculos = await _veiculoRepository.GetByTipo(TipoVeiculo.Van);

            return veiculos.Select(c => _mapper.Map<VeiculoResponseDto>(c)).ToList();
        }

        private Veiculo MontarVeiculo(VeiculoRequestDto veiculoRequest, int idVaga, DateTime dataEntrada)
        {
            var veiculo = _mapper.Map<Veiculo>(veiculoRequest);

            veiculo.Entrada = dataEntrada;
            veiculo.TipoVeiculo = TipoVeiculo.Van;
            veiculo.IdVaga = idVaga;

            return veiculo;
        }

        private static (bool, int, int) VerificaVagas(List<Vaga> vagas)
        {
            var tipoVagas = new List<TipoVaga>() { TipoVaga.Grande, TipoVaga.Carro };
            var temVaga = false;
            var quantidadeVagas = 0;
            var idVaga = 0;

            foreach (var tipo in tipoVagas)
            {
                var vagaEspecifica = vagas.Where(vaga => vaga.TipoVaga == tipo).First();

                var totalVagas = vagaEspecifica.Quantidade;
                var vagasEmUso = vagaEspecifica.Veiculos.Where(ve => ve.Saida is null).Count();

                if (vagasEmUso < totalVagas && vagaEspecifica.TipoVaga == TipoVaga.Grande)
                {
                    temVaga = true;
                    idVaga = vagaEspecifica.Id;
                    quantidadeVagas = 1;
                    break;
                }

                if ((totalVagas - vagasEmUso) / 3 >= 1 && vagaEspecifica.TipoVaga == TipoVaga.Carro)
                {
                    temVaga = true;
                    idVaga = vagaEspecifica.Id;
                    quantidadeVagas = 3;
                    break;
                }
            }

            return (temVaga, idVaga, quantidadeVagas);
        }
    }
}

