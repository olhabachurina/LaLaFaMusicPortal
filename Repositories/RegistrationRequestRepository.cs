using LaLaFaMusicPortal.Data;
using LaLaFaMusicPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace LaLaFaMusicPortal.Repositories
{
    public class RegistrationRequestRepository : IRepository<RegistrationRequest>
    {
        private readonly MusicPortalContext _context;

        public RegistrationRequestRepository(MusicPortalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RegistrationRequest>> GetAllAsync()
        {
            return await _context.RegistrationRequests.ToListAsync();
        }

        public async Task<RegistrationRequest> GetByIdAsync(int id)
        {
            return await _context.RegistrationRequests.FindAsync(id);
        }

        public async Task AddAsync(RegistrationRequest registrationRequest)
        {
            await _context.RegistrationRequests.AddAsync(registrationRequest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RegistrationRequest registrationRequest)
        {
            _context.RegistrationRequests.Update(registrationRequest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var registrationRequest = await _context.RegistrationRequests.FindAsync(id);
            if (registrationRequest != null)
            {
                _context.RegistrationRequests.Remove(registrationRequest);
                await _context.SaveChangesAsync();
            }
        }
    }
}