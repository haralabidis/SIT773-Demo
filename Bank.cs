using System;
using System.Collections.Generic;
using System.Linq;

public class Bank
{
    private List<Account> _accounts;
    private List<Transaction> _transactions;

    public Bank()
    {
        _accounts = new List<Account>();
        _transactions = new List<Transaction>();
    }
    public void AddAccount(Account account)
    {
        var matchingAccount = _accounts.FirstOrDefault(existingAccount => existingAccount.Name == account.Name);
        if(matchingAccount == null)
        {
            _accounts.Add(account);
        }
        else
        {
            throw new Exception("Account already exists");
        }
    }

    public Account GetAccount(string accountName)
    {
        foreach (Account acc in _accounts)
        {
            if (acc.Name.ToLower().Trim() == accountName.ToLower().Trim())
            {
                return acc;
            }
        }
        return null;
    }

    public void ExecuteTransaction(Transaction transaction)
    {
        transaction.Execute();
        _transactions.Add(transaction);
    }

    public void Print()
    {
        foreach (Account account in _accounts)
        {
            account.Print();
        }
    }

    public void PrintTransactionHistory()
    {
        foreach (Transaction transaction in _transactions)
        {
            transaction.Print();
        }
    }
}