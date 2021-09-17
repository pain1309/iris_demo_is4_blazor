using CMS.WebApplication.GrpcServices;
using CMS.WebApplication.Models;
using CMS.WebApplication.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMS.WebApplication.Controllers
{
    public class ProductManagementController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly ProductGrpcService _productGrpcService;

        public ProductManagementController(IProductRepository repository, ProductGrpcService productGrpcService)
        {
            _repository = repository;
            _productGrpcService = productGrpcService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productGrpcService.GetProducts();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCreateViewModel model)
        {
            var productCreateMolde = _productGrpcService.CreateProduct(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productGrpcService.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(ProductViewModel model)
        {
            var product = _productGrpcService.UpdateProduct(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete (int id)
        {
            var product = _productGrpcService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
