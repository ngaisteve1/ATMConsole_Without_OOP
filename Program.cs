using System;

namespace atm2
{
    class Program
    {
         static String cur = "RM ";
        static double latestAccount_amt = 1000;

        static void Main(string[] args)
        {
            mainOperation();
        }

        private static void showMenu1()
        {
            Console.WriteLine(" ------------------------");
            Console.WriteLine("| Maybank ATM Main Menu  |");
            Console.WriteLine("|                        |");
            Console.WriteLine("| 1. Insert ATM card     |");
            Console.WriteLine("| 2. Exit                |");
            Console.WriteLine("|                        |");
            Console.WriteLine(" ------------------------");
            Console.Write("Enter your option: ");            
        }

        private static void showMenu2()
        {
            Console.WriteLine(" ---------------------------");
            Console.WriteLine("| Maybank ATM Secure Menu    |");
            Console.WriteLine("|                            |");
            Console.WriteLine("| 1. Check balance           |");
            Console.WriteLine("| 2. Deposit                 |");
            Console.WriteLine("| 3. Withdraw                |");
            Console.WriteLine("| 4. Logout                  |");
            Console.WriteLine("|                            |");
            Console.WriteLine(" ---------------------------");
            Console.Write("Enter your option: ");
        }

        private static void printNewBalance(double newBalance)
        {
            Console.WriteLine("Balance is: RM " + newBalance.ToString("N"));
            Console.WriteLine();
        }

        private static bool validateInput(string input) {
            bool passValidation = false;
            int myInt = 0;
            if (!String.IsNullOrWhiteSpace(input)) {
                if (int.TryParse(input, out myInt))
                    passValidation = true;                
            }
            return passValidation;
        }

        private static void deposit() {
            double deposit_amt = 0;
            Console.Write("Enter your the deposit amount: " + cur);
            deposit_amt = Convert.ToDouble(Console.ReadLine());
            if (deposit_amt > 0)
            {
                latestAccount_amt = latestAccount_amt + deposit_amt;

                Console.WriteLine("You have successfully deposited " + cur + deposit_amt);
                printNewBalance(latestAccount_amt);
            }
            else
            {
                Console.WriteLine("Invalid deposit amount. Try again.");
            }
        }

        private static void withdraw() {
            double withdraw_amt = 0;
            double minimum_kept_amt = 20;

            Console.Write("Enter your the withdraw amount: " + cur);
            withdraw_amt = Convert.ToDouble(Console.ReadLine());

            if (withdraw_amt > 0)
            {
                if (withdraw_amt > latestAccount_amt)
                {
                    Console.WriteLine("Withdrawal failed. You do not have enough fund to withdraw " + cur + withdraw_amt);
                }
                else if ((latestAccount_amt - withdraw_amt) < minimum_kept_amt)
                {
                    Console.WriteLine("Withdrawal failed. Your account needs to have minimum " + cur + minimum_kept_amt);
                }
                else
                {
                    latestAccount_amt = latestAccount_amt - withdraw_amt;

                    Console.WriteLine("Please collect your money. You have successfully withdraw " + withdraw_amt);
                    printNewBalance(latestAccount_amt);
                }
            }
            else
            {
                Console.WriteLine("Invalid deposit amount. Try again.");
            }
        }

        private static bool checkCardNoPassword(int cardNo, int pwd) {
            bool pass = false;

            var myUserList = new Dictionary<int, int>();
            myUserList.Add(888888, 123456);
            myUserList.Add(333333, 654321);
            myUserList.Add(999999, 666666);

            foreach (var d in myUserList)
            {
                if (d.Key == cardNo && d.Value == pwd)
                {
                    pass = true;
                }
            }
            return pass;
        }

        private static void mainOperation()
        {
            string menu0 = "";
            int menu1 = 0;
            int menu2 = 0;
            int cardNo = 0;
            int pin = 0;            
            int tries = 0;
            int maxTries = 3;

            do
            {
                showMenu1();
                menu0 = Console.ReadLine();
                if (!validateInput(menu0))
                {
                    Console.WriteLine("Invalid Option Entered.");
                }
                else {
                    menu1 = Convert.ToInt32(menu0);
                    switch (menu1)
                    {
                        case 1:
                            Console.WriteLine("");
                            Console.Write("Enter ATM Card Number: ");
                            cardNo = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter 6 Digit PIN: ");
                            pin = Convert.ToInt32(Console.ReadLine());

                            if (checkCardNoPassword(cardNo, pin))
                            {
                                do
                                {
                                    showMenu2();
                                    menu0 = Console.ReadLine();
                                    if (!validateInput(menu0))
                                    {
                                        Console.WriteLine("Invalid Option Entered.");
                                    }
                                    else
                                    {
                                        menu2 = Convert.ToInt32(menu0);
                                        switch (menu2)
                                        {
                                            case 1:
                                                printNewBalance(latestAccount_amt);
                                                break;
                                            case 2:
                                                deposit();
                                                break;
                                            case 3:
                                                withdraw();
                                                break;
                                            case 4:
                                                Console.WriteLine("You have succesfully logout.");
                                                break;
                                            default:
                                                Console.WriteLine("Invalid Option Entered.");
                                                break;
                                        }
                                    }

                                } while (menu2 != 4);
                            }
                            else {
                                tries++;

                                if (tries >= maxTries)
                                {
                                    Console.WriteLine("Account locked. Please go to the nearest Maybank branch to reset your PIN.");
                                    Console.WriteLine("Thank you for using Maybank. ");
                                    System.Environment.Exit(1);
                                }

                                Console.WriteLine("Invalid PIN.");
                            }
                            
                            break;
                        case 2:
                            break;
                        default:
                            Console.WriteLine("Invalid Option Entered.");
                            break;
                    }
                }
                
            } while (menu1 != 2);
            Console.WriteLine("Thank you for using Maybank. ");

        }
    }
}
