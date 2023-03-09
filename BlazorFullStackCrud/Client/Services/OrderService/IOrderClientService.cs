

using BlazorFullStackCrud.Shared.Entities;

namespace BlazorFullStackCrud.Client.Services.SuperHeroService
{
    public interface IOrderClientService
    {
        List<Order> OrdersList { get; set; }
        List<Order> Orders { get; set; }
        Task GetOrders();
        Task CreateOrder(Order order);
        Task UpdateOrder(Order order);
    }
}
