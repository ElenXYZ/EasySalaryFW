using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySalaryFW.DAL
{
    public class Employees
    {
        private List<Employee> _employees = new List<Employee>();
        public Employees()
        {
        }
        public Employees(IEnumerable<Employee> employees)
        {
            _employees.AddRange(employees);
        }

        public void Add(Employee employee)
        {
            _employees.Add(employee);
        }

        public void Add(string fio, SalaryTypeEnum salaryType, string tabelNumber, int idFilial, int idBranch  )
        {
            int newId = _employees.Max(x => x.Id) + 1;
            _employees.Add(new Employee { Id = newId, Fio = fio, SalaryType = salaryType, TabelNumber = tabelNumber, IdFilial = idFilial, IdBranch = idBranch});
        }
        public void AddRange(IEnumerable<Employee> employees)
        {
            _employees.AddRange(employees);
        }

        public void ChangeSalaryType(int idEmployee, SalaryTypeEnum salaryType)
        {
            var f = _employees.FirstOrDefault(x => x.Id == idEmployee);
            f.SalaryType = salaryType;
        }
        public Employee Get(int idEmployee)
        {
            var f = _employees.FirstOrDefault(x => x.Id == idEmployee);
            return f;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _employees;
        }

        public void Remove(int idEmployee)
        {
            var f = _employees.FirstOrDefault(x => x.Id == idEmployee);
            if (f != null) _employees.Remove(f);
        }
        public void RemoveByTabelNumber(string tabelnumber)
        {
            var f = _employees.FirstOrDefault(x => x.TabelNumber == tabelnumber);
            if (f != null) _employees.Remove(f);
        }
    }
}
