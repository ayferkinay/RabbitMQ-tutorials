using RabbitMQ.Client;
using System.Text;



//bağlantı oluştruma 
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqp://guest:guest@localhost:5672/");


//bağlantı aktifleştirme ve kanal açma 
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();


//Queue Oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false);

//exclusive neydi?
//autodelete neydi?

//queue'ya mesaj gönderme
//rabbitmq kuyruğa atacağı mesajları byte türünde kabul etmektedir. haliyle biz mesajları byte'a dönüştürmeliyiz
//exchange belirtmediğimiz için direct exchange devreye girdi bunun için de routing key olarak kuyruğun ismini verdik 
byte[] message = Encoding.UTF8.GetBytes("Merhaba");
channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);

Console.Read();
