using System;

class SimpleBankSystem
{
    // Stores account balance
    static double balance = 0.0;

    // Fixed PIN for login
    const int CorrectPIN = 1234;

    // This method is called from Program.cs
    // It acts as the entry point for the Banking System
    public static void Run()
    {
        // User must login before accessing banking features
        if (!Login())
        {
            Console.WriteLine("Too many incorrect attempts. Account locked.");
            return;
        }

        bool exitBank = false;

        while (!exitBank)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n******************************");
            Console.WriteLine("     SIMPLE BANKING SYSTEM     ");
            Console.WriteLine("******************************");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Check Balance");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            Console.ResetColor();

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Deposit();
                    break;
                case "2":
                    Withdraw();
                    break;
                case "3":
                    CheckBalance();
                    break;
                case "4":
                    exitBank = true;
                    Console.WriteLine("Exiting Banking System...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select 1-4.");
                    break;
            }
        }
    }

    // Handles user authentication with 3 attempts
    static bool Login()
    {
        int attempts = 0;

        while (attempts < 3)
        {
            Console.Write("Enter PIN: ");
            bool validPin = int.TryParse(Console.ReadLine(), out int enteredPin);

            if (validPin && enteredPin == CorrectPIN)
            {
                Console.WriteLine("Login successful!");
                return true;
            }
            else
            {
                attempts++;
                Console.WriteLine($"Incorrect PIN. Attempts remaining: {3 - attempts}");
            }
        }
        return false;
    }

    // Adds money to balance
    static void Deposit()
    {
        Console.Write("Enter deposit amount: ");
        if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
        {
            balance += amount;
            Console.WriteLine($"Deposit successful. Balance: {balance}");
        }
        else
        {
            Console.WriteLine("Invalid amount.");
        }
    }

    // Withdraws money if sufficient balance exists
    static void Withdraw()
    {
        Console.Write("Enter withdrawal amount: ");
        if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
        {
            if (amount <= balance)
            {
                balance -= amount;
                Console.WriteLine($"Withdrawal successful. Balance: {balance}");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }
        else
        {
            Console.WriteLine("Invalid amount.");
        }
    }

    // Displays current balance
    static void CheckBalance()
    {
        Console.WriteLine($"Current balance: {balance}");
    }
}
