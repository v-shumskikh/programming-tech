using System.Text;

namespace Lab2;

public class BankAccount
{
    private static int s_accountNumberSeed = 1234567890;

    private List<Transaction> allTransactions = new List<Transaction>();

    // Минимальный допустимый баланс. Для обычного счёта = 0,
    // для кредитной линии будет отрицательным.
    private readonly decimal minimumBalance;

    public string Number { get; }
    public string Owner { get; set; }

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

    // Конструктор без minimumBalance — пробрасывает 0 в основной (через :this)
    public BankAccount(string name, decimal initialBalance)
        : this(name, initialBalance, 0)
    {
    }

    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;

        Owner = name;
        this.minimumBalance = minimumBalance;
        MakeDeposit(initialBalance, DateTime.Now, "Начальный баланс");
    }

    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Сумма пополнения должна быть положительной");
        }
        var deposit = new Transaction(amount, date, note);
        allTransactions.Add(deposit);
    }

    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Сумма снятия должна быть положительной");
        }
        if (Balance - amount < minimumBalance)
        {
            throw new InvalidOperationException("Недостаточно средств с учётом минимального баланса");
        }
        var withdrawal = new Transaction(-amount, date, note);
        allTransactions.Add(withdrawal);
    }

    // Месячные операции (проценты, комиссии, автопополнения).
    // В базовом классе ничего не делает — каждый наследник переопределяет под свою логику.
    public virtual void PerformMonthEndTransactions()
    {
    }

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
