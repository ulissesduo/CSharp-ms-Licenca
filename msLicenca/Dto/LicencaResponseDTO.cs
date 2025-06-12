using msLicenca.Entity;

namespace msLicenca.Dto
{
    public class LicencaResponseDTO
    {

        public int Id { get; set; }
        public int IdEpresa { get; set; }
        public int IdTipoLicenca { get; set; }
        public DateOnly Data_emissao { get; set; }
        public DateOnly Data_validade { get; set; }
        public StatusLicenca Status { get; set; }

    }
}
