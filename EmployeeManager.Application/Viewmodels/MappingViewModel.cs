using AutoMapper;
using EmployeeManager.Application.Viewmodels.Employees;
using EmployeeManager.Application.Viewmodels.Positions;
using EmployeeManager.Core.Entities;
using System.Globalization;

namespace EmployeeManager.Application.Viewmodels;
public class MappingViewModel : Profile
{
    public MappingViewModel()
    {
        CreateMap<Position, PositionViewModel>(MemberList.Destination).ReverseMap();
        CreateMap<PositionCreateModel, Position>(MemberList.Destination).ReverseMap();
        CreateMap<PositionUpdateModel, Position>(MemberList.Destination).ReverseMap();

        CreateMap<Employee, EmployeeViewModel>(MemberList.Destination)
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
              .ForMember(dest => dest.Age, opt => opt.MapFrom(src => GetAge(src.BirthDate)))
              .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
              .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.BirthDate.ToLocalTime().DateTime)));

        CreateMap<EmployeeCreateModel, Employee>(MemberList.Destination)
              .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => TryParseDateTimeOffset(src.BirthDate)));

        CreateMap<EmployeeUpdateModel, Employee>(MemberList.Destination)
              .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => TryParseDateTimeOffset(src.BirthDate)));
    }

    private long GetAge(DateTimeOffset birthDate)
    {
        DateTimeOffset endDate = DateTimeOffset.UtcNow;

        DateTime startDateTime = birthDate.UtcDateTime;
        DateTime endDateTime = endDate.UtcDateTime;

        long age = endDateTime.Year - startDateTime.Year;

        if (endDateTime.Month < startDateTime.Month ||
           (endDateTime.Month == startDateTime.Month && endDateTime.Day < startDateTime.Day))
        {
            age--;
        }

        return age;
    }

    public DateTimeOffset TryParseDateTimeOffset(string dateString)
    {
        string format = "dd/MM/yyyy";

        DateTimeOffset dateTimeOffset = DateTimeOffset.ParseExact(
            dateString,
            format,
            CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeUniversal
        );

        return dateTimeOffset;
    }
}

