using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TasksControllerApp.Services;

namespace TasksControllerApp.Controllers
{
    public class AuditsController : Controller
    {

        private readonly AuditLogService _auditLogService;

        public AuditsController(AuditLogService auditLogService) => _auditLogService = auditLogService;

        [HttpGet]
        public IActionResult Index()
        {
            return View("AuditType");
        }

        [HttpGet]
        public async Task<IActionResult> GetAuditTable()
        {
             var auditLogs = await _auditLogService.GetAuditLogs();
            return View("Index" , auditLogs);
        }

        [HttpPost]
        public async Task<ViewResult> FilterAuditLog(DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.MinValue;
            endDate ??= DateTime.MaxValue;

            var filteredAuditLogs = await _auditLogService.Filter(startDate, endDate);

            return View("Index", filteredAuditLogs);
        }
    }
}
