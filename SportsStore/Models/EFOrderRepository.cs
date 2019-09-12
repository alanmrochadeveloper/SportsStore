using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;
        public EFOrderRepository( ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Order> Orders => context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            if(order.OrderID == 0)
            {
                context.AttachRange(order.Lines.Select(l => l.Product));    /*an additional step is required when i store an Order object in the database. When the user’s cart data is deserialized from the session store, the JSon package creates new objects that are not known to entity Framework Core, which then tries to write all the objects into the database. For the Product objects, this means that entity Framework Core tries to write objects that have already been stored, which causes an error.to avoid this problem, i notify entity Framework Core that the objects exist and shouldn’t be stored in the database unless they are modified, as follows: */
                if (order.OrderID == 0)
                {
                    context.Orders.Add(order);
                }
                context.SaveChanges();
            }
        }
    }
}
