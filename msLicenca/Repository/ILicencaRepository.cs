using msLicenca.Entity;

namespace msLicenca.Repository
{
    public interface ILicencaRepository
    {
        Task<Licenca> criarLicenca(Licenca licenca);

        Task<List<Licenca>> listLicencas();

        Task<Licenca?> licencaById(int id);

        Task<Licenca?> atualizaLicenca(int id, Licenca licenca);

        Task<bool> deleteLicencencaById(int id);
    }
}
