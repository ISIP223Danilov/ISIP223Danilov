using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreInventory
{
    // Перечисление категорий товаров
    public enum ProductCategory
    {
        Electronics,
        Clothing,
        Food,
        Books,
        Sports
    }

    // Класс товара
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
            return $"Код: {Code}, Название: {Name}, Цена: {Price:C}, Количество: {Quantity}, " +
                   $"В наличии: {(InStock ? "Да" : "Нет")}, Категория: {Category}";
        }
    }

    // Основной класс приложения
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
            // Заполняем тестовыми данными
        }

        public void Run()
        {
            Console.WriteLine("Добро пожаловать в систему учета товаров магазина!");

            while (true)
            {
                ShowMenu();
                var input = Console.ReadLine();

                if (input == "0") break;

                ProcessCommand(input);
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("\n=== МЕНЮ ===");
            Console.WriteLine("1. Добавить товар");
            Console.WriteLine("2. Удалить товар");
            Console.WriteLine("3. Заказать поставку товара");
            Console.WriteLine("4. Продать товар");
            Console.WriteLine("5. Поиск товаров");
            Console.WriteLine("6. Показать все товары");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите команду: ");
        }

        private void ProcessCommand(string command)
        {
            // Обработка команд будет реализована в следующих коммитах
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