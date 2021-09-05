using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySalaryFW.DAL
{
    public class FilialCountEmployeeReport
    {
        public int IdFilial { get; set; }
        public string FilialName { get; set; }
        public string City { get; set; }
        public int CountEmployee { get; set; }
    }
}
