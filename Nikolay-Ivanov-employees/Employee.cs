
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Nikolay_Ivanov_employees
{
    public  class Employee
    {
        public string EmpID { get; set; }
        public string ProjectID { get; set; }  
        public string DateFrom { get; set; }
        public string? DateTo { get; set; }
    }
}
