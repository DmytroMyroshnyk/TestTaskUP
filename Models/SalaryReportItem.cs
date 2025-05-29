namespace TestTaskUP.Models
{
    public class SalaryReportItem
    {
        public string DepartmentName { get; set; }
        public decimal Salary { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public decimal AvgSalary { get; set; }
        public int EmployeeCount { get; set; }
        
    }
}
