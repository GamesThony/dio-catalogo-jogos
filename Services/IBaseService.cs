using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace desafio_catalogo_jogos.Services
{
    public interface IBaseService<ViewModel, InputModel, ID> : IDisposable
    {
        Task<List<ViewModel>> Get(int pag, int qnt);
        Task<ViewModel> Get(ID id);
        Task<List<ViewModel>> Find(string fragment, int page, int qnt);
        Task<ViewModel> Insert(InputModel input);
        Task Update(ID id, InputModel input);
        Task Delete(ID id);
    }
}