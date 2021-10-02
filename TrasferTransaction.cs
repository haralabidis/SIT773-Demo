using System;
public class TransferTransaction: Transaction
{
    private Account _toAccount;
    private Account _fromAccount;
    private DepositTransaction _theDeposit;
    private WithdrawTransaction _theWithdraw;

    public override bool Success => _theWithdraw.Success && _theDeposit.Success;
    
    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        _theWithdraw = new WithdrawTransaction(_fromAccount, _amount);
        _theDeposit = new DepositTransaction(_toAccount, _amount);
    }
    public override void Execute()
    {
        base.Execute();
        try
        {
            _theWithdraw.Execute();
            if (_theWithdraw.Success)
            {
                try
                {
                    _theDeposit.Execute();
                }
                catch
                {
                    Rollback();
                }
                if (!_theDeposit.Success)
                {
                    Rollback();
                }
            }
        }
        catch
        {
            throw new Exception("The transfer could not be executed");
        }
    }
    public override void Rollback()
    {
        if (_theDeposit.Success)
        {
            _theDeposit.Rollback();
            Console.WriteLine($"The deposit of {_amount} to {_toAccount} was reversed");
        }
        if (_theWithdraw.Success)
        {
            _theWithdraw.Rollback();
            Console.WriteLine($"The withdraw of {_amount} from {_fromAccount} was reversed");
        }
    }

    public override void Print()
    {
        Console.WriteLine($"Transferred ${_amount} from {_fromAccount.Name} to {_toAccount.Name} \n");
        Console.WriteLine("--------Withdraw Details------------------");
        _theWithdraw.Print();
        Console.WriteLine();
        Console.WriteLine("--------Deposit Details------------------");
        _theDeposit.Print();
    }
}