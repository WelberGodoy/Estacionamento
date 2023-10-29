using AutoMapper;
using Estacionamento.Application.Dto;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Extensions;

namespace Estacionamento.Infrastructure.Profiles
{
    public class VeiculoProfile : Profile
    {
        public VeiculoProfile()
        {
            CreateMap<Veiculo, VeiculoRequestDto>();
            CreateMap<Veiculo, VeiculoResponseDto>()
                .ForMember(dto => dto.TipoVeiculo, opts => opts.MapFrom(domain => domain.TipoVeiculo.GetDescription()));

            CreateMap<VeiculoRequestDto, Veiculo>();
            CreateMap<VeiculoResponseDto, Veiculo>();
        }
    }
}
