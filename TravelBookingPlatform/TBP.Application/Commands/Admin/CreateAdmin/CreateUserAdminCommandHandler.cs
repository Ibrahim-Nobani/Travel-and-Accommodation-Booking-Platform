using System.Data;
using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
using TravelBookingPlatform.Application.Exceptions;
namespace TravelBookingPlatform.Application.Commands;

public class CreateUserAdminCommandHandler : IRequestHandler<CreateUserAdminCommand, UserAdminResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserAdminCommandHandler(IUserRepository userRepository, IMapper mapper, IPasswordHashService passwordHashService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHashService = passwordHashService;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserAdminResponseDto> Handle(CreateUserAdminCommand request, CancellationToken cancellationToken)
    {
        var isUsernameTaken = await _userRepository.IsUsernameTakenAsync(request.CreateUserAdminDto.Username);
        if (isUsernameTaken)
        {
            throw new DuplicateUsernameException();
        }

        var isEmailTaken = await _userRepository.IsEmailTakenAsync(request.CreateUserAdminDto.Email);
        if (isEmailTaken)
        {
            throw new DuplicateEmailException();
        }

        var user = _mapper.Map<User>(request.CreateUserAdminDto);

        var hashedPassword = _passwordHashService.HashPassword(request.CreateUserAdminDto.Password);
        user.PasswordHash = hashedPassword;

        _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        var userDto = _mapper.Map<UserAdminResponseDto>(user);
        return userDto;
    }
}