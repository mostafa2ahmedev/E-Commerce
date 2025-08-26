using E_Commerce.Domain.Contracts.Persistence;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data
{
    internal class StoreContextInitializer(StoreDbContext _storeDbContext) : IStoreContextInitializer
    {

        public async Task InitializeAsync()
        {
            var pendingMigrations = await _storeDbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await _storeDbContext.Database.MigrateAsync();

       
        }

        public async Task SeedAsync()
        {
            if (!_storeDbContext.ProductBrands.Any())
            {
                var brandsData = await File.ReadAllTextAsync("../Infrastructure/E-Commerce.Persistence/Data/Seeds/brands.json");




                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count > 0)
                {

                    foreach (var brand in brands)
                    {
                        await _storeDbContext.ProductBrands.AddAsync(brand);

                    }
                    await _storeDbContext.SaveChangesAsync();
                }


            }
            if (!_storeDbContext.ProductCategories.Any())
            {
                var categoriesData = await File.ReadAllTextAsync("../Infrastructure/E-Commerce.Persistence/Data/Seeds/types.json");




                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

                if (categories?.Count > 0)
                {

                    foreach (var category in categories)
                    {
                        await _storeDbContext.ProductCategories.AddAsync(category);

                    }
                    await _storeDbContext.SaveChangesAsync();
                }


            }
            if (!_storeDbContext.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../Infrastructure/E-Commerce.Persistence/Data/Seeds/products.json");




                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count > 0)
                {

                    foreach (var product in products)
                    {
                        await _storeDbContext.Products.AddAsync(product);

                    }
                    await _storeDbContext.SaveChangesAsync();
                }


            }
        }
    }
}
