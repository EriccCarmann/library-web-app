﻿namespace Library.Application.DTOs.LibraryUserDTOs
{
    public class ShowLoggedInUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
