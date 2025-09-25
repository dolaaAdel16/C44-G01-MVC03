using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Models;
using Company.G01.PL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeerepository;

        // ASK CLR Create object form DepartmentRepository
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeerepository = employeeRepository;
        }

        [HttpGet] //GET: /Employee/Index
        public IActionResult Index()
        {
            var employees = _employeerepository.GetAll();

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(CreateEmployeeDTO model)
        {
            if (ModelState.IsValid) // Server Side Validation 
            {
               var employee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Address = model.Address,
                    Phone = model.Phone,
                    Salary = model.Salary,
                    HiringDate = model.HiringDate,
                    CreateAt = model.CreateAt,
                    Age = model.Age,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted
                };
                var count = _employeerepository.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [HttpGet]

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id "); // 400

            var department = _employeerepository.Get(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with id :{id}  is not found" });

            return View(viewName, department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id "); // 400

            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with id :{id}  is not found" });

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, UpdateEmployeeDTO model)
        {
            if (ModelState.IsValid)
            {

                {
                   var employee = new Employee()
                    {
                        Id = id,
                        Name = model.Name,
                        Email = model.Email,
                        Address = model.Address,
                        Phone = model.Phone,
                        Salary = model.Salary,
                        HiringDate = model.HiringDate,
                        CreateAt = model.CreateAt,
                        Age = model.Age,
                        IsActive = model.IsActive,
                        IsDeleted = model.IsDeleted
                    };
                    var count = _employeerepository.Update(employee);
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

            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.Id) return BadRequest();



                var count = _employeerepository.Delete(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(employee);

        }
    }
}
