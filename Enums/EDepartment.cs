using System.ComponentModel.DataAnnotations;

namespace TestTaskUP.Enums
{
    public enum EDepartment
    {
        [Display(Name = "IT")]
        IT = 10,

        [Display(Name = "Human Resources")]
        HumanResources = 20,

        [Display(Name = "Education")]
        Education = 30,

        [Display(Name = "Admin")]
        Admin = 40
    }
}
