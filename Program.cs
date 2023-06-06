using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banking_Project
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()

        {



            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new loginForm());

            

            //Data data = new Data();

           

           /* User hafsa = new User("Hafsa", "Touhafi", "hafsa.touahfi1@gmail.com", "2830 Willebroek", "hafsa12", "photo.png", 0);


            moad.Transfer("Test", 10, moad, hafsa, DateTime.Now);


            moad.viewHistory();
            hafsa.viewHistory();


            Console.WriteLine(moad.getBalance());

            Console.WriteLine(hafsa.getBalance());*/
            /* Account account = new Account("password", "photo.jpg", 1000);

             // Insert the account into the database and get the AccountId
             */

            //Data data = new Data();

            
            //data.insertUser(moad);


            // Create a new user and insert it into the database
            // User user = new User("John", "Doe", "johndoe@email.com", "123 Main St", accountId);
            


        }
    }
}
