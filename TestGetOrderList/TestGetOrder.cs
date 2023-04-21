using Ciber.EntityFramework.EntityFramework;
using Ciber.Manager;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;

namespace TestGetOrderList
{
    public class DependencySetupFixture
    {
        public DependencySetupFixture()
        {
            var con = @"Server=.;Database=CiberDBTest12;MultipleActiveResultSets=True;Trusted_Connection=True;";

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<CiberDbContext>(x => x.UseSqlServer(con));
            serviceCollection.AddTransient<IOrderManager, OrderManager>();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
    public class TestGetOrder:IClassFixture<DependencySetupFixture>
    {
        private readonly ServiceProvider _orderManager;
        public TestGetOrder(DependencySetupFixture orderManager)
        {
            _orderManager = orderManager.ServiceProvider;
        }
        [Fact]
        public  void TestGetData()
        {
            using (var scope = _orderManager.CreateScope())
            {
                var departmentAppService =  scope.ServiceProvider.GetService<IOrderManager>();
                int x;
                var data = departmentAppService.GetListOrder(0, 10, "ProductName asc", "", out x);
                Assert.True(x > 0);
            }
        }
    }
}