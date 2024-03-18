using b3_data.Repositorio.Interfaces;
using b3_domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace b3_Service.Services.Interfaces
{
    public interface ITarefasRepository : IRepository<Tarefa>
    {
        Task<IEnumerable<Tarefa>> GetTarefasDescricao(string value);
    }
}
