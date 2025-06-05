using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace msLicenca.Entity
{
    public class Licenca
    {
        public int Id { get; set; }

        public int IdEpresa { get; set; }
        public int IdTipoLicenca { get; set; }
        public DateOnly Data_emissao { get; set; }
        public DateOnly Data_validade { get; set; }
        public StatusLicenca Status { get; set; }

        public Licenca()
        {
            
        }

        public Licenca(int id_licenca, int idEpresa, int idTipoLicenca, DateOnly data_emissao, DateOnly data_validade, StatusLicenca status)
        {
            this.Id = id_licenca;
            this.IdEpresa = idEpresa;
            this.IdTipoLicenca = idTipoLicenca;
            this.Data_emissao = data_emissao;
            this.Data_validade = data_validade;
        }
    }
}
