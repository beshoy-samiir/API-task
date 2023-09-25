using System.ComponentModel.DataAnnotations.Schema;

namespace APITask.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        [ForeignKey("Category")]
        public int CatId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
