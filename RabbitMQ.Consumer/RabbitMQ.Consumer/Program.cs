using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//bağlantı oluştruma 
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqp://guest:guest@localhost:5672/");



//bağlantı aktifleştirme ve kanal açma 
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();


//Queue Oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false);

//Queue'dan mesaj okuma
EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queue: "example-queue", false, consumer);
consumer.Received += (sender, e) =>
{
    //kuyruktaki mesajın byte verisini getirip string halini gösterecek
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.ToArray()));
};


Console.Read();
