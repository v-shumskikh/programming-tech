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

        // Кредитный счёт может открываться с нулевым балансом — пополнения тогда не делаем
        if (initialBalance > 0)
        {
            MakeDeposit(initialBalance, DateTime.Now, "Начальный баланс");
        }
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

        // Если снятие выводит баланс ниже минимума — даём наследнику решить, что делать:
        // null — никакой комиссии (или сам выбросит исключение, как делает база)
        // Transaction — это будет дополнительная транзакция-комиссия
        Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < minimumBalance);

        var withdrawal = new Transaction(-amount, date, note);
        allTransactions.Add(withdrawal);
        if (overdraftTransaction != null)
        {
            allTransactions.Add(overdraftTransaction);
        }
    }

    // Поведение по умолчанию для обычного счёта: при превышении — ошибка.
    // Наследники (например, кредитная линия) могут переопределить и вернуть комиссию.
    protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
    {
        if (isOverdrawn)
        {
            throw new InvalidOperationException("Недостаточно средств с учётом минимального баланса");
        }
        return null;
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
