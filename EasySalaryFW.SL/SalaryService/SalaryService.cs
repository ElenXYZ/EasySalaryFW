using EasySalaryFW.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySalaryFW.SL.SalaryService
{
    public class SalaryService 
    {
        public decimal Calc(SalaryTypeEnum salaryType, decimal salary, decimal countHour)
        {
            switch (salaryType)
            {
                case SalaryTypeEnum.Fixed:
                    return salary / 150 * countHour;
                case SalaryTypeEnum.ByTime:
                    return salary * countHour;
                default:
                    return salary;
            }
        }
    }
}
