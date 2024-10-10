﻿using AutoMapper;
using Library.Domain.Entities;
using Library.Domain.Entities.BookDTOs;

namespace Library.Infrastructure.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookCreateDto, Book>()
                .ForMember(
                    dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(
                    dest => dest.ISBN,
                    opt => opt.MapFrom(src => src.ISBN))
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description))
                .ForMember(
                    dest => dest.Genre,
                    opt => opt.MapFrom(src => src.Genre)
                )
                .ForMember(
                    dest => dest.Author,
                    opt => opt.MapFrom(src => src.Author)
                );

            CreateMap<Book, BookReadDto>()
                .ForMember(
                    dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(
                    dest => dest.ISBN,
                    opt => opt.MapFrom(src => src.ISBN))
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description))
                .ForMember(
                    dest => dest.Genre,
                    opt => opt.MapFrom(src => src.Genre)
                )
                .ForMember(
                    dest => dest.IsTaken,
                    opt => opt.MapFrom(src => src.IsTaken)
                );
        }
    }
}