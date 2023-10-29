using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Enums;
using Estacionamento.Infrastructure.Contexts;
using Estacionamento.Contracts.Repositories;

namespace Estacionamento.Domain.Repositories
{
    public class VagaRepository : IVagaRepository
    {
        private readonly DataContext _context;

        public VagaRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Cadastrar(List<Vaga> vagas)
        {
            _context.Vagas.AddRange(vagas);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Vaga>> Consultar()
        {
            var vagas = await _context.Vagas
                .Include(v => v.Veiculos)
                .AsNoTracking()
                .ToListAsync();

            return vagas;
        }

        public async Task<List<Vaga>> ConsultarPorTipos(List<TipoVaga> tipoVagas)
        {
            var vagas = await _context.Vagas
                .Include(v => v.Veiculos)
                .Where(v => tipoVagas.Contains(v.TipoVaga))
                .AsNoTracking()
                .ToListAsync();

            return vagas;
        }
    }
}
