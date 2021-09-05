using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasySalaryFW.DAL;
using EasySalaryFW.SL.ReportService;

namespace EasySalaryFW
{
    class Program
    {
        static ApplicationContext context = new ApplicationContext();
        static void Main(string[] args)
        {
            
            Initializer initContext = new Initializer(context);
            Console.WriteLine("Выберите отчет: ");
            Console.WriteLine("1 - Список филиалов");
            Console.WriteLine("2 - Список подразделений");
            Console.WriteLine("3 - Количество сотрудников в филиалах");
            Console.WriteLine("4 - Расчетный лист за указанный месяц для филиала F и подразделения S");
            var input = Console.ReadKey();

            GetReport(input.Key);
            
            

            //var report = reportService.GetMonthSalaryByFilialBranch(context.Timesheets, context.Employees, 1, 2, 8, 2021);
            //foreach (var group in report)
            //    Console.WriteLine($"{group.Fio} : {String.Format("{0:0.00}", group.MonthSalary)}");

            //var report1 = reportService.GetMonthSalaryMore(context.Timesheets, context.Employees, 8, 2021, 100000);
            
            //foreach (var group in report1)
            //    Console.WriteLine($"{group.Fio} : {String.Format("{0:0.00}", group.MonthSalary)}");

            Console.Read();
        }
        static void GetReport(ConsoleKey key)
        {
            ReportService reportService = new ReportService();
            string strMonth, strIdFilial, strIdBranch = string.Empty;
            int intMonth, idFilial, idBranch;
            switch (key)
            {
                case ConsoleKey.D1:
                    var report1 = reportService.GetFilials(context.Filials);
                    Console.WriteLine("");
                    Console.WriteLine("Список филиалов");
                    foreach (var r in report1)
                        Console.WriteLine($"{r.FilialName} {r.City}");
                    break;
                case ConsoleKey.D2:
                    var report2 = reportService.GetBranches(context.Branches);
                    Console.WriteLine("");
                    Console.WriteLine("Список подразделений");
                    foreach (var r in report2)
                        Console.WriteLine($"{r.BranchName}");
                    break;
                case ConsoleKey.D3:
                    var report3 = reportService.GetCountEmployeeInFilials(context.Filials, context.Employees);
                    Console.WriteLine("");
                    Console.WriteLine("Количество сотрудников в филиалах");
                    foreach (var r in report3)
                        Console.WriteLine($"{r.FilialName} {r.CountEmployee}");
                    break;
                case ConsoleKey.D4:
                    Console.WriteLine();                    
                    do
                    {
                        Console.WriteLine("Введите требуемый месяц отчета(1-12)");

                        strMonth = Console.ReadLine();
                        if (!Int32.TryParse(strMonth, out intMonth))
                        {
                            Console.WriteLine("Введите число от 1 до 12");
                            continue; 
                        }

                    } while (intMonth < 1 || intMonth > 12);
                    
                    Console.WriteLine("Выберите филиал:");
                    foreach(var f in context.Filials)
                    {
                        Console.WriteLine($"{f.Id} - {f.FilialName}");
                    }
                    do
                    {
                        Console.WriteLine("Введите номер филиала");

                        strIdFilial = Console.ReadLine();
                        if (!Int32.TryParse(strIdFilial, out idFilial))
                        {
                            Console.WriteLine("Введите число");
                            continue;
                        }

                    } while (!context.Filials.Any(x=>x.Id == idFilial));
                    Console.WriteLine("Выберите подразделение:");
                    foreach (var f in context.Branches)
                    {
                        Console.WriteLine($"{f.Id} - {f.BranchName}");
                    }
                    do
                    {
                        Console.WriteLine("Введите номер подразделения");

                        strIdBranch = Console.ReadLine();
                        if (!Int32.TryParse(strIdBranch, out idBranch))
                        {
                            Console.WriteLine("Введите число");
                            continue;
                        }

                    } while (!context.Branches.Any(x => x.Id == idBranch));

                    Console.WriteLine();
                    var report4 = reportService.GetMonthSalaryByFilialBranch(context.Timesheets, context.Employees, idFilial, idBranch, intMonth, 2021);
                    Console.WriteLine("");
                    string monthName = new DateTime(2021, intMonth, 1).ToString("MMMМ", CultureInfo.CurrentCulture);
                    Console.WriteLine($"Расчетный лист за {monthName} 2021 года");
                    Console.WriteLine($"Филиал Подразделение");
                    foreach (var r in report4)
                        Console.WriteLine($"{r.Fio} {String.Format("{0:0.00}", r.MonthSalary)} rub");
                    break;
                default:
                    Console.WriteLine("Некорректный номер отчета");
                    break;
            }

        }
    }
}
