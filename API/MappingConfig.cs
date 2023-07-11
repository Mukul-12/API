using API.Models;
using API.Models.DTO;
using AutoMapper;

namespace API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Student, createStudentDTO>().ReverseMap();
            CreateMap<Student, updateStudentDTO>().ReverseMap();
        }
        
    }
}
