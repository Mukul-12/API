using Web.Models.DTO;
using AutoMapper;

namespace API
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<StudentDTO, createStudentDTO>().ReverseMap();
            CreateMap<StudentDTO, updateStudentDTO>().ReverseMap();
        }
        
    }
}
