using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTaskUP.DataAccess;
using TestTaskUP.EnumHelpers;
using TestTaskUP.Enums;
using TestTaskUP.Models;
using TestTaskUP.Repositories;

namespace TestTaskUP.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly EmployeeRepository _repository;

        public EditModel(IConfiguration config)
        {
            _repository = new EmployeeRepository(config.GetConnectionString("DefaultConnection"));
        }

        [BindProperty]
        public Employee Employee { get; set; }
        public List<SelectListItem> DepartmentOptions { get; set; }
        public List<SelectListItem> PositionOptions { get; set; }


        public IActionResult OnGet(int id)
        {
            var employee = _repository.GetEmployeeById(id);
            if (employee == null)
                return NotFound();
            Options(employee);
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Options(Employee);
                return Page();
            }

            _repository.UpdateEmployee(Employee);
            return RedirectToPage("/Employees/Employees");
        }
        private void Options(Employee employee)
        {
            Employee = employee;
            DepartmentOptions = Enum.GetValues(typeof(EDepartment))
            .Cast<EDepartment>()
            .Select(d => new SelectListItem
            {
                Value = ((int)d).ToString(),
                Text = d.GetDisplayName()
            })
            .ToList();

            PositionOptions = Enum.GetValues(typeof(EPosition))
            .Cast<EPosition>()
            .Select(d => new SelectListItem
            {
                Value = ((int)d).ToString(),
                Text = d.GetDisplayName()
            })
            .ToList();

        }
    }

}
