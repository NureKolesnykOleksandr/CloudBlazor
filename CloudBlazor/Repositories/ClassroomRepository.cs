using CloudBlazor.Interfaces;
using CloudBlazor.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

public class ClassroomRepository : IClassroomRepository
{
    private readonly string _connectionString;

    public ClassroomRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    private SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public async Task<Classroom> CreateAsync(Classroom classroom)
    {
        using IDbConnection db = new SqlConnection(_connectionString);

        var sql = """
                INSERT INTO Classrooms 
                (RoomNumber, Capacity, Building, Floor, HasProjector, HasWhiteboard, IsActive, Description) 
                VALUES 
                (@RoomNumber, @Capacity, @Building, @Floor, @HasProjector, @HasWhiteboard, @IsActive, @Description);
                SELECT CAST(SCOPE_IDENTITY() as int);
                """;

        classroom.ClassroomID = await db.ExecuteScalarAsync<int>(sql, classroom);
        return classroom;
    }

    public async Task<IEnumerable<Classroom>> GetAllAsync()
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        var result = await db.QueryAsync<Classroom>("SELECT * FROM Classrooms WHERE IsActive = 1");
        return result;
    }

    public async Task<bool> UpdateAsync(Classroom classroom)
    {
        using IDbConnection db = new SqlConnection(_connectionString);

        var sql = """
                UPDATE Classrooms SET 
                RoomNumber = @RoomNumber,
                Capacity = @Capacity,
                Building = @Building,
                Floor = @Floor,
                HasProjector = @HasProjector,
                HasWhiteboard = @HasWhiteboard,
                IsActive = @IsActive,
                Description = @Description
                WHERE ClassroomID = @ClassroomID
                """;

        int affectedRows = await db.ExecuteAsync(sql, classroom);
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using IDbConnection db = new SqlConnection(_connectionString);

        var sql = "DELETE FROM Classrooms WHERE ClassroomID = @Id";

        int affectedRows = await db.ExecuteAsync(sql, new { Id = id });
        return affectedRows > 0;
    }
}