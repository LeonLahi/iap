using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Dtos.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = String.Empty;
        public string Username { get; set; } = String.Empty;
        public string? DisplayName { get; set; }
        public string Email { get; set; } = String.Empty;        
    }
}