using b3_data;
using b3_data.Repositorio;
using b3_domain.Model;
using b3_Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace b3_Service.Services
{
    public class TarefasRepository : Repository<Tarefa>, ITarefasRepository
    {
        public TarefasRepository(Contexto contexto) : base(contexto)
        {


        }

        public async Task<IEnumerable<Tarefa>> GetTarefasDescricao(string value)
        {
            var prod = await Get()
                .Where(x => x.descricao == value
                || x.data.ToString() == value
                || x.status.ToString() == value)
                .ToListAsync();

            return prod;

        }
    }
}
