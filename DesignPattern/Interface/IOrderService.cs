
namespace DesignPattern.Interface
{
    public interface IOrderService : ITransientService
    {
        string Create(int id);
        string OrderHandler();
    }
}
