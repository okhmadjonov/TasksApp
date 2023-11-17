using System.ComponentModel.DataAnnotations;

namespace TasksControllerApp.Models
{
    public class TaskItem
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Due Date is required")]
        [DataType(DataType.Date)]
        public DateTimeOffset DueDate { get; set; }
        public DateTimeOffset UtcDueDate => DueDate.ToUniversalTime();

        [Required(ErrorMessage = "Status is required")]
        public TaskStatus Status { get; set; }
    }
}
