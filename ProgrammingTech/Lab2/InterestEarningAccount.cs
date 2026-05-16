namespace Lab2;

// Накопительный счёт.
public class InterestEarningAccount : BankAccount
{
    public InterestEarningAccount(string name, decimal initialBalance)
        : base(name, initialBalance)
    {
    }

    // В конце месяца начисляем 5% на остаток, если он больше 500
    public override void PerformMonthEndTransactions()
    {
        if (Balance > 500m)
        {
            decimal interest = Balance * 0.05m;
            MakeDeposit(interest, DateTime.Now, "Начисление процентов");
        }
    }
}
