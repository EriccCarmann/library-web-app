using AutoMapper;
using Library.Domain.Entities;
using LibraryWebApi.DTOs.AuthorDTOs;

namespace LibraryWebApi.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorCreateDto, Author>()
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.FirstName)
                )
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(src => src.LastName)
                )
                .ForMember(
                    dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth)
                )
                .ForMember(
                    dest => dest.Country,
                    opt => opt.MapFrom(src => src.Country)
                );

            CreateMap<Author, AuthorReadDto>()
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.FirstName)
                )
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(src => src.LastName)
                )
                .ForMember(
                    dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth)
                )
                .ForMember(
                    dest => dest.Country,
                    opt => opt.MapFrom(src => src.Country)
                );
        }
    }
}
