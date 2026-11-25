using AutoMapper;
using VisaService.Application.Models.Applicant;
using VisaService.Application.Models.Officer;
using VisaService.Application.Models.VisaApplication;
using VisaService.Domain.Entities;
using VisaService.ValueObjects;

namespace VisaService.Application.Services.Mapping;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        // Value Objects -> string
        CreateMap<LastName, string>().ConvertUsing(x => x.Value);
        CreateMap<FirstName, string>().ConvertUsing(x => x.Value);
        CreateMap<MiddleName, string>().ConvertUsing(x => x.Value);
        CreateMap<OfficerPosition, string>().ConvertUsing(x => x.Value);
        CreateMap<ApplicationNumber, string>().ConvertUsing(x => x.Value);

        CreateMap<VisaApplication, VisaApplicationModel>()
            .ForCtorParam(nameof(VisaApplicationModel.Number), opt => opt.MapFrom(src => src.Number.Value))
            .ForCtorParam(nameof(VisaApplicationModel.ApplicantId), opt => opt.MapFrom(src => src.Applicant.Id))
            .ForCtorParam(nameof(VisaApplicationModel.AssignedOfficerId),
                opt => opt.MapFrom(src => src.AssignedOfficer == null ? (Guid?)null : src.AssignedOfficer.Id));

        CreateMap<Applicant, ApplicantModel>()
            .ForCtorParam(nameof(ApplicantModel.LastName), opt => opt.MapFrom(src => src.LastName.Value))
            .ForCtorParam(nameof(ApplicantModel.FirstName), opt => opt.MapFrom(src => src.FirstName.Value))
            .ForCtorParam(nameof(ApplicantModel.MiddleName), opt => opt.MapFrom(src => src.MiddleName.Value))
            .ForCtorParam(nameof(ApplicantModel.Email), opt => opt.MapFrom(src => src.ContactInfo.Email.Value))
            .ForCtorParam(nameof(ApplicantModel.Phone), opt => opt.MapFrom(src => src.ContactInfo.Phone.Value))
            .ForMember(dest => dest.Applications, opt => opt.MapFrom(src => src.Applications));

        CreateMap<Officer, OfficerModel>()
            .ForCtorParam(nameof(OfficerModel.LastName), opt => opt.MapFrom(src => src.LastName.Value))
            .ForCtorParam(nameof(OfficerModel.FirstName), opt => opt.MapFrom(src => src.FirstName.Value))
            .ForCtorParam(nameof(OfficerModel.MiddleName), opt => opt.MapFrom(src => src.MiddleName.Value))
            .ForCtorParam(nameof(OfficerModel.Position), opt => opt.MapFrom(src => src.Position.Value));
    }
}
