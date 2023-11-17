using System.ComponentModel.DataAnnotations;

namespace TasksControllerApp.Models
{
    public class TaskItem
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DueDate { get; set; }

        public TaskStatus Status { get; set; }
    }
}
