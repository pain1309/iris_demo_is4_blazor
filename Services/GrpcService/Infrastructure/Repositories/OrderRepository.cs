using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async Task<Order> CreateOrder(Order order)
        {
            var orderCreateModel = await _dbContext.Orders.AddAsync(order);
            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return order;
            }
            return null;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            var orderList = new List<Order>();
            if (userName == "initital_search")
            {
                orderList = await _dbContext.Orders.ToListAsync();
            } else
            {
                orderList = await _dbContext.Orders
                                    .Where(o => o.UserName == userName)
                                    .ToListAsync();
            }
            return orderList;
        }
    }
}
