namespace Lab1;

public class BankAccount
{
    private static int s_accountNumberSeed = 1234567890;

    // Все операции по счёту (на их основе считается баланс)
    private List<Transaction> allTransactions = new List<Transaction>();

    public string Number { get; }
    public string Owner { get; set; }

    // Баланс — это сумма всех транзакций (вычисляется при каждом обращении)
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
        // Начальный баланс заводим как первую операцию-пополнение
        MakeDeposit(initialBalance, DateTime.Now, "Начальный баланс");
    }

    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        var deposit = new Transaction(amount, date, note);
        allTransactions.Add(deposit);
    }

    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        // В транзакции снятия сумма отрицательная — чтобы баланс уменьшался при суммировании
        var withdrawal = new Transaction(-amount, date, note);
        allTransactions.Add(withdrawal);
    }
}
