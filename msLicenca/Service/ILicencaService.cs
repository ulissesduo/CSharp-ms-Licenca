using msLicenca.Entity;

namespace msLicenca.Service
{
    public interface ILicencaService
    {
        /// <summary>
        /// Get all Licenca records.
        /// </summary>
        Task<List<Licenca>> GetAllLicencasAsync();

        /// <summary>
        /// Get a single Licenca by its ID.
        /// </summary>
        Task<Licenca?> GetLicencaByIdAsync(int id);

        /// <summary>
        /// Create a new Licenca.
        /// </summary>
        Task<Licenca> CreateLicencaAsync(Licenca licenca);

        /// <summary>
        /// Update an existing Licenca by ID.
        /// </summary>
        Task<Licenca?> UpdateLicencaAsync(int id, Licenca licenca);

        /// <summary>
        /// Delete a Licenca by ID.
        /// </summary>
        Task<bool> DeleteLicencaAsync(int id);
    }
}
