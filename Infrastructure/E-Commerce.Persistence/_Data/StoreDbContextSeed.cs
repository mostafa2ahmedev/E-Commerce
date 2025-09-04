using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace E_Commerce.Persistence.Data
{
    public static class StoreDbContextSeed
    {

        public static async Task SeedAsync(StoreDbContext storeDbContext) {

            if (!storeDbContext.ProductBrands.Any()) {
                var brandsData = await File.ReadAllTextAsync("../Infrastructure/E-Commerce.Persistence/Data/Seeds/brands.json");
            



                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count > 0) {

                    foreach (var brand in brands)
                    {
                     await   storeDbContext.ProductBrands.AddAsync(brand);
                  
                    }
                    await storeDbContext.SaveChangesAsync();
                }

       
            }
            if (!storeDbContext.ProductCategories.Any())
            {
                var categoriesData = await File.ReadAllTextAsync("../Infrastructure/E-Commerce.Persistence/Data/Seeds/types.json");




                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

                if (categories?.Count > 0)
                {

                    foreach (var category in categories)
                    {
                        await storeDbContext.ProductCategories.AddAsync(category);

                    }
                    await storeDbContext.SaveChangesAsync();
                }


            }
            if (!storeDbContext.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../Infrastructure/E-Commerce.Persistence/Data/Seeds/products.json");




                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count > 0)
                {

                    foreach (var product in products)
                    {
                        await storeDbContext.Products.AddAsync(product);

                    }
                    await storeDbContext.SaveChangesAsync();
                }


            }
        }
    }
}
