namespace E_Commerce.Domain.Entities.Products
{
    public class ProductCategory : BaseAuditableEntity<int>
    {
        public required string Name { get; set; }
    }
}
