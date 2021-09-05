using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySalaryFW.DAL
{
    public class Initializer
    {
        private ApplicationContext _context;
        public Initializer(ApplicationContext context)
        {
            _context = context;
            Initialize();
        }
        public void Initialize()
        {
            if (_context.Employees != null)
            {
                return;
            }
            var filials = new List<Filial>()
            {   new Filial{Id=1, FilialName="Центральный офис", City="Казань" },
                new Filial{Id=2, FilialName="Филиал Екатеринбург", City="Екатеринбург" },
                new Filial{Id=3, FilialName="Филиал Владивосток", City="Владивосток" }
                };
            var branches = new List<Branch>()
            { new Branch{Id=1, BranchName="Подразделение 1" },
            new Branch{Id=2, BranchName="Подразделение 2"},
            new Branch{Id=3, BranchName="Подразделение 3"}
            };
            var employees = new List<Employee>()
            {   new Employee{Id=1, TabelNumber="0001", Fio="Павел Буре", IdFilial=1, IdBranch=1, SalaryType=SalaryTypeEnum.Fixed, Salary = 100000 },
                new Employee{Id=2, TabelNumber="0020", Fio="Синичкина Валюша", IdFilial=2, IdBranch=1, SalaryType=SalaryTypeEnum.Fixed, Salary = 120000 },
                new Employee{Id=3, TabelNumber="0023", Fio="Зосим Петрович", IdFilial=2, IdBranch=2, SalaryType=SalaryTypeEnum.ByTime, Salary = 11000 },
                new Employee{Id=4, TabelNumber="0044", Fio="Сан Саныч", IdFilial=2, IdBranch=1, SalaryType=SalaryTypeEnum.Fixed, Salary = 120000  },
                new Employee{Id=5, TabelNumber="0050", Fio="Вемиамин", IdFilial=3, IdBranch=1, SalaryType=SalaryTypeEnum.ByTime, Salary = 10000 },
                new Employee{Id=6, TabelNumber="0062", Fio="Булгаков Михаил", IdFilial=1, IdBranch=2, SalaryType=SalaryTypeEnum.Fixed, Salary = 100000 },
                new Employee{Id=7, TabelNumber="0077", Fio="Станислав Лем", IdFilial=1, IdBranch=2, SalaryType=SalaryTypeEnum.ByTime, Salary = 10000 },
                new Employee{Id=8, TabelNumber="0088", Fio="Говард Лайкрафт", IdFilial=3, IdBranch=3, SalaryType=SalaryTypeEnum.Fixed , Salary = 60000 },
                new Employee{Id=9, TabelNumber="0091", Fio="Борис Стругацкий", IdFilial=3, IdBranch=3, SalaryType=SalaryTypeEnum.ByTime, Salary = 10000 },
                new Employee{Id=10, TabelNumber="0111", Fio="Жанна Дарк", IdFilial=1, IdBranch=3, SalaryType=SalaryTypeEnum.ByTime, Salary = 500 },
                new Employee{Id=11, TabelNumber="0121", Fio="Марина Абрамович", IdFilial=2, IdBranch=1, SalaryType=SalaryTypeEnum.ByTime , Salary = 8000},
                new Employee{Id=12, TabelNumber="0300", Fio="Тамара Эйдельман", IdFilial=1, IdBranch=2, SalaryType=SalaryTypeEnum.Fixed, Salary = 100000 }
            };

            var timesheets = new List<Timesheet>();
            DateTime dayTimesheet = new DateTime();
            for (int i = 1; i <= 8; i++)
            {
                foreach (var e in employees)
                {
                    dayTimesheet = new DateTime(2021, i, DateTime.DaysInMonth(2021, i));
                    timesheets.Add(new Timesheet() { Day = dayTimesheet, IdEmployee = e.Id, CountHour = 150 });
                }
            }
            _context.Filials = filials;
            _context.Branches = branches;
            _context.Employees = employees;
            _context.Timesheets = timesheets;
        }
    }
}
