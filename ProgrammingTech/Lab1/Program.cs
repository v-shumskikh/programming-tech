using Lab1;

var account = new BankAccount("Иван Петров", 1000);
Console.WriteLine($"Счёт {account.Number} ({account.Owner}) — баланс {account.Balance}");

account.MakeDeposit(500, DateTime.Now, "Зарплата");
account.MakeWithdrawal(200, DateTime.Now, "Покупка в магазине");

// Проверяем валидацию: пытаемся снять больше, чем есть
try
{
    account.MakeWithdrawal(10000, DateTime.Now, "Слишком много");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Ошибка снятия: {ex.Message}");
}

// Проверяем валидацию: пытаемся положить отрицательную сумму
try
{
    account.MakeDeposit(-100, DateTime.Now, "Отрицательное пополнение");
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Ошибка пополнения: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("История операций:");
Console.WriteLine(account.GetAccountHistory());
