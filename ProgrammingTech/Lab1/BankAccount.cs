using System.Text;

namespace Lab1;

public class BankAccount
{
    // Счётчик номеров счетов — общий для всех экземпляров (static), увеличивается при создании каждого счёта
    private static int s_accountNumberSeed = 1234567890;

    // История всех операций по счёту. Баланс считаем как сумму, отдельное поле не храним
    private List<Transaction> allTransactions = new List<Transaction>();

    public string Number { get; }
    public string Owner { get; set; }

    // Баланс — это сумма всех транзакций (пополнения с плюсом, снятия с минусом)
    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var item in allTransactions)
            {
                balance += item.Amount;
            }
            return balance;
        }
    }

    public BankAccount(string name, decimal initialBalance)
    {
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;

        Owner = name;
        // Начальный баланс заводим через MakeDeposit — чтобы он попал в историю как обычная транзакция
        MakeDeposit(initialBalance, DateTime.Now, "Начальный баланс");
    }

    // Пополнение счёта. Сумма должна быть положительной
    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Сумма пополнения должна быть положительной");
        }
        var deposit = new Transaction(amount, date, note);
        allTransactions.Add(deposit);
    }

    // Снятие со счёта. Нельзя уйти в минус — выбрасываем исключение
    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Сумма снятия должна быть положительной");
        }
        if (Balance - amount < 0)
        {
            throw new InvalidOperationException("На счёте недостаточно средств");
        }
        // Снятие сохраняем со знаком минус, чтобы автоматически уменьшать баланс при суммировании
        var withdrawal = new Transaction(-amount, date, note);
        allTransactions.Add(withdrawal);
    }

    // Возвращает историю операций в виде таблицы (через StringBuilder — быстрее, чем склейка строк +)
    public string GetAccountHistory()
    {
        var report = new StringBuilder();
        report.AppendLine("Дата\t\tСумма\tБаланс\tПримечание");

        decimal balance = 0;
        foreach (var item in allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
        }
        return report.ToString();
    }
}
