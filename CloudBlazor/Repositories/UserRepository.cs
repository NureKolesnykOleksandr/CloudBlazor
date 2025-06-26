using CloudBlazor.Interfaces;
using CloudBlazor.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace CloudBlazor.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<User> CreateAsync(User user)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var sql = """
                INSERT INTO Users 
                (FirstName, LastName, Email, PasswordHash, Role, CreatedAt, LastLogin, IsActive) 
                VALUES 
                (@FirstName, @LastName, @Email, @PasswordHash, @Role, @CreatedAt, @LastLogin, @IsActive);
                SELECT CAST(SCOPE_IDENTITY() as int);
                """;

            user.UserID = await db.ExecuteScalarAsync<int>(sql, user);
            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var sql = "DELETE FROM Users WHERE UserID = @Id";

            int affectedRows = await db.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return await db.QueryAsync<User>("SELECT * FROM Users WHERE IsActive = 1");
        }

        public async Task<bool> UpdateAsync(User user)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var sql = """
                UPDATE Users SET 
                FirstName = @FirstName,
                LastName = @LastName,
                Email = @Email,
                PasswordHash = @PasswordHash,
                Role = @Role,
                CreatedAt = @CreatedAt,
                LastLogin = @LastLogin,
                IsActive = @IsActive
                WHERE UserID = @UserID
                """;

            int affectedRows = await db.ExecuteAsync(sql, user);
            return affectedRows > 0;
        }
    }
}