using System.Data;
using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
using TravelBookingPlatform.Application.Exceptions;

namespace TravelBookingPlatform.Application.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IPasswordHashService passwordHashService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHashService = passwordHashService;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isUsernameTaken = await _userRepository.IsUsernameTakenAsync(request.CreateUserDto.Username);
        if (isUsernameTaken)
        {
            throw new DuplicateUsernameException();
        }

        var isEmailTaken = await _userRepository.IsEmailTakenAsync(request.CreateUserDto.Email);
        if (isEmailTaken)
        {
            throw new DuplicateEmailException();
        }

        var user = _mapper.Map<User>(request.CreateUserDto);

        var hashedPassword = _passwordHashService.HashPassword(request.CreateUserDto.Password);
        user.PasswordHash = hashedPassword;
        user.RoleId = (int)UserRoles.User;

        _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        var userDto = _mapper.Map<UserResponseDto>(user);
        return userDto;
    }
}