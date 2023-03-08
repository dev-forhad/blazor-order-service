using Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorFullStackCrud.Client.Services.SuperHeroService
{
    public class OrderClientService : IOrderClientService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public OrderClientService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<Order> Orders { get; set; } = new List<Order>();
        
        public async Task GetOrders()
        {
            var result = await _http.GetFromJsonAsync<List<Order>>("api/order/orders");
            if (result != null)
                Orders = result;
        }
    }
}
