namespace Lab2;

// Накопительный счёт. Поведение пока повторяет базовый класс.
public class InterestEarningAccount : BankAccount
{
    public InterestEarningAccount(string name, decimal initialBalance)
        : base(name, initialBalance)
    {
    }
}
