using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MB.School.Application.ViewModels;
using MB.School.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using MB.School.ViewModels;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;

namespace MB.School.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolDbContext _context;

        public HomeController(SchoolDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            ViewData["Message"] = "学生信息统计:";

            var entities = from student in _context.Students
                group student by student.EnrollmentDate
                into dataGroup
                select new EnrollmentDateGroup()
                {
                    EnrollmentDate = dataGroup.Key,
                    StudentCount =dataGroup.Count()
                };
            var dtos =await entities.ToListAsync();

            return View(dtos);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
