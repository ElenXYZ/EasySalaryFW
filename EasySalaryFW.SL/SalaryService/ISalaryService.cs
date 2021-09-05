using EasySalaryFW.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySalaryFW.SL.SalaryService
{
    interface ISalaryService
    {
        decimal Calc(SalaryTypeEnum salaryType, decimal salary, decimal countHour);
    }
}
