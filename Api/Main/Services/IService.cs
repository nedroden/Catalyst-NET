using System.Collections.Generic;
using System.Threading.Tasks;
using Catalyst.Api.Main.Models;

namespace Catalyst.Api.Main.Services
{
    public interface IService<T> where T : IModel
    {
        public IEnumerable<T> FetchAll();
        public T FetchSingle(long id);
        public T Create(T record);
        public void Update(T record);
        public void Remove(long id);
        public void Remove(T record);
        public bool Exists(long id);
    }
}