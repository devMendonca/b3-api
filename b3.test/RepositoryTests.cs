using b3_data;
using b3_data.Repositorio;
using b3_domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace b3.test
{
    public class RepositoryTests
    {
        private readonly DbContextOptions<Contexto> _options;
        private readonly Contexto _context;

        public RepositoryTests()
        {
            _options = new DbContextOptionsBuilder<Contexto>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new Contexto(_options);


            using (var context = new Contexto(_options))
            {
                context.Set<Tarefa>().Add(new Tarefa
                {
                    descricao = "Pinbolin",
                    responsavel = "dmendonca",
                    celular = "(11) 94332-8716",
                    cpf = "403.780.248-17",
                    status = true,
                    data = DateTime.Now
                });
                context.Set<Tarefa>().Add(new Tarefa
                {
                    descricao = "Skate",
                    responsavel = "Dmendonca",
                    celular = "(11) 94332-8716",
                    cpf = "403.780.248-17",
                    status = true,
                    data = DateTime.Now
                });
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task BuscarTarefaPorIdRetornoSucesso()
        {
            var repository = new Repository<Tarefa>(_context);

            var entity = new Tarefa
            {
                descricao = "Surf",
                responsavel = "David Mendonça",
                celular = "(11) 94332-8716",
                cpf = "403.780.248-17",
                status = true,
                data = DateTime.Now
            };

            _context.Set<Tarefa>().Add(entity);
            _context.SaveChanges();

            var result = await repository.GetById(e => e.descricao == "Surf");

            Assert.NotNull(result);
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task BuscarTarefaPorIdRetornoNuloQuandoNaoExistir()
        {
            var repository = new Repository<Tarefa>(_context);

            var result = await repository.GetById(e => e.descricao == "TarefaQueNaoExiste");

            Assert.Null(result);
        }

        [Fact]
        public void Get_DeveRetornarTodosOsItens()
        {
            using (var context = new Contexto(_options))
            {
                var repository = new Repository<Tarefa>(context);

                var result = repository.Get().ToList();

                Assert.Equal(2, result.Count);
                Assert.Contains(result, r => r.descricao == "Pinbolin");
                Assert.Contains(result, r => r.descricao == "Skate");
            }
        }

    }
}
