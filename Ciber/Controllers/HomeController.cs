using Ciber.EntityFramework.EntityFramework;
using Ciber.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Dynamic.Core;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CiberCommon.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectList = Ciber.Models.SelectList;
using Ciber.Manager;

namespace Ciber.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CiberDbContext _ciberDbContext;
        private readonly IOrderManager _orderManager;

        public HomeController(ILogger<HomeController> logger, CiberDbContext ciberDbContext, IOrderManager orderManager)
        {
            _logger = logger;
            _ciberDbContext = ciberDbContext;
            _orderManager = orderManager;
        }
        [Authorize(Roles ="AdminRole")]
        public IActionResult Index()
        {
            ViewBag.CustomerList = _ciberDbContext.Customers.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
            ViewBag.ProductList = _ciberDbContext.Products.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
            return View();
        }
        public IActionResult ListProduct()
        {
            return View();
        }
        public IActionResult ListViewProduct()
        {
            var products = _ciberDbContext.Products.ToList();
            return View(products);
        }
        [Authorize(Roles = "AdminRole")]
        public IActionResult DeleteProduct(int productId)
        {
            var findProduct = _ciberDbContext.Products.FirstOrDefault(s => s.Id == productId);
            if (findProduct != null)
            {
                _ciberDbContext.Remove(findProduct);
                _ciberDbContext.SaveChanges();
            }              
            return RedirectToAction("ListViewProduct","Home");
        }
        [HttpPost]
        public JsonResult GetEmployeeList()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
            int filterRecord;
            var data = _orderManager.GetListOrder(skip, pageSize, sortColumn + " " + sortColumnDirection, searchValue,out filterRecord);
            var returnObj = new
            {
                draw = draw,
                recordsTotal = filterRecord,
                recordsFiltered = filterRecord,
                data = data
            };
            return Json(returnObj);
        }
        [HttpPost]
        public JsonResult GetCustomerList()
        {
            var data = _ciberDbContext.Customers.Select(s => new SelectList
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
            return Json(new {item = data});
        }
        [HttpPost]
        public JsonResult GetProductList()
        {
            var data = _ciberDbContext.Products.Select(s => new SelectList
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
            return Json(new { item = data });
        }
        [HttpPost]
        public JsonResult InsertData(OrderInsertInput input)
        { 
            var findProduct = _ciberDbContext.Products.FirstOrDefault(s => s.Id == input.ProductId);
            var checkAmount = input.Amount <= findProduct.Quantity;
            if (!checkAmount)
                return Json(new { message = "Amount larger than Quantity",success = false });
            var order = new Order
            {
                Amount = input.Amount,
                CustomerId = input.CustomerId,
                ProductId = input.ProductId,
                OrderDate = input.OrderDate,
                OrderName = input.OrderName,
            };
            findProduct.Quantity -= input.Amount;
            _ciberDbContext.Orders.Add(order);
            _ciberDbContext.SaveChanges();
            return Json(new { message = "Save successfully", success = true }); 
        }     
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}