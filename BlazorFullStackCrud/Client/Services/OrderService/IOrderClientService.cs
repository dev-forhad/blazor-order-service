using Shared.Entities;

namespace BlazorFullStackCrud.Client.Services.SuperHeroService
{
    public interface IOrderClientService
    {
        List<Order> Orders { get; set; }
        Task GetOrders();
    }
}
