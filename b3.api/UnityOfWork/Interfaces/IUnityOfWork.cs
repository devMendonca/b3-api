using b3_Service.Services.Interfaces;

namespace b3.api.UnityOfWork.Interfaces
{
    public interface IUnityOfWork
    {

        ITarefasRepository TarefasRepository { get; }

        Task Commit();

        void Dispose();
    }
}
