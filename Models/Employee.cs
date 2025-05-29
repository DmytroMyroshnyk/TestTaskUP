using System.ComponentModel.DataAnnotations;
using TestTaskUP.Enums;

namespace TestTaskUP.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName => $"{LastName} {FirstName}" + (string.IsNullOrWhiteSpace(MiddleName) ? "" : $" {MiddleName}");
        [Required(ErrorMessage = "Ім'я обов’язкове")]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required(ErrorMessage = "Прізвище обов’язкове")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Адреса обов’язкова")]
        public string Address { get; set; }
        public string? Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public int PositionId { get; set; }
        public string PositionName { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;

    }
}
