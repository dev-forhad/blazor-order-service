

using BlazorFullStackCrud.Shared.DTO;
using BlazorFullStackCrud.Shared.Entities;

namespace BlazorFullStackCrud.Client.Services.SuperHeroService
{
    public interface IOrderClientService
    {
        List<Order> OrdersList { get; set; }
        List<OrderDTO> Orders { get; set; }
        Task GetOrders();
        Task<Order> GetSingleOrders(int id);
        Task CreateOrder(Order order);
        Task UpdateOrder(Order order);
    }
}
