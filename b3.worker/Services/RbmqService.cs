using b3_domain.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace b3.Worker.Services
{
    public class RbmqService
    {

        private const string rbmqQueue = "tarefaQueue";
        private const string stringConexao = "Data Source=DEVMENDONCA;Initial Catalog=TGG;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private Tarefa? tarefa;
        private DbService? dbService;

        public void ConsumerMessageRabbitMq()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: rbmqQueue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();

                    var message = Encoding.UTF8.GetString(body);

                    tarefa = JsonConvert.DeserializeObject<Tarefa>(message);

                    Console.WriteLine("Mensagem recebida");
                };

                channel.BasicConsume(queue: rbmqQueue,
                                     autoAck: true,
                consumer: consumer);

                if (tarefa != null)
                {
                    //inserir no banco SQL a mensagem
                    dbService = new DbService(stringConexao);
                    dbService.InsertTarefa(tarefa);
                }

            }
        }

    }
}

