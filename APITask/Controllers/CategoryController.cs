using APITask.DTOS;
using APITask.Models;
using APITask.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepo;
        
        public CategoryController(ICategoryRepository CategoryRepo)
        {
            categoryRepo = CategoryRepo;
        }
        [HttpGet]
        [Authorize]
        public ActionResult<List<Category>> GetCategories()
        {
            return Ok(categoryRepo.GetAll());
        }
        [HttpGet("Dto")]
        [Authorize]
        public ActionResult<List<CategoryWithProductToDisplayDTO>> GetCategoriesDto()
        {
            List<Category> catList = categoryRepo.GetAll("products");
            List<CategoryWithProductToDisplayDTO> catDto = new List<CategoryWithProductToDisplayDTO>();
            foreach (Category category in catList)
            {
                CategoryWithProductToDisplayDTO obj = new CategoryWithProductToDisplayDTO();
                obj.Name = category.Name;
                foreach (Product prodItem in category.products)
                {
                    obj.Prod.Add(new ProductDisplayDTO() 
                    { Id = prodItem.Id, Name = prodItem.Name });
                }
                catDto.Add(obj);
            }
            return Ok(catDto);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Category> GetByID(int id)
        {
            return Ok(categoryRepo.Get(id));
        }
        [HttpPost]
        public IActionResult Add(Category category)
        {
            categoryRepo.Insert(category);
            categoryRepo.Save();
            return CreatedAtAction(
                "GetByID",
                new { id = category.Id }
                , category);
        }
        [HttpPost("Dto")]
        public IActionResult AddCaregoryDto(CategoryDisplayDTO catDto)
        {
            Category category = new Category();
            category.Name = catDto.Name;
            categoryRepo.Insert(category); 
            categoryRepo.Save();
            return CreatedAtAction(
                "GetByID",new { id = category.Id }, catDto);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, Category NewCat)
        {

            if (id == NewCat.Id)
            {
                categoryRepo.Update(NewCat);
                categoryRepo.Save();
                return NoContent();
            }
            return BadRequest("Invalid Id");
        }
        [HttpDelete("{CatID}")]
        public IActionResult Remove(int CatID)
        {
            categoryRepo.Delete(CatID);
            categoryRepo.Save();
            return NoContent();
        }
    }
}
