using Microsoft.EntityFrameworkCore;
using msLicenca.Data;
using msLicenca.Entity;
using System.ComponentModel;

namespace msLicenca.Repository
{
    public class LicencaRepository : ILicencaRepository
    {
        private readonly DataContext _context;
        public LicencaRepository(DataContext context) 
        {
            _context = context;
        }
        

        public async Task<Licenca?> licencaById(int id)
        {
            return await _context.Licenca
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Licenca>> listLicencas()
        {
            return await _context.Licenca
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Licenca?> atualizaLicenca(int id, Licenca licenca)
        {
            var existingLicence = await _context.Licenca.FindAsync(id);
            if (existingLicence == null) return null;

            existingLicence.IdEpresa = licenca.IdEpresa;
            existingLicence.IdTipoLicenca = licenca.IdTipoLicenca;
            existingLicence.Data_validade = licenca.Data_validade;
            existingLicence.Data_emissao = licenca.Data_emissao;
            existingLicence.Status = licenca.Status;

            try
            {
                await _context.SaveChangesAsync();
                return existingLicence;
            }
            catch (DbUpdateException ex)
            {
                throw;
            }
        }

        public async Task<Licenca> criarLicenca(Licenca licenca)
        {
            try
            {
                await _context.Licenca.AddAsync(licenca);
                await _context.SaveChangesAsync();
                return licenca;
            }
            catch (DbUpdateException ex)
            {
                
                throw;
            }
        }

        public async Task<bool> deleteLicencencaById(int id)
        {
            var licenca = await _context.Licenca.FindAsync(id);
            if (licenca == null) return false;

            _context.Licenca.Remove(licenca);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                
                throw;
            }
        }


    }
}
