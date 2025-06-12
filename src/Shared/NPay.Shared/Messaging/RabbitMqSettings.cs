namespace NPay.Shared.Messaging;

public class RabbitMqSettings
{
    public const string RabbitMq = "RabbitMq";
    public string ConnectionName { get; set; } 
    public string HostAddress { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}