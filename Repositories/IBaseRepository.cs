using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace desafio_catalogo_jogos.Repositories
{
    public interface IBaseRepository<T, ID> : IDisposable
    {
        Task<List<T>> Get(int page, int qnt);
        Task<T> Get(ID id);
        Task<List<T>> Find(string fragment, int page, int qnt);
        Task Insert(T item);
        Task Update(T item);
        Task Delete(T item);
    }
}