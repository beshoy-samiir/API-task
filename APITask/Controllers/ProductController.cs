using APITask.DTOS;
using APITask.Models;
using APITask.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APITask.DTOS;

namespace APITask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepo;

        public ProductController(IProductRepository ProductRepo)
        {
            productRepo = ProductRepo;
        }
        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            return Ok(productRepo.GetAll());
        }
        [HttpGet("Dto")]
        public ActionResult<List<ProductWithCategoryToDisplayDTO>> GetCategoriesDto()
        {
            List<Product> prodList = productRepo.GetAll("Category");
            List<ProductWithCategoryToDisplayDTO> prodDto = new List<ProductWithCategoryToDisplayDTO>();
            foreach (Product product in prodList)
            {
                ProductWithCategoryToDisplayDTO obj = new ProductWithCategoryToDisplayDTO();
                obj.Name = product.Name;
                CategoryDisplayDTO categoryDisplayDTO = new CategoryDisplayDTO();
                categoryDisplayDTO.Name = product.Category.Name;
                obj.CategoryName = categoryDisplayDTO;
                prodDto.Add(obj);
            }
            return Ok(prodDto);
        }
        [HttpGet("{id:int}")]
        public ActionResult<Product> GetByID(int id)
        {
            return Ok(productRepo.Get(id));
        }
        [HttpPost]
        public IActionResult Add(Product product)
        {
            productRepo.Insert(product);
            productRepo.Save();
            return CreatedAtAction(
                "GetByID",
                new { id = product.Id }
                , product);
        }
        [HttpPost("Dto")]
        public IActionResult AddProductDto(ProductWithCategoryToDisplayDTO prodDto)
        {
            CategoryDisplayDTO categoryDisplayDTO = new CategoryDisplayDTO();
            Product product = new Product();
            product.Name = prodDto.Name;
            product.Price = prodDto.Price;
            product.Category.Name = categoryDisplayDTO.Name;
            productRepo.Insert(product);
            productRepo.Save();
            return CreatedAtAction(
                "GetByID", new { id = product.Id }, prodDto);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product NewProd)
        {

            if (id == NewProd.Id)
            {
                productRepo.Update(NewProd);
                productRepo.Save();
                return NoContent();
            }
            return BadRequest("Invalid Id");
        }
        [HttpDelete("{prodID}")]
        public IActionResult Remove(int ProductID)
        {
            productRepo.Delete(ProductID);
            productRepo.Save();
            return NoContent();
        }
    }
}
