using CloudBlazor.Interfaces;
using CloudBlazor.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace CloudBlazor.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly string _connectionString;

        public BookingRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<Booking> CreateAsync(Booking booking)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var sql = """
                INSERT INTO Bookings 
                (UserID, ClassroomID, StartTime, EndTime, Purpose, Status, CreatedAt, ApprovedBy, ApprovalDate) 
                VALUES 
                (@UserID, @ClassroomID, @StartTime, @EndTime, @Purpose, @Status, @CreatedAt, @ApprovedBy, @ApprovalDate);
                SELECT CAST(SCOPE_IDENTITY() as int);
                """;

            booking.BookingID = await db.ExecuteScalarAsync<int>(sql, booking);
            return booking;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var sql = "DELETE FROM Bookings WHERE BookingID = @Id";

            int affectedRows = await db.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return await db.QueryAsync<Booking>("SELECT * FROM Bookings");
        }

        public async Task<bool> UpdateAsync(Booking booking)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var sql = """
                UPDATE Bookings SET 
                UserID = @UserID,
                ClassroomID = @ClassroomID,
                StartTime = @StartTime,
                EndTime = @EndTime,
                Purpose = @Purpose,
                Status = @Status,
                CreatedAt = @CreatedAt,
                ApprovedBy = @ApprovedBy,
                ApprovalDate = @ApprovalDate
                WHERE BookingID = @BookingID
                """;

            int affectedRows = await db.ExecuteAsync(sql, booking);
            return affectedRows > 0;
        }
    }
}