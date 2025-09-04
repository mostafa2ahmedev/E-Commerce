using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Persistence.Data
{
    public class StoreDbContext : DbContext
    {


        public DbSet<Product> Products { get; set; }    
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly, type =>
            type.Namespace!.Contains("E_Commerce.Persistence.Data.Config")
            );
        }
    }
}
