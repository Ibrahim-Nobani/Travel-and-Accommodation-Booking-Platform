using MediatR;
using TravelBookingPlatform.Application.Authorization;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Interfaces;

namespace TravelBookingPlatform.Application.Queries;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, UserLoginResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHashService;
    private readonly ITokenService _tokenService;

    public LoginUserQueryHandler(IUserRepository userRepository, IPasswordHashService passwordHashService, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
        _tokenService = tokenService;
    }

    public async Task<UserLoginResponseDto> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.LoginUserDto.Username);

        if (user == null)
        {
            throw new InvalidCredentialsException();
        }

        if (!_passwordHashService.VerifyPassword(request.LoginUserDto.Password, user.PasswordHash))
        {
            throw new InvalidCredentialsException();
        }

        var accessToken = await _tokenService.GenerateTokenAsync(user);

        return new UserLoginResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            AccessToken = accessToken
        };
    }
}