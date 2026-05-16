namespace Lab2;

// Подарочная карта. Отличается от базового счёта тем, что у неё
// есть собственное поле monthlyDeposit — сумма автопополнения раз в месяц.
public class GiftCardAccount : BankAccount
{
    private readonly decimal monthlyDeposit;

    public GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0)
        : base(name, initialBalance)
    {
        this.monthlyDeposit = monthlyDeposit;
    }
}
