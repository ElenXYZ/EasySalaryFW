using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasySalaryFW.DAL;
using EasySalaryFW.DAL.ReportModels;
using SS = EasySalaryFW.SL.SalaryService;

namespace EasySalaryFW.SL.ReportService
{
    public class ReportService
    {
        const int defaultYear = 2021;
        public ReportService() { }
        /// <summary>
        /// Список филиалов в алфавитном порядке
        /// </summary>
        /// <param name="filials"></param>
        /// <returns></returns>
        public IEnumerable<Filial> GetFilials(IEnumerable<Filial> filials)
        {
            var result = filials.OrderBy(x => x.FilialName);
            return result;
        }
        /// <summary>
        /// Список подразделений
        /// </summary>
        /// <param name="branches"></param>
        /// <returns></returns>
        public IEnumerable<BranchReport> GetBranches(IEnumerable<Branch> branches)
        {
            var result = branches.OrderBy(x => x.BranchName).Select(x => new BranchReport { Id = x.Id, BranchName = x.BranchName }).ToList();
            return result;
        }
        /// <summary>
        /// Список филиалов и количество сотрудников
        /// </summary>
        /// <param name="filials"></param>
        /// <param name="employees"></param>
        /// <returns></returns>
        public IEnumerable<FilialCountEmployeeReport> GetCountEmployeeInFilials(IEnumerable<Filial> filials, IEnumerable<Employee> employees)
        {
            var result = employees.GroupBy(x => x.IdFilial).Select(g => new { Key = g.Key, Count = g.Count() })
                .Join(filials,
                e => e.Key,
                f => f.Id,
                (e, f) => new FilialCountEmployeeReport { IdFilial = f.Id, FilialName = f.FilialName, City = f.City, CountEmployee = e.Count })
                .OrderBy(x => x.FilialName);
            return result;
        }
        /// <summary>
        /// Список филиалов и количество сотрудников в подразделениях
        /// </summary>
        /// <param name="filials"></param>
        /// <param name="branches"></param>
        /// <param name="employees"></param>
        /// <returns></returns>
        public IEnumerable<FilialBranchCountEmployeeReport> GetCountEmployeeInFilialAndBranches(IEnumerable<Filial> filials, IEnumerable<Branch> branches, IEnumerable<Employee> employees)
        {
            var result = employees.OrderBy(x => x.IdFilial).ThenBy(x => x.IdBranch)
               .Join(filials,
               e => e.IdFilial,
               f => f.Id,
               (e, f) => new { IdEmployee = e.Id, FilialName = f.FilialName, IdBranch = e.IdBranch })
               .Join(branches,
               e => e.IdBranch,
               b => b.Id,
               (e, b) => new { IdEmployee = e.IdEmployee, FilialName = e.FilialName, BranchName = b.BranchName })
               .GroupBy(x => new { x.FilialName, x.BranchName }).
               Select(g => new FilialBranchCountEmployeeReport { FilialName = g.Key.FilialName, BranchName = g.Key.BranchName, CountEmployee = g.Count() });
            return result;
        }
        /// <summary>
        /// Расчетный лист
        /// </summary>
        /// <param name="timesheets"></param>
        /// <param name="employees"></param>
        /// <param name="IdFilial">филиал</param>
        /// <param name="IdBranch">подразделение</param>
        /// <param name="month">месяц отчета</param>
        /// <param name="year">год отчета</param>
        /// <returns></returns>
        public IEnumerable<EmployeeMonthSalaryReport> GetMonthSalaryByFilialBranch(IEnumerable<Timesheet> timesheets, IEnumerable<Employee> employees, int IdFilial, int IdBranch, int month, int year = defaultYear)
        {
            SS.SalaryService salaryService = new SS.SalaryService();
            DateTime currentDay = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            var result = timesheets.Where(x => x.Day == currentDay)
              .Join(employees.Where(x => x.IdFilial == IdFilial && x.IdBranch == IdBranch),
              t => t.IdEmployee,
              e => e.Id,
              (t, e) => new EmployeeMonthSalaryReport
              { //IdEmployee = e.Id, 
                  Fio = e.Fio,
                  //TableNumber = e.TabelNumber, 
                  //Salary = e.Salary, 
                  //SalaryType = e.SalaryType, 
                  //CountHour = t.CountHour, 
                  MonthSalary = salaryService.Calc(e.SalaryType, e.Salary, t.CountHour)
              })
              .OrderBy(x => x.Fio);
            return result;
        }
        /// <summary>
        /// Список сотрудников с заработной платой в текущем месяце > minSalary
        /// </summary>
        /// <param name="timesheets"></param>
        /// <param name="employees"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="minSalary"></param>
        /// <returns></returns>
        public IEnumerable<EmployeeMonthSalaryReport> GetMonthSalaryMore(IEnumerable<Timesheet> timesheets, IEnumerable<Employee> employees,  decimal minSalary, int month, int year = defaultYear)
        {
            SS.SalaryService salaryService = new SS.SalaryService();
            DateTime currentDay = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            var result = timesheets.Where(x => x.Day == currentDay)
              .Join(employees,
              t => t.IdEmployee,
              e => e.Id,
              (t, e) => new EmployeeMonthSalaryReport
              {
                  Fio = e.Fio,
                  MonthSalary = salaryService.Calc(e.SalaryType, e.Salary, t.CountHour)
              })
              .Where(x => x.MonthSalary > minSalary)
              .OrderBy(x => x.Fio);
            return result;
        }
        /// <summary>
        /// Список N-сотрудников с максимальными зп
        /// </summary>
        /// <param name="timesheets"></param>
        /// <param name="employees"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployeeWithMaxSalary(IEnumerable<Timesheet> timesheets, IEnumerable<Employee> employees, int count)
        {
            SS.SalaryService salaryService = new SS.SalaryService();
            DateTime currentDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            var result = timesheets.Where(x => x.Day == currentDay)
              .Join(employees,
              t => t.IdEmployee,
              e => e.Id,
              (t, e) => new
              {
                  Employee = e,
                  MonthSalary = salaryService.Calc(e.SalaryType, e.Salary, t.CountHour)
              })
              .OrderByDescending(x => x.MonthSalary)
              .Take(count);

            return result.Select(x => x.Employee);
        }
        /// <summary>
        /// Список сотрудников на окладе, которые отработали  все требуемые часы
        /// </summary>
        /// <param name="timesheets"></param>
        /// <param name="employees"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployeeWhoWorkAllHours(IEnumerable<Timesheet> timesheets, IEnumerable<Employee> employees, int month, int year = defaultYear)
        {
            DateTime currentDay = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            var result = timesheets.Where(x => x.Day == currentDay)
              .Join(employees.Where(x => x.SalaryType == SalaryTypeEnum.Fixed),
              t => t.IdEmployee,
              e => e.Id,
              (t, e) => new
              {
                  Employee = e,
                  CountHours = t.CountHour
              })
              .Where(x => x.CountHours >= 150)
              .OrderBy(x => x.Employee.Fio);
            return result.Select(x => x.Employee);
        }
        /// <summary>
        /// Список филиалов с указанием средней зарплаты в филиале
        /// </summary>
        /// <param name="filials"></param>
        /// <param name="employees"></param>
        /// <returns></returns>
        public IEnumerable<FilialAvgSalaryReport> GetAvgSalary(IEnumerable<Filial> filials, IEnumerable<Employee> employees)
        {
            var result = employees.GroupBy(x => x.IdFilial).Select(g => new { Key = g.Key, AvgSalary = g.Average(x => x.Salary) })
              .Join(filials,
              e => e.Key,
              f => f.Id,
              (e, f) => new FilialAvgSalaryReport { IdFilial = f.Id, FilialName = f.FilialName, AvgSalary = e.AvgSalary })
              .OrderBy(x => x.FilialName);
            return result;
        }
    
    }
}
