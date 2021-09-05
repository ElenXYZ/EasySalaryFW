using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySalaryFW.DAL.ReportModels
{
    public class FilialAvgSalaryReport
    {
        public int IdFilial { get; set; }
        public string FilialName { get; set; }
        public decimal AvgSalary { get; set; }
    }
}
