using AutoMapper;
using Transacao.API.Commands;
using Transacao.API.Models;
using Transacao.Domain.Entities;

namespace Transacao.API.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //Request
            CreateMap<TransacaoEntity, TransacaoInclusaoRequest>().ReverseMap();

            //Response
            CreateMap<TransacaoEntity, TransacaoResponse>().ReverseMap();
            CreateMap<ContaEntity, ContaResponse>().ReverseMap();
            CreateMap<ContaEntity, ContaModel>().ReverseMap();
        }
    }
}
