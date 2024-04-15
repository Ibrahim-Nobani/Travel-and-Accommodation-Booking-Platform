using System.Data;
using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
namespace TravelBookingPlatform.Application.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IPasswordHashService passwordHashService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHashService = passwordHashService;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserResponseDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
        {
            throw new EntityNotFoundException(nameof(user));
        }

        var isUsernameTaken = await _userRepository.IsUsernameTakenAsync(request.UpdateUserDto.Username);
        if (isUsernameTaken && user.Username != request.UpdateUserDto.Username)
        {
            throw new DuplicateUsernameException();
        }

        var isEmailTaken = await _userRepository.IsEmailTakenAsync(request.UpdateUserDto.Email);
        if (isEmailTaken && user.Email != request.UpdateUserDto.Email)
        {
            throw new DuplicateEmailException();
        }

        _mapper.Map(request.UpdateUserDto, user);

        var hashedPassword = _passwordHashService.HashPassword(request.UpdateUserDto.Password);
        user.PasswordHash = hashedPassword;

        _userRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();

        var userDto = _mapper.Map<UserResponseDto>(user);
        return userDto;
    }
}