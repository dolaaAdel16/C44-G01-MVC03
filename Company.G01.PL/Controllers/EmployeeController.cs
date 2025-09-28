using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Models;
using Company.G01.PL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeerepository;
        private readonly IDepartmentRepository _departmentRepository;

        // ASK CLR Create object form DepartmentRepository
        public EmployeeController(IEmployeeRepository employeeRepository , IDepartmentRepository departmentRepository)
        {
            _employeerepository = employeeRepository;
           _departmentRepository = departmentRepository;
        }

        [HttpGet] //GET: /Employee/Index
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                 employees = _employeerepository.GetAll();
            }
            else
            {
                 employees = _employeerepository.GetByName(SearchInput);
            }
            
            // Dictionary : Key , Value
            // 1.ViewData : Transfer Extra info from Controller (Action) to view
            //ViewData["Message"] = "Hello from ViewData";

            // 2.ViewBag  : Transfer Extra info from Controller (Action) to view
            //ViewBag.Message = "Hello from ViewBag";

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments =  _departmentRepository.GetAll();
            ViewData["Departments"] = departments;

            return View(departments);
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
                    TempData["Message"] = "Employee Created Successfully";
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
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Employee with id :{id}  is not found" });

            return View(viewName, department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var departments = _departmentRepository.GetAll();
            ViewData["Departments"] = departments;
            if (id is null) return BadRequest("Invalid Id "); // 400

            var employee = _employeerepository.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Department with id :{id}  is not found" });
         
        
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
                //if(id != model.Id) return BadRequest();
                var employee = new Employee()
                {
                    Id=id,
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
