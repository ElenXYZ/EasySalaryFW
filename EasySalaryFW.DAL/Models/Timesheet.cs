using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySalaryFW.DAL
{
    public class Timesheet
    {
        /// <summary>
        /// Дата выхода на работу
        /// </summary>
        public DateTime Day { get; set; }
        /// <summary>
        /// Количество отработанных часов
        /// </summary>
        public decimal CountHour { get; set; }
        /// <summary>
        /// Id сотрудника
        /// </summary>
        public int IdEmployee { get; set; }
    }
}
