using QLNH_Client.Models;

namespace QLNH_Client.Repositories
{
    public interface IGuestTableRepository
    {
        Task<List<GuestTable>> GetAllAsync();
        Task<GuestTable> GetByIdAsync(int id);
        Task<GuestTable> CreateAsync(GuestTable guestTable);
        Task<GuestTable> UpdateAsync(GuestTable guestTable);
        Task<GuestTable> DeleteAsync(int id);
        Task<GuestTable> RestoreAsync(int id);
    }
}
