using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;


        int count;
        do
        {
            Console.Write("Введите количество операций (2-40): ");
        } while (!int.TryParse(Console.ReadLine(), out count) || count < 2 || count > 40);


        string[] names = new string[count];
        decimal[] amounts = new decimal[count];


        Console.WriteLine("\nФормат: Название; Сумма");
        for (int i = 0; i < count; i++)
        {
            while (true)
            {
                Console.Write($"Операция {i + 1}: ");
                string input = Console.ReadLine();
                string[] parts = input.Split(';');

                if (parts.Length == 2 && decimal.TryParse(parts[1].Trim(), out decimal amount))
                {
                    names[i] = parts[0].Trim();
                    amounts[i] = amount;
                    break;
                }
                Console.WriteLine("Ошибка! Используйте формат: Название; Сумма");
            }
        }


        while (true)
        {
            Console.WriteLine("\n1. Вывод данных\n2. Статистика\n3. Сортировка\n4. Конвертация\n5. Поиск\n0. Выход");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    decimal total = 0;
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {names[i]}; {amounts[i]} руб.");
                        total += amounts[i];
                    }
                    Console.WriteLine($"Общая сумма: {total} руб.");
                    break;

                case "2":
                    if (count == 0) break;
                    decimal sum = 0, max = amounts[0], min = amounts[0];
                    for (int i = 0; i < count; i++)
                    {
                        sum += amounts[i];
                        if (amounts[i] > max) max = amounts[i];
                        if (amounts[i] < min) min = amounts[i];
                    }
                    Console.WriteLine($"Сумма: {sum} руб.\nСреднее: {sum / count} руб.\nМакс: {max} руб.\nМин: {min} руб.");
                    break;

                case "3":
                    for (int i = 0; i < count - 1; i++)
                    {
                        for (int j = 0; j < count - i - 1; j++)
                        {
                            if (amounts[j] > amounts[j + 1])
                            {

                                (amounts[j], amounts[j + 1]) = (amounts[j + 1], amounts[j]);
                                (names[j], names[j + 1]) = (names[j + 1], names[j]);
                            }
                        }
                    }
                    Console.WriteLine("Отсортировано по возрастанию цены!");
                    break;

                case "4":
                    Console.Write("Введите курс (рубли к валюте): ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal rate) && rate > 0)
                    {
                        for (int i = 0; i < count; i++)
                            Console.WriteLine($"{names[i]}; {amounts[i] / rate:F2} у.е.");
                    }
                    else
                    {
                        Console.WriteLine("Неверный курс!");
                    }
                    break;

                case "5":
                    Console.Write("Введите название для поиска: ");
                    string search = Console.ReadLine().ToLower();
                    bool found = false;
                    for (int i = 0; i < count; i++)
                    {
                        if (names[i].ToLower().Contains(search))
                        {
                            Console.WriteLine($"{names[i]}; {amounts[i]} руб.");
                            found = true;
                        }
                    }
                    if (!found) Console.WriteLine("Ничего не найдено");
                    break;

                case "0": return;

                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }
        }
    }
}