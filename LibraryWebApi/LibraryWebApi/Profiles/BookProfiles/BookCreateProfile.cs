using AutoMapper;
using Library.Domain.Entities;
using LibraryWebApi.DTOs.BookDTOs;

namespace LibraryWebApi.Profiles.BookProfiles
{
    public class BookCreateProfile : Profile
    {
        public BookCreateProfile()
        {
            CreateMap<BookCreateDto, Book>()
                .ForMember(
                    dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(
                    dest => dest.Genre,
                    opt => opt.MapFrom(src => src.Genre)
                )
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description))
                .ForMember(
                    dest => dest.ISBN,
                    opt => opt.MapFrom(src => src.ISBN))
                 .ForMember(
                    dest => dest.AuthorId,
                    opt => opt.MapFrom(src => src.AuthorId))
                ;
        }
    }
}
