using System;

public abstract class Transaction
{
    protected decimal _amount;
    private bool _executed;
    protected bool _reversed;
    private DateTime _dateStamp;

    public bool Executed => _executed;
    public bool Reversed => _reversed;
    public DateTime DateStamp => _dateStamp;
    public abstract bool Success { get; }

    public Transaction(decimal amount)
    {
        _amount = amount;
    }
    
    public Transaction(decimal amount, DateTime dateStamp)
    {
        _amount = amount;
        _dateStamp = dateStamp;
    }

    public abstract void Print();

    public virtual void Execute()
    {
        if (_executed)
        {
            throw new Exception("This transaction has already been executed.");
        }
        _executed = true;
        _dateStamp = DateTime.Now;
    }

    public virtual void Rollback()
    {
        if (!_executed)
        {
            throw new Exception("Cannot rollback this transaction as it has not been executed before");
        }
        if (_reversed)
        {
            throw new Exception("Cannot rollback transaction, it has already been rolled back");
        }
        _reversed = true;
        _executed = false;
    }
}