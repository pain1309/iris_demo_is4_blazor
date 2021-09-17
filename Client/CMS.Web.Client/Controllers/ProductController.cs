using AutoMapper;
using CMS.Web.Client.Models;
using CMS.Web.Client.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CMS.Web.Client.Controllers
{
    public class ProductController : Controller
    {
        private HubConnection _hubConnection;

        public List<Product> Data = new List<Product>();
        public List<Product> ExchangedData = new List<Product>();

        private readonly IMapper _mapper;

        public ProductController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            await StartHubConnection();
            await _hubConnection.InvokeAsync("GetProducts");

            var x =_hubConnection.On<List<Product>>("GetProducts", (data) =>
            {
                Data.AddRange(data);
            });

            return View(Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Create(ProductCreateViewModel model)
        //{
        //    //var productCreateMolde = _productGrpcService.CreateProduct(model);
        //    //return RedirectToAction("Index");
        //}

        //[HttpGet]
        //public async Task<IActionResult> Update(int id)
        //{
        //    //var product = await _productGrpcService.GetProductById(id);
        //    //return View(product);
        //}

        //[HttpPost]
        //public IActionResult Update(ProductViewModel model)
        //{
        //    //var product = _productGrpcService.UpdateProduct(model);
        //    //return RedirectToAction("Index");
        //}

        //[HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    //var product = _productGrpcService.DeleteProduct(id);
        //    //return RedirectToAction("Index");
        //}

        private async Task StartHubConnection()
        {
            _hubConnection = new HubConnectionBuilder()
                            .WithUrl("http://localhost:4300/TransferHub")
                            .Build();

            await _hubConnection.StartAsync();
            if (_hubConnection.State == HubConnectionState.Connected)
                Console.WriteLine("connection started");
        }
        //private void AddTransferChartDataListener()
        //{
        //    _hubConnection.On<List<Product>>("ExchangeDataProduct", (data) =>
        //    {
        //        foreach (var item in data)
        //        {
        //            Console.WriteLine($"item: {item}");
        //        }

        //        Data = data;
        //    });
        //}

        private void AddExchangeDataListener()
        {
            _hubConnection.On<List<Product>>("ExchangeChartData", (data) =>
            {
                ExchangedData = data;
            });
        }
    }
}
