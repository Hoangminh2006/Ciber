using Ciber.Models;

namespace Ciber.Manager
{
    public interface IOrderManager
    {
        List<OrderListDto> GetListOrder(int currentPage, int pageSize, string sort, string searchValue, out int totalRow);
    }
}
