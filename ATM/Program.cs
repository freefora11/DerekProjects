using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ATM
{
    class Program
    {
        static void Main(string[] args)
        {


            mainMenu();


        }

        private static void logInMenu()
        {

            Console.WriteLine("Username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();


            if (Verify(username, password))
            {
                logIn(username, password);
            }
            else
            {
                Console.WriteLine("The information you provided isn't correct, try again.");
                logInMenu();

            }
        }

        //Method to Verify if the username and password is correct
        private static bool Verify(string user,string pass)
        {
            //path
            string filePath = "c:\\temp\\" + user + ".txt";

            if (File.Exists(filePath)){
                //create a streamread to read in file
                StreamReader sr = new StreamReader(filePath);
                StringBuilder test = new StringBuilder(sr.ReadLine());
                string password = test.ToString();
                sr.Close();

                //check if the password is correct
                if (password.Equals(pass))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }else
            {
                return false;
            }
                
        }

        private static void logIn(string user,string pass)
        {
            string filePath = "c:\\temp\\" + user + ".txt";
            string[] lines = File.ReadAllLines(filePath);
            var balance = Convert.ToDouble(lines[1]);
            byte count = 0;
            bool controller = true;
            //menu
            while (controller)
            {
                Console.Clear();
                Console.WriteLine("Welcome Back " + user);
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1.Deposit");
                Console.WriteLine("2.Withdraw");
                Console.WriteLine("3.View Balance");
                Console.WriteLine("4.Change Password");
                Console.WriteLine("5.Delete Account");
                Console.WriteLine("6.Log Off");
                if(count > 0)
                    Console.WriteLine("Invalid Key!!!! {0} out of 5 attempts", count);

                var input = Console.ReadKey(true);
                if (char.IsDigit(input.KeyChar))
                {


                    int input2 = int.Parse(input.KeyChar.ToString());

                    switch (input2)
                    {
                        case 1:
                            Console.Clear();
                            deposit(balance,filePath);
                            break;
                        case 2:
                            Console.Clear();
                            withdraw(balance,filePath);
                            break;
                        case 3:
                            viewBalance(filePath);
                            break;
                        case 4:
                            changePass(filePath);
                            break;
                        case 5:
                            deleteAccount(filePath);
                            break;
                        case 6:
                            Console.Clear();
                            logInMenu();
                            break;
                        default:
                            count++;
                            if (count > 4)
                            {
                                Console.Clear();
                                Console.WriteLine("You have been logged off. Press any key to continue...");
                                Console.ReadKey();
                                logInMenu();

                            }
                            break;
                    }
                }
                else
                {
                    count++;
                    if (count > 4)
                    {
                        Console.Clear();
                        Console.WriteLine("You have been logged off. Press any key to continue...");
                        Console.ReadKey();
                        logInMenu();

                    }
                }
                Console.Clear();
            }
        }

        private static void deposit(double balance, string path)
        {
            Console.WriteLine("Enter The Amount to Deposit(Example: 12.50): ");
            var input = Console.ReadLine();

            
            string[] currency = input.Split('.');

            if (currency.Length == 2)
            {
                //makes sure the input are digits
                try
                {
                    var currency1 = Double.Parse(currency[0]);
                    var currency2 = Double.Parse(currency[1]);
                    if(currency2 > 100 || currency[1].Length != 2)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Not a valid format, please try again");
                        Console.WriteLine();
                        deposit(balance, path);
                    }

                    Console.WriteLine(currency1 + "." + currency[1] + " Is this the correct amount? (Y/N)");
                    var correctInput = Console.ReadKey(true);
                    if(correctInput.KeyChar.ToString() == "y" || correctInput.KeyChar.ToString() == "Y")
                    {
                        balance += currency1 + (currency2 / 100);


                        string[] lines = File.ReadAllLines(path);
                        lines[1] = balance.ToString();
                        File.WriteAllLines(path, lines);
                        Console.WriteLine("It has been deposited, press any key to continue");
                        Console.WriteLine(balance);
                        
                    }
                    else
                    {
                        deposit(balance, path);
                    }

                Console.ReadKey();
                }
                catch (FormatException e)
                {
                    Console.WriteLine();
                    Console.WriteLine("Not a valid format, please try again");
                    Console.WriteLine();
                    deposit(balance,path);
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Not a valid format, please try again");
                Console.WriteLine();
                deposit(balance,path);
            }
        }

        private static void withdraw(double balance, string path)
        {
            Console.WriteLine("Enter The Amount to Withdraw(Example: 12.50): ");
            var input = Console.ReadLine();


            string[] currency = input.Split('.');

            if (currency.Length == 2)
            {
                //makes sure the input are digits
                try
                {
                    var currency1 = Double.Parse(currency[0]);
                    var currency2 = Double.Parse(currency[1]);
                    if (currency2 > 100 || currency[1].Length != 2)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Not a valid format, please try again");
                        Console.WriteLine();
                        deposit(balance, path);
                    }

                    Console.WriteLine(currency1 + "." + currency2 + " Is this the correct amount? (Y/N)");
                    var correctInput = Console.ReadKey(true);
                    if (correctInput.KeyChar.ToString() == "y" || correctInput.KeyChar.ToString() == "Y")
                    {
                        balance -= currency1 + (currency2 / 100);


                        string[] lines = File.ReadAllLines(path);
                        lines[1] = balance.ToString();
                        File.WriteAllLines(path, lines);
                        Console.WriteLine("It has been deposited, press any key to continue");
                        Console.WriteLine(balance);

                    }
                    else
                    {
                        deposit(balance, path);
                    }

                    Console.ReadKey();
                }
                catch (FormatException e)
                {
                    Console.WriteLine();
                    Console.WriteLine("Not a valid format, please try again");
                    Console.WriteLine();
                    deposit(balance, path);
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Not a valid format, please try again");
                Console.WriteLine();
                deposit(balance, path);
            }
        }

        private static void viewBalance(string path)
        {
            string[] lines = File.ReadAllLines(path);
            Console.WriteLine("Your balance is: $" + lines[1]);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        private static void changePass(string path)
        {
            string[] lines = File.ReadAllLines(path);
            Console.Write("Enter Current Password: ");
            string currentPassword = Console.ReadLine();
            Console.Write("Enter New Password: ");
            string newPassword = Console.ReadLine();
            Console.Write("Enter New Password Again: ");
            string newPassword2 = Console.ReadLine();

            if (lines[0].Equals(currentPassword))
            {
                if (newPassword.Equals(newPassword2))
                {
                    lines[0] = newPassword;
                    File.WriteAllLines(path, lines);
                    Console.WriteLine("Password has been changed. Press any key to continue.");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("New Passwords don't match.");
                    changePass(path);
                }
            }
            else
            {
                Console.WriteLine("Current password is wrong.");
                changePass(path);
            }
        }

        private static void deleteAccount(string path)
        {
            Console.WriteLine("Are you sure you want to delete your account? \n (Once accepted, you cannot undo!) Type Yes:");
            var input = Console.ReadLine();
            if (input.Equals("Yes"))
            {
                File.Delete(path);
                logInMenu();
            }
            else
            {
                Console.WriteLine("Your account wasn't deleted");
                
            }
        }

        private static void createAccount()
        {
            string filePath = "c:\\temp\\";
            Console.WriteLine("Enter a username: ");
            var username = Console.ReadLine();
            Console.Write("Enter a password: ");
            var password = Console.ReadLine();
            Console.WriteLine("Enter password again: ");
            var password2 = Console.ReadLine();

            if (password.Equals(password2))
            {
                File.Create(filePath + username + ".txt").Close();
                Console.WriteLine("Account Created!! Press any key to continue");
                Console.ReadKey(true);
                mainMenu();
            }
            else
            {
                Console.WriteLine("Passwords do not match, try again.");
                createAccount();
            }
        }

        private static void mainMenu()
        {
            Console.WriteLine("Welcome to BECU! \n Please Enter In Your Info:");
            Console.WriteLine("1.Create Account");
            Console.WriteLine("2. Log In");
            var choice = Console.ReadKey(true);
            if (choice.KeyChar.ToString() == "1")
                createAccount();
            else if (choice.KeyChar.ToString() == "2")
                logInMenu();
        }
    }
}

//still need to create an account with all information and add a deposit to it.