using Lab1;

var account = new BankAccount("Иван Петров", 1000);
Console.WriteLine($"Счёт {account.Number} ({account.Owner}) — баланс {account.Balance}");

account.MakeDeposit(500, DateTime.Now, "Зарплата");
Console.WriteLine($"После пополнения на 500: баланс {account.Balance}");

account.MakeWithdrawal(200, DateTime.Now, "Покупка в магазине");
Console.WriteLine($"После снятия 200: баланс {account.Balance}");
