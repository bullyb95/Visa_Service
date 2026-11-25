using AutoMapper;
using VisaService.Application.Models.Applicant;
using VisaService.Application.Models.Officer;
using VisaService.Application.Models.VisaApplication;
using VisaService.WebHost.Requests.Applicant;
using VisaService.WebHost.Requests.Officer;
using VisaService.WebHost.Requests.VisaApplication;
using VisaService.WebHost.Responses.Applicant;
using VisaService.WebHost.Responses.Officer;
using VisaService.WebHost.Responses.VisaApplication;

namespace VisaService.WebHost.Mapping;

public class PresentationProfile : Profile
{
    public PresentationProfile()
    {
        CreateMap<ApplicantModel, ApplicantShortResponse>();
        CreateMap<ApplicantModel, ApplicantDetailedResponse>();
        CreateMap<CreateApplicantRequest, CreateApplicantModel>();

        CreateMap<OfficerModel, OfficerShortResponse>();
        CreateMap<OfficerModel, OfficerDetailedResponse>();
        CreateMap<CreateOfficerRequest, CreateOfficerModel>();

        CreateMap<VisaApplicationModel, VisaApplicationShortResponse>();
        CreateMap<VisaApplicationModel, VisaApplicationDetailedResponse>();
        CreateMap<VisaApplicationResultModel, VisaApplicationResultResponse>();
        CreateMap<CreateVisaApplicationRequest, CreateVisaApplicationModel>();
        CreateMap<ApplicantActionRequest, ApplicantActionModel>();
        CreateMap<OfficerActionRequest, OfficerActionModel>();
    }
}
