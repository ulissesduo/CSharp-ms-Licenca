using msLicenca.Entity;
using msLicenca.Repository;

namespace msLicenca.Service
{
    public class LicencaService : ILicencaService
    {
        private readonly ILicencaRepository _repository;

        public LicencaService(ILicencaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Licenca> CreateLicencaAsync(Licenca licenca)
        {
            return await _repository.criarLicenca(licenca);
        }

        public async Task<bool> DeleteLicencaAsync(int id)
        {
            var existingId = await _repository.licencaById(id);
            if(existingId == null) return false;
            return await _repository.deleteLicencencaById(id);
            
        }

        public async Task<List<Licenca>> GetAllLicencasAsync()
        {
            return await _repository.listLicencas();    
        }

        public async Task<Licenca?> GetLicencaByIdAsync(int id)
        {
            return await _repository.licencaById(id);
        }

        public async Task<Licenca?> UpdateLicencaAsync(int id, Licenca licenca)
        {
            return await _repository.atualizaLicenca(id,licenca);
        }
    }
}
