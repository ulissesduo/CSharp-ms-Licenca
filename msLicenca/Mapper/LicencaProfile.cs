using AutoMapper;
using msLicenca.Dto;
using msLicenca.Entity;

namespace msLicenca.Mapper
{
    public class LicencaProfile : Profile
    {
        public LicencaProfile() 
        {
            CreateMap<Licenca, LicencaRequestDTO>().ReverseMap();

            CreateMap<Licenca, LicencaResponseDTO>();

        }

    }
}
