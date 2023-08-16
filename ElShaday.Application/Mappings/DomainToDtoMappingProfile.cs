using AutoMapper;
using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.DTOs.Responses;
using ElShaday.Domain.Entities.User;

namespace ElShaday.Application.Mappings;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<User, UserRequestDto>()
            .ReverseMap();

        CreateMap<User, UserResponseDto>()
            .ReverseMap();
    }
}