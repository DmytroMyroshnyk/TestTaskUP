using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using TestTaskUP.DataAccess;
using TestTaskUP.EnumHelpers;
using TestTaskUP.Enums;
using TestTaskUP.Models;
using TestTaskUP.Repositories;

namespace TestTaskUP.Pages.Employees
{
    public class EmployeesModel : PageModel
    {
        private readonly EmployeeRepository _repository;

        public EmployeesModel(IConfiguration config)
        {
            _repository = new EmployeeRepository(config.GetConnectionString("DefaultConnection"));
        }

        [BindProperty(SupportsGet = true)]
        public int? DepartmentId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }
        public bool IsSearchPerformed { get; set; }

        public List<Employee> Employees { get; set; }
        public List<SelectListItem> DepartmentOptions { get; set; }

        public void OnGet()
        {
            IsSearchPerformed = !string.IsNullOrWhiteSpace(Search) || DepartmentId.HasValue;
            Employees = _repository.GetFilteredEmployees(DepartmentId, Search);

            DepartmentOptions = Enum.GetValues(typeof(EDepartment))
                        .Cast<EDepartment>()
                        .Select(d => new SelectListItem
                        {
                            Value = ((int)d).ToString(),
                            Text = d.GetDisplayName(),
                            Selected = DepartmentId.HasValue && DepartmentId.Value == (int)d
                        })
                        .ToList();
        }
        public async Task<IActionResult> OnPostExportAsync()
        {
            var employees = _repository.GetFilteredEmployees(DepartmentId, Search);

            var sb = new StringBuilder();
            foreach (var emp in employees)
            {
                sb.AppendLine($"Full Name: {emp.FullName}");
                sb.AppendLine($"Position: {emp.PositionName}");
                sb.AppendLine($"Department: {emp.DepartmentName}");
                sb.AppendLine($"Phone: {emp.Phone}");
                sb.AppendLine($"Address: {emp.Address}");
                sb.AppendLine($"Salary: {emp.Salary:C}");
                sb.AppendLine($"Birth Date: {emp.BirthDate:dd MMMM yyyy}");
                sb.AppendLine($"Hire Date: {emp.HireDate:dd MMMM yyyy}");
                sb.AppendLine($"Company Name: {emp.CompanyName}");
                sb.AppendLine(new string('-', 40));
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/plain", "Export.txt");
        }
    }
}
