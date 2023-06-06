using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_Project
{
    public class User
    {


        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }


        private Data _data = new Data();

        public User(string firstname, string lastname, string email, string password)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Password = password;

            ID = _data.insertUser(this);

        }


        public User()
        {

        }

        public List<User> GetUsers()
        {
            return _data.GetUsers();
        }


        public string[] getUserData(string email, string password)
        {
           


            return _data.GetData(email, password);
        }


        public string[] getUserData2(string email)
        {
           


            return _data.GetData2(email);
        }

        public string[] GetUserData3(string password, Account account)
        {
            return _data.GetData3(password, account);
        }

        public User GetUser(string email)
        {
           

            return _data.GetUser(email);
        }

        public int updateUser(string firstname, string lastname, string email, Account account)
        {
            return _data.updateUser(firstname, lastname, email, account);
        }

        public int updateUser2(string userid, string firstname, string lastname, string email)
        {
            return _data.updateUser2(userid, firstname, lastname, email);
        }

        public int updatePassword(string password, Account account)
        {
            return _data.updatePassword(password, account);
        }
        public int updatePassword2(string password, string email)
        {
            return _data.updatePassword2(password, email);
        }

        public bool DoesEmailExist(string email)
        {
            return _data.DoesEmailExist(email);
        }




    }
}
