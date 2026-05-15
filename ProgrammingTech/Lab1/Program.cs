using Lab1;

// Создаём счёт с начальным балансом
var account = new BankAccount("Иван Петров", 1000);

Console.WriteLine($"Счёт {account.Number}");
Console.WriteLine($"Владелец: {account.Owner}");
Console.WriteLine($"Баланс: {account.Balance}");
