using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bank
{
    public class Person
    {
        public string name;
        string address;
        int phoneNum;
        public Account savingAccount;
        public List<Account> FdAccount = new List<Account>() ;
        public Person(string name, string address, int phoneNum) {
          
                this.name = name;
                this.address = address;
                this.phoneNum = phoneNum;
        }
      
        public void CreateFD(int amount)
        {
            if (this.savingAccount.Balance() > amount)
            {
                this.savingAccount.WithDraw(amount);
                Account fd = new Account(amount, AccountType.FD);
                this.FdAccount.Add(fd);
                Console.WriteLine("FD created Successfully");

            }
            else { 
                Console.WriteLine("you don't have enough amount in your saving account");
            }
        }
        public override string ToString()
        {
            return this.name.ToString();
        }
    }
}
