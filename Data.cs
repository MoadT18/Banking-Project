using BCrypt.Net;
using System;
using MySqlConnector;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Collections.Generic;
using System.Globalization;

namespace Banking_Project
{
    
    class Data

       {
        private int userid;
        private int accountid;
        private string connectionString =
           "datasource=ID386715_shieldpro.db.webhosting.be;" +
           "port=3306;" +
           "username=ID386715_shieldpro;" +
           "password=MoadTheGamer19;database=ID386715_shieldpro;";


        private int Insert(string query)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, connection);

            try
            {
                connection.Open();
                int result = commandDatabase.ExecuteNonQuery();
                return (int)commandDatabase.LastInsertedId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public int ExecuteQuery(string query)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            object result = command.ExecuteScalar();
            connection.Close();
            return Convert.ToInt32(result);
        }


        // USER


        public int insertUser(User user)
        {
            // Check if the user already exists
            string query = $"SELECT COUNT(*) FROM User WHERE Email = '{user.Email}';";
            int userCount = ExecuteQuery(query);

            if (userCount > 0)
            {
                // User already exists, return an appropriate value or throw an exception
                return -1; // or throw an exception indicating the user already exists
            }

            // User doesn't exist, proceed with insertion
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            query = $"INSERT INTO User (UserID, FirstName, LastName, Email, Password) " +
                    $"VALUES (NULL, '{user.FirstName}', '{user.LastName}', '{user.Email}', '{hashedPassword}');";

            return Insert(query);
        }

        public List<User> GetUsers()
        {
            List<User> userList = new List<User>();

            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand("SELECT UserID, FirstName, LastName, Email FROM User", connection);

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int ID = reader.GetInt32("UserID");
                string fname = reader.GetString("FirstName");
                string lname = reader.GetString("LastName");
                string email = reader.GetString("Email");
                User user = new User();
                user.ID = ID;
                user.FirstName = fname;
                user.LastName = lname;
                user.Email = email;
                userList.Add(user);
            }

            connection.Close();

            return userList;
        }

        public string[] GetData(string email, string password)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand($"SELECT Email, Password FROM User WHERE Email='{email}'", connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            string[] data = new string[2];

            if (reader.Read())
            {
                string hashedPassword = reader.GetString(1);
                bool passwordMatches = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
                if (passwordMatches)
                {
                    data[0] = reader.GetString(0);
                    data[1] = hashedPassword;
                }
            }
            connection.Close();
            return data;
        }


        public string[] GetData2(string email)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand($"SELECT Email FROM User WHERE Email='{email}'", connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            string[] data = new string[2];

            if (reader.Read())
            {

                data[0] = reader.GetString(0);


            }
            connection.Close();
            return data;
        }

        public string[] GetData3(string password, Account account)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand($"SELECT Password FROM User WHERE Email='{account.Email}'", connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            string[] data = new string[2];

            if (reader.Read())
            {
                string hashedPassword = reader.GetString(0);
                bool passwordMatches = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
                if (passwordMatches)
                {
                    data[0] = hashedPassword;
                }
            }
            connection.Close();
            return data;
        }

        public int updateUser(string firstname, string lastname, string email, Account account)
        {


            string query =
                $"UPDATE User SET FirstName = '{firstname}',LastName = '{lastname}',Email = '{email}' Where UserID = '{account.userID}';";


            return Insert(query);
        }

        public int updateUser2(string userid, string firstname, string lastname, string email)
        {


            string query =
                $"UPDATE User SET FirstName = '{firstname}',LastName = '{lastname}',Email = '{email}' Where UserID = '{userid}';";


            return Insert(query);
        }

        public int updatePassword(string password, Account account)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            string query =
                $"UPDATE User SET Password = '{hashedPassword}' Where UserID = '{account.userID}';";


            return Insert(query);
        }

        public int updatePassword2(string password, string email)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            string query =
                $"UPDATE User SET Password = '{hashedPassword}' Where Email = '{email}';";


            return Insert(query);
        }


        public User GetUser(string email)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand($"SELECT * FROM User WHERE Email = '{email}'", connection);
            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                userid = reader.GetInt32(0);
                string userFirstname = reader.GetString(1);
                string userLastname = reader.GetString(2);

                string userEmail = reader.GetString(3);

                string userPassword = reader.GetString(4);

                User user = new User();
                user.ID = userid;
                user.FirstName = userFirstname;
                user.LastName = userLastname;
                user.Email = userEmail;
                user.Password = userPassword;


                return user;
            }

            return null;
        }

        public bool DoesEmailExist(string email)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand($"SELECT COUNT(*) FROM User WHERE Email = '{email}'", connection);
            connection.Open();
            int count = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            return count > 0;
        }









        // ACCOUNT



        public void GetTransactions(Account account)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand($"SELECT TransactionsID, Name, SenderID, RecepientID, Amount, Date FROM Transactions WHERE SenderID = '{account.userID}' OR RecepientID = '{account.userID}'", connection);

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int tID = reader.GetInt32("TransactionsID");
                string name = reader.GetString("Name");
                int sender = reader.GetInt32("SenderID");
                int recipient = reader.GetInt32("RecepientID");
                int amount = reader.GetInt32("Amount");

                string datestring = reader.GetString("Date");
                DateTime date;
                if (DateTime.TryParseExact(datestring, "d/M/yyyy H:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    Transaction transaction = new Transaction();
                    transaction.ID = tID;
                    transaction.Name = name;
                    transaction.Sender = sender;
                    transaction.Recipient = recipient;
                    transaction.Amount = amount;
                    transaction.Date = date;

                    account.TransactionsList.Add(transaction);
                }
            }

            connection.Close();
        }



        public bool isAccountActivated(string email)
        {
            bool isActivated = false;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand($"SELECT A.isActivated FROM UserAccount AS UA INNER JOIN User as U ON UA.UserID = U.UserID INNER JOIN Account as A ON UA.AccountID = A.AccountID Where U.Email = '{email}'", connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int isActivatedValue = Convert.ToInt32(reader["isActivated"]);
                        isActivated = (isActivatedValue == 1);
                    }
                }
            }

            return isActivated;
        }

        public int updateBalance(Account account)
        {


            string query =
                $"UPDATE Account AS A SET A.Balance = '{account.Balance}' WHERE A.AccountID IN (SELECT UA.AccountID FROM UserAccount AS UA INNER JOIN User U ON UA.UserID = U.UserID WHERE U.Email = '{account.Email}');";


            return Insert(query);
        }


        public int deActivate(Account account)
        {


            string query =
                $"UPDATE Account AS A SET A.isActivated = '{0}' WHERE A.AccountID = '{account.ID}';";


            return Insert(query);
        }

        public int deActivate2(int accountid)
        {


            string query =
                $"UPDATE Account AS A SET A.isActivated = '{0}' WHERE A.AccountID = '{accountid}';";


            return Insert(query);
        }




        public int updateAccount(string userid, string balance, string type, string photo)
        {


            string query =
                $"UPDATE Account AS A INNER JOIN UserAccount AS UA ON A.AccountID = UA.AccountID INNER JOIN User AS U ON UA.UserID = U.UserID SET A.Balance = '{balance}', A.AccountTypeID = '{type}', A.Photo = '{photo}' WHERE U.UserID = '{userid}';";


            return Insert(query);
        }

        public int updateAccount2(string userid, string photo)
        {


            string query =
                $"UPDATE Account AS A INNER JOIN UserAccount AS UA ON A.AccountID = UA.AccountID INNER JOIN User AS U ON UA.UserID = U.UserID SET A.Photo = '{photo}' WHERE U.UserID = '{userid}';";


            return Insert(query);
        }


        public Account GetAccount(User user)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand($"SELECT A.AccountID, A.AccountTypeID, A.Photo, A.Balance FROM UserAccount AS UA INNER JOIN User AS U on UA.UserID = U.UserID INNER JOIN Account AS A on A.AccountID = UA.AccountID WHERE U.Email = '{user.Email}'", connection);
            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {

                accountid = reader.GetInt32(0);


                int accounttype = reader.GetInt32(1);

                //(int) reader.GetString(2);
                string photo = reader.GetString(2);

                int balance = reader.GetInt32(3);





                Account account = new Account();
                account.ID = accountid;

                account.userID = user.ID;
                account.AccountTypeID = accounttype;
                account.Photo = photo;
                account.Balance = balance;

                account.FirstName = user.FirstName;
                account.LastName = user.LastName;
                account.Email = user.Email;
                account.Password = user.Password;

                return account;
            }

            return null;
        }


        public Account GetAccount2(int userid)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand($"SELECT A.AccountTypeID, A.Photo, A.Balance FROM UserAccount AS UA INNER JOIN User AS U on UA.UserID = U.UserID INNER JOIN Account AS A on A.AccountID = UA.AccountID WHERE U.UserID = '{userid}'", connection);
            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {




                int accounttype = reader.GetInt32(0);


                string photo = reader.GetString(1);

                int balance = reader.GetInt32(2);





                Account account = new Account();



                account.AccountTypeID = accounttype;
                account.Photo = photo;
                account.Balance = balance;


                return account;
            }

            return null;
        }



        public int insertAccount(Account account)
        {


            string query = $"INSERT INTO Account (AccountID, isActivated, AccountTypeID, Photo, Balance) " +
                $"VALUES (NULL, '{1}', '{account.AccountTypeID}', '{account.Photo}', '{account.Balance}');";

            return Insert(query);
        }







        // ADMIN



        public int insertAdmin(Admin admin)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(admin.Password);

          
            // If the email doesn't exist, insert the user data
            string query = $"INSERT INTO Admin (AdminID, FirstName, LastName, Email, Password) " +
                $"VALUES (NULL, '{admin.FirstName}', '{admin.LastName}', '{admin.Email}', '{hashedPassword}');";

            return Insert(query);
        }


        public string[] GetDataAdmin(string email, string password)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand($"SELECT Email, Password FROM Admin WHERE Email='{email}'", connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            string[] data = new string[2];

            if (reader.Read())
            {
                string hashedPassword = reader.GetString(1);
                bool passwordMatches = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
                if (passwordMatches)
                {
                    data[0] = reader.GetString(0);
                    data[1] = hashedPassword;
                }
            }
            connection.Close();
            return data;
        }


        public int getAccountID(string userid)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand($"SELECT A.AccountID FROM UserAccount AS UA INNER JOIN User AS U ON UA.UserID = U.UserID INNER JOIN Account AS A ON UA.AccountID = A.AccountID Where U.UserID = '{userid}'", connection);
            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                int accountid = reader.GetInt32(0);
                return accountid;

            }

            return -1;
        }
            public bool DoesEmailExist2(string email)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand($"SELECT COUNT(*) FROM Admin WHERE Email = '{email}'", connection);
            connection.Open();
            int count = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            return count > 0;
        }

        public Admin GetAdmin(string email)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand($"SELECT * FROM Admin WHERE Email = '{email}'", connection);
            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                int adminid = reader.GetInt32(0);
                string adminFirstname = reader.GetString(1);
                string adminLastname = reader.GetString(2);

                string adminEmail = reader.GetString(3);


                string adminPassword = reader.GetString(4);

                Admin admin = new Admin();
                admin.ID = adminid;
                admin.FirstName = adminFirstname;
                admin.LastName = adminLastname;
                admin.Email = adminEmail;

                admin.Password = adminPassword;


                return admin;
            }

            return null;
        }






        // TRANSACTION




        public int insertTransaction(Transaction transaction)
        {
          
            string query = $"INSERT INTO Transactions (TransactionsID, Name, SenderID, RecepientID, Amount, Date) " +
                $"VALUES (NULL, '{transaction.Name}', '{transaction.Sender}', '{transaction.Recipient}', '{transaction.Amount}', '{transaction.Date}');";

            return Insert(query);
        }


        public string GetSenderNameFromTransaction(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand($"SELECT U.FirstName FROM Transactions AS T INNER JOIN User AS U ON T.SenderID = U.UserID Where U.UserID = '{id}'", connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            string data = "";

            if (reader.Read())
            {
                data = reader.GetString(0);
            }
            connection.Close();
            return data;
        }

        public string GetRecepientNameFromTransaction(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand($"SELECT U.FirstName FROM Transactions AS T INNER JOIN User AS U ON T.RecepientID = U.UserID Where U.UserID = '{id}'", connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            string data = "";

            if (reader.Read())
            {
                data = reader.GetString(0);
            }
            connection.Close();
            return data;
        }









        // USER ACCOUNT

        public int insertUserAccount(Account account, User user)
        {
            

            string query =
                $"INSERT INTO UserAccount (UserAccountID, AccountID, UserID)" +
                $" VALUES(NULL, '{account.ID}', '{user.ID}');";


            return Insert(query);
        }

         
        

       

        


    }
}
