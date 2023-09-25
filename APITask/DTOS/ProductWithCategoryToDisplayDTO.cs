using APITask.Models;

namespace APITask.DTOS
{
    public class ProductWithCategoryToDisplayDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public CategoryDisplayDTO CategoryName { get; set; }

    }
}

