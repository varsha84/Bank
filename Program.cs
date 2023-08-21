using javax.management.loading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Serilog;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.Json;
using System.Transactions;

namespace Bank
{
    internal class Program
    {
        

        public static string numGenrator()
        {
            Random ran = new Random();
            int num = ran.Next(0, 100);
            return num.ToString();
        }
        
        static void Main(string[] args)
        {
            //var loggerFactory = new LoggerFactory();
            // ILogger logger = loggerFactory.CreateLogger<Program>();
            //logger.LogInformation("Logging information.");
            Log.Logger = (Serilog.ILogger)new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(@"C:\work\Csharp_projects\Log_.txt", rollingInterval: RollingInterval.Minute)
                .CreateLogger();
            Log.Information("log information");
            


            BankAccount bn = new BankAccount();
           
            while (true)
            {
                Console.WriteLine("Choose option below");
                Console.WriteLine("1.Open acoount");
                Console.WriteLine("2.Deposit money");
                Console.WriteLine("3.Withdraw money");
                Console.WriteLine("4.Check Balance");
                Console.WriteLine("5.Create FD");
                Console.WriteLine("6.exit");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        {
                            Console.WriteLine("enter person Name");
                            var name = Console.ReadLine();
                            if (string.IsNullOrEmpty(name))
                            {
                                Console.WriteLine("not a valid name ");
                                break;
                            }

                            Console.WriteLine("enter Address");
                            var address = Console.ReadLine();
                            if (string.IsNullOrEmpty(address))
                            {
                                Console.WriteLine("not a valid address ");
                                break;
                            }
                            Console.WriteLine("enter your 10 digit phone number");
                            var phoneNum = Console.ReadLine();

                            
                            int validPhoneNum;
                           
                            if (phoneNum?.Length > 1) 
                            {
                                if(int.TryParse(phoneNum, out validPhoneNum))
                                {
                                    Person pn = new Person(name, address, validPhoneNum);
                                    bn.AddPerson(pn);
                                    string fileName = @"C:\work\Csharp_projects\classData.json";
                                    Account ac = new Account();
                                    pn.savingAccount = ac;
                                    Console.WriteLine(" {0} account is open in bank ", name);
                                    //var opt = new JsonSerializerOptions() { WriteIndented = true };
                                    string jsonString = JsonSerializer.Serialize(bn);//JsonUtility.ToJson(bn, true);//JsonSerializer.Serialize<BankAccount>(bn, true);

                                   File.WriteAllText(fileName, jsonString);
                                }
                                else
                                {
                                    Console.WriteLine("not a valid number");
                                }                                
                            }
                            else
                            {
                                Console.WriteLine("enter valid value");
                            }
                        }
                        break;
                    case "2":
                        {
                            Console.WriteLine("enter name of person");
                            var name = Console.ReadLine();
                            Console.WriteLine("Enter your amount for deposit");
                            var amount = Console.ReadLine();
                            int validAmount;
                            var list = bn.personInfo;
                            if (!string.IsNullOrEmpty(name))
                            {
                                if (int.TryParse(amount, out validAmount))
                                {
                                    foreach (var p in list)
                                    {
                                        if (p.name == name)
                                        {
                                            var deposit = p.savingAccount.Deposit(validAmount);
                                            Console.WriteLine("your {0} is deposit in bank", validAmount);
                                        }
                                        else
                                        {
                                            Console.WriteLine("could not find");
                                        }
                                    }
                                }
                                Console.WriteLine("enter amount in numbers");
                            }
                            else
                            {
                                Console.WriteLine("please enter name in right format");
                            }
                        }
                        break;

                    case "3":
                        {
                            Console.WriteLine("enter name of person");
                            var name = Console.ReadLine();
                            Console.WriteLine("enter the amount for withdraw");
                            var amount = Console.ReadLine();
                            int validAmount;
                            if (!string.IsNullOrEmpty(name))
                            {
                                if (int.TryParse(amount, out validAmount))
                                {
                                    foreach (var p in bn.personInfo)
                                    {
                                        if (p.name == name)
                                        {
                                            if (p.savingAccount.Balance() >= validAmount)
                                            {
                                                var withdraw = p.savingAccount.WithDraw(validAmount);
                                                Console.WriteLine("your new balance is : {0}", withdraw);
                                            }
                                            else
                                            {
                                                Console.WriteLine("you dont have enough amount");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("could not find");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("please enter withdraw in numbers");
                                }
                            }
                            else
                            {
                                Console.WriteLine("please enter name in right format");
                                break;
                            }

                            
                            
                        }

                        break;
                        
                    case "4":
                        {
                            Console.WriteLine("enter your name");
                            var name = Console.ReadLine();
                            foreach( var p in bn.personInfo)
                            {
                                if(p.name == name)
                                {
                                    var personAmount = p.savingAccount.Balance();

                                    Console.WriteLine("your cureent balance amount is {0}", personAmount);
                                }
                            }
                        }
                        break;
                    case "5":
                        {
                            
                            Console.WriteLine("enter your name");
                            var name = Console.ReadLine();
                            Console.WriteLine("enter your FD amount");
                            var FDAmount = Console.ReadLine();
                            if (FDAmount != null)
                            {
                                try
                                {
                                    int amount = int.Parse(FDAmount);
                                    foreach (var p in bn.personInfo)
                                    {
                                        if (p.name == name)
                                        {
                                            p.CreateFD(amount);
                                        }
                                    }

                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine("enter correct amount");
                                }
                                
                            }
                            

                        }
                        
                        break;
                    case "6":
                        {
                            Console.WriteLine("exit program");
                            System.Environment.Exit(0);

                        }
                        break;
                    default:
                        {
                            Console.WriteLine("please choose the right option");
                        }
                        break;
                }

            }

        }
    }
}