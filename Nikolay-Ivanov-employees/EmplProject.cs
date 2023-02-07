
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nikolay_Ivanov_employees
{
    public class EmplProject
    {

        [Key]
        public string EmpId { get; set; }
        [ForeignKey(nameof(EmpId))]
        public Employee Employee { get; set; }


        [Key]
        [StringLength(100)]
        public string ProjectID { get; set; }

        [ForeignKey(nameof(ProjectID))]
        public Project Project { get; set; }
    }
}
