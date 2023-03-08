using Core.Interfaces.Service;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorFullStackCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _context;

        public OrderController(IOrderService context)
        {
            _context = context;
        }


        [HttpGet("orders")]
        public async Task<ActionResult<List<Comic>>> GetOrdersData()
        {
            var comics = await _context.GetOrders();
            return Ok(comics);
        }
    }
}
