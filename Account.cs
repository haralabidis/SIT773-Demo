using System;

public class Account
{
    private decimal _balance;
    private string _name;
    public string Name
    {
        get { return _name; }
    }

    public Account(string name, decimal startingBalanace)
    {
        _name = name;
        _balance = startingBalanace;
    }

    public bool Withdraw(decimal amountToWithdraw)
    {
        if (amountToWithdraw > 0 && amountToWithdraw <= _balance)
        {
            _balance -= amountToWithdraw;
            return true;
        }
        return false;
    }

    public bool Deposit(decimal amountToDeposit)
    {
        if (amountToDeposit > 0)
        {
            _balance += amountToDeposit;
            return true;
        }
        return false;
    }

    public void Print()
    {
        Console.WriteLine($"The account name is: {_name}");
        Console.WriteLine($"The account balance is: {_balance}");
    }
}