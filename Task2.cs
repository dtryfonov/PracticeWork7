using System;
using System.Collections.Generic;
using System.Linq;

ClothesRail<Dress> dresses = new("Сукні");
dresses.Add(new("Олена", "шовк", 8500m, new(2026, 11, 8), 42));
dresses.Add(new("Марія", "льон", 5200m, new(2026, 11, 12), 38));
dresses.Add(new("Наталія", "шовк", 7900m, new(2026, 11, 20), 40));

ClothesRail<Suit> suits = new("Костюми");
suits.Add(new("Андрій", "вовна", 13500m, new(2026, 11, 10), 2));
suits.Add(new("Тарас", "віскоза", 11200m, new(2026, 11, 7), 3));

Console.WriteLine($"Вартість суконь: {dresses.TotalPrice} грн");
Console.WriteLine($"Вартість костюмів: {suits.TotalPrice} грн");

Suit? dueFirst = suits.DueFirst();
Console.WriteLine(
    $"Найближча готовність: {dueFirst?.ClientName}, " +
    $"{dueFirst?.Pieces} предмети, {dueFirst?.DueDate:dd.MM.yyyy}");

Console.WriteLine("\nСукні з шовку:");
foreach (Dress dress in dresses.ByFabric("шовк"))
{
    Console.WriteLine(dress);
}

class ClothesRail<T>(string name) where T : Garment
{
    private readonly List<T> garments = [];

    public string Name => name;
    public int Count => garments.Count;
    public T this[int index] => garments[index];
    public decimal TotalPrice => garments.Sum(garment => garment.Price);

    public void Add(T garment) => garments.Add(garment);

    public T? DueFirst() =>
        garments.MinBy(garment => garment.DueDate);

    public List<T> ByFabric(string fabric) =>
        garments.FindAll(garment =>
            string.Equals(garment.Fabric, fabric,
                StringComparison.OrdinalIgnoreCase));
}

abstract record Garment(
    string ClientName,
    string Fabric,
    decimal Price,
    DateOnly DueDate)
{
    public override string ToString() =>
        $"{ClientName}, {Fabric}, {Price} грн, готово {DueDate:dd.MM}";
}

record Dress(
    string ClientName,
    string Fabric,
    decimal Price,
    DateOnly DueDate,
    int Size) : Garment(ClientName, Fabric, Price, DueDate)
{
    public override string ToString() =>
        $"{base.ToString()}, розмір {Size}";
}

record Suit(
    string ClientName,
    string Fabric,
    decimal Price,
    DateOnly DueDate,
    int Pieces) : Garment(ClientName, Fabric, Price, DueDate)
{
    public override string ToString() =>
        $"{base.ToString()}, {Pieces}-ка";
}