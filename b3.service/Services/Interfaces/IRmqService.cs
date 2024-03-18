using b3_domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace b3_Service.Services.Interfaces
{
    public interface IRmqService
    {
        Task SendMessageRb(Tarefa tarefa);
    }
}
