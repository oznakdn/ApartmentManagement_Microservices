namespace Shared.MessagePublising;

public interface IMessagePublisher
{
    void Publish<TBody>(string queue, TBody messageBody);
    Task PublishAsync<TBody>(string queue, TBody messageBody);
}
