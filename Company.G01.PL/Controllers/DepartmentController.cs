using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G01.DAL.Models;
using Company.G01.PL.DTOs;
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

        [HttpGet]
        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]

        public IActionResult Create(CreateDepartmentDTO model )
        {
           if(ModelState.IsValid) // Server Side Validation 
           {
                var department = new Department()
                {
                    Code = model.Code ,
                    Name = model.Name ,
                    CreateAt = model.CreateAt
                };
                var count = _departmentRepository.Add(department);
                if(count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
           }

            return View(model);
        }

        [HttpGet]

        public IActionResult Details(int? id, string viewName = "Details"  )
        {
            if (id is null) return BadRequest("Invalid Id "); // 400

            var department = _departmentRepository.Get(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with id :{id}  is not found" });

            return View(viewName, department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id "); // 400

            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with id :{id}  is not found" });

            return Details(id , "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id ,UpdateDepartmentDTO model)
        {
            if (ModelState.IsValid)
            {
               
                {
                    var department = new Department()
                    {
                        Id = id,
                        Code = model.Code,
                        Name = model.Name,
                        CreateAt = model.CreateAt
                    };
                    var count = _departmentRepository.Update(department);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
               
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
        //    if (id is null) return BadRequest("Invalid Id "); // 400

        //    var department = _departmentRepository.Get(id.Value);
        //    if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with id :{id}  is not found" });

            return Details(id , "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest();

                
                    
                    var count = _departmentRepository.Delete(department);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(department);

        }
        }
}
