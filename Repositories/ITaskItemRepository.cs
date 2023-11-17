using TasksControllerApp.Dto;
using TasksControllerApp.Models;

namespace TasksControllerApp.Repositories
{
    public interface ITaskItemRepository
    {
        Task<List<TaskItem>> GetAll();
        Task<TaskItem> Get(string id);
        Task Add(string userId, string userName, TaskItemDto taskDto);
        Task Update(string userId, string userName, string id, TaskItemDto taskDto);
        Task Delete(string userId, string userName, string id);
       
        Task<RoleTaskDto> RetrieveDto(string userId, string userName, string role);
    }
}
