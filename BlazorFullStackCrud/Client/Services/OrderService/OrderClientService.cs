
using BlazorFullStackCrud.Client.Pages;
using BlazorFullStackCrud.Shared.DTO;
using BlazorFullStackCrud.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorFullStackCrud.Client.Services.SuperHeroService
{
    public class OrderClientService : IOrderClientService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public List<Order> OrdersList { get; set; } = new List<Order>();

        public OrderClientService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<OrderDTO> Orders { get; set; } = new List<OrderDTO>();

        public async Task GetOrders()
        {
            var result = await _http.GetFromJsonAsync<List<OrderDTO>>("api/order/orders");
            if (result != null)
                Orders = result;
        }

        public async Task<Order> GetSingleOrders(int id)
        {
            var result = await _http.GetFromJsonAsync<Order>($"api/order/{id}");
            if (result != null)
                return result;
            throw new Exception("Hero not found!");
        }


        // Create method for creating a new order in the database

        public async Task CreateOrder(Order order)
        {
            var result = await _http.PostAsJsonAsync("api/order", order);
            await SetOrders(result);
        }


        private async Task SetOrders(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Order>>();
            OrdersList = response;
            _navigationManager.NavigateTo("superheroes");
        }

        // Update method for updating an existing order in the database
        public async Task UpdateOrder(Order order)
        {
            await _http.PutAsJsonAsync($"api/orders/{order.Id}", order);
        }



    }
}
