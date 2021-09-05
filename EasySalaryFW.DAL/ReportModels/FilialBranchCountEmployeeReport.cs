using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySalaryFW.DAL.ReportModels
{
    public class FilialBranchCountEmployeeReport
    {
        public string FilialName { get; set; }
        public string BranchName { get; set; }
        public int CountEmployee { get; set; }
    }
}
