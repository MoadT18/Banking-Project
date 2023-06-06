using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace Banking_Project
{
    public partial class dashboard : Form
    {
        private System.Drawing.Image selectedImage;
        private string imagePath;
        private string fileName;


        private int amount;
        private string message;
        private Account recepientA;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn

            (

            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse

            );



        Account account = new Account();
        User user = new User();


        public dashboard(Account newAccount)
        {
            InitializeComponent();

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            navPanel.Height = dashboardButton.Height;
            navPanel.Top = dashboardButton.Top;
            navPanel.Left = dashboardButton.Left;
            dashboardButton.BackColor = Color.FromArgb(46, 51, 73);

            //Maakt de profile picture rond
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pictureBox1.Width - 7, pictureBox1.Height - 3);
            Region rg = new Region(gp);
            pictureBox1.Region = rg;


            //Settings pf
            pictureBox3.Size = new Size(110, 110);
            int radius = 50;
            int diameter = radius * 2;
            pictureBox3.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, pictureBox3.Width + 1, pictureBox3.Height + 1, diameter, diameter));


            account = newAccount;

            ShowImage();

            labelName.Text = account.FirstName;

            balanceLabel.Text = "€" + account.Balance.ToString();

            welcomeT.Text = "Welcome, " + account.FirstName.ToString() + "!";

            if (account.AccountTypeID == 1)
            {
                accounTT.Text = "Current Account";
            }
            else
            {
                accounTT.Text = "Savings Account";

            }

            account.GetTransactions(account);

            addTransactionsToListView();




            sFN.Text = account.FirstName;
            sLN.Text = account.LastName;
            sEM.Text = account.Email;



        }

        

        public void addTransactionsToListView()
        {
            listView1.Items.Clear();

            foreach (Transaction transaction in account.TransactionsList)
            {
                ListViewItem listViewItem = new ListViewItem(transaction.ID.ToString());
                listViewItem.SubItems.Add(transaction.Name);

                string senderName = transaction.GetSenderNameFromTransaction(transaction.Sender);
                string recipientName = transaction.GetRecepientNameFromTransaction(transaction.Recipient);

                listViewItem.SubItems.Add(senderName);
                listViewItem.SubItems.Add(recipientName);

                //ListViewItem.ListViewSubItem amountSubItem = new ListViewItem.ListViewSubItem(listViewItem, transaction.Amount.ToString());
                listViewItem.SubItems.Add(transaction.Amount.ToString());

                if (account.userID == transaction.Sender)
                {
                    //listView1.ForeColor = Color.Red;

                    //listViewItem.ForeColor = Color.Red;

                    listViewItem.ForeColor = Color.Red;

                }


                else
                {
                    listViewItem.ForeColor = Color.Green;
                }

                listViewItem.SubItems.Add(transaction.Date.ToString());

                listView1.Items.Add(listViewItem);
            }
        }




        private void ShowImage()
        {
            // Get the file path of the project directory
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            // Combine the project directory and the Images folder name to create the full path of the folder
            string imagesDirectory = Path.Combine(projectDirectory, "Images");

            // Create a file path for the "moad" image
            string imagePath = Path.Combine(imagesDirectory, account.Photo.ToString());

            // Check if the image file exists
            if (File.Exists(imagePath))
            {
                // Load the image file into an Image object
                System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath);

                // Display the image in a PictureBox control
                pictureBox1.Image = image;
                pictureBox3.Image = image;

            }
            else
            {
                MessageBox.Show("The image could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dashboardButton_Click(object sender, EventArgs e)
        {
            navPanel.Height = dashboardButton.Height;
            navPanel.Top = dashboardButton.Top;
            navPanel.Left = dashboardButton.Left;
            dashboardButton.BackColor = Color.FromArgb(46, 51, 73);



            //Transaction panel
            transactionsPanel.Width = 761;
            transactionsPanel.Height = 10;
            transactionsPanel.Location = new Point(188, 567);

            //Transfer panel
            transferPanel.Width = 1015;
            transferPanel.Height = 10;
            transferPanel.Location = new Point(252, 700);


            //Settings panel
            settingsPanel.Width = 763;
            settingsPanel.Height = 10;
            settingsPanel.Location = new Point(187, 564);

            //changepassword panel
            changepasswordPanel.Width = 509;
            changepasswordPanel.Height = 10;
            changepasswordPanel.Location = new Point(190, 561);

            //delete panel
            deletePanel.Width = 732;
            deletePanel.Height = 10;
            deletePanel.Location = new Point(20, 526);


            //transfer confir
            transferconPanel.Width = 732;
            transferconPanel.Height = 10;
            transferconPanel.Location = new Point(20, 539);


        }

        private void transactionsButton_Click(object sender, EventArgs e)
        {
            navPanel.Height = transactionsButton.Height;
            navPanel.Top = transactionsButton.Top;

            transactionsButton.BackColor = Color.FromArgb(46, 51, 73);


            transactionsPanel.Width = 761;
            transactionsPanel.Height = 577;

            transactionsPanel.Location = new Point(188, 0);


            //Transfer panel
            transferPanel.Width = 1015;
            transferPanel.Height = 10;
            transferPanel.Location = new Point(252, 700);


            //Settings panel
            settingsPanel.Width = 763;
            settingsPanel.Height = 10;
            settingsPanel.Location = new Point(187, 564);

            //changepassword panel
            changepasswordPanel.Width = 509;
            changepasswordPanel.Height = 10;
            changepasswordPanel.Location = new Point(190, 561);

            //delete panel
            deletePanel.Width = 732;
            deletePanel.Height = 10;
            deletePanel.Location = new Point(20, 526);

            //transfer confir
            transferconPanel.Width = 732;
            transferconPanel.Height = 10;
            transferconPanel.Location = new Point(20, 539);


        }

        private void transferButton_Click(object sender, EventArgs e)
        {
            navPanel.Height = transferButton.Height;
            navPanel.Top = transferButton.Top;

            transferButton.BackColor = Color.FromArgb(46, 51, 73);


            transferPanel.Width = 761;
            transferPanel.Height = 577;

            transferPanel.Location = new Point(187, 0);


            //Transaction panel
            transactionsPanel.Width = 761;
            transactionsPanel.Height = 10;
            transactionsPanel.Location = new Point(188, 567);


            //Settings panel
            settingsPanel.Width = 763;
            settingsPanel.Height = 10;
            settingsPanel.Location = new Point(187, 564);


            //changepassword panel
            changepasswordPanel.Width = 509;
            changepasswordPanel.Height = 10;
            changepasswordPanel.Location = new Point(190, 561);

            //delete panel
            deletePanel.Width = 732;
            deletePanel.Height = 10;
            deletePanel.Location = new Point(20, 526);

            //transfer confir
            transferconPanel.Width = 732;
            transferconPanel.Height = 10;
            transferconPanel.Location = new Point(20, 539);


        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            navPanel.Height = settingsButton.Height;
            navPanel.Top = settingsButton.Top;

            settingsButton.BackColor = Color.FromArgb(46, 51, 73);


            
            settingsPanel.Width = 763;
            settingsPanel.Height = 572;
            settingsPanel.Location = new Point(187, 2);



            //Transaction panel
            transactionsPanel.Width = 761;
            transactionsPanel.Height = 10;
            transactionsPanel.Location = new Point(188, 567);



            //Transfer panel
            transferPanel.Width = 1015;
            transferPanel.Height = 10;
            transferPanel.Location = new Point(252, 700);

            //changepassword panel
            changepasswordPanel.Width = 509;
            changepasswordPanel.Height = 10;
            changepasswordPanel.Location = new Point(190, 561);


            //delete panel
            deletePanel.Width = 732;
            deletePanel.Height = 10;
            deletePanel.Location = new Point(20, 526);


            //transfer confir
            transferconPanel.Width = 732;
            transferconPanel.Height = 10;
            transferconPanel.Location = new Point(20, 539);

        }

        private void dashboardButton_Leave(object sender, EventArgs e)
        {
            dashboardButton.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void transactionsButton_Leave(object sender, EventArgs e)
        {
            transactionsButton.BackColor = Color.FromArgb(24, 30, 54);

        }

        private void transferButton_Leave(object sender, EventArgs e)
        {
            transferButton.BackColor = Color.FromArgb(24, 30, 54);

        }

        private void settingsButton_Leave(object sender, EventArgs e)
        {
            settingsButton.BackColor = Color.FromArgb(24, 30, 54);

        }


        //Close app
        private void button1_Click_1(object sender, EventArgs e)
        {
            account = null;
            loginForm form = new loginForm();

            form.Show();
            this.Hide();
        }

        private void transactionsPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        //Transfer button
        private void trButton_Click(object sender, EventArgs e)
        {
            {
                

                if (string.IsNullOrWhiteSpace(tmessageLabel.Text))
                {
                    MessageBox.Show("Please enter the message");
                    return;
                }
                if (string.IsNullOrWhiteSpace(temailLabel.Text))
                {
                    MessageBox.Show("Please enter the recepient email");
                    return;
                }
                if (string.IsNullOrWhiteSpace(tamountLabel.Text))
                {
                    MessageBox.Show("Please anther the amount");
                    return;
                }

                if(temailLabel.Text == account.Email)
                {
                    MessageBox.Show("You can't send yourself money!");
                    return;
                }

                if (user.DoesEmailExist(temailLabel.Text))
                {
                    User recepientU = user.GetUser(temailLabel.Text);


                    recepientA = account.GetAccount(recepientU);

                    message = tmessageLabel.Text;

                    amount = Convert.ToInt32(tamountLabel.Text);





                    if (amount > account.Balance || amount <= 0)
                    {
                        MessageBox.Show("Amount can't be higher than balance or below 1");
                        return;
                    }
                    else
                    {

                        transferconPanel.Width = 732;
                        transferconPanel.Height = 479;
                        transferconPanel.Location = new Point(20, 70);


                        transfermessage.Text = $"Are u sure you want to\ntransfer €{amount} to {recepientA.FirstName}?";


                        
                    }

                   


                }
                else
                {
                    MessageBox.Show("Email does not exist in the database");
                    return;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sFN.ReadOnly = false;
            sFN.BackColor = Color.White;
        }

        //Change password Button
        private void button7_Click(object sender, EventArgs e)
        {
            changepasswordPanel.Width = 509;
            changepasswordPanel.Height = 474;
            changepasswordPanel.Location = new Point(190, 97);

        }

        private void lnEdit_Click(object sender, EventArgs e)
        {
            sLN.ReadOnly = false;
            sLN.BackColor = Color.White;
        }

        private void emEdit_Click(object sender, EventArgs e)
        {
            sEM.ReadOnly = false;
            sEM.BackColor = Color.White;
        }


        private void SaveImage(System.Drawing.Image image)
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


        public void GeneratePDF()
        {
            Document document = new Document();

            // Set the path where the PDF file will be saved
            string path = "transaction_list.pdf";

            // Create a new PDF writer
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));

            document.Open();

            // Create a new table with the same number of columns as the ListView
            PdfPTable table = new PdfPTable(listView1.Columns.Count);

            // Add the column headers to the table
            foreach (ColumnHeader column in listView1.Columns)
            {
                table.AddCell(column.Text);
            }

            // Add the items from the ListView to the table
            foreach (ListViewItem item in listView1.Items)
            {
                foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                {
                    table.AddCell(subItem.Text);
                }
            }

            // Add the table to the document
            document.Add(table);

            document.Close();

            // Open the file for download
            System.Diagnostics.Process.Start(path);
        }


        //SAVE BUTTON
        private void button6_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrWhiteSpace(sFN.Text))
            {
                MessageBox.Show("Please enter the first name.");


                return;
            }
            if (string.IsNullOrWhiteSpace(sLN.Text))
            {
                MessageBox.Show("Please enter the last name.");


                return;
            }
            if (string.IsNullOrWhiteSpace(sEM.Text))
            {
                MessageBox.Show("Please enter the email.");


                return;
            }


            else
            {

                if (selectedImage != null)
                {


                    SaveImage(selectedImage);
                }

                    string firstname = sFN.Text;
                    string lastname = sLN.Text;
                    string email = sEM.Text;

                    user.updateUser(firstname, lastname, email, account);



                if (string.IsNullOrWhiteSpace(fileName))
                {
                    account.updateAccount2(account.userID.ToString(), account.Photo);
                }
                else
                {
                    account.updateAccount2(account.userID.ToString(), fileName);
                    account.Photo = fileName;
                    ShowImage();
                }

                MessageBox.Show("User updated!");

                sFN.ReadOnly = true;
                    sLN.ReadOnly = true;
                    sEM.ReadOnly = true;

                    account.FirstName = firstname;
                    account.LastName = lastname;
                    account.Email = email;
                    
                    labelName.Text = account.FirstName;

                    sFN.BackColor = Color.FromArgb(46, 51, 73);
                    sLN.BackColor = Color.FromArgb(46, 51, 73);
                    sEM.BackColor = Color.FromArgb(46, 51, 73);


                
            }


        }

        // Change password button
        private void cpasswordButton_Click(object sender, EventArgs e)
        {
            User user1 = new User();


            if (string.IsNullOrWhiteSpace(cpasswordLabel.Text)){

                MessageBox.Show("Password is empty");
                return;
            }

            if (string.IsNullOrWhiteSpace(cpassword2Label.Text)) {

                MessageBox.Show("Password is empty");
                return;
            }



            string password = cpasswordLabel.Text.Trim();
            string password2 = cpassword2Label.Text.Trim();

            string[] userData = user1.GetUserData3(password, account);

            string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";



           
            bool isValidPw = Regex.IsMatch(password2, passwordPattern);


            if (!isValidPw)
            {
                MessageBox.Show("The password is not valid");
                return;
            }


            if (userData[0] == null)
            {
                MessageBox.Show("The current password is not correct");
                return;
            }

            else
            {
                user.updatePassword(password2, account);
                MessageBox.Show("Password updated!");

                account = null;
                loginForm form = new loginForm();

                form.Show();
                this.Hide();
            }
        }

        // GO TO DELETE ACCOUNT
        private void button8_Click(object sender, EventArgs e)
        {
            deletePanel.Width = 732;
            deletePanel.Height = 452;

            deletePanel.Location = new Point(20, 84);

        }

        private void sAD_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }


        //Terug naar settings panel
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            navPanel.Height = settingsButton.Height;
            navPanel.Top = settingsButton.Top;

            settingsButton.BackColor = Color.FromArgb(46, 51, 73);



            settingsPanel.Width = 763;
            settingsPanel.Height = 572;
            settingsPanel.Location = new Point(187, 2);



            //Transaction panel
            transactionsPanel.Width = 761;
            transactionsPanel.Height = 10;
            transactionsPanel.Location = new Point(188, 567);



            //Transfer panel
            transferPanel.Width = 1015;
            transferPanel.Height = 10;
            transferPanel.Location = new Point(252, 700);

            //changepassword panel
            changepasswordPanel.Width = 509;
            changepasswordPanel.Height = 10;
            changepasswordPanel.Location = new Point(190, 561);
        }

        //LOG OUT
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            account = null;
            loginForm form = new loginForm();

            form.Show();
            this.Hide();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        //DELETE ACCOUNT
        private void button2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("U wordt nu uitgelogd!");
            account.deActivate(account);
            account = null;
            loginForm form = new loginForm();

            form.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
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
                    selectedImage = System.Drawing.Image.FromFile(imagePath);

                    // Display the selected image in a PictureBox control
                    pictureBox3.Image = selectedImage;
                }
                catch (Exception ex)
                {
                    // Display an error message if the file format is invalid
                    MessageBox.Show("Invalid file type. Please select a valid image file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        // NO BUTTON DELETE
        private void button3_Click(object sender, EventArgs e)
        {
            //delete panel
            deletePanel.Width = 732;
            deletePanel.Height = 10;
            deletePanel.Location = new Point(20, 526);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            account.Transfer(message, account, recepientA, amount, new DateTime());

            balanceLabel.Text = "€" + account.Balance.ToString();

            addTransactionsToListView();


            temailLabel.Text = "";
            tamountLabel.Text = "";
            tmessageLabel.Text = "";
            MessageBox.Show("Transaction send");

            transferconPanel.Width = 732;
            transferconPanel.Height = 10;
            transferconPanel.Location = new Point(20, 539);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            transferconPanel.Width = 732;
            transferconPanel.Height = 10;
            transferconPanel.Location = new Point(20, 539);
        }

        private void downloadT_Click(object sender, EventArgs e)
        {
            GeneratePDF();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox3.Checked)
            {
                cpasswordLabel.UseSystemPasswordChar = false;

            }
            else
            {
                cpasswordLabel.UseSystemPasswordChar = true;

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                cpassword2Label.UseSystemPasswordChar = false;

            }
            else
            {
                cpassword2Label.UseSystemPasswordChar = true;

            }
        }
    }
}
