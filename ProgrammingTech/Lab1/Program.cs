using Lab1;

var account = new BankAccount("Иван Петров", 1000);
Console.WriteLine($"Счёт {account.Number} ({account.Owner}) — баланс {account.Balance}");

account.MakeDeposit(500, DateTime.Now, "Зарплата");
account.MakeWithdrawal(200, DateTime.Now, "Покупка в магазине");

Console.WriteLine($"Баланс после двух операций: {account.Balance}");
Console.WriteLine();
Console.WriteLine("История операций:");
Console.WriteLine(account.GetAccountHistory());
