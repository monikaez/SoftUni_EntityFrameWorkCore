using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext context = new ProductShopContext();
            //1.Json user
            //string userJson = File.ReadAllText("../../../Datasets/users.json");
            //Console.WriteLine(ImportUsers( context, userJson));

            // 2. Import products
            //string productsJson = File.ReadAllText("../../../Datasets/products.json");
            // Console.WriteLine(ImportProducts(context, productsJson));

            //3.import Categories
            //string caterogiesJson = File.ReadAllText("../../../Datasets/categories.json");
            //Console.WriteLine(ImportCategories(context, caterogiesJson));

            //Query 4.Import Categories and Products
            //string caterogyProductsJson = File.ReadAllText("../../../Datasets/categories-products.json");
            //Console.WriteLine(ImportCategoryProducts(context, caterogyProductsJson));

            //Query 5. Export Products in Range
            //Console.WriteLine(GetProductsInRange(context));
            //Query 6. Export Sold Products
            //Console.WriteLine(GetSoldProducts(context));
            //Query 7. Export Categories by Products Count
            Console.WriteLine(GetCategoriesByProductsCount(context));
        }


        //Query 1. Import Users
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";

        }

        //Query 2. Import Products
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            if (products is not null)
            {
                context.Products.AddRange(products);
                context.SaveChanges();
            }

            return $"Successfully imported {products?.Length}";
        }

        //Query 3. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var allCategories = JsonConvert.DeserializeObject<Category[]>(inputJson);
            //ako e 0
            var validCategories = allCategories
                ?.Where(c => c.Name is not null)
                .ToArray();
            if (validCategories is not null)
            {
                context.Categories.AddRange(validCategories);
                context.SaveChanges();
                return $"Successfully imported {validCategories.Length}";
            }
            return $"Successfully imported 0";
        }

        //Query 4. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoriesProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Length}";

        }

        //Query 5. Export Products in Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            var productsInRange = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                })
                .OrderBy(p => p.price)
                .ToArray();
            var json = JsonConvert.SerializeObject(productsInRange, Formatting.Indented);

            return json;

        }

        //Query 6. Export Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            var userWithSoldProducts = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                        .Where(p => p.BuyerId != null)
                        .Select(p => new
                        {
                            name = p.Name,
                            price = p.Price,
                            buyerFirstName = p.Buyer!.FirstName,
                            buyerLastName = p.Buyer.LastName
                        }).ToArray()
                })
                .ToArray();

            string jsonOutput = JsonConvert.SerializeObject(userWithSoldProducts, Formatting.Indented);
            return jsonOutput;

        }

        //Query 7. Export Categories by Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categoriesByProductCount = context.Categories
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoriesProducts.Count,
                    averagePrice = c.CategoriesProducts
                        .Average(cp => cp.Product.Price).ToString("f2"),
                    totalRevenue = c.CategoriesProducts
                        .Sum(cp => cp.Product.Price).ToString("f2")
                })
                .OrderByDescending(x => x.productsCount)
                .ToArray();

            string jsonOutput = JsonConvert.SerializeObject(categoriesByProductCount, Formatting.Indented);
            return jsonOutput;
        }
    }
}