namespace TaskManagementSystem.Models
{
    public class ReportModel
    {
        public int TotalTasks { get; set; }
        public int PendingTasks { get; set; }
        public int InProgressTasks { get; set; }
        public int CompletedTasks { get; set; }
        public double CompletedPercentage => TotalTasks == 0 ? 0 : Math.Round((double)CompletedTasks / TotalTasks * 100, 2);
    }
}
