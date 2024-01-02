using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Клас, що представляє іграшку
class Toy : IComparable<Toy>
{
    public string Name { get; set; }
    public int Price { get; set; }
    public string AgeRange { get; set; }
    public string AdditionalInfo { get; set; }

    // Конструктор класу
    public Toy(string name, int price, string ageRange, string additionalInfo)
    {
        Name = name;
        Price = price;
        AgeRange = ageRange;
        AdditionalInfo = additionalInfo;
    }

    // Перевизначення методу для порівняння іграшок за вартістю
    public int CompareTo(Toy other)
    {
        return other.Price.CompareTo(this.Price);
    }

    // Перевизначення методу для виводу інформації про іграшку у вигляді рядка
    public override string ToString()
    {
        return $"{Name} - {Price} коп.";
    }
}

// Головний клас програми
class ToysAnalyzer
{
    static void Main()
    {
        List<Toy> toys = new List<Toy>();

        // Зчитування даних з файлу
        try
        {
            string[] lines = File.ReadAllLines("toys.txt");
            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                string name = parts[0].Trim();
                int price = int.Parse(parts[1].Trim());
                string ageRange = parts[2].Trim();
                string additionalInfo = parts[3].Trim();

                // Додавання нової іграшки до списку
                toys.Add(new Toy(name, price, ageRange, additionalInfo));
            }
        }
        catch (IOException e)
        {
            // Обробка винятків, якщо файл не може бути прочитаний
            Console.WriteLine(e.Message);
            return;
        }

        // Сортування іграшок за вартістю у спадаючому порядку
        toys.Sort();

        // Визначення найбільш коштовних іграшок
        List<Toy> expensiveToys = new List<Toy>();
        int maxPrice = toys[0].Price;
        foreach (var toy in toys)
        {
            if (maxPrice - toy.Price <= 1000) // Перевірка на відхилення вартості не більше ніж 10 грн.
            {
                expensiveToys.Add(toy);
            }
            else
            {
                break; // Виходимо з циклу, якщо вартість іграшки нижча
            }
        }

        // Виведення результатів
        Console.WriteLine("Найбiльш коштовнi грашки:");
        foreach (var toy in expensiveToys)
        {
            Console.WriteLine(toy);
        }
    }
}
