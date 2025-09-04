namespace E_Commerce.Domain.Entities.Products
{
    public class ProductBrand : BaseAuditableEntity<int>
    {
        public required string Name { get; set; }

   
    }
}
