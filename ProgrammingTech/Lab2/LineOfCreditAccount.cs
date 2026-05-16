namespace Lab2;

// Кредитная линия: можно уходить в минус, но не глубже -creditLimit.
// За уход в минус — фиксированная комиссия. В конце месяца — процент с долга.
public class LineOfCreditAccount : BankAccount
{
    public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit)
        : base(name, initialBalance, -creditLimit)
    {
    }

    // Вместо ошибки при превышении — берём комиссию 20 рублей
    protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn)
    {
        return isOverdrawn
            ? new Transaction(-20, DateTime.Now, "Комиссия за овердрафт")
            : null;
    }

    // Если на конец месяца есть долг — берём 7% от долга в виде процентов за пользование
    public override void PerformMonthEndTransactions()
    {
        if (Balance < 0)
        {
            decimal interest = -Balance * 0.07m;
            MakeWithdrawal(interest, DateTime.Now, "Проценты за пользование кредитом");
        }
    }
}
