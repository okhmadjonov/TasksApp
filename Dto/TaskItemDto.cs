using System.ComponentModel.DataAnnotations;

namespace TasksControllerApp.Dto
{
    public class TaskItemDto
    {

       
        public string Title { get; set; }

      
        public string Description { get; set; }

      
        public DateTimeOffset DueDate { get; set; }
        public DateTimeOffset UtcDueDate => DueDate.ToUniversalTime();

        public TaskStatus Status { get; set; }

    }
}
