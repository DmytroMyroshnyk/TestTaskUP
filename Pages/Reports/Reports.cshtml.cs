using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTaskUP.EnumHelpers;
using TestTaskUP.Enums;
using TestTaskUP.Models;
using TestTaskUP.Repositories;

namespace TestTaskUP.Pages.Reports
{
    public class SalaryReportModel : PageModel
    {
        private readonly EmployeeRepository _repository;

        public SalaryReportModel(IConfiguration config)
        {
            _repository = new EmployeeRepository(config.GetConnectionString("DefaultConnection"));
        }

        [BindProperty]
        public int? SelectedDepartmentId { get; set; }

        public List<SelectListItem> Departments { get; set; }

        public List<SalaryReportItem> ReportItems { get; set; }

        public void OnGet()
        {
            LoadDepartments();
        }

        public void OnPostGenerate()
        {
            LoadDepartments();
            ReportItems = _repository.GetSalaryReport(SelectedDepartmentId);
        }

        public IActionResult OnPostExport()
        {
            var report = _repository.GetSalaryReport(SelectedDepartmentId);

            var lines = report.Select(r =>
                $"Department: {r.DepartmentName}, Employee Count: {r.EmployeeCount}, Salary Summary: {r.Salary}, Minimal Salary: {r.MinSalary} ,Maximum Salary: {r.MaxSalary},Avarage Salary: {r.AvgSalary:F2}");

            var content = string.Join(Environment.NewLine, lines);
            var bytes = System.Text.Encoding.UTF8.GetBytes(content);

            return File(bytes, "text/plain", "salary_report.txt");
        }

        private void LoadDepartments()
        {
            Departments = Enum.GetValues(typeof(EDepartment))
                .Cast<EDepartment>()
                .Select(d => new SelectListItem
                {
                    Value = ((int)d).ToString(),
                    Text = d.GetDisplayName()
                }).ToList();

            Departments.Insert(0, new SelectListItem { Text = "All", Value = "" });
        }
    }


}
