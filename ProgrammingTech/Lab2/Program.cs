using Lab2;

var savings = new InterestEarningAccount("Анна", 5000);
var gift = new GiftCardAccount("Борис", 200, 50);
var credit = new LineOfCreditAccount("Виктор", 0, 2000);

Console.WriteLine("=== Стартовые балансы ===");
Console.WriteLine($"Накопительный: {savings.Balance}");
Console.WriteLine($"Подарочная карта: {gift.Balance}");
Console.WriteLine($"Кредитная линия: {credit.Balance}");

// Уходим за минимальный баланс — для кредитной линии это разрешено,
// сработает CheckWithdrawalLimit и спишет комиссию вместо ошибки
credit.MakeWithdrawal(2500, DateTime.Now, "Снятие наличных");
Console.WriteLine($"Кредитная линия после снятия 2500 (плюс комиссия 20): {credit.Balance}");

// Конец месяца — у каждого типа счёта своя логика (полиморфизм)
savings.PerformMonthEndTransactions();
gift.PerformMonthEndTransactions();
credit.PerformMonthEndTransactions();

Console.WriteLine();
Console.WriteLine("=== После конца месяца ===");
Console.WriteLine($"Накопительный (+проценты): {savings.Balance}");
Console.WriteLine($"Подарочная карта (+автопополнение): {gift.Balance}");
Console.WriteLine($"Кредитная линия (-проценты с долга): {credit.Balance}");

Console.WriteLine();
Console.WriteLine("История кредитной линии:");
Console.WriteLine(credit.GetAccountHistory());

// Проверка: для обычного счёта снятие выше остатка по-прежнему — ошибка
try
{
    savings.MakeWithdrawal(99999, DateTime.Now, "Слишком много");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Ошибка снятия с накопительного: {ex.Message}");
}
