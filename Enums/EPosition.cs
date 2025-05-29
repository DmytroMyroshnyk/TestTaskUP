using System.ComponentModel.DataAnnotations;

namespace TestTaskUP.Enums
{
    public enum EPosition
    {
        // Department 10 – IT
        [Display(Name = "Backend Developer")]
        BackendDeveloper = 1,

        [Display(Name = "FrontEnd Developer")]
        FrontEndDeveloper = 2,

        [Display(Name = "IOS Developer")]
        IOSDeveloper = 3,

        [Display(Name = "Android Developer")]
        AndroidDeveloper = 4,

        [Display(Name = "Flutter Developer")]
        FlutterDeveloper = 5,

        [Display(Name = "Python Developer")]
        PythonDeveloper = 6,

        [Display(Name = "JAVA Developer")]
        JAVADeveloper = 7,

        [Display(Name = "Net Developer")]
        NetDeveloper = 8,

        // Department 20 – Human Resources
        [Display(Name = "HR Coordinator")]
        HRCoordinator = 21,

        [Display(Name = "HR Manager")]
        HRManager = 22,

        [Display(Name = "Recruiter")]
        Recruiter = 23,

        // Department 30 – Education
        [Display(Name = "English Teacher")]
        EnglishTeacher = 31,

        // Department 40 – Admin
        [Display(Name = "CEO")]
        CEO = 41
    }
}
