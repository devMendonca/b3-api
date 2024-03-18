using AutoMapper;
using b3.api.DTO.Model;
using b3_domain.Model;

namespace b3.api.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TarefaDto, Tarefa>().ReverseMap(); ;
        }
    }
}
