using System;
using System.Collections.Generic;

class TextAnalyzer
{
    static void Main()
    {
        Console.WriteLine("=== АНАЛИЗАТОР ТЕКСТА ===\n");
        
        while (true)
        {
            Console.Write("Введите текст (минимум 100 символов): ");
            string text = Console.ReadLine();
            
            if (text.Length < 100)
            {
                Console.WriteLine("Слишком коротко! Нужно 100+ символов.\n");
                continue;
            }
            
            // Анализ
            int words = CountWords(text);
            string shortest = FindShortestWord(text);
            string longest = FindLongestWord(text);
            int sentences = CountSentences(text);
            
            // Вывод результатов
            Console.WriteLine($"\nСлов: {words}");
            Console.WriteLine($"Предложений: {sentences}");
            Console.WriteLine($"Самое короткое: '{shortest}'");
            Console.WriteLine($"Самое длинное: '{longest}'");
            
            // Продолжить?
            Console.Write("\nЕщё текст? (да/нет): ");
            if (Console.ReadLine().ToLower() != "да") break;
            Console.WriteLine();
        }
    }
    
    static int CountWords(string text)
    {
        int count = 0;
        bool inWord = false;
        
        foreach (char c in text)
        {
            if (char.IsLetter(c) && !inWord)
            {
                count++;
                inWord = true;
            }
            else if (!char.IsLetter(c)) inWord = false;
        }
        return count;
    }
    
    static string FindShortestWord(string text)
    {
        string[] words = text.Split(" .,!?;:".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        string shortest = words[0];
        foreach (string word in words)
            if (word.Length < shortest.Length) shortest = word;
        return shortest;
    }
    
    static string FindLongestWord(string text)
    {
        string[] words = text.Split(" .,!?;:".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        string longest = words[0];
        foreach (string word in words)
            if (word.Length > longest.Length) longest = word;
        return longest;
    }
    
    static int CountSentences(string text)
    {
        int count = 0;
        foreach (char c in text)
            if (c == '.' || c == '!' || c == '?') count++;
        return count;
    }
}
