using POS.Shared.Entities;
namespace POS.Server.Repositories
{
    public interface IMarcaRepository
    {
        Task<List<Marca>> GetAllAsync();
        Task<Marca?> GetByIdAsync(int id);
        Task<Marca?> GetByNameAsync(string nombre);
        Task<Marca> CreateAsync(Marca marca);
        Task<Marca> UpdateAsync(Marca marca);
        Task<bool> DeleteAsync(int id);
    }
}