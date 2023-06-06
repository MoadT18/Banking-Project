using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Banking_Project
{
    public class Transaction
    {
        public int ID { get; set; }
        public string Name { get; set;
        }

     

        public int Sender { get; set; }
        public int Recipient { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set;
        }


        




        private Data _data = new Data();

        public Transaction(string name, int sender, int recipient, int amount, DateTime date) { 
            Name = name;
            Sender = sender;
            Recipient = recipient;
            Amount = amount;
            Date = DateTime.Now;

            

            ID = _data.insertTransaction(this);


        }

        public Transaction()
        {

        }
       

        public override string ToString()
        {
            return $"Name: {Name}, Amount: {Amount}, Sender: {Sender}, Recipient: {Recipient}, Date: {Date}";
        }


        public string GetSenderNameFromTransaction(int id)
        {
            return _data.GetSenderNameFromTransaction(id);
        }

        public string GetRecepientNameFromTransaction(int id)
        {
            return _data.GetRecepientNameFromTransaction(id);
        }

    }
}
