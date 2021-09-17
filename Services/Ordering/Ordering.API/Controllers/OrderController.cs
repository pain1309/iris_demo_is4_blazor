using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.Entities;
using Ordering.API.GrpcServices;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly IOrderRepository _repository;
        private readonly OrderingGrpcService _orderingGrpcService;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository repository, OrderingGrpcService orderingGrpcService, IMapper mapper)
        {
            _repository = repository;
            _orderingGrpcService = orderingGrpcService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("[action]/{userName}")]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderByUserName(string userName)
        {
            var orders = await _repository.GetOrdersByUserName(userName);
            return Ok(orders);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Ordering.API.Entities.Ordering), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Ordering.API.Entities.Ordering order)
        {
            var orderEntity = _mapper.Map<Order>(order);
            var orderCreateResult = await _repository.CreateOrder(orderEntity);
            return orderCreateResult;
        }


        [HttpGet]
        [Route("[action]/{productName}")]
        [ProducesResponseType(typeof(ProductViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<ProductViewModel>>> GetProduct(string productName)
        {
            var product = await _orderingGrpcService.GetProductsByName(productName);
            var productResult = _mapper.Map<List<ProductViewModel>>(product);
            return productResult;
        }
    }
}
