﻿
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context) { }

        public async Task AddOrder(Order order)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Save the order
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Handle any exceptions
                Console.WriteLine(ex.Message);

            }
        }

        public async Task UpdateOrder(Order order)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Save the order
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Handle any exceptions
                Console.WriteLine(ex.Message);

            }
        }


        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _context.Orders.Include(o => o.Windows)
                .ThenInclude(w => w.SubElements).ToListAsync(); 
        }

        public async Task<Order> GetSingleOrders(int id)
        {
            return await _context.Orders
                .Include(h => h.Windows).ThenInclude(w => w.SubElements)
                .FirstOrDefaultAsync(h => h.Id == id);
        }
    }
}
