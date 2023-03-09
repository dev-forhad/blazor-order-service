using BlazorFullStackCrud.Shared.Entities;
using Core.Interfaces.Service;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace BlazorFullStackCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _context;
        private readonly ApplicationDbContext _dbContext;

        public OrderController(IOrderService context, ApplicationDbContext dbContext)
        {
            _context = context;
            _dbContext = dbContext;
        }


        [HttpGet("orders")]
        public async Task<ActionResult<List<Order>>> GetOrdersData()
        {
            var orders = await _context.GetOrders();
            return Ok(orders);
        }



        [HttpPost]
        public async Task<ActionResult<List<Order>>> PostOrder(Order order)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            

            try
            {

                // Save the order
                await _dbContext.Orders.AddAsync(order);
                await _dbContext.SaveChangesAsync();

                // Save the windows for the order
                //foreach (var window in order.Windows)
                //{
                //    window.OrderId = order.Id;
                //    await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Windows ON");
                //    await _dbContext.Windows.AddAsync(window);
                //    await _dbContext.SaveChangesAsync();
                //    await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Windows OFF");
                //    // Save the sub elements for the window
                //    foreach (var subElement in window.SubElements)
                //    {
                //        subElement.WindowId = window.Id;
                //        await _dbContext.SubElements.AddAsync(subElement);
                //        await _dbContext.SaveChangesAsync();
                //    }
                //}

                await transaction.CommitAsync();

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Handle any exceptions
                Console.WriteLine(ex.Message);

            }
            

            return Ok(await GetOrdersData());
        }

    }
}
