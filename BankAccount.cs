using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class BankAccount
    {
        //public Dictionary<String, Person> accountHolder = new Dictionary<String, Person>();
        public List<Person> personInfo = new List<Person>();
        public void AddPerson(Person pn)
        {

            this.personInfo.Add(pn);
        }
        public void RemovePerson(Person pn)
        {
            this.personInfo.Remove(pn);
        }
        public string numGenrator()
        {
            Random ran = new Random();
            int num = ran.Next(0, 100);
            return num.ToString();
        }
    }
}
