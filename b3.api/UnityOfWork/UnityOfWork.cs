using b3.api.UnityOfWork.Interfaces;
using b3_data;
using b3_Service.Services;
using b3_Service.Services.Interfaces;

namespace b3.api.UnityOfWork
{
    public class UnityOfWork : IUnityOfWork
    {

        private TarefasRepository _tarefasRepository;
        public Contexto _db;

        public UnityOfWork(Contexto db)
        {
            _db = db;
        }


        public ITarefasRepository TarefasRepository
        {
            get
            {
                return _tarefasRepository = _tarefasRepository ?? new TarefasRepository(_db);
            }
        }


        public async Task Commit()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
