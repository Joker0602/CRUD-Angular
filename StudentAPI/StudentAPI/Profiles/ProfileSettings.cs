using AutoMapper;
using StudentAPI.Dto;
using StudentAPI.Model;

namespace StudentAPI.Profiles
{
    public class ProfileSettings:Profile
    {
        public ProfileSettings() { 
            CreateMap<StudentMaster,StudentReadDto>().ReverseMap();
            CreateMap<StudentMaster,StudentCreateDto>().ReverseMap();
            CreateMap<StudentMaster,StudentUpdateDto>().ReverseMap();
        }
    }
}
