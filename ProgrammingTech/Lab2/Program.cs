using Lab2;

var savings = new InterestEarningAccount("Анна", 5000);

Console.WriteLine($"Накопительный счёт {savings.Number} ({savings.Owner}) — баланс {savings.Balance}");

// Унаследованные методы базового класса работают без переопределения
savings.MakeDeposit(1000, DateTime.Now, "Пополнение");
savings.MakeWithdrawal(200, DateTime.Now, "Снятие наличных");

Console.WriteLine($"Баланс после двух операций: {savings.Balance}");
Console.WriteLine();
Console.WriteLine(savings.GetAccountHistory());
