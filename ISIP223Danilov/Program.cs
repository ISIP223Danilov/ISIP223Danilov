using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreInventory
{
    public enum ProductCategory
    {
        Electronics = 1,
        Clothing = 2,
        Food = 3,
        Books = 4,
        Sports = 5
    }

    public class Product
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool InStock => Quantity > 0;
        public ProductCategory Category { get; set; }

        public Product(string code, string name, decimal price, int quantity, ProductCategory category)
        {
            Code = code;
            Name = name;
            Price = price;
            Quantity = quantity;
            Category = category;
        }

        public override string ToString()
        {
            return $"Code: {Code}, Name: {Name}, Price: {Price:C}, Quantity: {Quantity}, " +
                   $"In stock: {(InStock ? "Yes" : "No")}, Category: {Category}";
        }
    }

    public class StoreInventoryApp
    {
        private List<Product> products;
        private int nextProductId;

        public StoreInventoryApp()
        {
            products = new List<Product>();
            nextProductId = 1;
            InitializeTestData();
        }

        private void InitializeTestData()
        {
            AddProduct("Samsung Smartphone", 29999.99m, 10, ProductCategory.Electronics);
            AddProduct("Cotton T-shirt", 1499.50m, 25, ProductCategory.Clothing);
            AddProduct("Milk Chocolate", 89.90m, 100, ProductCategory.Food);
            AddProduct("War and Peace", 599.00m, 15, ProductCategory.Books);
            AddProduct("Football", 2499.00m, 8, ProductCategory.Sports);
        }

        private void AddProduct(string name, decimal price, int quantity, ProductCategory category)
        {
            string code = GenerateProductCode();
            Product product = new Product(code, name, price, quantity, category);
            products.Add(product);
            nextProductId++;
        }

        public void Run()
        {
            Console.WriteLine("Welcome to the store inventory system!");

            while (true)
            {
                ShowMenu();
                var input = Console.ReadLine();

                if (input == "0")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }

                ProcessCommand(input);
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("\n=== MENU ===");
            Console.WriteLine("1. Add product");
            Console.WriteLine("2. Delete product");
            Console.WriteLine("3. Order supply");
            Console.WriteLine("4. Sell product");
            Console.WriteLine("5. Search products");
            Console.WriteLine("6. Show all products");
            Console.WriteLine("0. Exit");
            Console.Write("Select command: ");
        }

        private void ProcessCommand(string command)
        {
            try
            {
                switch (command)
                {
                    case "1":
                        AddNewProduct();
                        break;
                    case "2":
                        DeleteProduct();
                        break;
                    case "3":
                        OrderSupply();
                        break;
                    case "4":
                        SellProduct();
                        break;
                    case "5":
                        SearchProducts();
                        break;
                    case "6":
                        DisplayAllProducts();
                        break;
                    default:
                        Console.WriteLine("Invalid command. Try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void AddNewProduct()
        {
            try
            {
                Console.Write("Enter product name: ");
                string name = Console.ReadLine();

                Console.Write("Enter product price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    Console.WriteLine("Error: Invalid price format.");
                    return;
                }

                Console.Write("Enter product quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity))
                {
                    Console.WriteLine("Error: Invalid quantity format.");
                    return;
                }

                Console.WriteLine("Available categories:");
                foreach (ProductCategory cat in Enum.GetValues(typeof(ProductCategory)))
                {
                    Console.WriteLine($"{(int)cat}. {cat}");
                }

                Console.Write("Select category (enter number): ");
                string categoryInput = Console.ReadLine();

                // Fixed: Proper category input handling
                if (!int.TryParse(categoryInput, out int categoryNumber) ||
                    !Enum.IsDefined(typeof(ProductCategory), categoryNumber))
                {
                    Console.WriteLine("Error: Invalid category.");
                    return;
                }

                ProductCategory selectedCategory = (ProductCategory)categoryNumber;

                if (ValidateProductData(name, price, quantity))
                {
                    AddProduct(name, price, quantity, selectedCategory);
                    Console.WriteLine("Product added successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product: {ex.Message}");
            }
        }

        private void DeleteProduct()
        {
            Console.Write("Enter product code to delete: ");
            string code = Console.ReadLine();

            var product = products.FirstOrDefault(p => p.Code == code);
            if (product == null)
            {
                Console.WriteLine("Product with specified code not found.");
                return;
            }

            products.Remove(product);
            Console.WriteLine("Product deleted successfully.");
        }

        private void OrderSupply()
        {
            Console.Write("Enter product code: ");
            string code = Console.ReadLine();

            var product = products.FirstOrDefault(p => p.Code == code);
            if (product == null)
            {
                Console.WriteLine("Product with specified code not found.");
                return;
            }

            Console.Write("Enter quantity for supply: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Error: Invalid quantity.");
                return;
            }

            product.Quantity += quantity;
            Console.WriteLine($"Supply registered successfully. New quantity: {product.Quantity}");
        }

        private void SellProduct()
        {
            Console.Write("Enter product code: ");
            string code = Console.ReadLine();

            var product = products.FirstOrDefault(p => p.Code == code);
            if (product == null)
            {
                Console.WriteLine("Product with specified code not found.");
                return;
            }

            if (!product.InStock)
            {
                Console.WriteLine("Error: Product is out of stock.");
                return;
            }

            Console.Write("Enter quantity to sell: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Error: Invalid quantity.");
                return;
            }

            if (quantity > product.Quantity)
            {
                Console.WriteLine($"Error: Not enough products in stock. Available: {product.Quantity}");
                return;
            }

            product.Quantity -= quantity;
            decimal total = product.Price * quantity;
            Console.WriteLine($"Sale completed successfully. Total: {total:C}");
            Console.WriteLine($"Remaining quantity: {product.Quantity}");
        }

        private void SearchProducts()
        {
            Console.WriteLine("Search by:");
            Console.WriteLine("1. Product code");
            Console.WriteLine("2. Name");
            Console.WriteLine("3. Category");
            Console.Write("Select search type: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    SearchByCode();
                    break;
                case "2":
                    SearchByName();
                    break;
                case "3":
                    SearchByCategory();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private void SearchByCode()
        {
            Console.Write("Enter product code: ");
            string code = Console.ReadLine();

            var product = products.FirstOrDefault(p => p.Code == code);
            if (product != null)
            {
                Console.WriteLine("Found product:");
                Console.WriteLine(product);
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        private void SearchByName()
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: Enter name for search.");
                return;
            }

            var foundProducts = products.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
            DisplayProducts(foundProducts, $"Search results for '{name}':");
        }

        private void SearchByCategory()
        {
            Console.WriteLine("Available categories:");
            foreach (ProductCategory cat in Enum.GetValues(typeof(ProductCategory)))
            {
                Console.WriteLine($"{(int)cat}. {cat}");
            }

            Console.Write("Select category (enter number): ");
            string categoryInput = Console.ReadLine();

            if (!int.TryParse(categoryInput, out int categoryNumber) ||
                !Enum.IsDefined(typeof(ProductCategory), categoryNumber))
            {
                Console.WriteLine("Invalid category.");
                return;
            }

            ProductCategory selectedCategory = (ProductCategory)categoryNumber;
            var foundProducts = products.Where(p => p.Category == selectedCategory).ToList();
            DisplayProducts(foundProducts, $"Products in category '{selectedCategory}':");
        }

        private void DisplayAllProducts()
        {
            DisplayProducts(products, "All products in store:");
        }

        private void DisplayProducts(List<Product> productsToDisplay, string title)
        {
            Console.WriteLine($"\n=== {title} ===");
            if (productsToDisplay.Any())
            {
                foreach (var product in productsToDisplay)
                {
                    Console.WriteLine(product);
                }
                Console.WriteLine($"Total found: {productsToDisplay.Count} products");
            }
            else
            {
                Console.WriteLine("No products found.");
            }
        }

        private string GenerateProductCode()
        {
            return "1" + nextProductId.ToString("D5");
        }

        private bool ValidateProductData(string name, decimal price, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: Product name cannot be empty.");
                return false;
            }

            if (price <= 0)
            {
                Console.WriteLine("Error: Price must be positive.");
                return false;
            }

            if (quantity < 0)
            {
                Console.WriteLine("Error: Quantity cannot be negative.");
                return false;
            }

            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            StoreInventoryApp app = new StoreInventoryApp();
            app.Run();
        }
    }
}