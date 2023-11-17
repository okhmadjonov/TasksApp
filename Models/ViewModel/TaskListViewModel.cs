namespace TasksControllerApp.Models.ViewModel
{
    public class TaskListViewModel
    {
        public List<TaskItem> Tasks { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}
