using Ciber.EntityFramework.EntityFramework;
using Ciber.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
namespace Ciber.Manager
{
    public class OrderManager: IOrderManager
    {
        private readonly CiberDbContext _ciberDbContext;
        public OrderManager(CiberDbContext ciberDbContext)
        {
            _ciberDbContext = ciberDbContext;
        }
        public List<OrderListDto> GetListOrder(int skip, int pageSize, string sort, string searchValue, out int totalRow)
        {
            var data = _ciberDbContext.Orders.Include(s => s.Product).ThenInclude(s => s.Category).Include(s => s.Customer).ToList();
            var result = new List<OrderListDto>();
            foreach (var item in data)
            {
                result.Add(new OrderListDto
                {
                    Amount = item.Amount,
                    CategoryName = item.Product.Category.Name,
                    CustomerName = item.Customer.Name,
                    OrderDate = item.OrderDate,
                    ProductName = item.Product.Name
                });
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                result = result.Where(x => x.CategoryName.ToLower().Contains(searchValue.ToLower())
                         || x.CustomerName.ToLower().Contains(searchValue.ToLower())
                         || x.ProductName.ToLower().Contains(searchValue.ToLower())).ToList();
            }
            totalRow = result.Count;
            result = result.Skip(skip)
                    .Take(pageSize).AsQueryable().OrderBy(sort).ToList();
            return result;
        }
    }
}
