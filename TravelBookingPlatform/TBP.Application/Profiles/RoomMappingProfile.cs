using AutoMapper;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.MappingProfiles;

public class RoomMappingProfile : Profile
{
    public RoomMappingProfile()
    {
        RoomMapping();
    }

    private void RoomMapping()
    {
        CreateMap<CreateRoomDto, Room>();
        CreateMap<Room, CreateRoomDto>();

        CreateMap<UpdateRoomDto, Room>();
        CreateMap<Room, UpdateRoomDto>();

        CreateMap<RoomDto, Room>();
        CreateMap<Room, RoomDto>();
    }
}