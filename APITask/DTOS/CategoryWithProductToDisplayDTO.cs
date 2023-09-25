namespace APITask.DTOS
{
    public class CategoryWithProductToDisplayDTO
    {
        public string Name { get; set; }
        public List<ProductDisplayDTO> Prod { get; set; }
                    = new List<ProductDisplayDTO>();
    }
}
