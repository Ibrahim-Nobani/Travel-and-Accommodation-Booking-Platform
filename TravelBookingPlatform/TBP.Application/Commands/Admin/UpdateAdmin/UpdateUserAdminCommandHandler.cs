using System.Data;
using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Infrastructure.Interfaces;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;

public class UpdateUserAdminCommandHandler : IRequestHandler<UpdateUserAdminCommand, UserAdminResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserAdminCommandHandler(IUserRepository userRepository, IMapper mapper, IPasswordHashService passwordHashService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHashService = passwordHashService;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserAdminResponseDto> Handle(UpdateUserAdminCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
        {
            throw new EntityNotFoundException(nameof(user));
        }

        var isUsernameTaken = await _userRepository.IsUsernameTakenAsync(request.UpdateUserAdminDto.Username);
        if (isUsernameTaken && user.Username != request.UpdateUserAdminDto.Username)
        {
            throw new DuplicateUsernameException();
        }

        var isEmailTaken = await _userRepository.IsEmailTakenAsync(request.UpdateUserAdminDto.Email);
        if (isEmailTaken && user.Email != request.UpdateUserAdminDto.Email)
        {
            throw new DuplicateEmailException();
        }

        _mapper.Map(request.UpdateUserAdminDto, user);

        var hashedPassword = _passwordHashService.HashPassword(request.UpdateUserAdminDto.Password);
        user.PasswordHash = hashedPassword;

        _userRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();

        var userDto = _mapper.Map<UserAdminResponseDto>(user);
        return userDto;
    }
}