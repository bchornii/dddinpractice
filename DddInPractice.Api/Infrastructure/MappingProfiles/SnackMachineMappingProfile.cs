using AutoMapper;
using DddInPractice.Api.Models;
using DddInPractice.CommandHandlers.Commands;
using DddInPractice.Commands.Commands;

namespace DddInPractice.Api.Infrastructure.MappingProfiles
{
    public class SnackMachineMappingProfile : Profile
    {
        public SnackMachineMappingProfile()
        {
            CreateMap<InitializeSnakMachineDto, InitializeSnakMachineCommand>();

            CreateMap<AddSnackMachineSlotDto, AddSnackMachineSlotCommand>();

            CreateMap<BuySnackDto, BuySnackCommand>();
        }
    }
}
