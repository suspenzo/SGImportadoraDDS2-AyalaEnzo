using POS.Server.Data;
using POS.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace POS.Server.Repositories
{
    namespace POS.Server.Repositories
    {
        public class ClientRepository : IClientRepository
        {
            private readonly AppDbContext _context;

            public ClientRepository(AppDbContext context) => _context = context;

            public async Task<List<Client>> GetAllAsync() =>
                await _context.Clients.ToListAsync();

            public async Task<Client?> GetByIdAsync(int id) =>
                await _context.Clients.FindAsync(id);

            public async Task<Client?> GetByDocumentAsync(string numDocumento) =>
                await _context.Clients.FirstOrDefaultAsync(c => c.NumDocumento == numDocumento);

            public async Task<Client> CreateAsync(Client client)
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                return client;
            }

            public async Task<Client> UpdateAsync(Client client)
            {
                _context.Clients.Update(client);
                await _context.SaveChangesAsync();
                return client;
            }

            public async Task<bool> DeleteAsync(int id)
            {
                var client = await _context.Clients.FindAsync(id);
                if (client == null) return false;

                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
