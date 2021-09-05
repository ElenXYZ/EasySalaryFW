using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySalaryFW.DAL
{
    /// <summary>
    /// Табель учета рабочего времени
    /// </summary>
    public class Timesheets
    {
        private List<Timesheet> _timesheets = new List<Timesheet>();
        public Timesheets() { }
        public Timesheets(IEnumerable<Timesheet> timesheets)
        {
            _timesheets.AddRange(timesheets);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEmployee">Id cотрудника</param>
        /// <param name="month">Месяц учета, значения от 1-12</param>
        /// <param name="year">Год учета</param>
        /// <param name="countHour">Количество часов</param>
        public void Add(int idEmployee, int month, int year, decimal countHour = 150)
        {
            DateTime dayTimesheet = new DateTime();
            try
            {
                dayTimesheet = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            }
            catch(Exception ex)
            {
                throw new ArgumentException($"Некорректно указан месяц или год учета рабочего времени: {month}.{year}");
            }
            _timesheets.Add(new Timesheet() { IdEmployee = idEmployee, Day = dayTimesheet, CountHour = countHour });
        }

        public void AddRange(IEnumerable<Timesheet> timesheets)
        {
            _timesheets.AddRange(timesheets);
        }

    }
}
