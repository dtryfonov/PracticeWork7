using System;
using System.Collections.Generic;

ClothesRail<Dress> inProgress = new("Сукні в роботі");
inProgress.Add(new("Олена", "шовк", 8500m, new(2026, 11, 8), 42));
inProgress.Add(new("Марія", "льон", 5200m, new(2026, 11, 12), 38));
inProgress.Add(new("Ірина", "атлас", 9100m, new(2026, 11, 15), 40));

ClothesRail<Dress> ready = new("Сукні готово");
ready.Add(new("Софія", "бавовна", 4600m, new(2026, 11, 5), 36));

ClothesRail<Suit> suits = new("Костюми");
suits.Add(new("Андрій", "вовна", 13500m, new(2026, 11, 10), 2));
suits.Add(new("Тарас", "віскоза", 11200m, new(2026, 11, 18), 3));

Console.WriteLine("До перенесення:");
Console.WriteLine(inProgress);
Console.WriteLine(ready);
Console.WriteLine(suits);

Finish(inProgress, ready, 0);
Finish(inProgress, ready, 0);

Console.WriteLine("\nПісля двох перенесень:");
Console.WriteLine(inProgress);
Console.WriteLine(ready);

Dress lastDress = ready[ready.Count - 1];
Console.WriteLine($"\nСукня через індексатор: {lastDress}");

static void Finish<T>(ClothesRail<T> from, ClothesRail<T> to, int index)
{
    T garment = from[index];
    from.RemoveAt(index);
    to.Add(garment);
}

class ClothesRail<T>(string name)
{
    private readonly List<T> garments = [];

    public string Name => name;
    public int Count => garments.Count;
    public T this[int index] => garments[index];

    public void Add(T garment) => garments.Add(garment);
    public void RemoveAt(int index) => garments.RemoveAt(index);

    public override string ToString() =>
        $"{Name} ({Count}): {string.Join("; ", garments)}";
}

record Dress(string ClientName, string Fabric, decimal Price,
    DateOnly DueDate, int Size)
{
    public override string ToString() =>
        $"{ClientName}, {Fabric}, розмір {Size}, {Price} грн, {DueDate:dd.MM}";
}

record Suit(string ClientName, string Fabric, decimal Price,
    DateOnly DueDate, int Pieces)
{
    public override string ToString() =>
        $"{ClientName}, {Fabric}, {Pieces}-ка, {Price} грн, {DueDate:dd.MM}";
}