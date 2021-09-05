using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySalaryFW.DAL
{
    public class Filials
    {
        private List<Filial> _filials = new List<Filial>();
        public Filials()
        {
        }
        public Filials(IEnumerable<Filial> f)
        {
            _filials.AddRange(f);
        }
        
        public void Add(Filial filial)
        {
            _filials.Add(filial);
        }

        public void Add (string filialName, string city = "Казань")
        {
            int newId = _filials.Max(x => x.Id)+1;
            _filials.Add(new Filial { Id = newId, FilialName = filialName, City = city });
        }
        public Filial Get(int idFilial)
        {
            var f = _filials.FirstOrDefault(x => x.Id == idFilial);
            return f;
        }
        public IEnumerable<Filial> GetAll()
        {
            return _filials;
        }
        public void AddRange(IEnumerable<Filial> filials)
        {
            _filials.AddRange(filials);
        }

        public void Remove(int idFilial)
        {
            var f = _filials.FirstOrDefault(x => x.Id == idFilial);
            if (f!=null) _filials.Remove(f);
        }

    }
}
