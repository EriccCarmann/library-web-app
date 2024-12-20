﻿using AutoMapper;
using Library.Domain.Entities;
using Library.Application.DTOs.AuthorDTOs;

namespace Library.Application.Profiles.AuthorProfiles
{
    public class AuthorCreateProfile : Profile
    {
        public AuthorCreateProfile()
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
        }
    }
}
