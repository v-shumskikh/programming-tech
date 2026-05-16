using Lab2;

var savings = new InterestEarningAccount("Анна", 5000);
var gift = new GiftCardAccount("Борис", 200, 50);

Console.WriteLine("=== До конца месяца ===");
Console.WriteLine($"Накопительный: {savings.Balance}");
Console.WriteLine($"Подарочная карта: {gift.Balance}");

// Один и тот же вызов — у каждого наследника срабатывает своя логика (полиморфизм)
savings.PerformMonthEndTransactions();
gift.PerformMonthEndTransactions();

Console.WriteLine();
Console.WriteLine("=== После конца месяца ===");
Console.WriteLine($"Накопительный (должны добавиться проценты): {savings.Balance}");
Console.WriteLine($"Подарочная карта (должно прийти автопополнение): {gift.Balance}");

Console.WriteLine();
Console.WriteLine("История накопительного счёта:");
Console.WriteLine(savings.GetAccountHistory());
