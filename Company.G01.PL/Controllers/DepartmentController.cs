using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    //MVC Controller
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        // ASK CLR Create object form DepartmentRepository
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet] //GET: /Department/Index
        public IActionResult Index()
        {
             var departments = _departmentRepository.GetAll();    

            return View(departments);
        }
    }
}
