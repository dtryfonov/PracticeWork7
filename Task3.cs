using System;
using System.Collections.Generic;
using System.Linq;

ClothesRail<Dress> dresses = new("Сукні");
dresses.Add(new("Олена", "шовк", 8500m, new(2026, 11, 8), 42));
dresses.Add(new("Марія", "льон", 5200m, new(2026, 11, 12), 38));

SuitRail suits = new("Костюми");
suits.Add(new("Андрій", "вовна", 13500m, new(2026, 11, 10), 2));
suits.Add(new("Тарас", "віскоза", 11200m, new(2026, 11, 7), 3));
suits.Add(new("Максим", "вовна", 14800m, new(2026, 11, 22), 3));

Report(dresses);
Report(suits);

Console.WriteLine($"\nКількість костюмів-трійок: {suits.ThreePieceCount}");

static void Report(IReadOnlyClothesRail<Garment> rail)
{
    Console.WriteLine(
        $"\nЗвіт: {rail.Name}. Кількість: {rail.Count}. " +
        $"Вартість: {rail.TotalPrice} грн");

    for (int i = 0; i < rail.Count; i++)
    {
        Console.WriteLine($"  {rail[i]}");
    }
}

interface IReadOnlyClothesRail<out T> where T : Garment
{
    string Name { get; }
    int Count { get; }
    decimal TotalPrice { get; }
    T this[int index] { get; }
}

class ClothesRail<T>(string name) : IReadOnlyClothesRail<T>
    where T : Garment
{
    protected readonly List<T> garments = [];

    public string Name => name;
    public int Count => garments.Count;
    public decimal TotalPrice => garments.Sum(garment => garment.Price);
    public T this[int index] => garments[index];

    public void Add(T garment) => garments.Add(garment);
}

class SuitRail(string name) : ClothesRail<Suit>(name)
{
    public int ThreePieceCount =>
        garments.Count(suit => suit.Pieces == 3);
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