using CloudBlazor.Models;

namespace CloudBlazor.Interfaces
{
    public interface IClassroomRepository
    {
        Task<Classroom> CreateAsync(Classroom classroom);
        Task<IEnumerable<Classroom>> GetAllAsync();
        Task<bool> UpdateAsync(Classroom classroom);
        Task<bool> DeleteAsync(int id);
    }
}
