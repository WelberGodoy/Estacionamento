using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Enums;

namespace Estacionamento.Contracts.Repositories
{
    public interface IVagaRepository
    {
        public Task Cadastrar(List<Vaga> vagas);
        public Task<List<Vaga>> Consultar();
        public Task<List<Vaga>> ConsultarPorTipos(List<TipoVaga> tipoVaga);
    }
}
