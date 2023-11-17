using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasksControllerApp.DataContext;
using TasksControllerApp.Models;
using TasksControllerApp.Models.ViewModel;

namespace TasksControllerApp.Controllers
{
    public class TasksController : Controller
    {
        private const int PageSize = 10;
        private readonly ApplicationDbContext _context;
        private List<TaskItem> fakeTasks = GetFakeTasks();
        public TasksController(ApplicationDbContext context)
        {
            _context = context;
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



        // GET: Task/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        [HttpPost]

        public IActionResult Create(TaskItem task)
        {
            task.DueDate = task.DueDate.ToUniversalTime();

            if (ModelState.IsValid)
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(task);
        }



       // POST: Task/Edit/id
       [HttpPost]

        public IActionResult Edit(int id, TaskItem task)
        {
            var taskFound =  _context.Tasks.FirstOrDefault(u => u.Id == id);

            if (task != null)
            {
                taskFound.Title = task.Title;
                taskFound.Description = task.Description;
                taskFound.DueDate = task.DueDate;
                taskFound.Status = task.Status;
                _context.Entry(taskFound).State = EntityState.Modified;
                 _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }


        // POST: Task/Delete/id
        [HttpPost, ActionName("Delete")]

        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }




        private static List<TaskItem> GetFakeTasks()
        {
        
            return Enumerable.Range(1, 50)
                .Select(i => new TaskItem
                {
                    Id = i,
                    Title = $"Task {i}",
                    Description = $"Description for Task {i}",
                    DueDate = DateTimeOffset.UtcNow.AddDays(i),
                    Status = Models.TaskStatus.NotStarted
                })
                .ToList();
        }




    }
}
