namespace E_Commerce.Domain.Entities.Products
{
    public class ProductCategory : BaseEntity<int>
    {
        public required string Name { get; set; }
    }
}
