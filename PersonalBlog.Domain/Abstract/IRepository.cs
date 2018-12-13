using PersonalBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Abstract
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IEnumerable<T> Get { get; }
        void Save(T item);
        void Delete(int id);
    }
}
