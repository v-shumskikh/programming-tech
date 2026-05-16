using Lab2;

var savings = new InterestEarningAccount("Анна", 5000);
var gift = new GiftCardAccount("Борис", 200, 50);

Console.WriteLine($"[Накопительный] {savings.Number} ({savings.Owner}), баланс {savings.Balance}");
Console.WriteLine($"[Подарочная]    {gift.Number} ({gift.Owner}), баланс {gift.Balance}");

// Унаследованные методы базового класса работают без переопределения
savings.MakeDeposit(1000, DateTime.Now, "Пополнение");
gift.MakeWithdrawal(50, DateTime.Now, "Покупка кофе");

Console.WriteLine($"[Накопительный] баланс после пополнения: {savings.Balance}");
Console.WriteLine($"[Подарочная]    баланс после покупки: {gift.Balance}");
