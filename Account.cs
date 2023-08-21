using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
   public enum AccountType{
        SAVING,
        FD
    };
    public class Account
    {
        int amount;

        AccountType acType;
        public Account(int amount=500, AccountType accountType= AccountType.SAVING)
        {
            this.amount = amount;
            this.acType = accountType;
        }
        public int Deposit(int amount)
        {
            return this.amount += amount;
        }
        /*public int Amount
        {
            get { return amount; }
            set { this.amount = value; }


        }*/
        public int WithDraw(int amount)
        {
            return this.amount -= amount;
        }
        public int Balance()
        {
            return this.amount;
        }
    }
}
