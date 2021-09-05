using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySalaryFW.DAL
{
    public class Branches
    {
        private List<Branch> _branches = new List<Branch>();
        public Branches()
        {
        }
        public Branches(IEnumerable<Branch> branches)
        {
            _branches.AddRange(branches);
        }

        public void Add(Branch branch)
        {
            _branches.Add(branch);
        }

        public void Add(string branchName)
        {
            int newId = _branches.Max(x => x.Id) + 1;
            _branches.Add(new Branch { Id = newId, BranchName = branchName });
        }
        public void AddRange(IEnumerable<Branch> branches)
        {
            _branches.AddRange(branches);
        }
        public Branch Get(int idBranch)
        {
            var f = _branches.FirstOrDefault(x => x.Id == idBranch);
            return f;
        }
        public IEnumerable<Branch> GetAll()
        {
            return _branches;
        }
        public void Remove(int id)
        {
            var f = _branches.FirstOrDefault(x => x.Id == id);
            if (f != null) _branches.Remove(f);
        }
    }
}
