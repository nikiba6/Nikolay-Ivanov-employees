
using System.ComponentModel.DataAnnotations;


namespace Nikolay_Ivanov_employees
{
    public class Project
    {
        [Key]
        [StringLength(100)]
        public string ProjectID { get; set; }
        //public ICollection<EmplProject> EmplProjects { get; set; }
    }
}
