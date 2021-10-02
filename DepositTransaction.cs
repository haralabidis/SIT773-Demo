using System;

public class DepositTransaction: Transaction
{
    private Account _account;
    private bool _success = false;
    public override bool Success => _success;
    
    public DepositTransaction(Account account, decimal amount): base(amount)
    {
        _account = account;
    }

    public override void Execute()
    {
        base.Execute();
        _success = _account.Deposit(_amount);
    }

    public override void Rollback()
    {
        base.Execute();
        _success = _account.Withdraw(_amount);
    }

    public override void Print()
    {
        if (_reversed && _success)
        {
            Console.WriteLine("The transaction was reversed");
        }
        else if (_success)
        {
            Console.WriteLine($"A deposit of {_amount} to {_account.Name} was succesfully completed");
        }
        else
        {
            Console.WriteLine("The transaction was not successful");
        }
    }

}