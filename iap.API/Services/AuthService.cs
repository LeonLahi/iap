using iap.API.Dtos.Auth;
using iap.API.Interfaces;
using iap.API.Models;
using Microsoft.AspNetCore.Identity;
using iap.API.Common;

namespace iap.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthService (UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<Result<AuthResponseDto>> RegisterAsync(RegisterDto dto)
        {
            // Check if the email is already in use
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser is not null)
                return Result<AuthResponseDto>.Conflict("Email is already in use.");

            var user = new User
            {
                UserName = dto.Username,
                Email = dto.Email,
                DisplayName = dto.DisplayName,
                CreatedAt = DateTimeOffset.UtcNow
            };

            // Add new user to db
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return Result<AuthResponseDto>.ValidationError(
                    string.Join(", ", result.Errors.Select(e => e.Description)));

            var token = _tokenService.GenerateToken(user);

            // Return the token and user info
            var response = new AuthResponseDto
            {
                Token = token,
                Username = user.UserName!,
                DisplayName = user.DisplayName,
                Email = user.Email!
            };

            return Result<AuthResponseDto>.Success(response);
        }
    }
}