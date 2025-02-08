using TaskManagementSystem.Models;

namespace TaskManagementSystem.Repositories
{
    public interface ITaskRepository
    {
        //Task<IEnumerable<TasksModel>> GetAllTask();
        Task<IEnumerable<TasksModel>> GetAllTask(string searchTerm = "", int page = 1, int pageSize = 10);
        IEnumerable<StatusModel> GetStatus();
        IEnumerable<PriorityModel> GetPriority();
        Task<TasksModel> GetTaskById(int id);
        Task<int> AddTask(TasksModel Task);
        Task<bool> UpdateTask(TasksModel Task);
        Task<bool> DeleteTask(int id);
        Task<IEnumerable<ReportModel>> GetReportAsync();
    }
}
