using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySalaryFW.DAL
{
    public class Branch
    {
        public int Id { get; set; }
        /// <summary>
        /// Наименование Подразделения
        /// </summary>
        public string BranchName { get; set; }
    }
}
