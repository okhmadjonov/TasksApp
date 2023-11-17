using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasksControllerApp.DataContext;
using TasksControllerApp.Dto;
using TasksControllerApp.Models;
using TasksControllerApp.Models.ViewModel;
using TasksControllerApp.Repositories;

namespace TasksControllerApp.Controllers
{
    public class TasksController : Controller
    {
        private const int PageSize = 10;
        private readonly ApplicationDbContext _context;
        private List<TaskItem> fakeTasks = GetFakeTasks();
        private readonly ITaskItemRepository _taskItemRepository;
        public TasksController(ApplicationDbContext context, ITaskItemRepository taskItemRepository)
        {
            _context = context;
            _taskItemRepository = taskItemRepository;
        }

        // New Task List

        public  ActionResult TasksNewList()
        {
            var tasks =  _context.Tasks.ToList();

            return View(tasks);
        }



        // GET: Task
        public IActionResult Index(int page = 1)
        {
            var totalTasks = fakeTasks.Count;
            var totalPages = (int)Math.Ceiling((double)totalTasks / PageSize);

            // Validate page number
            page = Math.Max(1, Math.Min(page, totalPages));

            var tasksToDisplay = fakeTasks
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            var viewModel = new TaskListViewModel
            {
                Tasks = tasksToDisplay,
                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = page,
                    TotalItems = totalTasks,
                    ItemsPerPage = PageSize,
                    TotalPages = totalPages
                }
            };

            return View(viewModel);
        }

        public async Task<ViewResult> Tasks(string role, string id, string userName)
        {
            var roleTaskDto = await _taskItemRepository.RetrieveDto(id, userName, role);

            return View("Index", roleTaskDto);
        }



        public async Task<IActionResult> Create(string userId, string userName, string role, TaskItemDto taskDto)
        {
            if (!ModelState.IsValid) return View("Index");
            await _taskItemRepository.Add(userId, userName, taskDto);
            var roleTaskDto = await _taskItemRepository.RetrieveDto(userId, userName, role);
            return View("Index", roleTaskDto);
        }


        public async Task<IActionResult> UpdateAsync(string userId, string role, string userName, string id, TaskItemDto taskDto)
        {
            if (!ModelState.IsValid) return View("Index");
            await _taskItemRepository.Update(userId, userName, id, taskDto);
            var roleTaskDto = await _taskItemRepository.RetrieveDto(userId, userName, role);
            return View("Index", roleTaskDto);
        }



        public async Task<IActionResult> Delete(string userId, string userName, string role, string id)
        {
            if (!ModelState.IsValid) return View("Index");
            await _taskItemRepository.Delete(userId, userName, id);
            var roleTaskDto = await _taskItemRepository.RetrieveDto(userId, userName, role);
            return View("Index", roleTaskDto);
        }



        private static List<TaskItem> GetFakeTasks()
        {

            return (List<TaskItem>)Enumerable.Range(1, 50)
                .Select(i => new TaskItem
                {
                    Id = i.ToString(),
                    Title = $"Task {i}",
                    Description = $"Description for Task {i}",
                    DueDate = DateTimeOffset.UtcNow.AddDays(i),
                    Status = Models.TaskStatus.NotStarted
                })
                .ToList();
        }




    }
}
