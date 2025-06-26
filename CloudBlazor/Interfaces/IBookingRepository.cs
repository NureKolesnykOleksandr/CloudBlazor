using CloudBlazor.Models;

namespace CloudBlazor.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> CreateAsync(Booking booking);
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<bool> UpdateAsync(Booking booking);
        Task<bool> DeleteAsync(int id);
    }
}
