using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Security;
using System.Net;
using System.Net.Mail;
using System.Security.Principal;


namespace Banking_Project
{
    public partial class loginForm : Form
    {
        private Image selectedImage;
        private string imagePath;
        private string fileName;
     
        private string securityCode;


        private string userFn;
        private string userLn;
        private string userEm;
        private string userPw;
        private string email;
        private int accounttype;










        public loginForm()
        {
            InitializeComponent();

            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pictureBox4.Width - 7, pictureBox4.Height - 3);
            Region rg = new Region(gp);
            pictureBox4.Region = rg;
        }
        
      



       
      /*  private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }*/

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        // Link label that goes to register panel
        private void registerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            registerPanel.Height = loginPanel.Height;
            loginPanel.Location = new Point(5, 5);
            panel.Location = new Point(380, 5);
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void registerPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        

        // Close button from register panel back to login panel
        private void closeButton_Click_1(object sender, EventArgs e)
        {
            registerPanel.Height = 0;
            loginPanel.Location = new Point(315, 5);
            panel.Location = new Point(5, 5);
        }



        // LOGIN BUTTON
        private void loginButton_Click(object sender, EventArgs e)
        {
            


            User user1 = new User();
            Admin admin1 = new Admin();

            Account account1 = new Account();
            string email = emailLabel.Text.ToLower();
            string password = passwordLabel.Text;

            string[] userData = user1.getUserData(email, password);
            string[] AdminData = admin1.GetDataAdmin(email, password);


            if (email == String.Empty && password == String.Empty)
            {
                //MessageBox.Show("Email and Password are empty!");
                emptymailLabel.Visible = true;
                emptypasswordLabel.Visible = true;
            }
            else if (email == String.Empty)
            {
               
                emptymailLabel.Visible = true;
                emptypasswordLabel.Visible = false;
            }
            else if (password == String.Empty)
            {
                //MessageBox.Show("Password is empty!");

                emptypasswordLabel.Visible = true;  
                emptymailLabel.Visible = false;
            }


            //USER
            else if (userData[0] != null && userData[1] != null)
            {

                
                User user = user1.GetUser(email);


                Account account = account1.GetAccount(user);

                if (!account.isAccountActivated(email))
                {
                    MessageBox.Show("Account is deactivated!");
                    return;
                }



                dashboard form2 = new dashboard(account);

                form2.Show();
                this.Hide();
                emptymailLabel.Visible = false;
                emptypasswordLabel.Visible = false;
                notcorrectLabel.Visible = false;


                


            }

            //Admin
            else if (AdminData[0] != null && AdminData[1] != null)
            {


                Admin admin = admin1.GetAdmin(email);


                 adminpanel form2 = new adminpanel(admin);

                 form2.Show();
                 this.Hide();
                 emptymailLabel.Visible = false;
                 emptypasswordLabel.Visible = false;
                 notcorrectLabel.Visible = false;


             


            }

            //Gegevens zijn niet correct
            else
            {

                //MessageBox.Show("The credentials are not correct");

                notcorrectLabel.Visible = true;
                emptymailLabel.Visible = false;
                emptypasswordLabel.Visible = false;

            }
        }


        // REGISTER BUTTON
        private void registerButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(firstnameLabel.Text))
            {
                //MessageBox.Show("Please enter your first name.");
                emptyfnLabel.Visible = true;
                emptylnLabel.Visible = false;
                remptymailLabel.Visible = false;
                remptypasswordLabel.Visible = false;
                remptypassword2Label.Visible = false;

                    
                return;
            }

            if (string.IsNullOrWhiteSpace(lastnameLabel.Text))
            {
                //MessageBox.Show("Please enter your last name.");
                emptyfnLabel.Visible = false;
                emptylnLabel.Visible = true;
                remptymailLabel.Visible = false;
                remptypasswordLabel.Visible = false;
                remptypassword2Label.Visible = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(remailLabel.Text))
            {
                //MessageBox.Show("Please enter your email address.");
                emptyfnLabel.Visible = false;
                emptylnLabel.Visible = false;
                remptymailLabel.Visible = true;
                remptypasswordLabel.Visible = false;
                remptypassword2Label.Visible = false;
                return;
            }

          

            if (string.IsNullOrWhiteSpace(rpasswordLabel.Text))
            {
                // MessageBox.Show("Please enter your password.");
                emptyfnLabel.Visible = false;
                emptylnLabel.Visible = false;
                remptymailLabel.Visible = false;
                remptypasswordLabel.Visible = true;
                remptypassword2Label.Visible = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(rpassword2Label.Text))
            {
                //MessageBox.Show("Please confirm your password.");
                emptyfnLabel.Visible = false;
                emptylnLabel.Visible = false;
                remptymailLabel.Visible = false;
                remptypasswordLabel.Visible = false;
                remptypassword2Label.Visible = true;
                return;
            }

            userFn = firstnameLabel.Text.Trim();
            userLn = lastnameLabel.Text.Trim();
            userEm = remailLabel.Text.Trim();
            userPw = rpasswordLabel.Text.Trim();
            string password2 = rpassword2Label.Text.Trim();

            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            string firstNamePattern = @"^[A-Za-z]+$";
            string lastNamePattern = @"^[A-Za-z]+$";
            string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";



            bool isValidEmail = Regex.IsMatch(userEm, emailPattern);
            bool isValidFn = Regex.IsMatch(userFn, firstNamePattern);
            bool isValidLn = Regex.IsMatch(userLn, lastNamePattern);
            bool isValidPw = Regex.IsMatch(userPw, passwordPattern);





            if (userPw != password2)
            {
                emptyfnLabel.Visible = false;
                emptylnLabel.Visible = false;
                remptymailLabel.Visible = false;
                remptypasswordLabel.Visible = true;
                remptypassword2Label.Visible = true;

                remptypasswordLabel.Text = "The two passwords do not match!";
                remptypassword2Label.Text = "The two passwords do not match!";

               /* MessageBox.Show("");*/
                return;

            }
            if (!isValidPw)
            {


                emptyfnLabel.Visible = false;
                emptylnLabel.Visible = false;
                remptymailLabel.Visible = false;
                remptypasswordLabel.Visible = false;
                remptypassword2Label.Visible = false;

                MessageBox.Show("Invalid password!\n\nThe Password needs to be:\n\n- At least one uppercase or lowercase letter\n- At least one digit\n- At least one special character from (@$!%*#?&)\n- Minimum length of 8 characters");
                return;
            }

            if (!isValidFn)
            {


                emptyfnLabel.Visible = true;
                emptylnLabel.Visible = false;
                remptymailLabel.Visible = false;
                remptypasswordLabel.Visible = false;
                remptypassword2Label.Visible = false;

                emptyfnLabel.Text = "Please enter a valid First Name!";
                return;
            }

            if (!isValidLn)
            {


                emptyfnLabel.Visible = false;
                emptylnLabel.Visible = true;
                remptymailLabel.Visible = false;
                remptypasswordLabel.Visible = false;
                remptypassword2Label.Visible = false;

                emptylnLabel.Text = "Please enter a valid Last Name!";
                return;
            }

            if (!isValidEmail)
            {


                emptyfnLabel.Visible = false;
                emptylnLabel.Visible = false;
                remptymailLabel.Visible = true;
                remptypasswordLabel.Visible = false;
                remptypassword2Label.Visible = false;

                remptymailLabel.Text = "Please enter a valid email address!";
                return;
            }

            



            User user1 = new User();
            Admin admin1 = new Admin();

            string[] user = user1.getUserData2(userEm);

            if (user1.DoesEmailExist(userEm) || admin1.DoesEmailExist2(userEm))
            {
                //MessageBox.Show("There is already an account with this email.");
                emptyfnLabel.Visible = false;
                emptylnLabel.Visible = false;
                remptymailLabel.Visible = false;
                remptypasswordLabel.Visible = false;
                remptypassword2Label.Visible = false;

                remptymailLabel.Visible = true;
                remptymailLabel.Text = "There is already an account with this email!";

                

                return;
            }
            else
            {
                userFn = userFn.Substring(0, 1).ToUpper() + userFn.Substring(1).ToLower();
                userLn = userLn.Substring(0, 1).ToUpper() + userLn.Substring(1).ToLower();
                userEm = userEm.ToLower();


                accountPanel.Height = registerPanel.Height; // account panel = register panel
                registerPanel.Location = new Point(5, 5); // register panel
                panel.Location = new Point(380, 5);
            }

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }


        //add photo button
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            openFileDialog1.Title = "Select an image file";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imagePath = openFileDialog1.FileName;

                try
                {
                    // Attempt to load the image file
                    selectedImage = Image.FromFile(imagePath);

                    // Display the selected image in a PictureBox control
                    pictureBox4.Image = selectedImage;
                }
                catch (Exception ex)
                {
                    // Display an error message if the file format is invalid
                    MessageBox.Show("Invalid file type. Please select a valid image file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        // NEXT BUTTON IN ACCOUNT PAGE
        private void button1_Click(object sender, EventArgs e)
        {
            accounttype = 0;
            if (radioButton1.Checked)
            {
                accounttype = 1;
            }
            else if (radioButton2.Checked)
            {
                accounttype = 2;
            }
            else
            {
                MessageBox.Show("You need to click wich Account type you want!");
            }

            if (selectedImage != null)
            {
                // Pass the selected image to another method for processing
                SaveImage(selectedImage);

                securityCode = GenerateSecurityCode();


                SendEmail(userEm, securityCode);


                securityPanel.Width = 373;
                securityPanel.Height = 436;
                securityPanel.Location = new Point(4, 0);
              

                // Send an email with the security code

                // Display a message to the user


            }
            else
            {
                MessageBox.Show("You need to select an image first!");
            }
        }


        private void SaveImage(Image image)
        {
            // Get the file path of the project directory
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            // Create a subdirectory called "Images" within the project directory
            string subdirectory = Path.Combine(projectDirectory, "Images");
            Directory.CreateDirectory(subdirectory);

            // Create a file name for the image based on the current date and time
            fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // Determine the file extension and image format based on the file type of the selected image
            ImageFormat imageFormat;
            switch (Path.GetExtension(imagePath).ToLower())
            {
                case ".bmp":
                    fileName += ".bmp";
                    imageFormat = ImageFormat.Bmp;
                    break;
                case ".jpeg":
                case ".jpg":
                    fileName += ".jpg";
                    imageFormat = ImageFormat.Jpeg;
                    break;
                case ".png":
                    fileName += ".png";
                    imageFormat = ImageFormat.Png;
                    break;
                default:
                    throw new ArgumentException("Invalid file type.");
            }

            // Combine the subdirectory and file name to create the full file path of the image
            string filePath = Path.Combine(subdirectory, fileName);

            // Save the image to the specified file path
            image.Save(filePath, imageFormat);
        }

        private void closeButton2_Click(object sender, EventArgs e)
        {
            accountPanel.Height = 0;   //account panel
            registerPanel.Location = new Point(315, 5); //register panel
            //panel.Location = new Point(5, 5);



        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                rpasswordLabel.UseSystemPasswordChar = false;

            }
            else
            {
                rpasswordLabel.UseSystemPasswordChar = true;

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                rpassword2Label.UseSystemPasswordChar = false;

            }
            else
            {
                rpassword2Label.UseSystemPasswordChar = true;

            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                passwordLabel.UseSystemPasswordChar = false;

            }
            else
            {
                passwordLabel.UseSystemPasswordChar = true;

            }
        }

        private string GenerateSecurityCode()
        {
            // Generate a random 6-digit security code
            Random random = new Random();
            int code = random.Next(100000, 999999);
            return code.ToString();
        }

        private void SendEmail(string recipientEmail, string code)
        {
            try
            {
                // Set up the SMTP client
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("shieldpro8@gmail.com", "iagenzndkdqkzftt"); // Replace with your email credentials

                // Create the email message
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("shieldpro8@gmail.com"); // Replace with your email address
                mailMessage.To.Add(recipientEmail);
                mailMessage.Subject = "Security Code";
                mailMessage.Body = "Your security code: " + code;

                // Send the email
                smtpClient.Send(mailMessage);





            }
            catch (Exception ex)
            {
                // Handle any errors that occurred during email sending
                MessageBox.Show("An error occurred while sending the email: " + ex.Message);
            }
        }

        private void SendEmail2(string recipientEmail)
        {
            try
            {
                // Set up the SMTP client
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("shieldpro8@gmail.com", "iagenzndkdqkzftt"); // Replace with your email credentials

                // Create the email message
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("shieldpro8@gmail.com"); // Replace with your email address
                mailMessage.To.Add(recipientEmail);
                mailMessage.Subject = "Account details";
                mailMessage.Body = $"Your account details: \n\nFirst name: {userFn}\nLast name: {userLn}\nE-mail: {userEm}";
                

                // Send the email
                smtpClient.Send(mailMessage);





            }
            catch (Exception ex)
            {
                // Handle any errors that occurred during email sending
                MessageBox.Show("An error occurred while sending the email: " + ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string enteredCode = securityText.Text;

            if (enteredCode == securityCode)
            {

                User newUser = new User(userFn, userLn, userEm, userPw);

                Account account = new Account(accounttype, fileName, 0, newUser.FirstName, newUser.LastName, newUser.Email, newUser.Password);
                Data data = new Data();
                data.insertUserAccount(account, newUser);

                SendEmail2(userEm);


                dashboard form2 = new dashboard(account);

                form2.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Incorrect security code!");
            }
        }


         //FORGOT Password
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            //Forgot panel
            forgotPanel.Width = 373;
            forgotPanel.Height = 432;

            forgotPanel.Location = new Point(4, 3);
        }

        //send request for forgot password
        private void button3_Click(object sender, EventArgs e)
        {
            User user1 = new User();

            email = emailR.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Enter email please!");
                return;
            }

                if (user1.DoesEmailExist(email)) {

                securityCode = GenerateSecurityCode();
                SendEmail(email, securityCode);

                //forgot panel2

                forgot2Panel.Width = 361;
                forgot2Panel.Height = 366;
                forgot2Panel.Location = new Point(9, 55);


            }
            else
            {
                MessageBox.Show("Email is not found in the database");
            }
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            User user1 = new User();
            string code = codeT.Text;
            string password = passwordT.Text;
            string password2 = password2T.Text;

            string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";



            bool isvalidPassword = Regex.IsMatch(password, passwordPattern);


            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Enter code please!");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Enter password please!");
                return;
            }

            if (string.IsNullOrWhiteSpace(password2))
            {
                MessageBox.Show("Enter second password please!");
                return;
            }


            if(password != password2)
            {

                MessageBox.Show("The two passwords are not correct!");
                return;
            }

            if(code != securityCode)
            {
                MessageBox.Show("The code is incorrect!");
                return;
            }

            if (!isvalidPassword)
            {
                MessageBox.Show("Invalid password!\n\nThe Password needs to be:\n\n- At least one uppercase or lowercase letter\n- At least one digit\n- At least one special character from (@$!%*#?&)\n- Minimum length of 8 characters");
            }

            else
            {
                user1.updatePassword2(password, email);
                MessageBox.Show("Password updated");

                
                loginPanel.Location = new Point(315, 5);
                panel.Location = new Point(5, 5);


                // FORGOT2 Panel
                forgot2Panel.Width = 361;
                forgot2Panel.Height = 10;

                forgot2Panel.Location = new Point(9, 411);


                //Forgot panel

                forgotPanel.Width = 361;
                forgotPanel.Height = 10;

                forgotPanel.Location = new Point(9, 411);

            }

        }


        // BACK TO Register from account
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            accountPanel.Width = 380;
            accountPanel.Height = 10;
            accountPanel.Location = new Point(0, 427);

            registerPanel.Width = 380;
            registerPanel.Height = 440;
            registerPanel.Location = new Point(0, 0);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            securityPanel.Width = 373;
            securityPanel.Height = 10;
            securityPanel.Location = new Point(4, 426);

            accountPanel.Width = 380;
            accountPanel.Height = 437;
            accountPanel.Location = new Point(0, 0);
        }

        //closes Forgot panel
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            forgotPanel.Width = 373;
            forgotPanel.Height = 10;

            forgotPanel.Location = new Point(4, 432);
        }

        private void forgotPanel_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            forgot2Panel.Width = 361;
            forgot2Panel.Height = 10;

            forgot2Panel.Location = new Point(9, 411);

            forgotPanel.Width = 373;
            forgotPanel.Height = 439;
            forgotPanel.Location = new Point(4, 3);
        }
    }
}
