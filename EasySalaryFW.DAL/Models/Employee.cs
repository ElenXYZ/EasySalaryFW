using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySalaryFW.DAL
{
    public class Employee
    {
        public int Id { get; set; }
        /// <summary>
        /// ID филиала
        /// </summary>
        public int IdFilial { get; set; }
        /// <summary>
        /// Id Подразделения
        /// </summary>
        public int IdBranch { get; set; }
        /// <summary>
        /// Табельный номер сотрудника
        /// </summary>
        public string TabelNumber { get; set; }
        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        public string Fio { get; set; }
        /// <summary>
        /// Тип оплаты
        /// </summary>
        public SalaryTypeEnum SalaryType { get; set; }
        /// <summary>
        /// Оклад сотрудника или сумма почасовой оплаты
        /// </summary>
        public decimal Salary { get; set; }
    }
}
