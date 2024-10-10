using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IEntityService<Dto>
    {
        public List<Dto> GetAll();
        public Dto GetById(int id);
        public void Add(Dto entity);
        public void Update(Dto entity);
        public void Delete(Dto entity);
    }
}
