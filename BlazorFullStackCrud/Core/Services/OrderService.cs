
using BlazorFullStackCrud.Shared.Entities;
using Core.Interfaces;
using Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository  orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IEnumerable<Order>> GetOrders()
        {
            try
            {
                return await _orderRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving orders from database.", ex);
            }
        }

        public async Task<Order> GetOrderById(int id)
        {
            try
            {
                return await _orderRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving order with ID {id} from database.", ex);
            }
        }

        public async Task AddOrder(Order order)
        {
            try
            {
               await _orderRepository.CreateAsync(order);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding order to database.", ex);
            }
        }

        public async Task UpdateOrder(Order order)
        {
            try
            {
                await _orderRepository.UpdateAsync(order);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating order with ID {order.Id} in database.", ex);
            }
        }

        public async Task DeleteOrder(int id)
        {
            try
            {
                var orderToDelete = await _orderRepository.GetByIdAsync(id);
                if (orderToDelete != null)
                {
                    await _orderRepository.DeleteAsync(orderToDelete.Id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting order with ID {id} from database.", ex);
            }
        }
    }
}



