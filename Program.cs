using System;
using System.Collections.Generic;
public enum MenuOption
{
    NEW_ACCOUNT,
    WITHDRAW,
    DEPOSIT,
    TRANSFER,
    PRINT,
    PRINT_TRANSACTIONS,
    QUIT
}

public class Program
{
    private static readonly string s_select = "Please select from the following options:";
    private static readonly string s_seperator = "***************************************";
    private static readonly string s_addAccount = "1 - Add account";
    private static readonly string s_withdraw = "2 - Withdraw";
    private static readonly string s_deposit = "3 - Deposit";
    private static readonly string s_transfer = "4 - Transfer";
    private static readonly string s_print = "5 - Print";
    private static readonly string s_printTransactionHistory = "6 - Print Transaction History";
    private static readonly string s_quit = "7 - Quit";
    private static readonly string s_selectPrompt = "Please select an option between 1 and 5";
    private static readonly string s_wrongInpout = "You did not enter a number! Please try again: ";
    private static readonly string s_createAccountName = "Please provide the name for the new account: ";
    private static readonly string s_createAccountBalance = "Please provide the starting balance for the account: ";
    private static readonly string s_depositAmount = "Please select the amount you wish to deposit: ";
    private static readonly string s_withdrawAmount = "Please select the amount you wish to withdraw: ";
    private static readonly string s_transferAmount = "Please select the amount you wish to transfer: ";


    public static void Main()
    {
        MenuOption userSelection;
        Bank bank = new Bank();
        do
        {
            userSelection = ReadUserOption();

            switch (userSelection)
            {
                case MenuOption.NEW_ACCOUNT:
                    {
                        DoCreateAccount(bank);
                        break;
                    }
                case MenuOption.DEPOSIT:
                    {
                        DoDeposit(bank);
                        break;
                    }
                case MenuOption.WITHDRAW:
                    {
                        DoWithdraw(bank);
                        break;
                    }
                case MenuOption.TRANSFER:
                    {
                        DoTransfer(bank);
                        break;
                    }
                case MenuOption.PRINT:
                    {
                        DoPrint(bank);
                        break;
                    }
                case MenuOption.PRINT_TRANSACTIONS:
                {
                    bank.PrintTransactionHistory();
                    break;
                }
                case MenuOption.QUIT:
                    {
                        Console.WriteLine("Goodbye");
                        break;
                    }
            }
        } while (userSelection != MenuOption.QUIT);
    }

    public static MenuOption ReadUserOption()
    {
        int option;
        Console.WriteLine(s_select);
        Console.WriteLine(s_seperator);
        Console.WriteLine(s_addAccount);
        Console.WriteLine(s_withdraw);
        Console.WriteLine(s_deposit);
        Console.WriteLine(s_transfer);
        Console.WriteLine(s_print);
        Console.WriteLine(s_printTransactionHistory);
        Console.WriteLine(s_quit);
        Console.WriteLine(s_seperator);
        do
        {
            try
            {
                option = Convert.ToInt32(Console.ReadLine());
                if (option > 7 || option < 1)
                {
                    Console.WriteLine(s_selectPrompt);
                }
            }
            catch
            {
                Console.Write(s_wrongInpout);
                option = -1;
            }
        } while (option > 7 || option < 1);

        return (MenuOption)(option - 1);
    }

    private static void DoCreateAccount(Bank bank)
    {
        Console.WriteLine(s_createAccountName);
        string accountName = Console.ReadLine();

        decimal startingBalance;
        Console.WriteLine(s_createAccountBalance);
        try
        {
            startingBalance = Convert.ToUInt32(Console.ReadLine());
            Account account = new Account(accountName, startingBalance);
            bank.AddAccount(account);
            Console.WriteLine("Account created successfully");
        }
        catch
        {
            Console.WriteLine(s_wrongInpout);
        }
    }

    private static void DoDeposit(Bank toBank)
    {
        Account toAccount = FindAccount(toBank); 
        if (toAccount == null) return;
        
        decimal amount;
        Console.WriteLine(s_depositAmount);
        try
        {
            amount = Convert.ToUInt32(Console.ReadLine());
            DepositTransaction depositTransaction = new DepositTransaction(toAccount, amount);
            toBank.ExecuteTransaction(depositTransaction);
            depositTransaction.Print();
        }
        catch
        {
            Console.WriteLine(s_wrongInpout);
        }
    }

    private static void DoWithdraw(Bank fromBank)
    {
        Account fromAccount = FindAccount(fromBank); 
        if (fromAccount == null) return;

        decimal amount;
        Console.WriteLine(s_withdrawAmount);
        try
        {
            amount = Convert.ToUInt32(Console.ReadLine());
            WithdrawTransaction withdrawTransaction = new WithdrawTransaction(fromAccount, amount);
            fromBank.ExecuteTransaction(withdrawTransaction);
            withdrawTransaction.Print();
        }
        catch
        {
            Console.WriteLine(s_wrongInpout);
        }
    }

       private static void DoTransfer(Bank bank)
    {
        Console.WriteLine("------Select from account---------");
        Account fromAccount = FindAccount(bank); 
        if (fromAccount == null) return;
        Console.WriteLine("------Select to account---------");
        Account toAccount = FindAccount(bank); 
        if (toAccount == null) return;

        decimal amount;
        Console.WriteLine(s_transferAmount);
        try
        {
            amount = Convert.ToUInt32(Console.ReadLine());
            TransferTransaction transferTransaction = new TransferTransaction(fromAccount, toAccount, amount);
            bank.ExecuteTransaction(transferTransaction);
            transferTransaction.Print();
        }
        catch
        {
            Console.WriteLine(s_wrongInpout);
        }
    }

    private static void DoPrint(Bank bank)
    {
        bank.Print();
    }

    private static Account FindAccount(Bank fromBank)
    {
        Console.Write("Enter account name: "); 
        String name = Console.ReadLine();
        Account result = fromBank.GetAccount(name);
        if (result == null)
        {
            Console.WriteLine($"No account found with name {name}");
        }
        return result;
    }
}

// declare a delegate
public delegate int AdditionDelegate(int first, int second);

public class DelegateDemo
{
    // target method
    static int AdditionMethod(int first, int second)
    {
        return first + second;
    }

    //Instantiate the delegate
    //static AdditionDelegate additionDelegate = AdditionMethod;
    private static AdditionDelegate additionDelegate => (first, second) => first + second;
    
    //store the value via the delegate
    int addresult = additionDelegate(1, 3);
}




