using TasksControllerApp.Dto;
using TasksControllerApp.Entities;
using TasksControllerApp.Entities.TaskViewModel;
using TasksControllerApp.Models;

namespace TasksControllerApp.Repositories
{
    public interface ITaskItemRepository
    {
        Task<List<TaskItem>> GetAll();
        Task<TaskItem> Get(int id);
        Task Add(TaskViewModel testViewModel, string userId, string username);
        Task Update(int id, TaskViewModel testViewModel, string userId, string username);
        Task Delete(int id, string userId, string username);
    }
}
