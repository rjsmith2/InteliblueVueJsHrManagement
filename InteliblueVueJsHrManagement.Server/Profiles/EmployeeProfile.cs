// This file is provided freely for use, modification, and distribution without restriction. 
// No specific license applies, and you are free to share, modify, and redistribute this file as needed.


using AutoMapper;

namespace InteliblueVueJsHrManagement.Server.Profiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Models.DbSets.Employee, Models.Dtos.Employee>();
    }
}
