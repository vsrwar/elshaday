using AutoMapper;
using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.DTOs.Responses;
using ElShaday.Domain.Entities;
using ElShaday.Domain.Entities.Department;
using ElShaday.Domain.Entities.Person;
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
        
        CreateMap<DepartmentForLegalPersonRequestDto, Department>()
            .ForMember(x => x.LegalPersonId, opt => opt.MapFrom(y => y.LegalPersonId))
            .ForMember(x => x.PersonType, opt => opt.MapFrom(y => y.Type))
            .ReverseMap();
        CreateMap<DepartmentForPhysicalPersonRequestDto, Department>()
            .ForMember(x => x.PhysicalPersonId, opt => opt.MapFrom(y => y.PhysicalPersonId))
            .ForMember(x => x.PersonType, opt => opt.MapFrom(y => y.Type))
            .ReverseMap();
        CreateMap<Department, DepartmentResponseDto>()
            .ReverseMap();
        
        CreateMap<LegalPerson, LegalPersonRequestDto>()
            .ForMember(x => x.Cnpj, opt => opt.MapFrom(y => y.Document.Value))
            .ForMember(x => x.Cnpj, opt => opt.MapFrom(y => y.Document.Value))
            .ReverseMap();
        CreateMap<LegalPerson, LegalPersonResponseDto>()
            .ForMember(x => x.Cnpj, opt => opt.MapFrom(y => y.Document.Value))
            .ReverseMap();
        
        CreateMap<PhysicalPerson, PhysicalPersonRequestDto>()
            .ForMember(x => x.Cpf, opt => opt.MapFrom(y => y.Document.Value))
            .ReverseMap();
        CreateMap<PhysicalPerson, PhysicalPersonResponseDto>()
            .ForMember(x => x.Cpf, opt => opt.MapFrom(y => y.Document.Value))
            .ReverseMap();

        CreateMap<Address, AddressRequestDto>()
            .ReverseMap();
        CreateMap<Address, AddressResponseDto>()
            .ReverseMap();
    }
}