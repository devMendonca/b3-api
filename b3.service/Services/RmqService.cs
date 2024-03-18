using b3_domain.Model;
using b3_Service.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace b3_Service.Services
{
    public class RmqService : IRmqService
    {
        private const string LOCALHOST = "localhost";
        public async Task SendMessageRb(Tarefa tarefa)
        {

            var factory = new ConnectionFactory()
            {
                HostName = LOCALHOST,
                UserName = "guest",
                Password = "guest"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "tarefaQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var mensagem = JsonConvert.SerializeObject(tarefa);
                var body = Encoding.UTF8.GetBytes(mensagem);

                channel.BasicPublish(exchange: "",
                                     routingKey: "tarefaQueue",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine("Mensagem enviada com sucesso");

            }


        }

    }
}
