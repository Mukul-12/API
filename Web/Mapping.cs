using Web.Models.DTO;
using AutoMapper;

namespace Web
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
