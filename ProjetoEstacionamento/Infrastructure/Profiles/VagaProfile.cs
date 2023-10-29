using AutoMapper;
using Estacionamento.Application.Dto;
using Estacionamento.Domain.Entities;

namespace Estacionamento.Infrastructure.Profiles
{
    public class VagaProfile : Profile
    {
        public VagaProfile()
        {
            CreateMap<Vaga, VagaRequestDto>();
            CreateMap<Vaga, VagaResponseDto>();

            CreateMap<VagaRequestDto, Vaga>();
            CreateMap<VagaResponseDto, Vaga>();
        }
    }
}
