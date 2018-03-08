using System;
using System.Linq;
using System.Threading.Tasks;
using MB.School.Common;
using MB.School.Core.Models;
using MB.School.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MB.School.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolDbContext _context;

        public StudentsController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string sortOrder,string searchStudent,int? page,string currentStudent)
        {
            //姓名排序字段
            ViewData["Name_Sort_Param"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //日期排序字段
            ViewData["Date_Sort_Param"] = sortOrder=="Date"? "date_desc" : "Date";
            

            
            //当前排序的关键字
            //当前过滤页的参数
            //
            if (searchStudent != null)
            {
                page = 1;
            }
            else
            {
                searchStudent = currentStudent;
            }
            //搜索的关键字
            ViewData["SearchStudent"] = searchStudent;
            ViewData["CurrentSort"] = sortOrder;
            #region 搜索和排序          
            var students = from student in _context.Students select student;
            if (!string.IsNullOrEmpty(searchStudent))
            {
                students = students.Where(s => s.RealName.Contains(searchStudent));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.RealName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                    default:
                    students = students.OrderBy(s => s.RealName);
                    break;
            }
            

            #endregion

            int pageSize = 3;
            

//            var dtos = await _context.Students.AsNoTracking().ToListAsync();
//            return View(dtos);
            var entities = students.AsNoTracking();
            var dtos = await PaginationList<Student>.CreatePaginationAsync(entities, page ?? 1, pageSize);
            return View(dtos);;
        }
        
        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(a=>a.Enrollments).ThenInclude(e=>e.Course).AsNoTracking()
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RealName,EnrollmentDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException e)
            {
                ModelState.AddModelError("","请检查填写的数据是否正确。");
            }
            
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.SingleOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,RealName,EnrollmentDate")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .SingleOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(m => m.StudentId == id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
