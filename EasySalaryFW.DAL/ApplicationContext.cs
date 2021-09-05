using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySalaryFW.DAL
{
    public class ApplicationContext
    {
        public List<Filial> Filials { get; set; }
        public List<Branch> Branches { get; set; }
        public SalaryTypeEnum SalaryType { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Timesheet> Timesheets { get; set; }
    }
}
