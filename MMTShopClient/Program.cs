using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MMTShopClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient() { BaseAddress= new Uri("https://localhost:44662/api/") };

        static async Task Main(string[] args)
        {
           await ShowPrompt();
           Console.WriteLine("Good bye ");
        }

        static async Task ShowPrompt()
        {
            var exit = false;
            do
            {
                Console.WriteLine("Please Select and option: ");
                Console.WriteLine("1 for Categories ");
                Console.WriteLine("2 for Featured Products ");
                Console.WriteLine("3 Exit ");
                var Key = Console.ReadLine();

                if (int.TryParse(Key, out int command))
                {
                    switch (command)
                    {
                        case 1:
                            await FetchCategories();
                            break;
                        case 2:
                            await FetchFeaturedProducts();
                            break;
                        case 3:
                            exit = true;
                            break;

                        default:
                            ShowInstruction();
                            await ShowPrompt();
                            break;
                    }
                }
                else
                {
                    ShowInstruction();
                }
            } while (!exit);
        }

        private async static Task FetchFeaturedProducts()
        {
            try
            {
                var response = await client.GetAsync("featured-products");
                var products = await ProcessResponse<List<Product>>(response);
                if (products != null && products.Count > 0)
                {
                    Console.Clear();
                    Console.WriteLine("Featured Products List");
                    Console.WriteLine($"");
                    Console.WriteLine($"");

                    foreach (var product in products)
                    {
                        Console.WriteLine($"Name: {product.Name}");
                        Console.WriteLine($"Price: {ShowPrice(product.Price)}");
                        Console.WriteLine($"Description: {product.Name}");
                        Console.WriteLine($"");
                        Console.WriteLine($"");
                    }

                    Console.WriteLine("Press any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("No products found. Press any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            catch (Exception exc)
            {

            }
        }

        static async Task FetchCategories()
        {
            try
            {
                var response = await client.GetAsync("categories");
                var categories = await ProcessResponse<List<Category>>(response);
                if (categories != null && categories.Count > 0)
                {
                    Console.Clear();
                    Console.WriteLine("Select a category number to view Products press 0 to exit");
                    foreach (var category in categories)
                    {
                        Console.WriteLine($"{category.Id} Name: {category.Name}");
                    }
                    await HandleCategorySelection(categories);
                }
                else
                {
                    Console.WriteLine("No categories found. Press any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            catch(Exception exc)
            {

            }
        }

        static async Task FetchCategoryProducts(int id)
        {
            try
            {
                var response = await client.GetAsync($"categories/{id}/products");
                var products = await ProcessResponse<List<Product>>(response);
                if (products != null && products.Count > 0)
                {
                    Console.Clear();

                    Console.WriteLine("Products List");
                    Console.WriteLine($"");
                    Console.WriteLine($"");

                    foreach (var product in products)
                    {
                        Console.WriteLine($"Name: {product.Name}");
                        Console.WriteLine($"Price: {ShowPrice(product.Price)}");
                        Console.WriteLine($"Description: {product.Name}");
                        Console.WriteLine($"");
                        Console.WriteLine($"");
                    }

                    
                }
                else
                {
                    Console.WriteLine("No products found. Press any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            catch (Exception exc)
            {

            }
        }

        static async Task HandleCategorySelection(List<Category> items)
        {
            var key = Console.ReadLine();
            if (int.TryParse(key, out int command))
            {
                switch (command)
                {
                    case 0:
                        ShowInstruction();
                        break;

                    default:

                        var item = items.FirstOrDefault(x => x.Id == command);
                        if (item == null)
                        {

                            Console.WriteLine("Select a category number to view Products press 0 to exit");
                            await HandleCategorySelection(items);
                        }
                        else
                        {
                            await FetchCategoryProducts(item.Id);
                        }
                        break;
                }
            }
            else
            {

                Console.WriteLine("Select a category number to view Products press 0 to exit");
                await HandleCategorySelection(items);
            }
        }

        static async Task<T> ProcessResponse<T>(HttpResponseMessage response)        {            string data = await response.Content.ReadAsStringAsync();            var items = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);            return items;        }

        static void ShowInstruction()
        {

            Console.Clear();

            Console.WriteLine("Please Select a number between 1 and 3 ");
        }

        static string ShowPrice(decimal price)
        {
            CultureInfo cultureInfo = new CultureInfo("en-GB");
            return string.Format(cultureInfo, "{0:C}", price);
        }
    }
}
