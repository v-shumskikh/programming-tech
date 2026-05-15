namespace Lab1;

public class BankAccount
{
    // Счётчик для автогенерации уникальных номеров счетов
    private static int s_accountNumberSeed = 1234567890;

    public string Number { get; }
    public string Owner { get; set; }
    public decimal Balance { get; }

    public BankAccount(string name, decimal initialBalance)
    {
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;

        Owner = name;
        Balance = initialBalance;
    }
}
