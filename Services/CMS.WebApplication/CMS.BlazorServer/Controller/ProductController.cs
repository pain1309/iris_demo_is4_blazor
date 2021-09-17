using Domain.Entities;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.BlazorServer.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IHubContext hubContext;

        public ProductController(IHubContext hubContext)
        {
            this.hubContext = hubContext;
        }

        [HttpPost]
        public async Task SendMessage(Product product)
        {
            //additional business logic

            await this.hubContext.Clients.All.SendAsync("dataReceivedFromApi", product);

            //additional business logic
        }
    }
}
