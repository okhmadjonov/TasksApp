using TasksControllerApp.Entities;
using TasksControllerApp.Models;

namespace TasksControllerApp.Dto
{
    public class RoleTaskDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public List<TaskItem> Tasks { get; set; }
    }
}
