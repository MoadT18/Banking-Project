using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace Banking_Project
{
    public class Account : User
    {

        public int ID { get; set; }

        public int userID { get; set; }
        public int AccountTypeID { get; set; }
        public string Photo { get; set; }
        public int Balance { get; set; }

        public List<Transaction> TransactionsList { get; } = new List<Transaction>();


        private Data _data = new Data();

        public Account(int accounttypeid, string photo, int balance, string firstname, string lastname, string email, string password): base(firstname,lastname,email,password)
        {
            AccountTypeID = accounttypeid;
            Photo = photo;
            Balance = balance;
            ID = _data.insertAccount(this);

        }

        public Account() {

          
        }

        public int getBalance()
        {
            return this.Balance;
        }



      /*  public void viewHistory(ListView listView)
        {
            foreach (Transaction transaction in TransactionsList)
            {
                ListViewItem listViewItem = new ListViewItem("Transaction"); // Replace TransactionId with the appropriate property of your Transaction class
                listViewItem.SubItems.Add(transaction.Name);
                listViewItem.SubItems.Add(transaction.Amount.ToString());
                listViewItem.SubItems.Add(transaction.Date.ToString());
                listViewItem.SubItems.Add("test");
                // Add more sub-items as needed

                listView.Items.Add(listViewItem);
            }
        }*/
        public void Transfer(string name, Account sender, Account recepient, int amount, DateTime date)
        {


            
            Transaction transaction = new Transaction(name, sender.userID, recepient.userID, amount, date);


            
            recepient.TransactionsList.Add(transaction);

            sender.TransactionsList.Add(transaction);



            sender.Balance -= amount;

           

            _data.updateBalance(sender);

            

            recepient.Balance += amount;
            _data.updateBalance(recepient);
        }


        public Account GetAccount(User user)
        {
            

            return _data.GetAccount(user);

        }

        public Account GetAccount2(int userid)
        {
            return _data.GetAccount2(userid);
        }
        public void GetTransactions(Account account)
        {
           _data.GetTransactions(account);
        }

        public int updateBalance(Account account)
        {
            return _data.updateBalance(account);
        }

        public int updateAccount(string userid, string balance, string type, string photo)
        {
            return _data.updateAccount(userid, balance, type, photo);
        }

        public int updateAccount2(string userid, string photo)
        {
            return _data.updateAccount2(userid, photo);

        }

        public int deActivate(Account account)
        {
            return _data.deActivate(account);
        }
        public int deActivate2(int accountid)
        {
            return _data.deActivate2(accountid);
        }

        public bool isAccountActivated(string email)
        {
            return _data.isAccountActivated(email);
        }

        public int getAccountID(string userid)
        {
            return _data.getAccountID(userid);
        }


    }






}
