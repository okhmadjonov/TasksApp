using Microsoft.EntityFrameworkCore;
using System;
using TasksControllerApp.DataContext;
using TasksControllerApp.Dto;
using TasksControllerApp.Models;
using TasksControllerApp.Repositories;

namespace TasksControllerApp.Services
{
    public class TaskItemService : ITaskItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public TaskItemService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<TaskItem>> GetAll()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskItem> Get(string id)
        {
            var product = await _context.Tasks.FirstOrDefaultAsync(p => p.Id == id);
            return product ?? throw new BadHttpRequestException("Task not found.");
        }

        public async Task Add(string userId, string userName, TaskItemDto taskDto)
        {
            var task = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                Status = (Models.TaskStatus)taskDto.Status,
                
            };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync(userId, userName);
        }


        public async Task Update(string userId, string userName, string id, TaskItemDto taskDto)
        {
           
            var task = await _context.Tasks.FirstOrDefaultAsync(p => p.Id == id);
            if (task is not null)
            {
                task.Title= taskDto.Title;
                task.Description= taskDto.Description;
                task.DueDate= taskDto.DueDate;
                task.Status = (Models.TaskStatus)taskDto.Status;
                _context.Entry(task).State = EntityState.Modified;
                await _context.SaveChangesAsync(userId, userName);
            }
            else
            {
                throw new BadHttpRequestException("Task not found");
            }
        }

        public async Task Delete(string userId, string userName, string id)
        {
            var product = await _context.Tasks.FirstOrDefaultAsync(p => p.Id == id);
            if (product is not null)
            {
                _context.Tasks.Remove(product);
                await _context.SaveChangesAsync(userId, userName);
            }
        }

      

        public async Task<RoleTaskDto> RetrieveDto(string userId, string userName, string role)
        {
            RoleTaskDto roleTaskDto = new RoleTaskDto()
            {
                Id = userId,
                Name = userName,
                Role = role,
                Tasks = await GetAll()
            };

            return roleTaskDto;
        }
    }
}
