using Ciber.EntityFramework.EntityFramework;
using Ciber.Manager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System;
using System.Configuration;

namespace TestGetOrderList
{
    public class DependencySetupFixture
    {
        public DependencySetupFixture()
        {
            var con = @"Server=.;Database=CiberDBTest12;MultipleActiveResultSets=True;Trusted_Connection=True;"; 
            var serviceCollection = new ServiceCollection();
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();
            serviceCollection.AddDbContext<CiberDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

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
                var orderManagerService =  scope.ServiceProvider.GetService<IOrderManager>();
                int x;
                var data = orderManagerService.GetListOrder(0, 10, "ProductName asc", "", out x);
                Assert.True(x > 0);
            }
        }
    }
}