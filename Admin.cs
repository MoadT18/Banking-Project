using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_Project
{
    public class Admin
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        private Data _data = new Data();

        public Admin(string firstname, string lastname, string email, string password) {

            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Password = password;

            ID = _data.insertAdmin(this);

        }


        public Admin() { 
        
        
        
        }

        public string[] GetDataAdmin(string email, string password)
        {
            return _data.GetDataAdmin(email, password);
        }

        public bool DoesEmailExist2(string email)
        {
            return _data.DoesEmailExist2(email);
        }

        public Admin GetAdmin(string email)
        {
            return _data.GetAdmin(email);
        }

       


    }
}

