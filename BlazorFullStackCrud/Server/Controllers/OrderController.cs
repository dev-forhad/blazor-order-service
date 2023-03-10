using AutoMapper;
using BlazorFullStackCrud.Server.Common;
using BlazorFullStackCrud.Shared.DTO;
using BlazorFullStackCrud.Shared.Entities;
using Core.Interfaces.Service;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace BlazorFullStackCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet("orders")]
        public async Task<ActionResult<List<Order>>> GetOrdersData()
        {

            var config = new MapperConfiguration(cfg => cfg.AddProfile<OrderMappingProfile>());
            var mapper = config.CreateMapper();
            var orders = await _orderService.GetOrders();
            List<OrderDTO> orderDTO = mapper.Map<List<OrderDTO>>(orders);

            //var options = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve,
            //    MaxDepth = 64 // increase the maximum depth if needed
            //};

            //var json = JsonSerializer.Serialize(orderDTO, options);


            return Ok(orderDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetSingleOrder(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound("Sorry, no order found. :/");
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<List<Order>>> PostOrder(Order order)
        {

            try
            {
                await _orderService.AddOrder(order);

            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine(ex.Message);

            }
            
            return Ok(await GetOrdersData());
        }

    }
}
