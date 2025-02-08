using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDbConnection _connection;

        public TaskRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<TasksModel>> GetAllTask(string searchTerm = "", int page = 1, int pageSize = 5)
        {
            try
            {
                var size = pageSize;
                var query = @"
        SELECT 
            t.Id, 
            t.Title, 
            t.Description, 
            t.CreatedAt, 
            t.DueDate,
            t.StatusId, 
            s.Title AS StatusTitle,  
            t.PriorityId, 
            p.Title AS PriorityTitle 
        FROM Tasks t
        LEFT JOIN Status s ON t.StatusId = s.Id
        LEFT JOIN Priority p ON t.PriorityId = p.Id
        WHERE (@SearchTerm = '' OR t.Title LIKE @SearchTerm 
            OR s.Title LIKE @SearchTerm
            OR p.Title LIKE @SearchTerm)
        ORDER BY t.Title ASC
        OFFSET @Offset ROWS
        FETCH NEXT @PageSize ROWS ONLY";

                var parameters = new
                {
                    SearchTerm = $"%{searchTerm}%",
                    Offset = (page - 1) * 5,
                    PageSize = 5
                };

                return await _connection.QueryAsync<TasksModel>(query, parameters);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        //public async Task<IEnumerable<TasksModel>> GetAllTask()
        //{
        //    string query = @"SELECT 
        //    t.Id, 
        //    t.Title, 
        //    t.Description, 
        //    t.CreatedAt, 
        //    t.DueDate, 
        //    t.StatusId, 
        //    s.Title AS StatusTitle,  
        //    t.PriorityId, 
        //    p.Title AS PriorityTitle 
        //FROM Tasks t
        //LEFT JOIN Status s ON t.StatusId = s.Id
        //LEFT JOIN Priority p ON t.PriorityId = p.Id";
        //    return await _connection.QueryAsync<TasksModel>(query);
        //}


        public IEnumerable<StatusModel> GetStatus()
        {

                string query = "SELECT Id, Title FROM Status";
                return _connection.Query<StatusModel>(query).ToList();
           
        }
        public IEnumerable<PriorityModel> GetPriority()
        {

                string query = "SELECT Id, Title FROM Priority";
                return _connection.Query<PriorityModel>(query).ToList();
           
        }

        public async Task<TasksModel> GetTaskById(int id)
        {
            try
            {
                string query = "SELECT * FROM Tasks WHERE Id = @Id";
                return await _connection.QueryFirstOrDefaultAsync<TasksModel>(query, new { Id = id });
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }


        public async Task<int> AddTask(TasksModel task)
        {
            try
            {
                string query = "INSERT INTO Tasks (Title, Description, DueDate, StatusId, PriorityId) VALUES (@Title, @Description, @DueDate, @StatusId, @PriorityId); SELECT CAST(SCOPE_IDENTITY() as int)";
                return await _connection.ExecuteScalarAsync<int>(query, task);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> UpdateTask(TasksModel task)
        {
            try
            {
                string query = "UPDATE Tasks SET Title = @Title, Description = @Description, DueDate = @DueDate, StatusId = @StatusId, PriorityId = @PriorityId WHERE Id = @Id";
                int rowsAffected = await _connection.ExecuteAsync(query, task);
                return rowsAffected > 0;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> DeleteTask(int id)
        {
            try
            {
                string query = "DELETE FROM Tasks WHERE Id = @Id";
                int rowsAffected = await _connection.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }



        public async Task<IEnumerable<ReportModel>> GetReportAsync()
        {
            try
            {
                var sql = @"
                SELECT 
                    (SELECT COUNT(*) FROM Tasks) AS TotalTasks,
                    (SELECT COUNT(*) FROM Tasks t LEFT JOIN Status s ON t.StatusId = s.Id WHERE s.Title = 'Pending') AS PendingTasks,
                    (SELECT COUNT(*) FROM Tasks t LEFT JOIN Status s ON t.StatusId = s.Id WHERE s.Title = 'In Progress') AS InProgressTasks,
                    (SELECT COUNT(*) FROM Tasks t LEFT JOIN Status s ON t.StatusId = s.Id WHERE s.Title = 'Completed') AS CompletedTasks";
                return await _connection.QueryAsync<ReportModel>(sql);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }
    }
}
